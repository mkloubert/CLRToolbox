// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Private Methods (1) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="ReflectionHelper.GetImplicitOperators(Type)" />
        public static IEnumerable<MethodInfo> GetImplicitOperators(this Type type)
        {
            return ReflectionHelper.GetImplicitOperators(type);
        }

        #endregion Methods
    }
}