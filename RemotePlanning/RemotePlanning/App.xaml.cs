using System;
using System.Threading;
using System.Windows;
using NetworkModel.Networking;
using RemotePlanning.Commands;
using RemotePlanning.Data;
using RemotePlanning.Main;

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
            var commandQueue = new CommandQueue();
            var planningGameManager = new PlanningGameManager(mainWindow, commandQueue, new StubDataLoader(mainWindow));
            mainWindow.Show();
        }
    }
}
