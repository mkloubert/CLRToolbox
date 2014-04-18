// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers
{
    /// <summary>
    /// Helper class for CLR types.
    /// </summary>
    public static class TypeHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Returns the documentation for a <see cref="Type" />.
        /// </summary>
        /// <param name="type">The CLR type.</param>
        /// <param name="ignoreXmlDocErrors">
        /// Ignore errors while loading XML documentation for <paramref name="type" /> or (re-)throw exception(s).
        /// </param>
        /// <returns>
        /// The document for <paramref name="type" /> or <see langword="null" />
        /// if <paramref name="type" /> is also <see langword="null" />.
        /// </returns>
        public static TypeDocumentation GetDocumentation(Type type,
                                                         bool ignoreXmlDocErrors = true)
        {
            if (type == null)
            {
                return null;
            }

            var asm = type.Assembly;
            if (asm == null)
            {
                return null;
            }

            var asmDoc = AssemblyDocumentation.FromAssembly(asm,
                                                            ignoreXmlDocErrors: ignoreXmlDocErrors,
                                                            useCache: true);

            var result = asmDoc.GetTypes()
                               .FirstOrDefault(t => TypeHelper.GetFullName(t.ClrType) == TypeHelper.GetFullName(type));

            if (result == null)
            {
                result = new TypeDocumentation(asmDoc, type,
                                               xml: null);
            }

            return result;
        }

        /// <summary>
        /// Gets the full name of a CLR type.
        /// </summary>
        /// <param name="type">The CLR type.</param>
        /// <returns>
        /// The full name of <paramref name="type" /> or <see langword="null" />
        /// if <paramref name="type" /> is also <see langword="null" />.
        /// </returns>
        public static string GetFullName(Type type)
        {
            if (type == null)
            {
                return null;
            }

            if (type.IsGenericParameter)
            {
                return type.Name;
            }

            var ns = type.Namespace;
            if (string.IsNullOrWhiteSpace(ns))
            {
                ns = string.Empty;
            }
            else
            {
                ns = ns.Trim() + ".";
            }

            return ns + GetName(type);
        }
        
        /// <summary>
        /// Gets the short name of a CLR type.
        /// </summary>
        /// <param name="type">The CLR type.</param>
        /// <returns>
        /// The short name of <paramref name="type" /> or <see langword="null" />
        /// if <paramref name="type" /> is also <see langword="null" />.
        /// </returns>
        public static string GetName(Type type)
        {
            if (type == null)
            {
                return null;
            }

            var result = type.Name;
            if (string.IsNullOrWhiteSpace(result))
            {
                result = string.Empty;
            }
            else
            {
                result = result.Trim();
            }

            return result;
        }

        #endregion Methods
    }
}