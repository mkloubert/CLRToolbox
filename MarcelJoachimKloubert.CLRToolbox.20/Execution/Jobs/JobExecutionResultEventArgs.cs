// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// Arguments for an <see cref="IJobExecutionResult" /> based event.
    /// </summary>
    public class JobExecutionResultEventArgs : EventArgs
    {
        #region Fields (1)

        private readonly IJobExecutionResult _RESULT;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="JobExecutionResultEventArgs" /> class.
        /// </summary>
        /// <param name="result">The value for the <see cref="JobExecutionResultEventArgs.Result" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="result" /> is <see langword="null" />.
        /// </exception>
        public JobExecutionResultEventArgs(IJobExecutionResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            this._RESULT = result;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying result context.
        /// </summary>
        public IJobExecutionResult Result
        {
            get { return this._RESULT; }
        }

        #endregion Properties
    }
}