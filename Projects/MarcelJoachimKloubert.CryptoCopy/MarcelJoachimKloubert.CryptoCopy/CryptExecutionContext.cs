// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CryptoCopy
{
    internal sealed class CryptExecutionContext : ICloneable
    {
        #region Fields (9)

        internal IList<string> Arguments;
        internal DirectoryInfo Destination;
        internal Action<CryptExecutionContext> InitialAction;
        internal RunningMode? Mode;
        internal byte[] Password;
        internal byte[] Salt;
        internal DirectoryInfo Source;
        internal DateTimeOffset StartTime;
        internal readonly object SYNC = new object();

        #endregion Fields

        #region Constructors (1)

        internal CryptExecutionContext(object sync)
        {
            this.SYNC = sync;

            this.StartTime = DateTimeOffset.Now;
        }

        #endregion Constructors

        #region Properties (1)

        internal int? ExitCode
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (3)

        // Private Methods (1) 

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        // Internal Methods (2) 

        internal CryptExecutionContext Clone()
        {
            return new CryptExecutionContext(this.SYNC)
                {
                    Arguments = this.Arguments,
                    Destination = this.Destination,
                    InitialAction = this.InitialAction,
                    Mode = this.Mode,
                    Password = this.Password,
                    Salt = this.Salt,
                    Source = this.Source,
                    StartTime = this.StartTime,
                };
        }

        internal void InvokeInitalAction()
        {
            this.InitialAction(this);
        }

        #endregion Methods
    }
}
