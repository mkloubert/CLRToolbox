// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.ApplicationServer
{
    partial class AppServerBase
    {
        #region Enums (1)

        /// <summary>
        /// List of invokation contextes for <see cref="AppServerBase.OnStart(StartStopContext, ref bool)" />
        /// and <see cref="AppServerBase.OnStop(StartStopContext, ref bool)" /> methods.
        /// </summary>
        protected enum StartStopContext
        {
            /// <summary>
            /// Invoked from <see cref="DisposableBase.Dispose()" /> method.
            /// </summary>
            Dispose,

            /// <summary>
            /// Invoked from <see cref="AppServerBase.Restart()" /> method.
            /// </summary>
            Restart,

            /// <summary>
            /// Invoked from <see cref="AppServerBase.Start()" /> method.
            /// </summary>
            Start,

            /// <summary>
            /// Invoked from <see cref="AppServerBase.Stop()" /> method.
            /// </summary>
            Stop,
        }

        #endregion Enums
    }
}
