// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    partial class AppServerModuleBase
    {
        #region Enums (1)

        /// <summary>
        /// List of invokation contextes for <see cref="AppServerModuleBase.OnStart(StartStopContext, ref bool)" />
        /// and <see cref="AppServerModuleBase.OnStop(StartStopContext, ref bool)" /> methods.
        /// </summary>
        protected enum StartStopContext
        {
            /// <summary>
            /// Invoked from <see cref="AppServerModuleBase.Restart()" /> method.
            /// </summary>
            Restart,

            /// <summary>
            /// Invoked from <see cref="AppServerModuleBase.Start()" /> method.
            /// </summary>
            Start,

            /// <summary>
            /// Invoked from <see cref="AppServerModuleBase.Stop()" /> method.
            /// </summary>
            Stop,
        }

        #endregion Enums
    }
}
