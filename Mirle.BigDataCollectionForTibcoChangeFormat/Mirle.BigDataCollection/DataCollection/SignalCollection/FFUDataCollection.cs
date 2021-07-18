using Mirle.MPLC;
using Mirle.MPLC.DataType;

namespace Mirle.BigDataCollection.DataCollection.Collection
{
    public class FFUDataCollection
    {
        public Word Rotote_Speed { get; }

        public FFUDataCollection(string mplcAddr, IMPLCProvider smReadWriter)
        {
            Rotote_Speed = new Word(smReadWriter, $"D{mplcAddr}");
        }
    }
}