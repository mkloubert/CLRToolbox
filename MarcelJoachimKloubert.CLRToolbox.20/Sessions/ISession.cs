// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
namespace MarcelJoachimKloubert.CLRToolbox.Sessions
{
    #region INTERFACE: ISession

    /// <summary>
    /// Describes a session.
    /// </summary>
    public interface ISession : IIdentifiable
    {
        #region Properties (1)

        /// <summary>
        /// Gets the (start) time of the session.
        /// </summary>
        DateTimeOffset Time { get; }

        #endregion Properties
    }

    #endregion INTERFACE: ISession

    #region INTERFACE: <TParent>

    /// <summary>
    /// Describes a session with a parent object.
    /// </summary>
    /// <typeparam name="TParent">Type of the parent object.</typeparam>
    public interface ISession<TParent> : ISession
    {
        #region Properties (1)

        /// <summary>
        /// Gets the underlying parent object.
        /// </summary>
        TParent Parent { get; }

        #endregion Properties
    }

    #endregion INTERFACE: <TParent>
}