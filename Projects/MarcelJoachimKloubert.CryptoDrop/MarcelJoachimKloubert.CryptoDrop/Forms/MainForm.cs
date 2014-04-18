// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using System;
using System.IO;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CryptoDrop.Forms
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

        public IConfigRepository Config
        {
            get;
            private set;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var configFile = new FileInfo(Path.Combine(Environment.CurrentDirectory,
                                                       "config.ini"));

            this.Config = new IniFileConfigRepository(configFile,
                                                      isReadOnly: false);
        }
    }
}