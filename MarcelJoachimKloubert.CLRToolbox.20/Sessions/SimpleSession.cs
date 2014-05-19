// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Sessions
{
    #region CLASS: SimpleSession

    /// <summary>
    /// Simple implementation of the <see cref="ISession" /> interface.
    /// </summary>
    public class SimpleSession : TMObject, ISession
    {
        #region Fields (2)
        
        private Guid _id;
        private DateTimeOffset _time;

        #endregion Fields
        
        #region Properties (2)

        /// <inheriteddoc />
        public Guid Id
        {
            get { return this._id; }

            set { this._id = value; }
        }
        
        /// <inheriteddoc />
        public DateTimeOffset Time
        {
            get { return this._time; }

            set { this._time = value; }
        }

        #endregion Properties

        #region Methods (4)

        /// <inheriteddoc />
        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        /// <inheriteddoc />
        public bool Equals(Guid other)
        {
            return this.Id == other;
        }

        /// <inheriteddoc />
        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
            {
                return this.Equals((IIdentifiable)other);
            }

            if (other is Guid)
            {
                return this.Equals((Guid)other);
            }

            return base.Equals(other);
        }

        /// <inheriteddoc />
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion Methods
    }

    #endregion CLASS: SimpleSession

    #region CLASS: SimpleSession<TParent>

    /// <summary>
    /// Simple implementation of the <see cref="ISession{TParent}" /> interface.
    /// </summary>
    /// <typeparam name="TParent">Type of the parent object.</typeparam>
    public sealed class SimpleSession<TParent> : SimpleSession, ISession<TParent>
    {
        #region Fields (1)

        private TParent _parent;

        #endregion Fields

        #region Properties (1)

        /// <inheriteddoc />
        public TParent Parent
        {
            get { return this._parent; }

            set { this._parent = value; }
        }

        #endregion Properties
    }

    #endregion CLASS: SimpleSession
}
