// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class MethodHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Tries to return the delegate type for a method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The type or <see langword="null" /> if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="method" /> is <see langword="null" />.
        /// </exception>
        public static Type TryGetDelegateTypeFromMethod(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            Type result = null;

            ParameterInfo[] @params = method.GetParameters();

            Type delegateType = null;
            Type delegateReturnType = null;

            Type returnType = method.ReturnType;
            if (returnType == null ||
                typeof(void).Equals(returnType))
            {
                // action

                foreach (Type type in KNOWN_ACTION_TYPES)
                {
                    if (type.GetGenericArguments().LongLength == @params.LongLength)
                    {
                        delegateType = type;
                        break;
                    }
                }
            }
            else
            {
                // function
                delegateReturnType = returnType;

                foreach (Type type in KNOWN_FUNC_TYPES)
                {
                    if (type.GetGenericArguments().LongLength ==
                        @params.LongLength + 1)
                    {
                        delegateType = type;
                        break;
                    }
                }
            }

            if (delegateType != null)
            {
                List<Type> delegateTypesForGenericArgs = new List<Type>();
                foreach (ParameterInfo pi in @params)
                {
                    delegateTypesForGenericArgs.Add(pi.ParameterType);
                }

                if (delegateReturnType != null)
                {
                    delegateTypesForGenericArgs.Add(delegateReturnType);
                }

                result = delegateType.MakeGenericType(delegateTypesForGenericArgs.ToArray());
            }

            return result;
        }

        #endregion Methods
    }
}
