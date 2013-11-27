// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de



using System;
namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Describes an object that converts values and objects.
    /// </summary>
    public interface IConverter : ITMObject
    {
        #region Operations (2)

        /// <summary>
        /// Changes a value to a target type if needed.
        /// </summary>
        /// <param name="value">The value to cast / convert.</param>
        /// <returns>The converted / casted version of <paramref name="value" />.</returns>
        T ChangeType<T>(object value);

        /// <summary>
        /// Changes a value to a target type if needed.
        /// </summary>
        /// <param name="value">The value to cast / convert.</param>
        /// <param name="provider">The format provider to use.</param>
        /// <returns>The converted / casted version of <paramref name="value" />.</returns>
        T ChangeType<T>(object value, IFormatProvider provider);

        #endregion Operations
    }
}
