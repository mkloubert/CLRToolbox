// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.CloudNET.Classes.IO
{
    /// <summary>
    /// Describes an item of a file system.
    /// </summary>
    public interface IFileSystemItem : IHasName
    {
        #region Data Members (1)

        /// <summary>
        /// Returns the full path of that item.
        /// </summary>
        string FullPath { get; }

        #endregion Data Members
    }
}
