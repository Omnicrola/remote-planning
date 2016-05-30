using System;
using System.Threading;
using System.Windows;
using NetworkModel.Networking;
using RemotePlanning.Commands;
using RemotePlanning.Data;
using RemotePlanning.Main;
using RemotePlanning.Network;

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
            var commandQueue = new OperationsQueue(mainWindow.Dispatcher);
            var networkManager = new NetworkManager();
            var planningGameManager = new PlanningGameManager(mainWindow, commandQueue, networkManager, new StubDataLoader(mainWindow));
            mainWindow.Closed += (s, e) => { planningGameManager.Dispose(); };
            mainWindow.Show();
        }
    }
}
