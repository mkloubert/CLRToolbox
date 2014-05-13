// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// Marks a member as start point for a workflow.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,
                    AllowMultiple = true,
                    Inherited = false)]
    public sealed class WorkflowStartAttribute : WorkflowAttributeBase
    {
        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowStartAttribute"/> class.
        /// </summary>
        /// <param name="contract">The value for the <see cref="WorkflowAttributeBase.Contract" /> property.</param>
        public WorkflowStartAttribute(Type contract)
            : base(contract)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowStartAttribute"/> class.
        /// </summary>
        /// <param name="contractName">The value for the <see cref="WorkflowAttributeBase.Contract" /> property.</param>
        public WorkflowStartAttribute(string contractName)
            : base(contractName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowStartAttribute"/> class.
        /// </summary>
        public WorkflowStartAttribute()
            : base()
        {
        }

        #endregion Constructors
    }
}