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
        /// Gets and sets a progress value between 0 and 100 percentage for the progress of the current tasks.
        /// </summary>
        double CurrentStepProgess { get; set; }

        /// <summary>
        /// Gets the list of directories to handle.
        /// </summary>
        IEnumerable<string> Directories { get; }

        /// <summary>
        /// Gets the list of files to handle.
        /// </summary>
        IEnumerable<string> Files { get; }

        /// <summary>
        /// Gets and sets a progress value between 0 and 100 percentage for the progress of all tasks.
        /// </summary>
        double OverallProgess { get; set; }

        /// <summary>
        /// Gets and sets the status text.
        /// </summary>
        string StatusText { get; set; }

        #endregion Data Members

        #region Operations (2)

        /// <summary>
        /// Calculates a value for the <see cref="IHandleFilesContext.CurrentStepProgess" /> property by using a current value
        /// and a maximum value.
        /// </summary>
        /// <param name="value">The current value.</param>
        /// <param name="maxValue">The maximum value.</param>
        void SetCurrentStepProgess(double value, double maxValue);

        /// <summary>
        /// Calculates a value for the <see cref="IHandleFilesContext.OverallProgess" /> property by using a current value
        /// and a maximum value.
        /// </summary>
        /// <param name="value">The current value.</param>
        /// <param name="maxValue">The maximum value.</param>
        void SetOverallProgess(double value, double maxValue);

        #endregion Operations
    }
}