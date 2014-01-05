namespace MarcelJoachimKloubert.FileSyncer.Forms
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
            this.MenuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem_Main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripMenuItem_Main_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.ToolStrip_Main = new System.Windows.Forms.ToolStrip();
            this.ElementHost_SyncJobs = new System.Windows.Forms.Integration.ElementHost();
            this.SyncJobList_Main = new MarcelJoachimKloubert.FileSyncer.WPF.Controls.SyncJobList();
            this.MenuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip_Main
            // 
            this.MenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem_Main_File});
            this.MenuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_Main.Name = "MenuStrip_Main";
            this.MenuStrip_Main.Size = new System.Drawing.Size(624, 24);
            this.MenuStrip_Main.TabIndex = 0;
            // 
            // FileToolStripMenuItem_Main_File
            // 
            this.FileToolStripMenuItem_Main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem_Main_File_Exit});
            this.FileToolStripMenuItem_Main_File.Name = "FileToolStripMenuItem_Main_File";
            this.FileToolStripMenuItem_Main_File.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem_Main_File.Text = "File";
            // 
            // FileToolStripMenuItem_Main_File_Exit
            // 
            this.FileToolStripMenuItem_Main_File_Exit.Name = "FileToolStripMenuItem_Main_File_Exit";
            this.FileToolStripMenuItem_Main_File_Exit.Size = new System.Drawing.Size(92, 22);
            this.FileToolStripMenuItem_Main_File_Exit.Text = "Exit";
            this.FileToolStripMenuItem_Main_File_Exit.Click += new System.EventHandler(this.FileToolStripMenuItem_Main_File_Exit_Click);
            // 
            // StatusStrip_Main
            // 
            this.StatusStrip_Main.Location = new System.Drawing.Point(0, 339);
            this.StatusStrip_Main.Name = "StatusStrip_Main";
            this.StatusStrip_Main.Size = new System.Drawing.Size(624, 22);
            this.StatusStrip_Main.TabIndex = 1;
            this.StatusStrip_Main.Text = "statusStrip1";
            // 
            // ToolStrip_Main
            // 
            this.ToolStrip_Main.Location = new System.Drawing.Point(0, 24);
            this.ToolStrip_Main.Name = "ToolStrip_Main";
            this.ToolStrip_Main.Size = new System.Drawing.Size(624, 25);
            this.ToolStrip_Main.TabIndex = 2;
            // 
            // ElementHost_SyncJobs
            // 
            this.ElementHost_SyncJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementHost_SyncJobs.Location = new System.Drawing.Point(0, 49);
            this.ElementHost_SyncJobs.Name = "ElementHost_SyncJobs";
            this.ElementHost_SyncJobs.Size = new System.Drawing.Size(624, 290);
            this.ElementHost_SyncJobs.TabIndex = 3;
            this.ElementHost_SyncJobs.Text = "elementHost1";
            this.ElementHost_SyncJobs.Child = this.SyncJobList_Main;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.ElementHost_SyncJobs);
            this.Controls.Add(this.ToolStrip_Main);
            this.Controls.Add(this.StatusStrip_Main);
            this.Controls.Add(this.MenuStrip_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.MenuStrip_Main;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Syncer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MenuStrip_Main.ResumeLayout(false);
            this.MenuStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip_Main;
        private System.Windows.Forms.StatusStrip StatusStrip_Main;
        private System.Windows.Forms.ToolStrip ToolStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem_Main_File;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem_Main_File_Exit;
        private System.Windows.Forms.Integration.ElementHost ElementHost_SyncJobs;
        private WPF.Controls.SyncJobList SyncJobList_Main;
    }
}

