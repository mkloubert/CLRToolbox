// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    /// <summary>
    /// Marks a method as a test.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,
                   AllowMultiple = false,
                   Inherited = true)]
    public class TestAttribute : Attribute
    {
    }
}