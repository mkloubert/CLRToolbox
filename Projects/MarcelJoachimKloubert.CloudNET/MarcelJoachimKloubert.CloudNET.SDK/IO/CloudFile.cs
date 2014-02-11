// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using Newtonsoft.Json;
using System;
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
        /// <exception cref="FileNotFoundException">
        /// File does not exist anymore.
        /// </exception>
        public void Delete()
        {
            this.Server
                .DeleteFile(this.Path);
        }

        /// <summary>
        /// Downloads the file from the underlying server.
        /// </summary>
        /// <returns>The downloaded data.</returns>
        /// <exception cref="FileNotFoundException">
        /// File does not exist anymore.
        /// </exception>
        public byte[] Download()
        {
            return this.Server
                       .DownloadFile(this.Path);
        }

        /// <summary>
        /// Downloads the file from the underlying server.
        /// </summary>
        /// <param name="target">The stream where to write the downloaded data to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// File does not exist anymore.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="target" /> cannot be written.
        /// </exception>
        public void Download(Stream target)
        {
            this.Server
                .DownloadFile(this.Path, target);
        }

        #endregion Methods
    }
}
