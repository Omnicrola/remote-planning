using System;
using System.Net.Sockets;
using System.Text;

namespace NetworkModel.Networking
{
    public class NetworkMessageWriter : IDisposable
    {
        private readonly Socket _socket;

        public NetworkMessageWriter(Socket socket)
        {
            _socket = socket;
        }

        public void SendMessage(string data)
        {
            data += NetworkConstants.MESSAGE_TERMINATOR;
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            _socket.BeginSend(byteData, 0, byteData.Length, 0, SendCompleteCallback, _socket);
        }

        private void SendCompleteCallback(IAsyncResult asyncResult)
        {
            try
            {
                int bytesSent = _socket.EndSend(asyncResult);
                Console.WriteLine($"Finished sending {bytesSent} bytes.");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Dispose()
        {
            _socket.Close();
        }
    }
}