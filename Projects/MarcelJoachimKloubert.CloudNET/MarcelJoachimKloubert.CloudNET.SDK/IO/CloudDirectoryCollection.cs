// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Read only collection of <see cref="CloudFile" /> objects.
    /// </summary>
    public sealed class CloudDirectoryCollection : ReadOnlyCollection<CloudDirectory>
    {
        #region Constructors (1)

        /// <inheriteddoc />
        public CloudDirectoryCollection(IList<CloudDirectory> dirs)
            : base(dirs)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Tries to return an item by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The item or <see langword="null" /> if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is <see langword="null" />.
        /// </exception>
        public CloudDirectory this[IEnumerable<char> name]
        {
            get
            {
                string dirName = (StringHelper.AsString(name) ?? string.Empty).ToLower().Trim();

                return CollectionHelper.SingleOrDefault(this,
                                                        delegate(CloudDirectory item)
                                                        {
                                                            return item != null &&
                                                                   dirName == (item.Name ?? string.Empty).ToLower().Trim();
                                                        });
            }
        }

        #endregion Properties
    }
}
