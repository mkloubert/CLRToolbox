// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel
{
    /// <summary>
    /// A simple and common implementation of an <see cref="IReadOnlyList{T}" /> based object.
    /// </summary>
    /// <typeparam name="T">Type of the items.</typeparam>
    public class TMReadOnlyList<T> : IReadOnlyList<T>
    {
        #region Fields (1)

        private readonly IList<T> _LIST;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMReadOnlyList{T}"/> class.
        /// </summary>
        /// <param name="list">The value for the <see cref="TMReadOnlyList{T}._LIST" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> is <see langword="null" />.
        /// </exception>
        public TMReadOnlyList(IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            this._LIST = list;
        }

        #endregion Constructors

        #region Properties (4)

        /// <inheriteddoc />
        public T this[int index]
        {
            get { return this._LIST[index]; }
        }

        /// <inheriteddoc />
        public int Count
        {
            get { return this._LIST.Count; }
        }

        /// <inheriteddoc />
        public IEnumerator<T> GetEnumerator()
        {
            return this._LIST.GetEnumerator();
        }

        /// <inheriteddoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Properties
    }
}