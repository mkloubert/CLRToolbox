// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Windows.Forms;

namespace MarcelJoachimKloubert.CryptCommander.Forms
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

        private void ToolStripMenuItem_File_Exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #endregion Methods
    }
}
