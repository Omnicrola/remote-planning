using System;
using NetworkModel.Networking;
using RemotePlanning.Network;

namespace RemotePlanning.Operations.Async
{
    internal class StartNetworkHostingAsyncOperation : IDiscreetAsyncOperation
    {
        public string Description => "Start hosting a server";
        public bool IsAsync => true;

        private readonly NetworkManager _networkManager;
        public event EventHandler<OperationEventArgs> OperationStatus;

        public StartNetworkHostingAsyncOperation(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void DoWork()
        {
            try
            {
                OperationStatus?.Invoke(this, new OperationEventArgs("Session host starting"));
                _networkManager.StartHosting();
            }
            catch (NetworkingException e)
            {
                OperationStatus?.Invoke(this, new OperationEventArgs("Error! " + e.Message));
            }
        }

    }
}