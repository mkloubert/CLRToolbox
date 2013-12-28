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
        /// <see cref="CompressionHelper.GZip(IEnumerable{byte})" />
        public static byte[] GZip(this IEnumerable<byte> data)
        {
            return CompressionHelper.GZip(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GZip(Stream)" />
        public static byte[] GZip(this Stream src)
        {
            return CompressionHelper.GZip(src);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GZip(Stream, int)" />
        public static byte[] GZip(this Stream src, int bufferSize)
        {
            return CompressionHelper.GZip(src, bufferSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GZip(IEnumerable{byte}, Stream)" />
        public static void GZip(this IEnumerable<byte> data, Stream dest)
        {
            CompressionHelper.GZip(data, dest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GZip(Stream, Stream)" />
        public static void GZip(this Stream src, Stream dest)
        {
            CompressionHelper.GZip(src, dest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="CompressionHelper.GZip(Stream, Stream, int)" />
        public static void GZip(this Stream src, Stream dest, int bufferSize)
        {
            CompressionHelper.GZip(src, dest, bufferSize);
        }

        #endregion Methods
    }
}
