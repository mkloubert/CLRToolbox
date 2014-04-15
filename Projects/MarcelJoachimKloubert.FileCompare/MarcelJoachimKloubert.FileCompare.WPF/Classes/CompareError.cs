// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.IO;
using System;
using System.IO;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// Stores compare errors.
    /// </summary>
    public sealed class CompareError : CompareResultBase
    {
        #region Fields (1)

        private readonly CompareFileSystemItemsEventArgs _EVENT_ARGS;

        #endregion Fields

        #region Constructors (1)

        internal CompareError(CompareFileSystemItemsEventArgs eventArgs, Exception ex)
            : base(invokeOnConstructor: false)
        {
            this._EVENT_ARGS = eventArgs;
            this.Exception = ex;

            this.OnConstructor();
        }

        #endregion Constructors

        #region Properties (3)

        /// <inheriteddoc />
        public override FileSystemInfo Destination
        {
            get { return this._EVENT_ARGS.Destination; }
        }

        /// <summary>
        /// Gets the underlying exception / error.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }

        /// <inheriteddoc />
        public override FileSystemInfo Source
        {
            get { return this._EVENT_ARGS.Source; }
        }

        #endregion Properties
    }
}