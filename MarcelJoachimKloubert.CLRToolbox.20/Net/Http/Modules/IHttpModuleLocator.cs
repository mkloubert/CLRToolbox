// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    /// <summary>
    /// Describes an object that locates <see cref="IHttpModule" />s.
    /// </summary>
    public interface IHttpModuleLocator : ITMObject
    {
        #region Operations (1)

        /// <summary>
        /// Returns a list of all HTTP modules.
        /// </summary>
        /// <returns>The list of all modules.</returns>
        IEnumerable<IHttpModule> GetAllModules();

        #endregion Operations
    }
}
