// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region INTERFACE: SimpleForAllItemExecutionContext<T>

    /// <summary>
    /// Simple implementation of <see cref="IForAllItemExecutionContext{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleForAllItemExecutionContext{T}.Item" /> property.</typeparam>
    public class SimpleForAllItemExecutionContext<T> : IForAllItemExecutionContext<T>
    {
        #region Fields (2)

        private long _index;
        private T _item;

        #endregion Fields (2)

        #region Properties (2)

        /// <inheriteddoc />
        public long Index
        {
            get { return this._index; }

            set { this._index = value; }
        }

        /// <inheriteddoc />
        public T Item
        {
            get { return this._item; }

            set { this._item = value; }
        }

        #endregion Properties (2)
    }

    #endregion INTERFACE: SimpleForAllItemExecutionContext<T>

    #region INTERFACE: SimpleForAllItemExecutionContext<T, S>

    /// <summary>
    /// Simple implementation of <see cref="IForAllItemExecutionContext{T, S}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleForAllItemExecutionContext{T}.Item" /> property.</typeparam>
    /// <typeparam name="S">Type of <see cref="SimpleForAllItemExecutionContext{T, S}.State" /> property.</typeparam>
    public class SimpleForAllItemExecutionContext<T, S> : SimpleForAllItemExecutionContext<T>, IForAllItemExecutionContext<T, S>
    {
        #region Fields (1)

        private S _state;

        #endregion Fields (1)

        #region Properties (1)

        /// <inheriteddoc />
        public S State
        {
            get { return this._state; }

            set { this._state = value; }
        }

        #endregion Properties
    }

    #endregion INTERFACE: SimpleForAllItemExecutionContext<T, S>
}