// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.DragNBatch.PlugIns;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MarcelJoachimKloubert.DragNBatch.ViewModel
{
    internal sealed class HandleFilesContext : IHandleFilesContext
    {
        #region Fields (3)

        private double _currentStepProgess;
        private double _overallProgess;
        private string _statusText;

        #endregion Fields

        #region Properties (11)

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

        public double CurrentStepProgess
        {
            get { return this._currentStepProgess; }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (value > 100)
                {
                    value = 100;
                }

                if (this._currentStepProgess != value)
                {
                    this._currentStepProgess = value;

                    var callback = this.CurrentStepProgessUpdated;
                    if (callback != null)
                    {
                        callback(this, value);
                    }
                }
            }
        }

        internal Action<HandleFilesContext, double> CurrentStepProgessUpdated
        {
            get;
            set;
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

        public double OverallProgess
        {
            get { return this._overallProgess; }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (value > 100)
                {
                    value = 100;
                }

                if (this._overallProgess != value)
                {
                    this._overallProgess = value;

                    var callback = this.OverallProgessUpdated;
                    if (callback != null)
                    {
                        callback(this, value);
                    }
                }
            }
        }

        internal Action<HandleFilesContext, double> OverallProgessUpdated
        {
            get;
            set;
        }

        public string StatusText
        {
            get { return this._statusText; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }
                else
                {
                    value = value.Trim();
                }

                if (this._statusText != value)
                {
                    this._statusText = value;

                    var callback = this.StatusTextUpdated;
                    if (callback != null)
                    {
                        callback(this, value);
                    }
                }
            }
        }

        internal Action<HandleFilesContext, string> StatusTextUpdated
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (2) 

        public void SetCurrentStepProgess(double value, double maxValue)
        {
            if (maxValue == 0)
            {
                this.CurrentStepProgess = 0;
            }
            else
            {
                this.CurrentStepProgess = value / maxValue * 100d;
            }
        }

        public void SetOverallProgess(double value, double maxValue)
        {
            if (maxValue == 0)
            {
                this.OverallProgess = 0;
            }
            else
            {
                this.OverallProgess = value / maxValue * 100d;
            }
        }

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