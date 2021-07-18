using Mirle.BigDataCollection.Define;

namespace Mirle.BigDataCollection.Info
{
    public class TracePositionKey
    {
        public CollectionTypes Collection;
        public int UnitNo { get; set; }
        public int CraneNo { get; set; }
        public string UnitName { get; set; }
        public int Frequency { get; set; }
        public int sendRemainingSeconds { get; set; }

        public TracePositionKey(CollectionTypes collection, int unitNumber, int craneNo, int frequency)
        {
            Collection = collection;
            UnitNo = unitNumber;
            CraneNo = craneNo;
            Frequency = frequency;
        }

        public TracePositionKey(CollectionTypes collection, int frequency)
        {
            Collection = collection;
            Frequency = frequency;
        }

        public TracePositionKey(CollectionTypes collection, string unitName, int frequency)
        {
            Collection = collection;
            UnitName = unitName;
            Frequency = frequency;
        }

        public TracePositionKey(CollectionTypes collection, int unitNumber, int frequency)
        {
            Collection = collection;
            UnitNo = unitNumber;
            Frequency = frequency;
        }
    }
}