// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    partial class HttpModuleBase
    {
        #region INTERFACE: IHandleRequestContext

        /// <summary>
        /// Stores the data for a <see cref="HttpModuleBase.OnAfterHandleRequest(IAfterHandleRequestContext)" /> method call.
        /// </summary>
        protected interface IHandleRequestContext
        {
            #region Data Members (2)

            /// <summary>
            /// Gets or sets if <see cref="HttpModuleBase.OnAfterHandleRequest(IAfterHandleRequestContext)" />
            /// should be invoked or not.
            /// Is <see langword="true" /> by default.
            /// </summary>
            bool InvokeAfterHandleRequest { get; set; }

            /// <summary>
            /// Gets the underlying HTTP request.
            /// </summary>
            IHttpRequestContext HttpRequest { get; }

            #endregion Data Members
        }

        #endregion

        #region CLASS: HandleRequestContext

        private sealed class HandleRequestContext : IHandleRequestContext
        {
            #region Fields (2)

            private IHttpRequestContext _httpRequest;
            private bool _invokeAfterHandleRequest;

            #endregion Fields

            #region Properties (2)

            public IHttpRequestContext HttpRequest
            {
                get { return this._httpRequest; }

                internal set { this._httpRequest = value; }
            }

            public bool InvokeAfterHandleRequest
            {
                get { return this._invokeAfterHandleRequest; }

                set { this._invokeAfterHandleRequest = value; }
            }

            #endregion Properties
        }

        #endregion
    }
}
