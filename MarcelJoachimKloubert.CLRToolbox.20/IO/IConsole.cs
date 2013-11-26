// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Describes a console.
    /// </summary>
    public interface IConsole : ITMObject
    {
        #region Data Members (2)

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        ConsoleColor? ForegroundColor { get; set; }

        #endregion Data Members
    }
}
