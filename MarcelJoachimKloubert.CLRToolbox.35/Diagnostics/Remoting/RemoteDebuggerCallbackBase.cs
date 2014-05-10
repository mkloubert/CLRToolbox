// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Remoting
{
    /// <summary>
    /// A basic remote debugger callback.
    /// </summary>
    public abstract class RemoteDebuggerCallbackBase : IRemoteDebuggerCallback
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteDebuggerCallbackBase"/> class.
        /// </summary>
        protected RemoteDebuggerCallbackBase()
        {
        }

        #endregion Constructors

        #region Methods (6)

        // Public Methods (6) 

        /// <inheriteddoc />
        public abstract void Debug(string value);

        /// <inheriteddoc />
        public abstract void ErrorOutput(string value);

        /// <inheriteddoc />
        public abstract void Log(RemoteLogMessage msg);

        /// <inheriteddoc />
        public abstract string SendMessage(int id, string args);

        /// <inheriteddoc />
        public abstract void StandardOutput(string value);

        /// <inheriteddoc />
        public abstract void Trace(string value);

        #endregion Methods
    }
}