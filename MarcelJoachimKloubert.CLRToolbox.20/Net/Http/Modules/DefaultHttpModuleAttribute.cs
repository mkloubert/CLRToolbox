// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    /// <summary>
    /// Marks a HTTP as the default one.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,
                   AllowMultiple = false,
                   Inherited = false)]
    public sealed class DefaultHttpModuleAttribute : Attribute
    {

    }
}
