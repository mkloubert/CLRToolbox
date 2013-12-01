// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/hh881542%28v=vs.110%29.aspx" />
    public interface IReadOnlyCollection<T> : IEnumerable<T>
    {
        #region Data Members (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/hh881496%28v=vs.110%29.aspx" />
        int Count { get; }

        #endregion Data Members
    }
}
