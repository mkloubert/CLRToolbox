// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Data;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.IO;
using MarcelJoachimKloubert.CLRToolbox.Security.Cryptography;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskTuple = System.Tuple<MarcelJoachimKloubert.FileCompare.WPF.Classes.CompareTask, System.Threading.CancellationTokenSource>;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// A compare task.
    /// </summary>
    public sealed class CompareTask : NotificationObjectBase, IHasName, IRunnable
    {
        #region Fields (11)

        private CancellationTokenSource _cancelSource;
        private const string _CONFIG_NAME_DEST = "destination";
        private const string _CONFIG_NAME_HASH = "hash";
        private const string _CONFIG_NAME_IS_ACTIVE = "is_active";
        private const string _CONFIG_NAME_RECURSIVE = "recursive";
        private const string _CONFIG_NAME_SOURCE = "source";
        private const string _CONFIG_NAME_TASKNAME = "name";
        private const string _FORMAT_FILETIME = "yyyy-MM-dd HH:mm:ss";
        private bool _isRunning;
        private readonly AggregateLogger _LOGGER;
        private CompareProgress _progress;

        #endregion Fields

        #region Constructors (1)

        private CompareTask()
        {
            this._LOGGER = new AggregateLogger();
        }

        #endregion Constructors

        #region Properties (17)

        /// <inheriteddoc />
        public bool CanRestart
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public bool CanStart
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public bool CanStop
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the path of the destination directory.
        /// </summary>
        public string Destination
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets, if defined, the type of the hash algorithm to use.
        /// </summary>
        public Type Hash
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the task is active or not.
        /// </summary>
        public bool IsActive
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public bool IsRunning
        {
            get { return this._isRunning; }

            private set { this.SetProperty(ref this._isRunning, value); }
        }

        /// <inheriteddoc />
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current progress context.
        /// </summary>
        public CompareProgress Progress
        {
            get { return this._progress; }

            private set { this.SetProperty(ref this._progress, value); }
        }

        /// <summary>
        /// Gets if <see cref="CompareTask.Source" /> and <see cref="CompareTask.Destination" /> should be compared recursivly or not.
        /// </summary>
        public bool Recursive
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of results.
        /// </summary>
        public SynchronizedObservableCollection<ICompareResult> Results
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command that saves a result list to a file.
        /// </summary>
        public SimpleCommand SaveResultCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the path of the source directory.
        /// </summary>
        public string Source
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to start that task.
        /// </summary>
        public SimpleCommand StartCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to stop that task.
        /// </summary>
        public SimpleCommand StopCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current system task.
        /// </summary>
        public Task Task
        {
            get;
            private set;
        }

        #endregion Properties

        #region Delegates and Events (4)

        // Events (4) 

        /// <summary>
        /// Is invoked when two items are compared.
        /// </summary>
        public event EventHandler<CompareFileSystemItemsEventArgs> ComparingItems;

        /// <summary>
        /// Is invoked if different items were found.
        /// </summary>
        public event EventHandler<FoundDifferentFileSystemItemsEventArgs> DifferentItemsFound;

        /// <summary>
        /// Is invoked after the task has been started.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Is invoked after the task has been stopped.
        /// </summary>
        public event EventHandler Stopped;

        #endregion Delegates and Events

        #region Methods (16)

        // Public Methods (7) 

        /// <summary>
        /// Creates a system task.
        /// </summary>
        /// <returns>The created task.</returns>
        public Task CreateTask()
        {
            CancellationTokenSource cancelTokenSrc;
            return this.CreateTask(out cancelTokenSrc);
        }

        /// <summary>
        /// Creates a system task.
        /// </summary>
        /// <param name="cancelTokenSrc">The variable where to write the source object, that is able to cancel the operation, to.</param>
        /// <returns>The created task.</returns>
        public Task CreateTask(out CancellationTokenSource cancelTokenSrc)
        {
            cancelTokenSrc = new CancellationTokenSource();

            return new Task(TaskAction,
                            state: new TaskTuple(this, cancelTokenSrc),
                            cancellationToken: cancelTokenSrc.Token,
                            creationOptions: TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Loads tasks from a config repository.
        /// </summary>
        /// <param name="config">The config repository.</param>
        /// <returns>The loaded data.</returns>
        public static IEnumerable<CompareTask> FromConfig(IConfigRepository config,
                                                          bool activeOnly = true)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            foreach (var taskName in config.GetCategoryNames())
            {
                CompareTask newTask;
                try
                {
                    bool isActive;
                    config.TryGetValue<bool>(category: taskName,
                                             name: _CONFIG_NAME_IS_ACTIVE,
                                             value: out isActive,
                                             defaultVal: true);

                    if (activeOnly &&
                        isActive == false)
                    {
                        continue;
                    }

                    string displayName;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_TASKNAME,
                                               value: out displayName);

                    string source;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_SOURCE,
                                               value: out source);

                    string destination;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_DEST,
                                               value: out destination);

                    bool recursive;
                    config.TryGetValue<bool>(category: taskName,
                                             name: _CONFIG_NAME_RECURSIVE,
                                             value: out recursive,
                                             defaultVal: false);

                    string hash;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_HASH,
                                               value: out hash);

                    newTask = new CompareTask();
                    newTask.Destination = destination;
                    newTask.DisplayName = string.IsNullOrWhiteSpace(displayName) ? taskName : displayName.Trim();
                    newTask.IsActive = isActive;
                    newTask.Name = taskName;
                    newTask.Recursive = recursive;
                    newTask.Source = source;

                    switch ((hash ?? string.Empty).ToLower().Trim())
                    {
                        case "":
                            break;

                        case "crc":
                        case "crc32":
                        case "crc-32":
                            newTask.Hash = typeof(Crc32);
                            break;

                        case "md5":
                        case "md-5":
                            newTask.Hash = typeof(MD5CryptoServiceProvider);
                            break;

                        case "sha1":
                        case "sha-1":
                            newTask.Hash = typeof(SHA1CryptoServiceProvider);
                            break;

                        case "sha256":
                        case "sha-256":
                            newTask.Hash = typeof(SHA256CryptoServiceProvider);
                            break;

                        case "sha384":
                        case "sha-384":
                            newTask.Hash = typeof(SHA384CryptoServiceProvider);
                            break;

                        case "sha512":
                        case "sha-512":
                            newTask.Hash = typeof(SHA512CryptoServiceProvider);
                            break;

                        default:
                            throw new NotSupportedException(hash);
                    }
                }
                catch
                {
                    newTask = null;
                }

                if (newTask != null)
                {
                    yield return newTask;
                }
            }
        }

        /// <inheriteddoc />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return this.DisplayName;
        }

        /// <inheriteddoc />
        public void Restart()
        {
            lock (this._SYNC)
            {
                this.OnStop();
                this.OnStart();
            }
        }

        /// <inheriteddoc />
        public void Start()
        {
            lock (this._SYNC)
            {
                if (this.IsRunning)
                {
                    return;
                }

                this.OnStart();
            }
        }

        /// <inheriteddoc />
        public void Stop()
        {
            lock (this._SYNC)
            {
                if (this.IsRunning == false)
                {
                    return;
                }

                this.OnStop();
            }
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            this.SaveResultCommand = new SimpleCommand(this.SaveResult);

            this.StartCommand = new SimpleCommand(this.Start);
            this.StopCommand = new SimpleCommand(this.Stop);

            this.Results = new SynchronizedObservableCollection<ICompareResult>();
        }

        // Private Methods (8) 

        private static DateTime NormalizeTimestamp(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day,
                                input.Hour, input.Minute, input.Second);
        }

        private void OnStart()
        {
            try
            {
                this.Task = this.CreateTask(out this._cancelSource);
                this.Task.Start();
            }
            catch
            {
                this.Task = null;
                this._cancelSource = null;
                this.IsRunning = false;

                throw;
            }
        }

        private void OnStop()
        {
            var src = this._cancelSource;
            if (src != null)
            {
                src.Cancel(throwOnFirstException: true);
            }
        }

        private void SaveResult()
        {
            try
            {
                var results = CollectionHelper.AsArray(this.Results);
                if (results.Length < 1)
                {
                    return;
                }

                var dialog = new SaveFileDialog();
                dialog.Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json|CSV files (*.csv; *.txt)|*.csv;*.txt|All files (*.*)|*.*";
                dialog.OverwritePrompt = true;
                if (dialog.ShowDialog().IsNotTrue())
                {
                    return;
                }

                var outputFile = new FileInfo(dialog.FileName);

                Action<Stream, IList<ICompareResult>> actionToInvoke;
                switch ((Path.GetExtension(outputFile.Name) ?? string.Empty).ToLower().Trim())
                {
                    case ".json":
                        actionToInvoke = this.SaveResult_Json;
                        break;

                    case ".csv":
                    case ".txt":
                        actionToInvoke = this.SaveResult_PlainText;
                        break;

                    default:
                        actionToInvoke = this.SaveResult_Xml;
                        break;
                }

                using (var stream = new FileStream(outputFile.FullName,
                                                   FileMode.Create, FileAccess.ReadWrite))
                {
                    actionToInvoke(stream, results);
                }
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void SaveResult_Json(Stream output, IList<ICompareResult> results)
        {
            var obj = new
            {
                differences = new List<object>(),

                errors = new List<object>(),
            };

            // differences
            foreach (var diff in results.OfType<CompareDifference>())
            {
                obj.differences.Add(new
                {
                    destination = new
                    {
                        path = diff.Destination != null ? diff.Destination.FullName : null,
                    },

                    source = new
                    {
                        path = diff.Source != null ? diff.Source.FullName : null,
                    },
                });
            }

            // errors
            foreach (var err in results.OfType<CompareError>())
            {
                obj.errors.Add(new
                {
                    destination = new
                    {
                        path = err.Destination != null ? err.Destination.FullName : null,
                    },

                    exception = new
                    {
                        message = err.Exception.ToString(),

                        type = err.Exception.GetType().FullName,
                    },

                    source = new
                    {
                        path = err.Source != null ? err.Source.FullName : null,
                    },
                });
            }

            var serializer = new JsonSerializer();

            using (var streamWriter = new StreamWriter(output, Encoding.UTF8))
            {
                serializer.Serialize(streamWriter, obj);

                streamWriter.Flush();
            }
        }

        private void SaveResult_PlainText(Stream output, IList<ICompareResult> results)
        {
            var table = new DataTable();

            table.Columns.Add("Type");
            table.Columns.Add("Source");
            table.Columns.Add("Destination");

            foreach (var r in results)
            {
                List<object> cells = new List<object>();

                if (r is CompareDifference)
                {
                    var diff = (CompareDifference)r;

                    cells.Add("difference");
                    cells.Add(diff.Source != null ? diff.Source.FullName : null);
                    cells.Add(diff.Destination != null ? diff.Destination.FullName : null);
                }
                else if (r is CompareError)
                {
                    var err = (CompareError)r;

                    cells.Add("error");
                    cells.Add(err.Source != null ? err.Source.FullName : null);
                    cells.Add(err.Destination != null ? err.Destination.FullName : null);
                }

                table.Rows.Add(cells.ToArray());
            }

            using (var streamWriter = new StreamWriter(output, Encoding.UTF8))
            {
                table.ToCsv(streamWriter);
            }
        }

        private void SaveResult_Xml(Stream output, IList<ICompareResult> results)
        {
            var xmlDoc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
            xmlDoc.Add(new XElement("fileComparer"));

            // differences
            {
                var differencesElement = new XElement("differences");
                foreach (var diff in results.OfType<CompareDifference>())
                {
                    var newDifferenceElement = new XElement("difference");

                    if (diff.Source != null)
                    {
                        newDifferenceElement.SetAttributeValue("source",
                                                               diff.Source.FullName);
                    }

                    if (diff.Destination != null)
                    {
                        newDifferenceElement.SetAttributeValue("destination",
                                                               diff.Destination.FullName);
                    }

                    if (diff.Differences.HasValue)
                    {
                        var dv = diff.Differences.Value;

                        foreach (var fsDiff in Enum.GetValues(typeof(FileSystemItemDifferences))
                                                   .Cast<FileSystemItemDifferences>()
                                                   .Where(f => f != FileSystemItemDifferences.None)
                                                   .OrderBy(f => f.ToString(), StringComparer.InvariantCultureIgnoreCase))
                        {
                            if (dv.HasFlag(fsDiff))
                            {
                                var newElement = new XElement(fsDiff.ToString());
                                switch (fsDiff)
                                {
                                    case FileSystemItemDifferences.LastWriteTime:
                                        {
                                            try
                                            {
                                                if (diff.Source != null)
                                                {
                                                    newElement.SetAttributeValue("source",
                                                                                 diff.Source.LastWriteTimeUtc.ToString(_FORMAT_FILETIME));
                                                }
                                            }
                                            catch
                                            {
                                                // ignore
                                            }

                                            try
                                            {
                                                if (diff.Destination != null)
                                                {
                                                    newElement.SetAttributeValue("destination",
                                                                                 diff.Destination.LastWriteTimeUtc.ToString(_FORMAT_FILETIME));
                                                }
                                            }
                                            catch
                                            {
                                                // ignore
                                            }
                                        }
                                        break;

                                    case FileSystemItemDifferences.Size:
                                        {
                                            try
                                            {
                                                var file = diff.Source as FileInfo;
                                                if (file != null)
                                                {
                                                    newElement.SetAttributeValue("source",
                                                                                 file.Length);
                                                }
                                            }
                                            catch
                                            {
                                                // ignore
                                            }

                                            try
                                            {
                                                var file = diff.Destination as FileInfo;
                                                if (file != null)
                                                {
                                                    newElement.SetAttributeValue("destination",
                                                                                 file.Length);
                                                }
                                            }
                                            catch
                                            {
                                                // ignore
                                            }
                                        }
                                        break;
                                }

                                newDifferenceElement.Add(newElement);
                            }
                        }
                    }

                    differencesElement.Add(newDifferenceElement);
                }

                xmlDoc.Root.Add(differencesElement);
            }

            // errors
            {
                var errorsElement = new XElement("errors");
                foreach (var err in results.OfType<CompareError>())
                {
                    var newErrorElement = new XElement("error");
                    newErrorElement.SetAttributeValue("type",
                                                      err.Exception.GetType().FullName);

                    if (err.Source != null)
                    {
                        newErrorElement.SetAttributeValue("source",
                                                          err.Source.FullName);
                    }

                    if (err.Destination != null)
                    {
                        newErrorElement.SetAttributeValue("destination",
                                                          err.Destination.FullName);
                    }

                    newErrorElement.Value = err.Exception.ToString();

                    errorsElement.Add(newErrorElement);
                }

                xmlDoc.Root.Add(errorsElement);
            }

            xmlDoc.Save(output);
        }

        private static void TaskAction(object state)
        {
            var tuple = (TaskTuple)state;

            var task = tuple.Item1;
            var cancelTokenSrc = tuple.Item2;

            try
            {
                task.Progress = new CompareProgress(task);

                App.Current
                   .BeginInvoke((a, appState) =>
                       {
                           appState.Task.Results.Clear();
                       }, actionState: new
                       {
                           Task = task,
                       });

                task.IsRunning = true;
                task.RaiseEventHandler(task.Started);

                var comparer = new DirectoryComparer(src: task.Source,
                                                     dest: task.Destination);

                var ctx = comparer.CreateContext(recursive: task.Recursive,
                                                 state: task);

                ctx.ComparingItems += (sender, e) =>
                    {
                        Action<Exception> addError = (err) =>
                            {
                                try
                                {
                                    task.Results.Add(new CompareError(e, err));
                                }
                                catch (Exception ex)
                                {
                                    task.OnError(new AggregateException(ex, err));
                                }
                            };

                        try
                        {
                            e.Handled = true;

                            if (e.Source.Name == "Program.cs")
                            {
                                throw new Exception("Wurst");
                            }

                            task.Progress.Source = e.Source;
                            task.Progress.Destination = e.Destination;

                            task.Progress.StatusText = null;
                            task.Progress.ContentState = null;
                            task.Progress.SizeState = null;
                            task.Progress.TimestampState = null;

                            if (e.Source is DirectoryInfo)
                            {
                                var srcDir = (DirectoryInfo)e.Source;
                                var destDir = (DirectoryInfo)e.Destination;

                                if (srcDir.Exists)
                                {
                                    if (destDir.Exists)
                                    {
                                    }
                                    else
                                    {
                                        e.Differences |= FileSystemItemDifferences.IsMissing;
                                    }
                                }
                                else
                                {
                                    if (destDir.Exists)
                                    {
                                        e.Differences |= FileSystemItemDifferences.IsExtra;
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                            else if (e.Source is FileInfo)
                            {
                                var srcFile = (FileInfo)e.Source;
                                var destFile = (FileInfo)e.Destination;

                                if (srcFile.Exists)
                                {
                                    if (destFile.Exists)
                                    {
                                        var doHash = true;

                                        // timestamp
                                        task.Progress.StatusText = "Checking last write time...";
                                        try
                                        {
                                            task.Progress.TimestampState = CompareState.InProgress;

                                            if (NormalizeTimestamp(srcFile.LastWriteTimeUtc) != NormalizeTimestamp(destFile.LastWriteTimeUtc))
                                            {
                                                e.Differences |= FileSystemItemDifferences.LastWriteTime;
                                                task.Progress.TimestampState = CompareState.Different;

                                                doHash = true;
                                            }
                                            else
                                            {
                                                task.Progress.TimestampState = CompareState.Match;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            addError(ex);

                                            task.Progress.TimestampState = CompareState.Failed;
                                        }

                                        // size
                                        task.Progress.StatusText = "Checking file size...";
                                        try
                                        {
                                            task.Progress.SizeState = CompareState.InProgress;

                                            if (srcFile.Length != destFile.Length)
                                            {
                                                e.Differences |= FileSystemItemDifferences.Size;

                                                task.Progress.SizeState = CompareState.Different;
                                                task.Progress.ContentState = CompareState.Different;

                                                doHash = false;
                                            }
                                            else
                                            {
                                                task.Progress.SizeState = CompareState.Match;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            addError(ex);

                                            task.Progress.SizeState = CompareState.Failed;
                                        }

                                        if (doHash && task.Hash != null)
                                        {
                                            task.Progress.StatusText = "Hashing files...";

                                            // hash content
                                            try
                                            {
                                                task.Progress.ContentState = CompareState.InProgress;

                                                task.Progress.StatusText = "Hashing source file...";
                                                byte[] hashSrc;
                                                using (var algo = (HashAlgorithm)Activator.CreateInstance(task.Hash))
                                                {
                                                    using (var stream = srcFile.OpenRead())
                                                    {
                                                        hashSrc = algo.ComputeHash(stream);
                                                    }
                                                }

                                                task.Progress.StatusText = "Hashing destination file...";
                                                byte[] hashDest;
                                                using (var algo = (HashAlgorithm)Activator.CreateInstance(task.Hash))
                                                {
                                                    using (var stream = destFile.OpenRead())
                                                    {
                                                        hashDest = algo.ComputeHash(stream);
                                                    }
                                                }

                                                if (CollectionHelper.SequenceEqual(hashSrc, hashDest))
                                                {
                                                    task.Progress.ContentState = CompareState.Match;
                                                }
                                                else
                                                {
                                                    task.Progress.ContentState = CompareState.Different;

                                                    e.Differences |= FileSystemItemDifferences.Content;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                addError(ex);

                                                task.Progress.TimestampState = CompareState.Failed;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e.Differences |= FileSystemItemDifferences.IsMissing;
                                    }
                                }
                                else
                                {
                                    if (destFile.Exists)
                                    {
                                        e.Differences |= FileSystemItemDifferences.IsExtra;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            addError(ex);
                        }
                    };

                ctx.DifferentItemsFound += (sender, e) =>
                    {
                        try
                        {
                            App.Current
                               .BeginInvoke((a, appState) =>
                               {
                                   appState.Task
                                           .Results
                                           .Add(appState.Difference);
                               }, actionState: new
                               {
                                   Difference = new CompareDifference(e),
                                   Task = task,
                               });
                        }
                        catch (Exception ex)
                        {
                            task.OnError(ex);
                        }
                    };

                var t = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            ctx.Start();
                        }
                        catch (Exception ex)
                        {
                            task.OnError(ex);
                        }
                    });

                while (t.Status == TaskStatus.WaitingToRun) { }

                while (t.Status == TaskStatus.Running)
                {
                    if (cancelTokenSrc.Token.IsCancellationRequested)
                    {
                        ctx.Cancel();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                task.OnError(ex);
            }
            finally
            {
                task.IsRunning = false;
                task.RaiseEventHandler(task.Stopped);

                task.Progress = null;
            }
        }

        #endregion Methods
    }
}