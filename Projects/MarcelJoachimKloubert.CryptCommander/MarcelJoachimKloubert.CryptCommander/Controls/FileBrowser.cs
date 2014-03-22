// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms;
using MarcelJoachimKloubert.CryptCommander.Classes.IO;
using MarcelJoachimKloubert.CryptCommander.Classes.IO.Local;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CryptCommander.Controls
{
    /// <summary>
    /// A file browser control.
    /// </summary>
    public partial class FileBrowser : UserControl
    {
        #region Fields (1)

        private static string[] _BYTE_SIZE_UNITS = new string[]
        {
            string.Empty,
            "kB",
            "MB",
            "GB",
            "TB",
            "PB",
            "EB",
            "ZB",
            "YB",
        };

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBrowser"/> class.
        /// </summary>
        /// <param name="fs">The underlying file system.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fs" /> is <see langword="null" />.
        /// </exception>
        public FileBrowser(IFileSystem fs)
        {
            if (fs == null)
            {
                throw new ArgumentNullException("fs");
            }

            this.FileSystem = fs;

            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBrowser"/> class.
        /// </summary>
        public FileBrowser()
            : this(new LocalFileSystem())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying file system.
        /// </summary>
        public IFileSystem FileSystem
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (6)

        // Private Methods (6) 

        private void ChangeDirectory(IDirectory dir)
        {
            this.ListView_CurrentDirectory.Items.Clear();

            Task.Factory.StartNew((state) =>
                {
                    var currentDirectory = (IDirectory)state;
                    try
                    {
                        while (this.ImageList_ListView_CurrentDirectory.Images.Count > 0)
                        {
                            using (var img = this.ImageList_ListView_CurrentDirectory.Images[0])
                            {
                                this.ImageList_ListView_CurrentDirectory.Images.RemoveAt(0);
                            }
                        }

                        this.TextBox_CurrentPath
                            .InvokeSafe((tb, tbState) =>
                            {
                                try
                                {
                                    tb.Text = tbState.Directory.FullPath;
                                }
                                catch
                                {

                                }
                            }, new
                            {
                                Directory = currentDirectory,
                            });

                        if (currentDirectory == null)
                        {
                            return;
                        }

                        this.ComboBox_Drives
                            .InvokeSafe((cb, cbState) =>
                            {
                                try
                                {
                                    this.ListView_CurrentDirectory
                                        .InvokeSafe((lv, lvState) =>
                                         {
                                             var dirs = lvState.Directory.GetDirectories() ?? Enumerable.Empty<IDirectory>();
                                             foreach (var d in dirs.OfType<IDirectory>()
                                                                   .OrderBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase))
                                             {
                                                 var lwt = d.LastWriteTime;

                                                 var lvi = new ListViewItem();
                                                 lvi.Text = d.DisplayName;
                                                 lvi.SubItems.Add(string.Empty);
                                                 lvi.SubItems.Add(lwt.ToLocalTime().ToString("yyyy-MM-dd"));
                                                 lvi.SubItems.Add(lwt.ToLocalTime().ToString("HH:mm:ss"));
                                                 lvi.Tag = d;

                                                 var icon = d.GetIcon();
                                                 if (icon != null)
                                                 {
                                                     this.ImageList_ListView_CurrentDirectory.Images.Add(icon);

                                                     lvi.ImageIndex = this.ImageList_ListView_CurrentDirectory.Images.Count - 1;
                                                 }

                                                 this.ListView_CurrentDirectory.Items.Add(lvi);
                                             }

                                             var files = lvState.Directory.GetFiles() ?? Enumerable.Empty<IFile>();
                                             foreach (var f in files.OfType<IFile>()
                                                                    .OrderBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase))
                                             {
                                                 var lwt = f.LastWriteTime;

                                                 var lvi = new ListViewItem();
                                                 lvi.Text = f.DisplayName;
                                                 lvi.SubItems.Add(ToHumanReadableFileSize(f.Size));
                                                 lvi.SubItems.Add(lwt.ToLocalTime().ToString("yyyy-MM-dd"));
                                                 lvi.SubItems.Add(lwt.ToLocalTime().ToString("HH:mm:ss"));
                                                 lvi.Tag = f;

                                                 var icon = f.GetIcon();
                                                 if (icon != null)
                                                 {
                                                     this.ImageList_ListView_CurrentDirectory.Images.Add(icon);

                                                     lvi.ImageIndex = this.ImageList_ListView_CurrentDirectory.Images.Count - 1;
                                                 }

                                                 this.ListView_CurrentDirectory.Items.Add(lvi);
                                             }
                                         }, cbState);
                                }
                                catch
                                {
                                    // ignore here
                                }
                            }, new
                            {
                                Directory = currentDirectory,
                            });
                    }
                    catch
                    {
                        // ignore here
                    }
                }, dir);
        }

        private void ComboBox_Drives_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDrive = this.ComboBox_Drives.SelectedItem as IDrive;
            try
            {
                IDirectory rootDir = null;
                if (selectedDrive != null)
                {
                    rootDir = selectedDrive.GetRootDirectory();
                }

                this.ChangeDirectory(rootDir);
            }
            catch
            {
                // ignore here
            }
        }

        private void FileBrowser_Enter(object sender, EventArgs e)
        {
            this.ListView_CurrentDirectory.Focus();
        }

        private void FileBrowser_Load(object sender, EventArgs e)
        {
            // drives
            this.ComboBox_Drives.Items.Clear();
            foreach (var drive in this.FileSystem
                                      .GetDrives()
                                      .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                this.ComboBox_Drives
                    .Items
                    .Add(drive);
            }

            if (this.ComboBox_Drives.Items.Count > 0)
            {
                this.ComboBox_Drives.SelectedIndex = 0;
            }
        }

        private void ListView_CurrentDirectory_DoubleClick(object sender, EventArgs e)
        {
            var lv = sender as ListView;

            try
            {
                var selectedDirectory = lv.SelectedItems
                                          .OfType<ListViewItem>()
                                          .Select(lvi => lvi.Tag)
                                          .OfType<IDirectory>()
                                          .LastOrDefault();

                if (selectedDirectory == null)
                {
                    return;
                }

                this.ChangeDirectory(selectedDirectory);
            }
            catch
            {

            }
        }

        private static string ToHumanReadableFileSize(long size)
        {
            var power = (int)Math.Floor(Math.Log(size, 1000));
            if (power >= _BYTE_SIZE_UNITS.Length)
            {
                power = _BYTE_SIZE_UNITS.Length - 1;
            }

            string result;
            if (power == 0)
            {
                result = size.ToString();
            }
            else
            {
                result = string.Format("{0} {1}",
                                       Math.Floor((float)size / Math.Pow(1000, power)),
                                       _BYTE_SIZE_UNITS[power]);
            }

            return result;
        }

        #endregion Methods
    }
}
