// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region INTERFACE: IForAllItemExecutionContext<T>

    /// <summary>
    /// Describes a context for an item in a foreach environment.
    /// </summary>
    /// <typeparam name="T">Type of the underlying item.</typeparam>
    public interface IForAllItemExecutionContext<T>
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying item object.
        /// </summary>
        T Item { get; }

        #endregion Data Members
    }

    #endregion

    #region INTERFACE: IForAllItemExecutionContext<T, S>

    /// <summary>
    /// Describes a context for an item in a foreach environment with a state object.
    /// </summary>
    /// <typeparam name="T">Type of the underlying item.</typeparam>
    /// <typeparam name="S">Type of the underlying state object.</typeparam>
    public interface IForAllItemExecutionContext<T, S> : IForAllItemExecutionContext<T>
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying state object.
        /// </summary>
        S State { get; }

        #endregion Data Members
    }

    #endregion
}
