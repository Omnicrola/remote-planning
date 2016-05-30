using System;
using System.Windows.Threading;
using NetworkModel.Networking;
using RemotePlanning.Network;

namespace RemotePlanning.Operations
{
    internal class StartNetworkHostingOperation : IDiscreetOperation
    {
        public string Description => "Start hosting a server";

        private readonly NetworkManager _networkManager;
        public event EventHandler<OperationEventArgs> OperationStatus;

        public StartNetworkHostingOperation(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void DoWork()
        {
            try
            {
                _networkManager.StartHosting();
            }
            catch (NetworkingException e)
            {

            }
        }

    }
}