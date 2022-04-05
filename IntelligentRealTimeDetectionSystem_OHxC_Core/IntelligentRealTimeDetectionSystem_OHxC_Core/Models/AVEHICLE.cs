using com.mirle.iibg3k0.ids.ProtocolFormat.OHTMessage;
using System;
using static com.mirle.iibg3k0.ids.ohxc.App.CSAppConstants;

namespace com.mirle.iibg3k0.ids.ohxc.Models
{
    public partial class AVEHICLE
    {
        public string VEHICLE_ID { get; set; }
        public E_VH_TYPE VEHICLE_TYPE { get; set; }
        public string CUR_ADR_ID { get; set; }
        public string CUR_SEC_ID { get; set; }
        public DateTime? SEC_ENTRY_TIME { get; set; }
        public double ACC_SEC_DIST { get; set; }
        public VHModeStatus MODE_STATUS { get; set; }
        public VHActionStatus ACT_STATUS { get; set; }
        public string MCS_CMD { get; set; }
        public string OHTC_CMD { get; set; }
        public VhStopSingle BLOCK_PAUSE { get; set; }
        public VhStopSingle CMD_PAUSE { get; set; }
        public VhStopSingle OBS_PAUSE { get; set; }
        public VhStopSingle HID_PAUSE { get; set; }
        public VhStopSingle ERROR { get; set; }
        public VhStopSingle EARTHQUAKE_PAUSE { get; set; }
        public VhStopSingle SAFETY_DOOR_PAUSE { get; set; }
        public VhStopSingle OHXC_OBS_PAUSE { get; set; }
        public VhStopSingle OHXC_BLOCK_PAUSE { get; set; }
        public int OBS_DIST { get; set; }
        public VhLoadCSTStatus HAS_CST { get; set; }
        public string CST_ID { get; set; }
        public DateTime? UPD_TIME { get; set; }
        public int VEHICLE_ACC_DIST { get; set; }
        public int MANT_ACC_DIST { get; set; }
        public DateTime? MANT_DATE { get; set; }
        public int GRIP_COUNT { get; set; }
        public int GRIP_MANT_COUNT { get; set; }
        public DateTime? GRIP_MANT_DATE { get; set; }
        public string NODE_ADR { get; set; }
        public bool IS_PARKING { get; set; }
        public DateTime? PARK_TIME { get; set; }
        public string PARK_ADR_ID { get; set; }
        public bool IS_CYCLING { get; set; }
        public DateTime? CYCLERUN_TIME { get; set; }
        public string CYCLERUN_ID { get; set; }
    }
}
