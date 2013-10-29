// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    partial class AsyncLogger
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLogger"/> class.
        /// </summary>
        /// <param name="innerLogger">The inner logger.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="innerLogger" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Logging is not thread safe.
        /// </remarks>
        public AsyncLogger(ILoggerFacade innerLogger)
            : base(innerLogger,
                   false)
        {

        }

        #endregion Constructors
    }
}
