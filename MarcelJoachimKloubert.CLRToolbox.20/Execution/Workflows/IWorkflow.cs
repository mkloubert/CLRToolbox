// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows
{
    /// <summary>
    /// Describes a workflow.
    /// </summary>
    public interface IWorkflow : ITMObject, IEnumerable<Action>
    {
    }
}