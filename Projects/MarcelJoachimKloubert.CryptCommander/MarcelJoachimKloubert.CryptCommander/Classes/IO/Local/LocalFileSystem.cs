// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CryptCommander.Classes.IO.Local
{
    /// <summary>
    /// A local file system.
    /// </summary>
    public sealed class LocalFileSystem : TMObject, IFileSystem
    {
        #region Methods (2)

        // Public Methods (1) 

        /// <inheriteddoc />
        public IEnumerable<LocalDrive> GetDrives()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                yield return new LocalDrive(this, drive);
            }
        }
        // Private Methods (1) 

        IEnumerable<IDrive> IFileSystem.GetDrives()
        {
            return this.GetDrives();
        }

        #endregion Methods
    }
}
