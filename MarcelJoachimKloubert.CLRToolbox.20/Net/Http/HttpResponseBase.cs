// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// A basic HTTP request context.
    /// </summary>
    public abstract partial class HttpResponseBase : TMObject, IHttpResponse
    {
        #region Fields (6)

        private Encoding _charset;
        private bool _compress;
        private string _contentType;
        private bool _documentNotFound;
        private bool _isForbidden;
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HttpResponseBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseBase" /> class.
        /// </summary>
        protected HttpResponseBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (9)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Charset" />
        public Encoding Charset
        {
            get { return this._charset; }

            set { this._charset = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Compress" />
        public bool Compress
        {
            get { return this._compress; }

            set { this._compress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.ContentType" />
        public string ContentType
        {
            get { return this._contentType; }

            set { this._contentType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.DirectOutput" />
        public bool DirectOutput
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.DocumentNotFound" />
        public bool DocumentNotFound
        {
            get { return this._documentNotFound; }

            set { this._documentNotFound = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Headers" />
        public abstract IDictionary<string, string> Headers
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.IsForbidden" />
        public bool IsForbidden
        {
            get { return this._isForbidden; }

            set { this._isForbidden = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.StatusCode" />
        public HttpStatusCode StatusCode
        {
            get { return this._statusCode; }

            set { this._statusCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Stream" />
        public abstract Stream Stream
        {
            get;
        }

        #endregion Properties

        #region Methods (9)

        // Public Methods (7) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Append(IEnumerable{byte})" />
        public HttpResponseBase Append(IEnumerable<byte> data)
        {
            lock (this._SYNC)
            {
                byte[] dataArray = CollectionHelper.AsArray(data);
                if (dataArray != null &&
                    dataArray.Length > 0)
                {
                    long lastPos = this.Stream.Position;
                    try
                    {
                        // go to end
                        this.Stream.Position = this.Stream.Length;

                        this.Stream.Write(dataArray, 0, dataArray.Length);
                    }
                    finally
                    {
                        this.Stream.Position = lastPos;
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Append(IEnumerable{char})" />
        public HttpResponseBase Append(IEnumerable<char> chars)
        {
            return this.Append(this.CharsToBytes(chars));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Clear()" />
        public virtual HttpResponseBase Clear()
        {
            lock (this._SYNC)
            {
                this.Stream.SetLength(0);
                return this;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Prefix(IEnumerable{byte})" />
        public HttpResponseBase Prefix(IEnumerable<byte> data)
        {
            lock (this._SYNC)
            {
                byte[] dataArray = CollectionHelper.AsArray(data);
                if (dataArray != null &&
                    dataArray.Length > 0)
                {
                    using (MemoryStream backup = new MemoryStream())
                    {
                        // backup
                        this.Stream.Position = 0;
                        IOHelper.CopyTo(this.Stream, backup);

                        this.Clear();

                        this.Stream.Write(dataArray, 0, dataArray.Length);

                        // restore
                        backup.Position = 0;
                        IOHelper.CopyTo(backup, this.Stream);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Prefix(IEnumerable{char})" />
        public HttpResponseBase Prefix(IEnumerable<char> chars)
        {
            return this.Prefix(this.CharsToBytes(chars));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Write(IEnumerable{byte})" />
        public HttpResponseBase Write(IEnumerable<byte> data)
        {
            byte[] dataArray = CollectionHelper.AsArray(data);
            if (dataArray != null)
            {
                this.Stream
                    .Write(dataArray, 0, dataArray.Length);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpResponse.Write(IEnumerable{char})" />
        public HttpResponseBase Write(IEnumerable<char> chars)
        {
            return this.Write(this.CharsToBytes(chars));
        }
        // Protected Methods (2) 

        /// <summary>
        /// Converts a char sequence to a binary sequence.
        /// </summary>
        /// <param name="chars">The chars to convert.</param>
        /// <returns>The chars as bytes.</returns>
        protected virtual IEnumerable<byte> CharsToBytes(IEnumerable<char> chars)
        {
            IEnumerable<byte> result = null;
            if (chars != null)
            {
                Encoding cs = this.Charset ?? this.GetDefaultCharset();
                if (cs != null)
                {
                    result = cs.GetBytes(StringHelper.AsString(chars));
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the default charset.
        /// </summary>
        /// <returns>The default charset.</returns>
        protected virtual Encoding GetDefaultCharset()
        {
            return Encoding.UTF8;
        }

        #endregion Methods
    }
}
