namespace NetworkModel.Networking
{
    public class NetworkPacket
    {

        public NetworkPacket(int bufferSize)
        {
            Buffer = new byte[bufferSize];
        }

        public byte[] Buffer { get; set; }
    }
}