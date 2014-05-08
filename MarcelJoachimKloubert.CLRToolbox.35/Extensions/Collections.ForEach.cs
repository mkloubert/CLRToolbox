// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForEach{T}(IEnumerable{T}, Action{IForEachItemExecutionContext{T}})" />
        public static void ForEach<T>(this IEnumerable<T> seq, Action<IForEachItemExecutionContext<T>> action)
        {
            CollectionHelper.ForEach<T>(seq, action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForEach{T, S}(IEnumerable{T}, Action{IForEachItemExecutionContext{T, S}}, S)" />
        public static void ForEach<T, S>(this IEnumerable<T> seq, Action<IForEachItemExecutionContext<T, S>> action, S actionState)
        {
            CollectionHelper.ForEach<T, S>(seq, action, actionState);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForEach{T, S}(IEnumerable{T}, Action{IForEachItemExecutionContext{T, S}}, Func{T, long, S})" />
        public static void ForEach<T, S>(this IEnumerable<T> seq, Action<IForEachItemExecutionContext<T, S>> action, Func<T, long, S> actionStateFactory)
        {
            CollectionHelper.ForEach<T, S>(seq, action, actionStateFactory);
        }

        #endregion Methods
    }
}