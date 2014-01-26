// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;
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
        /// <see cref="IOHelper.ToByteArray(Stream)" />
        public static byte[] ToByteArray(this Stream stream)
        {
            return IOHelper.ToByteArray(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IOHelper.ToByteArray(Stream, int)" />
        public static byte[] ToByteArray(this Stream stream, int bufferSize)
        {
            return IOHelper.ToByteArray(stream, bufferSize);
        }

        #endregion Methods
    }
}
