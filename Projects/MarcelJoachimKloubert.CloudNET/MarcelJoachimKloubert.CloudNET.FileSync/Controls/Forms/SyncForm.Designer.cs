namespace MarcelJoachimKloubert.CloudNET.FileSync.Controls.Forms
{
    partial class SyncForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ProgressBar_CurrentDirectory = new System.Windows.Forms.ProgressBar();
            this.Label_CurrentDirectory = new System.Windows.Forms.Label();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_RemainingTime = new System.Windows.Forms.Label();
            this.ToolTip_Label_Directories = new System.Windows.Forms.ToolTip(this.components);
            this.ProgressBar_Directories = new System.Windows.Forms.ProgressBar();
            this.Label_Directories = new System.Windows.Forms.Label();
            this.ToolTip_Label_CurrentDirectory = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ProgressBar_CurrentDirectory
            // 
            this.ProgressBar_CurrentDirectory.Location = new System.Drawing.Point(12, 67);
            this.ProgressBar_CurrentDirectory.Name = "ProgressBar_CurrentDirectory";
            this.ProgressBar_CurrentDirectory.Size = new System.Drawing.Size(382, 23);
            this.ProgressBar_CurrentDirectory.TabIndex = 3;
            // 
            // Label_CurrentDirectory
            // 
            this.Label_CurrentDirectory.Location = new System.Drawing.Point(12, 51);
            this.Label_CurrentDirectory.Name = "Label_CurrentDirectory";
            this.Label_CurrentDirectory.Size = new System.Drawing.Size(382, 13);
            this.Label_CurrentDirectory.TabIndex = 2;
            this.Label_CurrentDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(319, 150);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 32);
            this.Button_Cancel.TabIndex = 4;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Label_RemainingTime
            // 
            this.Label_RemainingTime.Location = new System.Drawing.Point(12, 9);
            this.Label_RemainingTime.Name = "Label_RemainingTime";
            this.Label_RemainingTime.Size = new System.Drawing.Size(382, 13);
            this.Label_RemainingTime.TabIndex = 5;
            this.Label_RemainingTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBar_Directories
            // 
            this.ProgressBar_Directories.Location = new System.Drawing.Point(12, 109);
            this.ProgressBar_Directories.Name = "ProgressBar_Directories";
            this.ProgressBar_Directories.Size = new System.Drawing.Size(382, 23);
            this.ProgressBar_Directories.TabIndex = 7;
            // 
            // Label_Directories
            // 
            this.Label_Directories.Location = new System.Drawing.Point(12, 93);
            this.Label_Directories.Name = "Label_Directories";
            this.Label_Directories.Size = new System.Drawing.Size(382, 13);
            this.Label_Directories.TabIndex = 6;
            this.Label_Directories.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 193);
            this.ControlBox = false;
            this.Controls.Add(this.ProgressBar_Directories);
            this.Controls.Add(this.Label_Directories);
            this.Controls.Add(this.Label_RemainingTime);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.ProgressBar_CurrentDirectory);
            this.Controls.Add(this.Label_CurrentDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SyncForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Synchronize";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar_CurrentDirectory;
        private System.Windows.Forms.Label Label_CurrentDirectory;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_RemainingTime;
        private System.Windows.Forms.ToolTip ToolTip_Label_Directories;
        private System.Windows.Forms.ProgressBar ProgressBar_Directories;
        private System.Windows.Forms.Label Label_Directories;
        private System.Windows.Forms.ToolTip ToolTip_Label_CurrentDirectory;
    }
}