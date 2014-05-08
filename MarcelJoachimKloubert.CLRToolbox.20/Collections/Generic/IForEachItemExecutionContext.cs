// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region INTERFACE: IForEachItemExecutionContext<T>

    /// <summary>
    /// Describes a context for an item in a foreach environment.
    /// </summary>
    /// <typeparam name="T">Type of the underlying item.</typeparam>
    public interface IForEachItemExecutionContext<T> : IForAllItemExecutionContext<T>
    {
        #region Data Members (1)

        /// <summary>
        /// Gets or sets if the whole operation should be canceled or not.
        /// </summary>
        bool Cancel { get; set; }
        
        #endregion Data Members
    }

    #endregion

    #region INTERFACE: IForEachItemExecutionContext<T, S>

    /// <summary>
    /// Describes a context for an item in a foreach environment with a state object.
    /// </summary>
    /// <typeparam name="T">Type of the underlying item.</typeparam>
    /// <typeparam name="S">Type of the underlying state object.</typeparam>
    public interface IForEachItemExecutionContext<T, S> : IForEachItemExecutionContext<T>, IForAllItemExecutionContext<T, S>
    {
        
    }

    #endregion
}
