// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Describes a module for an application server.
    /// </summary>
    public interface IAppServerModule : INotificationObject,
                                        IIdentifiable,
                                        IHasName,
                                        IRunnable
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying context.
        /// </summary>
        IAppServerModuleContext Context { get; }

        #endregion Data Members
    }
}
