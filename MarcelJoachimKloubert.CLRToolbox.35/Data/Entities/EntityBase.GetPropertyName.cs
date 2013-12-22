// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Entities
{
    partial class EntityBase
    {
        #region Methods (1)

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
