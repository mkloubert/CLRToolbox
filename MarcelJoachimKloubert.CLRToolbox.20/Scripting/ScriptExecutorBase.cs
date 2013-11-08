// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

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

        #region Methods (8)

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
        // Protected Methods (1) 

        /// <summary>
        /// The logic for the <see cref="ScriptExecutorBase.Execute(IEnumerable{char}, bool, bool)" /> method.
        /// </summary>
        /// <param name="context">The execution context.</param>
        protected abstract void OnExecute(OnExecuteContext context);

        #endregion Methods
    }
}
