﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// A basic workflow.
    /// </summary>
    public abstract class WorkflowBase : TMObject, IWorkflow
    {
        #region Fields (2)

        private readonly bool _IS_THREAD_SAFE;
        private readonly IDictionary<string, object> _VARS;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowBase" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public WorkflowBase(bool isThreadSafe, object syncRoot)
            : base(syncRoot)
        {
            this._IS_THREAD_SAFE = isThreadSafe;
            this._VARS = this.CreateVarStorage() ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowBase" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        public WorkflowBase(bool isThreadSafe)
            : this(isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public WorkflowBase(object syncRoot)
            : this(true, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowBase" /> class.
        /// </summary>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public WorkflowBase()
            : this(new object())
        {
        }

        #endregion Constructors

        #region Properties (4)

        /// <summary>
        /// Gets if that workflow workds thread safe or not.
        /// </summary>
        public bool Synchronized
        {
            get { return this._IS_THREAD_SAFE; }
        }

        /// <inheriteddoc />
        public object SyncRoot
        {
            get { return this._SYNC; }
        }

        /// <summary>
        /// Gets or sets a value of the variables of that workflow.
        /// </summary>
        /// <param name="name">The name of the var.</param>
        /// <returns>The value of the var.</returns>
        /// <remarks>
        /// The variable names are NOT case sensitive.
        /// </remarks>
        public object this[IEnumerable<char> name]
        {
            get { return this.Vars[SimpleWorkflowExecutionContext.ParseVarName(name)]; }

            set { this.Vars[SimpleWorkflowExecutionContext.ParseVarName(name)] = value; }
        }

        /// <summary>
        /// Gets the variables of that workflow as dictionary.
        /// </summary>
        /// <remarks>
        /// The variable names are NOT case sensitive.
        /// </remarks>
        public IDictionary<string, object> Vars
        {
            get { return this._VARS; }
        }

        #endregion Properties

        #region Methods (14)

        // Public Methods (7) 

        /// <inheriteddoc />
        public object Execute(IEnumerable<object> args)
        {
            return this.Execute(CollectionHelper.AsArray(args));
        }

        /// <inheriteddoc />
        public object Execute(params object[] args)
        {
            Func<object[], object> funcToInvoke;
            if (this.Synchronized)
            {
                funcToInvoke = this.Execute_ThreadSafe;
            }
            else
            {
                funcToInvoke = this.Execute_NonThreadSafe;
            }

            return funcToInvoke(args);
        }

        /// <inheriteddoc />
        public IEnumerator<WorkflowFunc> GetEnumerator()
        {
            Func<IEnumerator<WorkflowFunc>> funcToInvoke;
            if (this._IS_THREAD_SAFE)
            {
                funcToInvoke = this.GetEnumerator_ThreadSafe;
            }
            else
            {
                funcToInvoke = this.GetEnumerator_NonThreadSafe;
            }

            return funcToInvoke();
        }

        /// <summary>
        /// Returns a value of <see cref="WorkflowBase.Vars" /> property strong typed.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="name">The name of the var.</param>
        /// <returns>The strong typed version of var.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="name" /> does not exist.</exception>
        public T GetVar<T>(IEnumerable<char> name)
        {
            T result;
            if (this.TryGetVar<T>(name, out result))
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        /// <summary>
        /// Tries to return a value of <see cref="WorkflowBase.Vars" /> property strong typed.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="name">The name of the var.</param>
        /// <param name="value">The field where to write the found value to.</param>
        /// <returns>Var exists or not.</returns>
        public bool TryGetVar<T>(IEnumerable<char> name, out T value)
        {
            return this.TryGetVar<T>(name, out value, default(T));
        }

        /// <summary>
        /// Tries to return a value of <see cref="WorkflowBase.Vars" /> property strong typed.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="name">The name of the var.</param>
        /// <param name="value">The field where to write the found value to.</param>
        /// <param name="defaultValue">
        /// The default value for <paramref name="name" />
        /// if <paramref name="value" /> does not exist.
        /// </param>
        /// <returns>Var exists or not.</returns>
        public bool TryGetVar<T>(IEnumerable<char> name, out T value, T defaultValue)
        {
            return this.TryGetVar<T>(name, out value,
                                     delegate(string varName)
                                     {
                                         return defaultValue;
                                     });
        }

        /// <summary>
        /// Tries to return a value of <see cref="WorkflowBase.Vars" /> property strong typed.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="name">The name of the var.</param>
        /// <param name="value">The field where to write the found value to.</param>
        /// <param name="defaultValueProvider">
        /// The logic that produces the default value for <paramref name="name" />
        /// if <paramref name="value" /> does not exist.
        /// </param>
        /// <returns>Var exists or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultValueProvider" /> is <see langword="null" />.
        /// </exception>
        public bool TryGetVar<T>(IEnumerable<char> name, out T value, Func<string, T> defaultValueProvider)
        {
            if (defaultValueProvider == null)
            {
                throw new ArgumentNullException("defaultValueProvider");
            }

            lock (this.SyncRoot)
            {
                IDictionary<string, object> dict = this.Vars;
                if (dict != null)
                {
                    object foundValue;
                    if (dict.TryGetValue(SimpleWorkflowExecutionContext.ParseVarName(name), out foundValue))
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

        // Protected Methods (2) 
        
        /// <summary>
        /// Creates an empty storage for variables.
        /// </summary>
        /// <returns>The created storage.</returns>
        protected virtual IDictionary<string, object> CreateVarStorage()
        {
            return new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, true));
        }

        /// <summary>
        /// Returns the iterator for <see cref="WorkflowBase.GetEnumerator()" />.
        /// </summary>
        /// <returns>The iterator.</returns>
        protected virtual IEnumerable<WorkflowFunc> GetFunctionIterator()
        {
            yield break;
        }

        // Private Methods (5) 

        private object Execute_NonThreadSafe(object[] args)
        {
            object result = null;
            CollectionHelper.ForEach(this,
                                     delegate(IForEachItemExecutionContext<WorkflowFunc, object[]> ctx)
                                     {
                                         result = ctx.Item(ctx.State);
                                     }, args);

            return result;
        }

        private object Execute_ThreadSafe(object[] args)
        {
            object result = null;

            lock (this._SYNC)
            {
                result = this.Execute_NonThreadSafe(args);
            }

            return result;
        }

        private IEnumerator<WorkflowFunc> GetEnumerator_NonThreadSafe()
        {
            return this.GetFunctionIterator()
                       .GetEnumerator();
        }

        private IEnumerator<WorkflowFunc> GetEnumerator_ThreadSafe()
        {
            IEnumerator<WorkflowFunc> result;

            lock (this._SYNC)
            {
                result = this.GetEnumerator_NonThreadSafe();
            }

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods
    }
}