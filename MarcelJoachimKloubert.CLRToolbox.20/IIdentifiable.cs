// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Describes an object that is identifiable by an ID.
    /// </summary>
    public interface IIdentifiable : IEquatable<IIdentifiable>,
                                     IEquatable<Guid>
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the ID of that object.
        /// </summary>
        Guid Id { get; }

        #endregion Data Members
    }
}
