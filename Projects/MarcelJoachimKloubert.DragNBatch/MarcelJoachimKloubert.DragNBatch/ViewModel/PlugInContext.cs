// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.DragNBatch.PlugIns;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.DragNBatch.ViewModel
{
    internal sealed class PlugInContext : IPlugInContext
    {
        #region Properties (3)

        public Assembly Assembly
        {
            get;
            internal set;
        }

        public string AssemblyFile
        {
            get;
            internal set;
        }

        public IList<IPlugIn> PlugIns
        {
            get;
            internal set;
        }

        #endregion Properties
    }
}