// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation of a method.
    /// </summary>
    public sealed class MethodDocumentation : MemberDocumentationBase<MethodInfo>
    {
        #region Constructors (1)

        internal MethodDocumentation(TypeDocumentation typeDoc, MethodInfo method, XElement xml)
            : base(typeDoc, method, xml)
        {
        }

        #endregion Constructors

        #region Methods (2)

        /// <summary>
        /// Returns all parameters of that method.
        /// </summary>
        /// <returns>The parameters of that method.</returns>
        public IEnumerable<MethodParameterDocumentation> GetParameters()
        {
            foreach (var param in this.ClrMember
                                      .GetParameters()
                                      .OrderBy(p => p.Position))
            {
                yield return new MethodParameterDocumentation(this, param,
                                                              this.Xml
                                                                  .XPathSelectElements(string.Format("param[@name='{0}']",
                                                                                                     param.Name))
                                                                  .FirstOrDefault());
            }
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return string.Format("{0}.{1}{2}",
                                 this.Type.ClrType.FullName,
                                 this.ClrMember.Name,
                                 ParameterHelper.CreateStringList(this.ClrMember.GetParameters()));
        }

        #endregion Methods
    }
}