// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    partial class HttpModuleBase
    {
        #region Nested Classes (1)

        private sealed class HttpModuleHandleRequestResult : IHttpModuleHandleRequestResult
        {
            #region Fields (2)

            private IList<Exception> _errors;
            private IHttpRequestContext _requestContext;

            #endregion Fields

            #region Properties (2)

            public IList<Exception> Errors
            {
                get { return this._errors; }

                internal set { this._errors = value; }
            }

            public IHttpRequestContext RequestContext
            {
                get { return this._requestContext; }

                internal set { this._requestContext = value; }
            }

            #endregion Properties
        }

        #endregion Nested Classes
    }
}
