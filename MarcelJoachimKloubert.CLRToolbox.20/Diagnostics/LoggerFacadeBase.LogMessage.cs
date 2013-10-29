// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    partial class LoggerFacadeBase
    {
        #region Nested Classes (1)

        [Serializable]
        private sealed class LogMessage : MarshalByRefObject, ILogMessage
        {
            #region Properties (10)

            public Assembly Assembly
            {
                get;
                internal set;
            }

            public IList<LoggerFacadeCategories> Categories
            {
                get;
                internal set;
            }

            public Context Context
            {
                get;
                internal set;
            }

            public Guid Id
            {
                get;
                internal set;
            }

            public MemberInfo Member
            {
                get;
                internal set;
            }

            public object Message
            {
                get;
                internal set;
            }

            public IPrincipal Principal
            {
                get;
                internal set;
            }

            public string Tag
            {
                get;
                internal set;
            }

            public Thread Thread
            {
                get;
                internal set;
            }

            public DateTimeOffset Time
            {
                get;
                internal set;
            }

            #endregion Properties

            #region Methods (4)

            // Public Methods (4) 

            public override bool Equals(object other)
            {
                if (other is IIdentifiable)
                {
                    return this.Equals(other as IIdentifiable);
                }

                if (other is Guid)
                {
                    return this.Equals((Guid)other);
                }

                return base.Equals(other);
            }

            public bool Equals(IIdentifiable other)
            {
                return other != null ? this.Equals(other.Id) : false;
            }

            public bool Equals(Guid other)
            {
                return this.Id == other;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
