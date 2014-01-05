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
            this.StatusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.ToolStrip_Main = new System.Windows.Forms.ToolStrip();
            this.ListView_Jobs = new System.Windows.Forms.ListView();
            this.Panel_JobInfo = new System.Windows.Forms.Panel();
            this.FileToolStripMenuItem_Main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripMenuItem_Main_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
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
            // ListView_Jobs
            // 
            this.ListView_Jobs.Dock = System.Windows.Forms.DockStyle.Left;
            this.ListView_Jobs.Location = new System.Drawing.Point(0, 49);
            this.ListView_Jobs.Name = "ListView_Jobs";
            this.ListView_Jobs.Size = new System.Drawing.Size(224, 290);
            this.ListView_Jobs.TabIndex = 3;
            this.ListView_Jobs.UseCompatibleStateImageBehavior = false;
            // 
            // Panel_JobInfo
            // 
            this.Panel_JobInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_JobInfo.Location = new System.Drawing.Point(224, 49);
            this.Panel_JobInfo.Name = "Panel_JobInfo";
            this.Panel_JobInfo.Size = new System.Drawing.Size(400, 290);
            this.Panel_JobInfo.TabIndex = 4;
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
            this.FileToolStripMenuItem_Main_File_Exit.Size = new System.Drawing.Size(152, 22);
            this.FileToolStripMenuItem_Main_File_Exit.Text = "Exit";
            this.FileToolStripMenuItem_Main_File_Exit.Click += new System.EventHandler(this.FileToolStripMenuItem_Main_File_Exit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.Panel_JobInfo);
            this.Controls.Add(this.ListView_Jobs);
            this.Controls.Add(this.ToolStrip_Main);
            this.Controls.Add(this.StatusStrip_Main);
            this.Controls.Add(this.MenuStrip_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.MenuStrip_Main;
            this.MaximizeBox = false;
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
        private System.Windows.Forms.ListView ListView_Jobs;
        private System.Windows.Forms.Panel Panel_JobInfo;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem_Main_File;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem_Main_File_Exit;
    }
}

