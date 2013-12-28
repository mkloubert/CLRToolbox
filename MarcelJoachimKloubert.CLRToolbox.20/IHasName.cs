// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Globalization;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Describes an object that has a name.
    /// </summary>
    public interface IHasName : ITMObject
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the display name for the current culture.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the internal name of that object.
        /// </summary>
        /// <remarks>The name is case insensitive.</remarks>
        string Name { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Gets the display name for a specific culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The display name based on <paramref name="culture" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="culture" /> is <see langword="null" />.
        /// </exception>
        string GetDisplayName(CultureInfo culture);

        #endregion Operations
    }
}
