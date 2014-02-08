// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Windows
{
    static partial class ClrToolboxWpfExtensionMethods
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Takes a screenshot of a framework element.
        /// </summary>
        /// <param name="element">The element from where to take the screenshot from.</param>
        /// <returns>
        /// The token screenshot or <see langword="null" /> if <paramref name="element" /> has an empty size.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="element" /> is <see langword="null" />.
        /// </exception>
        public static BitmapSource TakeScreenshot(this FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var size = new Size(element.ActualWidth, element.ActualHeight);
            if (size.IsEmpty || size.Width == 0 || size.Height == 0)
            {
                return null;
            }

            var result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            var drawingvisual = new DrawingVisual();
            using (var context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(element), null, new Rect(new Point(), size));
                context.Close();
            }

            result.Render(drawingvisual);
            return result;
        }

        #endregion Methods
    }
}
