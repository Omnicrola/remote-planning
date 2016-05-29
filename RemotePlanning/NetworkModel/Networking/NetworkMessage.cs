using System;
using System.IO;
using System.Xml.Serialization;

namespace NetworkModel.Networking
{
    public class NetworkMessage
    {
        public NetworkMessage() { }
        public NetworkMessage(string message, DateTime sent, DateTime recieved)
        {
            Message = message;
            Sent = sent;
            Recieved = recieved;
        }

        public string Message { get; set; }
        public DateTime Sent { get; set; }
        public DateTime Recieved { get; set; }

        public string Serialize()
        {
            var serializer = new XmlSerializer(GetType());
            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, this);
                return stringWriter.ToString();
            }
        }

        public static NetworkMessage Deserialize(string singleMessage)
        {
            var serializer = new XmlSerializer(typeof(NetworkMessage));
            using (TextReader textReader = new StringReader(singleMessage))
            {
                return (NetworkMessage)serializer.Deserialize(textReader);
            }
        }
    }
}