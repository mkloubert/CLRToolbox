﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlAttribute : _XmlObject, IXmlAttribute
    {
        #region Constructors (1)

        internal _XmlAttribute(XAttribute xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XAttribute _Object
        {
            get { return (XAttribute)base._Object; }
        }

        #endregion Properties
    }
}