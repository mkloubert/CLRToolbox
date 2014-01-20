// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Runtime.Serialization;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Remoting
{
    /// <summary>
    /// Contains serializable data 
    /// </summary>
    [DataContract]
    public sealed class RemoteLogMessage : MarshalByRefObject
    {
        #region Properties (9)

        /// <summary>
        /// Gets or sets the name of the calling assembly.
        /// </summary>
        [DataMember]
        public string Assembly { get; set; }

        /// <summary>
        /// Gets or sets the categories of the message.
        /// </summary>
        [DataMember]
        public LoggerFacadeCategories Categories { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of <see cref="RemoteLogMessage.Message" />.
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the ID of the message.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets if <see cref="RemoteLogMessage.Message" /> is compressed by GZIP or not.
        /// </summary>
        [DataMember]
        public bool IsCompressed { get; set; }

        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        [DataMember]
        public byte[] Message { get; set; }

        /// <summary>
        /// Gets or sets the sending object.
        /// </summary>
        [DataMember]
        public RemoteLogMessageSender Sender { get; set; }

        /// <summary>
        /// Gets or sets the log tag.
        /// </summary>
        [DataMember]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the time of the message.
        /// </summary>
        [DataMember]
        public DateTimeOffset Time { get; set; }

        #endregion Properties
    }
}
