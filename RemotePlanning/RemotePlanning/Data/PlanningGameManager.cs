using System;
using System.Windows;
using RemotePlanning.Network;
using RemotePlanning.Operations;
using RemotePlanning.Ui.MainUi;
using RemotePlanning.Ui.ViewModels;

namespace RemotePlanning.Data
{
    internal class PlanningGameManager : IDisposable
    {
        private readonly IMainWindow _mainWindow;
        private readonly OperationsQueue _operationQueue;
        private readonly NetworkManager _networkManager;
        private readonly IDataPersister _dataPersister;
        private readonly ViewModelParser _viewModelParser;

        public PlanningGameManager(IMainWindow mainWindow,
            OperationsQueue operationQueue,
            NetworkManager networkManager,
            IDataPersister dataPersister,
            ViewModelParser viewModelParser)
        {
            _mainWindow = mainWindow;
            _operationQueue = operationQueue;
            _networkManager = networkManager;
            _dataPersister = dataPersister;
            _viewModelParser = viewModelParser;
            mainWindow.WindowLoaded += LoadData;
            mainWindow.WindowClosed += SaveData;

            mainWindow.NetworkConnect += WindowOnNetworkConnect;
            mainWindow.HostNetworkSession += WindowOnHostNetworkSession;
            _operationQueue.OperationStatus += Operation_OnStatusMessage;
        }

        private void SaveData(object sender, EventArgs e)
        {
            ApplicationDataStore dataStore = _viewModelParser.ExtractData();
            _dataPersister.WriteData(dataStore);
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            var applicationDataStore = _dataPersister.LoadData();
            _viewModelParser.ClearAndLoad(applicationDataStore);
        }

        private void Operation_OnStatusMessage(object sender, OperationEventArgs e)
        {
            _mainWindow.AddStatusMessage(e.Message);
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