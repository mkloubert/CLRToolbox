// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="ExecutionHelper.MakeAsync(IWorkflow)" />
        public static Action MakeAsync(this IWorkflow workflow)
        {
            return ExecutionHelper.MakeAsync(workflow);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="ExecutionHelper.MakeAsync(IWorkflow, Action{IAsyncExecutionResult})" />
        public static Action MakeAsync(this IWorkflow workflow, Action<IAsyncExecutionResult> completedAction)
        {
            return ExecutionHelper.MakeAsync(workflow, completedAction);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="ExecutionHelper.MakeAsync{S}(IWorkflow, S, Action{IAsyncExecutionResult{S}})" />
        public static Action MakeAsync<S>(this IWorkflow workflow, S state, Action<IAsyncExecutionResult<S>> completedAction)
        {
            return ExecutionHelper.MakeAsync(workflow, state, completedAction);
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="ExecutionHelper.MakeAsync{S}(IWorkflow, Func{IWorkflow, S}, Action{IAsyncExecutionResult{S}})" />
        public static Action MakeAsync<S>(this IWorkflow workflow, Func<IWorkflow, S> stateFactory, Action<IAsyncExecutionResult<S>> completedAction)
        {
            return ExecutionHelper.MakeAsync(workflow, stateFactory, completedAction);
        }

        #endregion Methods
    }
}