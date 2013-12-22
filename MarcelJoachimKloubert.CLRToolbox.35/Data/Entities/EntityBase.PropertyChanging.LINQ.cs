// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq.Expressions;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Entities
{
    partial class EntityBase
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Raises the <see cref="EntityBase.PropertyChanging" /> event by using a LINQ compiler expression.
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

        #endregion Methods
    }
}
