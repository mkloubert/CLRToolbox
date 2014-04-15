// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.IO;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// Describes an item that stores the data of a compare result.
    /// </summary>
    public interface ICompareResult
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the destination item.
        /// </summary>
        FileSystemInfo Destination { get; }

        /// <summary>
        /// Gets the source item.
        /// </summary>
        FileSystemInfo Source { get; }

        #endregion Data Members
    }
}