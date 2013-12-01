// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    /// <summary>
    /// Describes a result of an <see cref="IHttpModule.HandleRequest(IHttpRequestContext)" />
    /// call.
    /// </summary>
    public interface IHttpModuleHandleRequestResult
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the list of errors that occured while handling the request.
        /// </summary>
        IList<Exception> Errors { get; }

        /// <summary>
        /// Gets the underlying request context.
        /// </summary>
        IHttpRequestContext RequestContext { get; }

        #endregion Data Members
    }
}
