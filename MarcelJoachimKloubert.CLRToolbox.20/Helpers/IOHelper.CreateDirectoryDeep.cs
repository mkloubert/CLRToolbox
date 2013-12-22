// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class IOHelper
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Creates a directory tree if needed.
        /// </summary>
        /// <param name="dir">The directory to create.</param>
        /// <returns>The instance of <paramref name="dir" />.</returns>
        /// <remarks>
        /// <paramref name="dir" /> will be refreshed BEFORE and AFTER directory and its parents have been created.
        /// </remarks>
        public static DirectoryInfo CreateDirectoryDeep(DirectoryInfo dir)
        {
            return CreateDirectoryDeep(dir,
                                       true);
        }

        /// <summary>
        /// Creates a directory tree if needed.
        /// </summary>
        /// <param name="dir">The directory to create.</param>
        /// <param name="refreshBefore">
        /// Refresh <paramref name="dir" /> BEFORE directory and its parents will be created.
        /// </param>
        /// <returns>The instance of <paramref name="dir" />.</returns>
        /// <remarks>
        /// <paramref name="dir" /> will be refreshed AFTER directory and its parents have been created.
        /// </remarks>
        public static DirectoryInfo CreateDirectoryDeep(DirectoryInfo dir,
                                                        bool refreshBefore)
        {
            return CreateDirectoryDeep(dir,
                                       refreshBefore,
                                       true);
        }

        /// <summary>
        /// Creates a directory tree if needed.
        /// </summary>
        /// <param name="dir">The directory to create.</param>
        /// <param name="refreshBefore">
        /// Refresh <paramref name="dir" /> BEFORE directory and its parents will be created.
        /// </param>
        /// <param name="refreshAfter">
        /// Refresh <paramref name="dir" /> AFTER directory and its parents have been created.
        /// </param>
        /// <returns>The instance of <paramref name="dir" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dir" /> is <see langword="null" />.
        /// </exception>
        public static DirectoryInfo CreateDirectoryDeep(DirectoryInfo dir,
                                                        bool refreshBefore,
                                                        bool refreshAfter)
        {
            if (dir == null)
            {
                throw new ArgumentNullException("dir");
            }

            if (refreshBefore)
            {
                dir.Refresh();
            }

            DirectoryInfo parent = dir.Parent;
            if (parent != null)
            {
                // keep sure that parent exists

                parent = CreateDirectoryDeep(parent,
                                             refreshBefore,
                                             refreshAfter);
            }

            if (!dir.Exists)
            {
                dir.Create();

                if (refreshAfter)
                {
                    dir.Refresh();
                }
            }

            return dir;
        }

        #endregion Methods
    }
}
