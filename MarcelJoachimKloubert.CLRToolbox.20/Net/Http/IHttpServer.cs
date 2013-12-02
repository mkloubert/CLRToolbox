// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Security.Principal;
using MarcelJoachimKloubert.CLRToolbox.Security;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    #region DELEGATE: HttpPrincipalProvider

    /// <summary>
    /// Logic that tries to find an <see cref="IPrincipal" /> by an <see cref="IIdentity" />.
    /// </summary>
    /// <param name="id">The <see cref="IIdentity" />.</param>
    /// <returns>The found <see cref="IPrincipal" /> or <see langword="null" /> if not found.</returns>
    public delegate IPrincipal HttpPrincipalProvider(IIdentity id);

    #endregion

    #region DELEGATE: HttpRequestValidator

    /// <summary>
    /// Validator that checks the data of a request, e.g. the address of the remote client.
    /// </summary>
    /// <param name="request">The request context.</param>
    /// <returns>Request data fill criteria for handling it or not.</returns>
    public delegate bool HttpRequestValidator(IHttpRequest request);

    #endregion

    #region INTERFACE: IHttpServer

    /// <summary>
    /// Describes a HTTP server.
    /// </summary>
    public interface IHttpServer : ITMDisposable, IRunnable
    {
        #region Data Members (7)

        /// <summary>
        /// Gets or sets the validator that checks if the combination of a username and passsword matches.
        /// </summary>
        UsernamePasswordValidator CredentialValidator { get; set; }

        /// <summary>
        /// Gets or sets the TCP port to use.
        /// <see langword="null" /> indicates to use the default.
        /// </summary>
        int? Port { get; set; }

        /// <summary>
        /// Gets or sets the provider for finding an <see cref="IPrincipal" /> by an <see cref="IIdentity" />.
        /// </summary>
        HttpPrincipalProvider PrincipalFinder { get; set; }

        /// <summary>
        /// Gets or sets the validator that checks a <see cref="IHttpRequest" /> before handling it.
        /// </summary>
        HttpRequestValidator RequestValidator { get; set; }

        /// <summary>
        /// Gets if that server supports SSL or not.
        /// </summary>
        bool SupportsSecureHttp { get; }

        /// <summary>
        /// Gets or sets the mode the data should be transfered.
        /// </summary>
        HttpTransferMode TransferMode { get; set; }

        /// <summary>
        /// Gets or sets if SSL should be used or not.
        /// </summary>
        bool UseSecureHttp { get; set; }

        #endregion Data Members

        #region Delegates and Events (4)

        // Events (4) 

        /// <summary>
        /// Is invoked when a document / resource was not found.
        /// </summary>
        event EventHandler<HttpRequestEventArgs> HandleDocumentNotFound;

        /// <summary>
        /// Is invoked when the handling of a request failed.
        /// </summary>
        event EventHandler<HttpRequestErrorEventArgs> HandleError;

        /// <summary>
        /// Is invoked when a HTTP is forbidden.
        /// </summary>
        event EventHandler<HttpRequestEventArgs> HandleForbidden;

        /// <summary>
        /// Is invoked when a HTTP has been arrived that should be handled.
        /// </summary>
        event EventHandler<HttpRequestEventArgs> HandleRequest;

        #endregion Delegates and Events

        #region Operations (1)

        /// <summary>
        /// Sets the SSL certificate for the HTTPs connections by thumbprint.
        /// </summary>
        /// <param name="thumbprint">The thumbprint of the certificate as HEX string.</param>
        /// <returns>That instance.</returns>
        IHttpServer SetSslCertificateByThumbprint(IEnumerable<char> thumbprint);

        #endregion Operations
    }

    #endregion
}
