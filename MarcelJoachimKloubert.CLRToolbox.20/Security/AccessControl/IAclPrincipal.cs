// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security.Principal;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// A <see cref="IPrincipal" /> based on a <see cref="IAccessControlList" />.
    /// </summary>
    public interface IAclPrincipal : ITMObject, IPrincipal
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying access control list.
        /// </summary>
        IAccessControlList AccessControlList { get; }

        #endregion Data Members
    }
}
