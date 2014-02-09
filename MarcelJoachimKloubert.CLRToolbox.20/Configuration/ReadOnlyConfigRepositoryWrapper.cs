// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration
{
    /// <summary>
    /// A read-only wrapper for an <see cref="IConfigRepository" />.
    /// </summary>
    public sealed class ReadOnlyConfigRepositoryWrapper : ConfigRepositoryBase
    {
        #region Fields (1)

        private readonly IConfigRepository _REPO;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyConfigRepositoryWrapper"/> class.
        /// </summary>
        /// <param name="repo">The base config repository.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="repo" /> is <see langword="null" />.
        /// </exception>
        public ReadOnlyConfigRepositoryWrapper(IConfigRepository repo)
        {
            if (repo == null)
            {
                throw new ArgumentNullException("repo");
            }

            this._REPO = repo;
        }

        #endregion Constructors

        #region Properties (2)

        /// <inheriteddoc />
        public override bool CanRead
        {
            get { return this._REPO.CanRead; }
        }

        /// <inheriteddoc />
        public override bool CanWrite
        {
            get { return false; }
        }

        #endregion Properties

        #region Methods (5)

        // Protected Methods (5) 

        /// <inheriteddoc />
        protected override void OnClear(ref bool wasCleared)
        {
            throw new InvalidOperationException();
        }

        /// <inheriteddoc />
        protected override void OnDeleteValue(string category, string name, ref bool deleted)
        {
            throw new InvalidOperationException();
        }

        /// <inheriteddoc />
        protected override void OnGetCategoryNames(ICollection<IEnumerable<char>> names)
        {
            IList<string> categories = this._REPO.GetCategoryNames();
            if (categories != null)
            {
                CollectionHelper.AddRange(names,
                                          CollectionHelper.Cast<IEnumerable<char>>(categories));
            }
        }

        /// <inheriteddoc />
        protected override void OnSetValue<T>(string category, string name, T value, ref bool valueWasSet)
        {
            throw new InvalidOperationException();
        }

        /// <inheriteddoc />
        protected override void OnTryGetValue<T>(string category, string name, ref T foundValue, ref bool valueWasFound)
        {
            valueWasFound = this._REPO
                                .TryGetValue<T>(name, out foundValue, category);
        }

        #endregion Methods
    }
}
