using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetworkModel.Networking
{
    public class NetworkMessageReceiver : IDisposable
    {
        private readonly int _clientid;
        private Socket _workSocket;
        private readonly int _bufferSize;
        private readonly StringBuilder _stringBuilder;

        private readonly AutoResetEvent RECEIVE_ACCEPT_EVENT = new AutoResetEvent(false);

        public event EventHandler<NetworkMessageReceivedEventArgs> MessageRecieved;

        public NetworkMessageReceiver(int clientid, Socket workSocket, int bufferSize)
        {
            _clientid = clientid;
            _workSocket = workSocket;
            _bufferSize = bufferSize;
            _stringBuilder = new StringBuilder();
            Console.WriteLine("new Reciever");
        }


        public void Recieve()
        {
            if (_workSocket != null)
            {
                NetworkPacket networkPacket = new NetworkPacket(_bufferSize);
                ContinueReceiving(networkPacket);
                RECEIVE_ACCEPT_EVENT.WaitOne();
            }
        }

        private void ContinueReceiving(NetworkPacket networkPacket)
        {
            _workSocket.BeginReceive(networkPacket.Buffer, 0, networkPacket.Buffer.Length, 0, ReadCallback, networkPacket);
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            try
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
                        RECEIVE_ACCEPT_EVENT.Set();
                    }
                    else
                    {
                        ContinueReceiving(networkPacket);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ParseMessages()
        {
            var data = _stringBuilder.ToString();
            string[] messages = data.Split(new string[] { NetworkConstants.MESSAGE_TERMINATOR }, StringSplitOptions.RemoveEmptyEntries);
            _stringBuilder.Clear();

            string singleMessage = String.Empty;
            for (int i = 0; i < messages.Length - 1; i++)
            {
                singleMessage = messages[i];
                try
                {
                    NetworkMessage networkMessage = NetworkMessage.Deserialize(singleMessage);
                    Console.WriteLine("RECEIVE: " + networkMessage.Message);
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
            _workSocket = null;
        }
    }
}