// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Text;
using System.Xml;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class StringHelper
    {
        #region Methods (1)

        // Private Methods (1) 

        static partial void AsStringExtension(object obj, ref bool handled, ref StringBuilder result)
        {
            if (obj is XmlReader)
            {
                XmlReader xmlReader = (XmlReader)obj;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                result.Append(AsString(xmlDoc, true));
                handled = true;
            }
            else if (obj is XmlNode)
            {
                // old skool XML

                result.Append(((XmlNode)obj).OuterXml);
                handled = true;
            }
        }

        #endregion Methods
    }
}
