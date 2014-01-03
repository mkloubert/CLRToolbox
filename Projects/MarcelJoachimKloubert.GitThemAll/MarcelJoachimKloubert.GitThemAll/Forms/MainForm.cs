// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using System.Windows.Forms;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using AppSettings = MarcelJoachimKloubert.GitThemAll.Properties.Settings;

namespace MarcelJoachimKloubert.GitThemAll.Forms
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields (1)

        private Repository _repository;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties (2)

        public Repository Repository
        {
            get { return this._repository; }

            private set
            {
                this.ListView_Remotes.Items.Clear();
                foreach (var remote in value.Network
                                            .Remotes
                                            .OrderBy(r => r.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    var newItem = new ListViewItem();
                    newItem.Checked = true;
                    newItem.Tag = remote;
                    newItem.Text = remote.Name;

                    var col2 = newItem.SubItems.Add(remote.Url);

                    this.ListView_Remotes.Items.Add(newItem);
                }

                this._repository = value;
                this.TextBox_RepositoryPath.Text = value.Info.WorkingDirectory;
            }
        }

        internal AppSettings Settings
        {
            get { return AppSettings.Default; }
        }

        #endregion Properties

        #region Methods (8)

        // Private Methods (8) 

        private void Button_ClearProtocol_Click(object sender, EventArgs e)
        {

        }

        private void Button_Push_Click(object sender, EventArgs e)
        {
            var repo = this.Repository;
            if (repo == null)
            {
                return;
            }

            var origin = repo.Branches.Where(b => !b.IsRemote)
                                      .OrderBy(b => (b.Name ?? string.Empty).ToLower().Trim() == "origin" ? 1 : 0)
                                      .LastOrDefault();

            if (origin == null)
            {
                return;
            }

            foreach (var lvi in this.ListView_Remotes
                                    .Items.Cast<ListViewItem>()
                                    .Where(lvi => lvi.Checked))
            {
                var remote = (Remote)lvi.Tag;

                try
                {
                    var form = new CredentialForm(remote);
                    if (DialogResult.OK != form.ShowDialog())
                    {
                        return;
                    }

                    var opts = new PushOptions();
                    opts.Credentials = form.Credentials;
                    opts.OnPackBuilderProgress = this.PushOptions_PackBuilderProgress;
                    opts.OnPushTransferProgress = this.PushOptions_PushTransferProgress;

                    repo.Network.Push(remote, "HEAD", origin.TrackedBranch.CanonicalName, opts);
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);

                    return;
                }
            }
        }

        private void Button_SelectRepository_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select Git repository...";
            dialog.SelectedPath = this.Settings.LastSelectedFolder;
            dialog.ShowNewFolderButton = false;
            if (DialogResult.OK != dialog.ShowDialog())
            {
                return;
            }

            try
            {
                this.Repository = new Repository(dialog.SelectedPath);

                this.Settings.LastSelectedFolder = dialog.SelectedPath;
                this.Settings.Save();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Settings.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ToolStripStatusLabel_Main.Text = "";
        }

        private bool PushOptions_PackBuilderProgress(PackBuilderStage stage, int current, int total)
        {
            return true;
        }

        private bool PushOptions_PushTransferProgress(int current, int total, long bytes)
        {
            return true;
        }

        private void ShowError(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            var innerEx = ex.GetBaseException() ?? ex;

            MessageBox.Show(this,
                            innerEx.Message,
                            innerEx.GetType().FullName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        #endregion Methods
    }
}
