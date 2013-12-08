// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// Describes an access control list.
    /// </summary>
    public interface IAccessControlList : ITMObject
    {
        #region Data Members (1)

        /// <summary>
        /// Tries to return a role by its name.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>The role or <see langword="null" /> if not found.</returns>
        IAclRole this[IEnumerable<char> name] { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Returns a list of all managed roles.
        /// </summary>
        /// <returns>The list of roles.</returns>
        IEnumerable<IAclRole> GetRoles();

        #endregion Operations
    }
}
