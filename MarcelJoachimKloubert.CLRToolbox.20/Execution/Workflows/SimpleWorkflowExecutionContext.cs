// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    #region CLASS: SimpleWorkflowExecutionContext

    /// <summary>
    /// Simple implementation of <see cref="IWorkflowExecutionContext" /> interface.
    /// </summary>
    public class SimpleWorkflowExecutionContext : IWorkflowExecutionContext
    {
        #region Fields (9)

        private bool _continueOnError;
        private IDictionary<string, object> _globalVars;
        private long _index;

        /// <summary>
        /// Stores the value for the <see cref="SimpleWorkflowExecutionContext.Next" /> property.
        /// </summary>
        protected Delegate _next;

        private IDictionary<string, object> _nextVars;
        private IReadOnlyDictionary<string, object> _previousVars;
        private object _syncRoot;
        private bool _throwErrors;
        private IWorkflow _workflow;

        #endregion CLASS: SimpleWorkflowExecutionContext

        #region Properties (10)

        /// <inheriteddoc />
        public bool ContinueOnError
        {
            get { return this._continueOnError; }

            set { this._continueOnError = value; }
        }

        /// <inheriteddoc />
        public IDictionary<string, object> GlobalVars
        {
            get { return this._globalVars; }

            set { this._globalVars = value; }
        }

        /// <inheriteddoc />
        public long Index
        {
            get { return this._index; }

            set { this._index = value; }
        }
        
        /// <inheriteddoc />
        public bool IsFirst
        {
            get { return this.Index == 0; }
        }

        /// <inheriteddoc />
        public WorkflowActionNoState Next
        {
            get { return (WorkflowActionNoState)this._next; }

            set { this._next = value; }
        }

        /// <inheriteddoc />
        public IDictionary<string, object> NextVars
        {
            get { return this._nextVars; }

            set { this._nextVars = value; }
        }

        /// <inheriteddoc />
        public IReadOnlyDictionary<string, object> PreviousVars
        {
            get { return this._previousVars; }

            set { this._previousVars = value; }
        }

        /// <inheriteddoc />
        public object SyncRoot
        {
            get { return this._syncRoot; }

            set { this._syncRoot = value; }
        }

        /// <inheriteddoc />
        public bool ThrowErrors
        {
            get { return this._throwErrors; }

            set { this._throwErrors = value; }
        }

        /// <inheriteddoc />
        public IWorkflow Workflow
        {
            get { return this._workflow; }

            set { this._workflow = value; }
        }

        #endregion Properties

        #region Methods (14)

        // Public Methods (13) 

        /// <inheriteddoc />
        public T GetGlobalVar<T>(IEnumerable<char> name)
        {
            T result;
            if (this.TryGetGlobal<T>(name, out result))
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        /// <inheriteddoc />
        public T GetNextVar<T>(IEnumerable<char> name)
        {
            T result;
            if (this.TryGetNextVar<T>(name, out result))
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        /// <inheriteddoc />
        public T GetPreviousVar<T>(IEnumerable<char> name)
        {
            T result;
            if (this.TryGetPreviousVar<T>(name, out result))
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        /// <inheriteddoc />
        public W GetWorkflow<W>() where W : global::MarcelJoachimKloubert.CLRToolbox.Execution.Workflows.IWorkflow
        {
            return GlobalConverter.Current
                                  .ChangeType<W>(this.Workflow);
        }

        /// <inheriteddoc />
        public bool TryGetGlobal<T>(IEnumerable<char> name, out T value)
        {
            return this.TryGetGlobal<T>(name, out value, default(T));
        }

        /// <inheriteddoc />
        public bool TryGetGlobal<T>(IEnumerable<char> name, out T value, T defaultValue)
        {
            return this.TryGetGlobalVar<T>(name, out value,
                                        delegate(string varName)
                                        {
                                            return defaultValue;
                                        });
        }
        
        /// <inheriteddoc />
        public bool TryGetGlobalVar<T>(IEnumerable<char> name, out T value, Func<string, T> defaultValueProvider)
        {
            if (defaultValueProvider == null)
            {
                throw new ArgumentNullException("defaultValueProvider");
            }

            lock (this.SyncRoot)
            {
                IDictionary<string, object> dict = this.GlobalVars;
                if (dict != null)
                {
                    object foundValue;
                    if (dict.TryGetValue(ParseVarName(name), out foundValue))
                    {
                        value = GlobalConverter.Current
                                               .ChangeType<T>(foundValue);

                        return true;
                    }
                }
            }

            value = defaultValueProvider(StringHelper.AsString(name));
            return false;
        }

        /// <inheriteddoc />
        public bool TryGetNextVar<T>(IEnumerable<char> name, out T value)
        {
            return this.TryGetNextVar<T>(name, out value, default(T));
        }

        /// <inheriteddoc />
        public bool TryGetNextVar<T>(IEnumerable<char> name, out T value, T defaultValue)
        {
            return this.TryGetNextVar<T>(name, out value,
                                         delegate(string varName)
                                         {
                                             return defaultValue;
                                         });
        }
        
        /// <inheriteddoc />
        public bool TryGetNextVar<T>(IEnumerable<char> name, out T value, Func<string, T> defaultValueProvider)
        {
            if (defaultValueProvider == null)
            {
                throw new ArgumentNullException("defaultValueProvider");
            }

            lock (this.SyncRoot)
            {
                IDictionary<string, object> dict = this.NextVars;
                if (dict != null)
                {
                    object foundValue;
                    if (dict.TryGetValue(ParseVarName(name), out foundValue))
                    {
                        value = GlobalConverter.Current
                                               .ChangeType<T>(foundValue);

                        return true;
                    }
                }
            }

            value = defaultValueProvider(StringHelper.AsString(name));
            return false;
        }

        /// <inheriteddoc />
        public bool TryGetPreviousVar<T>(IEnumerable<char> name, out T value)
        {
            return this.TryGetPreviousVar<T>(name, out value, default(T));
        }

        /// <inheriteddoc />
        public bool TryGetPreviousVar<T>(IEnumerable<char> name, out T value, T defaultValue)
        {
            return this.TryGetPreviousVar<T>(name, out value,
                                             delegate(string varName)
                                             {
                                                 return defaultValue;
                                             });
        }
        
        /// <inheriteddoc />
        public bool TryGetPreviousVar<T>(IEnumerable<char> name, out T value, Func<string, T> defaultValueProvider)
        {
            if (defaultValueProvider == null)
            {
                throw new ArgumentNullException("defaultValueProvider");
            }

            lock (this.SyncRoot)
            {
                IReadOnlyDictionary<string, object> dict = this.PreviousVars;
                if (dict != null)
                {
                    object foundValue;
                    if (dict.TryGetValue(ParseVarName(name), out foundValue))
                    {
                        value = GlobalConverter.Current
                                               .ChangeType<T>(foundValue);

                        return true;
                    }
                }
            }

            value = defaultValueProvider(StringHelper.AsString(name));
            return false;
        }

        // Private Methods (1) 

        private static string ParseVarName(IEnumerable<char> name)
        {
            return StringHelper.AsString(name) ?? string.Empty;
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: SimpleWorkflowExecutionContext<S>

    /// <summary>
    /// Simple implementation of <see cref="SimpleWorkflowExecutionContext{S}" /> interface.
    /// </summary>
    /// <typeparam name="S">Type of the underlying state objects.</typeparam>
    public sealed class SimpleWorkflowExecutionContext<S> : SimpleWorkflowExecutionContext, IWorkflowExecutionContext<S>
    {
        #region Fields (1)

        private S _state;

        #endregion Fields

        #region Properties (2)

        /// <inheriteddoc />
        public new WorkflowAction<S> Next
        {
            get { return (WorkflowAction<S>)this._next; }

            set { this._next = value; }
        }

        /// <inheriteddoc />
        public S State
        {
            get { return this._state; }

            set { this._state = value; }
        }

        #endregion Properties
    }

    #endregion
}