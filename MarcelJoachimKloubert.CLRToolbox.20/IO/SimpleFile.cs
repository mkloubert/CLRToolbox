// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Simple implementation of <see cref="IFile" /> interface.
    /// </summary>
    public sealed class SimpleFile : TMObject, IFile
    {
        #region Fields (5)

        private string _contentType;
        private DestructorHandler _destructor;
        private GetDisplayNameHandler _getDisplayNameFunc;
        private string _name;
        private OpenStreamHandler _openStreamFunc;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Finalizes an instance of the <see cref="SimpleFile" /> class.
        /// </summary>
        ~SimpleFile()
        {
            DestructorHandler handler = this.Destructor;
            if (handler != null)
            {
                handler(this);
            }
        }

        #endregion Constructors

        #region Properties (6)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IContentProvider.ContentType" />
        public string ContentType
        {
            get { return this._contentType; }

            set { this._contentType = value; }
        }

        /// <summary>
        /// Gets or sets the optional logic for the finalizer.
        /// </summary>
        public DestructorHandler Destructor
        {
            get { return this._destructor; }

            set { this._destructor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.DisplayName" />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Gets or sets the logic for <see cref="SimpleFile.GetDisplayName(CultureInfo)" /> method.
        /// </summary>
        public GetDisplayNameHandler GetDisplayNameFunc
        {
            get { return this._getDisplayNameFunc; }

            set { this._getDisplayNameFunc = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.Name" />
        public string Name
        {
            get { return this._name; }

            set { this._name = value; }
        }

        /// <summary>
        /// Gets or sets the logic for <see cref="SimpleFile.OpenStream()" /> method.
        /// </summary>
        public OpenStreamHandler OpenStreamFunc
        {
            get { return this._openStreamFunc; }

            set { this._openStreamFunc = value; }
        }

        #endregion Properties

        #region Delegates and Events (3)

        // Delegates (3) 

        /// <summary>
        /// Describes logic for the destructor.
        /// </summary>
        /// <param name="file">The underlying instance.</param>
        public delegate void DestructorHandler(SimpleFile file);
        /// <summary>
        /// Describes logic for the <see cref="SimpleFile.GetDisplayName(CultureInfo)" /> method.
        /// </summary>
        /// <param name="file">The underlying file.</param>
        /// <param name="culture">The underlying culture.</param>
        /// <returns>The display name.</returns>
        public delegate IEnumerable<char> GetDisplayNameHandler(SimpleFile file, CultureInfo culture);
        /// <summary>
        /// Describes logic for the <see cref="SimpleFile.OpenStream()" /> method.
        /// </summary>
        public delegate Stream OpenStreamHandler();

        #endregion Delegates and Events

        #region Methods (11)

        // Public Methods (11) 

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

            byte[] content = this.GetData();
            if (content != null)
            {
                return algo.ComputeHash(content);
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
        /// <see cref="IHasName.GetDisplayName(CultureInfo)" />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            GetDisplayNameHandler func = this.GetDisplayNameFunc;
            if (func != null)
            {
                return StringHelper.AsString(func(this, culture));
            }

            return this.Name;
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
        public Stream OpenStream()
        {
            return this.OpenStreamFunc();
        }

        /// <summary>
        /// Sets the data of this file.
        /// </summary>
        /// <param name="data">The data to set.</param>
        /// <remarks>
        /// If <paramref name="data" /> is <see langword="null" /> <see cref="SimpleFile.OpenStreamFunc" />
        /// will also be set to <see langword="null" />.
        /// </remarks>
        public void SetData(IEnumerable<byte> data)
        {
            this.OpenStreamFunc = data == null ? null : new OpenStreamHandler(delegate()
                {
                    return new MemoryStream(CollectionHelper.AsArray(data));
                });
        }

        /// <summary>
        /// Sets the stream of this file.
        /// </summary>
        /// <param name="stream">The stream to set.</param>
        /// <remarks>
        /// If <paramref name="stream" /> is <see langword="null" /> <see cref="SimpleFile.OpenStreamFunc" />
        /// will also be set to <see langword="null" />.
        /// </remarks>
        public void SetStream(Stream stream)
        {
            this.OpenStreamFunc = stream == null ? null : new OpenStreamHandler(delegate()
                {
                    return stream;
                });
        }

        #endregion Methods
    }
}
