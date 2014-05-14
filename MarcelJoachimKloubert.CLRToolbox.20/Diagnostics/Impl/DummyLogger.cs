// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that does nothing.
    /// </summary>
    public sealed class DummyLogger : LoggerFacadeBase
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyLogger"/> class.
        /// </summary>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DummyLogger(bool isThreadSafe, object syncRoot)
            : base(isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyLogger"/> class.
        /// </summary>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        public DummyLogger(bool isThreadSafe)
            : base(isThreadSafe)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyLogger"/> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DummyLogger(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyLogger"/> class.
        /// </summary>
        public DummyLogger()
            : base()
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            // do nothing
        }

        #endregion Methods
    }
}