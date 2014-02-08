// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows
{
    static partial class ClrToolboxWpfExtensionMethods
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Saves a bitmap source as a TIFF image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <returns>The TIFF image data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static byte[] SaveAsTiff(this BitmapSource src)
        {
            return SaveAsTiff(src, (TiffCompressOption?)null);
        }

        /// <summary>
        /// Saves a bitmap source as a TIFF image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="compression">This is, if defined, the compression of the output image.</param>
        /// <returns>The TIFF image data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static byte[] SaveAsTiff(this BitmapSource src, TiffCompressOption? compression)
        {
            using (var temp = new MemoryStream())
            {
                SaveAsTiff(src, temp, compression);

                return temp.ToArray();
            }
        }

        /// <summary>
        /// Saves a bitmap source as a TIFF image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="target">The stream where to write the TIFF image data to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="target" /> are <see langword="null" />.
        /// </exception>
        public static void SaveAsTiff(this BitmapSource src, Stream target)
        {
            SaveAsTiff(src, target, null);
        }

        /// <summary>
        /// Saves a bitmap source as a TIFF image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="target">The stream where to write the TIFF image data to.</param>
        /// <param name="compression">This is, if defined, the compression of the output image.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="target" /> are <see langword="null" />.
        /// </exception>
        public static void SaveAsTiff(this BitmapSource src, Stream target, TiffCompressOption? compression)
        {
            SaveBitmapSourceAs<TiffBitmapEncoder, TiffCompressOption?>(src, target,
                                                       (encoder, c) =>
                                                       {
                                                           if (c.HasValue)
                                                           {
                                                               encoder.Compression = c.Value;
                                                           }
                                                       }, compression);
        }

        #endregion Methods
    }
}
