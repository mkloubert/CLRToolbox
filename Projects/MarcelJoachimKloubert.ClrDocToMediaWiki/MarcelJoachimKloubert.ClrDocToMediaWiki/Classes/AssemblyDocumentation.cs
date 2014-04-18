// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Concurrent;
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
        #region Fields (1)

        private static IDictionary<Assembly, AssemblyDocumentation> _CACHE = new ConcurrentDictionary<Assembly, AssemblyDocumentation>();

        #endregion Fields

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

        #region Methods (5)

        // Public Methods (5) 

        /// <summary>
        /// Returns all cached items.
        /// </summary>
        /// <returns>Cached items.</returns>
        public static IEnumerable<AssemblyDocumentation> GetCache()
        {
            foreach (var item in _CACHE.OrderBy(t => t.Key.FullName, StringComparer.InvariantCultureIgnoreCase))
            {
                yield return item.Value;
            }
        }

        /// <summary>
        /// Creates a new instance from an <see cref="Assembly" /> object.
        /// </summary>
        /// <param name="asm">The underlying assembly object.</param>
        /// <param name="ignoreXmlDocErrors">
        /// Ignore errors if XML documentation could not be loaded or (re-)throw exceptions.
        /// </param>
        /// <returns>The created instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asmFile" /> is <see langword="null" />.
        /// </exception>
        public static AssemblyDocumentation FromAssembly(Assembly asm,
                                                         bool ignoreXmlDocErrors = true,
                                                         bool useCache = true)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("asm");
            }

            if (useCache)
            {
                AssemblyDocumentation cachedAsm;
                if (_CACHE.TryGetValue(asm, out cachedAsm))
                {
                    return cachedAsm;
                }
            }

            // try find XML file
            FileInfo xmlFile = null;
            try
            {
                var path = asm.Location;
                if (string.IsNullOrWhiteSpace(path) == false)
                {
                    var asmFile = new FileInfo(path);

                    xmlFile = CollectionHelper.SingleOrDefault(asmFile.Directory.GetFiles(),
                                                               f => f.Name.ToLower().Trim() ==
                                                                    string.Format("{0}.xml",
                                                                                  Path.GetFileNameWithoutExtension(f.Name)).ToLower().Trim());
                }
            }
            catch
            {
                xmlFile = null;
            }

            XElement xml = null;
            if (xmlFile != null)
            {
                try
                {
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
            }

            if (xml == null)
            {
                xml = new XElement("doc");
            }
            
            var result =  new AssemblyDocumentation(xml)
            {
                ClrAssembly = asm,
            };

            if (useCache)
            {
                _CACHE[asm] = result;
            }

            return result;
        }

        /// <summary>
        /// Creates a new instance from an file.
        /// </summary>
        /// <param name="asmFile">The assembly file.</param>
        /// <param name="ignoreXmlDocErrors">
        /// Ignore errors if XML documentation could not be loaded or (re-)throw exceptions.
        /// </param>
        /// <returns>The created instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asmFile" /> is <see langword="null" />.
        /// </exception>
        public static AssemblyDocumentation FromFile(FileInfo asmFile,
                                                     bool ignoreXmlDocErrors = true,
                                                     bool useCache = false)
        {
            if (asmFile == null)
            {
                throw new ArgumentNullException("asmFile");
            }

            var asm = Assembly.LoadFrom(asmFile.FullName);

            return FromAssembly(asm: asm,
                                ignoreXmlDocErrors: ignoreXmlDocErrors,
                                useCache:useCache);
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