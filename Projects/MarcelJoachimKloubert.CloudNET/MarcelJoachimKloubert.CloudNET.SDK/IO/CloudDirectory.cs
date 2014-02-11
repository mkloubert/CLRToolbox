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
        #region Fields (3)

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

        #endregion Fields

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Lists that directory.
        /// </summary>
        /// <returns>The result.</returns>
        public ListCloudDirectoryResult List()
        {
            return this.Server.ListDirectory(this.Path);
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

            this.Server.UploadFile(path, data);
        }

        #endregion Methods
    }
}
