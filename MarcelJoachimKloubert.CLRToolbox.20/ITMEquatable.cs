// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Extension of <see cref="IEquatable{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">Type of items to compare.</typeparam>
    public interface ITMEquatable<T> : ITMObject, IEquatable<T>
    {

    }
}
