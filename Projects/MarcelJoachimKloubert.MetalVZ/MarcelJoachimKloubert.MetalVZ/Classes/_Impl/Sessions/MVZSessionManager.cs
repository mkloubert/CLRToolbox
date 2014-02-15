// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.MetalVZ.Classes.Sessions;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.MetalVZ.Classes._Impl.Sessions
{
    internal sealed class MVZSessionManager : MVZObject, IMVZSessionManager
    {
        #region Fields (1)

        private readonly IList<IMVZSession> _SESSIONS = new List<IMVZSession>();

        #endregion Fields

        #region Properties (2)

        public IMVZSession Current
        {
            get { return this.CurrentSessionProvider(); }
        }

        internal Func<IMVZSession> CurrentSessionProvider
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (3) 

        public IEnumerable<IMVZSession> GetSessions()
        {
            return this._SESSIONS;
        }

        public void Register(IMVZSession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            lock (this._SYNC)
            {
                var index = this._SESSIONS.IndexOf(session);
                if (index > -1)
                {
                    throw new InvalidOperationException();
                }

                this._SESSIONS.Add(session);
            }
        }

        public bool? Unregister(IMVZSession session)
        {
            if (session == null)
            {
                return null;
            }

            lock (this._SYNC)
            {
                return this._SESSIONS
                           .Remove(session);
            }
        }

        #endregion Methods
    }
}
