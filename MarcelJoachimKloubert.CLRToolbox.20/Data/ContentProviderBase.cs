// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// A baisc content provider.
    /// </summary>
    public abstract class ContentProviderBase : TMObject,
                                                IContentProvider
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentProviderBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ContentProviderBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentProviderBase" /> class.
        /// </summary>
        protected ContentProviderBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.ContentType" />
        public abstract string ContentType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.Encoding" />
        public abstract Encoding Encoding
        {
            get;
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (8) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.CalculateHashOfContent()" />
        public byte[] CalculateHashOfContent()
        {
            return this.CalculateHashOfContent<global::MarcelJoachimKloubert.CLRToolbox.Security.Cryptography.Crc32>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.CalculateHashOfContent{TAlgo}()" />
        public byte[] CalculateHashOfContent<TAlgo>() where TAlgo : HashAlgorithm, new()
        {
            using (TAlgo a = new TAlgo())
            {
                return this.CalculateHashOfContent(a);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.CalculateHashOfContent(HashAlgorithm)" />
        public byte[] CalculateHashOfContent(HashAlgorithm algo)
        {
            if (algo == null)
            {
                throw new ArgumentNullException("algo");
            }

            using (Stream stream = this.OpenStream())
            {
                if (stream != null)
                {
                    return algo.ComputeHash(stream);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.GetData()" />
        public byte[] GetData()
        {
            using (Stream stream = this.OpenStream())
            {
                if (stream != null)
                {
                    using (MemoryStream temp = new MemoryStream())
                    {
                        IOHelper.CopyTo(stream, temp);

                        return temp.ToArray();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.GetHashOfContentAsHexString()" />
        public string GetHashOfContentAsHexString()
        {
            return StringHelper.AsHexString(this.CalculateHashOfContent());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.GetHashOfContentAsHexString{TAlgo}()" />
        public string GetHashOfContentAsHexString<TAlgo>() where TAlgo : HashAlgorithm, new()
        {
            return StringHelper.AsHexString(this.CalculateHashOfContent<TAlgo>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.GetHashOfContentAsHexString(HashAlgorithm)" />
        public string GetHashOfContentAsHexString(HashAlgorithm algo)
        {
            return StringHelper.AsHexString(this.CalculateHashOfContent(algo));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.OpenStream()" />
        public abstract Stream OpenStream();

        #endregion Methods
    }
}
