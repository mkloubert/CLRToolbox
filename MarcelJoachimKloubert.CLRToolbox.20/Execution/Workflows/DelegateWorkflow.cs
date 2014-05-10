// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

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
    #region CLASS: DelegateWorkflow

    /// <summary>
    /// A workflow that is based on delegates.
    /// </summary>
    public class DelegateWorkflow : TMObject, IWorkflow
    {
        #region Fields (2)

        private readonly bool _IS_THREAD_SAFE;
        private readonly IDictionary<string, object> _VARS;

        #endregion CLASS: DelegateWorkflow

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(bool isThreadSafe, object syncRoot)
            : base(syncRoot)
        {
            this._IS_THREAD_SAFE = isThreadSafe;
            this._VARS = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, false));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        public DelegateWorkflow(bool isThreadSafe)
            : this(isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public DelegateWorkflow(object syncRoot)
            : this(false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow" /> class.
        /// </summary>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public DelegateWorkflow()
            : this(new object())
        {
        }

        #endregion Constructors

        #region Properties (3)

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

        #region Methods (22)

        // Public Methods (13) 

        /// <summary>
        /// Creates a new instance by using a <see cref="WorkflowActionNoState" /> based action.
        /// </summary>
        /// <param name="defaultStartAction">The default action.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> is <see langword="null" />.
        /// </exception>
        public static DelegateWorkflow Create(WorkflowActionNoState defaultStartAction)
        {
            return Create(defaultStartAction, false);
        }

        /// <summary>
        /// Creates a new instance by using a <see cref="WorkflowActionNoState" /> based action.
        /// </summary>
        /// <param name="defaultStartAction">The default action.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> and/or
        /// <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DelegateWorkflow Create(WorkflowActionNoState defaultStartAction, object syncRoot)
        {
            return Create(defaultStartAction, false, syncRoot);
        }

        /// <summary>
        /// Creates a new instance by using a <see cref="WorkflowActionNoState" /> based action.
        /// </summary>
        /// <param name="defaultStartAction">The default action.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> is <see langword="null" />.
        /// </exception>
        public static DelegateWorkflow Create(WorkflowActionNoState defaultStartAction, bool isThreadSafe)
        {
            return Create(defaultStartAction, isThreadSafe, new object());
        }

        /// <summary>
        /// Creates a new instance by using a <see cref="WorkflowActionNoState" /> based action.
        /// </summary>
        /// <param name="defaultStartAction">The default action.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> and/or
        /// <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static DelegateWorkflow Create(WorkflowActionNoState defaultStartAction, bool isThreadSafe, object syncRoot)
        {
            if (defaultStartAction == null)
            {
                throw new ArgumentNullException("defaultStartAction");
            }

            return new DelegateWorkflow<object>(delegate(IWorkflowExecutionContext<object> ctx)
                                                {
                                                    defaultStartAction(ctx);
                                                },
                                                (object)null,
                                                isThreadSafe,
                                                syncRoot);
        }

        /// <inheriteddoc />
        public void Execute()
        {
            Action actionToInvoke;
            if (this._IS_THREAD_SAFE)
            {
                actionToInvoke = this.Execute_ThreadSafe;
            }
            else
            {
                actionToInvoke = this.Execute_NonThreadSafe;
            }

            actionToInvoke();
        }

        /// <summary>
        /// Starts exeution of that workflow by using a custom action.
        /// </summary>
        /// <param name="startAction">The start action.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> is <see langword="null" />.
        /// </exception>
        public void Execute(WorkflowActionNoState startAction)
        {
            if (startAction == null)
            {
                throw new ArgumentNullException("startAction");
            }

            this.Execute<object>(delegate(IWorkflowExecutionContext<object> context)
                                 {
                                     startAction(context);
                                 }, (object)null);
        }

        /// <summary>
        /// Starts exeution of that workflow by using a custom action.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="startAction">The custom start action.</param>
        /// <param name="actionState">The state object for <paramref name="startAction" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> is <see langword="null" />.
        /// </exception>
        public void Execute<S>(WorkflowAction<S> startAction, S actionState)
        {
            this.Execute<S>(startAction,
                            delegate(long index)
                            {
                                return actionState;
                            });
        }

        /// <summary>
        /// Starts exeution of that workflow by using a custom action.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="startAction">The custom start action.</param>
        /// <param name="actionStateFactory">The function that provides the state object for <paramref name="startAction" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        public void Execute<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            Action<WorkflowAction<S>, Func<long, S>> actionToInvoke;
            if (this._IS_THREAD_SAFE)
            {
                actionToInvoke = this.Execute_ThreadSafe;
            }
            else
            {
                actionToInvoke = this.Execute_NonThreadSafe;
            }

            actionToInvoke(startAction, actionStateFactory);
        }

        /// <inheriteddoc />
        public IEnumerator<Action> GetEnumerator()
        {
            Func<IEnumerator<Action>> funcToInvoke;
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
        /// Returns a value of <see cref="DelegateWorkflow.Vars" /> property strong typed.
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
        /// Tries to return a value of <see cref="DelegateWorkflow.Vars" /> property strong typed.
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
        /// Tries to return a value of <see cref="DelegateWorkflow.Vars" /> property strong typed.
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
        /// Tries to return a value of <see cref="DelegateWorkflow.Vars" /> property strong typed.
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
        /// Returns the iterator for <see cref="DelegateWorkflow.GetEnumerator()" />.
        /// </summary>
        /// <returns>The iterator.</returns>
        protected virtual IEnumerable<Action> GetActionIterator()
        {
            yield break;
        }

        /// <summary>
        /// Returns the iterator for <see cref="DelegateWorkflow.GetEnumerator()" />.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="startAction">The start action.</param>
        /// <param name="actionStateFactory">The function that provides the state object for <paramref name="startAction" />.</param>
        /// <returns>The iterator.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        protected IEnumerable<Action> GetActionIterator<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            if (startAction == null)
            {
                throw new ArgumentNullException("startAction");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

            List<Exception> occuredErrors = new List<Exception>();

            WorkflowAction<S> currentAction = startAction;
            Dictionary<string, object> execVars = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, false));
            long index = -1;
            IReadOnlyDictionary<string, object> previousVars = null;
            object syncRoot = new object();
            bool throwErrors = true;
            while (currentAction != null)
            {
                yield return delegate()
                    {
                        SimpleWorkflowExecutionContext<S> ctx = new SimpleWorkflowExecutionContext<S>();
                        ctx.ContinueOnError = false;
                        ctx.ExecutionVars = execVars;
                        ctx.Index = ++index;
                        ctx.Next = null;
                        ctx.NextVars = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, false));
                        ctx.PreviousVars = previousVars;
                        ctx.State = actionStateFactory(ctx.Index);
                        ctx.SyncRoot = syncRoot;
                        ctx.ThrowErrors = throwErrors;
                        ctx.Workflow = this;
                        ctx.WorkflowVars = this.Vars;

                        // execution
                        try
                        {
                            currentAction(ctx);
                        }
                        catch (Exception ex)
                        {
                            occuredErrors.Add(ex);

                            if (ctx.ContinueOnError == false)
                            {
                                throw;
                            }
                        }

                        previousVars = new TMReadOnlyDictionary<string, object>(ctx.NextVars);
                        throwErrors = ctx.ThrowErrors;

                        currentAction = ((IWorkflowExecutionContext<S>)ctx).Next;
                    };
            }

            if (throwErrors && occuredErrors.Count > 0)
            {
                throw new AggregateException(occuredErrors);
            }
        }

        // Private Methods (7) 

        private void Execute_NonThreadSafe()
        {
            CollectionHelper.ForEach(this,
                                     delegate(IForEachItemExecutionContext<Action> ctx)
                                     {
                                         ctx.Item();
                                     });
        }

        private void Execute_NonThreadSafe<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            CollectionHelper.ForEach(this.GetActionIterator<S>(startAction, actionStateFactory),
                                     delegate(IForEachItemExecutionContext<Action> ctx)
                                     {
                                         ctx.Item();
                                     });
        }

        private void Execute_ThreadSafe()
        {
            lock (this._SYNC)
            {
                this.Execute_NonThreadSafe();
            }
        }

        private void Execute_ThreadSafe<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            lock (this._SYNC)
            {
                this.Execute_NonThreadSafe<S>(startAction, actionStateFactory);
            }
        }

        private IEnumerator<Action> GetEnumerator_NonThreadSafe()
        {
            return this.GetActionIterator()
                       .GetEnumerator();
        }

        private IEnumerator<Action> GetEnumerator_ThreadSafe()
        {
            IEnumerator<Action> result;

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

    #endregion

    #region CLASS: DelegateWorkflow<TState>

    /// <summary>
    /// A <see cref="DelegateWorkflow" /> with a default start action.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state object for the context that is used by <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.
    /// </typeparam>
    public class DelegateWorkflow<TState> : DelegateWorkflow
    {
        #region Fields (2)

        private readonly Func<long, TState> _DEFAULT_ACTION_STATE_FACTORY;
        private readonly WorkflowAction<TState> _DEFAULT_START_ACTION;

        #endregion Fields

        #region Constructors (9)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow{TState}" /> class.
        /// </summary>
        /// <param name="defaultStartAction">The value for the <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.</param>
        /// <param name="defaultActionStateFactory">The value for the <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" />, <paramref name="defaultActionStateFactory" /> and/or
        /// <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(WorkflowAction<TState> defaultStartAction, Func<long, TState> defaultActionStateFactory,
                                bool isThreadSafe, object syncRoot)
            : base(isThreadSafe, syncRoot)
        {
            if (defaultStartAction == null)
            {
                throw new ArgumentNullException("defaultStartAction");
            }

            if (defaultActionStateFactory == null)
            {
                throw new ArgumentNullException("defaultActionStateFactory");
            }

            this._DEFAULT_START_ACTION = defaultStartAction;
            this._DEFAULT_ACTION_STATE_FACTORY = defaultActionStateFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow{TState}" /> class.
        /// </summary>
        /// <param name="defaultStartAction">The value for the <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.</param>
        /// <param name="defaultActionState">The value that is returned by <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" />.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> and/or
        /// <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(WorkflowAction<TState> defaultStartAction, TState defaultActionState,
                                bool isThreadSafe, object syncRoot)
            : this(defaultStartAction, delegate(long index) { return defaultActionState; },
                   isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow{TState}" /> class.
        /// </summary>
        /// <param name="defaultStartAction">The value for the <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.</param>
        /// <param name="defaultActionStateFactory">The value for the <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" />, <paramref name="defaultActionStateFactory" /> and/or
        /// <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(WorkflowAction<TState> defaultStartAction, Func<long, TState> defaultActionStateFactory,
                                object syncRoot)
            : this(defaultStartAction, defaultActionStateFactory, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow{TState}" /> class.
        /// </summary>
        /// <param name="defaultStartAction">The value for the <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.</param>
        /// <param name="defaultActionStateFactory">The value for the <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> and/or
        /// <paramref name="defaultActionStateFactory" /> are <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(WorkflowAction<TState> defaultStartAction, Func<long, TState> defaultActionStateFactory,
                                bool isThreadSafe)
            : this(defaultStartAction, defaultActionStateFactory, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow{TState}" /> class.
        /// </summary>
        /// <param name="defaultStartAction">The value for the <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.</param>
        /// <param name="defaultActionState">The value should be returned by <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" />.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> and/or
        /// <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(WorkflowAction<TState> defaultStartAction, TState defaultActionState,
                                object syncRoot)
            : this(defaultStartAction, defaultActionState, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow{TState}" /> class.
        /// </summary>
        /// <param name="defaultStartAction">The value for the <see cref="DelegateWorkflow{TState}.DefaultStartAction" /> property.</param>
        /// <param name="defaultActionState">The value should be returned by <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" />.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="defaultStartAction" /> is <see langword="null" />.
        /// </exception>
        public DelegateWorkflow(WorkflowAction<TState> defaultStartAction, TState defaultActionState,
                                bool isThreadSafe)
            : this(defaultStartAction, defaultActionState, isThreadSafe, new object())
        {
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the action that returns the state object for the context of <see cref="DelegateWorkflow{TState}.DefaultStartAction" />.
        /// </summary>
        public Func<long, TState> DefaultActionStateFactory
        {
            get { return this._DEFAULT_ACTION_STATE_FACTORY; }
        }

        /// <summary>
        /// Gets the default start action.
        /// </summary>
        public WorkflowAction<TState> DefaultStartAction
        {
            get { return this._DEFAULT_START_ACTION; }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 
        
        /// <summary>
        /// Starts exeution of that workflow by using a custom action.
        /// </summary>
        /// <param name="startAction">The custom start action.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <see cref="DelegateWorkflow{TState}.DefaultActionStateFactory" /> is used to generate the state
        /// object for the context argument of <paramref name="startAction" />.
        /// </remarks>
        public void Execute(WorkflowAction<TState> startAction)
        {
            this.Execute<TState>(startAction, this.DefaultActionStateFactory);
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override IEnumerable<Action> GetActionIterator()
        {
            return this.GetActionIterator<TState>(this.DefaultStartAction,
                                                  this.DefaultActionStateFactory);
        }

        #endregion Methods
    }

    #endregion
}