// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Windows.Forms;
using LibGit2Sharp;

namespace MarcelJoachimKloubert.GitThemAll.Forms
{
    /// <summary>
    /// Remote credential form.
    /// </summary>
    public partial class CredentialForm : Form
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialForm"/> class.
        /// </summary>
        /// <param name="remote">The value for <see cref="CredentialForm.Remote" /> property.</param>
        public CredentialForm(Remote remote)
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the credentials to use.
        /// </summary>
        public Credentials Credentials
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the underlying remote.
        /// </summary>
        public Remote Remote
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (2) 

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Credentials = null;

            this.DialogResult = DialogResult.Cancel;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            var cred = new Credentials();
            cred.Username = (this.TextBox_Username.Text ?? string.Empty).Trim();
            cred.Password = string.IsNullOrEmpty(this.TextBox_Password.Text) ? string.Empty : this.TextBox_Password.Text;

            this.Credentials = cred.Username != string.Empty ? cred : null;

            this.DialogResult = DialogResult.OK;
        }

        #endregion Methods
    }
}
