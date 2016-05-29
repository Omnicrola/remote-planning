using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkModel.Networking
{
    public class NetworkServerConnection
    {
        private static int NEXT_CLIENT_ID = 1;

        private static readonly ManualResetEvent _serverAcceptManualEvent = new ManualResetEvent(false);
        private bool _isRunning;
        private readonly List<NetworkClient> _clients;
        private readonly string _ipString;

        public NetworkServerConnection(string ipString)
        {
            _ipString = ipString;
            _clients = new List<NetworkClient>();
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

            var networkClient = CreateNetworkClient(handler);

            _clients.Add(networkClient);
            Console.WriteLine("Client connected! " + _clients.Count);
        }

        private NetworkClient CreateNetworkClient(Socket handler)
        {
            var clientId = NEXT_CLIENT_ID++;
            NetworkMessageReceiver networkMessageReceiver = new NetworkMessageReceiver(clientId, handler, 1024);
            NetworkMessageWriter networkMessageWriter = new NetworkMessageWriter(handler);
            NetworkClient networkClient = new NetworkClient(clientId, networkMessageReceiver, networkMessageWriter);
            networkClient.MessageRecieved += RebroadcastMessage;
            return networkClient;
        }

        private void RebroadcastMessage(object sender, NetworkMessageReceivedEventArgs eventArgs)
        {
            foreach (NetworkClient networkClient in _clients)
            {
                if (networkClient.ClientId != eventArgs.Senderid)
                {
                    networkClient.SendMessage(eventArgs.NetworkMessage);
                }
            }
        }
    }
}