// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.ServiceModel;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Remoting
{
    /// <summary>
    /// Describes a contract that represents a connection from a server/host back to a client.
    /// </summary>
    [ServiceContract(Namespace = "http://wcf.marcel-kloubert.de")]
    public interface IRemoteDebuggerCallback
    {
        #region Operations (6)

        /// <summary>
        /// Receives content for the debug (<see cref="Debug" />) output.
        /// </summary>
        /// <param name="value">The received value / content.</param>
        [OperationContract(IsOneWay = true)]
        void Debug(string value);

        /// <summary>
        /// Receives content for the error output.
        /// </summary>
        /// <param name="value">The received value / content.</param>
        [OperationContract(IsOneWay = true)]
        void ErrorOutput(string value);

        /// <summary>
        /// Receives a log message.
        /// </summary>
        /// <param name="msg">The received message.</param>
        [OperationContract(IsOneWay = true)]
        void Log(RemoteLogMessage msg);

        /// <summary>
        /// Sends a generic message to the client.
        /// </summary>
        /// <param name="id">The ID of the message.</param>
        /// <param name="args">The arguments for the message.</param>
        /// <returns>The result of the message.</returns>
        [OperationContract(IsOneWay = false)]
        string SendMessage(int id, string args);

        /// <summary>
        /// Receives content for the standard output.
        /// </summary>
        /// <param name="value">The received value / content.</param>
        [OperationContract(IsOneWay = true)]
        void StandardOutput(string value);

        /// <summary>
        /// Receives content for the trace (<see cref="Trace" />) output.
        /// </summary>
        /// <param name="value">The received value / content.</param>
        [OperationContract(IsOneWay = true)]
        void Trace(string value);

        #endregion Operations
    }
}