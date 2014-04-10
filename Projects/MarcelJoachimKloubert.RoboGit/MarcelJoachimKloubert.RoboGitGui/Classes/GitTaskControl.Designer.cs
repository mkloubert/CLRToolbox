namespace MarcelJoachimKloubert.RoboGitGui.Classes
{
    partial class GitTaskControl
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
            this.GroupBox_Main = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.GroupBox_Actions = new System.Windows.Forms.GroupBox();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Button_Start = new System.Windows.Forms.Button();
            this.GroupBox_Logs = new System.Windows.Forms.GroupBox();
            this.GroupBox_Names = new System.Windows.Forms.GroupBox();
            this.TextBox_DisplayName = new System.Windows.Forms.TextBox();
            this.Label_DisplayName = new System.Windows.Forms.Label();
            this.TextBox_InternalName = new System.Windows.Forms.TextBox();
            this.Label_InternalName = new System.Windows.Forms.Label();
            this.ListView_Logs = new System.Windows.Forms.ListView();
            this.Button_ClearLogs = new System.Windows.Forms.Button();
            this.ImageList_Logs = new System.Windows.Forms.ImageList(this.components);
            this.GroupBox_Main.SuspendLayout();
            this.GroupBox_Actions.SuspendLayout();
            this.GroupBox_Logs.SuspendLayout();
            this.GroupBox_Names.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_Main
            // 
            this.GroupBox_Main.Controls.Add(this.progressBar1);
            this.GroupBox_Main.Controls.Add(this.GroupBox_Actions);
            this.GroupBox_Main.Controls.Add(this.GroupBox_Logs);
            this.GroupBox_Main.Controls.Add(this.GroupBox_Names);
            this.GroupBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_Main.Location = new System.Drawing.Point(0, 0);
            this.GroupBox_Main.Name = "GroupBox_Main";
            this.GroupBox_Main.Size = new System.Drawing.Size(512, 512);
            this.GroupBox_Main.TabIndex = 0;
            this.GroupBox_Main.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 483);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(500, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // GroupBox_Actions
            // 
            this.GroupBox_Actions.Controls.Add(this.Button_Stop);
            this.GroupBox_Actions.Controls.Add(this.Button_Start);
            this.GroupBox_Actions.Location = new System.Drawing.Point(6, 140);
            this.GroupBox_Actions.Name = "GroupBox_Actions";
            this.GroupBox_Actions.Size = new System.Drawing.Size(500, 109);
            this.GroupBox_Actions.TabIndex = 8;
            this.GroupBox_Actions.TabStop = false;
            this.GroupBox_Actions.Text = " Actions ";
            // 
            // Button_Stop
            // 
            this.Button_Stop.Enabled = false;
            this.Button_Stop.Location = new System.Drawing.Point(9, 66);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(485, 32);
            this.Button_Stop.TabIndex = 1;
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Button_Start
            // 
            this.Button_Start.Location = new System.Drawing.Point(9, 28);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(485, 32);
            this.Button_Start.TabIndex = 0;
            this.Button_Start.Text = "Start";
            this.Button_Start.UseVisualStyleBackColor = true;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // GroupBox_Logs
            // 
            this.GroupBox_Logs.Controls.Add(this.Button_ClearLogs);
            this.GroupBox_Logs.Controls.Add(this.ListView_Logs);
            this.GroupBox_Logs.Location = new System.Drawing.Point(6, 255);
            this.GroupBox_Logs.Name = "GroupBox_Logs";
            this.GroupBox_Logs.Size = new System.Drawing.Size(500, 222);
            this.GroupBox_Logs.TabIndex = 6;
            this.GroupBox_Logs.TabStop = false;
            this.GroupBox_Logs.Text = " Logs ";
            // 
            // GroupBox_Names
            // 
            this.GroupBox_Names.Controls.Add(this.TextBox_DisplayName);
            this.GroupBox_Names.Controls.Add(this.Label_DisplayName);
            this.GroupBox_Names.Controls.Add(this.TextBox_InternalName);
            this.GroupBox_Names.Controls.Add(this.Label_InternalName);
            this.GroupBox_Names.Location = new System.Drawing.Point(6, 19);
            this.GroupBox_Names.Name = "GroupBox_Names";
            this.GroupBox_Names.Size = new System.Drawing.Size(500, 115);
            this.GroupBox_Names.TabIndex = 5;
            this.GroupBox_Names.TabStop = false;
            this.GroupBox_Names.Text = " Names ";
            // 
            // TextBox_DisplayName
            // 
            this.TextBox_DisplayName.Location = new System.Drawing.Point(9, 80);
            this.TextBox_DisplayName.Name = "TextBox_DisplayName";
            this.TextBox_DisplayName.ReadOnly = true;
            this.TextBox_DisplayName.Size = new System.Drawing.Size(485, 20);
            this.TextBox_DisplayName.TabIndex = 6;
            // 
            // Label_DisplayName
            // 
            this.Label_DisplayName.AutoSize = true;
            this.Label_DisplayName.Location = new System.Drawing.Point(6, 64);
            this.Label_DisplayName.Name = "Label_DisplayName";
            this.Label_DisplayName.Size = new System.Drawing.Size(73, 13);
            this.Label_DisplayName.TabIndex = 5;
            this.Label_DisplayName.Text = "Display name:";
            this.Label_DisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_InternalName
            // 
            this.TextBox_InternalName.Location = new System.Drawing.Point(9, 41);
            this.TextBox_InternalName.Name = "TextBox_InternalName";
            this.TextBox_InternalName.ReadOnly = true;
            this.TextBox_InternalName.Size = new System.Drawing.Size(485, 20);
            this.TextBox_InternalName.TabIndex = 4;
            // 
            // Label_InternalName
            // 
            this.Label_InternalName.AutoSize = true;
            this.Label_InternalName.Location = new System.Drawing.Point(6, 25);
            this.Label_InternalName.Name = "Label_InternalName";
            this.Label_InternalName.Size = new System.Drawing.Size(74, 13);
            this.Label_InternalName.TabIndex = 3;
            this.Label_InternalName.Text = "Internal name:";
            this.Label_InternalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListView_Logs
            // 
            this.ListView_Logs.FullRowSelect = true;
            this.ListView_Logs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Logs.Location = new System.Drawing.Point(9, 19);
            this.ListView_Logs.Name = "ListView_Logs";
            this.ListView_Logs.Size = new System.Drawing.Size(485, 159);
            this.ListView_Logs.TabIndex = 0;
            this.ListView_Logs.UseCompatibleStateImageBehavior = false;
            this.ListView_Logs.View = System.Windows.Forms.View.Details;
            // 
            // Button_ClearLogs
            // 
            this.Button_ClearLogs.Location = new System.Drawing.Point(9, 184);
            this.Button_ClearLogs.Name = "Button_ClearLogs";
            this.Button_ClearLogs.Size = new System.Drawing.Size(485, 32);
            this.Button_ClearLogs.TabIndex = 1;
            this.Button_ClearLogs.Text = "Start";
            this.Button_ClearLogs.UseVisualStyleBackColor = true;
            // 
            // ImageList_Logs
            // 
            this.ImageList_Logs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList_Logs.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList_Logs.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // GitTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBox_Main);
            this.Name = "GitTaskControl";
            this.Size = new System.Drawing.Size(512, 512);
            this.GroupBox_Main.ResumeLayout(false);
            this.GroupBox_Actions.ResumeLayout(false);
            this.GroupBox_Logs.ResumeLayout(false);
            this.GroupBox_Names.ResumeLayout(false);
            this.GroupBox_Names.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_Main;
        private System.Windows.Forms.GroupBox GroupBox_Logs;
        private System.Windows.Forms.GroupBox GroupBox_Names;
        private System.Windows.Forms.TextBox TextBox_DisplayName;
        private System.Windows.Forms.Label Label_DisplayName;
        private System.Windows.Forms.TextBox TextBox_InternalName;
        private System.Windows.Forms.Label Label_InternalName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox GroupBox_Actions;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.Button Button_ClearLogs;
        private System.Windows.Forms.ListView ListView_Logs;
        private System.Windows.Forms.ImageList ImageList_Logs;
    }
}
