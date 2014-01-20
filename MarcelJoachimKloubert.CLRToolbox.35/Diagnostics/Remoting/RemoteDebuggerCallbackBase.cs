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

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRemoteDebuggerCallback.Debug(string)" />
        public abstract void Debug(string value);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRemoteDebuggerCallback.ErrorOutput(string)" />
        public abstract void ErrorOutput(string value);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRemoteDebuggerCallback.Log(RemoteLogMessage)" />
        public abstract void Log(RemoteLogMessage msg);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRemoteDebuggerCallback.SendMessage(int, string)" />
        public abstract string SendMessage(int id, string args);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRemoteDebuggerCallback.StandardOutput(string)" />
        public abstract void StandardOutput(string value);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRemoteDebuggerCallback.Trace(string)" />
        public abstract void Trace(string value);

        #endregion Methods
    }
}
