// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueHelper.IsNull{T}(T)" />
        public static bool IsNull<T>(this T obj) where T : class
        {
            return ValueHelper.IsNull<T>(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueHelper.IsNull{T}(T?)" />
        public static bool IsNull<T>(this Nullable<T> obj) where T : struct
        {
            return ValueHelper.IsNull<T>(obj);
        }

        #endregion Methods
    }
}
