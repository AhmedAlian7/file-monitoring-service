using System;
using System.ComponentModel;
using System.ServiceProcess;

namespace FolderMonitorService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            // Process installer configures the service's account
            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            // Service installer configures the service details
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FolderMonitorService",          // Must match the service class name
                DisplayName = "Folder Monitor Service",
                Description = "Monitors a folder for new files, renames them, and moves to a destination folder.",
                StartType = ServiceStartMode.Automatic
            };

            // Add both installers to the Installers collection
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
