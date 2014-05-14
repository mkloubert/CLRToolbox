// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using System;
using System.Windows;
using System.Windows.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Collections.ObjectModel
{
    #region CLASS: DispatcherObservableCollection<T>

    /// <summary>
    /// A thread safe observable collection that uses a <see cref="Dispatcher" /> for each operation.
    /// </summary>
    /// <typeparam name="T">Type of the items.</typeparam>
    public class DispatcherObservableCollection<T> : SynchronizedObservableCollection<T>
    {
        #region Constructors (10)

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="provider">The value for the <see cref="DispatcherObservableCollection{T}.Provider" /> property.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherProvider provider, DispatcherPriority prio, bool isBackground, object syncRoot)
            : base(syncRoot)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this.Provider = provider;
            this.Priority = prio;
            this.IsBackground = isBackground;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="provider">The value for the <see cref="DispatcherObservableCollection{T}.Provider" /> property.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherProvider provider, DispatcherPriority prio, object syncRoot)
            : this(provider, prio, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="provider">The value for the <see cref="DispatcherObservableCollection{T}.Provider" /> property.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherProvider provider, DispatcherPriority prio, bool isBackground)
            : this(provider, prio, isBackground, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherPriority prio, object syncRoot)
            : this(GetApplicationDispatcher, prio, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        public DispatcherObservableCollection(DispatcherPriority prio, bool isBackground)
            : this(GetApplicationDispatcher, prio, isBackground)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="provider">The value for the <see cref="DispatcherObservableCollection{T}.Provider" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> and/or <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherProvider provider, object syncRoot)
            : this(provider, DispatcherPriority.Normal, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="provider">The value for the <see cref="DispatcherObservableCollection{T}.Provider" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherProvider provider, bool isBackground)
            : this(provider, DispatcherPriority.Normal, isBackground)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="provider">The value for the <see cref="DispatcherObservableCollection{T}.Provider" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="provider" /> is <see langword="null" />.
        /// </exception>
        public DispatcherObservableCollection(DispatcherProvider provider)
            : this(provider, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        public DispatcherObservableCollection(DispatcherPriority prio)
            : this(GetApplicationDispatcher, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherObservableCollection{T}" /> class.
        /// </summary>
        public DispatcherObservableCollection()
            : this(GetApplicationDispatcher)
        {
        }

        #endregion CLASS: DispatcherObservableCollection<T>

        #region Properties (3)

        /// <summary>
        /// Gets if the <see cref="Dispatcher.BeginInvoke(Delegate, DispatcherPriority, object[])" /> method should be invoked
        /// for each operation ((<see langword="true" />)) or the <see cref="Dispatcher.Invoke(Delegate, DispatcherPriority, object[])" /> method
        /// (<see langword="false" />).
        /// </summary>
        public bool IsBackground
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the priority to use.
        /// </summary>
        public DispatcherPriority Priority
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the handler that provides the <see cref="Dispatcher" /> to use.
        /// </summary>
        public DispatcherProvider Provider
        {
            get;
            private set;
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes a method or function that provides the <see cref="Dispatcher" /> to use.
        /// </summary>
        /// <param name="coll">The instance of the underlying collection.</param>
        /// <returns>The dispatcher to use.</returns>
        public delegate Dispatcher DispatcherProvider(DispatcherObservableCollection<T> coll);

        #endregion Delegates and Events

        #region Methods (4)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void InvokeForCollection<S>(Action<SynchronizedObservableCollection<T>, S> action, S actionState)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Action syncAction = () =>
                {
                    lock (this._SYNC)
                    {
                        action(this, actionState);
                    }
                };

            Action<Dispatcher, Action, DispatcherPriority> dispAction;
            if (this.IsBackground)
            {
                dispAction = InvokeForCollection_BeginInvoke;
            }
            else
            {
                dispAction = InvokeForCollection_Invoke;
            }

            dispAction(this.Provider(this),
                       syncAction,
                       this.Priority);
        }

        // Private Methods (3) 

        private static Dispatcher GetApplicationDispatcher(DispatcherObservableCollection<T> coll)
        {
            return Application.Current.Dispatcher;
        }

        private static void InvokeForCollection_BeginInvoke(Dispatcher disp,
                                                            Action syncAction,
                                                            DispatcherPriority prio)
        {
            disp.BeginInvoke(syncAction,
                             prio);
        }

        private static void InvokeForCollection_Invoke(Dispatcher disp,
                                                       Action syncAction,
                                                       DispatcherPriority prio)
        {
            disp.Invoke(syncAction,
                        prio);
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: DispatcherObservableCollection

    /// <summary>
    /// Factory class for <see cref="DispatcherObservableCollection{T}" />.
    /// </summary>
    public static class DispatcherObservableCollection
    {
        #region Methods (13)

        // Public Methods (13) 

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for the instance of <see cref="Application.Current" /> property.
        /// </summary>
        public static DispatcherObservableCollection<T> Create<T>()
        {
            return Create<T>(Application.Current);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="Dispatcher" />.
        /// </summary>
        /// <param name="disp">The dispatcher object from where to get the dispatcher from.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disp" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(Dispatcher disp)
        {
            return Create<T>(disp, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="DispatcherObject" />.
        /// </summary>
        /// <param name="dispObj">The dispatcher object from where to get the dispatcher from.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispObj" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(DispatcherObject dispObj)
        {
            return Create<T>(dispObj, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="Dispatcher" />.
        /// </summary>
        /// <param name="disp">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(Dispatcher disp, object syncRoot)
        {
            return Create<T>(disp, DispatcherPriority.Normal, syncRoot);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="DispatcherObject" />.
        /// </summary>
        /// <param name="dispObj">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(DispatcherObject dispObj, object syncRoot)
        {
            return Create<T>(dispObj, DispatcherPriority.Normal, syncRoot);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="Dispatcher" />.
        /// </summary>
        /// <param name="disp">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disp" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(Dispatcher disp, bool isBackground)
        {
            return Create<T>(disp, DispatcherPriority.Normal, isBackground);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="DispatcherObject" />.
        /// </summary>
        /// <param name="dispObj">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispObj" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(DispatcherObject dispObj, bool isBackground)
        {
            return Create<T>(dispObj, DispatcherPriority.Normal, isBackground);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="Dispatcher" />.
        /// </summary>
        /// <param name="disp">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(Dispatcher disp, DispatcherPriority prio, object syncRoot)
        {
            return Create<T>(disp, prio, false, syncRoot);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="DispatcherObject" />.
        /// </summary>
        /// <param name="dispObj">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(DispatcherObject dispObj, DispatcherPriority prio, object syncRoot)
        {
            return Create<T>(dispObj, prio, false, syncRoot);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="Dispatcher" />.
        /// </summary>
        /// <param name="disp">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disp" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(Dispatcher disp, DispatcherPriority prio, bool isBackground)
        {
            return Create<T>(disp, prio, isBackground, new object());
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="DispatcherObject" />.
        /// </summary>
        /// <param name="dispObj">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispObj" /> is <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(DispatcherObject dispObj, DispatcherPriority prio, bool isBackground)
        {
            return Create<T>(dispObj, prio, isBackground, new object());
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="Dispatcher" />.
        /// </summary>
        /// <param name="disp">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disp" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(Dispatcher disp, DispatcherPriority prio, bool isBackground, object syncRoot)
        {
            if (disp == null)
            {
                throw new ArgumentNullException("disp");
            }

            return new DispatcherObservableCollection<T>((coll) => disp,
                                                          prio,
                                                          isBackground,
                                                          syncRoot);
        }

        /// <summary>
        /// Creates new instance of the <see cref="DispatcherObservableCollection{T}" /> class
        /// for a specific <see cref="DispatcherObject" />.
        /// </summary>
        /// <param name="dispObj">The dispatcher object from where to get the dispatcher from.</param>
        /// <param name="prio">The value for the <see cref="DispatcherObservableCollection{T}.Priority" /> property.</param>
        /// <param name="isBackground">The value for the <see cref="DispatcherObservableCollection{T}.IsBackground" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="SynchronizedObservableCollection{T}._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dispObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DispatcherObservableCollection<T> Create<T>(DispatcherObject dispObj, DispatcherPriority prio, bool isBackground, object syncRoot)
        {
            if (dispObj == null)
            {
                throw new ArgumentNullException("dispObj");
            }

            return new DispatcherObservableCollection<T>((coll) => dispObj.Dispatcher,
                                                          prio,
                                                          isBackground,
                                                          syncRoot);
        }

        #endregion Methods
    }

    #endregion
}