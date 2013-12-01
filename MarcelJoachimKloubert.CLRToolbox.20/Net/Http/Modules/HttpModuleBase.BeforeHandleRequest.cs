// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    partial class HttpModuleBase
    {
        #region INTERFACE: IBeforeHandleRequestContext

        /// <summary>
        /// Stores the data for a <see cref="HttpModuleBase.OnBeforeHandleRequest(IBeforeHandleRequestContext)" /> method call.
        /// </summary>
        protected interface IBeforeHandleRequestContext
        {
            #region Data Members (3)

            /// <summary>
            /// Gets or sets if <see cref="HttpModuleBase.OnAfterHandleRequest(IAfterHandleRequestContext)" />
            /// should be invoked or not.
            /// Is <see langword="true" /> by default.
            /// </summary>
            bool InvokeAfterHandleRequest { get; set; }

            /// <summary>
            /// Gets or sets if <see cref="HttpModuleBase.OnHandleRequest(IHandleRequestContext)" />
            /// should be invoked or not.
            /// Is <see langword="true" /> by default.
            /// </summary>
            bool InvokeHandleRequest { get; set; }

            /// <summary>
            /// Gets the underlying HTTP request.
            /// </summary>
            IHttpRequestContext HttpRequest { get; }

            #endregion Data Members
        }

        #endregion

        #region CLASS: BeforeHandleRequestContext

        private sealed class BeforeHandleRequestContext : IBeforeHandleRequestContext
        {
            #region Fields (3)

            private IHttpRequestContext _httpRequest;
            private bool _invokeAfterHandleRequest;
            private bool _invokeHandleRequest;

            #endregion Fields

            #region Properties (3)

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

            public bool InvokeHandleRequest
            {
                get { return this._invokeHandleRequest; }

                set { this._invokeHandleRequest = value; }
            }

            #endregion Properties
        }

        #endregion
    }
}
