namespace MarcelJoachimKloubert.GitThemAll.Forms
{
    partial class CredentialForm
    {
        #region Fields (8)

        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.CheckBox Checkbox_SaveCredentials;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label Label_Password;
        private System.Windows.Forms.Label Label_Username;
        private System.Windows.Forms.TextBox TextBox_Password;
        private System.Windows.Forms.TextBox TextBox_Username;

        #endregion Fields

        #region Methods (1)

        // Protected Methods (1) 

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

        #endregion Methods



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_Username = new System.Windows.Forms.Label();
            this.TextBox_Username = new System.Windows.Forms.TextBox();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Label_Password = new System.Windows.Forms.Label();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Checkbox_SaveCredentials = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Label_Username
            // 
            this.Label_Username.AutoSize = true;
            this.Label_Username.Location = new System.Drawing.Point(12, 9);
            this.Label_Username.Name = "Label_Username";
            this.Label_Username.Size = new System.Drawing.Size(58, 13);
            this.Label_Username.TabIndex = 0;
            this.Label_Username.Text = "Username:";
            // 
            // TextBox_Username
            // 
            this.TextBox_Username.Location = new System.Drawing.Point(12, 25);
            this.TextBox_Username.Name = "TextBox_Username";
            this.TextBox_Username.Size = new System.Drawing.Size(360, 20);
            this.TextBox_Username.TabIndex = 1;
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Location = new System.Drawing.Point(12, 64);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.PasswordChar = '*';
            this.TextBox_Password.Size = new System.Drawing.Size(360, 20);
            this.TextBox_Password.TabIndex = 3;
            this.TextBox_Password.UseSystemPasswordChar = true;
            // 
            // Label_Password
            // 
            this.Label_Password.AutoSize = true;
            this.Label_Password.Location = new System.Drawing.Point(12, 48);
            this.Label_Password.Name = "Label_Password";
            this.Label_Password.Size = new System.Drawing.Size(56, 13);
            this.Label_Password.TabIndex = 2;
            this.Label_Password.Text = "Password:";
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(297, 117);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 32);
            this.Button_Cancel.TabIndex = 6;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(216, 117);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 32);
            this.Button_OK.TabIndex = 5;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Checkbox_SaveCredentials
            // 
            this.Checkbox_SaveCredentials.AutoSize = true;
            this.Checkbox_SaveCredentials.Location = new System.Drawing.Point(12, 90);
            this.Checkbox_SaveCredentials.Name = "Checkbox_SaveCredentials";
            this.Checkbox_SaveCredentials.Size = new System.Drawing.Size(105, 17);
            this.Checkbox_SaveCredentials.TabIndex = 4;
            this.Checkbox_SaveCredentials.Text = "Save credentials";
            this.Checkbox_SaveCredentials.UseVisualStyleBackColor = true;
            // 
            // CredentialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.Checkbox_SaveCredentials);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.Label_Password);
            this.Controls.Add(this.TextBox_Username);
            this.Controls.Add(this.Label_Username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CredentialForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Credentials";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}