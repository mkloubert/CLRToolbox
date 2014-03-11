// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.ServerAdmin.Classes.Sessions
{
    /// <summary>
    /// Describes a session.
    /// </summary>
    public interface ISession : IIdentifiable
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the ID of the session that is defined and used by the system / environment.
        /// </summary>
        string SystemId { get; }

        #endregion Data Members

        #region Operations (7)

        /// <summary>
        /// Returns an existing session value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <returns>The session value.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// A value with the name that is defined in <paramref name="name" /> was not found.
        /// </exception>
        object Get(IEnumerable<char> name);

        /// <summary>
        /// Returns an existing session value.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="name">The name of the value.</param>
        /// <returns>The session value.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// A value with the name that is defined in <paramref name="name" /> was not found.
        /// </exception>
        T Get<T>(IEnumerable<char> name);

        /// <summary>
        /// Checks if a session value exists.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <returns>Value exists or not.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        bool Has(IEnumerable<char> name);

        /// <summary>
        /// Removes a session value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <returns>Value was deleted or not.</returns>
        bool Remove(IEnumerable<char> name);

        /// <summary>
        /// Sets a session value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The (new) value to set.</param>
        /// <returns>That instance.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="name" /> is invalid.
        /// </exception>
        ISession Set(IEnumerable<char> name, object value);

        /// <summary>
        /// Tries to return a session value.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The variable where to write the found value to.</param>
        /// <param name="defValue">
        /// The value for <paramref name="value" /> if value does not exist.
        /// </param>
        /// <returns>Value was found or not.</returns>
        bool TryGet(IEnumerable<char> name, out object value, object defValue = null);

        /// <summary>
        /// Tries to return a session value.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The variable where to write the found value to.</param>
        /// <param name="defValue">
        /// The value for <paramref name="value" /> if value does not exist.
        /// </param>
        /// <returns>Value was found or not.</returns>
        bool TryGet<T>(IEnumerable<char> name, out T value, T defValue = default(T));

        #endregion Operations
    }
}
