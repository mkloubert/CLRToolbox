// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation
{
    partial class ServiceLocatorBase
    {
        #region Methods (1)

        // Private Methods (1) 

        object IServiceProvider.GetService(Type serviceType)
        {
            try
            {
                return this.GetInstance(serviceType);
            }
            catch (ServiceActivationException sae)
            {
                Exception innerEx = sae.InnerException;
                if (innerEx != null)
                {
                    throw innerEx;
                }

                return null;
            }
        }

        #endregion Methods
    }
}