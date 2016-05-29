using System;
using System.Net.Sockets;
using System.Text;

namespace NetworkModel.Networking
{
    public class NetworkMessageBuilder
    {
        private readonly Socket _workSocket;
        private readonly AsyncCallback _messageRecieveCallback;
        private StringBuilder _stringBuilder;
        private byte[] _dataBuffer;

        public NetworkMessageBuilder(Socket workSocket, int bufferSize, AsyncCallback messageRecieveCallback)
        {
            _workSocket = workSocket;
            _messageRecieveCallback = messageRecieveCallback;
            _dataBuffer = new byte[bufferSize];
            _stringBuilder = new StringBuilder();
        }


        public void Recieve()
        {
            _workSocket.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, 0, ReadCallback, this);

        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            int bytesRead = _workSocket.EndReceive(asyncResult);
            if (bytesRead > 0)
            {
                StoreData(bytesRead);

                if (MessageTerminatorRecieved())
                {
                    string messageContent = _stringBuilder.ToString();
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        messageContent.Length, messageContent);

                    _messageRecieveCallback.Invoke(asyncResult);
                }
                else
                {
                    Recieve();
                }
            }
        }

        private bool MessageTerminatorRecieved()
        {
            string messageContent = _stringBuilder.ToString();
            return messageContent.IndexOf(NetworkConstants.MESSAGE_TERMINATOR) > -1;
        }

        private void StoreData(int bytesRead)
        {
            _stringBuilder.Append(Encoding.ASCII.GetString(
                _dataBuffer, 0, bytesRead));
        }

        public void Close(IAsyncResult asyncResult)
        {
        }
    }
}