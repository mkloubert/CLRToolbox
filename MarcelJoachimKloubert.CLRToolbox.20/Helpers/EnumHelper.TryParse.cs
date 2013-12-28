// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class EnumHelper
    {
        #region Methods (13)

        // Public Methods (12) 

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           out Nullable<TEnum> result) where TEnum : struct
        {
            return TryParse<TEnum>(value,
                                   false,
                                   out result);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           out TEnum result) where TEnum : struct
        {
            return TryParse<TEnum>(value,
                                   false,
                                   out result);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           bool ignoreCase,
                                           out Nullable<TEnum> result) where TEnum : struct
        {
            return TryParse<TEnum>(value,
                                   ignoreCase,
                                   out result,
                                   null);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           bool ignoreCase,
                                           out TEnum result) where TEnum : struct
        {
            return TryParse<TEnum>(value,
                                   ignoreCase,
                                   out result,
                                   default(TEnum));
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <param name="defaultValue">
        /// The value for <paramref name="result" /> if <paramref name="value" /> could not be parsed.
        /// </param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           out Nullable<TEnum> result,
                                           Nullable<TEnum> defaultValue) where TEnum : struct
        {
            return TryParse<TEnum>(value,
                                   false,
                                   out result,
                                   defaultValue);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <param name="defaultValue">
        /// The value for <paramref name="result" /> if <paramref name="value" /> could not be parsed.
        /// </param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           out TEnum result,
                                           TEnum defaultValue) where TEnum : struct
        {
            return TryParse<TEnum>(value,
                                   false,
                                   out result,
                                   defaultValue);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException"><paramref name="enumType" /> is NO valid type.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="enumType" /> is <see langword="null" />.</exception>
        public static bool TryParse(Type enumType,
                                    IEnumerable<char> value,
                                    out object result)
        {
            return TryParse(enumType,
                            value,
                            false,
                            out result);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <param name="defaultValue">
        /// The value for <paramref name="result" /> if <paramref name="value" /> could not be parsed.
        /// </param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           bool ignoreCase,
                                           out Nullable<TEnum> result,
                                           Nullable<TEnum> defaultValue) where TEnum : struct
        {
            object temp;
            bool success = TryParse(typeof(TEnum),
                                    value,
                                    ignoreCase,
                                    out temp,
                                    defaultValue);

            result = GlobalConverter.Current.ChangeType<TEnum>(temp);
            return success;
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <typeparam name="TEnum">Target enum type.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <param name="defaultValue">
        /// The value for <paramref name="result" /> if <paramref name="value" /> could not be parsed.
        /// </param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException">NO valid enum typedefined .</exception>
        public static bool TryParse<TEnum>(IEnumerable<char> value,
                                           bool ignoreCase,
                                           out TEnum result,
                                           TEnum defaultValue) where TEnum : struct
        {
            object temp;
            bool success = TryParse(typeof(TEnum),
                                    value,
                                    ignoreCase,
                                    out temp,
                                    defaultValue);

            result = GlobalConverter.Current.ChangeType<TEnum>(temp);
            return success;
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException"><paramref name="enumType" /> is NO valid type.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="enumType" /> is <see langword="null" />.</exception>
        public static bool TryParse(Type enumType,
                                    IEnumerable<char> value,
                                    bool ignoreCase,
                                    out object result)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }

            object defaultValue = null;
            if (Nullable.GetUnderlyingType(enumType) == null)
            {
                defaultValue = Activator.CreateInstance(enumType);
            }

            return TryParse(enumType,
                            value,
                            ignoreCase,
                            out result,
                            defaultValue);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <param name="defaultValue">
        /// The value for <paramref name="result" /> if <paramref name="value" /> could not be parsed.
        /// </param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException"><paramref name="enumType" /> is NO valid type.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="enumType" /> is <see langword="null" />.</exception>
        public static bool TryParse(Type enumType,
                                    IEnumerable<char> value,
                                    out object result,
                                    object defaultValue)
        {
            return TryParse(enumType,
                            value,
                            false,
                            out result,
                            defaultValue);
        }

        /// <summary>
        /// Tries to parse a string to a specific enum value.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value to parse.</param>
        /// <param name="ignoreCase">Ignore case or not.</param>
        /// <param name="result">The variable where to write the result to.</param>
        /// <param name="defaultValue">
        /// The value for <paramref name="result" /> if <paramref name="value" /> could not be parsed.
        /// </param>
        /// <returns><paramref name="value" /> could be parsed or not.</returns>
        /// <exception cref="ArgumentException"><paramref name="enumType" /> is NO valid type.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="enumType" /> is <see langword="null" />.</exception>
        public static bool TryParse(Type enumType,
                                    IEnumerable<char> value,
                                    bool ignoreCase,
                                    out object result,
                                    object defaultValue)
        {
            return TryParseInner(enumType,
                                 StringHelper.AsString(value),
                                 ignoreCase,
                                 out result,
                                 defaultValue);
        }
        // Private Methods (1) 

        [DebuggerStepThrough]
        private static bool TryParseInner(Type enumType,
                                          string value,
                                          bool ignoreCase,
                                          out object result,
                                          object defaultValue)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType");
            }

            bool success;
            object enumValue;
            try
            {
                enumValue = ParseInner(enumType, value, ignoreCase);
                success = true;
            }
            catch (FormatException)
            {
                success = false;
                enumValue = defaultValue;
            }

            result = GlobalConverter.Current
                                    .ChangeType(enumType, enumValue);
            return success;
        }

        #endregion Methods
    }
}
