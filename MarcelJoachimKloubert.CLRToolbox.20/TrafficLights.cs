// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// List of traffic light colors.
    /// </summary>
    [Flags]
    public enum TrafficLights
    {
        /// <summary>
        /// No light
        /// </summary>
        None = 0,

        /// <summary>
        /// Red
        /// </summary>
        Red = 1,

        /// <summary>
        /// Yellow
        /// </summary>
        Yellow = 2,

        /// <summary>
        /// Green
        /// </summary>
        Green = 4,
    }
}