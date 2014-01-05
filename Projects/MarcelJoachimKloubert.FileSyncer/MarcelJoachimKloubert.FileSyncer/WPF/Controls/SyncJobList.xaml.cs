// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Windows.Controls;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.FileSyncer.Jobs;

namespace MarcelJoachimKloubert.FileSyncer.WPF.Controls
{
    /// <summary>
    /// Code behind of "SyncJobList.xaml"
    /// </summary>
    public partial class SyncJobList : UserControl
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncJobList" /> class.
        /// </summary>
        public SyncJobList()
        {
            this.InitializeComponent();

            this.Jobs = new SynchronizedObservableCollection<ISyncJob>();

            this.DataContext = this;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the list of jobs.
        /// </summary>
        public IList<ISyncJob> Jobs
        {
            get;
            private set;
        }

        #endregion Properties
    }
}
