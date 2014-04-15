// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;
using System;
using System.Diagnostics;
using System.IO;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// A progress context for a <see cref="CompareTask" />.
    /// </summary>
    public sealed class CompareProgress : NotificationObjectBase
    {
        #region Fields (6)

        private CompareState? _contentState;
        private FileSystemInfo _destination;
        private CompareState? _sizeState;
        private FileSystemInfo _source;
        private string _statusText;
        private CompareState? _timestampState;

        #endregion Fields

        #region Constructors (1)

        internal CompareProgress(CompareTask task)
        {
            this.Task = task;
        }

        #endregion Constructors

        #region Properties (8)

        /// <summary>
        /// Gets the state of the contents of <see cref="CompareProgress.Source" /> and <see cref="CompareProgress.Destination" />.
        /// </summary>
        public CompareState? ContentState
        {
            get { return this._contentState; }

            internal set { this.SetProperty(ref this._contentState, value); }
        }

        /// <summary>
        /// Gets the destination item.
        /// </summary>
        public FileSystemInfo Destination
        {
            get { return this._destination; }

            internal set { this.SetProperty(ref this._destination, value); }
        }

        /// <summary>
        /// Gets the command that opens a directory or file.
        /// </summary>
        public SimpleCommand<FileSystemInfo> OpenItemCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the (file) length of <see cref="CompareProgress.Source" /> and <see cref="CompareProgress.Destination" />.
        /// </summary>
        public CompareState? SizeState
        {
            get { return this._sizeState; }

            internal set { this.SetProperty(ref this._sizeState, value); }
        }

        /// <summary>
        /// Gets the source item.
        /// </summary>
        public FileSystemInfo Source
        {
            get { return this._source; }

            internal set { this.SetProperty(ref this._source, value); }
        }

        public string StatusText
        {
            get { return this._statusText; }

            internal set { this.SetProperty(ref this._statusText, value); }
        }

        /// <summary>
        /// Gets the underlying task.
        /// </summary>
        public CompareTask Task
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the state of the last write timestamps of <see cref="CompareProgress.Source" /> and <see cref="CompareProgress.Destination" />.
        /// </summary>
        public CompareState? TimestampState
        {
            get { return this._timestampState; }

            internal set { this.SetProperty(ref this._timestampState, value); }
        }

        #endregion Properties

        #region Methods (2)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            this.OpenItemCommand = new SimpleCommand<FileSystemInfo>(this.OpenItem);
        }

        // Private Methods (1) 

        private void OpenItem(FileSystemInfo item)
        {
            try
            {
                item.Refresh();
                if (item.Exists == false)
                {
                    return;
                }

                if (item is FileInfo)
                {
                    Process.Start("explorer.exe", string.Format("/select, \"{0}\"",
                                                                item.FullName));
                }
                else if (item is DirectoryInfo)
                {
                    Process.Start(item.FullName);
                }
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        #endregion Methods
    }
}