using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.Vo
{
    public class VehicleHistoricalInfo
    {
        public DateTime Time;
        public string ID;
        public sc.ProtocolFormat.OHTMessage.VEHICLE_INFO VhInfo;
        public string sTime
        {
            get
            {
                return Time.ToString(sc.App.SCAppConstants.DateTimeFormat_23);
            }
        }

    }
}
