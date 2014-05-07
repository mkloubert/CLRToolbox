// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/hh192385%28v=vs.110%29.aspx" />
    public interface IReadOnlyList<T> : IReadOnlyCollection<T>
    {
        #region Data Members (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh192387%28v=vs.110%29.aspx" />
        T this[int index] { get; }

        #endregion Data Members 
    }
}
