// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class ReflectionHelper
    {
        #region Methods (2)

        // Private Methods (2) 

        /// <summary>
        /// Returns all implicit operators of a type.
        /// </summary>
        /// <typeparam name="T">The type from where to get the operators from.</typeparam>
        /// <returns>The implicit operator methods.</returns>
        public static IEnumerable<MethodInfo> GetImplicitOperators<T>()
        {
            return GetImplicitOperators(typeof(T));
        }

        /// <summary>
        /// Returns all implicit operators of a type.
        /// </summary>
        /// <param name="type">The type from where to get the operators from.</param>
        /// <returns>The implicit operator methods.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<MethodInfo> GetImplicitOperators(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IEnumerable<MethodInfo> allStaticMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);

            using (IEnumerator<MethodInfo> e = allStaticMethods.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    MethodInfo method = e.Current;

                    if (method.Name == "op_Implicit")
                    {
                        yield return method;
                    }
                }
            }
        }

        #endregion Methods
    }
}