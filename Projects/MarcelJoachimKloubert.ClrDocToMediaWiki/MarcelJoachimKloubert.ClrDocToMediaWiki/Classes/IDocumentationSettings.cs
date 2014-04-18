// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Security;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Object that stores app / documentation settings.
    /// </summary>
    public interface IDocumentationSettings
    {
        #region Data Members (11)

        /// <summary>
        /// Gets the full path of the assembly file.
        /// </summary>
        string AssemblyFile { get; }

        /// <summary>
        /// Gets if private methods should be documented or not.
        /// </summary>
        bool DocumentPrivateMethods { get; }

        /// <summary>
        /// Gets if private types should be documented or not.
        /// </summary>
        bool DocumentPrivateTypes { get; }

        /// <summary>
        /// Gets if public types should be documented or not.
        /// </summary>
        bool DocumentPublicMethods { get; }

        /// <summary>
        /// Gets if public types should be documented or not.
        /// </summary>
        bool DocumentPublicTypes { get; }

        /// <summary>
        /// Gets if the settings are active or not.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets the namespace prefix to use for the wiki page.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets the CLR namespace filter.
        /// </summary>
        string NamespaceFilter { get; }

        /// <summary>
        /// Gets the password to use.
        /// </summary>
        SecureString Password { get; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the base URL of the wiki.
        /// </summary>
        string WikiUrl { get; }

        #endregion Data Members
    }
}