using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RemotePlanning.Data;

namespace RemotePlanning
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Start(object sender, StartupEventArgs args)
        {
            var mainWindow = new MainWindow();
            var planningGameManager = new PlanningGameManager(mainWindow);
            mainWindow.Show();
        }
    }
}
