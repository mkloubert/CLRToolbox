// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.collections.istructuralequatable%28v=vs.110%29.aspx" />
    public interface IStructuralEquatable
    {
        #region Operations (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.collections.istructuralequatable.equals%28v=vs.110%29.aspx" />
        bool Equals(object other, IEqualityComparer comparer);

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.collections.istructuralequatable.gethashcode%28v=vs.110%29.aspx" />
        int GetHashCode(IEqualityComparer comparer);

        #endregion Operations
    }
}
