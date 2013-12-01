// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Simple implementation of the <see cref="IAppServerModuleInitContext" /> interface.
    /// </summary>
    public sealed class SimpleAppServerModuleInitContext : IAppServerModuleInitContext
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModuleInitContext.ModuleContext" />
        public IAppServerModuleContext ModuleContext
        {
            get;
            set;
        }

        #endregion Properties
    }
}
