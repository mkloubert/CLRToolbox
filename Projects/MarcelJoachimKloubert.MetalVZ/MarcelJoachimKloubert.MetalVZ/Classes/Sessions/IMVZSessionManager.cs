// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.MetalVZ.Classes.Sessions
{
    /// <summary>
    /// Describes a session manager.
    /// </summary>
    public interface IMVZSessionManager : IMVZObject
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the current session.
        /// </summary>
        IMVZSession Current { get; }

        #endregion Data Members

        #region Operations (3)

        /// <summary>
        /// Gets all registrated sessions.
        /// </summary>
        /// <returns>The list of sessions.</returns>
        IEnumerable<IMVZSession> GetSessions();

        /// <summary>
        /// Registers a session.
        /// </summary>
        /// <param name="session">The session to register.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="session" /> is <see langword="null" />.
        /// </exception>
        void Register(IMVZSession session);

        /// <summary>
        /// Unregisters a session.
        /// </summary>
        /// <param name="session">The session to unregister.</param>
        /// <returns>
        /// <paramref name="session" /> was unregistrated or not.
        /// <see langword="null" /> indicates that <paramref name="session" /> is
        /// a <see langword="null" /> reference.
        /// </returns>
        bool? Unregister(IMVZSession session);

        #endregion Operations
    }
}
