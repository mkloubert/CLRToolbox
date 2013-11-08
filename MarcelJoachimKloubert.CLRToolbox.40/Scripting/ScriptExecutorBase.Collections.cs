// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    partial class ScriptExecutorBase
    {
        #region Fields (3)

        /// <summary>
        /// Stores the list of exposed types.
        /// </summary>
        protected readonly IDictionary<Type, string> _EXPOSED_TYPES = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Stores the current function list.
        /// </summary>
        protected readonly IDictionary<string, Delegate> _FUNCS = new ConcurrentDictionary<string, Delegate>();

        /// <summary>
        /// Stores the current variable list.
        /// </summary>
        protected readonly IDictionary<string, object> _VARS = new ConcurrentDictionary<string, object>();

        #endregion Fields
    }
}
