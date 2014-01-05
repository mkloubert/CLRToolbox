// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.FileSyncer.Jobs.Actions;
using SyncJobActionQueue = System.Collections.Concurrent.ConcurrentQueue<MarcelJoachimKloubert.FileSyncer.Jobs.Actions.ISyncJobAction>;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    /// <summary>
    /// A sync job.
    /// </summary>
    public sealed class SyncJob : TMObject, ISyncJob
    {
        #region Fields (3)

        private CancellationTokenSource _cancelSource;
        private SyncJobActionQueue _queue;
        private Task _task;

        #endregion Fields

        #region Properties (8)

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
        public string DestionationDirectory
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

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IRunnable.IsRunning" />
        public bool IsRunning
        {
            get
            {
                var t = this._task;

                return t != null &&
                       t.Status == TaskStatus.Running;
            }
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
        /// Gets or sets the root source directory.
        /// </summary>
        public string SourceDirectory
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (18)

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
        // Private Methods (14) 

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
                    }
                    catch
                    {
                        //TODO log
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

        [DebuggerStepThrough]
        private static Task CreateCopyFileTask(FileInfo src, FileInfo dest,
                                               ISyncJobExecutionContext execCtx)
        {
            return new Task((state) =>
                {
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
                    }
                    catch
                    {
                        //TODO log
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
                    var ctx = (ISyncJobExecutionContext)state;

                    if (ctx.CancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    file.Refresh();
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }, state: execCtx
                 , cancellationToken: execCtx.CancelToken
                 , creationOptions: TaskCreationOptions.LongRunning);
        }

        private static Task CreateDelTreeTask(DirectoryInfo dir,
                                              ISyncJobExecutionContext execCtx)
        {
            return new Task((state) =>
                {
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
                                }
                            }
                            else
                            {
                                //TODO warn message that directory is NOT empty
                            }
                        }
                    }
                    catch
                    {
                        //TODO log
                    }
                }, state: execCtx
                 , cancellationToken: execCtx.CancelToken
                 , creationOptions: TaskCreationOptions.LongRunning);
        }

        private FileSystemEventHandler CreateFileSystemEventHandler(ISyncJobExecutionContext ctx)
        {
            return new FileSystemEventHandler((sender, e) =>
                {
                    lock (ctx.SyncRoot)
                    {
                        try
                        {
                            var watchedDir = Path.GetDirectoryName(e.FullPath);
                            if (!watchedDir.EndsWith("\\"))
                            {
                                watchedDir += "\\";
                            }

                            ISyncJobAction action;
                            if (!ctx.Queue.TryPeek(out action) ||
                                (watchedDir.ToLower().Trim() != (action.Tag.AsString(true) ?? string.Empty).ToLower().Trim()))
                            {
                                var watchedUri = new Uri(watchedDir);

                                var srcPath = ctx.SourceDirectory.TrimEnd();
                                if (!srcPath.EndsWith("\\"))
                                {
                                    srcPath += "\\";
                                }

                                var srcUri = new Uri(srcPath);
                                var relativeSrcPath = Uri.UnescapeDataString(
                                    watchedUri.MakeRelativeUri(srcUri)
                                              .ToString()
                                              .Replace('/', Path.DirectorySeparatorChar));

                                var newAction = new DelegateSyncAction(action: CompareDirectoriesSyncAction,
                                                                       src: new DirectoryInfo(watchedDir),
                                                                       dest: new DirectoryInfo(Path.Combine(ctx.DestionationDirectory,
                                                                                                            relativeSrcPath)));
                                newAction.Tag = watchedDir.ToLower().Trim();

                                ctx.Queue
                                   .Enqueue(newAction);
                            }
                        }
                        catch
                        {
                            //TODO log
                        }
                    }
                });
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
                catch
                {
                    //TODO log
                }
            }
        }

        private static string NormalizePath(IEnumerable<char> fs)
        {
            var fullName = fs.AsString();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return null;
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
            if (fs == null)
            {
                return null;
            }

            return NormalizePath(fs.FullName);
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
                    DestionationDirectory = Path.GetFullPath(this.DestionationDirectory),
                    Job = this,
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
                    // now 
                    using (var watcher = new FileSystemWatcher())
                    {
                        FileSystemEventHandler handler = null;
                        RenamedEventHandler renameHandler = null;
                        ErrorEventHandler errorHandler = null;
                        try
                        {
                            handler = this.CreateFileSystemEventHandler(ctx);
                            renameHandler = new RenamedEventHandler(handler);

                            errorHandler = (sender, e) =>
                                {
                                    retry = true;
                                };

                            watcher.Changed += handler;
                            watcher.Created += handler;
                            watcher.Deleted += handler;
                            watcher.Error += errorHandler;
                            watcher.Renamed += renameHandler;

                            watcher.Path = ctx.SourceDirectory;
                            watcher.IncludeSubdirectories = true;

                            lock (ctx.SyncRoot)
                            {
                                watcher.EnableRaisingEvents = true;

                                handler(watcher,
                                        new FileSystemEventArgs(WatcherChangeTypes.Changed,
                                                                watcher.Path,
                                                                null));
                            }

                            // now watch for sync actions
                            this.HandleSyncActions(watcher,
                                                   ctx);
                        }
                        finally
                        {
                            watcher.EnableRaisingEvents = false;

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
                    }
                }
                catch
                {
                    //TODO log
                }
                finally
                {
                    if (retry)
                    {
                        cancelSrc.Cancel();
                    }
                }
            }
            while (retry);
        }

        [DebuggerStepThrough]
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

        [DebuggerStepThrough]
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
