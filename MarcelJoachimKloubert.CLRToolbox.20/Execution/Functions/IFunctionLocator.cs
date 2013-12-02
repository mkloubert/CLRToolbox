// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Functions
{
    /// <summary>
    /// Describes an object that locates <see cref="IFunction" />s.
    /// </summary>
    public interface IFunctionLocator : ITMObject
    {
        #region Operations (1)

        /// <summary>
        /// Returns a list of all functions.
        /// </summary>
        /// <returns>The list of all functions.</returns>
        IEnumerable<IFunction> GetAllFunctions();

        #endregion Operations
    }
}
