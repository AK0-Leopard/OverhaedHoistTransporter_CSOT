using System.Xml.Serialization;

namespace Mirle.BigDataCollection.Info
{
    public class SUBEQP
    {
        [XmlElement(ElementName = "UNITNAME", Order = 1)]
        public string UNITNAME { get; set; } = string.Empty;

        [XmlElement(ElementName = "PARAM_LIST", Order = 2)]
        public PARAM_LIST PARAM_LIST = new PARAM_LIST();
    }
}