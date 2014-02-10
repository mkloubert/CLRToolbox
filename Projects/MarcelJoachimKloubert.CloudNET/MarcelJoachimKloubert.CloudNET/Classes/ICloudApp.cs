// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CloudNET.Classes
{
    /// <summary>
    /// Describes the context of a cloud application.
    /// </summary>
    public interface ICloudApp : ITMObject
    {
        #region Data Members (1)

        /// <summary>
        /// Gets the configuration data of that application.
        /// </summary>
        IConfigRepository Config { get; }

        #endregion Data Members

        #region Operations (1)

        /// <summary>
        /// Returns an application settings value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        string GetSettingValue(IEnumerable<char> key);

        #endregion Operations
    }
}
