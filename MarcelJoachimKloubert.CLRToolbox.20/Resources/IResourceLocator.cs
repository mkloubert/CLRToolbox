// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Resources
{
    /// <summary>
    /// Describes an object that locates resources.
    /// </summary>
    public interface IResourceLocator : ITMObject
    {
        #region Operations (1)

        /// <summary>
        /// Tries to return the stream of a resource.
        /// </summary>
        /// <param name="resourceName">the name of the resource.</param>
        /// <returns>The stream or <see langword="null" /> if not found.</returns>
        /// <remarks>
        /// <paramref name="resourceName" /> is handled case insensitive.
        /// </remarks>
        Stream TryGetResourceStream(IEnumerable<char> resourceName);

        #endregion Operations
    }
}
