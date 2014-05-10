// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.ServiceModel;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Remoting
{
    /// <summary>
    /// Describes a contract that represents a debugger connection from a client to a server/host.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IRemoteDebuggerCallback),
                     Namespace = "http://wcf.marcel-kloubert.de",
                     SessionMode = SessionMode.Required)]
    public interface IRemoteDebugger
    {
        #region Operations (3)

        /// <summary>
        /// Closes the session to the debugger.
        /// </summary>
        [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = true)]
        void Close();

        /// <summary>
        /// Opens the session to the debugger.
        /// </summary>
        /// <param name="user">The username.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="client">The ID of the client / application.</param>
        [OperationContract(IsInitiating = true, IsOneWay = true, IsTerminating = false)]
        void Open(string user, string pwd, byte[] client);

        /// <summary>
        /// Sends a generic message to the host.
        /// </summary>
        /// <param name="id">The ID of the message.</param>
        /// <param name="args">The arguments for the message.</param>
        /// <returns>The result of the message.</returns>
        [OperationContract(IsOneWay = false)]
        string SendMessage(int id, string args);

        #endregion Operations
    }
}