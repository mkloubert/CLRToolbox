// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Describes an application server.
    /// </summary>
    public interface IAppServer : INotificationObject, ITMDisposable, IRunnable
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the underlying context.
        /// </summary>
        IAppServerContext Context { get; }

        /// <summary>
        /// Gets if that server has already been initialized or not.
        /// </summary>
        bool IsInitialized { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Initializes that server.
        /// </summary>
        /// <param name="initContext">The context.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="initContext" /> has invalid data.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="initContext" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// That server has already been initialized.
        /// </exception>
        void Initialize(IAppServerInitContext initContext);

        #endregion Operations
    }
}
