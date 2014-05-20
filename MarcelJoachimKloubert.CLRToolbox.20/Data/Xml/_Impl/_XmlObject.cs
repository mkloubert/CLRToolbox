// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal abstract class _XmlObject : TMObject, IXmlObject
    {
        #region Fields (1)

        private readonly object _OBJECT;

        #endregion Fields

        #region Constructors (1)

        protected internal _XmlObject(object xmlObject)
        {
            this._OBJECT = xmlObject;
        }

        #endregion Constructors

        #region Properties (1)

        internal object _Object
        {
            get { return this._OBJECT; }
        }

        #endregion Properties

        #region Methods (2)

        internal static IEnumerable<IXmlAttribute> GetAttributesFromList(XmlAttributeCollection attribs)
        {
            if (attribs == null)
            {
                yield break;
            }

            foreach (XmlAttribute a in attribs)
            {
                if (a == null)
                {
                    continue;
                }

                yield return new _XmlAttribute(a);
            }
        }

        internal static IEnumerable<IXmlNode> GetNodesFromList(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                yield break;
            }

            foreach (XmlNode node in nodes)
            {
                if (node == null)
                {
                    continue;
                }

                if (node is XmlElement)
                {
                    XmlElement element = (XmlElement)node;

                    yield return new _XmlElement(element);
                }
                else if (node is XmlComment)
                {
                    XmlComment comment = (XmlComment)node;

                    yield return new _XmlComment(comment);
                }
                else if (node is XmlCDataSection)
                {
                    XmlCDataSection data = (XmlCDataSection)node;

                    yield return new _XmlCDData(data);
                }
                else
                {
                    yield return new _XmlNode(node);
                }
            }
        }

        #endregion Methods
    }
}