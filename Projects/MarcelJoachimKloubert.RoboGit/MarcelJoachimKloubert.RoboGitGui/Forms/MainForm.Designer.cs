namespace MarcelJoachimKloubert.RoboGitGui.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StatusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.MenuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.SplitContainer_Main = new System.Windows.Forms.SplitContainer();
            this.ListView_Tasks = new System.Windows.Forms.ListView();
            this.ColumnHeader_TaskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList_GitTasks = new System.Windows.Forms.ImageList(this.components);
            this.Button_GitAll = new System.Windows.Forms.Button();
            this.Button_GitSelected = new System.Windows.Forms.Button();
            this.Button_ReloadConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Main)).BeginInit();
            this.SplitContainer_Main.Panel1.SuspendLayout();
            this.SplitContainer_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip_Main
            // 
            this.StatusStrip_Main.Location = new System.Drawing.Point(0, 539);
            this.StatusStrip_Main.Name = "StatusStrip_Main";
            this.StatusStrip_Main.Size = new System.Drawing.Size(784, 22);
            this.StatusStrip_Main.TabIndex = 1;
            this.StatusStrip_Main.Text = "statusStrip1";
            // 
            // MenuStrip_Main
            // 
            this.MenuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_Main.Name = "MenuStrip_Main";
            this.MenuStrip_Main.Size = new System.Drawing.Size(784, 24);
            this.MenuStrip_Main.TabIndex = 2;
            this.MenuStrip_Main.Text = "menuStrip1";
            // 
            // SplitContainer_Main
            // 
            this.SplitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer_Main.Location = new System.Drawing.Point(0, 24);
            this.SplitContainer_Main.Name = "SplitContainer_Main";
            // 
            // SplitContainer_Main.Panel1
            // 
            this.SplitContainer_Main.Panel1.Controls.Add(this.ListView_Tasks);
            this.SplitContainer_Main.Panel1.Controls.Add(this.Button_GitAll);
            this.SplitContainer_Main.Panel1.Controls.Add(this.Button_GitSelected);
            this.SplitContainer_Main.Panel1.Controls.Add(this.Button_ReloadConfig);
            this.SplitContainer_Main.Size = new System.Drawing.Size(784, 515);
            this.SplitContainer_Main.SplitterDistance = 268;
            this.SplitContainer_Main.TabIndex = 3;
            // 
            // ListView_Tasks
            // 
            this.ListView_Tasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_TaskName});
            this.ListView_Tasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Tasks.FullRowSelect = true;
            this.ListView_Tasks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListView_Tasks.LargeImageList = this.ImageList_GitTasks;
            this.ListView_Tasks.Location = new System.Drawing.Point(0, 0);
            this.ListView_Tasks.Name = "ListView_Tasks";
            this.ListView_Tasks.Size = new System.Drawing.Size(268, 419);
            this.ListView_Tasks.SmallImageList = this.ImageList_GitTasks;
            this.ListView_Tasks.TabIndex = 3;
            this.ListView_Tasks.UseCompatibleStateImageBehavior = false;
            this.ListView_Tasks.View = System.Windows.Forms.View.Details;
            this.ListView_Tasks.SelectedIndexChanged += new System.EventHandler(this.ListView_Tasks_SelectedIndexChanged);
            // 
            // ColumnHeader_TaskName
            // 
            this.ColumnHeader_TaskName.Text = "Name";
            this.ColumnHeader_TaskName.Width = 256;
            // 
            // ImageList_GitTasks
            // 
            this.ImageList_GitTasks.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_GitTasks.ImageStream")));
            this.ImageList_GitTasks.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_GitTasks.Images.SetKeyName(0, "1396270533_icon-upload.png");
            this.ImageList_GitTasks.Images.SetKeyName(1, "1396270533_519624-123_CloudDownload.png");
            this.ImageList_GitTasks.Images.SetKeyName(2, "1396270638_Gnome-System-Log-Out-64.png");
            // 
            // Button_GitAll
            // 
            this.Button_GitAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_GitAll.Location = new System.Drawing.Point(0, 419);
            this.Button_GitAll.Name = "Button_GitAll";
            this.Button_GitAll.Size = new System.Drawing.Size(268, 32);
            this.Button_GitAll.TabIndex = 2;
            this.Button_GitAll.Text = "Git all";
            this.Button_GitAll.UseVisualStyleBackColor = true;
            this.Button_GitAll.Click += new System.EventHandler(this.Button_GitAll_Click);
            // 
            // Button_GitSelected
            // 
            this.Button_GitSelected.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_GitSelected.Location = new System.Drawing.Point(0, 451);
            this.Button_GitSelected.Name = "Button_GitSelected";
            this.Button_GitSelected.Size = new System.Drawing.Size(268, 32);
            this.Button_GitSelected.TabIndex = 1;
            this.Button_GitSelected.Text = "Git selected";
            this.Button_GitSelected.UseVisualStyleBackColor = true;
            this.Button_GitSelected.Click += new System.EventHandler(this.Button_GitSelected_Click);
            // 
            // Button_ReloadConfig
            // 
            this.Button_ReloadConfig.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_ReloadConfig.Location = new System.Drawing.Point(0, 483);
            this.Button_ReloadConfig.Name = "Button_ReloadConfig";
            this.Button_ReloadConfig.Size = new System.Drawing.Size(268, 32);
            this.Button_ReloadConfig.TabIndex = 0;
            this.Button_ReloadConfig.Text = "Reload config";
            this.Button_ReloadConfig.UseVisualStyleBackColor = true;
            this.Button_ReloadConfig.Click += new System.EventHandler(this.Button_ReloadConfig_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.SplitContainer_Main);
            this.Controls.Add(this.StatusStrip_Main);
            this.Controls.Add(this.MenuStrip_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.MenuStrip_Main;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RoboGit GUI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SplitContainer_Main.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Main)).EndInit();
            this.SplitContainer_Main.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStrip_Main;
        private System.Windows.Forms.MenuStrip MenuStrip_Main;
        private System.Windows.Forms.SplitContainer SplitContainer_Main;
        private System.Windows.Forms.ListView ListView_Tasks;
        private System.Windows.Forms.ColumnHeader ColumnHeader_TaskName;
        private System.Windows.Forms.Button Button_GitAll;
        private System.Windows.Forms.Button Button_GitSelected;
        private System.Windows.Forms.Button Button_ReloadConfig;
        private System.Windows.Forms.ImageList ImageList_GitTasks;

    }
}

