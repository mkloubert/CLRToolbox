// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

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
            var actionType = typeof(global::System.Action);
            var actionAsm = actionType.Assembly;

            var actionTypeList = new List<Type>();
            actionTypeList.Add(typeof(global::System.Action<>));

            foreach (var type in actionAsm.GetTypes())
            {
                if (type.FullName.StartsWith("System.Action"))
                {
                    actionTypeList.Add(type);
                }
            }

            var funcType = typeof(global::System.Func<>);
            var funcAsm = funcType.Assembly;

            var funcTypeList = new List<Type>();
            foreach (var type in funcAsm.GetTypes())
            {
                if (type.FullName.StartsWith("System.Func"))
                {
                    funcTypeList.Add(type);
                }
            }

            KNOWN_ACTION_TYPES = actionTypeList.ToArray();
            KNOWN_FUNC_TYPES = funcTypeList.ToArray();
        }

        #endregion Constructors
    }
}
