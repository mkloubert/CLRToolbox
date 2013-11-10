// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// Describes a logger.
    /// </summary>
    public interface ILoggerFacade : ITMObject
    {
        #region Operations (5)

        /// <summary>
        /// Loggs a message.
        /// </summary>
        /// <param name="msgObj">The message to log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="msgObj" /> is <see langword="null" />.
        /// </exception>
        void Log(ILogMessage msgObj);

        /// <summary>
        /// Logs an object.
        /// </summary>
        /// <param name="msg">The object to log.</param>
        void Log(object msg);

        /// <summary>
        /// Logs an object.
        /// </summary>
        /// <param name="msg">The object to log.</param>
        /// <param name="tag">The tag, like the name of the source.</param>
        void Log(object msg,
                 IEnumerable<char> tag);

        /// <summary>
        /// Logs an object.
        /// </summary>
        /// <param name="msg">The object to log.</param>
        /// <param name="categories">The list of categaories the message object belongs to.</param>
        void Log(object msg,
                 LoggerFacadeCategories categories);

        /// <summary>
        /// Logs an object.
        /// </summary>
        /// <param name="msg">The object to log.</param>
        /// <param name="tag">The tag, like the name of the source.</param>
        /// <param name="categories">The list of categaories the message object belongs to.</param>
        void Log(object msg,
                 IEnumerable<char> tag,
                 LoggerFacadeCategories categories);

        #endregion Operations
    }
}
