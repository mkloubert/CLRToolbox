// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.ServiceLocation.Impl
{
    partial class DelegateServiceLocator
    {
        #region Nested Classes (1)

        private sealed class InstanceProvider
        {
            #region Fields (2)

            internal readonly Delegate PROVIDER;
            internal readonly Type TYPE;

            #endregion Fields

            #region Constructors (1)

            internal InstanceProvider(Type type, Delegate provider)
            {
                this.TYPE = type;
                this.PROVIDER = provider;
            }

            #endregion Constructors

            #region Methods (1)

            // Internal Methods (1) 

            internal T Invoke<T>(IServiceLocator baseLocator, object key)
            {
                return (T)this.PROVIDER
                              .Method
                              .Invoke(this.PROVIDER.Target,
                                      new object[] { baseLocator, key });
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
