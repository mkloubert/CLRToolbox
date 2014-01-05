// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Windows.Forms;
using MarcelJoachimKloubert.FileSyncer.Jobs;

namespace MarcelJoachimKloubert.FileSyncer.Forms
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

        #region Methods (1)

        // Private Methods (1) 

        private void FileToolStripMenuItem_Main_File_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Methods

        private void MainForm_Load(object sender, EventArgs e)
        {
            var test = new SyncJob();
            test.SourceDirectory = @"E:\Dev\fs\src";
            test.DestionationDirectory = @"E:\Dev\fs\dest";

            test.Start();
        }
    }
}
