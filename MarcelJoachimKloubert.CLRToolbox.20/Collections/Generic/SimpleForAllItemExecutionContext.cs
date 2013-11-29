// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de



namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region INTERFACE: IForAllItemExecutionContext<T>

    /// <summary>
    /// Simple implementation of <see cref="IForAllItemExecutionContext{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleForAllItemExecutionContext{T}.Item" /> property.</typeparam>
    public class SimpleForAllItemExecutionContext<T> : IForAllItemExecutionContext<T>
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IForAllItemExecutionContext{T}.Item" />
        public T Item
        {
            get;
            set;
        }

        #endregion Properties
    }

    #endregion

    #region INTERFACE: IForAllItemExecutionContext<T, S>

    /// <summary>
    /// Simple implementation of <see cref="IForAllItemExecutionContext{T, S}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleForAllItemExecutionContext{T}.Item" /> property.</typeparam>
    /// <typeparam name="S">Type of <see cref="SimpleForAllItemExecutionContext{T, S}.State" /> property.</typeparam>
    public class SimpleForAllItemExecutionContext<T, S> : SimpleForAllItemExecutionContext<T>,
                                                          IForAllItemExecutionContext<T, S>
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IForAllItemExecutionContext{T, S}.State" />
        public S State
        {
            get;
            set;
        }

        #endregion Properties
    }

    #endregion
}
