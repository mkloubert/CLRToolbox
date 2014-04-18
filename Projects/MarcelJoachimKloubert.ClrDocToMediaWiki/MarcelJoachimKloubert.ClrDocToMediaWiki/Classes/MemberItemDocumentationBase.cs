// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// A basic item that is a child item of a <see cref="MemberDocumentationBase{M}" /> object.
    /// </summary>
    /// <typeparam name="M">Type of the CLR member.</typeparam>
    /// <typeparam name="P">Type of the parent.</typeparam>
    /// <typeparam name="I">Type of the CLR item.</typeparam>
    public abstract class MemberItemDocumentationBase<M, P, I> : DocumentableBase
        where M : global::System.Reflection.MemberInfo
        where P : global::MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.MemberDocumentationBase<M>
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="MemberItemDocumentationBase" /> class.
        /// </summary>
        /// <param name="parent">The value for the <see cref="MemberItemDocumentationBase{M, P, I}.Parent" /> property.</param>
        /// <param name="item">The value for the <see cref="MemberItemDocumentationBase{M, P, I}.Item" /> property.</param>
        /// <param name="parent">The value for the <see cref="DocumentableBase.Xml" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parent" />, <paramref name="item" /> and/or <paramref name="xml" /> are <see langword="null" />.
        /// </exception>
        protected MemberItemDocumentationBase(P parent, I item, XElement xml)
            : base(xml: xml)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.Parent = parent;
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the underlying CLR item.
        /// </summary>
        public I ClrItem
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the underlying parent.
        /// </summary>
        public P Parent
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        /// <inheriteddoc />
        protected override void InitRemarks()
        {
            this.Remarks = null;
        }

        /// <inheriteddoc />
        protected override void InitSummary()
        {
            this.Summary = this.Xml;
        }

        #endregion Methods
    }
}