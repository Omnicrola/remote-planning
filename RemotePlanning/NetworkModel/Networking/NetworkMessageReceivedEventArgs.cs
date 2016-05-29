using System;

namespace NetworkModel.Networking
{
    public class NetworkMessageReceivedEventArgs : EventArgs
    {
        public int Senderid { get; private set; }
        public NetworkMessage NetworkMessage { get; private set; }

        public NetworkMessageReceivedEventArgs(int senderid, NetworkMessage networkMessage)
        {
            Senderid = senderid;
            NetworkMessage = networkMessage;
        }
    }
}