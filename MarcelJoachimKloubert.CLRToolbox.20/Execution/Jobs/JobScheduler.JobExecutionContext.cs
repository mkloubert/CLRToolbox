// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    partial class JobScheduler
    {
        #region Nested classes (1)

        /// <summary>
        /// A simple implementation of the <see cref="IJobExecutionContext" /> interface.
        /// </summary>
        protected class JobExecutionContext : TMObject, IJobExecutionContext
        {
            #region Fields (4)

            private IJob _job;
            private Guid _id;
            private IDictionary<string, object> _result;
            private DateTimeOffset _time;

            #endregion Fields

            #region Properties (4)

            /// <inheriteddoc />
            public IJob Job
            {
                get { return this._job; }

                set { this._job = value; }
            }

            /// <inheriteddoc />
            public Guid Id
            {
                get { return this._id; }

                set { this._id = value; }
            }
            
            /// <inheriteddoc />
            public IDictionary<string, object> ResultVars
            {
                get { return this._result; }

                set { this._result = value; }
            }

            /// <inheriteddoc />
            public DateTimeOffset Time
            {
                get { return this._time; }

                set { this._time = value; }
            }

            #endregion Properties

            #region Methods (4)

            /// <inheriteddoc />
            public bool Equals(IIdentifiable other)
            {
                return other != null ? this.Equals(other.Id) : false;
            }

            /// <inheriteddoc />
            public bool Equals(Guid other)
            {
                return this.Id == other;
            }

            /// <inheriteddoc />
            public override bool Equals(object other)
            {
                if (other is IIdentifiable)
                {
                    return this.Equals((IIdentifiable)other);
                }

                if (other is Guid)
                {
                    return this.Equals((Guid)other);
                }

                return base.Equals(other);
            }

            /// <inheriteddoc />
            public override int GetHashCode()
            {
                return this.Id.GetHashCode();
            }

            #endregion Properties
        }

        #endregion Nested classes
    }
}