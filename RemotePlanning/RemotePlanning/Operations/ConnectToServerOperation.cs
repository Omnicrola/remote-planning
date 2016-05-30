using System;
using NetworkModel.Networking;
using RemotePlanning.Network;
using RemotePlanning.Operations;

namespace RemotePlanning.Data
{
    internal class ConnectToServerOperation : IDiscreetOperation
    {
        private readonly NetworkManager _networkManager;
        private readonly string _address;
        public string Description => "Connect to remote server";

        public ConnectToServerOperation(NetworkManager networkManager, string address)
        {
            _networkManager = networkManager;
            _address = address;
        }

        public event EventHandler<OperationEventArgs> OperationStatus;

        public void DoWork()
        {
            SendStatusMessage("Connecting to server...");
            try
            {
                _networkManager.Connect(_address);
                SendStatusMessage("Connected!");
            }
            catch (NetworkingException e)
            {
                SendStatusMessage("Error connecting : " + e.Message);
            }
        }

        private void SendStatusMessage(string message)
        {
            OperationStatus?.Invoke(this, new OperationEventArgs(message));
        }




    }
}