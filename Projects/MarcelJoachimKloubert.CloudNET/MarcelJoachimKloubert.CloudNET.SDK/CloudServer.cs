// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using MarcelJoachimKloubert.CloudNET.SDK.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MarcelJoachimKloubert.CloudNET.SDK
{
    /// <summary>
    /// Handles a connection to a cloud server.
    /// </summary>
    public sealed class CloudServer
    {
        #region Properties (4)

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        public ICredentials Credentials
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the host address.
        /// </summary>
        public string HostAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if HTTPs should be used or not.
        /// </summary>
        public bool IsSecure
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the TCP port.
        /// </summary>
        public int? Port
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (9) 

        /// <summary>
        /// Deletes a file on this server.
        /// </summary>
        /// <param name="filePath">The full path where the file should be stored.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="filePath" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filePath" /> is <see langword="null" />.
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

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(this.GetFilesUri());
            SetupRequestWirhCredentials(httpRequest);
            httpRequest.Method = "DELETE";
            httpRequest.AllowAutoRedirect = false;
            httpRequest.Headers["X-MJKTM-CloudNET-File"] = path;

            httpRequest.GetResponse().Close();
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

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(this.GetFilesUri());
            SetupRequestWirhCredentials(httpRequest);
            httpRequest.Method = "GET";
            httpRequest.AllowAutoRedirect = false;

            if (path != null)
            {
                httpRequest.Headers["X-MJKTM-CloudNET-File"] = path;
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (Stream stream = httpResponse.GetResponseStream())
            {
                IOHelper.CopyTo(stream, target);
            }
        }

        /// <summary>
        /// Returns the base URI.
        /// </summary>
        /// <returns>the base URI.</returns>
        public string GetBaseUri()
        {
            bool isHttps = this.IsSecure;

            int defPort;
            if (isHttps)
            {
                defPort = 443;
            }
            else
            {
                defPort = 80;
            }

            return string.Format("http{0}://{1}:{2}/",
                                 isHttps ? "s" : string.Empty,
                                 this.HostAddress,
                                 this.Port ?? defPort);
        }

        /// <summary>
        /// Returns the base URI for handling files.
        /// </summary>
        /// <returns>the base URI.</returns>
        public string GetFilesUri()
        {
            return string.Format("{0}files",
                                 this.GetBaseUri());
        }

        /// <summary>
        /// Tries to return the value of <see cref="CloudServer.Credentials" /> as <see cref="NetworkCredential" /> object. 
        /// </summary>
        /// <returns>
        /// <see cref="CloudServer.Credentials" /> as <see cref="NetworkCredential" /> object.
        /// </returns>
        public NetworkCredential GetNetworkCredentials()
        {
            NetworkCredential result = null;

            ICredentials cred = this.Credentials;
            if (cred != null)
            {
                result = cred as NetworkCredential;
                if (result == null)
                {
                    result = cred.GetCredential(null, null);
                }
            }

            return result;
        }

        /// <summary>
        /// Lists a directory.
        /// </summary>
        /// <param name="dir">The path of the directory.</param>
        /// <returns>The result.</returns>
        public ListCloudDirectoryResult ListDirectory(IEnumerable<char> dir)
        {
            string path = (StringHelper.AsString(dir) ?? string.Empty).Trim();
            if (path == string.Empty)
            {
                path = null;
            }

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(this.GetFilesUri() + "?action=list");
            SetupRequestWirhCredentials(httpRequest);
            httpRequest.Method = "GET";
            httpRequest.AllowAutoRedirect = false;

            if (path != null)
            {
                httpRequest.Headers["X-MJKTM-CloudNET-Directory"] = path;
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
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
                                result.Server = this;

                                IEnumerable<CloudFile> files = result.Files ?? new CloudFile[0];
                                foreach (CloudFile f in files)
                                {
                                    if (f == null)
                                    {
                                        continue;
                                    }

                                    f.Server = this;
                                }

                                IEnumerable<CloudDirectory> dirs = result.Directories ?? new CloudDirectory[0];
                                foreach (CloudDirectory d in dirs)
                                {
                                    if (d == null)
                                    {
                                        continue;
                                    }

                                    d.Server = this;
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

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(this.GetFilesUri());
            SetupRequestWirhCredentials(httpRequest);
            httpRequest.Method = "PUT";
            httpRequest.AllowAutoRedirect = false;
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
        // Private Methods (1) 

        private void SetupRequestWirhCredentials(HttpWebRequest httpRequest)
        {
            NetworkCredential cred = this.GetNetworkCredentials();
            if (cred == null)
            {
                return;
            }

            string user = (cred.UserName ?? string.Empty).Trim();
            if (StringHelper.IsNullOrWhitespace(cred.Domain) == false)
            {
                user = cred.Domain.Trim() + "\\" + user;
            }

            string authInfo = string.Format("{0}:{1}",
                                            user,
                                            StringHelper.AsString(cred.Password));

            httpRequest.Headers["Authorization"] = string.Format("Basic {0}",
                                                                 Convert.ToBase64String(Encoding.ASCII
                                                                                                .GetBytes(authInfo)));
        }

        #endregion Methods
    }
}
