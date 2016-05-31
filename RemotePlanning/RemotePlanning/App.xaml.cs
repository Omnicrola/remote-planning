using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using NetworkModel.Networking;
using RemotePlanning.Data;
using RemotePlanning.Network;
using RemotePlanning.Operations;
using RemotePlanning.Ui.IterationUi;
using RemotePlanning.Ui.MainUi;
using RemotePlanning.Ui.ProjectsUi;
using RemotePlanning.Ui.StorycardsUi;
using RemotePlanning.Ui.ViewModels;

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
            var planningGameManager = BuildPlanningGameManager(mainWindow);
            mainWindow.Closed += (s, e) => { planningGameManager.Dispose(); };
            mainWindow.Show();
        }

        private static PlanningGameManager BuildPlanningGameManager(MainWindow mainWindow)
        {
            var commandQueue = new OperationsQueue(mainWindow.Dispatcher);
            var networkManager = new NetworkManager();
            var stubDataLoader = new XmlDataPersister(XmlSerializerFactoy.Create());
            var viewModelParser = new ViewModelParser(mainWindow);
            var planningGameManager = new PlanningGameManager(mainWindow, commandQueue, networkManager, stubDataLoader,
                viewModelParser);
            return planningGameManager;
        }

    }
}
