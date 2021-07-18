using Mirle.MPLC;
using Mirle.MPLC.DataType;

namespace Mirle.BigDataCollection.DataCollection.SignalCollection
{
    public class PressureCollection
    {
        public Word Pressure { get; }

        public PressureCollection(int unitNumber, IMPLCProvider smReadWriter)
        {
            int addr = 11086 + unitNumber;
            Pressure = new Word(smReadWriter, $"D{addr}");
        }
    }
}