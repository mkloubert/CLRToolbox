// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.FileSyncer.Diagnostics;
using MarcelJoachimKloubert.FileSyncer.Jobs.Actions;
using SyncJobActionQueue = System.Collections.Concurrent.ConcurrentQueue<MarcelJoachimKloubert.FileSyncer.Jobs.Actions.ISyncJobAction>;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    /// <summary>
    /// A sync job.
    /// </summary>
    public sealed class SyncJob : NotificationObjectBase,
                                  ISyncJob
    {
        #region Fields (6)

        private CancellationTokenSource _cancelSource;
        private string _progressDescription;
        private int? _progressValue;
        private SyncJobActionQueue _queue;
        private SyncJobState _state = SyncJobState.Stopped;
        private Task _task;

        #endregion Fields

        #region Properties (12)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanRestart" />
        public bool CanRestart
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanStart" />
        public bool CanStart
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.CanStop" />
        public bool CanStop
        {
            get { return true; }
        }

        /// <summary>
        /// Gets or sets the root destination directory.
        /// </summary>
        public string DestinationDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.DisplayName" />
        public string DisplayName
        {
            get;
            set;
        }

        internal Action InitalSyncAction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.IsRunning" />
        public bool IsRunning
        {
            get { return this.State == SyncJobState.Running; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.Name" />
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISyncJob.ProgressDescription" />
        public string ProgressDescription
        {
            get { return this._progressDescription; }

            private set { this.SetProperty(ref this._progressDescription, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISyncJob.ProgressValue" />
        public int? ProgressValue
        {
            get { return this._progressValue; }

            private set { this.SetProperty(ref this._progressValue, value); }
        }

        /// <summary>
        /// Gets or sets the root source directory.
        /// </summary>
        public string SourceDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISyncJob.State" />
        public SyncJobState State
        {
            get { return this._state; }

            private set { this.SetProperty(ref this._state, value); }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ISyncJob.ProgressChanged" />
        public event ProgressChangedEventHandler ProgressChanged;

        #endregion Delegates and Events

        #region Methods (25)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.GetDisplayName(CultureInfo)" />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return this.DisplayName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Restart()" />
        public void Restart()
        {
            this.Stop();
            this.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Start()" />
        public void Start()
        {
            lock (this._SYNC)
            {
                if (this.IsRunning)
                {
                    return;
                }

                this.DisposeOldTask();

                CancellationTokenSource newCancelSrc = null;
                Task newTask = null;
                try
                {
                    newCancelSrc = new CancellationTokenSource();

                    newTask = new Task(this.TaskAction,
                                       newCancelSrc.Token,
                                       TaskCreationOptions.LongRunning);

                    this.State = SyncJobState.Running;
                    newTask.Start();

                    this._cancelSource = newCancelSrc;
                    this._queue = new SyncJobActionQueue();
                    this._task = newTask;
                }
                catch
                {
                    this.DisposeOldTask();

                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.Stop()" />
        public void Stop()
        {
            lock (this._SYNC)
            {
                if (!this.IsRunning)
                {
                    return;
                }

                this.DisposeOldTask();
            }
        }
        // Private Methods (21) 

        private static void CompareDirectoriesSyncAction(DirectoryInfo src, DirectoryInfo dest,
                                                         ISyncJobExecutionContext execCtx)
        {
            CreateCompareDirectoriesTask(src: src, dest: dest,
                                         execCtx: execCtx,
                                         recursive: true).RunSynchronously();
        }

        private static Task CreateCompareDirectoriesTask(DirectoryInfo src, DirectoryInfo dest,
                                                         ISyncJobExecutionContext execCtx,
                                                         bool recursive)
        {
            return new Task((state) =>
                {
                    const string LOG_CATEGORY = "CompareDirectories";

                    TrySetThreadPriority(ThreadPriority.BelowNormal);

                    var ctx = (ISyncJobExecutionContext)state;

                    try
                    {
                        if (!NormalizePath(src).StartsWith(NormalizePath(ctx.SourceDirectory)))
                        {
                            // must be source or inside source directory
                            return;
                        }

                        if (!NormalizePath(dest).StartsWith(NormalizePath(ctx.DestionationDirectory)))
                        {
                            // must be destination or inside destination directory
                            return;
                        }

                        src.Refresh();
                        if (!src.Exists)
                        {
                            return;
                        }

                        var srcDisplayText = GetRelativePath(ctx.SourceDirectory, src.FullName);
                        if (string.IsNullOrWhiteSpace(srcDisplayText))
                        {
                            srcDisplayText = src.FullName;
                        }

                        var destDisplayText = GetRelativePath(ctx.DestionationDirectory, dest.FullName);
                        if (string.IsNullOrWhiteSpace(destDisplayText))
                        {
                            destDisplayText = dest.FullName;
                        }

                        ctx.RaiseProgressChanged(text: string.Format("Comparing '{0}' with '{1}'...",
                                                                     srcDisplayText,
                                                                     destDisplayText));

                        // EXTRA items
                        {
                            // files
                            if (!ctx.CancelToken.IsCancellationRequested)
                            {
                                dest.Refresh();

                                if (dest.Exists)
                                {
                                    var tasks = new List<Task>();

                                    foreach (var destFile in dest.GetFiles())
                                    {
                                        try
                                        {
                                            var srcFile = new FileInfo(Path.Combine(src.FullName,
                                                                                    destFile.Name));

                                            if (!srcFile.Exists)
                                            {
                                                tasks.Add(CreateDeleteFileTask(destFile,
                                                                               ctx));
                                            }
                                        }
                                        catch
                                        {
                                            //TODO log
                                        }
                                    }

                                    StartTasksAndWaitForAll(tasks,
                                                            ctx);
                                }
                            }

                            // directories
                            if (!ctx.CancelToken.IsCancellationRequested)
                            {
                                dest.Refresh();

                                if (dest.Exists)
                                {
                                    var tasks = new List<Task>();

                                    foreach (var destSubDir in dest.GetDirectories())
                                    {
                                        try
                                        {
                                            var srcSubDir = new DirectoryInfo(Path.Combine(src.FullName,
                                                                                           destSubDir.Name));

                                            if (!srcSubDir.Exists)
                                            {
                                                tasks.Add(CreateDelTreeTask(destSubDir,
                                                                            ctx));
                                            }
                                        }
                                        catch
                                        {
                                            //TODO log
                                        }
                                    }

                                    StartTasksAndWaitForAll(tasks,
                                                            ctx);
                                }
                            }
                        }

                        // copy items
                        {
                            // files
                            if (!ctx.CancelToken.IsCancellationRequested)
                            {
                                dest.Refresh();

                                var tasks = new List<Task>();

                                // copy files
                                foreach (var srcFile in src.GetFiles())
                                {
                                    try
                                    {
                                        var destFile = new FileInfo(Path.Combine(dest.FullName,
                                                                                 srcFile.Name));

                                        var copyFile = false;
                                        if (destFile.Exists)
                                        {
                                            copyFile = (srcFile.Length != destFile.Length) ||
                                                       (srcFile.LastWriteTimeUtc != destFile.LastWriteTimeUtc);
                                        }
                                        else
                                        {
                                            copyFile = true;
                                        }

                                        if (copyFile)
                                        {
                                            destFile.Directory
                                                    .CreateDirectoryDeep(refreshBefore: true,
                                                                         refreshAfter: true);

                                            tasks.Add(CreateCopyFileTask(srcFile, destFile,
                                                                         ctx));
                                        }
                                    }
                                    catch
                                    {
                                        //TODO log
                                    }
                                }

                                StartTasksAndWaitForAll(tasks,
                                                        ctx);
                            }

                            // sub directories
                            if (!ctx.CancelToken.IsCancellationRequested)
                            {
                                var tasks = new List<Task>();

                                foreach (var srcSubDir in src.GetDirectories())
                                {
                                    try
                                    {
                                        var destSubDir = new DirectoryInfo(Path.Combine(dest.FullName,
                                                                                        srcSubDir.Name));

                                        try
                                        {
                                            if (!destSubDir.Exists)
                                            {
                                                destSubDir.CreateDirectoryDeep(refreshBefore: false,
                                                                               refreshAfter: true);

                                                ctx.Log(msg: string.Format("Destionation directory '{0}' was created.",
                                                                           destSubDir.FullName),
                                                        tag: LOG_CATEGORY,
                                                        type: SyncLogType.OK);
                                            }
                                        }
                                        finally
                                        {
                                            tasks.Add(CreateCompareDirectoriesTask(srcSubDir, destSubDir,
                                                                                   ctx,
                                                                                   recursive: true));
                                        }
                                    }
                                    catch
                                    {
                                        //TODO log
                                    }
                                }

                                StartTasksAndWaitForAll(tasks,
                                                        ctx);
                            }
                        }

                        ctx.RaiseProgressChanged(text: "Done");
                    }
                    catch (Exception ex)
                    {
                        ctx.Log(msg: string.Format("Comparing directory '{0}' to '{1}' failed: {2}",
                                                   src.FullName,
                                                   dest.FullName,
                                                   ex.GetBaseException() ?? ex),
                                tag: LOG_CATEGORY,
                                type: SyncLogType.Error);
                    }
                    finally
                    {
                        TrySyncCreationTimes(src, dest);
                        TrySyncLastWriteTimes(src, dest);
                    }
                }, state: execCtx
                 , cancellationToken: execCtx.CancelToken
                 , creationOptions: TaskCreationOptions.LongRunning);
        }

        private static Task CreateCopyFileTask(FileInfo src, FileInfo dest,
                                               ISyncJobExecutionContext execCtx)
        {
            return new Task((state) =>
                {
                    const string LOG_CATEGORY = "CopyFile";

                    TrySetThreadPriority(ThreadPriority.BelowNormal);

                    var ctx = (ISyncJobExecutionContext)state;

                    try
                    {
                        src.Refresh();
                        if (!src.Exists)
                        {
                            return;
                        }

                        var destDir = dest.Directory;
                        destDir.Refresh();
                        if (!destDir.Exists)
                        {
                            return;
                        }

                        dest.Refresh();

                        File.Copy(src.FullName,
                                  dest.FullName,
                                  true);

                        ctx.Log(msg: string.Format("File '{0}' was copied to '{1}'.",
                                                   src.FullName,
                                                   dest.Directory.FullName),
                                            tag: LOG_CATEGORY,
                                            type: SyncLogType.OK);
                    }
                    catch (Exception ex)
                    {
                        ctx.Log(msg: string.Format("Copying file '{0}' to '{1}' failed: {2}",
                                                   src.FullName,
                                                   dest.Directory.FullName,
                                                   ex.GetBaseException() ?? ex),
                                tag: LOG_CATEGORY,
                                type: SyncLogType.Error);
                    }
                    finally
                    {
                        TrySyncCreationTimes(src, dest);
                        TrySyncLastWriteTimes(src, dest);
                    }
                }, state: execCtx
                 , cancellationToken: execCtx.CancelToken
                 , creationOptions: TaskCreationOptions.LongRunning);
        }

        private static Task CreateDeleteFileTask(FileInfo file,
                                                 ISyncJobExecutionContext execCtx)
        {
            return new Task((state) =>
                {
                    const string LOG_CATEGORY = "CopyFile";

                    TrySetThreadPriority(ThreadPriority.BelowNormal);

                    var ctx = (ISyncJobExecutionContext)state;
                    if (ctx.CancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    try
                    {
                        file.Refresh();
                        if (file.Exists)
                        {
                            file.Delete();
                            file.Refresh();

                            ctx.Log(msg: string.Format("File '{0}' was deleted.",
                                                       file.FullName),
                                            tag: LOG_CATEGORY,
                                            type: SyncLogType.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        ctx.Log(msg: string.Format("Deleting file '{0}' failed: {1}",
                                                   file.FullName,
                                                   ex.GetBaseException() ?? ex),
                                tag: LOG_CATEGORY,
                                type: SyncLogType.Error);
                    }
                }, state: execCtx
                 , cancellationToken: execCtx.CancelToken);
        }

        private static Task CreateDelTreeTask(DirectoryInfo dir,
                                              ISyncJobExecutionContext execCtx)
        {
            return new Task((state) =>
                {
                    const string LOG_CATEGORY = "DelTree";

                    TrySetThreadPriority(ThreadPriority.BelowNormal);

                    var ctx = (ISyncJobExecutionContext)state;

                    try
                    {
                        dir.Refresh();

                        // files
                        StartTasksAndWaitForAll(dir.GetFiles()
                                                   .Select(f => CreateDeleteFileTask(f, ctx)),
                                                ctx);

                        // directories
                        StartTasksAndWaitForAll(dir.GetDirectories()
                                                   .Select(d => CreateDelTreeTask(d, ctx)),
                                                ctx);

                        if (!ctx.CancelToken.IsCancellationRequested)
                        {
                            dir.Refresh();

                            if (NormalizePath(dir) == NormalizePath(ctx.SourceDirectory) ||
                                NormalizePath(dir) == NormalizePath(ctx.DestionationDirectory))
                            {
                                // do not delete source or destination directory

                                return;
                            }

                            if (dir.GetFiles().IsEmpty() &&
                                dir.GetDirectories().IsEmpty())
                            {
                                if (dir.Exists)
                                {
                                    dir.Delete();
                                    dir.Refresh();

                                    ctx.Log(msg: string.Format("Directory '{0}' was deleted.",
                                                               dir.FullName),
                                            tag: LOG_CATEGORY,
                                            type: SyncLogType.OK);
                                }
                            }
                            else
                            {
                                ctx.Log(msg: string.Format("Directory '{0}' is NOT empty!",
                                                           dir.FullName),
                                        tag: LOG_CATEGORY,
                                        type: SyncLogType.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ctx.Log(msg: string.Format("Deleting directory structure '{0}' failed: {1}",
                                                   dir.FullName,
                                                   ex.GetBaseException() ?? ex),
                                tag: LOG_CATEGORY,
                                type: SyncLogType.Error);
                    }
                }, state: execCtx
                 , cancellationToken: execCtx.CancelToken
                 , creationOptions: TaskCreationOptions.LongRunning);
        }

        private FileSystemEventHandler CreateFileSystemEventHandlerForSource(ISyncJobExecutionContext ctx)
        {
            return new FileSystemEventHandler((sender, e) =>
                {
                    lock (ctx.SyncRoot)
                    {
                        try
                        {
                            var watchedDir = Path.GetDirectoryName(e.FullPath);
                            var normalizedWatchedDir = ToComparablePath(watchedDir);

                            var action = ctx.Queue
                                            .OrderBy(a => ToComparablePath(a.Tag.AsString(true)), StringComparer.InvariantCultureIgnoreCase)
                                            .FirstOrDefault(a => normalizedWatchedDir.Equals(a.Tag));
                            if (action != null)
                            {
                                return;
                            }

                            var relativeSrcPath = GetRelativePath(ctx.SourceDirectory,
                                                                  watchedDir);

                            action = new DelegateSyncAction(action: CompareDirectoriesSyncAction,
                                                            src: new DirectoryInfo(watchedDir),
                                                            dest: new DirectoryInfo(Path.Combine(ctx.DestionationDirectory,
                                                                                                 relativeSrcPath)));
                            action.Tag = normalizedWatchedDir;

                            ctx.Queue
                               .Enqueue(action);
                        }
                        catch (Exception ex)
                        {
                            ctx.Log(msg: string.Format("Sync failed: {0}",
                                                       ex.GetBaseException() ?? ex),
                                    tag: "FileSystemEvent",
                                    type: SyncLogType.Error);
                        }
                    }
                });
        }

        private static void DisposeFileSystemWatcher(FileSystemWatcher watcher,
                                                     FileSystemEventHandler handler,
                                                     RenamedEventHandler renameHandler,
                                                     ErrorEventHandler errorHandler)
        {
            if (handler != null)
            {
                watcher.Changed -= handler;
                watcher.Created -= handler;
                watcher.Deleted -= handler;
            }

            if (renameHandler != null)
            {
                watcher.Renamed -= renameHandler;
            }

            if (errorHandler != null)
            {
                watcher.Error -= errorHandler;
            }
        }

        private void DisposeOldTask()
        {
            using (var t = this._task)
            {
                if (t != null)
                {
                    var cs = this._cancelSource;
                    if (cs != null)
                    {
                        cs.Cancel();
                    }

                    t.Wait();
                }
            }

            this._queue = null;
            this._task = null;
            this._cancelSource = null;
            this.State = SyncJobState.Stopped;
        }

        private static string GetRelativePath(string rootDir, string path)
        {
            rootDir = Path.GetFullPath(rootDir ?? string.Empty).TrimEnd();
            if (!rootDir.EndsWith("\\"))
            {
                rootDir += "\\";
            }

            path = Path.GetFullPath(path ?? string.Empty).TrimEnd();
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }

            return Uri.UnescapeDataString(new Uri(rootDir).MakeRelativeUri(new Uri(path))
                                                          .ToString()
                                                          .Replace('/',
                                                                   Path.DirectorySeparatorChar));
        }

        private void HandleSyncActions(FileSystemWatcher watcher,
                                       ISyncJobExecutionContext ctx)
        {
            while (!ctx.CancelSource.IsCancellationRequested)
            {
                try
                {
                    ISyncJobAction action;
                    if (ctx.Queue.TryDequeue(out action))
                    {
                        action.Execute(ctx);
                    }
                }
                catch (Exception ex)
                {
                    ctx.Log(msg: string.Format("Handling of sync action failed: {0}",
                                                ex.GetBaseException() ?? ex),
                            tag: "HandleSyncAction",
                            type: SyncLogType.Error);
                }
            }
        }

        private void Log(DateTimeOffset time,
                         SyncLogType? type,
                         string tag,
                         object msg)
        {
            //TODO
        }

        private static string NormalizePath(IEnumerable<char> fs)
        {
            var fullName = fs.AsString();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return string.Empty;
            }

            fullName = Path.GetFullPath(fullName.ToLower())
                           .Replace("/", "\\")
                           .Trim();

            while (fullName.EndsWith("\\"))
            {
                fullName = fullName.Substring(0,
                                              fullName.Length - 1)
                                   .Trim();
            }

            return fullName;
        }

        private static string NormalizePath(FileSystemInfo fs)
        {
            return NormalizePath(fs != null ? fs.FullName : null);
        }

        /// <summary>
        /// Raises the <see cref="SyncJob.ProgressChanged" /> event.
        /// </summary>
        /// <param name="text">The status text.</param>
        /// <param name="percentage">A value between -1 (no progress) and 100 (100 %).</param>
        private void OnProgressChanged(IEnumerable<char> text = null,
                                       int? percentage = null)
        {
            var statusText = (text.AsString() ?? string.Empty);
            if (statusText == string.Empty)
            {
                statusText = null;
            }

            if (percentage < 0)
            {
                percentage = null;
            }
            else if (percentage > 100)
            {
                percentage = 100;
            }

            this.ProgressValue = percentage;
            this.ProgressDescription = statusText;

            var handler = this.ProgressChanged;
            if (handler != null)
            {
                handler(this, new ProgressChangedEventArgs(progressPercentage: percentage ?? -1,
                                                           userState: statusText));
            }
        }

        private static void SetupFileSystemWatcher(FileSystemWatcher watcher,
                                                   FileSystemEventHandler handler,
                                                   RenamedEventHandler renameHandler,
                                                   ErrorEventHandler errorHandler)
        {
            watcher.Changed += handler;
            watcher.Created += handler;
            watcher.Deleted += handler;
            watcher.Error += errorHandler;
            watcher.Renamed += renameHandler;
        }

        private static void StartTasksAndWaitForAll(IEnumerable<Task> tasks,
                                                    ISyncJobExecutionContext ctx)
        {
            var runningTasks = new List<Task>();
            foreach (var t in tasks)
            {
                try
                {
                    if (ctx.CancelToken.IsCancellationRequested)
                    {
                        break;
                    }

                    t.Start();
                    runningTasks.Add(t);
                }
                catch
                {
                    //TODO log
                }
            }

            TaskHelper.WaitAll(runningTasks);
        }

        private void TaskAction()
        {
            TrySetThreadPriority(ThreadPriority.BelowNormal);

            var cancelSrc = this._cancelSource;
            if (cancelSrc == null)
            {
                return;
            }

            var queue = this._queue;
            if (queue == null)
            {
                return;
            }

            var ctx = new SyncJobExeuctionContext()
                {
                    CancelSource = cancelSrc,
                    DestionationDirectory = Path.GetFullPath(this.DestinationDirectory),
                    Job = this,
                    LogAction = this.Log,
                    ProgressChangedHandler = this.OnProgressChanged,
                    Queue = queue,
                    SourceDirectory = Path.GetFullPath(this.SourceDirectory),
                    SyncRoot = new object(),
                };

            bool retry;
            do
            {
                retry = false;

                try
                {
                    this.State = SyncJobState.Running;

                    // now 
                    using (var srcWatcher = new FileSystemWatcher())
                    {
                        FileSystemEventHandler srcFsHandler = null;
                        RenamedEventHandler srcRenameHandler = null;
                        ErrorEventHandler srcErrHandler = null;

                        try
                        {
                            // setup 'srcWatcher'
                            {
                                srcFsHandler = this.CreateFileSystemEventHandlerForSource(ctx);
                                srcRenameHandler = new RenamedEventHandler(srcFsHandler);

                                srcErrHandler = (sender, e) =>
                                    {
                                        retry = true;
                                    };

                                SetupFileSystemWatcher(watcher: srcWatcher,
                                                       handler: srcFsHandler,
                                                       renameHandler: srcRenameHandler,
                                                       errorHandler: srcErrHandler);

                                srcWatcher.Path = ctx.SourceDirectory;
                                srcWatcher.IncludeSubdirectories = true;
                            }

                            Action initalSync;
                            this.InitalSyncAction = initalSync = () =>
                                {
                                    ctx.RaiseProgressChanged(text: "Start inital sync...");

                                    srcFsHandler(srcWatcher,
                                                 new FileSystemEventArgs(WatcherChangeTypes.Changed,
                                                                         srcWatcher.Path,
                                                                         null));
                                };

                            lock (ctx.SyncRoot)
                            {
                                srcWatcher.EnableRaisingEvents = true;

                                initalSync();
                            }

                            // now watch for sync actions
                            this.HandleSyncActions(srcWatcher,
                                                   ctx);
                        }
                        finally
                        {
                            lock (ctx.SyncRoot)
                            {
                                srcWatcher.EnableRaisingEvents = false;
                            }

                            DisposeFileSystemWatcher(watcher: srcWatcher,
                                                     handler: srcFsHandler,
                                                     renameHandler: srcRenameHandler,
                                                     errorHandler: srcErrHandler);
                        }
                    }

                    if (ctx.CancelSource.IsCancellationRequested)
                    {
                        this.State = SyncJobState.Canceled;
                    }
                    else
                    {
                        this.State = SyncJobState.Finished;
                    }
                }
                catch
                {
                    this.State = SyncJobState.Faulted;
                }
                finally
                {
                    this.InitalSyncAction = null;

                    if (retry)
                    {
                        cancelSrc.Cancel();
                    }
                }
            }
            while (retry);
        }

        private static string ToComparablePath(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            return Path.GetFullPath(input)
                       .ToLower()
                       .Trim();
        }

        private static bool TrySetThreadPriority(ThreadPriority prio)
        {
            try
            {
                Thread.CurrentThread.Priority = prio;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool? TrySyncCreationTimes(FileSystemInfo src, FileSystemInfo dest)
        {
            try
            {
                dest.Refresh();
                if (dest.Exists)
                {
                    dest.CreationTimeUtc = src.CreationTimeUtc;
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return null;
        }

        private static bool? TrySyncLastWriteTimes(FileSystemInfo src, FileSystemInfo dest)
        {
            try
            {
                dest.Refresh();
                if (dest.Exists)
                {
                    dest.LastWriteTimeUtc = src.LastWriteTimeUtc;
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return null;
        }

        #endregion Methods
    }
}
