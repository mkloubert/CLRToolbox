using System;
using System.Windows.Forms;
using MarcelJoachimKloubert.MusicSorter.Helpers;

namespace MarcelJoachimKloubert.MusicSorter.Forms
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructors (1)

        public MainForm()
        {
            this.InitializeComponent();

            VlcHelper.FixupVlcControl(this.VlcControl_Main);
        }

        #endregion Constructors

        #region Methods (1)

        // Private Methods (1) 

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        #endregion Methods
    }
}
