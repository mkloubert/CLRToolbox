// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    partial class NotificationObjectBase
    {
        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="INotifyPropertyChanging.PropertyChanging" />
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion Delegates and Events

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanging" /> event.
        /// </summary>
        /// <param name="propertyName">The name of the property to raise the event for.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="ArgumentException"><paramref name="propertyName" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="propertyName" /> has an invalid format.
        /// </exception>
        protected bool OnPropertyChanging(IEnumerable<char> propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            string pn = StringHelper.AsString(propertyName).Trim();
            if (pn == string.Empty)
            {
                throw new ArgumentException("propertyName");
            }

            PropertyChangingEventHandler handler = this.PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(pn));
                return true;
            }

            // no delegate linked
            return false;
        }

        #endregion Methods
    }
}
