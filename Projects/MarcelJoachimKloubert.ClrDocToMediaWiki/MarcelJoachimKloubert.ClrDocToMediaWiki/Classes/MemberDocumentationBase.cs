// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// A basic object that documents a <see cref="MemberInfo" />.
    /// </summary>
    /// <typeparam name="M">Type of the underlying member.</typeparam>
    public abstract class MemberDocumentationBase<M> : DocumentableBase where M : global::System.Reflection.MemberInfo
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="MemberDocumentationBase" /> class.
        /// </summary>
        /// <param name="typeDoc">The value for the <see cref="MemberDocumentationBase{M}.Type" /> property.</param>
        /// <param name="member">The value for the <see cref="MemberDocumentationBase{M}.ClrMember" /> property.</param>
        /// <param name="xml">The value for the <see cref="DocumentableBase.Xml" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="typeDoc" /> and/or <paramref name="member" /> are <see langword="null" />.
        /// </exception>
        protected MemberDocumentationBase(TypeDocumentation typeDoc, M member, XElement xml)
            : base(xml: xml ?? new XElement("member"))
        {
            if (typeDoc == null)
            {
                throw new ArgumentNullException("typeDoc");
            }

            if (member == null)
            {
                throw new ArgumentNullException("member");
            }

            this.Type = typeDoc;
            this.ClrMember = member;
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the underlying CLR member.
        /// </summary>
        public M ClrMember
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the underlying type.
        /// </summary>
        public TypeDocumentation Type
        {
            get;
            private set;
        }

        #endregion Properties
    }
}