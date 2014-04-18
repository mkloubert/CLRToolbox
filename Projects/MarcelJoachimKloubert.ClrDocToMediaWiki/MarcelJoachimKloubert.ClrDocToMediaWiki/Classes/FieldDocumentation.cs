// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Reflection;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation of a field.
    /// </summary>
    public sealed class FieldDocumentation : MemberDocumentationBase<FieldInfo>
    {
        #region Constructors (1)

        internal FieldDocumentation(TypeDocumentation typeDoc, FieldInfo field, XElement xml)
            : base(typeDoc, field, xml)
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <inheriteddoc />
        public override string ToString()
        {
            return string.Format("F:{0}.{1}",
                                 this.Type.ClrType.FullName,
                                 this.ClrMember.Name);
        }

        #endregion Methods
    }
}