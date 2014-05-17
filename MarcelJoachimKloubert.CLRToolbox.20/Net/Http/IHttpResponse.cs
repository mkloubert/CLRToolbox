// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
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
        #region Data Members (12)

        /// <summary>
        /// Gets if the capacity of <see cref="IHttpResponse.Stream" /> can be definied or not.
        /// </summary>
        bool CanSetStreamCapacity { get; }

        /// <summary>
        /// Gets or sets the charset.
        /// </summary>
        Encoding Charset { get; set; }

        /// <summary>
        /// Gets or sets if response should be compressed or not.
        /// <see langword="null" /> indicates to use the default value.
        /// </summary>
        bool? Compress { get; set; }

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
        /// Get the list of variables (and their values) that should be overwritten in overwall frontend HTML template (if in use).
        /// </summary>
        IDictionary<string, object> FrontendVars { get; }

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

        #region Operations (16)

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
        /// Resets the content of <see cref="IHttpResponse.Stream" /> and sets its (new) capacity by using a default value.
        /// </summary>
        /// <returns>That instance.</returns>
        /// <exception cref="NotSupportedException">Capacity cannot be set.</exception>
        IHttpResponse SetDefaultStreamCapacity();

        /// <summary>
        /// Sets the new value for <see cref="IHttpResponse.Stream" /> property.
        /// </summary>
        /// <param name="stream">The new stream.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>Old stream is NOT disposed.</remarks>
        IHttpResponse SetStream(Stream stream);

        /// <summary>
        /// Sets the new value for <see cref="IHttpResponse.Stream" /> property.
        /// </summary>
        /// <param name="stream">The new stream.</param>
        /// <param name="disposeOld">Dispose old stream or not.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        IHttpResponse SetStream(Stream stream, bool disposeOld);

        /// <summary>
        /// Resets the content of <see cref="IHttpResponse.Stream" /> and sets its (new) capacity.
        /// </summary>
        /// <param name="capacity">The new capacity to set.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value of <paramref name="capacity" /> is invalid.</exception>
        /// <exception cref="NotSupportedException">Capacity cannot be set.</exception>
        IHttpResponse SetStreamCapacity(int capacity);

        /// <summary>
        /// Sets up that context for direct UTF-8 encoded JSON output.
        /// </summary>
        /// <returns>That instance.</returns>
        IHttpResponse SetupForJson();

        /// <summary>
        /// Writes an object to <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="obj">The object to write.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Write(object obj);

        /// <summary>
        /// Writes binary data to <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="data">The data to write.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Write(IEnumerable<byte> data);

        /// <summary>
        /// Writes text as binary data to <see cref="IHttpResponse.Stream" />
        /// based on <see cref="IHttpResponse.Charset" /> property.
        /// </summary>
        /// <param name="chars">The chars to write.</param>
        /// <returns>That instance.</returns>
        /// <remarks>DNNull value is handled as <see langword="null" /> reference.</remarks>
        IHttpResponse Write(IEnumerable<char> chars);

        /// <summary>
        /// Writes an object to <see cref="IHttpResponse.Stream" />.
        /// </summary>
        /// <param name="obj">The object to write.</param>
        /// <param name="handleDBNullAsNull">Handle DBNull value as <see langword="null" /> reference or not.</param>
        /// <returns>That instance.</returns>
        IHttpResponse Write(object obj, bool handleDBNullAsNull);

        /// <summary>
        /// Writes ECMA script code surrounded by a script HTML tag.
        /// </summary>
        /// <param name="js">The code to write.</param>
        /// <returns>That instance.</returns>
        IHttpResponse WriteJavaScript(IEnumerable<char> js);

        /// <summary>
        /// Writes an object as serialized JSON string.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to write.</param>
        /// <returns>That instance.</returns>
        IHttpResponse WriteJson<T>(T obj);

        #endregion Operations
    }
}