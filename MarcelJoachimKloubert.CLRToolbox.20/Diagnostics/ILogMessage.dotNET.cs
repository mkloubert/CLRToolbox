// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Runtime.Remoting.Contexts;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    partial interface ILogMessage
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the underyling context.
        /// </summary>
        Context Context { get; }

        #endregion Data Members
    }
}
