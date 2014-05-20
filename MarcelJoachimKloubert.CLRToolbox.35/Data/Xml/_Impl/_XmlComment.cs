﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Xml.Linq;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Xml._Impl
{
    internal class _XmlComment : _XmlNode, IXmlComment
    {
        #region Constructors (1)

        internal _XmlComment(XComment xmlObject)
            : base(xmlObject)
        {
        }

        #endregion Constructors

        #region Properties (1)

        internal new XComment _Object
        {
            get { return (XComment)base._Object; }
        }

        #endregion Properties
    }
}