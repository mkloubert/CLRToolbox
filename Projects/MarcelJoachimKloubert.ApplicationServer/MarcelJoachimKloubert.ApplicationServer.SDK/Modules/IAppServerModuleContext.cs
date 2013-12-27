// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using MarcelJoachimKloubert.CLRToolbox.Timing;

namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Describes a context of an <see cref="IAppServerModule" /> object.
    /// </summary>
    public interface IAppServerModuleContext : IObjectContext<IAppServerModule>,
                                               ITimeProvider
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the configuration for that module.
        /// </summary>
        IConfigRepository Config { get; }

        /// <summary>
        /// Gets the logger for that module.
        /// </summary>
        ILoggerFacade Logger { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Returns a new list of all other modules that are part of the context.
        /// </summary>
        /// <returns>The list of other modules.</returns>
        IList<IAppServerModule> GetOtherModules();

        #endregion Operations
    }
}
