// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MarcelJoachimKloubert.CLRToolbox.Data.Entities
{
    partial class EntityBase
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Raises the <see cref="EntityBase.PropertyChanged" /> and <see cref="EntityBase.PropertyChanging" />
        /// events if two values are different and automatically overwrites the underlying field.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">The field where the (current) value of the property is stored.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">
        /// Name name of the property. Should be automatically be set by compiler when that
        /// method is called from inside the underlying property.
        /// </param>
        /// <returns>
        /// <paramref name="field" /> and <paramref name="newValue" /> are different and events were raised.
        /// </returns>
        protected bool SetProperty<T>(ref T field,
                                      T newValue,
                                      [CallerMemberName] IEnumerable<char> propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                this.OnPropertyChanging(propertyName);
                field = newValue;
                this.OnPropertyChanged(propertyName);

                return true;
            }

            // both are the same
            return false;
        }

        #endregion Methods
    }
}
