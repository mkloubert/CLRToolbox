namespace MarcelJoachimKloubert.GitThemAll.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GroupBox_Repository = new System.Windows.Forms.GroupBox();
            this.Button_SelectRepository = new System.Windows.Forms.Button();
            this.TextBox_RepositoryPath = new System.Windows.Forms.TextBox();
            this.GroupBox_Remotes = new System.Windows.Forms.GroupBox();
            this.Button_Pull = new System.Windows.Forms.Button();
            this.Button_Push = new System.Windows.Forms.Button();
            this.ListView_Remotes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox_Protocol = new System.Windows.Forms.GroupBox();
            this.Button_ClearProtocol = new System.Windows.Forms.Button();
            this.TextBox_Protocol = new System.Windows.Forms.TextBox();
            this.StatusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel_Main = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripProgressBar_Main = new System.Windows.Forms.ToolStripProgressBar();
            this.GroupBox_Repository.SuspendLayout();
            this.GroupBox_Remotes.SuspendLayout();
            this.GroupBox_Protocol.SuspendLayout();
            this.StatusStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_Repository
            // 
            this.GroupBox_Repository.Controls.Add(this.Button_SelectRepository);
            this.GroupBox_Repository.Controls.Add(this.TextBox_RepositoryPath);
            this.GroupBox_Repository.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_Repository.Name = "GroupBox_Repository";
            this.GroupBox_Repository.Size = new System.Drawing.Size(600, 51);
            this.GroupBox_Repository.TabIndex = 0;
            this.GroupBox_Repository.TabStop = false;
            this.GroupBox_Repository.Text = " Repository ";
            // 
            // Button_SelectRepository
            // 
            this.Button_SelectRepository.Location = new System.Drawing.Point(562, 19);
            this.Button_SelectRepository.Name = "Button_SelectRepository";
            this.Button_SelectRepository.Size = new System.Drawing.Size(32, 20);
            this.Button_SelectRepository.TabIndex = 2;
            this.Button_SelectRepository.Text = "...";
            this.Button_SelectRepository.UseVisualStyleBackColor = true;
            this.Button_SelectRepository.Click += new System.EventHandler(this.Button_SelectRepository_Click);
            // 
            // TextBox_RepositoryPath
            // 
            this.TextBox_RepositoryPath.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox_RepositoryPath.Location = new System.Drawing.Point(6, 19);
            this.TextBox_RepositoryPath.Name = "TextBox_RepositoryPath";
            this.TextBox_RepositoryPath.ReadOnly = true;
            this.TextBox_RepositoryPath.Size = new System.Drawing.Size(550, 20);
            this.TextBox_RepositoryPath.TabIndex = 1;
            // 
            // GroupBox_Remotes
            // 
            this.GroupBox_Remotes.Controls.Add(this.Button_Pull);
            this.GroupBox_Remotes.Controls.Add(this.Button_Push);
            this.GroupBox_Remotes.Controls.Add(this.ListView_Remotes);
            this.GroupBox_Remotes.Location = new System.Drawing.Point(12, 69);
            this.GroupBox_Remotes.Name = "GroupBox_Remotes";
            this.GroupBox_Remotes.Size = new System.Drawing.Size(276, 280);
            this.GroupBox_Remotes.TabIndex = 3;
            this.GroupBox_Remotes.TabStop = false;
            this.GroupBox_Remotes.Text = " Remotes ";
            // 
            // Button_Pull
            // 
            this.Button_Pull.Location = new System.Drawing.Point(141, 245);
            this.Button_Pull.Name = "Button_Pull";
            this.Button_Pull.Size = new System.Drawing.Size(129, 29);
            this.Button_Pull.TabIndex = 6;
            this.Button_Pull.Text = "Pull";
            this.Button_Pull.UseVisualStyleBackColor = true;
            // 
            // Button_Push
            // 
            this.Button_Push.Location = new System.Drawing.Point(6, 245);
            this.Button_Push.Name = "Button_Push";
            this.Button_Push.Size = new System.Drawing.Size(129, 29);
            this.Button_Push.TabIndex = 5;
            this.Button_Push.Text = "Push";
            this.Button_Push.UseVisualStyleBackColor = true;
            this.Button_Push.Click += new System.EventHandler(this.Button_Push_Click);
            // 
            // ListView_Remotes
            // 
            this.ListView_Remotes.CheckBoxes = true;
            this.ListView_Remotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ListView_Remotes.FullRowSelect = true;
            this.ListView_Remotes.Location = new System.Drawing.Point(6, 19);
            this.ListView_Remotes.Name = "ListView_Remotes";
            this.ListView_Remotes.Size = new System.Drawing.Size(264, 220);
            this.ListView_Remotes.TabIndex = 4;
            this.ListView_Remotes.UseCompatibleStateImageBehavior = false;
            this.ListView_Remotes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 96;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Url";
            this.columnHeader2.Width = 160;
            // 
            // GroupBox_Protocol
            // 
            this.GroupBox_Protocol.Controls.Add(this.Button_ClearProtocol);
            this.GroupBox_Protocol.Controls.Add(this.TextBox_Protocol);
            this.GroupBox_Protocol.Location = new System.Drawing.Point(294, 69);
            this.GroupBox_Protocol.Name = "GroupBox_Protocol";
            this.GroupBox_Protocol.Size = new System.Drawing.Size(318, 280);
            this.GroupBox_Protocol.TabIndex = 7;
            this.GroupBox_Protocol.TabStop = false;
            this.GroupBox_Protocol.Text = " Protocoll ";
            // 
            // Button_ClearProtocol
            // 
            this.Button_ClearProtocol.Location = new System.Drawing.Point(6, 245);
            this.Button_ClearProtocol.Name = "Button_ClearProtocol";
            this.Button_ClearProtocol.Size = new System.Drawing.Size(306, 29);
            this.Button_ClearProtocol.TabIndex = 9;
            this.Button_ClearProtocol.Text = "Clear";
            this.Button_ClearProtocol.UseVisualStyleBackColor = true;
            this.Button_ClearProtocol.Click += new System.EventHandler(this.Button_ClearProtocol_Click);
            // 
            // TextBox_Protocol
            // 
            this.TextBox_Protocol.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_Protocol.Location = new System.Drawing.Point(6, 19);
            this.TextBox_Protocol.Multiline = true;
            this.TextBox_Protocol.Name = "TextBox_Protocol";
            this.TextBox_Protocol.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Protocol.Size = new System.Drawing.Size(306, 220);
            this.TextBox_Protocol.TabIndex = 8;
            this.TextBox_Protocol.WordWrap = false;
            // 
            // StatusStrip_Main
            // 
            this.StatusStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripProgressBar_Main,
            this.ToolStripStatusLabel_Main});
            this.StatusStrip_Main.Location = new System.Drawing.Point(0, 363);
            this.StatusStrip_Main.Name = "StatusStrip_Main";
            this.StatusStrip_Main.Size = new System.Drawing.Size(624, 22);
            this.StatusStrip_Main.TabIndex = 8;
            this.StatusStrip_Main.Text = "statusStrip1";
            // 
            // ToolStripStatusLabel_Main
            // 
            this.ToolStripStatusLabel_Main.Name = "ToolStripStatusLabel_Main";
            this.ToolStripStatusLabel_Main.Size = new System.Drawing.Size(51, 17);
            this.ToolStripStatusLabel_Main.Text = "@TODO";
            this.ToolStripStatusLabel_Main.Visible = false;
            // 
            // ToolStripProgressBar_Main
            // 
            this.ToolStripProgressBar_Main.Name = "ToolStripProgressBar_Main";
            this.ToolStripProgressBar_Main.Size = new System.Drawing.Size(128, 16);
            this.ToolStripProgressBar_Main.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 385);
            this.Controls.Add(this.StatusStrip_Main);
            this.Controls.Add(this.GroupBox_Protocol);
            this.Controls.Add(this.GroupBox_Remotes);
            this.Controls.Add(this.GroupBox_Repository);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GitThemAll";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.GroupBox_Repository.ResumeLayout(false);
            this.GroupBox_Repository.PerformLayout();
            this.GroupBox_Remotes.ResumeLayout(false);
            this.GroupBox_Protocol.ResumeLayout(false);
            this.GroupBox_Protocol.PerformLayout();
            this.StatusStrip_Main.ResumeLayout(false);
            this.StatusStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_Repository;
        private System.Windows.Forms.Button Button_SelectRepository;
        private System.Windows.Forms.TextBox TextBox_RepositoryPath;
        private System.Windows.Forms.GroupBox GroupBox_Remotes;
        private System.Windows.Forms.Button Button_Pull;
        private System.Windows.Forms.Button Button_Push;
        private System.Windows.Forms.ListView ListView_Remotes;
        private System.Windows.Forms.GroupBox GroupBox_Protocol;
        private System.Windows.Forms.Button Button_ClearProtocol;
        private System.Windows.Forms.TextBox TextBox_Protocol;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.StatusStrip StatusStrip_Main;
        private System.Windows.Forms.ToolStripProgressBar ToolStripProgressBar_Main;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel_Main;
    }
}

