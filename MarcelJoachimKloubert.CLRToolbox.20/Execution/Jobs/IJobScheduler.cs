// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Jobs
{
    /// <summary>
    /// Describes an object that handles scheduled jobs.
    /// </summary>
    public interface IJobScheduler : ITMDisposable, IRunnable, IInitializable, INotificationObject
    {
    }
}