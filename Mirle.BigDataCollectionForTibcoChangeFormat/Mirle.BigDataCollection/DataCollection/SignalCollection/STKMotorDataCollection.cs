using Mirle.MPLC;
using Mirle.MPLC.DataType;

namespace Mirle.BigDataCollection.DataCollection
{
    public class STKMotorDataCollection
    {
        public Word Command_Position { get; }
        public Word Current_Position { get; }
        public Word Torque { get; }
        public Word Deviation { get; }
        public Word Speed { get; }
        public Word Load_Factor { get; }
        public Word Regenerative_Load_Factor { get; }

        public STKMotorDataCollection(int craneNo, int unitNumber, IMPLCProvider smReadWriter)
        {
            int addr = 11019 + ((craneNo - 1) * 200) + ((unitNumber - 1) * 6);
            Current_Position = new Word(smReadWriter, $"D{addr + 1}");
            Command_Position = new Word(smReadWriter, $"D{addr + 2}");
            Speed = new Word(smReadWriter, $"D{addr + 3}");
            Torque = new Word(smReadWriter, $"D{addr + 4}");
            Load_Factor = new Word(smReadWriter, $"D{addr + 5}");
            Regenerative_Load_Factor = new Word(smReadWriter, $"D{addr + 6}");
        }
    }
}