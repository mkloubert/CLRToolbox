// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// Describes a notification object.
    /// </summary>
    public partial interface INotificationObject : ITMObject,
                                                   INotifyPropertyChanged,
                                                   INotifyPropertyChanging
    {

    }
}
