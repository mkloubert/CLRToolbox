// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.FileSync.Classes.Sessions;
using MarcelJoachimKloubert.CloudNET.SDK.IO;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CloudNET.FileSync.Controls.Forms
{
    public partial class SyncForm : Form
    {
        #region Constructors (2)

        internal SyncForm(SyncSession session,
                          bool recursive)
        {
            this.InitializeComponent();

            this.Recursive = recursive;
            this.Session = session;
        }

        public SyncForm()
            : this(null, false)
        {

        }

        #endregion Constructors

        #region Properties (4)

        public Task CurrentSyncTask
        {
            get;
            private set;
        }

        public bool Recursive
        {
            get;
            private set;
        }

        internal SyncSession Session
        {
            get;
            private set;
        }

        public CancellationTokenSource SyncTaskCanceller
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (6)

        // Public Methods (2) 

        public void ShowDialogAndStart(IWin32Window parent)
        {
            this.Start();
            this.ShowDialog();
        }

        public void Start()
        {
            var cancelSrc = new CancellationTokenSource();

            var newSyncTask = new Task(this.SyncTaskAction,
                                       state: cancelSrc.Token,
                                       cancellationToken: cancelSrc.Token,
                                       creationOptions: TaskCreationOptions.LongRunning);
            newSyncTask.Start();

            this.SyncTaskCanceller = cancelSrc;
            this.CurrentSyncTask = newSyncTask;
        }
        // Private Methods (4) 

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.InvokeSafe(form =>
                {
                    if (MessageBox.Show(form,
                                        "Are you sure to cancel the current sync process?",
                                        "Cancel sync operation?",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }

                    var cancelSrc = this.SyncTaskCanceller;
                    if (cancelSrc != null)
                    {
                        form.Button_Cancel
                            .InvokeSafe(btn => btn.Enabled = false);

                        cancelSrc.Cancel();

                        form.Label_RemainingTime
                            .InvokeSafe(lbl => lbl.Text = "Cancelling...");
                    }
                });
        }

        private static void CollectDirectories(DirectoryInfo dir, IList<DirectoryInfo> coll)
        {
            coll.Add(dir);

            foreach (var subDir in dir.GetDirectories())
            {
                CollectDirectories(subDir, coll);
            }
        }

        private void SyncDirectory(DirectoryInfo localDir,
                                   ListCloudDirectoryResult remoteDir,
                                   CancellationToken cancelToken)
        {
            this.ProgressBar_CurrentDirectory
                .InvokeSafe(pb => pb.Value = 0);

            var wait = true;

            var syncCtx = remoteDir.SyncWithLocalDirectory(localDir, recursive: false,
                                                           autostart: false);
            syncCtx.Started += (sender, e) =>
                {
                    wait = false;
                };
            syncCtx.SyncProgress += (sender, e) =>
                {
                    var labelTxt = e.StatusText ?? string.Empty;

                    this.Label_CurrentDirectory
                        .InvokeSafe((lbl, lblState) =>
                            {
                                lbl.Text = lblState.Text;
                                lblState.ToolTip.SetToolTip(lbl, lblState.Text);
                            }, new
                            {
                                Text = labelTxt,
                                ToolTip = this.ToolTip_Label_CurrentDirectory,
                            });

                    this.ProgressBar_CurrentDirectory
                        .InvokeSafe((pb, lblState) =>
                        {
                            var percentage = 0;
                            if (lblState.EventArguments.Progress.HasValue)
                            {
                                percentage = (int)Math.Floor(lblState.EventArguments.Progress.Value * 100);
                            }

                            if (percentage < 0)
                            {
                                percentage = 0;
                            }
                            else if (percentage > 100)
                            {
                                percentage = 100;
                            }

                            pb.Value = percentage;
                        }, new
                        {
                            EventArguments = e,
                        });
                };

            var task = new Task((state) =>
                {
                    try
                    {
                        var ctx = (ISyncWithLocalDirectoryExecutionContext)state;

                        ctx.Start();
                    }
                    finally
                    {
                        wait = false;
                    }
                }, state: syncCtx
                 , creationOptions: TaskCreationOptions.LongRunning);
            task.Start();

            // wait until sync process has been started
            while (wait) { }

            while (syncCtx.IsRunning)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    syncCtx.CancelAndWait();
                }
            }
        }

        private void SyncTaskAction(object state)
        {
            bool canceled = false;

            try
            {
                var startTime = DateTimeOffset.Now;

                var cancelToken = (CancellationToken)state;

                var localRootDir = new DirectoryInfo(this.Session.DirectoryToSync);

                IList<DirectoryInfo> allDirs = new SynchronizedCollection<DirectoryInfo>();
                if (this.Recursive)
                {
                    CollectDirectories(localRootDir, allDirs);
                }

                this.ProgressBar_Directories
                    .InvokeSafe((pb, pbState) =>
                        {
                            pb.Minimum = 0;
                            pb.Maximum = pbState.DirectoryList.Count;

                            pb.Value = 0;
                        }, new
                        {
                            DirectoryList = allDirs,
                        });

                var linesLeft = allDirs.Count;
                var linesProcessed = 0;

                foreach (var localDir in allDirs.OrderBy(d => d.FullName.Length)
                                                .ThenBy(d => d.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        canceled = true;
                        break;
                    }

                    var remotePath = this.Session.ToServerFilePath(localDir.FullName);
                    var remoteDir = this.Session.Server.FileSystem.ListDirectory(remotePath);

                    var labelTxt = string.Format("{0} => {1}",
                                                 localDir.FullName,
                                                 remotePath);

                    this.Label_Directories
                        .InvokeSafe((lbl, lblState) =>
                        {
                            lbl.Text = lblState.Text;
                            lblState.ToolTip.SetToolTip(lbl, lblState.Text);
                        }, new
                        {
                            Text = labelTxt,
                            ToolTip = this.ToolTip_Label_Directories,
                        });

                    this.SyncDirectory(localDir, remoteDir,
                                       cancelToken);

                    var now = DateTimeOffset.Now;
                    var timeTaken = now - startTime;

                    var secondsLeft = (timeTaken.TotalSeconds / (double)++linesProcessed) *
                                      (double)--linesLeft;

                    this.Label_RemainingTime
                        .InvokeSafe((lbl, lblState) =>
                        {
                            lbl.Text = string.Format("Remaining time: about {0:hh}:{0:mm}:{0:ss}",
                                                     lblState.TimeLeft);
                        }, new
                        {
                            TimeLeft = TimeSpan.FromSeconds(secondsLeft),
                        });

                    this.ProgressBar_Directories
                        .InvokeSafe((pb) =>
                        {
                            pb.Increment(1);
                        });
                }

                this.Label_RemainingTime
                    .InvokeSafe((lbl, lblState) =>
                    {
                        lbl.Text = lblState.Canceled ? "Canceled" : "Done";
                    }, new
                    {
                        Canceled = canceled,
                    });
            }
            catch (Exception ex)
            {
                canceled = true;

                this.InvokeSafe((form, frmState) =>
                    {
                        MessageBox.Show(form,
                                        frmState.Exception.ToString(),
                                        frmState.Exception.GetType().FullName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }, new
                    {
                        Exception = ex.GetBaseException() ?? ex,
                    });
            }
            finally
            {
                this.CurrentSyncTask = null;
                this.SyncTaskCanceller = null;

                this.InvokeSafe((form, frmState) =>
                    {
                        form.DialogResult = frmState.Canceled ? DialogResult.Cancel : DialogResult.OK;
                    }, new
                    {
                        Canceled = canceled,
                    });
            }
        }

        #endregion Methods
    }
}
