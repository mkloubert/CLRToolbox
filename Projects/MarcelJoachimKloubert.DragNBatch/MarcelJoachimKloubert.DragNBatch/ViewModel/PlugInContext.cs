// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.DragNBatch.PlugIns;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.DragNBatch.ViewModel
{
    internal sealed class PlugInContext : ServiceLocatorBase, IPlugInContext
    {
        #region Properties (4)

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

        internal IServiceLocator ServiceLocator
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (2)

        // Protected Methods (2) 

        protected override IEnumerable<object> OnGetAllInstances(Type serviceType, object key)
        {
            return this.ServiceLocator
                       .GetAllInstances(serviceType, key);
        }

        protected override object OnGetInstance(Type serviceType, object key)
        {
            return this.ServiceLocator
                       .GetInstance(serviceType, key);
        }

        #endregion Methods
    }
}