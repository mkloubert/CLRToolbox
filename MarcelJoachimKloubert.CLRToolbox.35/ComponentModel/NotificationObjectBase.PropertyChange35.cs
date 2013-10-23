// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    partial class NotificationObjectBase
    {
        #region Methods (3)

        // Protected Methods (2) 

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanged" /> event by using a LINQ compiler expression.
        /// </summary>
        /// <typeparam name="T">Type of the underlying property.</typeparam>
        /// <param name="expr">The property expression.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="expr" /> contains no <see cref="MemberExpression" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// <paramref name="expr" /> is no no property expression.
        /// </exception>
        protected bool OnPropertyChanged<T>(Expression<Func<T>> expr)
        {
            return this.OnPropertyChanged(GetPropertyName<T>(expr));
        }

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanging" /> event by using a LINQ compiler expression.
        /// </summary>
        /// <typeparam name="T">Type of the underlying property.</typeparam>
        /// <param name="expr">The property expression.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="expr" /> contains no <see cref="MemberExpression" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// <paramref name="expr" /> is no no property expression.
        /// </exception>
        protected bool OnPropertyChanging<T>(Expression<Func<T>> expr)
        {
            return this.OnPropertyChanging(GetPropertyName<T>(expr));
        }
        // Private Methods (1) 

        private static string GetPropertyName<T>(Expression<Func<T>> expr)
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

            var property = memberExpr.Member as PropertyInfo;
            if (property == null)
            {
                throw new InvalidCastException("expr.Body.Member");
            }

            return property.Name;
        }

        #endregion Methods
    }
}
