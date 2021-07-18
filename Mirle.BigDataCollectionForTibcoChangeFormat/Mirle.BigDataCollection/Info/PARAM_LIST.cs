using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mirle.BigDataCollection.Info
{

    [Serializable]
    public class PARAM_LIST
    {
        [XmlElement(ElementName = "PARAM")]
        public List<PARAM> PARAM = new List<PARAM>();
    }
}