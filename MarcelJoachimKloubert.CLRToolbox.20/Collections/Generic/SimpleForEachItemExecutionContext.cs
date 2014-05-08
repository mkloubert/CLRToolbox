// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region INTERFACE: SimpleForEachItemExecutionContext<T>

    /// <summary>
    /// Simple implementation of <see cref="IForEachItemExecutionContext{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleForAllItemExecutionContext{T}.Item" /> property.</typeparam>
    public class SimpleForEachItemExecutionContext<T> : SimpleForAllItemExecutionContext<T>, IForEachItemExecutionContext<T>
    {
        #region Fields (1)

        private bool _cancel;

        #endregion Fields (1)

        #region Properties (1)

        /// <inheriteddoc />
        public bool Cancel
        {
            get { return this._cancel; }

            set { this._cancel = value; }
        }

        #endregion Properties (1)
    }

    #endregion

    #region INTERFACE: SimpleForEachItemExecutionContext<T, S>

    /// <summary>
    /// Simple implementation of <see cref="IForEachItemExecutionContext{T, S}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleForAllItemExecutionContext{T}.Item" /> property.</typeparam>
    /// <typeparam name="S">Type of <see cref="SimpleForAllItemExecutionContext{T, S}.State" /> property.</typeparam>
    public class SimpleForEachItemExecutionContext<T, S> : SimpleForAllItemExecutionContext<T, S>, IForEachItemExecutionContext<T, S>
    {
        #region Fields (1)

        private bool _cancel;

        #endregion Fields (1)

        #region Properties (1)

        /// <inheriteddoc />
        public bool Cancel
        {
            get { return this._cancel; }

            set { this._cancel = value; }
        }

        #endregion Properties (1)
    }

    #endregion
}