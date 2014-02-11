// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CloudNET.FileSync.Classes.Sessions
{
    internal sealed class SyncSession : TMObject
    {
        #region Fields (2)

        private readonly Queue<Action<ListViewItem>> _ACTION_QUEUE = new Queue<Action<ListViewItem>>();
        private const string _FORMAT_LOGDATE = "yyyy-MM-dd HH:mm:ss";

        #endregion Fields

        #region Constructors (1)

        internal SyncSession(CloudServer server, string dirToSync)
        {
            this.Server = server;
            this.DirectoryToSync = dirToSync;
        }

        #endregion Constructors

        #region Properties (5)

        internal string DirectoryToSync
        {
            get;
            private set;
        }

        internal FileSystemWatcher FileWatcher
        {
            get;
            private set;
        }

        internal bool IsRunning
        {
            get
            {
                var fw = this.FileWatcher;
                return fw != null &&
                       fw.EnableRaisingEvents;
            }
        }

        internal CloudServer Server
        {
            get;
            private set;
        }

        internal Timer Timer
        {
            get;
            private set;
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Events (1) 

        internal event EventHandler<SyncLogEventArgs> LogItemReceived;

        #endregion Delegates and Events

        #region Methods (12)

        // Private Methods (10) 

        private Action<ListViewItem> CreateFileChangedAction(FileSystemEventArgs e)
        {
            return new Action<ListViewItem>((logItem) =>
                {
                    var filePath = this.ToServerFilePath(e.FullPath);

                    using (var stream = File.OpenRead(e.FullPath))
                    {
                        this.Server
                            .UploadFile(filePath, stream);
                    }
                });
        }

        private Action<ListViewItem> CreateFileCreatedAction(FileSystemEventArgs e)
        {
            return new Action<ListViewItem>((logItem) =>
                {

                });
        }

        private Action<ListViewItem> CreateFileDeletedAction(FileSystemEventArgs e)
        {
            return new Action<ListViewItem>((logItem) =>
                {
                    var filePath = this.ToServerFilePath(e.FullPath);

                    this.Server
                        .DeleteFile(filePath);
                });
        }

        private Action<ListViewItem> CreateFileRenamedAction(RenamedEventArgs e)
        {
            return new Action<ListViewItem>((logItem) =>
                {
                    var oldFilePath = this.ToServerFilePath(e.OldFullPath);
                    var newFilePath = this.ToServerFilePath(e.FullPath);

                    try
                    {
                        this.Server
                            .DeleteFile(oldFilePath);
                    }
                    catch
                    {
                        //TODO log
                    }

                    using (var stream = File.OpenRead(e.FullPath))
                    {
                        this.Server
                            .UploadFile(newFilePath, stream);
                    }
                });
        }

        private void DisposeTimerAndWatcher()
        {
            try
            {
                using (var timer = this.Timer)
                {
                    if (timer != null)
                    {
                        timer.Stop();
                    }
                }
            }
            finally
            {
                using (var fw = this.FileWatcher)
                {
                    if (fw != null)
                    {
                        fw.EnableRaisingEvents = false;

                        this.FileWatcher = null;
                    }
                }
            }
        }

        private void FileWatcher_Change(object sender, FileSystemEventArgs e)
        {
            lock (this._SYNC)
            {
                try
                {
                    if (this.IsRunning == false)
                    {
                        return;
                    }

                    var fw = (FileSystemWatcher)sender;

                    Action<ListViewItem> actionToEnqueue = null;

                    if (File.Exists(e.FullPath))
                    {
                        var re = e as RenamedEventArgs;
                        if (re == null)
                        {
                            switch (e.ChangeType)
                            {
                                case WatcherChangeTypes.Changed:
                                    actionToEnqueue = this.CreateFileChangedAction(e);
                                    break;

                                case WatcherChangeTypes.Created:
                                    actionToEnqueue = this.CreateFileCreatedAction(e);
                                    break;

                                case WatcherChangeTypes.Deleted:
                                    actionToEnqueue = this.CreateFileDeletedAction(e);
                                    break;
                            }
                        }
                        else
                        {
                            // rename operation
                            actionToEnqueue = this.CreateFileRenamedAction(re);
                        }
                    }

                    if (actionToEnqueue != null)
                    {
                        this._ACTION_QUEUE.Enqueue(actionToEnqueue);
                    }
                }
                catch
                {
                    //TODO log
                }
            }
        }

        private void FileWatcher_Error(object sender, ErrorEventArgs e)
        {
            lock (this._SYNC)
            {
                try
                {
                    if (this.IsRunning == false)
                    {
                        return;
                    }

                    var fw = (FileSystemWatcher)sender;
                }
                catch
                {
                    //TODO log
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (this._SYNC)
            {
                ListViewItem logItem = null;

                try
                {
                    var timer = (Timer)sender;
                    try
                    {
                        timer.Stop();

                        while (this._ACTION_QUEUE.Count > 0)
                        {
                            try
                            {
                                logItem = new ListViewItem();
                                logItem.Text = DateTimeOffset.Now.ToString(_FORMAT_LOGDATE);
                                logItem.SubItems.Add(string.Empty);  // subject
                                logItem.SubItems.Add(string.Empty);  // message

                                var handler = this.LogItemReceived;
                                if (handler != null)
                                {
                                    handler(this, new SyncLogEventArgs(logItem));
                                }

                                var action = this._ACTION_QUEUE.Dequeue();
                                action(logItem);
                            }
                            catch
                            {
                                //TODO log
                            }
                        }
                    }
                    finally
                    {
                        timer.Start();
                    }
                }
                catch
                {
                    //TODO log
                }
            }
        }

        private string ToServerFilePath(string path)
        {
            var rootDir = Path.GetFullPath(this.DirectoryToSync ?? string.Empty).TrimEnd();
            if (rootDir.EndsWith("\\") == false)
            {
                rootDir += "\\";
            }

            path = Path.GetFullPath(path ?? string.Empty).TrimEnd();
            if (path.EndsWith("\\") == false)
            {
                path += "\\";
            }

            var result = Uri.UnescapeDataString(new Uri(rootDir).MakeRelativeUri(new Uri(path))
                                                                .ToString());

            while (result.EndsWith("/"))
            {
                result = result.Substring(0, result.Length - 1).Trim();
            }

            if (result.StartsWith("/") == false)
            {
                result = "/" + result;
            }

            return result;
        }

        private void UpdateLogItem(ListViewItem logItem,
                                   int? iconIndex = null,
                                   IEnumerable<char> subject = null,
                                   IEnumerable<char> msg = null)
        {
            if (logItem == null)
            {
                return;
            }

            try
            {
                logItem.ListView
                       .InvokeSafe((lv, state) =>
                       {
                           if (state.Icon.HasValue)
                           {
                               state.Item.ImageIndex = state.Icon.Value;
                           }

                           state.Item.Text = state.Now.ToString(_FORMAT_LOGDATE);

                           if (state.Subject != null)
                           {
                               state.Item.SubItems[0].Text = state.Subject.Trim();
                           }

                           if (state.Message != null)
                           {
                               state.Item.SubItems[1].Text = state.Message.Trim();
                           }
                       }, new
                       {
                           Icon = iconIndex,
                           Item = logItem,
                           Message = msg.AsString(),
                           Now = DateTimeOffset.Now,
                           Subject = subject.AsString(),
                       });
            }
            catch
            {
                // ignore errors here
            }
        }
        // Internal Methods (2) 

        internal void Start()
        {
            lock (this._SYNC)
            {
                if (this.IsRunning)
                {
                    return;
                }

                var errHandler = new ErrorEventHandler(this.FileWatcher_Error);
                var fwHandler = new FileSystemEventHandler(this.FileWatcher_Change);
                var renameHandler = new RenamedEventHandler(fwHandler);

                try
                {
                    this._ACTION_QUEUE.Clear();

                    this.FileWatcher = new FileSystemWatcher(this.DirectoryToSync);
                    this.FileWatcher.IncludeSubdirectories = true;
                    this.FileWatcher.Changed += fwHandler;
                    this.FileWatcher.Created += fwHandler;
                    this.FileWatcher.Deleted += fwHandler;
                    this.FileWatcher.Error += errHandler;
                    this.FileWatcher.Renamed += renameHandler;

                    this.Timer = new Timer();
                    this.Timer.Interval = 1500;
                    this.Timer.Tick += this.Timer_Tick;

                    this.FileWatcher.EnableRaisingEvents = true;
                    this.Timer.Start();
                }
                catch
                {
                    this.DisposeTimerAndWatcher();

                    throw;
                }
            }
        }

        internal void Stop()
        {
            lock (this._SYNC)
            {
                if (this.IsRunning == false)
                {
                    return;
                }

                this.DisposeTimerAndWatcher();

                this._ACTION_QUEUE.Clear();
            }
        }

        #endregion Methods
    }
}
