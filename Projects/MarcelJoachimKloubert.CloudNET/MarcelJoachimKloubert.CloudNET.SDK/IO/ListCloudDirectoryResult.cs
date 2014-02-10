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
    /// List the result of cloud directory.
    /// </summary>
    public sealed class ListCloudDirectoryResult
    {
        #region Fields (3)

        /// <summary>
        /// Stores the list of directories that are inside the directory.
        /// </summary>
        [JsonProperty(PropertyName = "dirs")]
        public IList<CloudDirectory> Directories;
        /// <summary>
        /// Stores the list of files that are inside the directory.
        /// </summary>
        [JsonProperty(PropertyName = "files")]
        public IList<CloudFile> Files;
        /// <summary>
        /// Stores the underlying path.
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path;

        #endregion Fields

        #region Properties (1)

        /// <summary>
        /// Gets or sets the underlying server.
        /// </summary>
        public CloudServer Server
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

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

            this.Server.UploadFile(path, data);
        }

        #endregion Methods
    }
}
