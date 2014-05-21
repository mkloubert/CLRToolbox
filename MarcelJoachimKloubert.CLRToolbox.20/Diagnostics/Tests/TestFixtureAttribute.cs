// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    /// <summary>
    /// Marks a class that contains tests and, optionally, setup or teardown methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,
                    AllowMultiple = true,
                    Inherited = true)]
    public class TestFixtureAttribute : Attribute
    {
    }
}