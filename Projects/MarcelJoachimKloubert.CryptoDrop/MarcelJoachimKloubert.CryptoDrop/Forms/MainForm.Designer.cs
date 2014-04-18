namespace MarcelJoachimKloubert.CryptoDrop.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ContextMenuStrip_Main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Main_Mode = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_Mode_Encrypt = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_Mode_Decrypt = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Main_UncryptedDir = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_UncryptedDir_Current = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_CryptedDir = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_CryptedDir_Current = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Main_TargetFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Main_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgressBar_AllFiles = new System.Windows.Forms.ProgressBar();
            this.ProgressBar_CurrentFile = new System.Windows.Forms.ProgressBar();
            this.Label_Info = new System.Windows.Forms.Label();
            this.ContextMenuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextMenuStrip_Main
            // 
            this.ContextMenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Main_Mode,
            this.ToolStripMenuItem_Main_Separator1,
            this.ToolStripMenuItem_Main_UncryptedDir,
            this.ToolStripMenuItem_Main_CryptedDir,
            this.ToolStripMenuItem_Main_Separator2,
            this.ToolStripMenuItem_Main_TargetFiles,
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_Main_Stop});
            this.ContextMenuStrip_Main.Name = "ContextMenuStrip_Main";
            this.ContextMenuStrip_Main.Size = new System.Drawing.Size(180, 132);
            // 
            // ToolStripMenuItem_Main_Mode
            // 
            this.ToolStripMenuItem_Main_Mode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Main_Mode_Encrypt,
            this.ToolStripMenuItem_Main_Mode_Decrypt});
            this.ToolStripMenuItem_Main_Mode.Name = "ToolStripMenuItem_Main_Mode";
            this.ToolStripMenuItem_Main_Mode.Size = new System.Drawing.Size(179, 22);
            this.ToolStripMenuItem_Main_Mode.Text = "Mode";
            // 
            // ToolStripMenuItem_Main_Mode_Encrypt
            // 
            this.ToolStripMenuItem_Main_Mode_Encrypt.Name = "ToolStripMenuItem_Main_Mode_Encrypt";
            this.ToolStripMenuItem_Main_Mode_Encrypt.Size = new System.Drawing.Size(115, 22);
            this.ToolStripMenuItem_Main_Mode_Encrypt.Text = "Encrypt";
            // 
            // ToolStripMenuItem_Main_Mode_Decrypt
            // 
            this.ToolStripMenuItem_Main_Mode_Decrypt.Name = "ToolStripMenuItem_Main_Mode_Decrypt";
            this.ToolStripMenuItem_Main_Mode_Decrypt.Size = new System.Drawing.Size(115, 22);
            this.ToolStripMenuItem_Main_Mode_Decrypt.Text = "Decrypt";
            // 
            // ToolStripMenuItem_Main_Separator1
            // 
            this.ToolStripMenuItem_Main_Separator1.Name = "ToolStripMenuItem_Main_Separator1";
            this.ToolStripMenuItem_Main_Separator1.Size = new System.Drawing.Size(176, 6);
            // 
            // ToolStripMenuItem_Main_UncryptedDir
            // 
            this.ToolStripMenuItem_Main_UncryptedDir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Main_UncryptedDir_Current});
            this.ToolStripMenuItem_Main_UncryptedDir.Name = "ToolStripMenuItem_Main_UncryptedDir";
            this.ToolStripMenuItem_Main_UncryptedDir.Size = new System.Drawing.Size(179, 22);
            this.ToolStripMenuItem_Main_UncryptedDir.Text = "Uncrypted directory";
            // 
            // ToolStripMenuItem_Main_UncryptedDir_Current
            // 
            this.ToolStripMenuItem_Main_UncryptedDir_Current.Name = "ToolStripMenuItem_Main_UncryptedDir_Current";
            this.ToolStripMenuItem_Main_UncryptedDir_Current.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_Main_UncryptedDir_Current.Text = "...";
            // 
            // ToolStripMenuItem_Main_CryptedDir
            // 
            this.ToolStripMenuItem_Main_CryptedDir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Main_CryptedDir_Current});
            this.ToolStripMenuItem_Main_CryptedDir.Name = "ToolStripMenuItem_Main_CryptedDir";
            this.ToolStripMenuItem_Main_CryptedDir.Size = new System.Drawing.Size(179, 22);
            this.ToolStripMenuItem_Main_CryptedDir.Text = "Crypted directory";
            // 
            // ToolStripMenuItem_Main_CryptedDir_Current
            // 
            this.ToolStripMenuItem_Main_CryptedDir_Current.Name = "ToolStripMenuItem_Main_CryptedDir_Current";
            this.ToolStripMenuItem_Main_CryptedDir_Current.Size = new System.Drawing.Size(83, 22);
            this.ToolStripMenuItem_Main_CryptedDir_Current.Text = "...";
            // 
            // ToolStripMenuItem_Main_Separator2
            // 
            this.ToolStripMenuItem_Main_Separator2.Name = "ToolStripMenuItem_Main_Separator2";
            this.ToolStripMenuItem_Main_Separator2.Size = new System.Drawing.Size(176, 6);
            // 
            // ToolStripMenuItem_Main_TargetFiles
            // 
            this.ToolStripMenuItem_Main_TargetFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs,
            this.ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles});
            this.ToolStripMenuItem_Main_TargetFiles.Name = "ToolStripMenuItem_Main_TargetFiles";
            this.ToolStripMenuItem_Main_TargetFiles.Size = new System.Drawing.Size(179, 22);
            this.ToolStripMenuItem_Main_TargetFiles.Text = "Target files";
            // 
            // ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs
            // 
            this.ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs.Name = "ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs";
            this.ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs.Size = new System.Drawing.Size(210, 22);
            this.ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs.Text = "Cleanup target directories";
            // 
            // ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles
            // 
            this.ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles.Name = "ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles";
            this.ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles.Size = new System.Drawing.Size(210, 22);
            this.ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles.Text = "Overwrite existing files";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(176, 6);
            // 
            // ToolStripMenuItem_Main_Stop
            // 
            this.ToolStripMenuItem_Main_Stop.Name = "ToolStripMenuItem_Main_Stop";
            this.ToolStripMenuItem_Main_Stop.Size = new System.Drawing.Size(179, 22);
            this.ToolStripMenuItem_Main_Stop.Text = "Stop";
            // 
            // ProgressBar_AllFiles
            // 
            this.ProgressBar_AllFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressBar_AllFiles.Location = new System.Drawing.Point(0, 105);
            this.ProgressBar_AllFiles.Name = "ProgressBar_AllFiles";
            this.ProgressBar_AllFiles.Size = new System.Drawing.Size(144, 16);
            this.ProgressBar_AllFiles.TabIndex = 1;
            this.ProgressBar_AllFiles.Visible = false;
            // 
            // ProgressBar_CurrentFile
            // 
            this.ProgressBar_CurrentFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressBar_CurrentFile.Location = new System.Drawing.Point(0, 89);
            this.ProgressBar_CurrentFile.Name = "ProgressBar_CurrentFile";
            this.ProgressBar_CurrentFile.Size = new System.Drawing.Size(144, 16);
            this.ProgressBar_CurrentFile.TabIndex = 2;
            this.ProgressBar_CurrentFile.Visible = false;
            // 
            // Label_Info
            // 
            this.Label_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_Info.Location = new System.Drawing.Point(0, 0);
            this.Label_Info.Name = "Label_Info";
            this.Label_Info.Size = new System.Drawing.Size(144, 89);
            this.Label_Info.TabIndex = 3;
            this.Label_Info.Text = "Drop your files here...";
            this.Label_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 121);
            this.ContextMenuStrip = this.ContextMenuStrip_Main;
            this.Controls.Add(this.Label_Info);
            this.Controls.Add(this.ProgressBar_CurrentFile);
            this.Controls.Add(this.ProgressBar_AllFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CryptoDrop";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ContextMenuStrip_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_Mode;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_Mode_Encrypt;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_Mode_Decrypt;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Main_Separator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_UncryptedDir;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_CryptedDir;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Main_Separator2;
        private System.Windows.Forms.ProgressBar ProgressBar_AllFiles;
        private System.Windows.Forms.ProgressBar ProgressBar_CurrentFile;
        private System.Windows.Forms.Label Label_Info;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_Stop;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_UncryptedDir_Current;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_CryptedDir_Current;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_TargetFiles;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_TargetFiles_CleanupTargetDirs;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Main_TargetFiles_OverwriteExistingFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

