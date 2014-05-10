// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Execution
{
    /// <summary>
    /// Describes the result of a log command execution.
    /// </summary>
    public interface ILogCommandExecutionResult
    {
        #region Data Members (6)

        /// <summary>
        /// Gets the underlying command.
        /// </summary>
        ILogCommand Command { get; }

        /// <summary>
        /// Gets if the value of <see cref="ILogCommandExecutionResult.MessageValueToLog" />
        /// should be send to "real" log logic or not.
        /// </summary>
        bool DoLogMessage { get; }

        /// <summary>
        /// Gets a readonly list of errors that had been thrown while command was executed.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets if the execution had been failed or not.
        /// </summary>
        bool HasFailed { get; }

        /// <summary>
        /// Gets the original message.
        /// </summary>
        ILogMessage Message { get; }

        /// <summary>
        /// Gets the message value to log.
        /// </summary>
        object MessageValueToLog { get; }

        #endregion Data Members
    }
}