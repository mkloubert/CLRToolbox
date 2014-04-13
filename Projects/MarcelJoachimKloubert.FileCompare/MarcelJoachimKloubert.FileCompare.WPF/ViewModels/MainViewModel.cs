// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.FileCompare.WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcelJoachimKloubert.FileCompare.WPF.ViewModels
{
    public sealed class MainViewModel : NotificationObjectBase
    {
        private IEnumerable<CompareTask> _tasks;
        private CompareTask _selectedTask;

        public IEnumerable<CompareTask> Tasks
        {
            get { return this._tasks; }

            set { this.SetProperty(ref this._tasks, value); }
        }

        public CompareTask SelectedTask
        {
            get { return this._selectedTask; }

            set { this.SetProperty(ref this._selectedTask, value); }
        }

        /// <inheriteddoc />
        protected override void OnConstructor()
        {
            base.OnConstructor();
        }
    }
}
