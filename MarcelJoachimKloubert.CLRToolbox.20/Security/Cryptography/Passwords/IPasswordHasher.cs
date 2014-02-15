// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Security.Cryptography.Passwords
{
    /// <summary>
    /// Describes a hasher for passwords.
    /// </summary>
    public interface IPasswordHasher : IHasher
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the salt (if defined).
        /// </summary>
        byte[] Salt { get; }

        #endregion Data Members
    }
}
