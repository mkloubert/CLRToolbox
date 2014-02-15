// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;

namespace MarcelJoachimKloubert.MetalVZ.Classes.Sessions
{
    /// <summary>
    /// Describes a session.
    /// </summary>
    public interface IMVZSession : IMVZObject, IIdentifiable
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the time the session started.
        /// </summary>
        DateTimeOffset StartTime { get; }

        #endregion Data Members
    }
}
