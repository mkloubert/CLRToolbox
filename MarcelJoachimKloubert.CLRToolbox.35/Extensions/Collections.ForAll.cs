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
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForAll{T}(IEnumerable{T}, Action{IForAllItemExecutionContext{T}})" />
        public static AggregateException ForAll<T>(this IEnumerable<T> items,
                                                   Action<IForAllItemExecutionContext<T>> action)
        {
            return CollectionHelper.ForAll<T>(items,
                                              action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForAll{T}(IEnumerable{T}, Action{IForAllItemExecutionContext{T}}, bool)" />
        public static AggregateException ForAll<T>(this IEnumerable<T> items,
                                                   Action<IForAllItemExecutionContext<T>> action,
                                                   bool throwExceptions)
        {
            return CollectionHelper.ForAll<T>(items,
                                              action,
                                              throwExceptions);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForAll{T, S}(IEnumerable{T}, Action{IForAllItemExecutionContext{T, S}}, S)" />
        public static AggregateException ForAll<T, S>(this IEnumerable<T> items,
                                                      Action<IForAllItemExecutionContext<T, S>> action,
                                                      S actionState)
        {
            return CollectionHelper.ForAll<T, S>(items,
                                                 action,
                                                 actionState);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForAll{T, S}(IEnumerable{T}, Action{IForAllItemExecutionContext{T, S}}, Func{T, long, S})" />
        public static AggregateException ForAll<T, S>(this IEnumerable<T> items,
                                                      Action<IForAllItemExecutionContext<T, S>> action,
                                                      Func<T, long, S> actionStateFactory)
        {
            return CollectionHelper.ForAll<T, S>(items,
                                                 action,
                                                 actionStateFactory);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForAll{T, S}(IEnumerable{T}, Action{IForAllItemExecutionContext{T, S}}, S, bool)" />
        public static AggregateException ForAll<T, S>(this IEnumerable<T> items,
                                                      Action<IForAllItemExecutionContext<T, S>> action,
                                                      S actionState,
                                                      bool throwExceptions)
        {
            return CollectionHelper.ForAll<T, S>(items,
                                                 action,
                                                 actionState,
                                                 throwExceptions);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="CollectionHelper.ForAll{T, S}(IEnumerable{T}, Action{IForAllItemExecutionContext{T, S}}, Func{T, long, S}, bool)" />
        public static AggregateException ForAll<T, S>(this IEnumerable<T> items,
                                                      Action<IForAllItemExecutionContext<T, S>> action,
                                                      Func<T, long, S> actionStateFactory,
                                                      bool throwExceptions)
        {
            return CollectionHelper.ForAll<T, S>(items,
                                                 action,
                                                 actionStateFactory,
                                                 throwExceptions);
        }

        #endregion Methods
    }
}