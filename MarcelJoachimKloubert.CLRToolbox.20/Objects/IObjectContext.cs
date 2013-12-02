// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    #region INTERFACE: IObjectContext

    /// <summary>
    /// Describes a context for an object.
    /// </summary>
    public interface IObjectContext : IServiceLocator
    {
        #region Data Members (3)

        /// <summary>
        /// Gets the underlying assembly.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the full path of the underlying assembly file.
        /// </summary>
        string AssemblyFile { get; }

        /// <summary>
        /// Gets the underlying object.
        /// </summary>
        object Object { get; }

        #endregion Data Members

        #region Operations (3)

        /// <summary>
        /// Calculates the hash of that context.
        /// </summary>
        /// <returns>The calculated hash.</returns>
        byte[] CalculateHash();

        /// <summary>
        /// Returns the value of <see cref="IObjectContext.CalculateHash()" /> as lower case hex string.
        /// </summary>
        /// <returns>The value of <see cref="IObjectContext.CalculateHash()" /> as lower case hex string.</returns>
        string GetHashAsHexString();

        /// <summary>
        /// Tries to return the stream of a resource inside <see cref="IObjectContext.Assembly" />.
        /// </summary>
        /// <param name="resourceName">the name of the resource.</param>
        /// <returns>The stream or <see langword="null" /> if not found.</returns>
        Stream TryGetResourceStream(IEnumerable<char> resourceName);

        #endregion Operations
    }

    #endregion

    #region INTERFACE: IObjectContext<TObj>

    /// <summary>
    /// 
    /// </summary>
    /// <see cref="IObjectContext" />
    public interface IObjectContext<TObj> : IObjectContext
    {
        #region Data Members (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.Object" />
        new TObj Object { get; }

        #endregion Data Members
    }

    #endregion
}
