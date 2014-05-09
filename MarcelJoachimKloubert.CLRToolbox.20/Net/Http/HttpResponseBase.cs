// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// A basic HTTP request context.
    /// </summary>
    public abstract partial class HttpResponseBase : TMObject, IHttpResponse
    {
        #region Fields (9)

        private Encoding _charset;
        private bool? _compress;
        private string _contentType;
        private bool _directOutput;
        private bool _documentNotFound;
        private bool _isForbidden;
        private HttpStatusCode _statusCode = HttpStatusCode.OK;
        private string _statusDescription;
        private Stream _stream;

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

        #region Properties (12)

        /// <inheriteddoc />
        public virtual bool CanSetStreamCapacity
        {
            get { return false; }
        }

        /// <inheriteddoc />
        public Encoding Charset
        {
            get { return this._charset; }

            set { this._charset = value; }
        }

        /// <inheriteddoc />
        public bool? Compress
        {
            get { return this._compress; }

            set { this._compress = value; }
        }

        /// <inheriteddoc />
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
            get { return this._directOutput; }

            set { this._directOutput = value; }
        }

        /// <inheriteddoc />
        public bool DocumentNotFound
        {
            get { return this._documentNotFound; }

            set { this._documentNotFound = value; }
        }

        /// <inheriteddoc />
        public abstract IDictionary<string, object> FrontendVars
        {
            get;
        }

        /// <inheriteddoc />
        public abstract IDictionary<string, string> Headers
        {
            get;
        }

        /// <inheriteddoc />
        public bool IsForbidden
        {
            get { return this._isForbidden; }

            set { this._isForbidden = value; }
        }

        /// <inheriteddoc />
        public HttpStatusCode StatusCode
        {
            get { return this._statusCode; }

            set { this._statusCode = value; }
        }

        /// <inheriteddoc />
        public string StatusDescription
        {
            get { return this._statusDescription; }

            set { this._statusDescription = value; }
        }

        /// <inheriteddoc />
        public Stream Stream
        {
            get { return this._stream; }
        }

        #endregion Properties

        #region Methods (22)

        // Public Methods (16) 

        /// <inheriteddoc />
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

        /// <inheriteddoc />
        public HttpResponseBase Append(IEnumerable<char> chars)
        {
            return this.Append(this.CharsToBytes(chars));
        }

        /// <inheriteddoc />
        public virtual HttpResponseBase Clear()
        {
            lock (this._SYNC)
            {
                this.Stream.SetLength(0);
                return this;
            }
        }

        /// <inheriteddoc />
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

        /// <inheriteddoc />
        public HttpResponseBase Prefix(IEnumerable<char> chars)
        {
            return this.Prefix(this.CharsToBytes(chars));
        }

        /// <inheriteddoc />
        public HttpResponseBase SetDefaultStreamCapacity()
        {
            return this.SetStreamCapacityInner(null);
        }

        /// <inheriteddoc />
        public HttpResponseBase SetStream(Stream stream)
        {
            return this.SetStream(stream, false);
        }

        /// <inheriteddoc />
        public HttpResponseBase SetStream(Stream stream, bool disposeOld)
        {
            lock (this._SYNC)
            {
                this.OnSetStream(stream,
                                 disposeOld);
            }

            return this;
        }

        /// <inheriteddoc />
        public HttpResponseBase SetStreamCapacity(int capacity)
        {
            return this.SetStreamCapacityInner(capacity);
        }

        /// <inheriteddoc />
        public HttpResponseBase SetupForJson()
        {
            this.Charset = Encoding.UTF8;
            this.ContentType = "application/json";
            this.DirectOutput = true;

            return this;
        }

        /// <inheriteddoc />
        public HttpResponseBase Write(IEnumerable<byte> data)
        {
            byte[] dataArray = CollectionHelper.AsArray(data);
            if (dataArray != null)
            {
                lock (this._SYNC)
                {
                    this.Stream
                        .Write(dataArray, 0, dataArray.Length);
                }
            }

            return this;
        }

        /// <inheriteddoc />
        public HttpResponseBase Write(IEnumerable<char> chars)
        {
            return this.Write(this.CharsToBytes(chars));
        }

        /// <inheriteddoc />
        public HttpResponseBase Write(object obj)
        {
            return this.Write(obj, true);
        }

        /// <inheriteddoc />
        public HttpResponseBase Write(object obj, bool handleDBNullAsNull)
        {
            if (obj is IEnumerable<byte>)
            {
                return this.Write(obj as IEnumerable<byte>);
            }

            return this.Write(StringHelper.AsString(obj, handleDBNullAsNull));
        }

        /// <inheriteddoc />
        public HttpResponseBase WriteJavaScript(IEnumerable<char> js)
        {
            return this.Write("<script type=\"text/javascript\">\n\n")
                       .Write(js)
                       .Write("\n\n</script>");
        }

        /// <inheriteddoc />
        public HttpResponseBase WriteJson<T>(T obj)
        {
            StringBuilder json = new StringBuilder();
            this.OnWriteJson<T>(obj, ref json);

            if (json != null)
            {
                this.Write(json);
            }

            return this;
        }

        // Protected Methods (5) 

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

        /// <summary>
        /// The logic for the <see cref="HttpResponseBase.SetStream(Stream, bool)" /> method.
        /// </summary>
        /// <param name="stream">The new stream.</param>
        /// <param name="disposeOld">Dispose old stream or not.</param>
        protected void OnSetStream(Stream stream, bool disposeOld)
        {
            Stream oldStream = this._stream;
            this._stream = stream;

            if (oldStream != null && disposeOld)
            {
                oldStream.Dispose();
            }
        }

        /// <summary>
        /// Defines the logic for <see cref="HttpResponseBase.SetStreamCapacity(int)" /> method.
        /// </summary>
        /// <param name="capacity">
        /// The new capacity. <see langword="null" /> indicates to use a default value.
        /// </param>
        protected virtual void OnSetStreamCapacity(int? capacity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The logic for <see cref="HttpResponseBase.WriteJson{T}(T)" /> method.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="json">The string builder that builds/stores the JSON string of <paramref name="obj" />.</param>
        protected virtual void OnWriteJson<T>(T obj, ref StringBuilder json)
        {
            throw new NotImplementedException();
        }

        // Private Methods (1) 

        private HttpResponseBase SetStreamCapacityInner(int? capacity)
        {
            lock (this._SYNC)
            {
                if (!this.CanSetStreamCapacity)
                {
                    throw new NotSupportedException();
                }

                if (capacity < 0)
                {
                    throw new ArgumentOutOfRangeException("capacity");
                }

                this.OnSetStreamCapacity(capacity);
            }

            return this;
        }

        #endregion Methods
    }
}