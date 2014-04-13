// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using MarcelJoachimKloubert.CLRToolbox.IO;
using MarcelJoachimKloubert.CLRToolbox.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TaskTuple = System.Tuple<MarcelJoachimKloubert.FileCompare.WPF.Classes.CompareTask, System.Threading.CancellationTokenSource>;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    public sealed class CompareTask : NotificationObjectBase, IHasName, IRunnable
    {
        #region Fields (8)

        private CancellationTokenSource _cancelSource;
        private const string _CONFIG_NAME_DEST = "destination";
        private const string _CONFIG_NAME_HASH = "hash";
        private const string _CONFIG_NAME_IS_ACTIVE = "is_active";
        private const string _CONFIG_NAME_RECURSIVE = "recursive";
        private const string _CONFIG_NAME_SOURCE = "source";
        private const string _CONFIG_NAME_TASKNAME = "name";
        private readonly AggregateLogger _LOGGER;

        #endregion Fields

        #region Constructors (1)

        private CompareTask()
        {
            this._LOGGER = new AggregateLogger();
        }

        #endregion Constructors

        #region Properties (12)

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
            get;
            private set;
        }

        /// <inheriteddoc />
        public string Name
        {
            get;
            private set;
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
        /// Gets the path of the source directory.
        /// </summary>
        public string Source
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

        #region Methods (11)

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

        // Private Methods (4) 

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

        private static void TaskAction(object state)
        {
            var tuple = (TaskTuple)state;

            var task = tuple.Item1;
            var cancelTokenSrc = tuple.Item2;

            try
            {
                task.IsRunning = true;
                task.RaiseEventHandler(task.Started);

                var comparer = new DirectoryComparer(src: task.Source,
                                                     dest: task.Destination);

                var ctx = comparer.CreateContext(recursive: task.Recursive,
                                                 state: task);

                ctx.ComparingItems += (sender, e) =>
                    {
                        try
                        {
                            task.RaiseEventHandler(task.ComparingItems, e);
                        }
                        catch (Exception ex)
                        {
                            task.OnError(ex);
                        }
                    };

                ctx.DifferentItemsFound += (sender, e) =>
                    {
                        try
                        {
                            task.RaiseEventHandler(task.DifferentItemsFound, e);
                        }
                        catch (Exception ex)
                        {
                            task.OnError(ex);
                        }
                    };

                ctx.Start();
            }
            catch (Exception ex)
            {
                task.OnError(ex);
            }
            finally
            {
                task.IsRunning = false;
                task.RaiseEventHandler(task.Stopped);
            }
        }

        #endregion Methods
    }
}