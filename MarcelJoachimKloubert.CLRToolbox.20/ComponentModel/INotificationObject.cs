// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// Describes a notification object.
    /// </summary>
    public partial interface INotificationObject : IErrorHandler, INotifyPropertyChanged
    {
        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked when object is closing.
        /// </summary>
        event CancelEventHandler Closing;

        #endregion Delegates and Events
    }
}