// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarcelJoachimKloubert.CryptoCopy.Helpers;

namespace MarcelJoachimKloubert.CryptoCopy.Mapping
{
    internal sealed class FileMapping
    {
        #region Fields (7)

        internal readonly CryptExecutionContext CONTEXT;
        internal readonly FileInfo DESTINATION;
        internal byte[] PASSWORD;
        internal byte[] SALT;
        internal readonly FileInfo SOURCE;
        internal readonly Task TASK;
        internal readonly XElement XML;

        #endregion Fields

        #region Constructors (3)

        private FileMapping(CryptExecutionContext ctx,
                            FileInfo src,
                            FileInfo dest,
                            XElement xml,
                            bool generatePwd)
        {
            this.CONTEXT = ctx;
            this.DESTINATION = dest;
            this.SOURCE = src;
            this.XML = xml;

            if (generatePwd)
            {
                var rng = new RNGCryptoServiceProvider();

                this.PASSWORD = new byte[48];
                rng.GetBytes(this.PASSWORD);
            }

            this.SALT = ctx.Salt;

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

        internal FileMapping(CryptExecutionContext ctx,
                             FileInfo src,
                             FileInfo dest,
                             XElement xml)
            : this(ctx: ctx,
                   src: src,
                   dest: dest,
                   xml: xml,
                   generatePwd: false)
        {

        }

        internal FileMapping(CryptExecutionContext ctx,
                             FileInfo src,
                             FileInfo dest)
            : this(ctx: ctx,
                   src: src,
                   dest: dest,
                   xml: null,
                   generatePwd: true)
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Private Methods (2) 

        private void TaskAction_Decrypt()
        {
            var pwd = Convert.FromBase64String(this.XML.Attribute("pwd").Value.Trim());

            using (var srcStream = this.SOURCE.OpenRead())
            {
                using (var destStream = new FileStream(this.DESTINATION.FullName,
                                                       FileMode.CreateNew,
                                                       FileAccess.ReadWrite))
                {
                    DecryptionHelper.DecryptStream(srcStream, destStream,
                                                   pwd, this.SALT);
                }
            }

            FileSystemHelper.TrySetCreationTimeUtc(this.DESTINATION,
                                                   new DateTimeOffset(ticks: long.Parse(this.XML.Attribute("created").Value.Trim()),
                                                                      offset: TimeSpan.Zero));

            FileSystemHelper.TrySetLastWriteTimeUtc(this.DESTINATION,
                                                    new DateTimeOffset(ticks: long.Parse(this.XML.Attribute("lastWrite").Value.Trim()),
                                                                       offset: TimeSpan.Zero));
        }

        private void TaskAction_Encrypt()
        {
            using (var srcStream = this.SOURCE.OpenRead())
            {
                using (var destStream = new FileStream(this.DESTINATION.FullName,
                                                       FileMode.CreateNew,
                                                       FileAccess.ReadWrite))
                {
                    EncryptionHelper.EncryptStream(srcStream, destStream,
                                                   this.PASSWORD, this.SALT);
                }
            }

            FileSystemHelper.TrySetTimestampsUtc(this.DESTINATION,
                                                 this.CONTEXT.StartTime);
        }

        #endregion Methods
    }
}
