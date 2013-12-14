// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System
{
    #region DELEGATE: Func<TResult>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/de-de/library/bb534960%28v=vs.110%29.aspx" />
    public delegate TResult Func<TResult>();

    #endregion

    #region DELEGATE: Func<T, TResult>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/bb549151%28v=vs.110%29.aspx" />
    public delegate TResult Func<T, TResult>(T arg);

    #endregion

    #region DELEGATE: Func<T1, T2, TResult>

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/bb534647%28v=vs.110%29.aspx" />
    public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

    #endregion
}
