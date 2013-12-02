// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Scripting;
using RemObjects.Script;
using AppServerImpl = MarcelJoachimKloubert.ApplicationServer.ApplicationServer;

namespace MarcelJoachimKloubert.AppServer.Services.Script
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Scripting.IScriptExecutor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class JavaScriptEngine : ScriptExecutorBase
    {
        #region Fields (1)

        private readonly AppServerImpl _SERVER;

        #endregion Fields

        #region Constructors (1)

        [ImportingConstructor]
        internal JavaScriptEngine(AppServerImpl server)
        {
            this._SERVER = server;
        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (2) 

        protected override void OnDispose(bool disposing)
        {

        }

        protected override void OnExecute(ScriptExecutorBase.OnExecuteContext context)
        {
            using (var comp = new EcmaScriptComponent())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(context.Source))
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
                                                     var src = a.AsString(true);
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
                                                     var srcFile = a.AsString(true);
                                                     var file = new FileInfo(srcFile);

                                                     var src = File.ReadAllText(file.FullName, Encoding.UTF8);
                                                     comp.Include(file.FullName, src);
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
                                                     var modFile = a.AsString(true);
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
