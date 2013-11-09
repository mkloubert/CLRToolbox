// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class MethodHelper
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes the <see cref="MethodHelper" /> class.
        /// </summary>
        static MethodHelper()
        {
            Type actionType = typeof(global::System.Action<>);
            Assembly actionAsm = actionType.Assembly;

            List<Type> actionTypeList = new List<Type>();
            foreach (Type type in actionAsm.GetTypes())
            {
                if (type.FullName.StartsWith("System.Action"))
                {
                    actionTypeList.Add(type);
                }
            }

            KNOWN_ACTION_TYPES = actionTypeList.ToArray();
            KNOWN_FUNC_TYPES = new Type[0];
        }

        #endregion Constructors
    }
}
