// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// A basic notification object.
    /// </summary>
    public abstract partial class NotificationObjectBase : TNObject,
                                                           INotificationObject
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
        protected readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="invokeOnConstructor">
        /// Invoke <see cref="NotificationObjectBase.OnConstructor()" /> method or not.
        /// </param>
        protected NotificationObjectBase(bool invokeOnConstructor)
        {
            if (invokeOnConstructor)
            {
                this.OnConstructor();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        protected NotificationObjectBase()
            : this(true)
        {

        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Events (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="INotifyPropertyChanged.PropertyChanged" />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="INotifyPropertyChanging.PropertyChanging" />
        public event PropertyChangingEventHandler PropertyChanging;

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
