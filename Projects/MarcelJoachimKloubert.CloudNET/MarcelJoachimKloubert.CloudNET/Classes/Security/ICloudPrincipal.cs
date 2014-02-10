// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CLRToolbox.Security.AccessControl;

namespace MarcelJoachimKloubert.CloudNET.Classes.Security
{
    /// <summary>
    /// Describes a principal that is managed by that application.
    /// </summary>
    public interface ICloudPrincipal : IAclPrincipal
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the file manager of that principal.
        /// </summary>
        IFileManager Files { get; }

        /// <inheriteddoc />
        new ICloudIdentity Identity { get; }

        #endregion Data Members
    }
}
