using Mirle.BigDataCollection.Info;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mirle.BigDataCollection.DataCollection
{
    public class EQP_LIST
    {
        [XmlElement(ElementName = "EQP")]
        public List<EQP> EQP = new List<EQP>();
    }
}