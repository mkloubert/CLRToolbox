// 
// WPF based tool to create product pages for auctions on eBay, e.g.
// Copyright (C) 2013  Marcel Joachim Kloubert
//     
// This library is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or (at
// your option) any later version.
//     
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// General Public License for more details.
//     
// You should have received a copy of the GNU General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301,
// USA.
// 


using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Media.Imaging;
using FreeImageAPI;

namespace MarcelJoachimKloubert.WpfAuctionDesigner.Classes.Drawing
{
    /// <summary>
    /// Helper class for image operations.
    /// </summary>
    public static class TMImageHelper
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Calculates the new size of an image.
        /// </summary>
        /// <param name="img">The input image.</param>
        /// <param name="maxWidth">The maximum width of the new image.</param>
        /// <returns>The new size.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="img" /> is <see langword="null" />.
        /// </exception>
        public static Size CalcNewSize(Image img,
                                       int? maxWidth)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            var newWidth = (decimal)img.Width;
            var newHeight = (decimal)img.Height;

            if (maxWidth.HasValue &&
                newWidth > maxWidth.Value)
            {
                var ratio = (decimal)maxWidth.Value / newWidth;

                newWidth = maxWidth.Value;
                newHeight = newHeight * ratio;
            }

            return new Size((int)newWidth, (int)newHeight);
        }

        /// <summary>
        /// Loads a <see cref="Bitmap" /> from a <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        /// <returns>The loaded bitmap.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="stream" /> is NOT readable.
        /// </exception>
        public static Bitmap LoadBitmap(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (!stream.CanRead)
            {
                throw new IOException("stream.CanRead");
            }

            FIBITMAP? dib = null;

            try
            {
                dib = FreeImage.LoadFromStream(stream);

                return FreeImage.GetBitmap(dib.Value);
            }
            finally
            {
                if (dib.HasValue)
                {
                    FreeImage.Unload(dib.Value);
                }
            }
        }

        /// <summary>
        /// Resizes an <see cref="Image" />.
        /// </summary>
        /// <param name="img">The input image.</param>
        /// <param name="maxWidth">The maximum width of the new bitmap.</param>
        /// <returns>The resized bitmap.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="img" /> is <see langword="null" />.
        /// </exception>
        public static Bitmap ResizeImage(Image img,
                                         int? maxWidth)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            Bitmap result = null;

            try
            {
                var newSize = CalcNewSize(img, maxWidth);
                result = new Bitmap(newSize.Width, newSize.Height);

                using (var gr = Graphics.FromImage(result))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    gr.DrawImage(img,
                                 new Rectangle(0, 0, result.Width, result.Height));
                }
            }
            catch
            {
                if (result != null)
                {
                    result.Dispose();
                }

                throw;
            }

            return result;
        }

        /// <summary>
        /// Converts a <see cref="BitmapSource" /> to a <see cref="Bitmap" />.
        /// </summary>
        /// <param name="bmp">The input object.</param>
        /// <returns>The output object.</returns>
        public static Bitmap ToBitmap(BitmapSource bmp)
        {
            if (bmp == null)
            {
                return null;
            }

            using (var temp = new MemoryStream())
            {
                var enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bmp));
                enc.Save(temp);

                temp.Position = 0;
                return LoadBitmap(temp);
            }
        }

        #endregion Methods
    }
}
