// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class EnumHelper
    {
        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Parses a string to a nullable enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target type (nullable).</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <returns>The enum value.</returns>
        /// <exception cref="ArgumentException">Target type if no enum type.</exception>
        /// <exception cref="FormatException">Parsing failed.</exception>
        /// <remarks>
        /// If <paramref name="value" /> is <see langword="null" />, empty or contains
        /// whitespaces only, a <see langword="null" /> reference is returned.
        /// </remarks>
        public static TEnum? Parse<TEnum>(IEnumerable<char> value) where TEnum : struct
        {
            return Parse<TEnum>(value, false);
        }

        /// <summary>
        /// Parses a string to a nullable enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target type (nullable).</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <returns>The enum value.</returns>
        /// <exception cref="ArgumentException">Target type if no enum type.</exception>
        /// <exception cref="FormatException">Parsing failed.</exception>
        /// <remarks>
        /// If <paramref name="value" /> is <see langword="null" />, empty or contains
        /// whitespaces only, a <see langword="null" /> reference is returned.
        /// </remarks>
        public static TEnum? Parse<TEnum>(IEnumerable<char> value, bool ignoreCase) where TEnum : struct
        {
            return (TEnum?)ParseInner(typeof(TEnum?),
                                      StringHelper.AsString(value),
                                      ignoreCase);
        }
        // Private Methods (1) 

        private static object ParseInner(Type enumType, string value, bool ignoreCase)
        {
            Type underlyingType = Nullable.GetUnderlyingType(enumType);
            if (underlyingType != null)
            {
                // nullable struct / enum
                enumType = underlyingType;
            }

            if (enumType.IsEnum == false)
            {
                throw new ArgumentException("enumType");
            }

            if (StringHelper.IsNullOrWhiteSpace(value) == false)
            {
                try
                {
                    return Enum.Parse(enumType, value.Trim(), ignoreCase);
                }
                catch (Exception ex)
                {
                    throw new FormatException("Failed parsing string to enum value!",
                                              ex);
                }
            }

            return null;
        }

        #endregion Methods
    }
}
