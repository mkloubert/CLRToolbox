// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Documents
{
    /// <summary>
    /// A basic document.
    /// </summary>
    public abstract class DocumentBase : ContentProviderBase, IDocument
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected DocumentBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentBase" /> class.
        /// </summary>
        protected DocumentBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (2)

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
        /// <see cref="IHasName.Name" />
        public abstract string Name
        {
            get;
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (1) 

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

            return StringHelper.AsString(this.OnGetDisplayName(culture));
        }
        // Protected Methods (1) 

        /// <summary>
        /// Stores the logic for the <see cref="DocumentBase.GetDisplayName(CultureInfo)" /> method.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The display name.</returns>
        protected virtual IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return this.Name;
        }

        #endregion Methods
    }
}
