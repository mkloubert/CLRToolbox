// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Security
{
    /// <summary>
    /// Logic for checking a username and a password.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password</param>
    /// <returns>Is valid or not.</returns>
    public delegate bool UsernamePasswordValidator(string username, string password);
}
