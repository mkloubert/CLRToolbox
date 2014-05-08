// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns
{
    /// <summary>
    /// Describes a plug in context.
    /// </summary>
    public interface IPlugInContext : IServiceLocator
    {
        #region Data Members (3)

        /// <summary>
        /// Gets the underyling assembly.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the full path to the assembly file.
        /// </summary>
        string AssemblyFile { get; }

        /// <summary>
        /// Gets the list of the plug ins that are part of that context.
        /// </summary>
        IList<IPlugIn> PlugIns { get; }

        #endregion Data Members
    }
}