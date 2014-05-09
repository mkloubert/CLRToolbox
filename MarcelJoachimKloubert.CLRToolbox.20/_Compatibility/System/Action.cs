// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace System
{
#if NET2 || NET20 || MONO2 || MONO20

    /// <summary>
    ///
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.action%28v=vs.110%29.aspx" />
    public delegate void Action();

    /// <summary>
    ///
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/bb549311%28v=vs.110%29.aspx" />
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);

    /// <summary>
    ///
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/bb549392%28v=vs.110%29.aspx" />
    public delegate void Action<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    /// <summary>
    ///
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/bb548654%28v=vs.110%29.aspx" />
    public delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

#endif
#if NET2 || NET20 || MONO2 || MONO20 || NET35 || WINDOWS_PHONE

    /// <summary>
    ///
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/dd289012%28v=vs.110%29.aspx" />
    public delegate void Action<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

#endif
}