using com.mirle.ibg3k0.sc.BLL;
using com.mirle.ibg3k0.sc.Data.VO;
using System;

namespace com.mirle.ibg3k0.sc.ObjectRelay
{
    public class ControlZoneToShow
    {
        ControlZoneInfo controlZoneInfo;
        public ControlZoneToShow(ControlZoneInfo controlZoneInfo)
        {
            this.controlZoneInfo = controlZoneInfo;
            WarnWaterLevel = VhLimitCount * 0.8;
        }

        public string ID { get { return controlZoneInfo?.ID; } }
        public string VhCount { get { return controlZoneInfo?.VhCount.ToString(); } }
        public int VhLimitCount { get { return controlZoneInfo == null ? 0 : controlZoneInfo.VhLimitCount; } }
        public double WarnWaterLevel { get; private set; }
    }
}
