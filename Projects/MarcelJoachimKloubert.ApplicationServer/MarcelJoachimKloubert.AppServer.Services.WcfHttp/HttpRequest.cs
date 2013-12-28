// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.IO;
using MarcelJoachimKloubert.CLRToolbox.Net;
using MarcelJoachimKloubert.CLRToolbox.Net.Http;
using BclHttpUtility = System.Web.HttpUtility;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    internal sealed partial class HttpRequest : HttpRequestBase
    {
        #region Fields (15)

        private readonly string _CONTENT_TYPE;
        private readonly IReadOnlyDictionary<string, IFile> _FILES;
        private readonly IReadOnlyDictionary<string, string> _GET;
        private readonly IReadOnlyDictionary<string, string> _HEADERS;
        private readonly Message _MESSAGE;
        private readonly string _METHOD;
        private readonly IReadOnlyDictionary<string, string> _POST;
        private readonly HttpRequestMessageProperty _PROPERTY;
        private static readonly Regex _REGEX_MULTIPART_BOUNDARY = new Regex(pattern: @"^([\s]*)(multipart\/form-data)([\s]*)(;)([\s]*)(boundary)([\s]*)(=)(.*?)$",
                                                                            options: RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private readonly ITcpAddress _REMOTE_ADDRESS;
        private readonly IReadOnlyDictionary<string, string> _REQUEST;
        private readonly WcfHttpServer _SERVER;
        private readonly DateTimeOffset _TIME;
        private readonly IPrincipal _USER;
        private readonly MessageEncoder _WEB_ENCODER = WcfHttpServerService.CreateWebMessageBindingEncoder()
                                                                           .CreateMessageEncoderFactory()
                                                                           .Encoder;

        #endregion Fields

        #region Constructors (1)

        internal HttpRequest(DateTimeOffset time,
                             HttpRequestMessageProperty property,
                             Message message,
                             WcfHttpServer server)
        {
            this._TIME = time;
            this._PROPERTY = property;
            this._MESSAGE = message;
            this._SERVER = server;

            this._METHOD = (this._PROPERTY.Method ?? string.Empty).ToUpper().Trim();
            if (this._METHOD == string.Empty)
            {
                this._METHOD = null;
            }

            //  address of remote client
            try
            {
                var messageProperties = OperationContext.Current.IncomingMessageProperties;
                var endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                this._REMOTE_ADDRESS = new SimpleTcpAddress()
                {
                    Address = endpointProperty.Address,
                    Port = endpointProperty.Port,
                };
            }
            catch
            {
                // ignore here
            }

            // requesting user
            try
            {
                ServiceSecurityContext context = ServiceSecurityContext.Current;
                if (context != null)
                {
                    var finder = this._SERVER.PrincipalFinder;
                    if (finder != null)
                    {
                        this._USER = finder(context.PrimaryIdentity);
                    }
                }
            }
            catch
            {
                // ignore here
            }

            // request headers
            {
                var headers = new ConcurrentDictionary<string, string>(EqualityComparerFactory.CreateHttpKeyComparer());
                foreach (var key in this._PROPERTY.Headers.AllKeys)
                {
                    headers[key] = this._PROPERTY.Headers[key];
                }

                this._HEADERS = new TMReadOnlyDictionary<string, string>(headers);
            }

            // content type
            this._HEADERS
                .TryGetValue("content-type",
                             out this._CONTENT_TYPE);
            if (string.IsNullOrWhiteSpace(this._CONTENT_TYPE))
            {
                this._CONTENT_TYPE = null;
            }
            else
            {
                this._CONTENT_TYPE = this._CONTENT_TYPE.ToLower().Trim();
            }

            // GET
            this._GET = new TMReadOnlyDictionary<string, string>(ExtractVarsFromQueryString(this._PROPERTY.QueryString));

            // POST
            {
                IDictionary<string, string> postVars = null;
                IDictionary<string, IFile> files = null;

                if (this.Method == "POST")
                {
                    try
                    {
                        if (!this.ProcessMultipartData(ref postVars, ref files))
                        {
                            // no uploaded files

                            using (var reader = new StreamReader(this.GetBody(), Encoding.ASCII))
                            {
                                postVars = ExtractVarsFromQueryString(reader.ReadToEnd());
                            }
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }

                this._POST = new TMReadOnlyDictionary<string, string>(postVars ??
                                                                      new ConcurrentDictionary<string, string>(EqualityComparerFactory.CreateHttpKeyComparer()));

                this._FILES = new TMReadOnlyDictionary<string, IFile>(files ??
                                                                      new ConcurrentDictionary<string, IFile>(EqualityComparerFactory.CreateHttpKeyComparer()));
            }

            // REQUEST
            {
                var vars = new ConcurrentDictionary<string, string>(EqualityComparerFactory.CreateHttpKeyComparer());

                // first copy GET
                foreach (var g in this._GET)
                {
                    vars[g.Key] = g.Value;
                }

                // then set POST vars and overwrite existing ones
                foreach (var p in this._POST)
                {
                    vars[p.Key] = p.Value;
                }

                this._REQUEST = new TMReadOnlyDictionary<string, string>(vars);
            }
        }

        #endregion Constructors

        #region Properties (11)

        public override Uri Address
        {
            get { return this._MESSAGE.Headers.To; }
        }

        public override string ContentType
        {
            get { return this._CONTENT_TYPE; }
        }

        public override IReadOnlyDictionary<string, IFile> Files
        {
            get { return this._FILES; }
        }

        public override IReadOnlyDictionary<string, string> GET
        {
            get { return this._GET; }
        }

        public override IReadOnlyDictionary<string, string> Headers
        {
            get { return this._HEADERS; }
        }

        public override string Method
        {
            get { return this._METHOD; }
        }

        public override IReadOnlyDictionary<string, string> POST
        {
            get { return this._POST; }
        }

        public override ITcpAddress RemoteAddress
        {
            get { return this._REMOTE_ADDRESS; }
        }

        public override IReadOnlyDictionary<string, string> REQUEST
        {
            get { return this._REQUEST; }
        }

        public override DateTimeOffset Time
        {
            get { return this._TIME; }
        }

        public override IPrincipal User
        {
            get { return this._USER; }
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (1) 

        public override Stream GetBody()
        {
            var result = new MemoryStream();
            try
            {
                this._WEB_ENCODER
                    .WriteMessage(this._MESSAGE, result);

                result.Position = 0;
            }
            catch
            {
                result.Dispose();

                throw;
            }

            return result;
        }
        // Private Methods (2) 

        private static IDictionary<string, string> ExtractVarsFromQueryString(IEnumerable<char> queryStr)
        {
            var result = new ConcurrentDictionary<string, string>(EqualityComparerFactory.CreateHttpKeyComparer());

            try
            {
                var coll = BclHttpUtility.ParseQueryString(queryStr.AsString() ?? string.Empty);
                foreach (var key in coll.AllKeys)
                {
                    result[key ?? string.Empty] = coll[key];
                }
            }
            catch
            {
                // ignore
            }

            return result;
        }

        private bool ProcessMultipartData(ref IDictionary<string, string> postVars, ref IDictionary<string, IFile> files)
        {
            bool? result = null;

            try
            {
                var match = _REGEX_MULTIPART_BOUNDARY.Match(this.ContentType ?? string.Empty);
                if (match.Success)
                {
                    postVars = new ConcurrentDictionary<string, string>(EqualityComparerFactory.CreateHttpKeyComparer());
                    files = new ConcurrentDictionary<string, IFile>(EqualityComparerFactory.CreateHttpKeyComparer());

                    var parser = new HttpMultipartContentParser(this.GetBodyData(),
                                                                Encoding.ASCII.GetBytes("--" + match.Groups[9].Value));

                    foreach (var part in parser.PARTS
                                               .Where(p => !string.IsNullOrWhiteSpace(p.NAME)))
                    {
                        try
                        {
                            var data = part.DATA ?? new byte[0];

                            if (string.IsNullOrWhiteSpace(part.FILE_NAME))
                            {
                                // POST data

                                postVars[part.NAME] = Encoding.ASCII.GetString(data);
                            }
                            else
                            {
                                // uploaded file
                                var newFile = new SimpleFile();
                                newFile.ContentType = string.IsNullOrWhiteSpace(part.CONTENT_TYPE) ? "application/octet-stream" : part.CONTENT_TYPE.ToLower().Trim();
                                newFile.Name = part.FILE_NAME.Trim();
                                newFile.SetData(data);

                                files[part.NAME] = newFile;
                            }
                        }
                        catch
                        {
                            // ignore
                        }
                    }

                    result = true;
                }
            }
            catch
            {
                // ignore
            }

            return result ?? false;
        }

        #endregion Methods
    }
}
