// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Stores the result of listening a cloud directory.
    /// </summary>
    public sealed partial class ListCloudDirectoryResult
    {
        #region Fields (8)

        /// <summary>
        /// Stores the creation time in UTC format.
        /// </summary>
        [JsonProperty(PropertyName = "creationTime")]
        public DateTime? CreationTime;

        /// <summary>
        /// Stores the list of directories that are inside the directory.
        /// </summary>
        public CloudDirectoryCollection Directories;

        /// <summary>
        /// Stores the list of files that are inside the directory.
        /// </summary>
        public CloudFileCollection Files;

        /// <summary>
        /// Stores if the underlying directory is the root directory or not.
        /// </summary>
        [JsonProperty(PropertyName = "isRootDir")]
        public bool? IsRootDirectory;

        /// <summary>
        /// Stores the path of parent directory if available.
        /// </summary>
        [JsonProperty(PropertyName = "parentPath")]
        public string ParentPath;

        /// <summary>
        /// Stores the underlying path.
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

        #region Properties (2)

        [JsonProperty(PropertyName = "dirs")]
        private IList<CloudDirectory> dirsInner
        {
            set { this.Directories = value != null ? new CloudDirectoryCollection(value) : null; }
        }

        [JsonProperty(PropertyName = "files")]
        private IList<CloudFile> filesInner
        {
            set { this.Files = value != null ? new CloudFileCollection(value) : null; }
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (10) 

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dirName">The name of the directory to create.</param>
        /// <returns>The created directory.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirName" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirName" /> is <see langword="null" />.
        /// </exception>
        public ListCloudDirectoryResult CreateDirectory(IEnumerable<char> dirName)
        {
            string name = StringHelper.AsString(dirName);
            if (name == null)
            {
                throw new ArgumentNullException("dirName");
            }

            name = name.Trim();
            if (name == string.Empty)
            {
                throw new ArgumentException("dirName");
            }

            string path = (this.Path ?? string.Empty).Trim();

            if (path.StartsWith("/") == false)
            {
                path = "/" + path;
            }

            if (path.EndsWith("/") == false)
            {
                path += "/";
            }

            path += name + "/";

            this.Server
                .FileSystem
                .CreateDirectory(path);

            return this.Server
                       .FileSystem
                       .ListDirectory(path);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dirPath">The path of the local directory.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirPath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> seems to represent a non existing directory.
        /// </exception>
        /// <remarks>
        /// Directory is synced recursivly and process is started autmatically.
        /// </remarks>
        public ISyncWithLocalDirectoryExecutionContext SyncWithLocalDirectory(IEnumerable<char> dirPath)
        {
            return this.SyncWithLocalDirectory(dirPath, true);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dir">The local directory.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dir" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Directory is synced recursivly and process is started autmatically.
        /// </remarks>
        public ISyncWithLocalDirectoryExecutionContext SyncWithLocalDirectory(DirectoryInfo dir)
        {
            return this.SyncWithLocalDirectory(dir, true);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dirPath">The path of the local directory.</param>
        /// <param name="recursive">Sync recursively or not.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirPath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> seems to represent a non existing directory.
        /// </exception>
        /// <remarks>
        /// Sync process is started autmatically.
        /// </remarks>
        public ISyncWithLocalDirectoryExecutionContext SyncWithLocalDirectory(IEnumerable<char> dirPath,
                                                                              bool recursive)
        {
            return this.SyncWithLocalDirectory(new DirectoryInfo(StringHelper.AsString(dirPath)),
                                               recursive);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dir">The local directory.</param>
        /// <param name="recursive">Sync recursively or not.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dir" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// Sync process is started autmatically.
        /// </remarks>
        public ISyncWithLocalDirectoryExecutionContext SyncWithLocalDirectory(DirectoryInfo dir,
                                                                              bool recursive)
        {
            return this.SyncWithLocalDirectory(dir,
                                               recursive,
                                               true);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dirPath">The path of the local directory.</param>
        /// <param name="recursive">Sync recursively or not.</param>
        /// <param name="autostart">Start sync process automatically or not.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> is <see langword="null" />.
        /// </exception>
        public ISyncWithLocalDirectoryExecutionContext SyncWithLocalDirectory(IEnumerable<char> dirPath,
                                                                              bool recursive,
                                                                              bool autostart)
        {
            return this.SyncWithLocalDirectory(new DirectoryInfo(StringHelper.AsString(dirPath)),
                                               recursive,
                                               true);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="dir">The local directory.</param>
        /// <param name="recursive">Sync recursively or not.</param>
        /// <param name="autostart">Start sync process automatically or not.</param>
        /// <returns>The created directory.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dir" /> is <see langword="null" />.
        /// </exception>
        public ISyncWithLocalDirectoryExecutionContext SyncWithLocalDirectory(DirectoryInfo dir,
                                                                              bool recursive,
                                                                              bool autostart)
        {
            if (dir == null)
            {
                throw new ArgumentNullException("dir");
            }

            SyncWithLocalDirectoryExecutionContext ctx = new SyncWithLocalDirectoryExecutionContext();
            ctx.LocalDirectory = dir.FullName;
            ctx.RemoteDirectory = this;

            if (autostart)
            {
                ctx.Start();
            }

            return ctx;
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
