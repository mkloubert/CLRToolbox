// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Data
{
    static partial class ClrToolboxDataExtensionMethods
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.BuildAll{TRead, T}(TRead, Func{TRead, T})" />
        public static IEnumerable<T> BuildAll<TRead, T>(this TRead rec, Func<TRead, T> func)
            where TRead : System.Data.IDataReader
        {
            return DataHelper.BuildAll<TRead, T>(rec, func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.BuildAll{TRead, S, T}(TRead, Func{TRead, S, T}, S)" />
        public static IEnumerable<T> BuildAll<TRead, S, T>(this TRead rec, Func<TRead, S, T> func, S funcState)
            where TRead : System.Data.IDataReader
        {
            return DataHelper.BuildAll<TRead, S, T>(rec, func, funcState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.BuildAll{TRead, S, T}(TRead, Func{TRead, S, T}, Func{TRead, S})" />
        public static IEnumerable<T> BuildAll<TRead, S, T>(this TRead rec, Func<TRead, S, T> func, Func<TRead, S> funcStateFactory)
            where TRead : System.Data.IDataReader
        {
            return DataHelper.BuildAll<TRead, S, T>(rec, func, funcStateFactory);
        }

        #endregion Methods
    }
}
