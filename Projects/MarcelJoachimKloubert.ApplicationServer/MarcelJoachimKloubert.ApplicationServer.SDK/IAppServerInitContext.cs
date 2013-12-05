// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Diagnostics;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Describes a context for an <see cref="IAppServer.Initialize(IAppServerInitContext)" /> method.
    /// </summary>
    public interface IAppServerInitContext
    {
        #region Data Members (4)

        /// <summary>
        /// Gets the list of (command line) arguments.
        /// </summary>
        string[] Arguments { get; }

        /// <summary>
        /// Gets the optional global logger instance.
        /// </summary>
        ILoggerFacade Logger { get; }

        /// <summary>
        /// Gets the underlying server context.
        /// </summary>
        IAppServerContext ServerContext { get; }

        /// <summary>
        /// Gets the working / root directory of the server.
        /// </summary>
        string WorkingDirectory { get; }

        #endregion Data Members
    }
}
