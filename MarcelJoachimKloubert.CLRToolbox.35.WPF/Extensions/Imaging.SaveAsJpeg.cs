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
        /// Saves a bitmap source as a JPEG image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <returns>The JPEG image data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static byte[] SaveAsJpeg(this BitmapSource src)
        {
            return SaveAsJpeg(src, (int?)null);
        }

        /// <summary>
        /// Saves a bitmap source as a JPEG image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="quality">This is, if defined, the quality of the output image (from 1 to 100).</param>
        /// <returns>The JPEG image data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="quality" /> is invalid.
        /// </exception>
        public static byte[] SaveAsJpeg(this BitmapSource src, int? quality)
        {
            using (var temp = new MemoryStream())
            {
                SaveAsJpeg(src, temp, quality);

                return temp.ToArray();
            }
        }

        /// <summary>
        /// Saves a bitmap source as a JPEG image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="target">The stream where to write the JPEG image data to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="target" /> are <see langword="null" />.
        /// </exception>
        public static void SaveAsJpeg(this BitmapSource src, Stream target)
        {
            SaveAsJpeg(src, target, null);
        }

        /// <summary>
        /// Saves a bitmap source as a JPEG image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="target">The stream where to write the JPEG image data to.</param>
        /// <param name="quality">This is, if defined, the quality of the output image (from 1 to 100).</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="target" /> are <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="quality" /> is invalid.
        /// </exception>
        public static void SaveAsJpeg(this BitmapSource src, Stream target, int? quality)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (quality < 1 || quality > 100)
            {
                throw new ArgumentOutOfRangeException("quality");
            }

            SaveBitmapSourceAs<JpegBitmapEncoder, int?>(src, target,
                                                       (encoder, q) =>
                                                       {
                                                           if (q.HasValue)
                                                           {
                                                               encoder.QualityLevel = q.Value;
                                                           }
                                                       }, quality);
        }

        #endregion Methods
    }
}
