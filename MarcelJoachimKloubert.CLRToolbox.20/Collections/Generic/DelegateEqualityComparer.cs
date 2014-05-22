// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region CLASS: DelegateEqualityComparer<T>

    /// <summary>
    /// An <see cref="EqualityComparer{T}" /> based on delegates.
    /// </summary>
    /// <typeparam name="T">Underlying type.</typeparam>
    public sealed class DelegateEqualityComparer<T> : EqualityComparer<T>
    {
        #region Fields (2)

        private readonly EqualsHandler _EQUALS;
        private readonly GetHashCodeHandler _GET_HASH_CODE;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateEqualityComparer{T}" /> class.
        /// </summary>
        /// <param name="equalsHandler">
        /// The optional logic for <see cref="DelegateEqualityComparer{T}.Equals(T, T)" />.
        /// </param>
        /// <param name="getHashCodeHandler">
        /// The optional logic for <see cref="DelegateEqualityComparer{T}.GetHashCode(T)" />.
        /// </param>
        /// <remarks>
        /// If <paramref name="equalsHandler" /> and/or <paramref name="getHashCodeHandler" /> are <see langword="null" />, default
        /// logic from <see cref="EqualityComparer{T}.Default" /> will be used.
        /// </remarks>
        public DelegateEqualityComparer(EqualsHandler equalsHandler,
                                        GetHashCodeHandler getHashCodeHandler)
        {
            this._EQUALS = equalsHandler ?? EqualityComparer<T>.Default.Equals;
            this._GET_HASH_CODE = getHashCodeHandler ?? EqualityComparer<T>.Default.GetHashCode;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateEqualityComparer{T}" /> class.
        /// </summary>
        /// <param name="equalsHandler">
        /// The optional logic for <see cref="DelegateEqualityComparer{T}.Equals(T, T)" />.
        /// </param>
        /// <remarks>
        /// If <paramref name="equalsHandler" /> is <see langword="null" />, default
        /// logic from <see cref="EqualityComparer{T}.Default" /> will be used.
        /// Default logic for <see cref="DelegateEqualityComparer{T}.GetHashCode(T)" /> will be used here.
        /// </remarks>
        public DelegateEqualityComparer(EqualsHandler equalsHandler)
            : this(equalsHandler, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateEqualityComparer{T}" /> class.
        /// </summary>
        /// <param name="getHashCodeHandler">
        /// The optional logic for <see cref="DelegateEqualityComparer{T}.GetHashCode(T)" />.
        /// </param>
        /// <remarks>
        /// If <paramref name="getHashCodeHandler" /> is <see langword="null" />, default
        /// logic from <see cref="EqualityComparer{T}.Default" /> will be used.
        /// Default logic for <see cref="DelegateEqualityComparer{T}.Equals(T, T)" /> will be used here.
        /// </remarks>
        public DelegateEqualityComparer(GetHashCodeHandler getHashCodeHandler)
            : this(null, getHashCodeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateEqualityComparer{T}" /> class.
        /// </summary>
        /// <remarks>
        /// Default logic for <see cref="DelegateEqualityComparer{T}.Equals(T, T)" /> and
        /// <see cref="DelegateEqualityComparer{T}.GetHashCode(T)" /> will be used here.
        /// </remarks>
        public DelegateEqualityComparer()
            : this(null, null)
        {
        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes a handler for <see cref="DelegateEqualityComparer{T}.Equals(T, T)" /> method.
        /// </summary>
        /// <param name="x">The left value.</param>
        /// <param name="y">The right value.</param>
        /// <returns>Are equal or not.</returns>
        public delegate bool EqualsHandler(T x, T y);

        /// <summary>
        /// Describes a handler for <see cref="DelegateEqualityComparer{T}.GetHashCode(T)" /> method.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>The hash code of the object.</returns>
        public delegate int GetHashCodeHandler(T obj);

        #endregion Delegates and Events

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="EqualityComparer{T}.Equals(T, T)" />
        public override bool Equals(T x, T y)
        {
            return this._EQUALS(x, y);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="EqualityComparer{T}.GetHashCode(T)" />
        public override int GetHashCode(T obj)
        {
            return this._GET_HASH_CODE(obj);
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: DelegateEqualityComparer

    /// <summary>
    /// Factory class for creating <see cref="DelegateEqualityComparer{T}" /> instances.
    /// </summary>
    public static class DelegateEqualityComparer
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DelegateEqualityComparer{T}.DelegateEqualityComparer()" />
        public static DelegateEqualityComparer<T> Create<T>(T dummyObj)
        {
            return new DelegateEqualityComparer<T>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DelegateEqualityComparer{T}.DelegateEqualityComparer(DelegateEqualityComparer{T}.EqualsHandler)" />
        public static DelegateEqualityComparer<T> Create<T>(T dummyObj,
                                                            DelegateEqualityComparer<T>.EqualsHandler equalsHandler)
        {
            return new DelegateEqualityComparer<T>(equalsHandler);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DelegateEqualityComparer{T}.DelegateEqualityComparer(DelegateEqualityComparer{T}.GetHashCodeHandler)" />
        public static DelegateEqualityComparer<T> Create<T>(T dummyObj,
                                                            DelegateEqualityComparer<T>.GetHashCodeHandler getHashCodeHandler)
        {
            return new DelegateEqualityComparer<T>(getHashCodeHandler);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="DelegateEqualityComparer{T}.DelegateEqualityComparer(DelegateEqualityComparer{T}.EqualsHandler, DelegateEqualityComparer{T}.GetHashCodeHandler)" />
        public static DelegateEqualityComparer<T> Create<T>(T dummyObj,
                                                            DelegateEqualityComparer<T>.EqualsHandler equalsHandler,
                                                            DelegateEqualityComparer<T>.GetHashCodeHandler getHashCodeHandler)
        {
            return new DelegateEqualityComparer<T>(equalsHandler, getHashCodeHandler);
        }

        #endregion Methods
    }

    #endregion
}