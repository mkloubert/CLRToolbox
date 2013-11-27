using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial interface IConsole
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

        #region Operations (1)

        /// <summary>
        /// Clears the console.
        /// </summary>
        /// <returns>That instance.</returns>
        IConsole Clear();

        #endregion Operations
    }
}
