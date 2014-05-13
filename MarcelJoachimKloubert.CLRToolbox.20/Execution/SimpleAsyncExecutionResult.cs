// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution
{
    #region INTERFACE: SimpleAsyncExecutionResult

    /// <summary>
    /// Simple implementation of the <see cref="IAsyncExecutionResult" /> interface.
    /// </summary>
    public class SimpleAsyncExecutionResult : IAsyncExecutionResult
    {
        #region Fields (2)

        private IList<Exception> _errors;
        private object _result;

        #endregion Fields

        #region Properties (3)

        /// <inheriteddoc />
        public IList<Exception> Errors
        {
            get { return this._errors; }

            set { this._errors = value; }
        }

        /// <inheriteddoc />
        public bool HasFailed
        {
            get
            {
                IList<Exception> errs = this.Errors;
                return errs != null &&
                       CollectionHelper.Any(errs,
                                            delegate(Exception ex, long index)
                                            {
                                                return ex != null;
                                            });
            }
        }

        /// <inheriteddoc />
        public object Result
        {
            get { return this._result; }

            set { this._result = value; }
        }

        #endregion Properties

        #region Methods (1)

        /// <inheriteddoc />
        public T GetResult<T>()
        {
            return GlobalConverter.Current
                                  .ChangeType<T>(this.Result);
        }

        #endregion Methods
    }

    #endregion

    #region INTERFACE: SimpleAsyncExecutionResult<TState>

    /// <summary>
    /// Simple implementation of the <see cref="IAsyncExecutionResult{TState}" /> interface.
    /// </summary>
    /// <typeparam name="TState">Type of the underlying state object.</typeparam>
    public sealed class SimpleAsyncExecutionResult<TState> : SimpleAsyncExecutionResult, IAsyncExecutionResult<TState>
    {
        #region Fields (1)

        private TState _state;

        #endregion Fields

        #region Properties (1)

        /// <inheriteddoc />
        public TState State
        {
            get { return this._state; }

            set { this._state = value; }
        }

        #endregion Data Members
    }

    #endregion
}