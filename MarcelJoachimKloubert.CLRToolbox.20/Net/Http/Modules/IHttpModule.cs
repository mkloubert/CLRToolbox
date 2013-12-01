// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    /// <summary>
    /// Describes a HTTP module.
    /// </summary>
    public interface IHttpModule : IHasName, IIdentifiable
    {
        #region Operations (1)

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        IHttpModuleHandleRequestResult HandleRequest(IHttpRequestContext context);

        #endregion Operations
    }
}
