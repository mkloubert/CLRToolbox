// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers;
using System.Reflection;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation of a constructor.
    /// </summary>
    public sealed class ConstructorDocumentation : MemberDocumentationBase<ConstructorInfo>
    {
        #region Constructors (1)

        internal ConstructorDocumentation(TypeDocumentation typeDoc, ConstructorInfo constructor, XElement xml)
            : base(typeDoc, constructor, xml)
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <inheriteddoc />
        public override string ToString()
        {
            return string.Format("C:{0}.#ctor{1}",
                                 this.Type.ClrType.FullName,
                                 ParameterHelper.CreateStringList(this.ClrMember.GetParameters()));
        }

        #endregion Methods
    }
}