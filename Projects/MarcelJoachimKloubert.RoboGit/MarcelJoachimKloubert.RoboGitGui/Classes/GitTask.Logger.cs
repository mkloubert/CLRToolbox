// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Diagnostics;

namespace MarcelJoachimKloubert.RoboGitGui.Classes
{
    partial class GitTask
    {
        #region Nested Classes (1)

        private sealed class _GitTaskLogger : LoggerFacadeBase
        {
            #region Fields (1)

            private readonly GitTask _TASK;

            #endregion Fields

            #region Constructors (1)

            internal _GitTaskLogger(GitTask task)
                : base(isThreadSafe: false)
            {
                this._TASK = task;
            }

            #endregion Constructors

            #region Methods (1)

            // Protected Methods (1) 

            protected override void OnLog(ILogMessage msg)
            {
                this._TASK
                    .OnLogMessageReceived(msg);
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
