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
            this.Panel_Path = new System.Windows.Forms.Panel();
            this.ListView_Directory = new System.Windows.Forms.ListView();
            this.Text_Path = new System.Windows.Forms.TextBox();
            this.ColumnHeader_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Panel_Path.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Path
            // 
            this.Panel_Path.Controls.Add(this.Text_Path);
            this.Panel_Path.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Path.Location = new System.Drawing.Point(0, 0);
            this.Panel_Path.Name = "Panel_Path";
            this.Panel_Path.Size = new System.Drawing.Size(532, 26);
            this.Panel_Path.TabIndex = 0;
            // 
            // ListView_Directory
            // 
            this.ListView_Directory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_Name,
            this.ColumnHeader_Size,
            this.ColumnHeader_Date,
            this.ColumnHeader_Time});
            this.ListView_Directory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Directory.Location = new System.Drawing.Point(0, 26);
            this.ListView_Directory.Name = "ListView_Directory";
            this.ListView_Directory.Size = new System.Drawing.Size(532, 616);
            this.ListView_Directory.TabIndex = 1;
            this.ListView_Directory.UseCompatibleStateImageBehavior = false;
            this.ListView_Directory.View = System.Windows.Forms.View.Details;
            // 
            // Text_Path
            // 
            this.Text_Path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text_Path.Location = new System.Drawing.Point(0, 0);
            this.Text_Path.Name = "Text_Path";
            this.Text_Path.Size = new System.Drawing.Size(532, 20);
            this.Text_Path.TabIndex = 0;
            this.Text_Path.Text = "*.*";
            // 
            // ColumnHeader_Name
            // 
            this.ColumnHeader_Name.Text = "Filename";
            this.ColumnHeader_Name.Width = 200;
            // 
            // ColumnHeader_Size
            // 
            this.ColumnHeader_Size.Text = "Size";
            this.ColumnHeader_Size.Width = 64;
            // 
            // ColumnHeader_Date
            // 
            this.ColumnHeader_Date.Text = "Date";
            this.ColumnHeader_Date.Width = 64;
            // 
            // ColumnHeader_Time
            // 
            this.ColumnHeader_Time.Text = "Time";
            this.ColumnHeader_Time.Width = 64;
            // 
            // FileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListView_Directory);
            this.Controls.Add(this.Panel_Path);
            this.Name = "FileBrowser";
            this.Size = new System.Drawing.Size(532, 642);
            this.Panel_Path.ResumeLayout(false);
            this.Panel_Path.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Path;
        private System.Windows.Forms.ListView ListView_Directory;
        private System.Windows.Forms.TextBox Text_Path;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Name;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Size;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Date;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Time;
    }
}
