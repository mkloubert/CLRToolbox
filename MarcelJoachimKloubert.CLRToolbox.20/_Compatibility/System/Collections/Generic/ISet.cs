// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace System.Collections.Generic
{
    /// <summary>
    ///
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd412081%28v=vs.110%29.aspx" />
    public interface ISet<T> : ICollection<T>
    {
        #region Operations (2)

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd412075%28v=vs.110%29.aspx" />
        new bool Add(T item);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd382043%28v=vs.110%29.aspx" />
        void ExceptWith(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd394889%28v=vs.110%29.aspx" />
        void IntersectWith(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd321100%28v=vs.110%29.aspx" />
        bool IsProperSubsetOf(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd411711%28v=vs.110%29.aspx" />
        bool IsProperSupersetOf(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd412074%28v=vs.110%29.aspx" />
        bool IsSubsetOf(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd382354%28v=vs.110%29.aspx" />
        bool IsSupersetOf(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd412095%28v=vs.110%29.aspx" />
        bool Overlaps(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd412096%28v=vs.110%29.aspx" />
        bool SetEquals(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd411718%28v=vs.110%29.aspx" />
        void SymmetricExceptWith(IEnumerable<T> other);

        /// <summary>
        ///
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd411713%28v=vs.110%29.aspx" />
        void UnionWith(IEnumerable<T> other);

        #endregion Operations
    }
}