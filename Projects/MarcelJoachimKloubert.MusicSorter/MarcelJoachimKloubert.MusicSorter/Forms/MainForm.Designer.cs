namespace MarcelJoachimKloubert.MusicSorter.Forms
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
            this.VlcControl_Main = new Vlc.DotNet.Forms.VlcControl();
            this.TrackBar_CurrentSong = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_CurrentSong)).BeginInit();
            this.SuspendLayout();
            // 
            // VlcControl_Main
            // 
            this.VlcControl_Main.BackColor = System.Drawing.Color.Black;
            this.VlcControl_Main.Location = new System.Drawing.Point(0, 0);
            this.VlcControl_Main.Name = "VlcControl_Main";
            this.VlcControl_Main.Rate = 0F;
            this.VlcControl_Main.Size = new System.Drawing.Size(0, 0);
            this.VlcControl_Main.TabIndex = 2;
            // 
            // TrackBar_CurrentSong
            // 
            this.TrackBar_CurrentSong.Location = new System.Drawing.Point(61, 49);
            this.TrackBar_CurrentSong.Name = "TrackBar_CurrentSong";
            this.TrackBar_CurrentSong.Size = new System.Drawing.Size(104, 45);
            this.TrackBar_CurrentSong.TabIndex = 3;
            this.TrackBar_CurrentSong.ValueChanged += new System.EventHandler(this.TrackBar_CurrentSong_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 150);
            this.Controls.Add(this.TrackBar_CurrentSong);
            this.Controls.Add(this.VlcControl_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MusicSorter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_CurrentSong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Vlc.DotNet.Forms.VlcControl VlcControl_Main;
        private System.Windows.Forms.TrackBar TrackBar_CurrentSong;
    }
}

