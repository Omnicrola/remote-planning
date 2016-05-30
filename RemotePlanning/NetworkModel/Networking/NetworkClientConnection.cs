using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkModel.Networking
{
    public class NetworkClientConnection
    {

        private static readonly ManualResetEvent connectionAsyncLock = new ManualResetEvent(false);

        private readonly string _ipString;
        private NetworkMessageWriter _networkWriter;

        public NetworkClientConnection(string ipString)
        {
            _ipString = ipString;

        }

        public void Connect()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(_ipString);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, NetworkConstants.SERVER_PORT);

                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _networkWriter = new NetworkMessageWriter(clientSocket);

                clientSocket.BeginConnect(ipEndPoint, ClientConnectCallback, clientSocket);
                connectionAsyncLock.WaitOne();
            }
            catch (Exception e)
            {
                throw new NetworkingException(e.Message);
            }
        }

        public void SendMessage(string message)
        {
            if (_networkWriter == null)
            {
                throw new NetworkingException("Cannot send message, client is not connected.");
            }
            _networkWriter.SendMessage(new NetworkMessage(message, DateTime.Now, DateTime.MinValue));
        }

        public void Close()
        {
            Console.WriteLine("Shutting down client connection...");
            _networkWriter?.Dispose();
            _networkWriter = null;
        }

        private void ClientConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                Console.WriteLine($"Client connected to {client.RemoteEndPoint}");
                connectionAsyncLock.Set();
            }
            catch (Exception e)
            {
                throw new NetworkingException(e.Message);
            }
        }


    }
}