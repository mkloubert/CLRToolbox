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

        #region Properties (4)
        
        // Public Properties (4) 

        /// <summary>
        /// Gets the display name of that method.
        /// </summary>
        public string DisplayName
        {
            get
            {
                var result = this.Name;
                if (string.IsNullOrEmpty(result))
                {
                    return result;
                }

                var genArgs = this.GetGenericArguments().ToArray();
                var displayArgs = "(" + string.Join(",",
                                                    genArgs.Select(ga =>
                                                    {
                                                        if (ga.ClrType.IsGenericParameter)
                                                        {
                                                            return ga.Name;
                                                        }

                                                        return ga.FullDisplayName;
                                                    })) + ")";

                IEnumerable<ParameterInfo> @params = this.ClrMember.GetParameters();
                if (this.IsExtensionMethod)
                {
                    @params = @params.Skip(1);
                }

                return result.Trim() + displayArgs
                                     + ParameterHelper.CreateStringList(@params);
            }
        }

        /// <summary>
        /// If that method is an extension method, the target type is returned;
        /// otherwise <see langword="null" />.
        /// </summary>
        public TypeDocumentation ExtensionMethodTargetType
        {
            get
            {
                if (this.IsExtensionMethod)
                {
                    var firstParam = this.GetParameters().FirstOrDefault();
                    if (firstParam != null)
                    {
                        return TypeHelper.GetDocumentation(firstParam.ClrItem
                                                                     .ParameterType);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets if that method represents an extension method or not.
        /// </summary>
        public bool IsExtensionMethod
        {
            get
            {
                return this.ClrMember
                           .IsDefined(typeof(global::System.Runtime.CompilerServices.ExtensionAttribute), true);
            }
        }

        /// <summary>
        /// Gets the name of that method.
        /// </summary>
        public string Name
        {
            get { return this.ClrMember.Name; }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Returns the list of parameters that are used if that method is used as extension method.
        /// </summary>
        /// <returns>
        /// The list of parameters or <see langword="null" /> if that method is NO extension method.
        /// </returns>
        public IEnumerable<MethodParameterDocumentation> GetExtensionMethodParameters()
        {
            if (this.IsExtensionMethod == false)
            {
                return null;
            }

            return this.GetParameters().Skip(1);
        }

        /// <summary>
        /// Returns the documented generic arguments of that method.
        /// </summary>
        /// <returns>The generic arguments.</returns>
        public IEnumerable<TypeDocumentation> GetGenericArguments()
        {
            foreach (var genArg in this.ClrMember
                                       .GetGenericArguments()
                                       .OrderBy(ga => ga.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return TypeHelper.GetDocumentation(genArg);
            }
        }

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
                var paramElements = this.Xml == null ? Enumerable.Empty<XElement>()
                                                     : this.Xml
                                                           .XPathSelectElements(string.Format("param[@name='{0}']",
                                                                                              param.Name));

                yield return new MethodParameterDocumentation(this, param,
                                                              paramElements.FirstOrDefault());
            }
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return this.DisplayName;
        }

        #endregion Methods
    }
}