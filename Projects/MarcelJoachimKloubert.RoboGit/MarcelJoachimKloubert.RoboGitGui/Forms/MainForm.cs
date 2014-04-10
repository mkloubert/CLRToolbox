// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.RoboGitGui.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.RoboGitGui.Forms
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the list of running tasks.
        /// </summary>
        public IEnumerable<Task> RunningTasks
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (15)

        // Private Methods (15) 

        private void Button_GitAll_Click(object sender, EventArgs e)
        {
            this.ListView_Tasks
                .InvokeSafe((lv, lvState) =>
                {
                    try
                    {
                        lvState.GitAllButton.InvokeSafe(btn => btn.Enabled = false);
                        lvState.GitSelectedButton.InvokeSafe(btn => btn.Enabled = false);

                        var runningTaskList =
                            this.RunningTasks = CreateGrouppedTasks(lv.Items
                                                                      .Cast<ListViewItem>()
                                                                      .Select(lvi => lvi.Tag)
                                                                      .OfType<GitTask>());

                        runningTaskList.ForAllAsync(ctx => ctx.Item.Start());
                    }
                    catch (Exception ex)
                    {
                        this.RunningTasks = null;

                        this.ShowError(ex);
                    }
                    finally
                    {
                        lvState.GitAllButton.InvokeSafe(btn => btn.Enabled = true);
                        lvState.GitSelectedButton.InvokeSafe(btn => btn.Enabled = true);
                    }
                }, new
                {
                    GitAllButton = this.Button_GitAll,
                    GitSelectedButton = this.Button_GitSelected,
                });
        }

        private void Button_GitSelected_Click(object sender, EventArgs e)
        {
            this.ListView_Tasks
                .InvokeSafe((lv, lvState) =>
                {
                    try
                    {
                        lvState.GitAllButton.InvokeSafe(btn => btn.Enabled = false);
                        lvState.GitSelectedButton.InvokeSafe(btn => btn.Enabled = false);

                        var runningTaskList =
                            this.RunningTasks = CreateGrouppedTasks(lv.Items
                                                                      .Cast<ListViewItem>()
                                                                      .Where(lvi => lvi.Selected)
                                                                      .Select(lvi => lvi.Tag)
                                                                      .OfType<GitTask>());

                        runningTaskList.ForAllAsync(ctx => ctx.Item.Start());
                    }
                    catch (Exception ex)
                    {
                        this.RunningTasks = null;
                        this.ShowError(ex);
                    }
                    finally
                    {
                        lvState.GitAllButton.InvokeSafe(btn => btn.Enabled = true);
                        lvState.GitSelectedButton.InvokeSafe(btn => btn.Enabled = true);
                    }
                }, new
                {
                    GitAllButton = this.Button_GitAll,
                    GitSelectedButton = this.Button_GitSelected,
                });
        }

        private void Button_ReloadConfig_Click(object sender, EventArgs e)
        {
            this.ReloadTasks();
        }

        private static IEnumerable<Task> CreateGrouppedTasks(IEnumerable<GitTask> tasks)
        {
            foreach (var nonGrpTask in tasks.Where(t => t.Group == null))
            {
                yield return nonGrpTask.CreateTask();
            }

            foreach (var grpTask in tasks.Where(t => t.Group != null)
                                         .GroupBy(t => t.Group))
            {
                yield return CreateTaskForGroupedGitTasks(grpTask);
            }
        }

        private static Task CreateTaskForGroupedGitTasks(IEnumerable<GitTask> tasks)
        {
            return new Task((state) =>
            {
                foreach (var t in (IEnumerable<GitTask>)state)
                {
                    try
                    {
                        var tplTask = t.CreateTask();
                        tplTask.RunSynchronously();
                    }
                    catch
                    {

                    }
                }
            }, state: tasks);
        }

        private void ListView_Tasks_Resize(object sender, EventArgs e)
        {
            this.ColumnHeader_TaskName
                .Width = this.ListView_Tasks.Width;
        }

        private void ListView_Tasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var lv = (ListView)sender;

                var selectedTasks = lv.SelectedItems
                                      .Cast<ListViewItem>()
                                      .Select(lvi => lvi.Tag)
                                      .OfType<GitTask>()
                                      .ToArray();

                this.SplitContainer_Main.Panel2.Controls.Clear();
                if (selectedTasks.Length == 1)
                {
                    var ctrl = selectedTasks[0].Control;
                    ctrl.Dock = DockStyle.Fill;

                    this.SplitContainer_Main.Panel2
                        .Controls
                        .Add(ctrl);
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ReloadTasks();
        }

        private void ReloadTasks()
        {
            this.Button_ReloadConfig
                .InvokeSafe((btn) => btn.Enabled = false);

            Task.Factory.StartNew((state) =>
                {
                    var frm = (MainForm)state;
                    try
                    {
                        var config = new IniFileConfigRepository(@"./config.ini");

                        frm.ListView_Tasks
                           .InvokeSafe((lv, lvState) =>
                            {
                                try
                                {
                                    lv.Items.Clear();
                                    while (lv.Items.Count > 0)
                                    {
                                        var t = lv.Items[0].Tag as GitTask;
                                        t.Error -= lvState.Form.Task_Error;
                                        t.Started -= lvState.Form.Task_Started;
                                        t.Stopped -= lvState.Form.Task_Stopped;

                                        lv.Items.RemoveAt(0);
                                    }

                                    lv.Groups.Clear();

                                    foreach (var grpName in GitTask.FromConfig(lvState.Config)
                                                                   .Select(t => t.Group)
                                                                   .Distinct()
                                                                   .OrderBy(gn => gn, StringComparer.CurrentCultureIgnoreCase))
                                    {
                                        var lvg = new ListViewGroup();
                                        lvg.Header = string.IsNullOrWhiteSpace(grpName) ? "(no group)" : grpName;
                                        lvg.Tag = grpName;

                                        lv.Groups.Add(lvg);
                                    }

                                    foreach (var task in GitTask.FromConfig(lvState.Config)
                                                                .OrderBy(t => t.DisplayName, StringComparer.CurrentCultureIgnoreCase))
                                    {
                                        try
                                        {
                                            var lvi = new ListViewItem();
                                            lvi.Text = task.DisplayName ?? string.Empty;
                                            lvi.Text = task.DisplayName ?? string.Empty;
                                            lvi.Tag = task;

                                            lvi.Group = CollectionHelper.SingleOrDefault(lv.Groups
                                                                                           .Cast<ListViewGroup>(),
                                                                                         lvg => object.Equals(lvg.Tag, task.Group));

                                            try
                                            {
                                                task.Error += lvState.Form.Task_Error;
                                                task.Started += lvState.Form.Task_Started;
                                                task.Stopped += lvState.Form.Task_Stopped;

                                                lv.Items.Add(lvi);
                                                lvState.Form.UpdateIcon(lvi);
                                            }
                                            catch
                                            {
                                                task.Error -= lvState.Form.Task_Error;
                                                task.Started -= lvState.Form.Task_Started;
                                                task.Stopped -= lvState.Form.Task_Stopped;

                                                throw;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            lvState.Form.ShowError(ex);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lvState.Form.ShowError(ex);
                                }
                            }, actionState: new
                            {
                                Config = config,
                                Form = frm,
                            });
                    }
                    catch (Exception ex)
                    {
                        frm.ShowError(ex);
                    }
                    finally
                    {
                        frm.Button_ReloadConfig
                           .InvokeSafe((btn) => btn.Enabled = true);
                    }
                }, state: this);
        }

        private void ShowError(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            try
            {
                this.InvokeSafe((win, state) =>
                    {
                        try
                        {
                            MessageBox.Show(win,
                                            text: state.Exception.ToString(),
                                            caption: "[ERROR] " + state.Exception.GetType().FullName,
                                            buttons: MessageBoxButtons.OK,
                                            icon: MessageBoxIcon.Error);
                        }
                        catch
                        {
                            // ignore
                        }
                    },
                    actionState: new
                    {
                        Exception = ex.GetBaseException() ?? ex,
                    });
            }
            catch
            {
                // ignore
            }
        }

        private void Task_Error(object sender, ErrorEventArgs e)
        {
            var task = (GitTask)sender;

            try
            {
                var lvi = this.TryFindListViewItemByGitTask(task);

                this.UpdateIcon(lvi);
            }
            catch
            {
                // ignore
            }
        }

        private void Task_Started(object sender, EventArgs e)
        {
            var task = (GitTask)sender;

            try
            {
                var lvi = this.TryFindListViewItemByGitTask(task);

                this.UpdateIcon(lvi);
            }
            catch
            {
                // ignore
            }
        }

        private void Task_Stopped(object sender, EventArgs e)
        {
            var task = (GitTask)sender;

            try
            {
                var lvi = this.TryFindListViewItemByGitTask(task);

                this.UpdateIcon(lvi);
            }
            catch
            {
                // ignore
            }
        }

        private ListViewItem TryFindListViewItemByGitTask(GitTask task)
        {
            return this.ListView_Tasks
                       .InvokeSafe((lv, lvState) =>
                       {
                           return CollectionHelper.SingleOrDefault(lv.Items
                                                                     .Cast<ListViewItem>(),
                                                                   lvi => object.Equals(lvi.Tag, lvState.Task));
                       }, funcState: new
                       {
                           Task = task,
                       });
        }

        private void UpdateIcon(ListViewItem lvi)
        {
            this.ListView_Tasks
                .InvokeSafe((lv, lvState) =>
                    {
                        try
                        {
                            var task = lvState.ListViewItem.Tag as GitTask;
                            if (task != null)
                            {
                                var iconIndx = -1;

                                if (task.IsRunning)
                                {
                                    iconIndx = 2;
                                }
                                else
                                {
                                    switch (task.Method)
                                    {
                                        case GitTaskMethod.Pull:
                                            iconIndx = 1;
                                            break;

                                        case GitTaskMethod.Push:
                                            iconIndx = 0;
                                            break;
                                    }
                                }

                                if (iconIndx > -1)
                                {
                                    lvState.ListViewItem.ImageIndex = iconIndx;
                                }
                            }
                        }
                        catch
                        {
                            // ignore
                        }
                    }, new
                    {
                        ListViewItem = lvi,
                    });
        }

        #endregion Methods
    }
}
