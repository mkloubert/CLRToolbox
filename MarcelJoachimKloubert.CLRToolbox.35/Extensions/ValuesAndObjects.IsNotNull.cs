// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueHelper.IsNotNull{T}(T)" />
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return ValueHelper.IsNotNull<T>(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueHelper.IsNotNull{T}(T?)" />
        public static bool IsNotNull<T>(this T? obj) where T : struct
        {
            return ValueHelper.IsNotNull<T>(obj);
        }

        #endregion Methods
    }
}
