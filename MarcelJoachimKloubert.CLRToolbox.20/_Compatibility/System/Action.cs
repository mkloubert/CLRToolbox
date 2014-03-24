// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System
{
    #region DELEGATE: Action<T1, T2>

#if NET2 || NET20
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.action%28v=vs.110%29.aspx" />
    public delegate void Action();

#endif
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/bb549311%28v=vs.110%29.aspx" />
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);

    #endregion
}
