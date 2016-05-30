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
        private readonly List<NetworkConnection> _clients;
        private readonly string _ipString;
        private Thread _thread;

        public NetworkServerConnection(string ipString)
        {
            _ipString = ipString;
            _clients = new List<NetworkConnection>();
        }

        public void Start()
        {
            _thread = new Thread(Run);
            _thread.Name = "Network Server";
            _thread.Start();
        }

        private void Run()
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
            Console.WriteLine("Shutting down server...");
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

        private NetworkConnection CreateNetworkClient(Socket handler)
        {
            var clientId = NEXT_CLIENT_ID++;
            NetworkMessageReceiver networkMessageReceiver = new NetworkMessageReceiver(clientId, handler, 1024);
            NetworkMessageWriter networkMessageWriter = new NetworkMessageWriter(handler);
            NetworkConnection networkConnection = new NetworkConnection(clientId, networkMessageReceiver, networkMessageWriter);
            networkConnection.MessageRecieved += RebroadcastMessage;
            return networkConnection;
        }

        private void RebroadcastMessage(object sender, NetworkMessageReceivedEventArgs eventArgs)
        {
            Console.WriteLine($"Rebroadcasting message from ({eventArgs.Senderid}) : {eventArgs.NetworkMessage.Message}");
            foreach (

                NetworkConnection networkClient in _clients)
            {
                if (networkClient.ClientId != eventArgs.Senderid)
                {
                    networkClient.SendMessage(eventArgs.NetworkMessage);
                }
            }
        }
    }
}