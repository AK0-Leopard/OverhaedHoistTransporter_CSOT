using Mirle.BigDataCollection.Define;

namespace Mirle.BigDataCollection.Info
{
    public class Device
    {
        public string StockerID;
        public ControlModeTypes ControlMode;

        public Device(string stockerID, int controlMode)
        {
            StockerID = stockerID;
            ControlMode = (ControlModeTypes)controlMode;
        }
    }
}