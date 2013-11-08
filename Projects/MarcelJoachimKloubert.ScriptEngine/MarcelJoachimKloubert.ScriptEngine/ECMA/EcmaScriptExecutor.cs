// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Scripting;
using RemObjects.Script;

namespace MarcelJoachimKloubert.ScriptEngine.ECMA
{
    /// <summary>
    /// A script executor using ECMA Script syntax.
    /// </summary>
    public class EcmaScriptExecutor : ScriptExecutorBase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="EcmaScriptExecutor" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected EcmaScriptExecutor(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcmaScriptExecutor" /> class.
        /// </summary>
        protected EcmaScriptExecutor()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TMDisposableBase.OnDispose(bool)"/>
        protected override void OnDispose(bool disposing)
        {
            // dummy
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ScriptExecutorBase.OnExecute(ScriptExecutorBase.OnExecuteContext)" />
        protected override void OnExecute(ScriptExecutorBase.OnExecuteContext context)
        {
            using (var comp = new EcmaScriptComponent())
            {
                try
                {
                    if (StringHelper.IsNullOrWhiteSpace(comp.Source))
                    {
                        // nothing to do
                        return;
                    }

                    // exposed types
                    foreach (var item in this._EXPOSED_TYPES)
                    {
                        var type = item.Key;
                        var name = item.Value;

                        comp.ExposeType(type,
                                        name == null ? type.Name : name);
                    }

                    // variables
                    foreach (var item in this._VARS)
                    {
                        var varName = item.Key;
                        var value = item.Value;

                        comp.Globals
                            .SetVariable(varName,
                                         value);
                    }

                    // functions
                    foreach (var item in this._FUNCS)
                    {
                        var funcName = item.Key;
                        var func = item.Value;

                        comp.Globals
                            .SetVariable(funcName,
                                         func);
                    }

                    comp.Source = comp.Source;
                    comp.Run();
                }
                finally
                {
                    context.ScriptResult = comp.RunResult;
                }
            }
        }

        #endregion Methods
    }
}
