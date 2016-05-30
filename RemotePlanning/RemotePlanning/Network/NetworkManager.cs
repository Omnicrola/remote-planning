using System;

namespace RemotePlanning.Network
{
    internal class NetworkManager : IDisposable
    {
        public void Connect(string address)
        {
            Console.WriteLine("Connecting! (not really)");
        }

        public void StartHosting()
        {
            Console.WriteLine("Hosting! (not really)");
        }

        public void Dispose()
        {

        }
    }
}