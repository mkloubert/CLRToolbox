// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Linq;
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

        #region Methods (2)

        // Public Methods (2) 
        
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

        #endregion Methods
    }
}