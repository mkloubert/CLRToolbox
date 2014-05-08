// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.DragNBatch.PlugIns;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MarcelJoachimKloubert.DragNBatch.ViewModel
{
    internal sealed class HandleFilesContext : IHandleFilesContext
    {
        #region Properties (5)

        public IEnumerable<string> AllDirectories
        {
            get
            {
                var dirs = this.Directories;

                if (dirs == null)
                {
                    yield break;
                }

                foreach (var dirPath in dirs)
                {
                    foreach (var path in CollectDirectories(dirPath))
                    {
                        yield return path;
                    }
                }
            }
        }

        public IEnumerable<string> AllFiles
        {
            get
            {
                var files = this.Files;
                var dirs = this.Directories;

                if (files == null && dirs == null)
                {
                    yield break;
                }

                foreach (var dirPath in dirs)
                {
                    foreach (var filePath in CollectFiles(dirPath))
                    {
                        yield return filePath;
                    }
                }

                foreach (var filePath in files)
                {
                    yield return filePath;
                }
            }
        }

        public CultureInfo Culture
        {
            get;
            internal set;
        }

        public IEnumerable<string> Directories
        {
            get;
            internal set;
        }

        public IEnumerable<string> Files
        {
            get;
            internal set;
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (2) 

        private static IEnumerable<string> CollectDirectories(string dirPath)
        {
            if (Directory.Exists(dirPath) == false)
            {
                yield break;
            }

            var dir = new DirectoryInfo(dirPath);

            // directory itself
            yield return dir.FullName;

            // sub directories
            foreach (var subDir in dir.GetDirectories())
            {
                foreach (var subDirPath in CollectDirectories(subDir.FullName))
                {
                    yield return subDirPath;
                }
            }
        }

        private static IEnumerable<string> CollectFiles(string dirPath)
        {
            if (Directory.Exists(dirPath) == false)
            {
                yield break;
            }

            var dir = new DirectoryInfo(dirPath);

            // sub directories
            foreach (var subDir in dir.GetDirectories())
            {
                foreach (var filePath in CollectFiles(subDir.FullName))
                {
                    yield return filePath;
                }
            }

            // files
            foreach (var file in dir.GetFiles())
            {
                yield return file.FullName;
            }
        }

        #endregion Methods
    }
}