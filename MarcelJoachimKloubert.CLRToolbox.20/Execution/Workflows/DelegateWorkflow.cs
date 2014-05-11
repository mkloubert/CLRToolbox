// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    #region CLASS: DelegateWorkflow

    /// <summary>
    /// A workflow that is based on delegates.
    /// </summary>
    public class DelegateWorkflow : WorkflowBase
    {
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
            : base(isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        public DelegateWorkflow(bool isThreadSafe)
            : base(isThreadSafe)
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
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateWorkflow" /> class.
        /// </summary>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public DelegateWorkflow()
            : base()
        {
        }

        #endregion CLASS: DelegateWorkflow

        #region Methods (10)

        // Public Methods (7) 

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

        /// <summary>
        /// Starts exeution of that workflow by using a custom action.
        /// </summary>
        /// <param name="startAction">The start action.</param>
        /// <returns>The result of the last execution.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> is <see langword="null" />.
        /// </exception>
        public object Execute(WorkflowActionNoState startAction)
        {
            if (startAction == null)
            {
                throw new ArgumentNullException("startAction");
            }

            return this.Execute<object>(delegate(IWorkflowExecutionContext<object> context)
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
        /// <returns>The result of the last execution.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> is <see langword="null" />.
        /// </exception>
        public object Execute<S>(WorkflowAction<S> startAction, S actionState)
        {
            return this.Execute<S>(startAction,
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
        /// <returns>The result of the last execution.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        public object Execute<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            Func<WorkflowAction<S>, Func<long, S>, object> funcToInvoke;
            if (this.Synchronized)
            {
                funcToInvoke = this.Execute_ThreadSafe;
            }
            else
            {
                funcToInvoke = this.Execute_NonThreadSafe;
            }

            return funcToInvoke(startAction, actionStateFactory);
        }

        // Protected Methods (1) 

        /// <summary>
        /// Returns the iterator for <see cref="WorkflowBase.GetEnumerator()" />.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="startAction">The start action.</param>
        /// <param name="actionStateFactory">The function that provides the state object for <paramref name="startAction" />.</param>
        /// <returns>The iterator.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        protected IEnumerable<WorkflowFunc> GetFunctionIterator<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
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
            object result = null;
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
                        ctx.Result = result;
                        ctx.State = actionStateFactory(ctx.Index);
                        ctx.SyncRoot = syncRoot;
                        ctx.ThrowErrors = throwErrors;
                        ctx.Workflow = this;
                        ctx.WorkflowVars = this.Vars;

                        // execution
                        try
                        {
                            currentAction(ctx);

                            result = ctx.Result;
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

                        return ctx;
                    };
            }

            if (throwErrors && occuredErrors.Count > 0)
            {
                throw new AggregateException(occuredErrors);
            }
        }

        // Private Methods (2) 

        private object Execute_NonThreadSafe<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            object result = null;
            CollectionHelper.ForEach(this.GetFunctionIterator<S>(startAction, actionStateFactory),
                                     delegate(IForEachItemExecutionContext<WorkflowFunc> ctx)
                                     {
                                         result = ctx.Item();
                                     });

            return result;
        }

        private object Execute_ThreadSafe<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            object result;

            lock (this._SYNC)
            {
                result = this.Execute_NonThreadSafe<S>(startAction, actionStateFactory);
            }

            return result;
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

        #region Constructors (6)

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
        protected override IEnumerable<WorkflowFunc> GetFunctionIterator()
        {
            return this.GetFunctionIterator<TState>(this.DefaultStartAction,
                                                    this.DefaultActionStateFactory);
        }

        #endregion Methods
    }

    #endregion
}