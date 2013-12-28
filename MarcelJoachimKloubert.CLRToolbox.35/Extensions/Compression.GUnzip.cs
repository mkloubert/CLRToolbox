// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GUnzip(IEnumerable{byte})" />
        public static byte[] GUnzip(this IEnumerable<byte> data)
        {
            return CompressionHelper.GUnzip(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GUnzip(Stream)" />
        public static byte[] GUnzip(this Stream src)
        {
            return CompressionHelper.GUnzip(src);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GUnzip(Stream, int)" />
        public static byte[] GUnzip(this Stream src, int bufferSize)
        {
            return CompressionHelper.GUnzip(src, bufferSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GUnzip(IEnumerable{byte}, Stream)" />
        public static void GUnzip(this IEnumerable<byte> data, Stream dest)
        {
            CompressionHelper.GUnzip(data, dest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GUnzip(Stream, Stream)" />
        public static void GUnzip(this Stream src, Stream dest)
        {
            CompressionHelper.GUnzip(src, dest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GUnzip(Stream, Stream, int)" />
        public static void GUnzip(this Stream src, Stream dest, int bufferSize)
        {
            CompressionHelper.GUnzip(src, dest, bufferSize);
        }

        #endregion Methods
    }
}
