// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Describes a console.
    /// </summary>
    public partial interface IConsole : ITMObject
    {
        #region Operations (8)

        /// <summary>
        /// Reads the current line of console input.
        /// </summary>
        /// <returns>The read line.</returns>
        string ReadLine();

        /// <summary>
        /// Writes an object to console.
        /// </summary>
        /// <param name="obj">The object to write.</param>
        /// <returns>That instance.</returns>
        IConsole Write(object obj);

        /// <summary>
        /// Writes a char sequence to console.
        /// </summary>
        /// <param name="chars">The chars to write.</param>
        /// <returns>That instance.</returns>
        IConsole Write(IEnumerable<char> chars);

        /// <summary>
        /// Writes a formated string to console.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments to write.</param>
        /// <returns>That instance.</returns>
        IConsole Write(IEnumerable<char> format, params object[] args);

        /// <summary>
        /// Writes a new line.
        /// </summary>
        /// <returns>That instance.</returns>
        IConsole WriteLine();

        /// <summary>
        /// Writes an object to console and adds a new line.
        /// </summary>
        /// <param name="obj">The object to write.</param>
        /// <returns>That instance.</returns>
        IConsole WriteLine(object obj);

        /// <summary>
        /// Writes a char sequence to console and adds a new line.
        /// </summary>
        /// <param name="chars">The chars to write.</param>
        /// <returns>That instance.</returns>
        IConsole WriteLine(IEnumerable<char> chars);

        /// <summary>
        /// Writes a formated string to console and adds a new line.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments to write.</param>
        /// <returns>That instance.</returns>
        IConsole WriteLine(IEnumerable<char> format, params object[] args);

        #endregion Operations
    }
}
