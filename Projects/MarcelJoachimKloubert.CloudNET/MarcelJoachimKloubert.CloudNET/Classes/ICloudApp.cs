// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CloudNET.Classes
{
    /// <summary>
    /// Describes the context of a cloud application.
    /// </summary>
    public interface ICloudApp : ITMObject
    {
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
