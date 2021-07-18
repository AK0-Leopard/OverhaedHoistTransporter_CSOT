using Mirle.MPLC;
using Mirle.MPLC.DataType;

namespace Mirle.BigDataCollection.DataCollection.SignalCollection
{
    public class EnergyCollection
    {
        public WordBlock CurrentR { get; }
        public WordBlock CurrentT { get; }
        public WordBlock VoltageRS { get; }
        public WordBlock VoltageST { get; }
        public WordBlock ElectricEnergy { get; }

        public EnergyCollection(int craneNo, IMPLCProvider smReadWriter)
        {
            int addr = 11000 + (200 * (craneNo - 1));

            CurrentR = new WordBlock(smReadWriter, $"D{addr}", 2);
            addr += 4;

            CurrentT = new WordBlock(smReadWriter, $"D{addr}", 2);
            addr += 8;

            VoltageRS = new WordBlock(smReadWriter, $"D{addr}", 2);
            addr += 2;

            VoltageST = new WordBlock(smReadWriter, $"D{addr}", 2);
            addr += 4;

            ElectricEnergy = new WordBlock(smReadWriter, $"D{addr}", 2);
        }
    }
}