// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Stores the arguments of an event that is disposing an object.
    /// </summary>
    public sealed class DisposeObjectEventArgs : EventArgs
    {
        #region Fields (3)

        private bool _cancel;
        private readonly bool _IS_DISPOSING;
        private readonly IDisposable _OBJECT;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposeObjectEventArgs" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="DisposeObjectEventArgs.Object" /> property.</param>
        /// <param name="isDisposing">The value for the <see cref="DisposeObjectEventArgs.IsDispoing" /> property.</param>
        /// <param name="cancel">The inital value for the <see cref="DisposeObjectEventArgs.Cancel" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public DisposeObjectEventArgs(IDisposable obj, bool isDisposing, bool cancel)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            this._OBJECT = obj;
            this._IS_DISPOSING = isDisposing;

            this._cancel = cancel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposeObjectEventArgs" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="DisposeObjectEventArgs.Object" /> property.</param>
        /// <param name="isDisposing">The value for the <see cref="DisposeObjectEventArgs.IsDispoing" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <see cref="DisposeObjectEventArgs.Cancel" /> is <see langword="false" /> by default.
        /// </remarks>
        public DisposeObjectEventArgs(IDisposable obj, bool isDisposing)
            : this(obj, isDisposing, false)
        {

        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets or sets if operation should be canceled or not.
        /// </summary>
        public bool Cancel
        {
            get { return this._cancel; }

            set { this._cancel = value; }
        }

        /// <summary>
        /// Gets if <see cref="IDisposable.Dispose()" /> method of <see cref="DisposeObjectEventArgs.Object" /> is called
        /// or the finalizer.
        /// </summary>
        public bool IsDispoing
        {
            get { return this._IS_DISPOSING; }
        }

        /// <summary>
        /// Gets the underlying object.
        /// </summary>
        public IDisposable Object
        {
            get { return this._OBJECT; }
        }

        #endregion Properties
    }
}
