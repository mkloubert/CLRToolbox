// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Stores the documentation of an <see cref="ClrAssembly" />.
    /// </summary>
    public sealed class AssemblyDocumentation : DocumentableBase
    {
        #region Constructors (1)

        private AssemblyDocumentation(XElement xml)
            : base(xml: xml)
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underying assembly.
        /// </summary>
        public Assembly ClrAssembly
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Creates a new instance from an file.
        /// </summary>
        /// <param name="asmFile">The assembly file.</param>
        /// <param name="ignoreXmlDocErrors">
        /// Ignore errors if XML documentation could not be loaded.
        /// </param>
        /// <returns>The created instance.</returns>
        public static AssemblyDocumentation FromFile(FileInfo asmFile,
                                                     bool ignoreXmlDocErrors = true)
        {
            if (asmFile == null)
            {
                throw new ArgumentNullException("asmFile");
            }

            var asm = Assembly.LoadFrom(asmFile.FullName);

            XElement xml = null;
            try
            {
                var xmlFile = CollectionHelper.SingleOrDefault(asmFile.Directory.GetFiles(),
                                                               f => f.Name.ToLower().Trim() ==
                                                                    string.Format("{0}.xml",
                                                                                  Path.GetFileNameWithoutExtension(f.Name)).ToLower().Trim());

                if (xmlFile.Exists)
                {
                    using (var stream = xmlFile.OpenRead())
                    {
                        xml = XDocument.Load(stream).Root;
                    }
                }
            }
            catch
            {
                xml = null;

                if (ignoreXmlDocErrors == false)
                {
                    throw;
                }
            }

            return new AssemblyDocumentation(xml)
            {
                ClrAssembly = asm,
            };
        }

        /// <summary>
        /// Returns all types of that assembly.
        /// </summary>
        /// <returns>The list of types.</returns>
        public IEnumerable<TypeDocumentation> GetTypes()
        {
            foreach (var type in this.ClrAssembly
                                     .GetTypes()
                                     .OrderBy(t => t.FullName, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return new TypeDocumentation(this, type,
                                                   this.Xml
                                                       .XPathSelectElements(string.Format("members/member[@name='T:{0}']",
                                                                                          type.FullName))
                                                       .SingleOrDefault());
            }
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return "A:" + this.ClrAssembly.FullName;
        }

        #endregion Methods
    }
}