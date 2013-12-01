// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    [ServiceContract]
    internal interface IWcfHttpServerService
    {
        #region Operations (1)

        [OperationContract(Action = "*", ReplyAction = "*")]
        Message Request(Message message);

        #endregion Operations
    }
}
