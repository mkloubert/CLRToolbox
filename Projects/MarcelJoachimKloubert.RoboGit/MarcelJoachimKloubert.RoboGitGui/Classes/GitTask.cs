// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using LibGit2Sharp;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskTuple = System.Tuple<MarcelJoachimKloubert.RoboGitGui.Classes.GitTask, System.Threading.CancellationTokenSource>;

namespace MarcelJoachimKloubert.RoboGitGui.Classes
{
    /// <summary>
    /// Stores the data for a git task.
    /// </summary>
    public sealed partial class GitTask : ErrorHandlerBase, IHasName, IRunnable
    {
        #region Fields (13)

        private CancellationTokenSource _cancelSource;
        private const string _CONFIG_NAME_EMAIL = "email";
        private const string _CONFIG_NAME_GROUP = "group";
        private const string _CONFIG_NAME_IS_ACTIVE = "is_active";
        private const string _CONFIG_NAME_METHOD = "method";
        private const string _CONFIG_NAME_PASSWORD = "password";
        private const string _CONFIG_NAME_REMOTES = "remotes";
        private const string _CONFIG_NAME_TASKNAME = "name";
        private const string _CONFIG_NAME_USE_CREDENTIALS = "use_credentials";
        private const string _CONFIG_NAME_USERNAME = "username";
        private readonly AggregateLogger _LOGGER;
        private const string _METHOD_PULL = "PULL";
        private const string _METHOD_PUSH = "PUSH";

        #endregion Fields

        #region Constructors (1)

        private GitTask()
        {
            this._LOGGER = new AggregateLogger();
        }

        #endregion Constructors

        #region Properties (15)

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
        /// Gets the underlying control.
        /// </summary>
        public GitTaskControl Control
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the credentials that should be used for the task.
        /// </summary>
        public Credentials Credentials
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
        /// Gets the optional email address.
        /// </summary>
        public Uri eMail
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        public string Group
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

        /// <summary>
        /// Gets the underlying logger.
        /// </summary>
        public AggregateLogger Logger
        {
            get { return this._LOGGER; }
        }

        /// <summary>
        /// Gets the method to use.
        /// </summary>
        public GitTaskMethod Method
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
        /// Gets the read-only list of remotes.
        /// </summary>
        public IList<string> Remotes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the underlying system task if Git task is running.
        /// </summary>
        public Task Task
        {
            get;
            private set;
        }

        #endregion Properties

        #region Delegates and Events (3)

        // Events (3) 

        /// <summary>
        /// Is invoked when a log message has been received.
        /// </summary>
        public event EventHandler<GitTaskLogEventArgs> LogMessageReceived;

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
        public static IEnumerable<GitTask> FromConfig(IConfigRepository config,
                                                      bool activeOnly = true)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            foreach (var taskName in config.GetCategoryNames())
            {
                GitTask newTask;
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

                    string group;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_GROUP,
                                               value: out group);

                    string method;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_METHOD,
                                               value: out method);

                    string displayName;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_TASKNAME,
                                               value: out displayName);

                    bool useCredentials;
                    config.TryGetValue<bool>(category: taskName,
                                             name: _CONFIG_NAME_USE_CREDENTIALS,
                                             value: out useCredentials);

                    string remotes;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_REMOTES,
                                               value: out remotes);

                    Credentials cred = null;
                    if (useCredentials)
                    {
                        string user;
                        config.TryGetValue<string>(category: taskName,
                                                   name: _CONFIG_NAME_USERNAME,
                                                   value: out user);

                        if (string.IsNullOrWhiteSpace(user) == false)
                        {
                            cred = new Credentials();
                            cred.Username = user.Trim();

                            string pwd;
                            config.TryGetValue<string>(category: taskName,
                                                       name: _CONFIG_NAME_PASSWORD,
                                                       value: out pwd);

                            if (string.IsNullOrEmpty(pwd) == false)
                            {
                                cred.Password = pwd;
                            }
                        }
                    }

                    string email;
                    config.TryGetValue<string>(category: taskName,
                                               name: _CONFIG_NAME_EMAIL,
                                               value: out email);

                    newTask = new GitTask();
                    newTask.Credentials = cred;
                    newTask.DisplayName = string.IsNullOrWhiteSpace(displayName) ? taskName : displayName.Trim();
                    newTask.IsActive = isActive;
                    newTask.Logger.Add(new _GitTaskLogger(newTask));
                    newTask.Name = taskName;
                    newTask.Remotes = new ReadOnlyCollection<string>((remotes ?? string.Empty).Split('\n')
                                                                                              .Select(x => x.ToUpper().Trim())
                                                                                              .Where(x => x != string.Empty)
                                                                                              .Distinct()
                                                                                              .ToArray());

                    if (string.IsNullOrWhiteSpace(email) == false)
                    {
                        newTask.eMail = new Uri("mailto:" + email.Trim());
                    }

                    if (string.IsNullOrWhiteSpace(group) == false)
                    {
                        newTask.Group = group.ToUpper().Trim();
                    }

                    switch ((method ?? string.Empty).ToUpper().Trim())
                    {
                        case _METHOD_PULL:
                            newTask.Method = GitTaskMethod.Pull;
                            break;

                        case "":
                        case _METHOD_PUSH:
                            newTask.Method = GitTaskMethod.Push;
                            break;

                        default:
                            throw new NotSupportedException();
                    }

                    newTask.Control = new GitTaskControl(newTask);
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

        private bool? OnLogMessageReceived(ILogMessage msg)
        {
            if (msg == null)
            {
                return null;
            }

            return this.RaiseEventHandler(this.LogMessageReceived,
                                          new GitTaskLogEventArgs(msg));
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

                Action<GitTask, CancellationToken> actionToInvoke = null;

                switch (task.Method)
                {
                    case GitTaskMethod.Pull:
                        actionToInvoke = PullAction;
                        break;

                    case GitTaskMethod.Push:
                        actionToInvoke = PushAction;
                        break;
                }

                if (actionToInvoke != null)
                {
                    actionToInvoke(task, cancelTokenSrc.Token);

                    Thread.Sleep(2500);
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
            }
        }

        #endregion Methods
    }
}
