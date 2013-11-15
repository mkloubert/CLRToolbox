// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration
{
    /// <summary>
    /// Describes a repository that provides configuration data stored in categories as key/value pairs.
    /// </summary>
    public interface IConfigRepository
    {
        #region Data Members (2)

        /// <summary>
        /// Gets if that repository can be written.
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// Gets if that repository can be written.
        /// </summary>
        bool CanWrite { get; }

        #endregion Data Members

        #region Operations (9)

        /// <summary>
        /// Clears all categories and values.
        /// </summary>
        /// <returns>Repository was cleared (write operation was done) or not.</returns>
        /// <exception cref="InvalidOperationException">Repository cannot be written.</exception>
        bool Clear();

        /// <summary>
        /// Checks if a value exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The optional category.</param>
        /// <returns>Value exists or not.</returns>
        bool ContainsValue(IEnumerable<char> name, IEnumerable<char> category = null);

        /// <summary>
        /// Deletes a value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The optional category.</param>
        /// <returns>Value was deleted or not.</returns>
        /// <exception cref="InvalidOperationException">Repository cannot be written.</exception>
        bool DeleteValue(IEnumerable<char> name, IEnumerable<char> category = null);

        /// <summary>
        /// Returns the list of category names.
        /// </summary>
        /// <returns>The current list of category names.</returns>
        IList<string> GetCategoryNames();

        /// <summary>
        /// Returns a config value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The optional category.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value does not exist.</exception>
        /// <exception cref="InvalidOperationException">Repository cannot be read.</exception>
        object GetValue(IEnumerable<char> name, IEnumerable<char> category = null);

        /// <summary>
        /// Returns a config value strong typed.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="category">The optional category.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value does not exist.</exception>
        /// <exception cref="InvalidOperationException">Repository cannot be read.</exception>
        T GetValue<T>(IEnumerable<char> name, IEnumerable<char> category = null);

        /// <summary>
        /// Sets a config value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="category">The optional category.</param>
        /// <returns>Value was set (write operation was done) or not.</returns>
        /// <exception cref="InvalidOperationException">Repository cannot be written.</exception>
        bool SetValue(IEnumerable<char> name, object value, IEnumerable<char> category = null);

        /// <summary>
        /// Sets a config value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="category">The optional category.</param>
        /// <returns>Value was set (write operation was done) or not.</returns>
        /// <exception cref="InvalidOperationException">Repository cannot be written.</exception>
        bool SetValue<T>(IEnumerable<char> name, T value, IEnumerable<char> category = null);

        /// <summary>
        /// Tries to return a config value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The variable where to found value to.</param>
        /// <param name="category">The optional category.</param>
        /// <param name="defaultVal">
        /// the value that is written to <paramref name="value" /> if not found.
        /// </param>
        /// <returns>Value was found or not.</returns>
        /// <exception cref="InvalidOperationException">Repository cannot be read.</exception>
        bool TryGetValue(IEnumerable<char> name, out object value, IEnumerable<char> category = null, object defaultVal = null);

        #endregion Operations

        /// <summary>
        /// Tries to return a config value.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The variable where to found value to.</param>
        /// <param name="category">The optional category.</param>
        /// <param name="defaultVal">
        /// the value that is written to <paramref name="value" /> if not found.
        /// </param>
        /// <returns>Value was found or not.</returns>
        /// <exception cref="InvalidOperationException">Repository cannot be read.</exception>
        bool TryGetValue<T>(IEnumerable<char> name, out T value, IEnumerable<char> category = null, T defaultVal = default(T));
    }
}
