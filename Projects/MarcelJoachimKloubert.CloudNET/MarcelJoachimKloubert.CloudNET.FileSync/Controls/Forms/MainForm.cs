// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using FileTypeAndIcon;
using MarcelJoachimKloubert.CloudNET.FileSync.Classes.Sessions;
using MarcelJoachimKloubert.CloudNET.SDK;
using MarcelJoachimKloubert.CloudNET.SDK.IO;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CloudNET.FileSync.Controls.Forms
{
    /// <summary>
    /// The main window.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields (1)

        private static string[] _FILESIZE_UNITS = new string[]
        {
            "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB", 
        };

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        /// <param name="args">The arguments from the command line.</param>
        public MainForm(string[] args)
        {
            this.InitializeComponent();

            this.CommandLineArguments = args.Where(a => string.IsNullOrWhiteSpace(a) == false)
                                            .Select(a => a.TrimStart())
                                            .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
            : this(new string[0])
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the arguments from the command line.
        /// </summary>
        public string[] CommandLineArguments
        {
            get;
            private set;
        }

        internal SyncSession CurrentSession
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (17)

        // Private Methods (17) 

        private void Button_RefreshRemoteDirectory_Click(object sender, EventArgs e)
        {
            this.LoadRemoteDirectory(this.TextBox_RemotePath.Text);
        }

        private void Button_Sync_Click(object sender, EventArgs e)
        {
            var session = this.CurrentSession;
            if (session == null)
            {
                var server = this.CreateServerObject();
                if (server == null)
                {
                    return;
                }

                session = new SyncSession(server, this.TextBox_SyncDirectory.Text);
                session.LogItemReceived += this.CurrentSession_LogItemReceived;

                var syncFrm = new SyncForm(session, true);
                syncFrm.FormClosed += (sender2, e2) =>
                    {
                        if (syncFrm.DialogResult == DialogResult.OK)
                        {
                            // session.Start();
                            this.CurrentSession = session;
                        }
                    };
                syncFrm.ShowDialogAndStart(this);
            }
            else
            {
                session.Stop();
                this.CurrentSession = null;
            }
        }

        private void ContextMenuStrip_RemoteFiles_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            var selectedItem = this.TryGetSelectedRemoteFileItem();
            if (selectedItem == null)
            {
                return;
            }

            e.Cancel = false;

            var menuItems = this.ContextMenuStrip_RemoteFiles
                                .Items
                                .ToEnumerableSafe()
                                .OfType<ToolStripItem>();

            foreach (var tsi in menuItems)
            {
                tsi.Visible = false;
            }

            var showDirItems = new Action(() =>
                {
                    foreach (var tsi in menuItems.Where(i => (i.Name ?? string.Empty).StartsWith("ToolStripMenuItem_RemoteFiles_Dir_")))
                    {
                        tsi.Visible = true;
                    }
                });

            var showFileItems = new Action(() =>
                {
                    foreach (var tsi in menuItems.Where(i => (i.Name ?? string.Empty).StartsWith("ToolStripMenuItem_RemoteFiles_File_")))
                    {
                        tsi.Visible = true;
                    }
                });

            if ((selectedItem.Tag is CloudDirectory) ||
                (selectedItem.Tag is ListCloudDirectoryResult))
            {
                showDirItems();
                return;
            }

            if ((selectedItem.Tag is CloudFile))
            {
                showFileItems();
                return;
            }
        }

        private CloudServer CreateServerObject()
        {
            var result = new CloudServer();
            result.HostAddress = this.TextBox_ServerAddress.Text;

            if (string.IsNullOrWhiteSpace(this.TextBox_ServerPort.Text) == false)
            {
                result.Port = int.Parse(this.TextBox_ServerPort.Text.Trim());
            }

            if (string.IsNullOrWhiteSpace(this.TextBox_ServerUser.Text) == false)
            {
                var pwd = new SecureString();
                pwd.Append(this.TextBox_ServerPassword.Text ?? string.Empty);

                result.Credentials = new NetworkCredential(this.TextBox_ServerUser.Text.Trim(),
                                                           pwd);
            }

            switch (this.ComboBox_Protocol.SelectedItem.AsString())
            {
                case "http":
                    result.IsSecure = false;
                    break;

                default:
                    result.IsSecure = true;
                    break;
            }

            return result;
        }

        private void CurrentSession_LogItemReceived(object sender, SyncLogEventArgs e)
        {
            try
            {
                this.ListView_SyncLog
                    .InvokeSafe((lv, state) =>
                        {
                            try
                            {
                                lv.Items
                                  .Insert(0,
                                          state.EventArguments.LogItem);
                            }
                            catch
                            {
                                // ignore errors here
                            }
                        }, new
                        {
                            EventArguments = e,
                        });
            }
            catch
            {
                // ignore errors here
            }
        }

        private void DownloadFileTo(CloudFile file, string targetFile)
        {
            using (var stream = new FileStream(targetFile,
                                               FileMode.Create,
                                               FileAccess.ReadWrite))
            {
                file.Download(stream);
            }
        }

        private void ListView_RemoteFiles_DoubleClick(object sender, EventArgs e)
        {
            var lv = (ListView)sender;

            var selectedItem = TryGetSelectedRemoteFileItem(lv);
            if (selectedItem == null)
            {
                return;
            }

            var dir = selectedItem.Tag as CloudDirectory;
            if (dir != null)
            {
                this.LoadRemoteDirectory(dir.Path);

                return;
            }

            var file = selectedItem.Tag as CloudFile;
            if (file != null)
            {
                try
                {
                    var dialog = new SaveFileDialog();
                    dialog.Title = "Download file to...";
                    dialog.FileName = file.Name ?? string.Empty;
                    dialog.OverwritePrompt = true;
                    if (DialogResult.OK != dialog.ShowDialog())
                    {
                        return;
                    }

                    this.DownloadFileTo(file, dialog.FileName);
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(ex);
                }

                return;
            }

            var listDirResult = selectedItem.Tag as ListCloudDirectoryResult;
            if (listDirResult != null)
            {
                try
                {
                    this.LoadRemoteDirectory(listDirResult.ParentPath);
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(ex);
                }

                return;
            }
        }

        private void LoadRemoteDirectory(ListCloudDirectoryResult result)
        {
            try
            {
                this.ListView_RemoteFiles
                    .InvokeSafe((lv, state) =>
                    {
                        lv.Items.Clear();

                        if (state.Directory.IsRootDirectory == false)
                        {
                            // parent directory

                            var newLvi = new ListViewItem();
                            newLvi.Tag = state.Directory;
                            newLvi.Text = "..";

                            lv.Items.Add(newLvi);
                        }

                        // sub directories
                        foreach (var dir in state.Directory
                                                 .Directories
                                                 .ToEnumerableSafe(ofType: true)
                                                 .OrderBy(d => d.Name, StringComparer.InvariantCultureIgnoreCase))
                        {
                            var newLvi = new ListViewItem();
                            newLvi.ImageIndex = 1;
                            newLvi.Tag = dir;
                            newLvi.Text = dir.Name ?? string.Empty;
                            newLvi.SubItems.Add("<DIR>");  // size

                            if (dir.WriteTime.HasValue)
                            {
                                newLvi.SubItems.Add(dir.WriteTime.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
                            }

                            lv.Items.Add(newLvi);
                        }

                        // files
                        var fileItems = new List<ListViewItem>();
                        foreach (var file in state.Directory
                                                  .Files
                                                  .ToEnumerableSafe(ofType: true)
                                                  .OrderBy(d => d.Name, StringComparer.InvariantCultureIgnoreCase))
                        {
                            var newLvi = new ListViewItem();
                            newLvi.ImageIndex = 0;
                            newLvi.Tag = file;
                            newLvi.Text = file.Name ?? string.Empty;

                            if (file.Size.HasValue)
                            {
                                newLvi.SubItems.Add(MakeFileSizeHumanReadable(file.Size));
                            }

                            if (file.WriteTime.HasValue)
                            {
                                newLvi.SubItems.Add(file.WriteTime.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
                            }

                            lv.Items.Add(newLvi);
                            fileItems.Add(newLvi);
                        }

                        // collect available file extensions
                        var extensions = fileItems.Select(i => (CloudFile)i.Tag)
                                                  .Select(f =>
                                                          {
                                                              try
                                                              {
                                                                  return Path.GetExtension(f.Name).ToLower().Trim();
                                                              }
                                                              catch
                                                              {
                                                                  return null;
                                                              }
                                                          }).Where(e => string.IsNullOrWhiteSpace(e) == false)
                                                            .Distinct();

                        // remove old icons
                        const int MIN_IMAGE_COUNT = 2;
                        while (state.ImageList.Images.Count > MIN_IMAGE_COUNT)
                        {
                            using (var img = state.ImageList.Images[MIN_IMAGE_COUNT])
                            {
                                state.ImageList.Images.RemoveAt(MIN_IMAGE_COUNT);
                            }
                        }

                        // load and set icons for entries
                        var allIcons = RegisteredFileType.GetFileTypeAndIcon();
                        foreach (var ext in extensions)
                        {
                            try
                            {
                                var iconFile = (string)allIcons[ext];
                                if (string.IsNullOrWhiteSpace(iconFile) == false)
                                {
                                    var icon = RegisteredFileType.ExtractIconFromFile(iconFile, false);
                                    if (icon != null)
                                    {
                                        var matchingItems = fileItems.Where(i =>
                                            {
                                                var f = (CloudFile)i.Tag;

                                                return (f.Name ?? string.Empty).ToLower()
                                                                               .Trim()
                                                                               .EndsWith(ext);
                                            });

                                        state.ImageList.Images.Add(icon);
                                        foreach (var mi in matchingItems)
                                        {
                                            mi.ImageIndex = state.ImageList.Images.Count - 1;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                // ignore here
                            }
                        }
                    }, new
                    {
                        Directory = result,
                        ImageList = this.ImageList_RemoteFiles,
                    });
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void LoadRemoteDirectory(string path)
        {
            try
            {
                this.TextBox_RemotePath
                    .InvokeSafe((tb, state) =>
                    {
                        tb.Text = state.NewPath;
                    }, new
                    {
                        NewPath = path,
                    });

                var server = this.CreateServerObject();
                if (server == null)
                {
                    return;
                }

                var result = server.FileSystem.ListDirectory(path);
                this.LoadRemoteDirectory(result);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            var startSync = false;

            this.ComboBox_Protocol.SelectedIndex = 1;

            foreach (var arg in this.CommandLineArguments)
            {
                var lowerArg = arg.ToLower().Trim();

                if (lowerArg.StartsWith("/server:"))
                {
                    var serverAddress = arg.Substring(8).Trim();
                    if (string.IsNullOrWhiteSpace(serverAddress) == false)
                    {
                        var url = new Uri(serverAddress);

                        var schema = url.Scheme.ToLower().Trim();
                        switch (schema)
                        {
                            case "http":
                            case "https":
                                {
                                    this.ComboBox_Protocol.SelectedItem = schema;

                                    // address and port
                                    this.TextBox_ServerAddress.Text = url.Host;
                                    if (url.IsDefaultPort == false)
                                    {
                                        this.TextBox_ServerPort.Text = url.Port.ToString();
                                    }

                                    // username and password
                                    if (string.IsNullOrWhiteSpace(url.UserInfo) == false)
                                    {
                                        if (url.UserInfo.Contains(":"))
                                        {
                                            var parts = url.UserInfo.Split(new string[] { ":" }, 2, StringSplitOptions.None);

                                            this.TextBox_ServerUser.Text = parts[0].Trim();
                                            this.TextBox_ServerPassword.Text = parts[1];
                                        }
                                        else
                                        {
                                            this.TextBox_ServerUser.Text = url.UserInfo.Trim();
                                        }
                                    }
                                }
                                break;

                            default:
                                throw new NotSupportedException(schema);
                        }
                    }
                }
                else if (lowerArg.StartsWith("/sync_dir:"))
                {
                    this.TextBox_SyncDirectory.Text = arg.Substring(10).Trim();
                }
                else if (lowerArg == "/autostart")
                {
                    startSync = true;
                }
            }

            if (startSync)
            {
                this.Button_Sync_Click(this.Button_Sync, EventArgs.Empty);
            }
        }

        private static string MakeFileSizeHumanReadable(long? fileSize)
        {
            if (fileSize.HasValue == false)
            {
                return "---";
            }

            double result = fileSize.Value;

            var level = (int)Math.Floor(Math.Log(result, 1000));
            if (level < 1)
            {
                return result.ToString();
            }

            if (level > 7)
            {
                level = 7;
            }

            result = result / Math.Pow(1000, level);

            return result.ToString("0.00") + " " + _FILESIZE_UNITS[level - 1];
        }

        private void ShowErrorMessage(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            this.InvokeSafe((form, state) =>
                {
                    MessageBox.Show(form,
                                    state.Exception.ToString(),
                                    state.Exception.GetType().FullName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }, new
                {
                    Exception = ex.GetBaseException() ?? ex,
                });
        }

        private void ToolStripMenuItem_RemoteFiles_Dir_Open_Click(object sender, EventArgs e)
        {
            var selectedItem = this.TryGetSelectedRemoteFileItem();
            if (selectedItem == null)
            {
                return;
            }

            if ((selectedItem.Tag is CloudDirectory) ||
                (selectedItem.Tag is ListCloudDirectoryResult))
            {
                this.ListView_RemoteFiles_DoubleClick(this.ListView_RemoteFiles, EventArgs.Empty);
                return;
            }
        }

        private void ToolStripMenuItem_RemoteFiles_File_Download_Click(object sender, EventArgs e)
        {
            var selectedItem = this.TryGetSelectedRemoteFileItem();
            if (selectedItem == null)
            {
                return;
            }

            if (selectedItem.Tag is CloudFile)
            {
                this.ListView_RemoteFiles_DoubleClick(this.ListView_RemoteFiles, EventArgs.Empty);
                return;
            }
        }

        private void ToolStripMenuItem_RemoteFiles_File_Open_Click(object sender, EventArgs e)
        {
            var selectedItem = this.TryGetSelectedRemoteFileItem();
            if (selectedItem == null)
            {
                return;
            }

            var file = selectedItem.Tag as CloudFile;
            if (file != null)
            {

                try
                {
                    string targetFile = Path.Combine(Path.GetTempPath(),
                                                     file.Name);

                    this.DownloadFileTo(file, targetFile);
                    Process.Start(targetFile);
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(ex);
                }

                return;
            }
        }

        private ListViewItem TryGetSelectedRemoteFileItem()
        {
            return TryGetSelectedRemoteFileItem(this.ListView_RemoteFiles);
        }

        private static ListViewItem TryGetSelectedRemoteFileItem(ListView lv)
        {
            var allSelectedItems = lv.SelectedItems.Cast<ListViewItem>().ToArray();
            if (allSelectedItems.Length != 1)
            {
                return null;
            }

            return allSelectedItems[0];
        }

        #endregion Methods
    }
}
