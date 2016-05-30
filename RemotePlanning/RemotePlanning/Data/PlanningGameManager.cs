using System;
using System.Windows;
using RemotePlanning.Commands;
using RemotePlanning.Iteration;
using RemotePlanning.Main;
using RemotePlanning.PlanningSheets;
using RemotePlanning.Projects;
using RemotePlanning.Storycards;

namespace RemotePlanning.Data
{
    internal class PlanningGameManager
    {
        private readonly IMainWindow _mainWindow;
        private readonly CommandQueue _commandQueue;

        public PlanningGameManager(IMainWindow mainWindow, CommandQueue commandQueue, StubDataLoader stubDataLoader)
        {
            _mainWindow = mainWindow;
            _commandQueue = commandQueue;
            mainWindow.WindowLoaded += stubDataLoader.Window_OnLoaded;

            mainWindow.NetworkConnect += WindowOnNetworkConnect;
        }

        private void WindowOnNetworkConnect(object sender, NetworkConnectEventArgs eventArgs)
        {

        }
    }
}