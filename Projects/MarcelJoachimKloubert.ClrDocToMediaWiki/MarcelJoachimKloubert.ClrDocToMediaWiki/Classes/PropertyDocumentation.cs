// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation of a property.
    /// </summary>
    public sealed class PropertyDocumentation : MemberDocumentationBase<PropertyInfo>
    {
        #region Constructors (1)

        internal PropertyDocumentation(TypeDocumentation typeDoc, PropertyInfo property, XElement xml)
            : base(typeDoc, property, xml)
        {
        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Returns all index parameters of that property.
        /// </summary>
        /// <returns>The index parameters of that property.</returns>
        public IEnumerable<PropertyParameterDocumentation> GetIndexParameters()
        {
            if (this.Xml == null)
            {
                yield break;
            }

            foreach (var param in this.ClrMember
                                      .GetIndexParameters()
                                      .OrderBy(p => p.Position))
            {
                yield return new PropertyParameterDocumentation(this, param,
                                                                this.Xml
                                                                    .XPathSelectElements(string.Format("param[@name='{0}']",
                                                                                                       param.Name))
                                                                    .FirstOrDefault());
            }
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return string.Format("P:{0}.{1}{2}",
                                 this.Type.ClrType.FullName,
                                 this.ClrMember.Name,
                                 ParameterHelper.CreateStringList(this.ClrMember.GetIndexParameters()));
        }

        #endregion Methods
    }
}