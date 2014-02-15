// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    /// <summary>
    /// Manages the file system of a server.
    /// </summary>
    public sealed class CloudFileSystem : CloudServerObjectBase
    {
        #region Fields (3)

        private const string _DATEFORMAT = "yyyy-MM-dd HH:mm:ss";
        private const string _PATCH_TYPE_CREATIONTIME = "creationtime";
        private const string _PATCH_TYPE_WRITETIME = "writetime";

        #endregion Fields

        #region Constructors (1)

        internal CloudFileSystem(CloudServer server, object sync)
            : base(server, sync)
        {

        }

        #endregion Constructors

        #region Methods (15)

        // Public Methods (13) 

        /// <summary>
        /// Creates a new dtirectory.
        /// </summary>
        /// <param name="dirPath">The path of the new directory.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirPath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> is <see langword="null" />.
        /// </exception>
        public void CreateDirectory(IEnumerable<char> dirPath)
        {
            string path = StringHelper.AsString(dirPath);
            if (path == null)
            {
                throw new ArgumentNullException("dirPath");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("dirPath");
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri());
            httpRequest.Method = "PUT";
            httpRequest.Headers["X-MJKTM-CloudNET-Directory"] = path;
            httpRequest.SendChunked = true;

            using (Stream stream = httpRequest.GetRequestStream())
            {
                stream.Close();
            }

            httpRequest.GetResponse().Close();
        }

        /// <summary>
        /// Deletes a directory on this server.
        /// </summary>
        /// <param name="dirPath">The full path of the directory that should be deleted</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirPath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="dirPath" /> does not exist.
        /// </exception>
        public void DeleteDirectory(IEnumerable<char> dirPath)
        {
            string path = StringHelper.AsString(dirPath);
            if (path == null)
            {
                throw new ArgumentNullException("dirPath");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("dirPath");
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri());
            httpRequest.Method = "DELETE";
            httpRequest.Headers["X-MJKTM-CloudNET-Directory"] = path;

            try
            {
                httpRequest.GetResponse().Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse resp = wex.Response as HttpWebResponse;
                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new DirectoryNotFoundException(path ?? string.Empty);
                    }
                }

                throw;
            }
        }

        /// <summary>
        /// Deletes a file on this server.
        /// </summary>
        /// <param name="filePath">The full path of the file that should be deleted</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// <paramref name="filePath" /> does not exist.
        /// </exception>
        public void DeleteFile(IEnumerable<char> filePath)
        {
            string path = StringHelper.AsString(filePath);
            if (path == null)
            {
                throw new ArgumentNullException("filePath");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("filePath");
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri());
            httpRequest.Method = "DELETE";
            httpRequest.Headers["X-MJKTM-CloudNET-File"] = path;

            try
            {
                httpRequest.GetResponse().Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse resp = wex.Response as HttpWebResponse;
                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new FileNotFoundException(path ?? string.Empty);
                    }
                }

                throw;
            }
        }

        /// <summary>
        /// Downloads a file from this server.
        /// </summary>
        /// <param name="filePath">The full path from where the file should be downloaded.</param>
        /// <returns>The downloaded data.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// <paramref name="filePath" /> does not exist.
        /// </exception>
        public byte[] DownloadFile(IEnumerable<char> filePath)
        {
            using (MemoryStream temp = new MemoryStream())
            {
                this.DownloadFile(filePath, temp);

                return temp.ToArray();
            }
        }

        /// <summary>
        /// Downloads a file from this server.
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
        /// <exception cref="FileNotFoundException">
        /// <paramref name="filePath" /> does not exist.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="target" /> cannot be written.
        /// </exception>
        public void DownloadFile(IEnumerable<char> filePath, Stream target)
        {
            string path = StringHelper.AsString(filePath);
            if (path == null)
            {
                throw new ArgumentNullException("filePath");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("filePath");
            }

            if (target == null)
            {
                throw new ArgumentNullException("data");
            }

            if (target.CanWrite == false)
            {
                throw new IOException();
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri());
            httpRequest.Method = "GET";

            if (path != null)
            {
                httpRequest.Headers["X-MJKTM-CloudNET-File"] = path;
            }

            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException wex)
            {
                HttpWebResponse resp = wex.Response as HttpWebResponse;
                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new FileNotFoundException(path ?? string.Empty);
                    }
                }

                throw;
            }

            using (Stream stream = httpResponse.GetResponseStream())
            {
                IOHelper.CopyTo(stream, target);
            }
        }

        /// <summary>
        /// Returns the base URI for handling files.
        /// </summary>
        /// <returns>the base URI.</returns>
        public string GetBaseUri()
        {
            return this.Server.GetBaseUri() + "files";
        }

        /// <summary>
        /// Lists a directory.
        /// </summary>
        /// <param name="dir">The path of the directory.</param>
        /// <returns>The result.</returns>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="dir" /> does not exist.
        /// </exception>
        public ListCloudDirectoryResult ListDirectory(IEnumerable<char> dir)
        {
            string path = (StringHelper.AsString(dir) ?? string.Empty).Trim();
            if (path == string.Empty)
            {
                path = null;
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri() + "?action=list");
            httpRequest.Method = "GET";

            if (path != null)
            {
                httpRequest.Headers["X-MJKTM-CloudNET-Directory"] = path;
            }

            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException wex)
            {
                HttpWebResponse resp = wex.Response as HttpWebResponse;
                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new DirectoryNotFoundException(path ?? string.Empty);
                    }
                }

                throw;
            }

            using (Stream stream = httpResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string json = reader.ReadToEnd().Trim();
                    if (json != string.Empty &&
                        json != "null")
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        using (StringReader strReader = new StringReader(json))
                        {
                            using (JsonTextReader jsonReader = new JsonTextReader(strReader))
                            {
                                ListCloudDirectoryResult result = serializer.Deserialize<ListCloudDirectoryResult>(jsonReader);
                                result.Server = this.Server;

                                // link that server instance with directory items
                                CloudDirectoryCollection dirs = result.Directories;
                                if (dirs != null)
                                {
                                    foreach (CloudDirectory d in dirs)
                                    {
                                        if (d == null)
                                        {
                                            continue;
                                        }

                                        d.Server = this.Server;
                                    }
                                }

                                // link that server instance with file items
                                CloudFileCollection files = result.Files;
                                if (files != null)
                                {
                                    foreach (CloudFile f in files)
                                    {
                                        if (f == null)
                                        {
                                            continue;
                                        }

                                        f.Server = this.Server;
                                    }
                                }

                                return result;
                            }
                        }
                    }
                }
            }

            // no data availables
            return null;
        }

        /// <summary>
        /// Lists the root directory.
        /// </summary>
        /// <returns>The result.</returns>
        public ListCloudDirectoryResult ListRootDirectory()
        {
            return this.ListDirectory(null);
        }

        /// <summary>
        /// Updates the creation time of a directory.
        /// </summary>
        /// <param name="dirPath">The path to the directory.</param>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirPath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="dirPath" /> does not exist.
        /// </exception>
        public void UpdateDirectoryCreationTime(IEnumerable<char> dirPath, DateTimeOffset? newValue)
        {
            this.UpdateDirectoryTime(StringHelper.AsString(dirPath),
                                     newValue,
                                     _PATCH_TYPE_CREATIONTIME);
        }

        /// <summary>
        /// Updates the write time of a directory.
        /// </summary>
        /// <param name="dirPath">The path to the directory.</param>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dirPath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dirPath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="dirPath" /> does not exist.
        /// </exception>
        public void UpdateDirectoryWriteTime(IEnumerable<char> dirPath, DateTimeOffset? newValue)
        {
            this.UpdateDirectoryTime(StringHelper.AsString(dirPath),
                                     newValue,
                                     _PATCH_TYPE_WRITETIME);
        }

        /// <summary>
        /// Updates the creation time of a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// <paramref name="filePath" /> does not exist.
        /// </exception>
        public void UpdateFileCreationTime(IEnumerable<char> filePath, DateTimeOffset? newValue)
        {
            this.UpdateFileTime(StringHelper.AsString(filePath),
                                newValue,
                                _PATCH_TYPE_CREATIONTIME);
        }

        /// <summary>
        /// Updates the write time of a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// <paramref name="filePath" /> does not exist.
        /// </exception>
        public void UpdateFileWriteTime(IEnumerable<char> filePath, DateTimeOffset? newValue)
        {
            this.UpdateFileTime(StringHelper.AsString(filePath),
                                newValue,
                                _PATCH_TYPE_WRITETIME);
        }

        /// <summary>
        /// Uploads a file to this server.
        /// </summary>
        /// <param name="filePath">The full path where the file should be stored.</param>
        /// <param name="data">The data to upload.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> and/or <paramref name="data" />
        /// are <see langword="null" />.
        /// </exception>
        /// <exception cref="IOException">
        /// <paramref name="data" /> cannot be read.
        /// </exception>
        public void UploadFile(IEnumerable<char> filePath, Stream data)
        {
            string path = StringHelper.AsString(filePath);
            if (path == null)
            {
                throw new ArgumentNullException("filePath");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("filePath");
            }

            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (data.CanRead == false)
            {
                throw new IOException();
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri());
            httpRequest.Method = "PUT";
            httpRequest.SendChunked = true;

            if (path != null)
            {
                httpRequest.Headers["X-MJKTM-CloudNET-File"] = path;
            }

            using (Stream stream = httpRequest.GetRequestStream())
            {
                IOHelper.CopyTo(data, stream);

                stream.Close();
            }

            httpRequest.GetResponse().Close();
        }
        // Private Methods (2) 

        private void UpdateDirectoryTime(string path, DateTimeOffset? newValue, string type)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("path");
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri() + "?type=" + type);
            httpRequest.Method = "PATCH";
            httpRequest.Headers["X-MJKTM-CloudNET-Directory"] = path;
            httpRequest.Headers["X-MJKTM-CloudNET-DirectoryTime"] = newValue.HasValue ? newValue.Value.ToUniversalTime().ToString(_DATEFORMAT) : string.Empty;

            try
            {
                httpRequest.GetResponse().Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse resp = wex.Response as HttpWebResponse;
                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new DirectoryNotFoundException(path ?? string.Empty);
                    }
                }

                throw;
            }
        }

        private void UpdateFileTime(string path, DateTimeOffset? newValue, string type)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            path = path.Trim();
            if (path == string.Empty)
            {
                throw new ArgumentException("path");
            }

            HttpWebRequest httpRequest = this.Server.CreateHttpRequest(this.GetBaseUri() + "?type=" + type);
            httpRequest.Method = "PATCH";
            httpRequest.Headers["X-MJKTM-CloudNET-File"] = path;
            httpRequest.Headers["X-MJKTM-CloudNET-FileTime"] = newValue.HasValue ? newValue.Value.ToUniversalTime().ToString(_DATEFORMAT) : string.Empty;

            try
            {
                httpRequest.GetResponse().Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse resp = wex.Response as HttpWebResponse;
                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new FileNotFoundException(path ?? string.Empty);
                    }
                }

                throw;
            }
        }

        #endregion Methods
    }
}
