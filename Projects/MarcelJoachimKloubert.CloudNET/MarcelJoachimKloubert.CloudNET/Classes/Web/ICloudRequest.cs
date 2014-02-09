// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CloudNET.Classes.Sessions;
using MarcelJoachimKloubert.CLRToolbox;
using System.Web;

namespace MarcelJoachimKloubert.CloudNET.Classes.Web
{
    /// <summary>
    /// Describes a request context.
    /// </summary>
    public interface ICloudRequest : ITMObject
    {
        #region Data Members (3)

        /// <summary>
        /// Gets the underlying HTTP context.
        /// </summary>
        HttpContext Context { get; }

        /// <summary>
        /// Gets the principal if available.
        /// </summary>
        ICloudPrincipal Principal { get; }

        /// <summary>
        /// Gets the underlying session.
        /// </summary>
        ICloudSession Session { get; }

        #endregion Data Members
    }
}
