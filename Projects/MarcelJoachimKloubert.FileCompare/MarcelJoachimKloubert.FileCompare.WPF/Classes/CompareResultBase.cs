// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
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

        #region Properties (2)

        /// <inheriteddoc />
        public abstract FileSystemInfo Destination
        {
            get;
        }

        /// <inheriteddoc />
        public abstract FileSystemInfo Source
        {
            get;
        }

        #endregion Properties
    }
}