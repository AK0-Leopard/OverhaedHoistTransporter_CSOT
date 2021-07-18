using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mirle.BigDataCollection.Info
{
    public class EQP
    {
        [XmlElement(ElementName = "MACHINENAME", Order = 1)]
        public string MACHINENAME { get; set; } = string.Empty;

        [XmlElement(ElementName = "CHECK_TIME", Order = 2)]
        public string CHECK_TIME { get; set; } = string.Empty;

        [XmlElement(ElementName = "SUBEQP_LIST", Order = 3)]
        public MSUBEQP SUBEQP_LIST = new MSUBEQP();
    }

    public class MSUBEQP
    {
        [XmlElement(ElementName = "SUBEQP")]
        public List<SUBEQP> SUBEQP_LIST = new List<SUBEQP>();
    }
}