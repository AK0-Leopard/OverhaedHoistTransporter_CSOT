using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Mirle.BigDataCollection.Info
{
    [Serializable]
    public class SUBEQP
    {
        [XmlElement(ElementName = "UNITNAME", Order = 1)]
        public string UNITNAME { get; set; } = string.Empty;

        [XmlElement(ElementName = "PARAM_LIST", Order = 2)]
        public PARAM_LIST PARAM_LIST = new PARAM_LIST();

        public SUBEQP Clone()
        {

            using (Stream objectStream = new MemoryStream())
            {
                //序列化物件格式
                IFormatter formatter = new BinaryFormatter();
                //將自己所有資料序列化
                formatter.Serialize(objectStream, this);
                //複寫資料流位置，返回最前端
                objectStream.Seek(0, SeekOrigin.Begin);
                //再將objectStream反序列化回去 
                return formatter.Deserialize(objectStream) as SUBEQP;
            }


        }
    }
}