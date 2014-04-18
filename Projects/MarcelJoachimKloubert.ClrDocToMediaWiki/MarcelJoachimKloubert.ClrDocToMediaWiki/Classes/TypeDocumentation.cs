// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation for a <see cref="Type" />
    /// </summary>
    public sealed class TypeDocumentation : DocumentableBase
    {
        #region Fields (1)

        private Lazy<IEnumerable<MethodDocumentation>> _methods;

        #endregion Fields

        #region Constructors (1)

        internal TypeDocumentation(AssemblyDocumentation asmDoc, Type type, XElement xml)
            : base(xml)
        {
            if (asmDoc == null)
            {
                throw new ArgumentNullException("asmDoc");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this.Assembly = asmDoc;
            this.ClrType = type;

            this.Reset();
        }

        #endregion Constructors

        #region Properties (6)

        /// <summary>
        /// Gets the underlying assembly.
        /// </summary>
        public AssemblyDocumentation Assembly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the underlying CLR type object.
        /// </summary>
        public Type ClrType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the full display name.
        /// </summary>
        public string FullDisplayName
        {
            get
            {
                var result = this.FullName;
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

                return result.Replace("`" + genArgs.Length,
                                      displayArgs);
            }
        }

        /// <summary>
        /// Gets the full name of that type.
        /// </summary>
        public string FullName
        {
            get { return TypeHelper.GetFullName(this.ClrType); }
        }

        /// <summary>
        /// Gets the short name of that type.
        /// </summary>
        public string Name
        {
            get { return TypeHelper.GetName(this.ClrType); }
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (8) 

        /// <summary>
        /// Returns all constructors and their documentations.
        /// </summary>
        /// <returns>The list of constructor documentations.</returns>
        public IEnumerable<ConstructorDocumentation> GetConstructors()
        {
            foreach (var constructor in this.ClrType
                                            .GetConstructors()
                                            .OrderBy(c => c.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return new ConstructorDocumentation(this, constructor,
                                                          this.Assembly
                                                              .Xml
                                                              .XPathSelectElements(string.Format("members/member[@name='M:{0}.#ctor{1}']",
                                                                                                 this.ClrType.FullName,
                                                                                                 ParameterHelper.CreateStringList(constructor.GetParameters())))
                                                              .FirstOrDefault());
            }
        }

        /// <summary>
        /// Returns all events and their documentations.
        /// </summary>
        /// <returns>The list of event documentations.</returns>
        public IEnumerable<EventDocumentation> GetEvents()
        {
            foreach (var @event in this.ClrType
                                       .GetEvents()
                                       .OrderBy(e => e.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return new EventDocumentation(this, @event,
                                                    this.Assembly
                                                        .Xml
                                                        .XPathSelectElements(string.Format("members/member[@name='E:{0}.{1}']",
                                                                                           this.ClrType.FullName,
                                                                                           @event.Name))
                                                        .FirstOrDefault());
            }
        }

        /// <summary>
        /// Returns all fields and their documentations.
        /// </summary>
        /// <returns>The list of field documentations.</returns>
        public IEnumerable<FieldDocumentation> GetFields()
        {
            foreach (var field in this.ClrType
                                      .GetFields()
                                      .OrderBy(f => f.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return new FieldDocumentation(this, field,
                                                    this.Assembly
                                                        .Xml
                                                        .XPathSelectElements(string.Format("members/member[@name='F:{0}.{1}']",
                                                                                           this.ClrType.FullName,
                                                                                           field.Name))
                                                        .FirstOrDefault());
            }
        }

        /// <summary>
        /// Returns the documented generic arguments of that type.
        /// </summary>
        /// <returns>The generic arguments.</returns>
        public IEnumerable<TypeDocumentation> GetGenericArguments()
        {
            foreach (var genArg in this.ClrType
                                       .GetGenericArguments()
                                       .OrderBy(ga => ga.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return TypeHelper.GetDocumentation(genArg);
            }
        }

        /// <summary>
        /// Returns all methods and their documentations.
        /// </summary>
        /// <returns>The list of method documentations.</returns>
        public IEnumerable<MethodDocumentation> GetMethods()
        {
            return this._methods.Value;
        }

        /// <summary>
        /// Returns all properties and their documentations.
        /// </summary>
        /// <returns>The list of property documentations.</returns>
        public IEnumerable<PropertyDocumentation> GetProperties()
        {
            foreach (var property in this.ClrType
                                         .GetProperties()
                                         .OrderBy(m => m.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return new PropertyDocumentation(this, property,
                                                       this.Assembly
                                                           .Xml
                                                           .XPathSelectElements(string.Format("members/member[@name='P:{0}.{1}{2}']",
                                                                                              this.ClrType.FullName,
                                                                                              property.Name,
                                                                                              ParameterHelper.CreateStringList(property.GetIndexParameters())))
                                                           .FirstOrDefault());
            }
        }

        /// <summary>
        /// Resets that instance.
        /// </summary>
        public void Reset()
        {
            this._methods = new Lazy<IEnumerable<MethodDocumentation>>(this.GetMethodsInner, isThreadSafe: true);
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return "T:" + this.FullDisplayName;
        }
        
        // Private Methods (1) 

        private IEnumerable<MethodDocumentation> GetMethodsInner()
        {
            var result = new List<MethodDocumentation>();

            foreach (var method in this.ClrType
                                       .GetMethods()
                                       .OrderBy(m => m.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                result.Add(new MethodDocumentation(this, method,
                                                   this.Assembly
                                                       .Xml
                                                       .XPathSelectElements(string.Format("members/member[@name='M:{0}.{1}{2}']",
                                                                                          this.ClrType.FullName,
                                                                                          method.Name,
                                                                                          ParameterHelper.CreateStringList(method.GetParameters())))
                                                       .FirstOrDefault()));
            }

            return result.ToArray();
        }

        #endregion Methods
    }
}