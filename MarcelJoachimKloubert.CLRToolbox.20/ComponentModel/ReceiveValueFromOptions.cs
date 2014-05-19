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
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Default options
        /// </summary>
        Default = 1,

        /// <summary>
        /// Only if values are different
        /// </summary>
        OnlyIfDifferent = 2,

        /// <summary>
        /// Even if old and new value are equal
        /// </summary>
        EvenIfEqual = 4,
    }
}