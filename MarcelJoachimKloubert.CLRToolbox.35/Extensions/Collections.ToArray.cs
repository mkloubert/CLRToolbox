// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CollectionHelper.ToArray(IEnumerable)" />
        public static object[] ToArray(this IEnumerable seq)
        {
            return CollectionHelper.ToArray(seq);
        }

        #endregion Methods
    }
}
