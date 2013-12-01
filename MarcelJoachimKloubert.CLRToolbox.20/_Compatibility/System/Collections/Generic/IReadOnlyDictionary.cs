// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/hh136548%28v=vs.110%29.aspx" />
    public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        #region Data Members (3)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh192240%28v=vs.110%29.aspx" />
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh136396%28v=vs.110%29.aspx" />
        TValue this[TKey key] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh192237%28v=vs.110%29.aspx" />
        IEnumerable<TValue> Values { get; }

        #endregion Data Members

        #region Operations (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh136572%28v=vs.110%29.aspx" />
        bool ContainsKey(TKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh192400%28v=vs.110%29.aspx" />
        bool TryGetValue(TKey key, out TValue value);

        #endregion Operations
    }
}
