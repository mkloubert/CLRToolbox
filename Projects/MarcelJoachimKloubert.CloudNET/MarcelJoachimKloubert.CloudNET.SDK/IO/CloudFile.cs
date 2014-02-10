// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Stores the data of a cloud file.
    /// </summary>
    public sealed class CloudFile
    {
        #region Fields (4)

        /// <summary>
        /// Stores the name of the file.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name;

        /// <summary>
        /// Stores the path of the file.
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path;

        /// <summary>
        /// Stores the underlying server.
        /// </summary>
        public CloudServer Server;

        /// <summary>
        /// Stores the size of the file.
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size;

        #endregion Fields

        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Deletes that file.
        /// </summary>
        public void Delete()
        {
            this.Server.DeleteFile(this.Path);
        }

        /// <summary>
        /// Downloads a file from the underlying server.
        /// </summary>
        /// <param name="filePath">The full path from where the file should be downloaded.</param>
        /// <returns>The downloaded data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> is <see langword="null" />.
        /// </exception>
        public byte[] DownloadFile(IEnumerable<char> filePath)
        {
            return this.Server.DownloadFile(filePath);
        }

        /// <summary>
        /// Downloads a file from the underlying server.
        /// </summary>
        /// <param name="filePath">The full path from where the file should be downloaded.</param>
        /// <param name="target">The stream where to write the downloaded data to.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> and/or <paramref name="target" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="target" /> cannot be written.
        /// </exception>
        public void DownloadFile(IEnumerable<char> filePath, Stream target)
        {
            this.Server.DownloadFile(filePath, target);
        }

        #endregion Methods
    }
}
