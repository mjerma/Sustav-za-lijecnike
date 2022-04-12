using SustavZaLijecnike.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SustavZaLijecnike
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            LoginWindow loginWindow = new LoginWindow();

            if (loginWindow.ShowDialog() == true)
            {
                MainWindow mainWindow = new MainWindow();
                //Re-enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Current.MainWindow = mainWindow;
                mainWindow.SetAmbulanta(loginWindow.zaposlenik);
                mainWindow.Show();
            }
            else
            {
                Current.Shutdown(-1);
            }
        }
    }
}
