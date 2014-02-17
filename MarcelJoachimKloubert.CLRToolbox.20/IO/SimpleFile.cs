// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Simple implementation of <see cref="IFile" /> interface.
    /// </summary>
    public sealed class SimpleFile : ContentProviderBase, IFile
    {
        #region Fields (6)

        private string _contentType;
        private DestructorHandler _destructor;
        private Encoding _encoding;
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

        #region Properties (7)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ContentProviderBase.ContentType" />
        public override string ContentType
        {
            get { return this._contentType; }
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
        /// 
        /// </summary>
        /// <see cref="ContentProviderBase.Encoding" />
        public override Encoding Encoding
        {
            get { return this._encoding; }
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

        #region Methods (6)

        // Public Methods (6) 

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
        /// <see cref="ContentProviderBase.OpenStream()" />
        public override Stream OpenStream()
        {
            return this.OpenStreamFunc();
        }

        /// <summary>
        /// Sets the value for <see cref="SimpleFile.ContentType" /> property.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns>That instance.</returns>
        public SimpleFile SetContentType(IEnumerable<char> newValue)
        {
            this._contentType = StringHelper.AsString(newValue);
            return this;
        }

        /// <summary>
        /// Sets the data of this file.
        /// </summary>
        /// <param name="data">The data to set.</param>
        /// <returns>That instance.</returns>
        /// <remarks>
        /// If <paramref name="data" /> is <see langword="null" /> <see cref="SimpleFile.OpenStreamFunc" />
        /// will also be set to <see langword="null" />.
        /// </remarks>
        public SimpleFile SetData(IEnumerable<byte> data)
        {
            this.OpenStreamFunc = data == null ? null : new OpenStreamHandler(delegate()
                {
                    return new MemoryStream(CollectionHelper.AsArray(data));
                });

            return this;
        }

        /// <summary>
        /// Sets the value for <see cref="SimpleFile.Encoding" /> property.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns>That instance.</returns>
        public SimpleFile SetEncoding(Encoding newValue)
        {
            this._encoding = newValue;
            return this;
        }

        /// <summary>
        /// Sets the stream of this file.
        /// </summary>
        /// <param name="stream">The stream to set.</param>
        /// <returns>That instance.</returns>
        /// <remarks>
        /// If <paramref name="stream" /> is <see langword="null" />, <see cref="SimpleFile.OpenStreamFunc" />
        /// will also be set to <see langword="null" />.
        /// </remarks>
        public SimpleFile SetStream(Stream stream)
        {
            this.OpenStreamFunc = stream == null ? null : new OpenStreamHandler(delegate()
                {
                    return stream;
                });

            return this;
        }

        #endregion Methods
    }
}
