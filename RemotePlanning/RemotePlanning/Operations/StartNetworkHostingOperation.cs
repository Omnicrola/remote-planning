using RemotePlanning.Network;

namespace RemotePlanning.Operations
{
    internal class StartNetworkHostingOperation : AsyncDiscreetOperation
    {
        private readonly NetworkManager _networkManager;

        public StartNetworkHostingOperation(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public override string Description { get; }
        protected override void DoWorkInternal()
        {
            _networkManager.StartHosting();
        }
    }
}