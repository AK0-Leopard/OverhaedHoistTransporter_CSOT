using System;

namespace Mirle.BigDataCollection.DataCollection.Cache
{
    public class STKMotorDataCache : ICashe
    {
        public string Command_Position { get; set; }
        public string Current_Position { get; set; }
        public string Torque { get; set; }
        public string Deviation { get; set; }
        public string Speed { get; set; }
        public string Load_Factor { get; set; }
        public string Regenerative_Load_Factor { get; set; }

        public STKMotorDataCache(STKMotorDataCollection collection)
        {
            Command_Position = collection.Command_Position.GetValue().ToString();
            Current_Position = collection.Current_Position.GetValue().ToString();
            Torque = collection.Torque.GetValue().ToString();
            Command_Position = collection.Command_Position.GetValue().ToString();
            Speed = collection.Speed.GetValue().ToString();
            Load_Factor = collection.Load_Factor.GetValue().ToString();
            Regenerative_Load_Factor = collection.Regenerative_Load_Factor.GetValue().ToString();
            Deviation = Convert.ToString(Convert.ToInt32(Command_Position) - Convert.ToInt32(Current_Position));
        }

        public STKMotorDataCache()
        {
            Command_Position = "0";
            Current_Position = "0";
            Torque = "0";
            Command_Position = "0";
            Speed = "0";
            Load_Factor = "0";
            Regenerative_Load_Factor = "0";
            Deviation = "0";
        }
    }
}