namespace MarcelJoachimKloubert.CloudNET.FileSync.Controls.Forms
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
            this.NotifyIcon_Main = new System.Windows.Forms.NotifyIcon(this.components);
            this.GroupBox_Server = new System.Windows.Forms.GroupBox();
            this.Button_StartStop = new System.Windows.Forms.Button();
            this.Local_ServerAddress = new System.Windows.Forms.Label();
            this.ComboBox_Protocol = new System.Windows.Forms.ComboBox();
            this.TextBox_ServerAddress = new System.Windows.Forms.TextBox();
            this.TextBox_ServerPort = new System.Windows.Forms.TextBox();
            this.Local_ServerUser = new System.Windows.Forms.Label();
            this.Local_ServerPassword = new System.Windows.Forms.Label();
            this.TextBox_ServerUser = new System.Windows.Forms.TextBox();
            this.TextBox_ServerPassword = new System.Windows.Forms.TextBox();
            this.ContextMenuStrip_SyncLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TabControl_Main = new System.Windows.Forms.TabControl();
            this.TabPage_Main_LocalSystem = new System.Windows.Forms.TabPage();
            this.TabPage_Main_Server = new System.Windows.Forms.TabPage();
            this.ListView_SyncLog = new System.Windows.Forms.ListView();
            this.ColumnHeader_SyncLog_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_SyncLog_Subject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_SyncLog_Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_SelectSyncDirectory = new System.Windows.Forms.Button();
            this.TextBox_SyncDirectory = new System.Windows.Forms.TextBox();
            this.Label_DirectoryToSync = new System.Windows.Forms.Label();
            this.TextBox_RemotePath = new System.Windows.Forms.TextBox();
            this.Label_RemotePath = new System.Windows.Forms.Label();
            this.Button_RefreshRemoteDirectory = new System.Windows.Forms.Button();
            this.ListView_RemoteFiles = new System.Windows.Forms.ListView();
            this.ColumnHeader_RemoteFiles_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_RemoteFiles_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox_Server.SuspendLayout();
            this.TabControl_Main.SuspendLayout();
            this.TabPage_Main_LocalSystem.SuspendLayout();
            this.TabPage_Main_Server.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon_Main
            // 
            this.NotifyIcon_Main.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon_Main.Icon")));
            this.NotifyIcon_Main.Text = "notifyIcon1";
            this.NotifyIcon_Main.Visible = true;
            // 
            // GroupBox_Server
            // 
            this.GroupBox_Server.Controls.Add(this.TextBox_ServerPassword);
            this.GroupBox_Server.Controls.Add(this.TextBox_ServerUser);
            this.GroupBox_Server.Controls.Add(this.Local_ServerPassword);
            this.GroupBox_Server.Controls.Add(this.Local_ServerUser);
            this.GroupBox_Server.Controls.Add(this.TextBox_ServerPort);
            this.GroupBox_Server.Controls.Add(this.TextBox_ServerAddress);
            this.GroupBox_Server.Controls.Add(this.ComboBox_Protocol);
            this.GroupBox_Server.Controls.Add(this.Local_ServerAddress);
            this.GroupBox_Server.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_Server.Name = "GroupBox_Server";
            this.GroupBox_Server.Size = new System.Drawing.Size(472, 80);
            this.GroupBox_Server.TabIndex = 0;
            this.GroupBox_Server.TabStop = false;
            this.GroupBox_Server.Text = " Server ";
            // 
            // Button_StartStop
            // 
            this.Button_StartStop.Location = new System.Drawing.Point(12, 301);
            this.Button_StartStop.Name = "Button_StartStop";
            this.Button_StartStop.Size = new System.Drawing.Size(472, 32);
            this.Button_StartStop.TabIndex = 14;
            this.Button_StartStop.Text = "Start";
            this.Button_StartStop.UseVisualStyleBackColor = true;
            this.Button_StartStop.Click += new System.EventHandler(this.Button_StartStop_Click);
            // 
            // Local_ServerAddress
            // 
            this.Local_ServerAddress.AutoSize = true;
            this.Local_ServerAddress.Location = new System.Drawing.Point(6, 25);
            this.Local_ServerAddress.Name = "Local_ServerAddress";
            this.Local_ServerAddress.Size = new System.Drawing.Size(48, 13);
            this.Local_ServerAddress.TabIndex = 1;
            this.Local_ServerAddress.Text = "Address:";
            // 
            // ComboBox_Protocol
            // 
            this.ComboBox_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Protocol.FormattingEnabled = true;
            this.ComboBox_Protocol.Items.AddRange(new object[] {
            "http",
            "https"});
            this.ComboBox_Protocol.Location = new System.Drawing.Point(60, 25);
            this.ComboBox_Protocol.Name = "ComboBox_Protocol";
            this.ComboBox_Protocol.Size = new System.Drawing.Size(89, 21);
            this.ComboBox_Protocol.TabIndex = 2;
            // 
            // TextBox_ServerAddress
            // 
            this.TextBox_ServerAddress.Location = new System.Drawing.Point(155, 25);
            this.TextBox_ServerAddress.Name = "TextBox_ServerAddress";
            this.TextBox_ServerAddress.Size = new System.Drawing.Size(236, 20);
            this.TextBox_ServerAddress.TabIndex = 3;
            // 
            // TextBox_ServerPort
            // 
            this.TextBox_ServerPort.Location = new System.Drawing.Point(397, 25);
            this.TextBox_ServerPort.Name = "TextBox_ServerPort";
            this.TextBox_ServerPort.Size = new System.Drawing.Size(69, 20);
            this.TextBox_ServerPort.TabIndex = 4;
            // 
            // Local_ServerUser
            // 
            this.Local_ServerUser.AutoSize = true;
            this.Local_ServerUser.Location = new System.Drawing.Point(10, 52);
            this.Local_ServerUser.Name = "Local_ServerUser";
            this.Local_ServerUser.Size = new System.Drawing.Size(32, 13);
            this.Local_ServerUser.TabIndex = 5;
            this.Local_ServerUser.Text = "User:";
            // 
            // Local_ServerPassword
            // 
            this.Local_ServerPassword.AutoSize = true;
            this.Local_ServerPassword.Location = new System.Drawing.Point(247, 52);
            this.Local_ServerPassword.Name = "Local_ServerPassword";
            this.Local_ServerPassword.Size = new System.Drawing.Size(56, 13);
            this.Local_ServerPassword.TabIndex = 7;
            this.Local_ServerPassword.Text = "Password:";
            // 
            // TextBox_ServerUser
            // 
            this.TextBox_ServerUser.Location = new System.Drawing.Point(60, 52);
            this.TextBox_ServerUser.Name = "TextBox_ServerUser";
            this.TextBox_ServerUser.Size = new System.Drawing.Size(169, 20);
            this.TextBox_ServerUser.TabIndex = 6;
            // 
            // TextBox_ServerPassword
            // 
            this.TextBox_ServerPassword.Location = new System.Drawing.Point(309, 52);
            this.TextBox_ServerPassword.Name = "TextBox_ServerPassword";
            this.TextBox_ServerPassword.PasswordChar = '*';
            this.TextBox_ServerPassword.Size = new System.Drawing.Size(157, 20);
            this.TextBox_ServerPassword.TabIndex = 8;
            this.TextBox_ServerPassword.UseSystemPasswordChar = true;
            // 
            // ContextMenuStrip_SyncLog
            // 
            this.ContextMenuStrip_SyncLog.Name = "ContextMenuStrip_SyncLog";
            this.ContextMenuStrip_SyncLog.Size = new System.Drawing.Size(61, 4);
            // 
            // TabControl_Main
            // 
            this.TabControl_Main.Controls.Add(this.TabPage_Main_LocalSystem);
            this.TabControl_Main.Controls.Add(this.TabPage_Main_Server);
            this.TabControl_Main.Location = new System.Drawing.Point(12, 98);
            this.TabControl_Main.Name = "TabControl_Main";
            this.TabControl_Main.SelectedIndex = 0;
            this.TabControl_Main.Size = new System.Drawing.Size(466, 197);
            this.TabControl_Main.TabIndex = 16;
            // 
            // TabPage_Main_LocalSystem
            // 
            this.TabPage_Main_LocalSystem.Controls.Add(this.ListView_SyncLog);
            this.TabPage_Main_LocalSystem.Controls.Add(this.Button_SelectSyncDirectory);
            this.TabPage_Main_LocalSystem.Controls.Add(this.TextBox_SyncDirectory);
            this.TabPage_Main_LocalSystem.Controls.Add(this.Label_DirectoryToSync);
            this.TabPage_Main_LocalSystem.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Main_LocalSystem.Name = "TabPage_Main_LocalSystem";
            this.TabPage_Main_LocalSystem.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Main_LocalSystem.Size = new System.Drawing.Size(458, 171);
            this.TabPage_Main_LocalSystem.TabIndex = 0;
            this.TabPage_Main_LocalSystem.Text = "Local system";
            this.TabPage_Main_LocalSystem.UseVisualStyleBackColor = true;
            // 
            // TabPage_Main_Server
            // 
            this.TabPage_Main_Server.Controls.Add(this.ListView_RemoteFiles);
            this.TabPage_Main_Server.Controls.Add(this.Button_RefreshRemoteDirectory);
            this.TabPage_Main_Server.Controls.Add(this.TextBox_RemotePath);
            this.TabPage_Main_Server.Controls.Add(this.Label_RemotePath);
            this.TabPage_Main_Server.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Main_Server.Name = "TabPage_Main_Server";
            this.TabPage_Main_Server.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Main_Server.Size = new System.Drawing.Size(458, 171);
            this.TabPage_Main_Server.TabIndex = 1;
            this.TabPage_Main_Server.Text = "Server";
            this.TabPage_Main_Server.UseVisualStyleBackColor = true;
            // 
            // ListView_SyncLog
            // 
            this.ListView_SyncLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_SyncLog_Time,
            this.ColumnHeader_SyncLog_Subject,
            this.ColumnHeader_SyncLog_Message});
            this.ListView_SyncLog.ContextMenuStrip = this.ContextMenuStrip_SyncLog;
            this.ListView_SyncLog.FullRowSelect = true;
            this.ListView_SyncLog.Location = new System.Drawing.Point(9, 34);
            this.ListView_SyncLog.Name = "ListView_SyncLog";
            this.ListView_SyncLog.Size = new System.Drawing.Size(443, 131);
            this.ListView_SyncLog.TabIndex = 17;
            this.ListView_SyncLog.UseCompatibleStateImageBehavior = false;
            this.ListView_SyncLog.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader_SyncLog_Time
            // 
            this.ColumnHeader_SyncLog_Time.Text = "Time";
            this.ColumnHeader_SyncLog_Time.Width = 96;
            // 
            // ColumnHeader_SyncLog_Subject
            // 
            this.ColumnHeader_SyncLog_Subject.Text = "Subject";
            this.ColumnHeader_SyncLog_Subject.Width = 120;
            // 
            // ColumnHeader_SyncLog_Message
            // 
            this.ColumnHeader_SyncLog_Message.Text = "Message";
            this.ColumnHeader_SyncLog_Message.Width = 230;
            // 
            // Button_SelectSyncDirectory
            // 
            this.Button_SelectSyncDirectory.Location = new System.Drawing.Point(410, 8);
            this.Button_SelectSyncDirectory.Name = "Button_SelectSyncDirectory";
            this.Button_SelectSyncDirectory.Size = new System.Drawing.Size(42, 20);
            this.Button_SelectSyncDirectory.TabIndex = 16;
            this.Button_SelectSyncDirectory.Text = "...";
            this.Button_SelectSyncDirectory.UseVisualStyleBackColor = true;
            // 
            // TextBox_SyncDirectory
            // 
            this.TextBox_SyncDirectory.Location = new System.Drawing.Point(97, 8);
            this.TextBox_SyncDirectory.Name = "TextBox_SyncDirectory";
            this.TextBox_SyncDirectory.Size = new System.Drawing.Size(307, 20);
            this.TextBox_SyncDirectory.TabIndex = 15;
            // 
            // Label_DirectoryToSync
            // 
            this.Label_DirectoryToSync.AutoSize = true;
            this.Label_DirectoryToSync.Location = new System.Drawing.Point(6, 8);
            this.Label_DirectoryToSync.Name = "Label_DirectoryToSync";
            this.Label_DirectoryToSync.Size = new System.Drawing.Size(89, 13);
            this.Label_DirectoryToSync.TabIndex = 14;
            this.Label_DirectoryToSync.Text = "Directory to sync:";
            // 
            // TextBox_RemotePath
            // 
            this.TextBox_RemotePath.Location = new System.Drawing.Point(83, 8);
            this.TextBox_RemotePath.Name = "TextBox_RemotePath";
            this.TextBox_RemotePath.Size = new System.Drawing.Size(323, 20);
            this.TextBox_RemotePath.TabIndex = 17;
            this.TextBox_RemotePath.Text = "/";
            // 
            // Label_RemotePath
            // 
            this.Label_RemotePath.AutoSize = true;
            this.Label_RemotePath.Location = new System.Drawing.Point(6, 8);
            this.Label_RemotePath.Name = "Label_RemotePath";
            this.Label_RemotePath.Size = new System.Drawing.Size(71, 13);
            this.Label_RemotePath.TabIndex = 16;
            this.Label_RemotePath.Text = "Remote path:";
            // 
            // Button_RefreshRemoteDirectory
            // 
            this.Button_RefreshRemoteDirectory.Location = new System.Drawing.Point(412, 8);
            this.Button_RefreshRemoteDirectory.Name = "Button_RefreshRemoteDirectory";
            this.Button_RefreshRemoteDirectory.Size = new System.Drawing.Size(42, 20);
            this.Button_RefreshRemoteDirectory.TabIndex = 18;
            this.Button_RefreshRemoteDirectory.Text = "...";
            this.Button_RefreshRemoteDirectory.UseVisualStyleBackColor = true;
            this.Button_RefreshRemoteDirectory.Click += new System.EventHandler(this.Button_RefreshRemoteDirectory_Click);
            // 
            // ListView_RemoteFiles
            // 
            this.ListView_RemoteFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_RemoteFiles_Name,
            this.ColumnHeader_RemoteFiles_Size});
            this.ListView_RemoteFiles.ContextMenuStrip = this.ContextMenuStrip_SyncLog;
            this.ListView_RemoteFiles.FullRowSelect = true;
            this.ListView_RemoteFiles.Location = new System.Drawing.Point(9, 34);
            this.ListView_RemoteFiles.Name = "ListView_RemoteFiles";
            this.ListView_RemoteFiles.Size = new System.Drawing.Size(443, 131);
            this.ListView_RemoteFiles.TabIndex = 19;
            this.ListView_RemoteFiles.UseCompatibleStateImageBehavior = false;
            this.ListView_RemoteFiles.View = System.Windows.Forms.View.Details;
            this.ListView_RemoteFiles.DoubleClick += new System.EventHandler(this.ListView_RemoteFiles_DoubleClick);
            // 
            // ColumnHeader_RemoteFiles_Name
            // 
            this.ColumnHeader_RemoteFiles_Name.Text = "Name";
            this.ColumnHeader_RemoteFiles_Name.Width = 310;
            // 
            // ColumnHeader_RemoteFiles_Size
            // 
            this.ColumnHeader_RemoteFiles_Size.Text = "Size";
            this.ColumnHeader_RemoteFiles_Size.Width = 120;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 345);
            this.Controls.Add(this.TabControl_Main);
            this.Controls.Add(this.Button_StartStop);
            this.Controls.Add(this.GroupBox_Server);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud.NET FileSync";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.GroupBox_Server.ResumeLayout(false);
            this.GroupBox_Server.PerformLayout();
            this.TabControl_Main.ResumeLayout(false);
            this.TabPage_Main_LocalSystem.ResumeLayout(false);
            this.TabPage_Main_LocalSystem.PerformLayout();
            this.TabPage_Main_Server.ResumeLayout(false);
            this.TabPage_Main_Server.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon_Main;
        private System.Windows.Forms.GroupBox GroupBox_Server;
        private System.Windows.Forms.Label Local_ServerAddress;
        private System.Windows.Forms.Button Button_StartStop;
        private System.Windows.Forms.TextBox TextBox_ServerPassword;
        private System.Windows.Forms.TextBox TextBox_ServerUser;
        private System.Windows.Forms.Label Local_ServerPassword;
        private System.Windows.Forms.Label Local_ServerUser;
        private System.Windows.Forms.TextBox TextBox_ServerPort;
        private System.Windows.Forms.TextBox TextBox_ServerAddress;
        private System.Windows.Forms.ComboBox ComboBox_Protocol;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_SyncLog;
        private System.Windows.Forms.TabControl TabControl_Main;
        private System.Windows.Forms.TabPage TabPage_Main_LocalSystem;
        private System.Windows.Forms.ListView ListView_SyncLog;
        private System.Windows.Forms.ColumnHeader ColumnHeader_SyncLog_Time;
        private System.Windows.Forms.ColumnHeader ColumnHeader_SyncLog_Subject;
        private System.Windows.Forms.ColumnHeader ColumnHeader_SyncLog_Message;
        private System.Windows.Forms.Button Button_SelectSyncDirectory;
        private System.Windows.Forms.TextBox TextBox_SyncDirectory;
        private System.Windows.Forms.Label Label_DirectoryToSync;
        private System.Windows.Forms.TabPage TabPage_Main_Server;
        private System.Windows.Forms.ListView ListView_RemoteFiles;
        private System.Windows.Forms.ColumnHeader ColumnHeader_RemoteFiles_Name;
        private System.Windows.Forms.ColumnHeader ColumnHeader_RemoteFiles_Size;
        private System.Windows.Forms.Button Button_RefreshRemoteDirectory;
        private System.Windows.Forms.TextBox TextBox_RemotePath;
        private System.Windows.Forms.Label Label_RemotePath;
    }
}

