// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Globalization;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns
{
    /// <summary>
    /// Stores context data for handling files.
    /// </summary>
    public interface IHandleFilesContext
    {
        #region Data Members (5)

        /// <summary>
        /// Gets all directories from <see cref="IHandleFilesContext.Directories" /> and its sub directories.
        /// </summary>
        IEnumerable<string> AllDirectories { get; }

        /// <summary>
        /// Gets all files from <see cref="IHandleFilesContext.Files" />
        /// and <see cref="IHandleFilesContext.Directories" /> and its sub directories.
        /// </summary>
        IEnumerable<string> AllFiles { get; }

        /// <summary>
        /// Gets the underlying culture.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the list of directories to handle.
        /// </summary>
        IEnumerable<string> Directories { get; }

        /// <summary>
        /// Gets the list of files to handle.
        /// </summary>
        IEnumerable<string> Files { get; }

        #endregion Data Members
    }
}