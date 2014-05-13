// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Timing
{
    /// <summary>
    /// Describes an object that provides the current time.
    /// </summary>
    public interface ITimeProvider : ITMObject
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the current time.
        /// </summary>
        DateTimeOffset Now { get; }

        #endregion Data Members
    }
}