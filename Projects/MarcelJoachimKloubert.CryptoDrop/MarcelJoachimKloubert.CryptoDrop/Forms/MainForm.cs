// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.WinForms.Controls;
using System;
using System.IO;

namespace MarcelJoachimKloubert.CryptoDrop.Forms
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : BorderlessMoveableForm
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
        /// Gets the configuration for that application.
        /// </summary>
        public IConfigRepository Config
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (1)

        // Private Methods (1) 

        private void MainForm_Load(object sender, EventArgs e)
        {
            var configFile = new FileInfo(Path.Combine(Environment.CurrentDirectory,
                                                       "config.ini"));

            this.Config = new IniFileConfigRepository(configFile,
                                                      isReadOnly: false);
        }

        #endregion Methods
    }
}