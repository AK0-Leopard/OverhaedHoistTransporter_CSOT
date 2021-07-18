using Mirle.BigDataCollection.Info;
using System.Collections.Generic;

namespace Mirle.BigDataCollection.DataCollection
{
    public class MESSAGE : IMessage
    {
        public EQP_LIST EQP_LIST = new EQP_LIST() { EQP = new List<EQP>() };

        public MESSAGE() : base()
        {
        }

        public MESSAGE(MessageParameter message) : base(message)
        {
        }
    }
}