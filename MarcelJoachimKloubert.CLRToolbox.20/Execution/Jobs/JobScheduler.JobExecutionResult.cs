// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
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
        protected class JobExecutionResult : TMObject, IJobExecutionResult
        {
            #region Fields (4)

            private JobExecutionContext _context;
            private IList<Exception> _errors;
            private IReadOnlyDictionary<string, object> _result;
            private DateTimeOffset _time;

            #endregion Fields

            #region Properties (6)

            /// <inheriteddoc />
            public JobExecutionContext Context
            {
                get { return this._context; }

                set { this._context = value; }
            }

            /// <inheriteddoc />
            public IList<Exception> Errors
            {
                get { return this._errors; }

                set { this._errors = value; }
            }

            /// <inheriteddoc />
            public bool HasFailed
            {
                get
                {
                    IList<Exception> errs = this.Errors;
                    return errs != null &&
                           CollectionHelper.Any(errs,
                                                delegate(Exception ex, long index)
                                                {
                                                    return ex != null;
                                                });
                }
            }

            /// <inheriteddoc />
            public IReadOnlyDictionary<string, object> Result
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

            IJobExecutionContext IJobExecutionResult.Context
            {
                get { return this.Context; }
            }

            #endregion Properties
        }

        #endregion Nested classes
    }
}