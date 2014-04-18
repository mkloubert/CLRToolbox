// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// A basic object that is documented.
    /// </summary>
    public abstract class DocumentableBase : IDocumentable
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="DocumentableBase" /> class.
        /// </summary>
        /// <param name="xml">The value for the <see cref="DocumentableBase.Xml" /> property.</param>
        protected DocumentableBase(XElement xml)
        {
            this.Xml = xml;

            this.InitSummary();
            this.InitRemarks();
        }

        #endregion Constructors

        #region Properties (3)

        /// <inheriteddoc />
        public XElement Remarks
        {
            get;
            protected set;
        }

        /// <inheriteddoc />
        public XElement Summary
        {
            get;
            protected set;
        }

        /// <inheriteddoc />
        public XElement Xml
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (7)

        // Public Methods (3) 

        /// <inheriteddoc />
        public string GetWikiPageName()
        {
            return this.OnGetWikiPageName().AsString();
        }

        /// <inheriteddoc />
        public string ToMediaWiki()
        {
            var builder = new StringBuilder();
            this.OnToMediaWiki(ref builder);

            return builder != null ? builder.ToString() : null;
        }

        /// <inheriteddoc />
        public void ToMediaWiki(StringBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            this.OnToMediaWiki(ref builder);
        }

        // Protected Methods (4) 

        /// <summary>
        /// Initializes the <see cref="DocumentableBase.Remarks" /> property.
        /// </summary>
        protected virtual void InitRemarks()
        {
            if (this.Xml != null)
            {
                this.Remarks = this.Xml
                                   .Elements("remarks")
                                   .FirstOrDefault();
            }
        }

        /// <summary>
        /// Initializes the <see cref="DocumentableBase.Summary" /> property.
        /// </summary>
        protected virtual void InitSummary()
        {
            if (this.Xml != null)
            {
                this.Summary = this.Xml
                                   .Elements("summary")
                                   .FirstOrDefault();
            }
        }

        /// <summary>
        /// Stores the logic for the <see cref="DocumentableBase.GetWikiPageName()" /> method.
        /// </summary>
        /// <returns>The result for <see cref="DocumentableBase.GetWikiPageName()" />.</returns>
        protected virtual IEnumerable<char> OnGetWikiPageName()
        {
            return null;
        }

        /// <summary>
        /// Stores the logic for the <see cref="DocumentableBase.ToMediaWiki(StringBuilder)" /> method.
        /// </summary>
        /// <param name="builder">The builder that should be used to write the markup to.</param>
        protected virtual void OnToMediaWiki(ref StringBuilder builder)
        {
            builder = null;
        }

        #endregion Methods
    }
}