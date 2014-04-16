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
    /// A basic <see cref="ICompareResult" /> item.
    /// </summary>
    public abstract class CompareResultBase : NotificationObjectBase, ICompareResult
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of <see cref="CompareResultBase" /> class.
        /// </summary>
        /// <param name="invokeOnConstructor">
        /// Invoke <see cref="NotificationObjectBase.OnConstructor()" /> method or not.
        /// </param>
        protected CompareResultBase(bool invokeOnConstructor)
            : base(invokeOnConstructor: invokeOnConstructor)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CompareResultBase" /> class.
        /// </summary>
        protected CompareResultBase()
            : base()
        {
        }

        #endregion Constructors

        #region Properties (3)

        /// <inheriteddoc />
        public abstract FileSystemInfo Destination
        {
            get;
        }

        /// <summary>
        /// Gets the command that opens a directory or file.
        /// </summary>
        public SimpleCommand<FileSystemInfo> OpenItemCommand
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public abstract FileSystemInfo Source
        {
            get;
        }

        #endregion Properties

        #region Methods (3)

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
                if (item.Exists)
                {
                    Process.Start("explorer.exe", string.Format("/select,{0}",
                                                                item.FullName));
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