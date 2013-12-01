// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    internal sealed class BinaryMessage : Message
    {
        #region Fields (2)

        private MessageHeaders _HEADERS;
        private MessageProperties _PROPERTIES;

        #endregion Fields

        #region Constructors (1)

        internal BinaryMessage(IEnumerable<byte> data)
        {
            this.Data = CollectionHelper.AsArray(data);

            this._HEADERS = new MessageHeaders(MessageVersion.None);
            this._PROPERTIES = new MessageProperties();

            this.Properties
                .Add(WebBodyFormatMessageProperty.Name,
                     new WebBodyFormatMessageProperty(WebContentFormat.Raw));
        }

        #endregion Constructors

        #region Properties (4)

        public byte[] Data
        {
            get;
            private set;
        }

        public override MessageHeaders Headers
        {
            get { return this._HEADERS; }
        }

        public override MessageProperties Properties
        {
            get { return this._PROPERTIES; }
        }

        public override MessageVersion Version
        {
            get { return MessageVersion.None; }
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            var writeState = writer.WriteState;

            if (writeState == WriteState.Start)
            {
                writer.WriteStartElement("Binary");
            }

            var data = this.Data ?? new byte[0];
            writer.WriteBase64(data, 0, data.Length);

            if (writeState == WriteState.Start)
            {
                writer.WriteEndElement();
            }
        }

        #endregion Methods
    }
}
