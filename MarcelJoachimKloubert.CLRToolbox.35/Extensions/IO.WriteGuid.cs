// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
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
        /// <see cref="IOHelper.WriteGuid(BinaryWriter, Guid)" />
        public static void WriteGuid(this BinaryWriter writer, Guid value)
        {
            IOHelper.WriteGuid(writer, value);
        }

        #endregion Methods
    }
}
