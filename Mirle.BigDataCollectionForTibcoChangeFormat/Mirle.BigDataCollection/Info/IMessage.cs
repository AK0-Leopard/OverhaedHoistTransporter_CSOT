using System.Xml.Serialization;

namespace Mirle.BigDataCollection.DataCollection
{
    public class IMessage
    {
        [XmlElement(ElementName = "MESSAGE_ID")]
        public string MessageID { get; set; } = string.Empty;

        [XmlElement(ElementName = "TRANSACTIONID")]
        public string TransactionID { get; set; } = string.Empty;

        [XmlElement(ElementName = "SYSTEMBYTE")]
        public string SystemByte { get; set; } = string.Empty;

        [XmlElement(ElementName = "TIMESTAMP")]
        public string TimeStamp { get; set; } = string.Empty;

        [XmlElement(ElementName = "LINENAME")]
        public string LineName { get; set; } = string.Empty;

        public IMessage()
        {
        }

        public IMessage(MessageParameter message)
        {
            MessageID = message.MessageID;
            TransactionID = message.TransactionID;
            TimeStamp = message.TimeStamp;
            LineName = message.LineName;
        }
    }
}