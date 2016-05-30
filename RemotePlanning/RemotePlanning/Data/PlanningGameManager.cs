using System;
using System.Windows;
using RemotePlanning.Commands;
using RemotePlanning.Iteration;
using RemotePlanning.Main;
using RemotePlanning.Network;
using RemotePlanning.Operations;
using RemotePlanning.PlanningSheets;
using RemotePlanning.Projects;
using RemotePlanning.Storycards;

namespace RemotePlanning.Data
{
    internal class PlanningGameManager : IDisposable
    {
        private readonly IMainWindow _mainWindow;
        private readonly OperationsQueue _operationQueue;
        private readonly NetworkManager _networkManager;

        public PlanningGameManager(IMainWindow mainWindow,
            OperationsQueue operationQueue,
            NetworkManager networkManager,
            StubDataLoader stubDataLoader)
        {
            _mainWindow = mainWindow;
            _operationQueue = operationQueue;
            _networkManager = networkManager;
            mainWindow.WindowLoaded += stubDataLoader.Window_OnLoaded;

            mainWindow.NetworkConnect += WindowOnNetworkConnect;
            mainWindow.HostNetworkSession += WindowOnHostNetworkSession;
        }

        private void WindowOnHostNetworkSession(object sender, NetworkHostEventArgs e)
        {
            var startNetworkHostingOperation = new StartNetworkHostingOperation(_networkManager);
            _operationQueue.AddOperation(startNetworkHostingOperation);
        }

        private void WindowOnNetworkConnect(object sender, NetworkConnectEventArgs eventArgs)
        {
            var connectToServerOperation = new ConnectToServerOperation(_networkManager, eventArgs.Address);
            _operationQueue.AddOperation(connectToServerOperation);
        }

        public void Dispose()
        {
            _operationQueue.Dispose();
            _networkManager.Dispose();
        }
    }
}