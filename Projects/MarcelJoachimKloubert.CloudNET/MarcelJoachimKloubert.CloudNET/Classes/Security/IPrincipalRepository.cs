// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CloudNET.Classes.Security
{
    /// <summary>
    /// Describes an object that handles principals.
    /// </summary>
    public interface IPrincipalRepository : ITMObject
    {
        #region Operations (1)

        /// <summary>
        /// Tries to find a principal by username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The found (and marked as authenticated) principal or <see langword="null" /> if not found.
        /// </returns>
        ICloudPrincipal TryFindPrincipalByLogin(IEnumerable<char> username,
                                                IEnumerable<char> password);

        #endregion Operations
    }
}
