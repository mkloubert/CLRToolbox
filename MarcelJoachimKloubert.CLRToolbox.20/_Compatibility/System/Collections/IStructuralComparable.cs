// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.collections.istructuralcomparable%28v=vs.110%29.aspx" />
    public interface IStructuralComparable
    {
        #region Operations (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.collections.istructuralcomparable.compareto%28v=vs.110%29.aspx" />
        int CompareTo(object other, IComparer comparer);

        #endregion Operations
    }
}
