// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.FileSyncer.Jobs
{
    /// <summary>
    /// Describes a sync job.
    /// </summary>
    public interface ISyncJob : IHasName, IRunnable
    {

    }
}
