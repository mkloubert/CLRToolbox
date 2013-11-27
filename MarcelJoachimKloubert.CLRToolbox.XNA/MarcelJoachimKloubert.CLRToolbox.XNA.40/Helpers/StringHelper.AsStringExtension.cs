// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Text;
using System.Xml;
using System.Xml.Linq;

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
                var xmlReader = (XmlReader)obj;

                var xmlDoc = XDocument.Load(xmlReader);

                result.Append(xmlDoc.ToString());
                handled = true;
            }
        }

        #endregion Methods
    }
}
