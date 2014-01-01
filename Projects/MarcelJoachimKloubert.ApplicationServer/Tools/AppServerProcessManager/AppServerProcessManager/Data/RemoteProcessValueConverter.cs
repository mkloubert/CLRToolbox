// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using AppServerProcessManager.JSON.ListProcesses;
using MarcelJoachimKloubert.CLRToolbox.Windows.Data;

namespace AppServerProcessManager.Data
{
    /// <summary>
    /// Converts <see cref="RemoteProcess" /> objects to another value.
    /// </summary>
    public sealed class RemoteProcessValueConverter : ValueConverterBase<RemoteProcess, object>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase{TInput, TOutput}.Convert(TInput, object, CultureInfo)" />
        protected override object Convert(RemoteProcess value, string parameter, CultureInfo culture)
        {
            object result = null;

            if (value != null)
            {
                switch (parameter)
                {
                    case "icon":
                        {
                            var loadDefault = true;

                            var icon = value.Icon;
                            if (icon != null)
                            {
                                var data = icon.Data;
                                if (data != null)
                                {
                                    try
                                    {
                                        using (var temp = new MemoryStream(data, false))
                                        {
                                            data = null;

                                            var bmp = new BitmapImage();
                                            bmp.BeginInit();
                                            bmp.CacheOption = BitmapCacheOption.OnLoad;
                                            bmp.StreamSource = temp;
                                            bmp.EndInit();

                                            result = bmp;
                                            loadDefault = false;
                                        }
                                    }
                                    catch
                                    {
                                        // ignore here
                                    }
                                }
                            }

                            if (loadDefault)
                            {
                                var bmp = new BitmapImage();
                                bmp.BeginInit();
                                bmp.UriSource = new Uri("pack://application:,,,/Resources/process_128x128.png");
                                bmp.EndInit();

                                result = bmp;
                            }
                        }
                        break;
                }
            }

            return result;
        }

        #endregion Methods
    }
}
