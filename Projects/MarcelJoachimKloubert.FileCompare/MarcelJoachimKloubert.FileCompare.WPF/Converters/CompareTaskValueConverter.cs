// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using MarcelJoachimKloubert.FileCompare.WPF.Classes;
using System.Globalization;

namespace MarcelJoachimKloubert.FileCompare.WPF.Converters
{
    /// <summary>
    /// Converts a <see cref="CompareTask" /> object.
    /// </summary>
    public sealed class CompareTaskValueConverter : ValueConverterBase<CompareTask, object>
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override object Convert(CompareTask task, string parameter, CultureInfo culture)
        {
            if (task != null)
            {
                switch (parameter)
                {
                    case "destination":
                        return task.Destination;

                    case "hash":
                        return task.Hash != null ? task.Hash.Name : "(no)";

                    case "scan_recursive":
                        return task.Recursive ? "yes" : "no";

                    case "source":
                        return task.Source;
                }
            }

            return null;
        }

        #endregion Methods
    }
}