// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Reflection;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation of a method parameter.
    /// </summary>
    public sealed class MethodParameterDocumentation : MemberItemDocumentationBase<MethodInfo, MethodDocumentation, ParameterInfo>
    {
        #region Constructors (1)

        internal MethodParameterDocumentation(MethodDocumentation parent, ParameterInfo parameter, XElement xml)
            : base(parent, parameter, xml)
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <inheriteddoc />
        public override string ToString()
        {
            return this.ClrItem.Name;
        }

        #endregion Methods
    }
}