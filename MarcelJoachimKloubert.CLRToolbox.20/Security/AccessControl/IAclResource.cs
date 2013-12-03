// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// Describes a resource of/for an access control list.
    /// </summary>
    public interface IAclResource : ITMEquatable<IEnumerable<char>>,
                                    ITMEquatable<IAclResource>,
                                    IHasName
    {
        #region Data Members (1)

        /// <summary>
        /// Gets if that resource is allowed or not.
        /// </summary>
        bool IsAllowed { get; }

        #endregion Data Members
    }
}
