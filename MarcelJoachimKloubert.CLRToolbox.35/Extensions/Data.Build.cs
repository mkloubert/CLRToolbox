// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
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
        /// <see cref="DataHelper.Build{TRec, T}(TRec, Func{TRec, T})" />
        public static T Build<TRec, T>(this TRec rec, Func<TRec, T> func)
            where TRec : System.Data.IDataRecord
        {
            return DataHelper.Build<TRec, T>(rec, func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.Build{TRec, S, T}(TRec, Func{TRec, S, T}, S)" />
        public static T Build<TRec, S, T>(this TRec rec, Func<TRec, S, T> func, S funcState)
            where TRec : System.Data.IDataRecord
        {
            return DataHelper.Build<TRec, S, T>(rec, func, funcState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="DataHelper.Build{TRec, S, T}(TRec, Func{TRec, S, T}, Func{TRec, S})" />
        public static T Build<TRec, S, T>(this TRec rec, Func<TRec, S, T> func, Func<TRec, S> funcStateFactory)
            where TRec : System.Data.IDataRecord
        {
            return DataHelper.Build<TRec, S, T>(rec, func, funcStateFactory);
        }

        #endregion Methods
    }
}
