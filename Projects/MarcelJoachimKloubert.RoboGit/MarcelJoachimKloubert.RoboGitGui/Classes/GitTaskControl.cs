// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.RoboGitGui.Classes
{
    /// <summary>
    /// Handles a git task.
    /// </summary>
    public partial class GitTaskControl : UserControl
    {
        #region Constructors (1)

        internal GitTaskControl(GitTask task)
        {
            this.InitializeComponent();

            this.Task = task;

            this.Task.Error += this.Task_Error;
            this.Task.Started += this.Task_Started;
            this.Task.Stopped += this.Task_Stopped;
            this.Task.LogMessageReceived += this.Task_LogMessageReceived;

            this.TextBox_DisplayName.Text = this.Task.DisplayName ?? string.Empty;
            this.TextBox_InternalName.Text = this.Task.Name ?? string.Empty;

            this.UpdateButtonStates();
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying task.
        /// </summary>
        public GitTask Task
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (8)

        // Private Methods (8) 

        private void Button_Start_Click(object sender, EventArgs e)
        {
            try
            {
                this.Task.Start();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                this.Task.Stop();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void ShowError(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            try
            {
                this.InvokeSafe((ctrl, state) =>
                    {
                        try
                        {
                            MessageBox.Show(ctrl,
                                            text: state.Exception.ToString(),
                                            caption: string.Format("[ERROR from '{0}'] {1}",
                                                                   ctrl.Task.DisplayName,
                                                                   state.Exception.GetType().FullName),
                                            buttons: MessageBoxButtons.OK,
                                            icon: MessageBoxIcon.Error);
                        }
                        catch
                        {
                            // ignore
                        }
                    }, actionState: new
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
            try
            {
                this.ShowError(e.GetException());
            }
            catch
            {
                // ignore here
            }

        }

        private void Task_LogMessageReceived(object sender, GitTaskLogEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                // ignore
            }
        }

        private void Task_Started(object sender, EventArgs e)
        {
            try
            {
                this.UpdateButtonStates();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void Task_Stopped(object sender, EventArgs e)
        {
            try
            {
                this.UpdateButtonStates();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }

        private void UpdateButtonStates()
        {
            this.Button_Start
                .InvokeSafe((btn, state) =>
                {
                    btn.Enabled = state.Task.IsRunning == false;
                }, new
                {
                    Task = this.Task,
                });

            this.Button_Stop
                .InvokeSafe((btn, state) =>
                {
                    btn.Enabled = state.Task.IsRunning;
                }, new
                {
                    Task = this.Task,
                });
        }

        #endregion Methods
    }
}
