// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Describes a context for an <see cref="IAppServerModule.Initialize(IAppServerModuleInitContext)" /> method.
    /// </summary>
    public interface IAppServerModuleInitContext
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underlying module context.
        /// </summary>
        IAppServerModuleContext ModuleContext { get; }

        #endregion Data Members
    }
}
