using System;
using NetworkModel.Networking;

namespace RemotePlanning.Network
{
    internal class NetworkManager : IDisposable
    {
        private NetworkClientConnection _networkClientConnection;
        private NetworkServerConnection _networkServerConnection;

        public NetworkManager()
        {

        }

        public void Connect(string address)
        {
            CloseServerConnection();
            _networkClientConnection = new NetworkClientConnection(address);
            _networkClientConnection.Connect();
        }

        private void CloseServerConnection()
        {
            _networkServerConnection?.StopServer();
            _networkServerConnection = null;
        }

        public void StartHosting()
        {
            CloseClientConnection();
            _networkServerConnection = new NetworkServerConnection("127.0.0.1");
            _networkServerConnection.Start();
        }

        private void CloseClientConnection()
        {
            _networkClientConnection?.Close();
            _networkClientConnection = null;
        }

        public void Dispose()
        {
            CloseClientConnection();
            CloseServerConnection();
        }
    }
}