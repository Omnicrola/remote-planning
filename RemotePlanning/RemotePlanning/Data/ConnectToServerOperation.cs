using System;
using NetworkModel.Networking;
using RemotePlanning.Network;
using RemotePlanning.Operations;

namespace RemotePlanning.Data
{
    internal class ConnectToServerOperation : AsyncDiscreetOperation
    {
        private readonly NetworkManager _networkManager;
        private readonly string _address;

        public ConnectToServerOperation(NetworkManager networkManager, string address)
        {
            _networkManager = networkManager;
            _address = address;
        }

        public override string Description => "Connect to remote server";

        protected override void DoWorkInternal()
        {
            try
            {
                _networkManager.Connect(_address);
            }
            catch (NetworkingException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}