using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace NetworkModel.Networking
{
    public class NetworkServer
    {
        public static ManualResetEvent _serverAcceptManualEvent = new ManualResetEvent(false);
        private bool _isRunning;
        private List<NetworkMessageWriter> _clients;
        private string _ipString;

        public NetworkServer(string ipString)
        {
            _ipString = ipString;
            _clients = new List<NetworkMessageWriter>();
        }

        public void Start()
        {

            IPAddress ipAddress = IPAddress.Parse(_ipString);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, NetworkConstants.SERVER_PORT);


            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(100);
                _isRunning = true;
                while (_isRunning)
                {
                    _serverAcceptManualEvent.Reset();
                    Console.WriteLine("Waiting for connection....");
                    serverSocket.BeginAccept(ServerAcceptNewConnectionCallback, serverSocket);
                    _serverAcceptManualEvent.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void StopServer()
        {
            _isRunning = false;
            _serverAcceptManualEvent.Set();
            _clients.ForEach(c => c.Dispose());
        }

        private void ServerAcceptNewConnectionCallback(IAsyncResult ar)
        {
            _serverAcceptManualEvent.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            NetworkMessageBuilder networkMessageBuilder = new NetworkMessageBuilder(handler, 1024, MessageRecieveCallback);
            NetworkMessageWriter networkMessageWriter = new NetworkMessageWriter(handler);

            _clients.Add(networkMessageWriter);
            Console.WriteLine("Client connected! " + _clients.Count);
            networkMessageBuilder.Recieve();
        }

        private void MessageRecieveCallback(IAsyncResult asyncResult)
        {
            Console.WriteLine("Server finished receiving message.");
        }



    }
}