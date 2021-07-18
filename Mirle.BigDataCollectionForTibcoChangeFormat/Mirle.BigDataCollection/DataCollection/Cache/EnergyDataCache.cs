using Mirle.BigDataCollection.DataCollection.SignalCollection;
using Mirle.Extensions;

namespace Mirle.BigDataCollection.DataCollection.Cache
{
    public class EnergyDataCache
    {
        public string CurrentR { get; set; }
        public string CurrentT { get; set; }
        public string VoltageRS { get; set; }
        public string VoltageST { get; set; }
        public string ElectricEnergy { get; set; }

        public EnergyDataCache(EnergyCollection collection)
        {
            var currentR = collection.CurrentR.GetData().ToASCII();
            var currentT = collection.CurrentT.GetData().ToASCII();
            var voltageRS = collection.VoltageRS.GetData().ToASCII();
            var voltageST = collection.VoltageST.GetData().ToASCII();
            var electricEnergy = collection.ElectricEnergy.GetData().ToASCII();

            CurrentR = currentR == string.Empty ? "0" : currentR;
            CurrentT = currentT == string.Empty ? "0" : currentT;
            VoltageRS = voltageRS == string.Empty ? "0" : voltageRS;
            VoltageST = voltageST == string.Empty ? "0" : voltageST;
            ElectricEnergy = electricEnergy == string.Empty ? "0" : electricEnergy;
        }

        public EnergyDataCache()
        {
            CurrentR = "0";
            CurrentT = "0";
            VoltageRS = "0";
            VoltageST = "0";
            ElectricEnergy = "0";
        }
    }
}