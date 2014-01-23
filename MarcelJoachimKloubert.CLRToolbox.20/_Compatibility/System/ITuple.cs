// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections;
using System.Text;

namespace System
{
    internal interface ITuple
    {
        #region Data Members (1)

        int Size
        {
            get;
        }

        #endregion Data Members

        #region Operations (2)

        int GetHashCode(IEqualityComparer comparer);

        string ToString(StringBuilder sb);

        #endregion Operations
    }
}
