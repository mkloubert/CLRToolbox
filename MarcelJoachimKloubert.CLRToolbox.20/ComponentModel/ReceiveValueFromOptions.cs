// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// Options for <see cref="ReceiveValueFromAttribute" />.
    /// </summary>
    [Flags]
    public enum ReceiveValueFromOptions
    {
        /// <summary>
        /// Default options
        /// </summary>
        Default = 0,

        /// <summary>
        /// Receive only if value is different.
        /// </summary>
        OnlyIfDifferent = 1,
    }
}