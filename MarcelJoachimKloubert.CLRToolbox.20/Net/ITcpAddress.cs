// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Net
{
    /// <summary>
    /// Describes an object that stores the data of a TCP network based address with port.
    /// </summary>
    public interface ITcpAddress
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the (host) address.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Gets the TCP port.
        /// </summary>
        int Port { get; }

        #endregion Data Members
    }
}