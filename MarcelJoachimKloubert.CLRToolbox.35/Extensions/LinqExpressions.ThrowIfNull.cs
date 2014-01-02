// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> is a parameter / field is <see langword="null" /> / <see cref="DBNull" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <remarks>
        /// <see cref="DBNull" /> is handled as <see langword="null" />.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="expr" /> are <see langword="null" />.
        /// </exception>
        public static void ThrowIfNull<T>(this T obj, Expression<Func<T>> expr) where T : class
        {
            ThrowIfNull<T>(obj, expr, null);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> is a parameter / field is <see langword="null" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="value">The object to check.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value" /> and/or <paramref name="expr" /> are <see langword="null" />.
        /// </exception>
        public static void ThrowIfNull<T>(this T? value, Expression<Func<T?>> expr) where T : struct
        {
            ThrowIfNull<T>(value, expr, null);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> is a parameter / field is <see langword="null" /> / <see cref="DBNull" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <param name="msg">The optional exception message.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="expr" /> are <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <see cref="DBNull" /> is handled as <see langword="null" />.
        /// </remarks>
        public static void ThrowIfNull<T>(this T obj, Expression<Func<T>> expr, IEnumerable<char> msg) where T : class
        {
            ThrowIfNull<T>(obj, expr, msg, true);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> is a parameter / field is <see langword="null" /> / <see cref="DBNull" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="expr" /> are <see langword="null" />.
        /// </exception>
        /// <param name="handleDBNullAsNull">
        /// Handle <see cref="DBNull" /> as <see langword="null" /> or not.
        /// </param>
        public static void ThrowIfNull<T>(this T obj, Expression<Func<T>> expr, bool handleDBNullAsNull) where T : class
        {
            ThrowIfNull<T>(obj, expr, null, handleDBNullAsNull);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> is a parameter / field is <see langword="null" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="value">The object to check.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <param name="msg">The optional exception message.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value" /> and/or <paramref name="expr" /> are <see langword="null" />.
        /// </exception>
        public static void ThrowIfNull<T>(this T? value, Expression<Func<T?>> expr, IEnumerable<char> msg) where T : struct
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }

            var memberExpr = expr.Body as MemberExpression;
            if (memberExpr == null)
            {
                throw new ArgumentException("expr.Body");
            }

            var field = memberExpr.Member as FieldInfo;
            if (field == null)
            {
                throw new ArgumentException("expr.Body.Member");
            }

            if (!value.HasValue)
            {
                var strMsg = AsString(msg);
                if (IsNullOrWhiteSpace(strMsg))
                {
                    throw new ArgumentNullException(field.Name);
                }
                else
                {
                    throw new ArgumentNullException(field.Name,
                                                    strMsg);
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> is a parameter / field is <see langword="null" /> / <see cref="DBNull" />.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <param name="msg">The optional exception message.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="expr" /> are <see langword="null" />.
        /// </exception>
        /// <param name="handleDBNullAsNull">
        /// Handle <see cref="DBNull" /> as <see langword="null" /> or not.
        /// </param>
        public static void ThrowIfNull<T>(this T obj, Expression<Func<T>> expr, IEnumerable<char> msg, bool handleDBNullAsNull) where T : class
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }

            var memberExpr = expr.Body as MemberExpression;
            if (memberExpr == null)
            {
                throw new ArgumentException("expr.Body");
            }

            var field = memberExpr.Member as FieldInfo;
            if (field == null)
            {
                throw new ArgumentException("expr.Body.Member");
            }

            if (handleDBNullAsNull &&
                DBNull.Value.Equals(obj))
            {
                obj = null;
            }

            if (obj == null)
            {
                var strMsg = AsString(msg);
                if (IsNullOrWhiteSpace(strMsg))
                {
                    throw new ArgumentNullException(field.Name);
                }
                else
                {
                    throw new ArgumentNullException(field.Name,
                                                    strMsg);
                }
            }
        }

        #endregion Methods
    }
}
