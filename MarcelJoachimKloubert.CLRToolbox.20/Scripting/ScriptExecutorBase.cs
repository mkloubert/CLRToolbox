// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Scripting.Export;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting
{
    /// <summary>
    /// A basic object for execute a script.
    /// </summary>
    public abstract partial class ScriptExecutorBase : TMDisposableBase, IScriptExecutor
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptExecutorBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ScriptExecutorBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptExecutorBase" /> class.
        /// </summary>
        protected ScriptExecutorBase()
            : base()
        {

        }

        #endregion Constructors

        #region Delegates and Events (2)

        // Delegates (2) 

        /// <summary>
        /// Describes a procedure with a variable number of parameters.
        /// </summary>
        /// <param name="args">The parameters for the procedure.</param>
        public delegate void SimpleAction(params object[] args);

        /// <summary>
        /// Describes a function with a variable number of parameters and a variable result type.
        /// </summary>
        /// <param name="args">The parameters for the function.</param>
        /// <returns>The result of the function.</returns>
        public delegate object SimpleFunc(params object[] args);

        #endregion Delegates and Events

        #region Methods (12)

        // Public Methods (7) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.Execute(IEnumerable{char}, bool, bool)" />
        public IScriptExecutionContext Execute(IEnumerable<char> src,
                                               bool autoStart = true,
                                               bool debug = false)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                ScriptExecutionContext result = new ScriptExecutionContext();
                result.Executor = this;
                result.Source = StringHelper.AsString(src);
                result.IsDebug = debug;

                result.StartAction = delegate()
                    {
                        OnExecuteContext onExecCtx = new OnExecuteContext();
                        try
                        {
                            onExecCtx.IsDebug = result.IsDebug;
                            onExecCtx.Source = result.Source;
                            onExecCtx.StartTime = DateTimeOffset.Now;

                            this.OnExecute(onExecCtx);
                        }
                        finally
                        {
                            result.Result = onExecCtx.ScriptResult;
                        }
                    };

                if (autoStart)
                {
                    result.Start();
                }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.ExposeType{T}()" />
        public ScriptExecutorBase ExposeType<T>()
        {
            return this.ExposeType(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.ExposeType{T}(IEnumerable{char})" />
        public ScriptExecutorBase ExposeType<T>(IEnumerable<char> alias)
        {
            return this.ExposeType(typeof(T), alias);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.ExposeType(Type)" />
        public ScriptExecutorBase ExposeType(Type type)
        {
            return this.ExposeType(type, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.ExposeType(Type, IEnumerable{char})" />
        public ScriptExecutorBase ExposeType(Type type, IEnumerable<char> alias)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }

                string typeAlias = StringHelper.AsString(alias);
                this._EXPOSED_TYPES[type] = StringHelper.IsNullOrWhiteSpace(typeAlias) ? null : typeAlias.Trim();

                return this;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.SetFunction(IEnumerable{char}, Delegate)" />
        public ScriptExecutorBase SetFunction(IEnumerable<char> funcName, Delegate func)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (funcName == null)
                {
                    throw new ArgumentNullException("funcName");
                }

                string name = StringHelper.AsString(funcName).Trim();
                if (name == string.Empty)
                {
                    throw new ArgumentException("funcName");
                }

                if (func == null)
                {
                    throw new ArgumentNullException("func");
                }

                this._FUNCS[name] = func;
                return this;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IScriptExecutor.SetVariable(IEnumerable{char}, object)" />
        public ScriptExecutorBase SetVariable(IEnumerable<char> varName, object value)
        {
            lock (this._SYNC)
            {
                this.ThrowIfDisposed();

                if (varName == null)
                {
                    throw new ArgumentNullException("varName");
                }

                string name = StringHelper.AsString(varName).Trim();
                if (name == string.Empty)
                {
                    throw new ArgumentException("varName");
                }

                if (DBNull.Value.Equals(value))
                {
                    value = null;
                }

                this._VARS[name] = value;
                return this;
            }
        }
        // Protected Methods (5) 

        /// <summary>
        /// Exports types and methods from an assembly that are marked with <see cref="ExportScriptTypeAttribute" />
        /// and/or <see cref="ExportScriptFuncAttribute" /> attributes.
        /// </summary>
        /// <param name="asm">The assembly where to search in.</param>
        /// <param name="exportedFuncs">
        /// The variable where to save the exported functions.
        /// The key is the alias.
        /// The value is the delegate.
        /// </param>
        /// <param name="exportedTypes">
        /// The variable where to save the exported types.
        /// The key is the type.
        /// The value is the alias.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asm" /> is <see langword="null" />.
        /// </exception>
        protected virtual void ExportTypesAndFunctions(Assembly asm,
                                                       out Dictionary<string, Delegate> exportedFuncs,
                                                       out Dictionary<Type, string> exportedTypes)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("asm");
            }

            exportedFuncs = new Dictionary<string, Delegate>();
            exportedTypes = new Dictionary<Type, string>();

            if (!this.IsTrustedAssembly(asm))
            {
                // assembly is not trusted
                return;
            }

            foreach (Type type in asm.GetTypes())
            {
                if (!this.IsTrustedType(type))
                {
                    // type is not trusted
                    continue;
                }

                object[] allExpTypeAttribs = type.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Scripting.Export.ExportScriptTypeAttribute),
                                                                      false);
                if (allExpTypeAttribs.LongLength > 0)
                {
                    ExportScriptTypeAttribute expTypeAttrib = (ExportScriptTypeAttribute)allExpTypeAttribs[0];
                    if (StringHelper.IsNullOrWhiteSpace(expTypeAttrib.Alias))
                    {
                        exportedTypes[type] = null;
                    }
                    else
                    {
                        exportedTypes[type] = expTypeAttrib.Alias.Trim();
                    }
                }

                List<MethodInfo> allMethods = new List<MethodInfo>();
                allMethods.AddRange(type.GetMethods(BindingFlags.Public | BindingFlags.Static));
                allMethods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));
                allMethods.AddRange(type.GetMethods(BindingFlags.Public | BindingFlags.Instance));
                allMethods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance));

                object instanceOfType = null;
                foreach (MethodInfo method in allMethods)
                {
                    if (!this.IsTrustedMethod(method))
                    {
                        // method is not trusted
                        continue;
                    }

                    object[] allExpFuncAttribs = method.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Scripting.Export.ExportScriptFuncAttribute),
                                                                            false);
                    if (allExpFuncAttribs.LongLength < 1)
                    {
                        continue;
                    }

                    Type delegateType = MethodHelper.TryGetDelegateTypeFromMethod(method);
                    if (delegateType == null)
                    {
                        continue;
                    }

                    if (!method.IsStatic &&
                        instanceOfType == null)
                    {
                        instanceOfType = Activator.CreateInstance(type);
                    }

                    Delegate @delegate;
                    if (method.IsStatic)
                    {
                        @delegate = Delegate.CreateDelegate(delegateType,
                                                            method);
                    }
                    else
                    {
                        @delegate = Delegate.CreateDelegate(delegateType,
                                                            instanceOfType,
                                                            method);
                    }

                    ExportScriptFuncAttribute expFuncAttrib = (ExportScriptFuncAttribute)allExpFuncAttribs[0];
                    if (StringHelper.IsNullOrWhiteSpace(expFuncAttrib.Alias))
                    {
                        exportedFuncs[method.Name] = @delegate;
                    }
                    else
                    {
                        exportedFuncs[expFuncAttrib.Alias.Trim()] = @delegate;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if an assembly is trusted for that script executor or not.
        /// </summary>
        /// <param name="asm">The assembly to check.</param>
        /// <returns>Is trusted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asm" /> is <see langword="null" />.
        /// </exception>
        protected bool IsTrustedAssembly(Assembly asm)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("asm");
            }

            return true;
        }

        /// <summary>
        /// Checks if a method is trusted for that script executor or not.
        /// </summary>
        /// <param name="method">The method to check.</param>
        /// <returns>Is trusted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="method" /> is <see langword="null" />.
        /// </exception>
        protected bool IsTrustedMethod(MethodBase method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            Type reflType = method.ReflectedType;
            if (!this.IsTrustedType(reflType))
            {
                Type decType = method.DeclaringType;
                if (!this.IsTrustedType(decType))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a type is trusted for that script executor or not.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>Is trusted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        protected bool IsTrustedType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!this.IsTrustedAssembly(type.Assembly))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The logic for the <see cref="ScriptExecutorBase.Execute(IEnumerable{char}, bool, bool)" /> method.
        /// </summary>
        /// <param name="context">The execution context.</param>
        protected abstract void OnExecute(OnExecuteContext context);

        #endregion Methods
    }
}
