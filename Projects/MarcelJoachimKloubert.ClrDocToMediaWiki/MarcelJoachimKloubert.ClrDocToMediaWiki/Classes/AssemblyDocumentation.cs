// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Stores the documentation of an <see cref="ClrAssembly" />.
    /// </summary>
    public sealed class AssemblyDocumentation : DocumentableBase
    {
        #region Fields (2)

        private static IDictionary<Assembly, AssemblyDocumentation> _CACHE = new ConcurrentDictionary<Assembly, AssemblyDocumentation>();
        private Lazy<IEnumerable<TypeDocumentation>> _types;

        #endregion Fields

        #region Constructors (1)

        private AssemblyDocumentation(XElement xml)
            : base(xml: xml)
        {
            this.UpdateSettings(null);

            this.Reset();
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the underying assembly.
        /// </summary>
        public Assembly ClrAssembly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current settings.
        /// </summary>
        public IDocumentationSettings Settings
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (7) 

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

            var result = new AssemblyDocumentation(xml)
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
                                useCache: useCache);
        }

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
        /// Returns all types of that assembly.
        /// </summary>
        /// <returns>The list of types.</returns>
        public IEnumerable<TypeDocumentation> GetTypes()
        {
            return this._types.Value;
        }

        /// <summary>
        /// Resets the state of the assembly documentations.
        /// </summary>
        public void Reset()
        {
            this._types = new Lazy<IEnumerable<TypeDocumentation>>(this.GetTypesInner, isThreadSafe: true);
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return "A:" + this.ClrAssembly.FullName;
        }

        /// <summary>
        /// Updates <see cref="AssemblyDocumentation.Settings" /> property.
        /// </summary>
        /// <param name="newSettings">
        /// The new value. If <see langword="null" /> a default instance is set.
        /// </param>
        public void UpdateSettings(IDocumentationSettings newSettings)
        {
            this.Settings = newSettings ?? new DocumentationSettings();
        }

        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override IEnumerable<char> OnGetWikiPageName()
        {
            var @namespace = this.Settings.Namespace;

            byte[] pubKey;
            try
            {
                pubKey = this.ClrAssembly.GetName().GetPublicKeyToken();
            }
            catch
            {
                pubKey = null;
            }

            string asmName = null;
            try
            {
                var attrib = this.ClrAssembly
                                 .GetCustomAttributes(typeof(global::System.Reflection.AssemblyProductAttribute), false)
                                 .OfType<AssemblyProductAttribute>()
                                 .FirstOrDefault(a => string.IsNullOrWhiteSpace(a.Product) == false);

                if (attrib != null)
                {
                    asmName = (attrib.Product ?? string.Empty).Trim();
                }
            }
            catch
            {
                asmName = null;
            }

            if (string.IsNullOrWhiteSpace(asmName) == false)
            {
                var asmPath = this.ClrAssembly.Location;
                if (string.IsNullOrWhiteSpace(asmPath) == false)
                {
                    var asmFile = new FileInfo(asmPath);

                    asmName = Path.GetFileNameWithoutExtension(asmFile.Name);
                }
            }

            if (asmName != null)
            {
                asmName = asmName.Replace('.', '_')
                                 .Replace(' ', '_');
            }

            string asmPageName;
            if (pubKey != null && pubKey.Length > 0)
            {
                asmPageName = string.Format("{0}_{1}",
                                            asmName,
                                            pubKey.AsHexString());
            }
            else
            {
                asmPageName = asmName;
            }

            if (string.IsNullOrWhiteSpace(@namespace))
            {
                return asmPageName;
            }
            else
            {
                return @namespace + "/" + asmPageName;
            }
        }

        /// <inheriteddoc />
        protected override void OnToMediaWiki(ref StringBuilder builder)
        {
            var nsFilter = this.Settings.NamespaceFilter;

            Regex regex = null;
            RegexOptions? regexOpts = null;
            if (nsFilter != null)
            {
                if (regexOpts.HasValue)
                {
                    regex = new Regex(nsFilter, regexOpts.Value);
                }
                else
                {
                    regex = new Regex(nsFilter);
                }
            }

            builder.AppendFormat("== {0} ==", this.ClrAssembly.FullName)
                   .AppendLine();

            // extension methods
            {
                builder.AppendLine()
                       .AppendFormat("=== Extension methods ===")
                       .AppendLine();

                var extensionMethods = this.GetTypes()
                                           .SelectMany(t => t.GetMethods())
                                           .Where(m => m.IsExtensionMethod);

                if (this.Settings.DocumentPublicMethods == false)
                {
                    extensionMethods = extensionMethods.Where(m => m.ClrMember.IsPublic == false);
                }

                if (this.Settings.DocumentPrivateMethods == false)
                {
                    extensionMethods = extensionMethods.Where(m => m.ClrMember.IsPublic);
                }

                if (extensionMethods.Count() > 0)
                {
                    foreach (var grpMethods in extensionMethods.GroupBy(em =>
                                                                        {
                                                                            var name = (em.ClrMember.Name ?? string.Empty).ToUpper().Trim();
                                                                            if (name != string.Empty)
                                                                            {
                                                                                var firstLetter = name[0];
                                                                                if (firstLetter >= '0' && firstLetter <= '9')
                                                                                {
                                                                                    return "0 - 9";
                                                                                }
                                                                                else if ((firstLetter >= 'A' && firstLetter <= 'Z'))
                                                                                {
                                                                                    return firstLetter.ToString();
                                                                                }
                                                                                else if ((firstLetter >= 'Ä' && firstLetter <= 'Ü') ||
                                                                                         (firstLetter == 'ß'))
                                                                                {
                                                                                    return "Ä - Ü, ß";
                                                                                }
                                                                            }

                                                                            return string.Empty;
                                                                        })
                                                               .OrderBy(gex => gex.Key, StringComparer.InvariantCultureIgnoreCase))
                    {
                        var title = grpMethods.Key;

                        builder.AppendFormat("==== {0} ====",
                                             title != string.Empty ? title : "#")
                               .AppendLine();

                        builder.AppendLine(@"{| class=""prettytable"" style=""width:100%""");

                        builder.AppendLine(@"|-
! style=""width: 192px"" |'''Name'''
!'''Summary'''
! style=""width: 192px"" |'''Target type'''
! style=""width: 192px"" |'''Namespace'''");

                        foreach (var method in grpMethods.OrderBy(t => t.ClrMember.Name, StringComparer.InvariantCultureIgnoreCase)
                                                         .ThenBy(t => t.ToString(), StringComparer.InvariantCultureIgnoreCase))
                        {
                            builder.AppendLine()
                                   .AppendFormat(@"|-
|[[{0}]]
|{1}
|{2}
|{3}
", WikiHelper.ToTableCellValue(method.DisplayName)
 , WikiHelper.ToTableCellValue((WikiHelper.ToWikiMarkup(method.Summary) ?? string.Empty).Trim())
 , WikiHelper.ToTableCellValue(method.ExtensionMethodTargetType.FullDisplayName)
 , WikiHelper.ToTableCellValue(method.ClrMember.DeclaringType.Namespace));
                        }

                        builder.AppendLine()
                               .AppendLine(@"|}");
                    }
                }
            }

            // types
            {
                builder.AppendLine()
                       .AppendFormat("=== Types ===")
                       .AppendLine();

                foreach (var grpTypes in this.GetTypes()
                                             .Where(t => regex == null ||
                                                         regex.IsMatch(t.ClrType.Namespace ?? string.Empty))
                                             .GroupBy(t => (t.ClrType.Namespace ?? string.Empty).Trim())
                                             .OrderBy(t => t.Key, StringComparer.InvariantCultureIgnoreCase))
                {
                    var @namespace = grpTypes.Key;
                    IEnumerable<TypeDocumentation> typesToList = grpTypes.ToArray();

                    if (this.Settings.DocumentPublicTypes == false)
                    {
                        typesToList = typesToList.Where(t => t.ClrType.IsPublic == false);
                    }

                    if (this.Settings.DocumentPrivateTypes == false)
                    {
                        typesToList = typesToList.Where(t => t.ClrType.IsPublic);
                    }

                    if (typesToList.Count() < 1)
                    {
                        continue;
                    }

                    builder.AppendLine();

                    builder.AppendFormat("==== {0} ====",
                                         @namespace != string.Empty ? @namespace : "(no namespace)")
                           .AppendLine();

                    builder.AppendLine(@"{| class=""prettytable"" style=""width:100%""");

                    builder.AppendLine(@"|-
! style=""width: 192px"" |'''Name'''
!'''Summary'''");

                    foreach (var type in typesToList.OrderBy(t => t.ClrType.Name, StringComparer.InvariantCultureIgnoreCase))
                    {
                        builder.AppendLine()
                               .AppendFormat(@"|-
|[[{0}]]
|{1}
", WikiHelper.ToTableCellValue(type.ClrType.Name)
 , (WikiHelper.ToWikiMarkup(type.Summary) ?? string.Empty).Trim());
                    }

                    builder.AppendLine()
                           .AppendLine(@"|}");
                }
            }
        }

        // Private Methods (1) 

        private IEnumerable<TypeDocumentation> GetTypesInner()
        {
            var result = new List<TypeDocumentation>();

            foreach (var type in this.ClrAssembly
                                     .GetTypes()
                                     .OrderBy(t => t.FullName, StringComparer.InvariantCultureIgnoreCase))
            {
                result.Add(new TypeDocumentation(this, type,
                                                 this.Xml
                                                     .XPathSelectElements(string.Format("members/member[@name='T:{0}']",
                                                                                        type.FullName))
                                                     .SingleOrDefault()));
            }

            return result.ToArray();
        }

        #endregion Methods
    }
}