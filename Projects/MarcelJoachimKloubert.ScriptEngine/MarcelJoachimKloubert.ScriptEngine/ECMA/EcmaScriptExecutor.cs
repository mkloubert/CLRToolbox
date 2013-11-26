// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
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
        public EcmaScriptExecutor(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcmaScriptExecutor" /> class.
        /// </summary>
        public EcmaScriptExecutor()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (2) 

        /// <summary>
        /// Creates a instance of a <see cref="EcmaScriptExecutor" /> that is set upped with a
        /// <see cref="CommonScriptFunctionSetup{TExecutor}" /> instance.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static EcmaScriptExecutor CreateCommonInstance()
        {
            CommonScriptFunctionSetup<EcmaScriptExecutor> setup;
            return CreateCommonInstance(out setup);
        }

        /// <summary>
        /// Creates a instance of a <see cref="EcmaScriptExecutor" /> that is set upped with a
        /// <see cref="CommonScriptFunctionSetup{TExecutor}" /> instance.
        /// </summary>
        /// <param name="setup">
        /// The variable where to write the <see cref="CommonScriptFunctionSetup{TExecutor}" /> instance
        /// that is / was used to setup the result <see cref="EcmaScriptExecutor" /> object.
        /// </param>
        /// <returns>The created instance.</returns>
        public static EcmaScriptExecutor CreateCommonInstance(out CommonScriptFunctionSetup<EcmaScriptExecutor> setup)
        {
            var result = new EcmaScriptExecutor();

            setup = CommonScriptFunctionSetup.Create(result)
                                             .SetupAll();

            return result;
        }
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
                    if (StringHelper.IsNullOrWhiteSpace(context.Source))
                    {
                        // nothing to do
                        return;
                    }

                    const string NAME_INCLUDE = "include";
                    const string NAME_INCLUDE_FILES = "include_files";
                    const string NAME_LOAD_MODULES = "load_modules";

                    var hasCustomInclude = false;
                    var hasCustomIncludeFiles = false;
                    var hasCustomLoadModules = false;

                    Action<string> checkForDefaultFuncNames = (name) =>
                        {
                            switch (name)
                            {
                                case NAME_INCLUDE:
                                    hasCustomInclude = true;
                                    break;

                                case NAME_INCLUDE_FILES:
                                    hasCustomIncludeFiles = true;
                                    break;

                                case NAME_LOAD_MODULES:
                                    hasCustomLoadModules = true;
                                    break;
                            }
                        };

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
                        checkForDefaultFuncNames(varName);

                        var value = item.Value;

                        comp.Globals
                            .SetVariable(varName,
                                         value);
                    }

                    // functions
                    foreach (var item in this._FUNCS)
                    {
                        var funcName = item.Key;
                        checkForDefaultFuncNames(funcName);

                        var func = item.Value;

                        comp.Globals
                            .SetVariable(funcName,
                                         func);
                    }

                    // include
                    if (!hasCustomInclude)
                    {
                        comp.Globals
                            .SetVariable(NAME_INCLUDE,
                                         new SimplePredicate((args) =>
                                         {
                                             try
                                             {
                                                 // each argument is script code to include
                                                 foreach (var a in args)
                                                 {
                                                     var src = StringHelper.AsString(a, true);
                                                     comp.Include(null, src);
                                                 }

                                                 return true;
                                             }
                                             catch
                                             {
                                                 return false;
                                             }
                                         }));
                    }

                    // include_files
                    if (!hasCustomIncludeFiles)
                    {
                        comp.Globals
                            .SetVariable(NAME_INCLUDE_FILES,
                                         new SimplePredicate((args) =>
                                         {
                                             try
                                             {
                                                 // each argument is a script file
                                                 foreach (var a in args)
                                                 {
                                                     var srcFile = StringHelper.AsString(a, true);
                                                     var file = new FileInfo(srcFile);

                                                     var src = File.ReadAllText(file.FullName, Encoding.UTF8);
                                                     comp.Include(file.FullName, StringHelper.AsString(src, true));
                                                 }

                                                 return true;
                                             }
                                             catch
                                             {
                                                 return false;
                                             }
                                         }));
                    }

                    // load_modules
                    if (!hasCustomLoadModules)
                    {
                        comp.Globals
                            .SetVariable(NAME_LOAD_MODULES,
                                         new SimplePredicate((args) =>
                                         {
                                             try
                                             {
                                                 // each argument is a module file
                                                 foreach (var a in args)
                                                 {
                                                     var modFile = StringHelper.AsString(a, true);
                                                     var asmFile = new FileInfo(modFile);

                                                     var asm = Assembly.LoadFile(asmFile.FullName);

                                                     IDictionary<string, Delegate> functionsToRegister;
                                                     IDictionary<Type, string> typesToExpose;
                                                     this.ExportTypesAndFunctions(asm,
                                                                                  out functionsToRegister,
                                                                                  out typesToExpose);

                                                     // expose types
                                                     foreach (var item in typesToExpose)
                                                     {
                                                         string name = item.Value;
                                                         if (name == null)
                                                         {
                                                             // use type name
                                                             name = item.Key.Name;
                                                         }

                                                         comp.ExposeType(item.Key,
                                                                         name);
                                                     }

                                                     // register functions
                                                     foreach (var item in functionsToRegister)
                                                     {
                                                         comp.Globals
                                                             .SetVariable(item.Key,
                                                                          item.Value);
                                                     }
                                                 }

                                                 return true;
                                             }
                                             catch
                                             {
                                                 return false;
                                             }
                                         }));
                    }

                    comp.Source = context.Source;
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
