// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Security.Cryptography;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes an object that provides content.
    /// </summary>
    public interface IContentProvider : ITMObject
    {
        #region Data Members (2)

        /// <summary>
        /// If available, gets the lower case MIME type of the data of that object (if available).
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// If available, gets the encoding of the underlying content.
        /// </summary>
        Encoding Encoding { get; }

        #endregion Data Members

        #region Operations (8)

        /// <summary>
        /// Calculates the hash of the object's content by using <see cref="Crc32" /> algorithm.
        /// </summary>
        /// <returns>The calculated hash.</returns>
        byte[] CalculateHashOfContent();

        /// <summary>
        /// Calculates the hash of the object's content by using a sepcific algorithm.
        /// </summary>
        /// <typeparam name="TAlgo">The algorithm to use.</typeparam>
        /// <returns>The calculated hash.</returns>
        byte[] CalculateHashOfContent<TAlgo>() where TAlgo : global::System.Security.Cryptography.HashAlgorithm, new();

        /// <summary>
        /// Calculates the hash of the object's content by using a sepcific algorithm.
        /// </summary>
        /// <param name="algo">The algorithm to use.</param>
        /// <returns>The calculated hash.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="algo" /> is <see langword="null" />.</exception>
        byte[] CalculateHashOfContent(HashAlgorithm algo);

        /// <summary>
        /// Returns the data of the stream from <see cref="IContentProvider.OpenStream()" /> as byte array.
        /// </summary>
        /// <returns>Stream as byte array.</returns>
        byte[] GetData();

        /// <summary>
        /// Returns the CRC32 hash of the object's content as lower case hex string.
        /// </summary>
        /// <returns>The calculated hash.</returns>
        string GetHashOfContentAsHexString();

        /// <summary>
        /// Returns the hash of the object's content as lower case hex string.
        /// </summary>
        /// <typeparam name="TAlgo">The algorithm to use.</typeparam>
        /// <returns>The calculated hash.</returns>
        string GetHashOfContentAsHexString<TAlgo>() where TAlgo : global::System.Security.Cryptography.HashAlgorithm, new();

        /// <summary>
        /// Returns the hash of the object's content as upper case hex string.
        /// </summary>
        /// <param name="algo">The algorithm to use.</param>
        /// <returns>The calculated hash.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="algo" /> is <see langword="null" />.</exception>
        string GetHashOfContentAsHexString(HashAlgorithm algo);

        /// <summary>
        /// Opens a new stream that can access the data that is provided by that object.
        /// </summary>
        /// <returns>The stream that accesses the data that is provided by that object.</returns>
        Stream OpenStream();

        #endregion Operations
    }
}