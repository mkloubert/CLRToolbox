// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.icloneable.aspx" />
    public interface ICloneable
    {
        #region Operations (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.icloneable.clone.aspx" />
        object Clone();

        #endregion Operations
    }
}