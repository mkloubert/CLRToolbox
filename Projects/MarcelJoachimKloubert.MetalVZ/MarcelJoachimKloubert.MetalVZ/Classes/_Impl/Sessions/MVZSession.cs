// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.MetalVZ.Classes.Sessions;
using System;

namespace MarcelJoachimKloubert.MetalVZ.Classes._Impl.Sessions
{
    internal sealed class MVZSession : MVZObject, IMVZSession
    {
        #region Constructors (2)

        internal MVZSession(Guid id)
        {
            this.Id = id;
        }

        internal MVZSession()
            : this(Guid.NewGuid())
        {

        }

        #endregion Constructors

        #region Properties (4)

        public Guid Id
        {
            get;
            private set;
        }

        public DateTimeOffset StartTime
        {
            get;
            internal set;
        }

        public string SystemId
        {
            get { return this.SystemIdProvider(); }
        }

        internal Func<string> SystemIdProvider
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (4) 

        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        public bool Equals(Guid otherId)
        {
            return this.Id == otherId;
        }

        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
            {
                return this.Equals(other as IIdentifiable);
            }

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion Methods
    }
}
