// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// Marks a member as start point for a workflow.
    /// </summary>
    public abstract class WorkflowAttributeBase : Attribute
    {
        #region Fields (1)

        private readonly string _CONTRACT;

        #endregion

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowAttributeBase"/> class.
        /// </summary>
        /// <param name="contract">The value for the <see cref="WorkflowAttributeBase.Contract" /> property.</param>
        protected WorkflowAttributeBase(Type contract)
            : this(GetContractName(contract))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowAttributeBase"/> class.
        /// </summary>
        /// <param name="contractName">The value for the <see cref="WorkflowAttributeBase.Contract" /> property.</param>
        protected WorkflowAttributeBase(string contractName)
        {
            this._CONTRACT = ParseContractName(contractName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowAttributeBase"/> class.
        /// </summary>
        protected WorkflowAttributeBase()
            : this((string)null)
        {
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the name of the contract.
        /// </summary>
        public string Contract
        {
            get { return this._CONTRACT; }
        }

        #endregion Properties

        #region Methods (2)

        /// <summary>
        /// Returns the contract name from a <see cref="Type" /> object.
        /// </summary>
        /// <param name="type">The type from where to get the contract name from.</param>
        /// <returns>
        /// The contract name or <see langword="null" /> is <paramref name="type" /> is also <see langword="null" />.
        /// </returns>
        public static string GetContractName(Type type)
        {
            return type != null ? string.Format("{0}\n{1}",
                                                type.Assembly,
                                                type.FullName) : null;
        }

        /// <summary>
        /// Gets the name of the contract.
        /// </summary>
        public static string ParseContractName(IEnumerable<char> contract)
        {
            if (contract == null)
            {
                return null;
            }

            string result = StringHelper.AsString(contract).ToUpper().Trim();
            if (result == string.Empty)
            {
                result = null;
            }

            return result;
        }

        #endregion
    }
}