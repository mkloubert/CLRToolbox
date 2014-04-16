// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows;
using MarcelJoachimKloubert.CLRToolbox.IO;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// Stores data for an entry that describes the differences between two items.
    /// </summary>
    public sealed class CompareDifference : CompareResultBase
    {
        #region Fields (2)

        private readonly FoundDifferentFileSystemItemsEventArgs _EVENT_ARGS;
        private ImageSource _icon;

        #endregion Fields

        #region Constructors (2)

        internal CompareDifference(FoundDifferentFileSystemItemsEventArgs eventArgs)
            : base(invokeOnConstructor: false)
        {
            this._EVENT_ARGS = eventArgs;

            this.OnConstructor();
        }

        ~CompareDifference()
        {
            this.Dispose(false);
        }

        #endregion Constructors

        #region Properties (8)

        /// <inheriteddoc />
        public override FileSystemInfo Destination
        {
            get { return this._EVENT_ARGS.Destination; }
        }

        /// <summary>
        /// Gets the differences between <see cref="CompareDifference.Source" />
        /// and <see cref="CompareDifference.Destination" />.
        /// </summary>
        public FileSystemItemDifferences? Differences
        {
            get { return this._EVENT_ARGS.Differences; }
        }

        /// <summary>
        /// Gets if <see cref="CompareDifference.Source" /> and <see cref="CompareDifference.Destination" /> have
        /// differnt contents or not.
        /// </summary>
        public bool HasDifferentContents
        {
            get
            {
                return this.Differences.Value.HasFlag(FileSystemItemDifferences.Content);
            }
        }

        /// <summary>
        /// Gets if <see cref="CompareDifference.Source" /> and <see cref="CompareDifference.Destination" /> have
        /// differnt sizes or not.
        /// </summary>
        public bool HasDifferentSizes
        {
            get
            {
                return this.Differences.Value.HasFlag(FileSystemItemDifferences.Size);
            }
        }

        /// <summary>
        /// Gets if <see cref="CompareDifference.Source" /> and <see cref="CompareDifference.Destination" /> have
        /// differnt last write timestamps or not.
        /// </summary>
        public bool HasDifferentTimeStamps
        {
            get
            {
                return this.Differences.Value.HasFlag(FileSystemItemDifferences.LastWriteTime);
            }
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        public ImageSource Icon
        {
            get { return this._icon; }

            private set { this._icon = value; }
        }

        /// <summary>
        /// Gets if <see cref="CompareDifference.Source" /> and/or <see cref="CompareDifference.Destination" />
        /// are missing or not.
        /// </summary>
        public bool IsMissing
        {
            get
            {
                return this.Differences.Value.HasFlag(FileSystemItemDifferences.IsExtra) ||
                       this.Differences.Value.HasFlag(FileSystemItemDifferences.IsMissing);
            }
        }

        /// <inheriteddoc />
        public override FileSystemInfo Source
        {
            get { return this._EVENT_ARGS.Source; }
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            base.OnConstructor();

            App.Current
               .BeginInvoke((a, appState) =>
                {
                    ImageSource i = null;

                    try
                    {
                        if (this._EVENT_ARGS.Source is FileInfo)
                        {
                            try
                            {
                                string path = null;
                                if (appState.DiffObject.Source.Exists)
                                {
                                    path = appState.DiffObject.Source.FullName;
                                }
                                else if (appState.DiffObject.Destination.Exists)
                                {
                                    path = appState.DiffObject.Destination.FullName;
                                }

                                if (string.IsNullOrWhiteSpace(path) == false)
                                {
                                    using (var icon = Win32Helper.GetSystemIcon(path))
                                    {
                                        using (var iconBmp = icon.ToBitmap())
                                        {
                                            var imgStream = new MemoryStream();
                                            try
                                            {
                                                iconBmp.Save(imgStream, ImageFormat.Png);
                                                imgStream.Position = 0;

                                                var bmp = new BitmapImage();
                                                bmp.BeginInit();
                                                bmp.StreamSource = imgStream;
                                                bmp.EndInit();

                                                i = bmp;
                                            }
                                            catch
                                            {
                                                imgStream.Dispose();
                                                throw;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                i = null;
                            }

                            if (i == null)
                            {
                                var bmp = new BitmapImage();
                                bmp.BeginInit();
                                bmp.UriSource = new Uri("pack://application:,,,/Resources/Icons/file.png", UriKind.Absolute);
                                bmp.EndInit();

                                i = bmp;
                            }
                        }
                        else if (this._EVENT_ARGS.Source is DirectoryInfo)
                        {
                            var bmp = new BitmapImage();
                            bmp.BeginInit();
                            bmp.UriSource = new Uri("pack://application:,,,/Resources/Icons/folder.png", UriKind.Absolute);
                            bmp.EndInit();

                            i = bmp;
                        }
                    }
                    catch
                    {
                        i = null;
                    }

                    appState.DiffObject.Icon = i;
                }, new
                {
                    DiffObject = this,
                });
        }

        // Private Methods (2) 

        private void Dispose(bool disposing)
        {
            lock (this._SYNC)
            {
                this.Icon = null;
            }
        }

        private void OpenItem(FileSystemInfo item)
        {
            try
            {
                item.Refresh();
                if (item.Exists == false)
                {
                    return;
                }

                if (item is FileInfo)
                {
                    Process.Start("explorer.exe", string.Format("/select, \"{0}\"",
                                                                item.FullName));
                }
                else if (item is DirectoryInfo)
                {
                    Process.Start(item.FullName);
                }
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        #endregion Methods
    }
}