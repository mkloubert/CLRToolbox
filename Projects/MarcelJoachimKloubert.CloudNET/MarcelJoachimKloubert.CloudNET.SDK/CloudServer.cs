// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using MarcelJoachimKloubert.CloudNET.SDK.IO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MarcelJoachimKloubert.CloudNET.SDK
{
    /// <summary>
    /// Handles a connection to a cloud server.
    /// </summary>
    public sealed class CloudServer
    {
        #region Fields (2)

        private readonly CloudFileSystem _FILE_SYSTEM;
        private readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudServer" /> class.
        /// </summary>
        public CloudServer()
        {
            this._FILE_SYSTEM = new CloudFileSystem(this, this._SYNC);
        }

        #endregion Constructors

        #region Properties (5)

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        public ICredentials Credentials
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the object that manages the file system of that server.
        /// </summary>
        public CloudFileSystem FileSystem
        {
            get { return this._FILE_SYSTEM; }
        }

        /// <summary>
        /// Gets or sets the host address.
        /// </summary>
        public string HostAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if HTTPs should be used or not.
        /// </summary>
        public bool IsSecure
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the TCP port.
        /// </summary>
        public int? Port
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// The callback that allows any SSL certificate.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="chain">The certificate chain.</param>
        /// <param name="sslPolicyErrors">The SSL policy errors.</param>
        /// <returns>Always <see langword="true" />.</returns>
        public static bool AllowAnySslCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Creates a basic <see cref="HttpWebRequest" /> object based on the current data of that object
        /// and a specific url.
        /// </summary>
        /// <param name="url">The URL for the request.</param>
        /// <returns>The created object.</returns>
        public HttpWebRequest CreateHttpRequest(IEnumerable<char> url)
        {
            HttpWebRequest result = (HttpWebRequest)HttpWebRequest.Create(StringHelper.AsString(url));
            result.AllowAutoRedirect = false;
#if NET45
            result.ServerCertificateValidationCallback = this.SslValidator ?? AllowAnySslCertificate;
#endif

            SetupRequestWithCredentials(result);

            return result;
        }

        /// <summary>
        /// Returns the base URI.
        /// </summary>
        /// <returns>the base URI.</returns>
        public string GetBaseUri()
        {
            string host = this.HostAddress;
            if (StringHelper.IsNullOrWhitespace(host))
            {
                host = "127.0.0.1";
            }

            bool isHttps = this.IsSecure;
            int defPort = isHttps ? 443 : 80;

            return string.Format("http{0}://{1}:{2}/",
                                 isHttps ? "s" : string.Empty,
                                 host.Trim(),
                                 this.Port ?? defPort);
        }

        /// <summary>
        /// Tries to return the value of <see cref="CloudServer.Credentials" /> as <see cref="NetworkCredential" /> object. 
        /// </summary>
        /// <returns>
        /// <see cref="CloudServer.Credentials" /> as <see cref="NetworkCredential" /> object.
        /// </returns>
        public NetworkCredential GetNetworkCredentials()
        {
            NetworkCredential result = null;

            ICredentials cred = this.Credentials;
            if (cred != null)
            {
                result = cred as NetworkCredential;
                if (result == null)
                {
                    result = cred.GetCredential(null, null);
                }
            }

            return result;
        }
        // Private Methods (1) 

        private void SetupRequestWithCredentials(HttpWebRequest httpRequest)
        {
            NetworkCredential cred = this.GetNetworkCredentials();
            if (cred == null)
            {
                return;
            }

            string user = (cred.UserName ?? string.Empty).Trim();
            if (StringHelper.IsNullOrWhitespace(cred.Domain) == false)
            {
                user = cred.Domain.Trim() + "\\" + user;
            }

            string pwd = null;
#if !NET2 && !NET20 && !NET35
            pwd = StringHelper.ToUnsecureString(cred.SecurePassword);
#endif

            if (pwd == null)
            {
                pwd = cred.Password;
            }

            string authInfo = string.Format("{0}:{1}",
                                            user,
                                            StringHelper.AsString(pwd));

            httpRequest.Headers["Authorization"] = string.Format("Basic {0}",
                                                                 Convert.ToBase64String(Encoding.ASCII
                                                                                                .GetBytes(authInfo)));
        }

        #endregion Methods

#if NET45
        /// <summary>
        /// Gets or sets the callcack for SSL validation.
        /// <see langword="null" /> indicates to allow any SSL certificate.
        /// </summary>
        public global::System.Net.Security.RemoteCertificateValidationCallback SslValidator
        {
            get;
            set;
        }
#endif
    }
}
