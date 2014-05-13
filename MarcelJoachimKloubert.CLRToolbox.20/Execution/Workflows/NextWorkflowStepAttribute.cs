// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// Stores that member should be invoked next.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,
                    AllowMultiple = true,
                    Inherited = false)]
    public sealed class NextWorkflowStepAttribute : WorkflowAttributeBase
    {
        #region Fields (1)

        private readonly string _MEMBER;

        #endregion

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="NextWorkflowStepAttribute"/> class.
        /// </summary>
        /// <param name="member">The value for the <see cref="NextWorkflowStepAttribute.Member" /> property.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="member" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="member" /> is <see langword="null" />.
        /// </exception>
        public NextWorkflowStepAttribute(string member)
            : this(member, (string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextWorkflowStepAttribute"/> class.
        /// </summary>
        /// <param name="member">The value for the <see cref="NextWorkflowStepAttribute.Member" /> property.</param>
        /// <param name="contractName">The value for the <see cref="WorkflowAttributeBase.Contract" /> property.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="member" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="member" /> is <see langword="null" />.
        /// </exception>
        public NextWorkflowStepAttribute(string member, string contractName)
            : base(contractName)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member");
            }

            this._MEMBER = member.Trim();
            if (member == string.Empty)
            {
                throw new ArgumentException("member");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextWorkflowStepAttribute"/> class.
        /// </summary>
        /// <param name="member">The value for the <see cref="NextWorkflowStepAttribute.Member" /> property.</param>
        /// <param name="contract">The value for the <see cref="WorkflowAttributeBase.Contract" /> property.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="member" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="member" /> is <see langword="null" />.
        /// </exception>
        public NextWorkflowStepAttribute(string member, Type contract)
            : this(member, GetContractName(contract))
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the name of the contract.
        /// </summary>
        public string Member
        {
            get { return this._MEMBER; }
        }

        #endregion Properties
    }
}