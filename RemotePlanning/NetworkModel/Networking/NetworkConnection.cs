using System;
using System.Threading;

namespace NetworkModel.Networking
{
    internal class NetworkConnection : IDisposable
    {
        public int ClientId { get; private set; }
        private readonly NetworkMessageReceiver _networkMessageReceiver;
        private readonly NetworkMessageWriter _networkMessageWriter;
        private Thread _receiverThread;
        private bool _isReceiving;

        public event EventHandler<NetworkMessageReceivedEventArgs> MessageRecieved;

        public NetworkConnection(int clientId, NetworkMessageReceiver networkMessageReceiver, NetworkMessageWriter networkMessageWriter)
        {
            ClientId = clientId;
            _networkMessageReceiver = networkMessageReceiver;
            _networkMessageWriter = networkMessageWriter;
            _networkMessageReceiver.MessageRecieved += ElevateMessageEvent;
            _isReceiving = true;
            _receiverThread = new Thread(GetMessages);
            _receiverThread.Start();
        }

        private void GetMessages()
        {
            while (_isReceiving)
            {
                _networkMessageReceiver.Recieve(); ;
            }

        }

        private void ElevateMessageEvent(object sender, NetworkMessageReceivedEventArgs eventArgs)
        {
            MessageRecieved?.Invoke(sender, eventArgs);
        }

        public void SendMessage(NetworkMessage message)
        {
            _networkMessageWriter.SendMessage(message);
        }


        public void Dispose()
        {
            _isReceiving = false;
            _networkMessageWriter.Dispose();
        }
    }
}