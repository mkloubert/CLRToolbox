// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    #region DELEGATE: WorkflowFunc

    /// <summary>
    /// Describes a function / methods that is a function of a workflow.
    /// </summary>
    /// <returns>The context that is / was in use.</returns>
    public delegate IWorkflowExecutionContext WorkflowFunc();

    #endregion DELEGATE: WorkflowFunc

    #region INTERFACE: IWorkflow

    /// <summary>
    /// Describes a workflow.
    /// </summary>
    public interface IWorkflow : ITMObject, IEnumerable<WorkflowFunc>
    {
        #region Properties (1)

        /// <summary>
        /// Gets the object for thread safe operations.
        /// </summary>
        object SyncRoot { get; }

        #endregion INTERFACE: IWorkflow

        #region Methods (1)

        /// <summary>
        /// Executes the workflow.
        /// </summary>
        /// <returns>The result of the execution.</returns>
        object Execute();

        #endregion Methods
    }

    #endregion
}