// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServerProcessManager.Helpers;
using AppServerProcessManager.JSON.ListProcesses;
using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;

namespace AppServerProcessManager.Views
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public sealed partial class MainViewModel : NotificationObjectBase
    {
        #region Fields (2)

        private bool _isBusy;
        private IList<RemoteProcess> _processes;

        #endregion Fields

        #region Properties (4)

        /// <summary>
        /// Gets the command for the exit main menu entry.
        /// </summary>
        public SimpleCommand ExitCommand
        {
            get;
            private set;
        }

        public bool IsBusy
        {
            get { return this._isBusy; }

            private set { this.SetProperty(ref this._isBusy, value); }
        }

        /// <summary>
        /// Gets the current list of loaded processes.
        /// </summary>
        public IList<RemoteProcess> Processes
        {
            get { return this._processes; }

            private set { this.SetProperty(ref this._processes, value); }
        }

        /// <summary>
        /// Gets the command for the test main menu entry.
        /// </summary>
        public SimpleCommand TestCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (7)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="NotificationObjectBase.OnConstructor()" />
        protected override void OnConstructor()
        {
            base.OnConstructor();

            this.TestCommand = new SimpleCommand(this.Test);
            this.TestCommand.ExecutionError += this.Command_ExecutionError;

            this.ExitCommand = new SimpleCommand(this.Exit);
            this.ExitCommand.ExecutionError += this.Command_ExecutionError;
        }
        // Private Methods (6) 

        private void Command_ExecutionError(object sender, ExecutionErrorEventArgs<object> e)
        {
            this.OnError(e.Exception);
        }

        private void Exit()
        {
            this.OnClosing();
        }

        private void OnBusy(Action<MainViewModel> action)
        {
            this.OnBusy<object>((vm, s) => action(vm),
                                null);
        }

        private void OnBusy<T>(Action<MainViewModel, T> action, T state)
        {
            Task.Factory
                .StartNew((s) =>
                {
                    var args = (OnBusyArgs<T>)s;
                    lock (args.VIEW_MODEL._SYNC)
                    {
                        try
                        {
                            args.VIEW_MODEL
                                .IsBusy = true;

                            args.InvokeAction();
                        }
                        catch (Exception ex)
                        {
                            args.VIEW_MODEL
                                .OnError(ex);
                        }
                        finally
                        {
                            args.VIEW_MODEL
                                .IsBusy = false;
                        }
                    }
                }, new OnBusyArgs<T>(vm: this,
                                     action: action,
                                     state: state));
        }

        private void Test()
        {
            this.OnBusy(Test_OnBusy);
        }

        private static void Test_OnBusy(MainViewModel vm)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:23979/exec/0F4EC862D33746E1BB6DA54734DF0D7B");
            request.Method = "POST";
            request.SetBasicAuth("test", "test");
            request.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };

            using (var reqStream = request.GetRequestStream())
            {
                var inputParams = new Dictionary<string, object>();
                inputParams["AllModules"] = true;
                inputParams["WithIcon"] = true;

                var data = Encoding.UTF8.GetBytes(JsonHelper.Serialize(inputParams));
                reqStream.Write(data, 0, data.Length);

                reqStream.Close();
            }

            ListProcessesFuncResult funcResult;
            {
                var response = request.GetResponse();
                using (var respStream = response.GetResponseStream())
                {
                    funcResult = JsonHelper.Deserialize<ListProcessesFuncResult>(respStream);
                }
            }

            vm.Processes = new ObservableCollection<RemoteProcess>(collection: funcResult.Parameters);
        }

        #endregion Methods
    }
}
