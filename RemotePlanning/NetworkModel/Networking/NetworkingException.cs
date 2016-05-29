using System;

namespace NetworkModel.Networking
{
    public class NetworkingException : Exception
    {
        public NetworkingException(string message) : base(message)
        {
        }
    }
}