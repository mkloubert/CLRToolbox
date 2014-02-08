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

        // Private Methods (2) 

        private static void SaveBitmapSourceAs<TEnc>(BitmapSource src, Stream target)
            where TEnc : global::System.Windows.Media.Imaging.BitmapEncoder, new()
        {
            SaveBitmapSourceAs<TEnc, object>(src, target,
                                             null, null);
        }

        private static void SaveBitmapSourceAs<TEnc, T>(BitmapSource src, Stream target, Action<TEnc, T> encoderSetup, T setupState)
            where TEnc : global::System.Windows.Media.Imaging.BitmapEncoder, new()
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            var encoder = new TEnc();
            if (encoderSetup != null)
            {
                encoderSetup(encoder, setupState);
            }

            encoder.Frames.Add(BitmapFrame.Create(src));

            encoder.Save(target);
        }

        #endregion Methods
    }
}
