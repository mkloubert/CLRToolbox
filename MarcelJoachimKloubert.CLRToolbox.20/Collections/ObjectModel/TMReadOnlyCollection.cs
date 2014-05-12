// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel
{
    /// <summary>
    /// A simple and common implementation of an <see cref="IReadOnlyCollection{T}" /> based object.
    /// </summary>
    /// <typeparam name="T">Type of the items.</typeparam>
    public class TMReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        #region Fields (1)

        private readonly ICollection<T> _COLLECTION;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMReadOnlyCollection{T}"/> class.
        /// </summary>
        /// <param name="coll">The value for the <see cref="TMReadOnlyCollection{T}._COLLECTION" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coll" /> is <see langword="null" />.
        /// </exception>
        public TMReadOnlyCollection(ICollection<T> coll)
        {
            if (coll == null)
            {
                throw new ArgumentNullException("coll");
            }

            this._COLLECTION = coll;
        }

        #endregion Constructors

        #region Properties (3)

        /// <inheriteddoc />
        public int Count
        {
            get { return this._COLLECTION.Count; }
        }

        /// <inheriteddoc />
        public IEnumerator<T> GetEnumerator()
        {
            return this._COLLECTION.GetEnumerator();
        }

        /// <inheriteddoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Properties
    }
}