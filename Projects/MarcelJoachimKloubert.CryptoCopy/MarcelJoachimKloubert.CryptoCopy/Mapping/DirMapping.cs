// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarcelJoachimKloubert.CryptoCopy.Helpers;

namespace MarcelJoachimKloubert.CryptoCopy.Mapping
{
    internal sealed class DirMapping
    {
        #region Fields (5)

        internal readonly CryptExecutionContext CONTEXT;
        internal readonly DirectoryInfo DESTINATION;
        internal readonly DirectoryInfo SOURCE;
        internal readonly Task TASK;
        internal readonly XElement XML;

        #endregion Fields

        #region Constructors (1)

        internal DirMapping(CryptExecutionContext ctx,
                            DirectoryInfo src,
                            DirectoryInfo dest,
                            XElement xml = null)
        {
            this.CONTEXT = ctx.Clone();
            this.DESTINATION = dest;
            this.SOURCE = src;
            this.XML = xml;

            switch (this.CONTEXT.Mode)
            {
                case RunningMode.Decrypt:
                    this.TASK = new Task(this.TaskAction_Decrypt);
                    break;

                case RunningMode.Encrypt:
                    this.TASK = new Task(this.TaskAction_Encrypt);
                    break;
            }
        }

        #endregion Constructors

        #region Methods (2)

        // Private Methods (2) 

        private void TaskAction_Decrypt()
        {
            DecryptionHelper.CreateDecryptionTask(this.CONTEXT,
                                                  this.SOURCE, this.DESTINATION,
                                                  this.XML)
                            .RunSynchronously();
        }

        private void TaskAction_Encrypt()
        {
            EncryptionHelper.CreateEncryptionTask(this.CONTEXT,
                                                  this.SOURCE, this.DESTINATION)
                            .RunSynchronously();

            FileSystemHelper.TrySetTimestampsUtc(this.DESTINATION,
                                                 this.CONTEXT.StartTime);
        }

        #endregion Methods
    }
}
