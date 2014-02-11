// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Read only collection of <see cref="CloudFile" /> objects.
    /// </summary>
    public sealed class CloudFileCollection : ReadOnlyCollection<CloudFile>
    {
        #region Constructors (1)

        /// <inheriteddoc />
        public CloudFileCollection(IList<CloudFile> files)
            : base(files)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Tries to return an item by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The item or <see langword="null" /> if not found.</returns>
        public CloudFile this[IEnumerable<char> name]
        {
            get
            {
                string fileName = (StringHelper.AsString(name) ?? string.Empty).ToLower().Trim();

                return CollectionHelper.SingleOrDefault(this,
                                                        delegate(CloudFile item)
                                                        {
                                                            return item != null &&
                                                                   fileName == (item.Name ?? string.Empty).ToLower().Trim();
                                                        });
            }
        }

        #endregion Properties
    }
}
