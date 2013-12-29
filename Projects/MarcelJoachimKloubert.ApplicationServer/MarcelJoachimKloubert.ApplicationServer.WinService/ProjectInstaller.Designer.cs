namespace MarcelJoachimKloubert.ApplicationServer.WinService
{
    partial class ProjectInstaller
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
            this.ServiceProcessInstaller_Main = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceInstaller_Main = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServiceProcessInstaller_Main
            // 
            this.ServiceProcessInstaller_Main.Password = null;
            this.ServiceProcessInstaller_Main.Username = null;
            // 
            // ServiceInstaller_Main
            // 
            this.ServiceInstaller_Main.DelayedAutoStart = true;
            this.ServiceInstaller_Main.DisplayName = "Application Server by Marcel J. Kloubert";
            this.ServiceInstaller_Main.ServiceName = "MJKApplicationServer";
            this.ServiceInstaller_Main.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServiceProcessInstaller_Main,
            this.ServiceInstaller_Main});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServiceProcessInstaller_Main;
        private System.ServiceProcess.ServiceInstaller ServiceInstaller_Main;
    }
}