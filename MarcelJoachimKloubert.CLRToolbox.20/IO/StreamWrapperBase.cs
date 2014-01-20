// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A general wrapper for a <see cref="Stream" />.
    /// </summary>
    public abstract class StreamWrapperBase : Stream
    {
        #region Fields (2)

        /// <summary>
        /// Stores the inner stream.
        /// </summary>
        protected readonly Stream _BASE_STREAM;
        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected object _SYNC = new object();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamWrapperBase" /> class.
        /// </summary>
        /// <param name="baseStream">The value for <see cref="StreamWrapperBase._BASE_STREAM" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseStream" /> is <see langword="null" />.
        /// </exception>
        protected StreamWrapperBase(Stream baseStream)
        {
            if (baseStream == null)
            {
                throw new ArgumentNullException("baseStream");
            }

            this._BASE_STREAM = baseStream;
        }

        #endregion Constructors

        #region Properties (8)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.CanRead" />
        public override bool CanRead
        {
            get { return this._BASE_STREAM.CanRead; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.CanSeek" />
        public override bool CanSeek
        {
            get { return this._BASE_STREAM.CanSeek; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.CanTimeout" />
        public override bool CanTimeout
        {
            get { return this._BASE_STREAM.CanTimeout; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.CanWrite" />
        public override bool CanWrite
        {
            get { return this._BASE_STREAM.CanWrite; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Length" />
        public override long Length
        {
            get { return this._BASE_STREAM.Length; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Position" />
        public override long Position
        {
            get { return this._BASE_STREAM.Position; }

            set { this._BASE_STREAM.Position = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.ReadTimeout" />
        public override int ReadTimeout
        {
            get { return this._BASE_STREAM.ReadTimeout; }

            set { this._BASE_STREAM.ReadTimeout = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.WriteTimeout" />
        public override int WriteTimeout
        {
            get { return this._BASE_STREAM.WriteTimeout; }

            set { this._BASE_STREAM.WriteTimeout = value; }
        }

        #endregion Properties

        #region Methods (17)

        // Public Methods (15) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.BeginRead(byte[], int, int, AsyncCallback, object)" />
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this._BASE_STREAM
                       .BeginRead(buffer, offset, count, callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.BeginRead(byte[], int, int, AsyncCallback, object)" />
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this._BASE_STREAM
                       .BeginWrite(buffer, offset, count, callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Close()" />
        public override void Close()
        {
            this._BASE_STREAM
                .Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.EndRead(IAsyncResult)" />
        public override int EndRead(IAsyncResult asyncResult)
        {
            return this._BASE_STREAM
                       .EndRead(asyncResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.EndWrite(IAsyncResult)" />
        public override void EndWrite(IAsyncResult asyncResult)
        {
            this._BASE_STREAM
                .EndWrite(asyncResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object obj)
        {
            return this._BASE_STREAM
                       .Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Flush()" />
        public override void Flush()
        {
            this._BASE_STREAM
                .Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()" />
        public override int GetHashCode()
        {
            return this._BASE_STREAM
                       .GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Read(byte[], int, int)" />
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this._BASE_STREAM
                       .Read(buffer, offset, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.ReadByte()" />
        public override int ReadByte()
        {
            return this._BASE_STREAM
                       .ReadByte();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Seek(long, SeekOrigin)" />
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this._BASE_STREAM
                       .Seek(offset, origin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.SetLength(long)" />
        public override void SetLength(long value)
        {
            this._BASE_STREAM
                .SetLength(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return this._BASE_STREAM
                       .ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Write(byte[], int, int)" />
        public override void Write(byte[] buffer, int offset, int count)
        {
            this._BASE_STREAM
                .Write(buffer, offset, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.WriteByte(byte)" />
        public override void WriteByte(byte value)
        {
            this._BASE_STREAM
                .WriteByte(value);
        }
        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Stream.Dispose(bool)" />
        protected override void Dispose(bool disposing)
        {
            this.InvokeDispose(disposing);
        }

        /// <summary>
        /// Invokes the <see cref="Stream.Dispose(bool)" /> method of
        /// <see cref="StreamWrapperBase._BASE_STREAM" /> object.
        /// </summary>
        /// <param name="disposing">The parameter value for the <see cref="Stream.Dispose(bool)" /> method.</param>
        protected void InvokeDispose(bool disposing)
        {
            // find Dispose(bool) method of inner base stream
            MethodInfo disposeMethod = CollectionHelper.Single(this._BASE_STREAM
                                                                   .GetType()
                                                                   .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic),
                                                               delegate(MethodInfo m)
                                                               {
                                                                   if (m.Name != "Dispose")
                                                                   {
                                                                       // invalid name
                                                                       return false;
                                                                   }

                                                                   if (m.GetGenericArguments().Length != 0)
                                                                   {
                                                                       // must NOT habe generic arguments
                                                                       return false;
                                                                   }

                                                                   // only one boolean parameter
                                                                   ParameterInfo[] @params = m.GetParameters();
                                                                   return @params.Length == 1 &&
                                                                          typeof(bool).Equals(@params[0].ParameterType);
                                                               });

            disposeMethod.Invoke(this._BASE_STREAM,
                                 new object[] { disposing });
        }

        #endregion Methods
    }
}
