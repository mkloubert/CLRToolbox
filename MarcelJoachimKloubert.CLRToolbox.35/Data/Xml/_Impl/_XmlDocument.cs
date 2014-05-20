// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlDocument : _XmlContainer, IXmlDocument
    {
        #region Constructors (1)

        internal _XmlDocument(XDocument xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XDocument _Object
        {
            get { return (XDocument)base._Object; }
        }

        #endregion Properties

        #region Methods (5)

        internal static _XmlDocument Load(TextReader reader)
        {
            return new _XmlDocument(XDocument.Load(reader));
        }

        internal static _XmlDocument Load(Stream stream)
        {
            using (XmlReader xmlReader = XmlReader.Create(stream))
            {
                return new _XmlDocument(XDocument.Load(xmlReader));
            }
        }

        internal static _XmlDocument Load(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Load(stream);
            }
        }

        internal static _XmlDocument Parse(StringBuilder builder)
        {
            return Parse(builder != null ? builder.ToString() : null);
        }

        internal static _XmlDocument Parse(IEnumerable<char> xml)
        {
            return new _XmlDocument(XDocument.Parse(StringHelper.AsString(xml)));
        }

        #endregion Methods
    }
}