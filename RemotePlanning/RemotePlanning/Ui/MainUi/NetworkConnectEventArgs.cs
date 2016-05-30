namespace RemotePlanning.Ui.MainUi
{
    public class NetworkConnectEventArgs
    {
        public NetworkConnectEventArgs(string address)
        {
            Address = address;
        }

        public string Address { get; private set; }
    }
}