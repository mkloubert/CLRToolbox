// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes.Helpers
{
    /// <summary>
    /// Helper class for parameter operations.
    /// </summary>
    public static class ParameterHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Creates a string list from a sequence of parameters.
        /// </summary>
        /// <param name="params">The parameter sequence.</param>
        /// <returns>The sequence as string.</returns>
        public static string CreateStringList(IEnumerable<ParameterInfo> @params)
        {
            var paramArray = CollectionHelper.AsArray(@params);
            if (paramArray != null)
            {
                if (paramArray.Length > 0)
                {
                    return "(" + string.Join(",",
                                             paramArray.Select(p => TypeHelper.GetFullName(p.ParameterType))) + ")";
                }
            }

            return null;
        }

        #endregion Methods
    }
}