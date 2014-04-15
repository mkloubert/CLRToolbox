// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;
using MarcelJoachimKloubert.FileCompare.WPF.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarcelJoachimKloubert.FileCompare.WPF.ViewModels
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public sealed class MainViewModel : NotificationObjectBase
    {
        #region Fields (2)

        private CompareTask _selectedTask;
        private IEnumerable<CompareTask> _tasks;

        #endregion Fields

        #region Properties (3)

        /// <summary>
        /// Gets the command that opens a directory.
        /// </summary>
        public SimpleCommand<string> OpenDirectoryCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected task.
        /// </summary>
        public CompareTask SelectedTask
        {
            get { return this._selectedTask; }

            set { this.SetProperty(ref this._selectedTask, value); }
        }

        /// <summary>
        /// Gets or sets the list of available tasks.
        /// </summary>
        public IEnumerable<CompareTask> Tasks
        {
            get { return this._tasks; }

            set { this.SetProperty(ref this._tasks, value); }
        }

        #endregion Properties

        #region Methods (2)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            this.OpenDirectoryCommand = new SimpleCommand<string>(this.OpenDirectory);
        }

        // Private Methods (1) 

        private void OpenDirectory(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        #endregion Methods
    }
}