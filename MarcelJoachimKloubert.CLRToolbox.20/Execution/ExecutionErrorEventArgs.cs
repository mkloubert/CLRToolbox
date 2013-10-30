﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Execution
{
    /// <summary>
    /// Stores the event arguments for an event that reports an execution error
    /// with a parameter.
    /// </summary>
    /// <typeparam name="TParam">Type of the parameter.</typeparam>
    public class ExecutionErrorEventArgs<TParam> : ErrorEventArgs
    {
        #region Fields (1)

        private TParam _parameter;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionErrorEventArgs{TParam}"/> class.
        /// </summary>
        /// <param name="param">
        /// The value for <see cref="ExecutionErrorEventArgs{TParam}.Parameter" /> property.
        /// </param>
        /// <param name="exception">The exception that was thrown.</param>
        public ExecutionErrorEventArgs(TParam param, Exception exception)
            : base(exception)
        {
            this._parameter = param;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying parameter.
        /// </summary>
        public TParam Parameter
        {
            get { return this._parameter; }
        }

        #endregion Properties
    }
}