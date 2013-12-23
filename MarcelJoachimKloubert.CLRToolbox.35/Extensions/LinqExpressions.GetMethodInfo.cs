// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Returns the <see cref="MethodInfo" /> object by a LINQ compiler expression.
        /// </summary>
        /// <typeparam name="O">Type of the underlying object.</typeparam>
        /// <param name="obj">The object from where to get the name from.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <returns>The method information.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="expr" /> contains no valid expression to extract the <see cref="MethodInfo" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <paramref name="obj" /> can be <see langword="null" /> here.
        /// </remarks>
        public static MethodInfo GetMethodInfo<O>(this O obj, Expression<Action<O>> expr)
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }

            var methodCallExpr = expr.Body as MethodCallExpression;
            if (methodCallExpr == null)
            {
                throw new ArgumentException("expr.Body");
            }

            return methodCallExpr.Method;
        }

        /// <summary>
        /// Returns the <see cref="MethodInfo" /> object by a LINQ compiler expression.
        /// </summary>
        /// <typeparam name="O">Type of the underlying object.</typeparam>
        /// <typeparam name="R">(Result) Type of the member.</typeparam>
        /// <param name="obj">The object from where to get the name from.</param>
        /// <param name="expr">The expression from where to extract the name from.</param>
        /// <returns>The method information.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="expr" /> contains no valid expression to extract the <see cref="MethodInfo" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <paramref name="obj" /> can be <see langword="null" /> here.
        /// </remarks>
        public static MethodInfo GetMethodInfo<O, R>(this O obj, Expression<Func<O, R>> expr)
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }

            var methodCallExpr = expr.Body as MethodCallExpression;
            if (methodCallExpr == null)
            {
                throw new ArgumentException("expr.Body");
            }

            return methodCallExpr.Method;
        }

        #endregion Methods
    }
}
