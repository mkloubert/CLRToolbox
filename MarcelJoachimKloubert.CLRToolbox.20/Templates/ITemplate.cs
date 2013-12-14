// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Templates
{
    /// <summary>
    /// Describes a template.
    /// </summary>
    public interface ITemplate : ITMObject
    {
        #region Data Members (1)

        /// <summary>
        /// Gets or sets a variable by indexer.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The value of the variable.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Variable not found.</exception>
        object this[IEnumerable<char> name] { get; set; }

        #endregion Data Members

        #region Operations (4)

        /// <summary>
        /// Returns the current list of all variables.
        /// </summary>
        /// <returns>The list of variables.</returns>
        IDictionary<string, object> GetAllVars();

        /// <summary>
        /// Renders content based on the data of that object.
        /// </summary>
        /// <returns>The rendered content.</returns>
        object Render();

        /// <summary>
        /// Sets a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        /// <returns>That instance.</returns>
        ITemplate SetVar(IEnumerable<char> name, object value);

        /// <summary>
        /// Sets a variable.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        /// <returns>That instance.</returns>
        ITemplate SetVar<T>(IEnumerable<char> name, T value);

        #endregion Operations
    }
}
