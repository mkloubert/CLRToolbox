// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// Describes a role of/for an access control list.
    /// </summary>
    public interface IAclRole : ITMEquatable<IEnumerable<char>>,
                                ITMEquatable<IAclRole>,
                                IHasName
    {

    }
}
