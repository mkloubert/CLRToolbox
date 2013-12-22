// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IOHelper.CreateDirectoryDeep(DirectoryInfo)" />
        public static DirectoryInfo CreateDirectoryDeep(this DirectoryInfo dir)
        {
            return IOHelper.CreateDirectoryDeep(dir);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IOHelper.CreateDirectoryDeep(DirectoryInfo, bool)" />
        public static DirectoryInfo CreateDirectoryDeep(this DirectoryInfo dir,
                                                        bool refreshBefore)
        {
            return IOHelper.CreateDirectoryDeep(dir,
                                                refreshBefore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IOHelper.CreateDirectoryDeep(DirectoryInfo, bool, bool)" />
        public static DirectoryInfo CreateDirectoryDeep(this DirectoryInfo dir,
                                                        bool refreshBefore,
                                                        bool refreshAfter)
        {
            return IOHelper.CreateDirectoryDeep(dir,
                                                refreshBefore,
                                                refreshAfter);
        }

        #endregion Methods
    }
}
