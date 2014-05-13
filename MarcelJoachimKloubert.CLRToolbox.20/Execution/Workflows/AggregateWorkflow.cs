// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// A workflow that manages a list of workflows that are called as flatten steps.
    /// </summary>
    public class AggregateWorkflow : WorkflowBase
    {
        #region Fields (1)

        private readonly List<IWorkflow> _WORKFLOWS = new List<IWorkflow>();

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateWorkflow" /> class.
        /// </summary>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public AggregateWorkflow()
            : base(true)
        {
        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Adds a new workflow.
        /// </summary>
        /// <param name="wf">The workflow to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="wf" /> is <see langword="null" />.
        /// </exception>
        public void Add(IWorkflow wf)
        {
            lock (this._SYNC)
            {
                if (wf == null)
                {
                    throw new ArgumentNullException("wf");
                }

                this._WORKFLOWS.Add(wf);
            }
        }

        /// <summary>
        /// Removes all workflows.
        /// </summary>
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._WORKFLOWS.Clear();
            }
        }

        /// <summary>
        /// Returns a list of all currently stored workflows.
        /// </summary>
        public List<IWorkflow> GetWorkflows()
        {
            List<IWorkflow> result;

            lock (this._SYNC)
            {
                result = new List<IWorkflow>(this._WORKFLOWS);
            }

            return result;
        }

        /// <summary>
        /// Removes a new workflow.
        /// </summary>
        /// <param name="wf">The workflow to remove.</param>
        /// <returns>Item was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="wf" /> is <see langword="null" />.
        /// </exception>
        public bool Remove(IWorkflow wf)
        {
            bool result;

            lock (this._SYNC)
            {
                if (wf == null)
                {
                    throw new ArgumentNullException("wf");
                }

                result = this._WORKFLOWS.Remove(wf);
            }

            return result;
        }

        // Public Methods (5) 

        /// <inheriteddoc />
        protected override IEnumerable<WorkflowFunc> GetFunctionIterator()
        {
            IEnumerable<WorkflowFunc> result;

            lock (this._SYNC)
            {
                result = CollectionHelper.SelectMany<WorkflowFunc>(this._WORKFLOWS);
            }

            return result;
        }

        #endregion Methods
    }
}