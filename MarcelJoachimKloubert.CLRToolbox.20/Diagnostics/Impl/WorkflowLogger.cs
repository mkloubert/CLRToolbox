// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// A logger that is based on a <see cref="IWorkflow" />.
    /// </summary>
    public sealed class WorkflowLogger : LoggerFacadeBase
    {
        #region Fields (2)

        private readonly ArgumentProvider _PROVIDER_OF_ARGUMENTS;
        private readonly IWorkflow _WORKFLOW;

        #endregion Fields

        #region Constructors (7)

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="argProvider">The value for the <see cref="WorkflowLogger.ProviderOfArguments" /> property.</param>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" />, <paramref name="argProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow, ArgumentProvider argProvider, bool isThreadSafe, object syncRoot)
            : base(isThreadSafe, syncRoot)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }

            if (argProvider == null)
            {
                throw new ArgumentNullException("argProvider");
            }

            this._WORKFLOW = workflow;
            this._PROVIDER_OF_ARGUMENTS = argProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow, bool isThreadSafe, object syncRoot)
            : this(workflow, GetEmptyArguments, isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="argProvider">The value for the <see cref="WorkflowLogger.ProviderOfArguments" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" />, <paramref name="argProvider" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow, ArgumentProvider argProvider, object syncRoot)
            : this(workflow, argProvider, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="argProvider">The value for the <see cref="WorkflowLogger.ProviderOfArguments" /> property.</param>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> and/or <paramref name="argProvider" /> are <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow, ArgumentProvider argProvider, bool isThreadSafe)
            : this(workflow, argProvider, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow, object syncRoot)
            : this(workflow, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> is <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow, bool isThreadSafe)
            : this(workflow, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> is <see langword="null" />.
        /// </exception>
        public WorkflowLogger(IWorkflow workflow)
            : this(workflow, false)
        {
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets function that provides the the arguments for each function of <see cref="WorkflowLogger.Workflow" />.
        /// </summary>
        public ArgumentProvider ProviderOfArguments
        {
            get { return this._PROVIDER_OF_ARGUMENTS; }
        }

        /// <summary>
        /// Gets the underlying workflow.
        /// </summary>
        public IWorkflow Workflow
        {
            get { return this._WORKFLOW; }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes a function or method that provides the instance of <see cref="WorkflowLogger.Workflow" />.
        /// </summary>
        /// <param name="logger">The underlying logger instance.</param>
        /// <returns>The argument to use.</returns>
        public delegate IEnumerable ArgumentProvider(WorkflowLogger logger);

        #endregion Delegates and Events

        #region Methods (5)

        // Public Methods (3) 

        /// <summary>
        /// Creates a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="args">The arguments to use for each invokation.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static WorkflowLogger Create(IWorkflow workflow, IEnumerable args, object syncRoot)
        {
            return Create(workflow, args, false, syncRoot);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="args">The arguments to use for each invokation.</param>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> is <see langword="null" />.
        /// </exception>
        public static WorkflowLogger Create(IWorkflow workflow, IEnumerable args, bool isThreadSafe)
        {
            return Create(workflow, args, isThreadSafe, new object());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="WorkflowLogger" /> class.
        /// </summary>
        /// <param name="workflow">The value for the <see cref="WorkflowLogger.Workflow" /> property.</param>
        /// <param name="args">The arguments to use for each invokation.</param>
        /// <param name="isThreadSafe">Object is thread safe or not.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static WorkflowLogger Create(IWorkflow workflow, IEnumerable args, bool isThreadSafe, object syncRoot)
        {
            return new WorkflowLogger(workflow,
                                      delegate(WorkflowLogger logger)
                                      {
                                          return args;
                                      }, isThreadSafe
                                       , syncRoot);
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnLog(ILogMessage msg)
        {
            // use instance of 'msg'
            // as first argument for the workflow
            object[] basicArgs = new object[] { msg };

            // additional arguments from 'ProviderOfArguments' property
            IEnumerable<object> additionalArgs = CollectionHelper.AsSequence<object>(this._PROVIDER_OF_ARGUMENTS(this));

            // concat both
            IEnumerable<object> allArgs = basicArgs;
            if (additionalArgs != null)
            {
                allArgs = CollectionHelper.Concat(allArgs,
                                                  additionalArgs);
            }

            CollectionHelper.ForEach(this._WORKFLOW,
                                     delegate(IForEachItemExecutionContext<WorkflowFunc, object[]> ctx)
                                     {
                                         WorkflowFunc func = ctx.Item;
                                         object[] args = ctx.State;

                                         IWorkflowExecutionContext res = func(args);

                                         if (res.HasBeenCanceled)
                                         {
                                             ctx.Cancel = true;
                                         }
                                     }, CollectionHelper.ToArray(additionalArgs));
        }

        // Private Methods (1)

        private static object[] GetEmptyArguments(WorkflowLogger logger)
        {
            return new object[0];
        }

        #endregion Methods
    }
}