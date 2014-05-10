// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// A workflow that is based on delegates.
    /// </summary>
    public class DelegateWorkflow : TMObject, IWorkflow
    {
        #region Fields (1)

        private readonly bool _IS_THREAD_SAFE;

        #endregion Fields

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

        #region Methods (9)

        // Public Methods (4) 

        /// <summary>
        /// Starts exeution of a delegate based workflow.
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
        /// Starts exeution of a delegate based workflow.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="startAction">The start action.</param>
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
        /// Starts exeution of a delegate based workflow.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="startAction">The start action.</param>
        /// <param name="actionStateFactory">The function that provides the state object for <paramref name="startAction" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="startAction" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        public void Execute<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            if (startAction == null)
            {
                throw new ArgumentNullException("startAction");
            }

            if (actionStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

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
            return this.GetActionIterator()
                       .GetEnumerator();
        }

        // Protected Methods (1) 

        /// <summary>
        /// Returns the iterator for <see cref="DelegateWorkflow.GetEnumerator()" />.
        /// </summary>
        /// <returns>The iterator.</returns>
        protected virtual IEnumerable<Action> GetActionIterator()
        {
            yield break;
        }

        // Private Methods (3) 

        private void Execute_NonThreadSafe<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            List<Exception> occuredErrors = new List<Exception>();

            WorkflowAction<S> currentAction = startAction;
            Dictionary<string, object> globalVars = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, false));
            long index = -1;
            IReadOnlyDictionary<string, object> previousVars = null;
            object syncRoot = new object();
            bool throwErrors = true;
            while (currentAction != null)
            {
                SimpleWorkflowExecutionContext<S> ctx = new SimpleWorkflowExecutionContext<S>();
                ctx.ContinueOnError = false;
                ctx.GlobalVars = globalVars;
                ctx.Index = ++index;
                ctx.Next = null;
                ctx.NextVars = new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, false));
                ctx.PreviousVars = previousVars;
                ctx.State = actionStateFactory(ctx.Index);
                ctx.SyncRoot = syncRoot;
                ctx.ThrowErrors = throwErrors;
                ctx.Workflow = this;

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

                currentAction = ctx.Next;
            }

            if (throwErrors && occuredErrors.Count > 0)
            {
                throw new AggregateException(occuredErrors);
            }
        }

        private void Execute_ThreadSafe<S>(WorkflowAction<S> startAction, Func<long, S> actionStateFactory)
        {
            lock (this._SYNC)
            {
                this.Execute_NonThreadSafe<S>(startAction, actionStateFactory);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods
    }
}