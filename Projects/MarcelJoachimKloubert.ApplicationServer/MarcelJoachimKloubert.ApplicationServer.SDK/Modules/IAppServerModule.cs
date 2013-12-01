// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Describes a module for an application server.
    /// </summary>
    public interface IAppServerModule : INotificationObject, IIdentifiable, IHasName, IRunnable
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the underlying context.
        /// </summary>
        IAppServerModuleContext Context { get; }

        /// <summary>
        /// Gets if that module has already been initialized or not.
        /// </summary>
        bool IsInitialized { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Initializes that module.
        /// </summary>
        /// <param name="initContext">The context.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="initContext" /> has invalid data.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initContext" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// That module has already been initialized.
        /// </exception>
        void Initialize(IAppServerModuleInitContext initContext);

        #endregion Operations
    }
}
