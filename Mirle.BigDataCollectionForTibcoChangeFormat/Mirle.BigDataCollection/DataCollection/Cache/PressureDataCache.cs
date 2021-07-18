using Mirle.BigDataCollection.DataCollection.SignalCollection;

namespace Mirle.BigDataCollection.DataCollection.Cache
{
    public class PressureDataCache
    {
        public string Pressure { get; }

        public PressureDataCache(PressureCollection collection)
        {
            Pressure = collection.Pressure.GetValue().ToString();
        }

        public PressureDataCache()
        {
            Pressure = "0";
        }
    }
}