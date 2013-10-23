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
        /// <see cref="StringHelper.AsString(object)" />
        public static string AsString(this object obj)
        {
            return StringHelper.AsString(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StringHelper.AsString(object, bool)" />
        public static string AsString(this object obj, bool handleDBNullAsNull)
        {
            return StringHelper.AsString(obj, handleDBNullAsNull);
        }

        #endregion Methods
    }
}
