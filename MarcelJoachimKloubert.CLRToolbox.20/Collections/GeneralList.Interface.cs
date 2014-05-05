// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections
{
    partial class GeneralList : List<object>, IGeneralList
    {
        #region Methods (5)

        // Private Methods (5) 

        /// <inheriteddoc />
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <inheriteddoc />
        IGeneralList IGeneralList.Clone()
        {
            return this.Clone();
        }

        TResult IGeneralList.Materialize<TResult>(Func<IGeneralList, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.Materialize<TResult>(delegate(GeneralList list)
                                             {
                                                 return func(list);
                                             });
        }

        TResult IGeneralList.Materialize<TState, TResult>(Func<IGeneralList, TState, TResult> func, TState funcState)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.Materialize<TState, TResult>(delegate(GeneralList list, TState state)
                                                     {
                                                         return func(list, state);
                                                     }, funcState);
        }

        TResult IGeneralList.Materialize<TState, TResult>(Func<IGeneralList, TState, TResult> func, Func<IGeneralList, TState> funcStateFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("funcStateFactory");
            }

            return this.Materialize<TState, TResult>(delegate(GeneralList list, TState state)
                                                     {
                                                         return func(list, state);
                                                     },
                                                     delegate(GeneralList list)
                                                     {
                                                         return funcStateFactory(list);
                                                     });
        }

        #endregion Methods
    }
}