// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// A basic notification object.
    /// </summary>
    public abstract partial class NotificationObjectBase : TMObject,
                                                           INotificationObject
    {


        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="invokeOnConstructor">
        /// Invoke <see cref="NotificationObjectBase.OnConstructor()" /> method or not.
        /// </param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected NotificationObjectBase(bool invokeOnConstructor, object syncRoot)
            : base(syncRoot)
        {
            if (invokeOnConstructor)
            {
                this.OnConstructor();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="invokeOnConstructor">
        /// Invoke <see cref="NotificationObjectBase.OnConstructor()" /> method or not.
        /// </param>
        protected NotificationObjectBase(bool invokeOnConstructor)
            : this(invokeOnConstructor, new object())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected NotificationObjectBase(object syncRoot)
            : this(true, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        protected NotificationObjectBase()
            : this(true)
        {

        }

        #endregion Constructors


        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="INotifyPropertyChanged.PropertyChanged" />
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Delegates and Events


        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Contains additional logic for the constructor.
        /// </summary>
        protected virtual void OnConstructor()
        {
            // dummy
        }

        #endregion Methods
    }
}
