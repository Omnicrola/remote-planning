using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetworkModel.Networking
{
    public class NetworkMessageReceiver : IDisposable
    {
        private readonly int _clientid;
        private readonly Socket _workSocket;
        private readonly int _bufferSize;
        private readonly StringBuilder _stringBuilder;

        public ManualResetEvent RECEIVE_ACCEPT_EVENT = new ManualResetEvent(false);

        public event EventHandler<NetworkMessageReceivedEventArgs> MessageRecieved;

        public NetworkMessageReceiver(int clientid, Socket workSocket, int bufferSize)
        {
            _clientid = clientid;
            _workSocket = workSocket;
            _bufferSize = bufferSize;
            _stringBuilder = new StringBuilder();
        }


        public void Recieve()
        {
            NetworkPacket networkPacket = new NetworkPacket(_bufferSize);
            ContinueReceiving(networkPacket);
            RECEIVE_ACCEPT_EVENT.WaitOne();

        }

        private void ContinueReceiving(NetworkPacket networkPacket)
        {
            _workSocket.BeginReceive(networkPacket.Buffer, 0, networkPacket.Buffer.Length, 0, ReadCallback, networkPacket);
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            int bytesRead = _workSocket.EndReceive(asyncResult);
            NetworkPacket networkPacket = (NetworkPacket)asyncResult.AsyncState;
            if (bytesRead > 0)
            {
                StoreData(bytesRead, networkPacket);

                if (MessageTerminatorRecieved())
                {
                    Console.WriteLine("Read {0} bytes from socket. ",
                        _stringBuilder.ToString().Length);
                    ParseMessages();
                }
                else
                {
                    ContinueReceiving(networkPacket);
                }
            }
            RECEIVE_ACCEPT_EVENT.Set();
        }

        private void ParseMessages()
        {
            string[] messages = _stringBuilder.ToString().Split(new string[] { NetworkConstants.MESSAGE_TERMINATOR }, StringSplitOptions.RemoveEmptyEntries);
            _stringBuilder.Clear();

            string singleMessage = String.Empty;
            for (int i = 0; i < messages.Length - 1; i++)
            {
                singleMessage = messages[i];
                try
                {
                    NetworkMessage networkMessage = NetworkMessage.Deserialize(singleMessage);
                    MessageRecieved?.Invoke(this, new NetworkMessageReceivedEventArgs(_clientid, networkMessage));
                }
                catch (NetworkSerializationException e)
                {
                    _stringBuilder.Append(singleMessage);
                    _stringBuilder.Append(NetworkConstants.MESSAGE_TERMINATOR);
                }
            }
        }

        private bool MessageTerminatorRecieved()
        {
            string messageContent = _stringBuilder.ToString();
            return messageContent.IndexOf(NetworkConstants.MESSAGE_TERMINATOR) > -1;
        }

        private void StoreData(int bytesRead, NetworkPacket networkPacket)
        {
            _stringBuilder.Append(Encoding.ASCII.GetString(
                networkPacket.Buffer, 0, bytesRead));
        }

        public void Dispose()
        {
            RECEIVE_ACCEPT_EVENT.Set();
            _workSocket.Dispose();
        }
    }

    public class NetworkPacket
    {

        public NetworkPacket(int bufferSize)
        {
            Buffer = new byte[bufferSize];
        }

        public byte[] Buffer { get; set; }
    }
}