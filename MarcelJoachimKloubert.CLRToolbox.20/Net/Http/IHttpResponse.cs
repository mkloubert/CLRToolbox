// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http
{
    /// <summary>
    /// Describes a response for a HTTP request.
    /// </summary>
    public interface IHttpResponse : ITMObject
    {
        #region Data Members (10)

        /// <summary>
        /// Gets or sets the charset.
        /// </summary>
        Encoding Charset { get; set; }

        /// <summary>
        /// Gets or sets if response should be compressed or not.
        /// </summary>
        bool Compress { get; set; }

        /// <summary>
        /// Gets or sets the mime / content type.
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Only output content of <see cref="IHttpResponse.Stream" />
        /// (<see langword="true" />) or surround as HTML page with frontend
        /// data (<see langword="true" />).
        /// </summary>
        bool DirectOutput { get; set; }

        /// <summary>
        /// Gets or sets if a HTTP 404 should be raised or not.
        /// Is <see langword="false" /> by default.
        /// </summary>
        bool DocumentNotFound { get; set; }

        /// <summary>
        /// Gets the list of headers that should be send as response.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets or sets if a HTTP 403 should be raised or not.
        /// Is <see langword="false" /> by default.
        /// </summary>
        bool IsForbidden { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// Is <see cref="HttpStatusCode.OK" /> by default.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        string StatusDescription { get; set; }

        /// <summary>
        /// Gets the stream for the response data.
        /// </summary>
        Stream Stream { get; }

        #endregion Data Members

        #region Operations (7)

        /// <summary>
        /// Adds data at the end of <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="data">The data to add.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Append(IEnumerable<byte> data);

        /// <summary>
        /// Adds data at the end of <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="chars">The data to add.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Append(IEnumerable<char> chars);

        /// <summary>
        /// Clearts the current content in <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <returns>That instance.</returns>
        IHttpResponse Clear();

        /// <summary>
        /// Adds data at the beginning of <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="data">The data to add.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Prefix(IEnumerable<byte> data);

        /// <summary>
        /// Adds data at the beginning of <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="chars">The data to add.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Prefix(IEnumerable<char> chars);

        /// <summary>
        /// Writes binary data to <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="data">The data to write.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Write(IEnumerable<byte> data);

        /// <summary>
        /// Writes binary data to <see cref="IHttpResponse.Stream" />
        /// based on <see cref="IHttpResponse.Charset" /> property.
        /// </summary>
        /// <param name="chars">The chars to write.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Write(IEnumerable<char> chars);

        #endregion Operations
    }
}
