﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlDocumentType : _XmlNode, IXmlDocumentType
    {
        #region Constructors (1)

        internal _XmlDocumentType(XmlDocumentType xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XmlDocumentType _Object
        {
            get { return (XmlDocumentType)base._Object; }
        }

        #endregion Properties
    }
}