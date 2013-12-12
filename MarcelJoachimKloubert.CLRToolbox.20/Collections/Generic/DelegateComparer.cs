// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    /// <summary>
    /// Describes an <see cref="IComparer{T}" /> based on delegates.
    /// </summary>
    /// <typeparam name="T">Type of the items to compare.</typeparam>
    public sealed class DelegateComparer<T> : Comparer<T>
    {
        #region Fields (1)

        private readonly CompareHandler _HANDLER;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateComparer{T}" /> class.
        /// </summary>
        /// <param name="handler">
        /// The optional logic for <see cref="DelegateComparer{T}.Compare(T, T)" />.
        /// </param>
        /// <remarks>
        /// If <paramref name="handler" /> is <see langword="null" />, default
        /// logic from <see cref="Comparer{T}.Default" /> will be used.
        /// </remarks>
        public DelegateComparer(CompareHandler handler)
        {
            this._HANDLER = handler ?? Comparer<T>.Default.Compare;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateComparer{T}" /> class.
        /// </summary>
        public DelegateComparer()
            : this(null)
        {

        }

        #endregion Constructors

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes a handler for <see cref="DelegateComparer{T}.Compare(T, T)" /> method.
        /// </summary>
        /// <param name="x">The left value.</param>
        /// <param name="y">The right value.</param>
        /// <returns>The comparison value.</returns>
        public delegate int CompareHandler(T x, T y);

        #endregion Delegates and Events

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Comparer{T}.Compare(T, T)" />
        public override int Compare(T x, T y)
        {
            return this._HANDLER(x, y);
        }

        #endregion Methods
    }
}
