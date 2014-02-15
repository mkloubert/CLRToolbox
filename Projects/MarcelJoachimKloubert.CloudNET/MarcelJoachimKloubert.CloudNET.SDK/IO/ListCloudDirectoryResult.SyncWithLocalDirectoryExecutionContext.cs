// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    partial class ListCloudDirectoryResult
    {
        #region Nested Classes (1)

        private sealed class SyncWithLocalDirectoryExecutionContext : ISyncWithLocalDirectoryExecutionContext
        {
            #region Fields (6)

            private IList<Exception> _errors;
            private bool _isCancelling;
            private bool _isRunning;
            private string _localDirectory;
            private ListCloudDirectoryResult _remoteDirectory;
            private bool _syncRecursively;

            #endregion Fields

            #region Properties (6)

            public IList<Exception> Errors
            {
                get { return this._errors; }

                private set { this._errors = value; }
            }

            public bool IsCancelling
            {
                get { return this._isCancelling; }

                private set { this._isCancelling = value; }
            }

            public bool IsRunning
            {
                get { return this._isRunning; }

                private set { this._isRunning = value; }
            }

            public string LocalDirectory
            {
                get { return this._localDirectory; }

                internal set { this._localDirectory = value; }
            }

            public ListCloudDirectoryResult RemoteDirectory
            {
                get { return this._remoteDirectory; }

                internal set { this._remoteDirectory = value; }
            }

            public bool SyncRecursively
            {
                get { return this._syncRecursively; }

                internal set { this._syncRecursively = value; }
            }

            #endregion Properties

            #region Delegates and Events (2)

            // Events (2) 

            public event EventHandler Started;

            /// <summary>
            /// Is invoked if progress of <see cref="ListCloudDirectoryResult.SyncWithLocalDirectory(DirectoryInfo, bool)" /> method has been changed.
            /// </summary>
            public event SyncCloudDirectoryProgressEventHandler SyncProgress;

            #endregion Delegates and Events

            #region Methods (6)

            // Public Methods (3) 

            public void Cancel()
            {
                if (this.IsCancelling)
                {
                    return;
                }

                if (this.IsRunning)
                {
                    this.IsCancelling = true;
                }
            }

            public void CancelAndWait()
            {
                this.Cancel();
                while (this.IsCancelling)
                { }
            }

            public void Start()
            {
                try
                {
                    if (this.IsRunning)
                    {
                        return;
                    }

                    this.IsRunning = true;
                    this.Errors = null;

                    this.RaiseEventHandler(this.Started);

                    DirectoryInfo dir = new DirectoryInfo(this.LocalDirectory);

                    CloudDirectoryCollection remoteDirList = this.RemoteDirectory.Directories;
                    DirectoryInfo[] localDirList = dir.GetDirectories();

                    CloudFileCollection remoteFileList = this.RemoteDirectory.Files;
                    FileInfo[] localFileList = dir.GetFiles();

                    long allItemCount = remoteDirList.Count + localDirList.Length +
                                        remoteFileList.Count + localFileList.Length;
                    long currentItemIndex = -1;

                    Action<string> invokeNextProgressEvent =
                        delegate(string statusText)
                        {
                            double? progress;

                            ++currentItemIndex;
                            if (allItemCount < 1)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (double)currentItemIndex / (double)allItemCount;
                            }

                            this.OnLocalDirectorySyncProgress(progress, statusText);
                        };

                    // directories
                    if (remoteDirList != null)
                    {
                        // check for EXTRA directories
                        foreach (CloudDirectory remoteDir in remoteDirList)
                        {
                            if (this.IsCancelling)
                            {
                                this.OnCanceled();
                                return;
                            }

                            if (remoteDir == null)
                            {
                                invokeNextProgressEvent(null);
                                continue;
                            }

                            invokeNextProgressEvent(string.Format("Check if '{0}' is an EXTRA directory...",
                                                                  remoteDir.Path));

                            // try find matching local directory
                            string remoteDirName = (remoteDir.Name ?? string.Empty).ToLower().Trim();
                            DirectoryInfo matchingDir = CollectionHelper.FirstOrDefault(localDirList,
                                                                                        delegate(DirectoryInfo d)
                                                                                        {
                                                                                            return d.Name.ToLower().Trim() ==
                                                                                                   remoteDirName;
                                                                                        });

                            if (matchingDir == null)
                            {
                                // does not exist in source anymore
                                remoteDir.Delete();
                            }
                        }

                        // sync directories
                        foreach (DirectoryInfo localDir in localDirList)
                        {
                            if (this.IsCancelling)
                            {
                                this.OnCanceled();
                                return;
                            }

                            invokeNextProgressEvent(string.Format("Sync directory '{0}'...",
                                                                  localDir.Name));

                            string remotePath = (this.RemoteDirectory.Path ?? string.Empty).Trim();
                            if (remotePath.EndsWith("/") == false)
                            {
                                remotePath += "/";
                            }

                            remotePath += localDir.Name + "/";

                            // create directory if needed
                            {
                                bool doUpdateTimeStamps = false;

                                CloudDirectory matchingDir = remoteDirList[localDir.Name];
                                if (matchingDir == null)
                                {
                                    doUpdateTimeStamps = true;

                                    this.RemoteDirectory
                                        .CreateDirectory(localDir.Name);
                                }
                                else
                                {
                                    if (AreEqual(localDir.CreationTimeUtc, matchingDir.CreationTime) == false ||
                                        AreEqual(localDir.LastWriteTimeUtc, matchingDir.WriteTime) == false)
                                    {
                                        doUpdateTimeStamps = true;
                                    }
                                    else
                                    {

                                    }
                                }

                                // update timestamps
                                if (doUpdateTimeStamps)
                                {
                                    this.RemoteDirectory
                                        .Server
                                        .FileSystem
                                        .UpdateDirectoryCreationTime(remotePath, localDir.CreationTimeUtc);

                                    this.RemoteDirectory
                                        .Server
                                        .FileSystem
                                        .UpdateDirectoryWriteTime(remotePath, localDir.LastWriteTimeUtc);
                                }
                            }

                            if (this.SyncRecursively)
                            {
                                try
                                {
                                    this.RemoteDirectory
                                        .Server
                                        .FileSystem
                                        .ListDirectory(remotePath)
                                        .SyncWithLocalDirectory(localDir, true);
                                }
                                catch (WebException wex)
                                {
                                    bool rethrowException = true;

                                    HttpWebResponse resp = wex.Response as HttpWebResponse;
                                    if (resp != null)
                                    {
                                        if (resp.StatusCode == HttpStatusCode.NotFound)
                                        {
                                            // does not exist => needs to be created

                                            rethrowException = false;
                                            this.RemoteDirectory.CreateDirectory(dir.Name);
                                        }
                                    }

                                    if (rethrowException)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                    }

                    // files
                    if (remoteFileList != null)
                    {
                        // check for EXTRA files
                        foreach (CloudFile remoteFile in remoteFileList)
                        {
                            if (this.IsCancelling)
                            {
                                this.OnCanceled();
                                return;
                            }

                            if (remoteFile == null)
                            {
                                invokeNextProgressEvent(null);
                                continue;
                            }

                            invokeNextProgressEvent(string.Format("Check if '{0}' is an EXTRA file...",
                                                                  remoteFile.Path));
                            // try find matching local file
                            string remoteFileName = (remoteFile.Name ?? string.Empty).ToLower().Trim();
                            FileInfo matchingFile = CollectionHelper.FirstOrDefault(localFileList,
                                                                                    delegate(FileInfo f)
                                                                                    {
                                                                                        return f.Name.ToLower().Trim() ==
                                                                                                remoteFileName;
                                                                                    });

                            if (matchingFile == null)
                            {
                                // does not exist in source anymore
                                remoteFile.Delete();
                            }
                        }

                        // sync files
                        foreach (FileInfo localFile in localFileList)
                        {
                            if (this.IsCancelling)
                            {
                                this.OnCanceled();
                                return;
                            }

                            invokeNextProgressEvent(string.Format("Sync file '{0}'...",
                                                                  localFile.Name));

                            string remotePath = (this.RemoteDirectory.Path ?? string.Empty).Trim();
                            if (remotePath.EndsWith("/") == false)
                            {
                                remotePath += "/";
                            }

                            remotePath += localFile.Name;

                            try
                            {
                                bool doUploadFile = true;
                                bool doUpdateTimeStamps = false;

                                CloudFile matchingFile = remoteFileList[localFile.Name];
                                if (matchingFile != null)
                                {
                                    if (AreEqual(localFile.LastWriteTimeUtc, matchingFile.WriteTime) &&
                                        localFile.Length == matchingFile.Size)
                                    {
                                        // same file, no need to upload

                                        doUploadFile = false;
                                    }
                                    else
                                    {

                                    }

                                    if (AreEqual(localFile.CreationTimeUtc, matchingFile.CreationTime) == false ||
                                        AreEqual(localFile.LastWriteTimeUtc,  matchingFile.WriteTime) == false)
                                    {
                                        doUpdateTimeStamps = true;
                                    }
                                    else
                                    {

                                    }
                                }

                                if (doUploadFile)
                                {
                                    doUpdateTimeStamps = true;

                                    using (FileStream stream = localFile.OpenRead())
                                    {
                                        this.RemoteDirectory
                                            .UploadFile(localFile.Name,
                                                        stream);
                                    }
                                }

                                // update timestamps
                                if (doUpdateTimeStamps)
                                {
                                    this.RemoteDirectory
                                        .Server
                                        .FileSystem
                                        .UpdateFileCreationTime(remotePath, localFile.CreationTimeUtc);

                                    this.RemoteDirectory
                                        .Server
                                        .FileSystem
                                        .UpdateFileWriteTime(remotePath, localFile.LastWriteTimeUtc);
                                }
                            }
                            catch (WebException wex)
                            {
                                bool rethrowException = true;

                                HttpWebResponse resp = wex.Response as HttpWebResponse;
                                if (resp != null)
                                {
                                    if (resp.StatusCode == HttpStatusCode.NotFound)
                                    {
                                        // does not exist => needs to be created

                                        rethrowException = false;
                                        this.RemoteDirectory.CreateDirectory(dir.Name);
                                    }
                                }

                                if (rethrowException)
                                {
                                    throw;
                                }
                            }
                        }
                    }

                    this.OnLocalDirectorySyncProgress(1, "Synchronization completed");

                    this.Errors = new Exception[0];
                }
                catch (Exception ex)
                {
                    this.Errors = new Exception[] { ex };
                }
                finally
                {
                    this.IsRunning = false;
                    this.IsCancelling = false;
                }
            }
            // Private Methods (3) 

            private static bool AreEqual(DateTime? x, DateTime? y)
            {
                return NormalizeValue(x) == NormalizeValue(y);
            }

            private static DateTime? NormalizeValue(DateTime? input)
            {
                if (input.HasValue == false)
                {
                    return null;
                }

                return new DateTime(input.Value.Year, input.Value.Month, input.Value.Day,
                                    input.Value.Hour, input.Value.Minute, input.Value.Second,
                                    input.Value.Kind);
            }

            private void OnCanceled()
            {
                this.OnLocalDirectorySyncProgress(null,
                                                  "Canceled");
            }

            private bool OnLocalDirectorySyncProgress(double? progress,
                                                      string statusText)
            {
                SyncCloudDirectoryProgressEventHandler handler = this.SyncProgress;
                if (handler != null)
                {
                    handler(this,
                            new SyncCloudDirectoryProgressEventArgs(progress, statusText));

                    return true;
                }

                return false;
            }

            private bool RaiseEventHandler(EventHandler handler)
            {
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                    return true;
                }

                return false;
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
