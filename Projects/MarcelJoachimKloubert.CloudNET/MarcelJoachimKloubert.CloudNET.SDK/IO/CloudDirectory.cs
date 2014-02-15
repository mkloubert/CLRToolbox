// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Stores the data of a cloud directory.
    /// </summary>
    public sealed class CloudDirectory
    {
        #region Fields (6)

        /// <summary>
        /// Stores the creation time in UTC format.
        /// </summary>
        [JsonProperty(PropertyName = "creationTime")]
        public DateTime? CreationTime;

        /// <summary>
        /// Stores if that directory is the root directory or not.
        /// </summary>
        [JsonProperty(PropertyName = "isRoot")]
        public bool? IsRoot;

        /// <summary>
        /// Stores the name of the directory.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name;

        /// <summary>
        /// Stores the path of the directory.
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path;

        /// <summary>
        /// Stores the underlying server.
        /// </summary>
        public CloudServer Server;

        /// <summary>
        /// Stores the last write time in UTC format.
        /// </summary>
        [JsonProperty(PropertyName = "lastWriteTime")]
        public DateTime? WriteTime;

        #endregion Fields

        #region Methods (5)

        // Public Methods (5) 

        /// <summary>
        /// Deletes that directory on its server.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">
        /// Directory does not exist.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Root directory cannot be deleted.
        /// </exception>
        public void Delete()
        {
            if (this.IsRoot == true)
            {
                throw new InvalidOperationException();
            }

            this.Server
                .FileSystem
                .DeleteDirectory(this.Path);
        }

        /// <summary>
        /// Lists that directory.
        /// </summary>
        /// <returns>The result.</returns>
        /// <exception cref="DirectoryNotFoundException">
        /// Directory does not exist anymore.
        /// </exception>
        public ListCloudDirectoryResult List()
        {
            return this.Server
                       .FileSystem
                       .ListDirectory(this.Path);
        }

        /// <summary>
        /// Updates the creation time of that directory.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="DirectoryNotFoundException">
        /// Directory does not exist.
        /// </exception>
        public void UpdateCreationTime(DateTime? newValue)
        {
            this.Server
                .FileSystem
                .UpdateDirectoryCreationTime(this.Path, newValue);

            this.CreationTime = newValue;
        }

        /// <summary>
        /// Updates the write time of that directory.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="DirectoryNotFoundException">
        /// Directory does not exist.
        /// </exception>
        public void UpdateWriteTime(DateTime? newValue)
        {
            this.Server
                .FileSystem
                .UpdateDirectoryWriteTime(this.Path, newValue);

            this.WriteTime = newValue;
        }

        /// <summary>
        /// Uploads a file to a server.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="data">The data to upload.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="fileName" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fileName" /> and/or <paramref name="data" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="data" /> cannot be read.
        /// </exception>
        public void UploadFile(IEnumerable<char> fileName, Stream data)
        {
            string name = StringHelper.AsString(fileName);
            if (name == null)
            {
                throw new ArgumentNullException("fileName");
            }

            name = name.Trim();
            if (name == string.Empty)
            {
                throw new ArgumentException("fileName");
            }

            string path = this.Path ?? string.Empty;

            if (path.StartsWith("/") == false)
            {
                path = "/" + path;
            }

            if (path.EndsWith("/") == false)
            {
                path += "/";
            }

            path += name;

            this.Server
                .FileSystem
                .UploadFile(path, data);
        }

        #endregion Methods
    }
}
