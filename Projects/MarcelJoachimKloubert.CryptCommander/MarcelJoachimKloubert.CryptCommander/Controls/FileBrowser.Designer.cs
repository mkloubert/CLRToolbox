namespace MarcelJoachimKloubert.CryptCommander.Controls
{
    partial class FileBrowser
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Panel_Path = new System.Windows.Forms.Panel();
            this.TextBox_CurrentPath = new System.Windows.Forms.TextBox();
            this.ComboBox_Drives = new System.Windows.Forms.ComboBox();
            this.ListView_CurrentDirectory = new System.Windows.Forms.ListView();
            this.ColumnHeader_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList_ListView_CurrentDirectory = new System.Windows.Forms.ImageList(this.components);
            this.Panel_Path.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Path
            // 
            this.Panel_Path.Controls.Add(this.TextBox_CurrentPath);
            this.Panel_Path.Controls.Add(this.ComboBox_Drives);
            this.Panel_Path.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Path.Location = new System.Drawing.Point(0, 0);
            this.Panel_Path.Name = "Panel_Path";
            this.Panel_Path.Size = new System.Drawing.Size(532, 26);
            this.Panel_Path.TabIndex = 0;
            // 
            // TextBox_CurrentPath
            // 
            this.TextBox_CurrentPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_CurrentPath.Location = new System.Drawing.Point(64, 0);
            this.TextBox_CurrentPath.Name = "TextBox_CurrentPath";
            this.TextBox_CurrentPath.Size = new System.Drawing.Size(468, 20);
            this.TextBox_CurrentPath.TabIndex = 1;
            // 
            // ComboBox_Drives
            // 
            this.ComboBox_Drives.Dock = System.Windows.Forms.DockStyle.Left;
            this.ComboBox_Drives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Drives.FormattingEnabled = true;
            this.ComboBox_Drives.Location = new System.Drawing.Point(0, 0);
            this.ComboBox_Drives.Name = "ComboBox_Drives";
            this.ComboBox_Drives.Size = new System.Drawing.Size(64, 21);
            this.ComboBox_Drives.TabIndex = 0;
            this.ComboBox_Drives.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Drives_SelectedIndexChanged);
            // 
            // ListView_CurrentDirectory
            // 
            this.ListView_CurrentDirectory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_Name,
            this.ColumnHeader_Size,
            this.ColumnHeader_Date,
            this.ColumnHeader_Time});
            this.ListView_CurrentDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_CurrentDirectory.FullRowSelect = true;
            this.ListView_CurrentDirectory.LargeImageList = this.ImageList_ListView_CurrentDirectory;
            this.ListView_CurrentDirectory.Location = new System.Drawing.Point(0, 26);
            this.ListView_CurrentDirectory.Name = "ListView_CurrentDirectory";
            this.ListView_CurrentDirectory.Size = new System.Drawing.Size(532, 616);
            this.ListView_CurrentDirectory.SmallImageList = this.ImageList_ListView_CurrentDirectory;
            this.ListView_CurrentDirectory.TabIndex = 1;
            this.ListView_CurrentDirectory.UseCompatibleStateImageBehavior = false;
            this.ListView_CurrentDirectory.View = System.Windows.Forms.View.Details;
            this.ListView_CurrentDirectory.DoubleClick += new System.EventHandler(this.ListView_CurrentDirectory_DoubleClick);
            // 
            // ColumnHeader_Name
            // 
            this.ColumnHeader_Name.Text = "Filename";
            this.ColumnHeader_Name.Width = 200;
            // 
            // ColumnHeader_Size
            // 
            this.ColumnHeader_Size.Text = "Size";
            this.ColumnHeader_Size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ColumnHeader_Size.Width = 72;
            // 
            // ColumnHeader_Date
            // 
            this.ColumnHeader_Date.Text = "Date";
            this.ColumnHeader_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader_Date.Width = 72;
            // 
            // ColumnHeader_Time
            // 
            this.ColumnHeader_Time.Text = "Time";
            this.ColumnHeader_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader_Time.Width = 72;
            // 
            // ImageList_ListView_CurrentDirectory
            // 
            this.ImageList_ListView_CurrentDirectory.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ImageList_ListView_CurrentDirectory.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList_ListView_CurrentDirectory.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListView_CurrentDirectory);
            this.Controls.Add(this.Panel_Path);
            this.Name = "FileBrowser";
            this.Size = new System.Drawing.Size(532, 642);
            this.Load += new System.EventHandler(this.FileBrowser_Load);
            this.Enter += new System.EventHandler(this.FileBrowser_Enter);
            this.Panel_Path.ResumeLayout(false);
            this.Panel_Path.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Path;
        private System.Windows.Forms.ListView ListView_CurrentDirectory;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Name;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Size;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Date;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Time;
        private System.Windows.Forms.TextBox TextBox_CurrentPath;
        private System.Windows.Forms.ComboBox ComboBox_Drives;
        private System.Windows.Forms.ImageList ImageList_ListView_CurrentDirectory;
    }
}
