// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// List of states of a compare process.
    /// </summary>
    public enum CompareState
    {
        /// <summary>
        /// In progress
        /// </summary>
        InProgress,

        /// <summary>
        /// Same / does match
        /// </summary>
        Match,

        /// <summary>
        /// Different items
        /// </summary>
        Different,

        /// <summary>
        /// Failed
        /// </summary>
        Failed,
    }
}