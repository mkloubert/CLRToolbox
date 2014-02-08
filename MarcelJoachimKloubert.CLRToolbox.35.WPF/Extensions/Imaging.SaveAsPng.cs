// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows
{
    static partial class ClrToolboxWpfExtensionMethods
    {
        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Saves a bitmap source as a PNG image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <returns>The PNG image data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public static byte[] SaveAsPng(this BitmapSource src)
        {
            using (var temp = new MemoryStream())
            {
                SaveAsPng(src, temp);

                return temp.ToArray();
            }
        }

        /// <summary>
        /// Saves a bitmap source as a PNG image.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="target">The stream where to write the PNG image data to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="target" /> are <see langword="null" />.
        /// </exception>
        public static void SaveAsPng(this BitmapSource src, Stream target)
        {
            SaveBitmapSourceAs<PngBitmapEncoder>(src, target);
        }

        #endregion Methods
    }
}
