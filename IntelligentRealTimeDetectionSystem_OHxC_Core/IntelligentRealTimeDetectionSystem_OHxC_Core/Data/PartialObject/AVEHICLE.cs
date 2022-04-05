using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace com.mirle.iibg3k0.ids.ohxc.PartialObject
{
    public class AVEHICLE : com.mirle.ibg3k0.sc.AVEHICLE
    {

        public event EventHandler<bool> IndleWarningFlagChanged;
        public event EventHandler<bool> IsLoadUnloadTooLongFlagChanged;
        public event EventHandler<string> BlockBlockingTooLong;

        public event EventHandler<double> DistanceChanged;

        private bool isidlewarning = false;
        public bool IsIdleWarning
        {
            set
            {
                if (isidlewarning != value)
                {
                    isidlewarning = value;
                    IndleWarningFlagChanged?.Invoke(this, value);
                }
            }
            get { return isidlewarning; }
        }
        private bool isloadunloadtoolong = false;
        public bool IsLoadUnloadTooLong
        {
            set
            {
                if (isloadunloadtoolong != value)
                {
                    isloadunloadtoolong = value;
                    IsLoadUnloadTooLongFlagChanged?.Invoke(this, value);
                }
            }
            get { return isloadunloadtoolong; }
        }

        public Stopwatch FromTheLastSectionUpdateTimer { get; private set; } = new Stopwatch();
        private string cur_sec_id
        {
            set
            {
                if (CUR_SEC_ID != value)
                {
                    CUR_SEC_ID = value;
                    FromTheLastSectionUpdateTimer.Restart();
                }
            }
        }
        private double acc_sec_dist
        {
            set
            {
                if (ACC_SEC_DIST != value)
                {
                    ACC_SEC_DIST = value;
                    DistanceChanged?.Invoke(this, ACC_SEC_DIST);
                }
            }
        }

        private VhStopSingle obs_pause
        {
            set
            {
                if (OBS_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    OBS_PAUSE = value;
                }
            }
        }
        private VhStopSingle block_pause
        {
            set
            {
                if (BLOCK_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    BLOCK_PAUSE = value;
                }
            }
        }
        private VhStopSingle cmd_pause
        {
            set
            {
                if (CMD_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    CMD_PAUSE = value;
                }
            }
        }
        private VhStopSingle hid_pause
        {
            set
            {
                if (HID_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    HID_PAUSE = value;
                }
            }
        }
        private VhStopSingle error
        {
            set
            {
                if (ERROR != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    ERROR = value;
                }
            }
        }
        private VhStopSingle earthquake_pause
        {
            set
            {
                if (EARTHQUAKE_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    EARTHQUAKE_PAUSE = value;
                }
            }
        }
        private VhStopSingle safty_door_pause
        {
            set
            {
                if (SAFETY_DOOR_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    SAFETY_DOOR_PAUSE = value;
                }
            }
        }
        private VhStopSingle ohxc_obs_pause
        {
            set
            {
                if (OHXC_OBS_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    OHXC_OBS_PAUSE = value;
                }
            }
        }
        private VhStopSingle ohxc_block_pause
        {
            set
            {
                if (OHXC_BLOCK_PAUSE != value)
                {
                    if (value == VhStopSingle.StopSingleOff)
                    {
                        FromTheLastSectionUpdateTimer.Restart();
                    }
                    OHXC_BLOCK_PAUSE = value;
                }
            }
        }

        [NotMapped]
        public Stopwatch LoadUnloadTimer { get; private set; } = new Stopwatch();
        private EventType vhrecenttranevent = EventType.AdrPass;
        public override EventType VhRecentTranEvent
        {
            get { return vhrecenttranevent; }
            set
            {
                if ((vhrecenttranevent == EventType.Vhloading && value != EventType.Vhloading) ||
                    (vhrecenttranevent == EventType.Vhunloading && value != EventType.Vhunloading))
                {
                    FromTheLastSectionUpdateTimer.Restart();
                }
                vhrecenttranevent = value;
                switch (vhrecenttranevent)
                {
                    case EventType.Vhloading:
                    case EventType.Vhunloading:
                        LoadUnloadTimer.Restart();
                        break;
                    default:
                        LoadUnloadTimer.Stop();
                        break;
                }
            }
        }

        public void ObstacleVh_DistanceChanged(object sender, double e)
        {
            var ObstacleVh = sender as AVEHICLE;
            //App.CSApplication.getInstance().CheckBLL.SetEventNoticeObstructedVhToContinue(VEHICLE_ID);
            App.CSApplication.getInstance().EventBLL.PublishEvent
                (App.CSAppConstants.REDIS_EVENT_CODE_NOTICE_OBSTRUCTED_VH_CONTINUE, VEHICLE_ID, "");

            ObstacleVh.DistanceChanged -= ObstacleVh_DistanceChanged;
        }


        public int Block_Recheck_Times = 0;

        public void setObject(com.mirle.ibg3k0.sc.AVEHICLE aVEHICLE)
        {
            cur_sec_id = aVEHICLE.CUR_SEC_ID.Trim();
            CUR_ADR_ID = aVEHICLE.CUR_ADR_ID.Trim();
            acc_sec_dist = aVEHICLE.ACC_SEC_DIST;
            WillPassSectionID = aVEHICLE.WillPassSectionID;
            OHTC_CMD = aVEHICLE.OHTC_CMD;
            VhRecentTranEvent = aVEHICLE.VhRecentTranEvent;
            obs_pause = aVEHICLE.OBS_PAUSE;
            block_pause = aVEHICLE.BLOCK_PAUSE;
            cmd_pause = aVEHICLE.CMD_PAUSE;
            hid_pause = aVEHICLE.HID_PAUSE;
            error = aVEHICLE.ERROR;
            earthquake_pause = aVEHICLE.EARTHQUAKE_PAUSE;
            safty_door_pause = aVEHICLE.SAFETY_DOOR_PAUSE;
            ohxc_obs_pause = aVEHICLE.OHXC_BLOCK_PAUSE;


            this.MODE_STATUS = aVEHICLE.MODE_STATUS;
            this.ACT_STATUS = aVEHICLE.ACT_STATUS;
            this.IS_PARKING = aVEHICLE.IS_PARKING;
        }

        public void setObject(VEHICLE_INFO aVEHICLE)
        {
            cur_sec_id = aVEHICLE.CURSECID.Trim();
            CUR_ADR_ID = aVEHICLE.CURADRID.Trim();
            acc_sec_dist = aVEHICLE.ACCSECDIST;
            WillPassSectionID = aVEHICLE.WillPassSectionID.ToList();
            OHTC_CMD = aVEHICLE.OHTCCMD;
            VhRecentTranEvent = aVEHICLE.VhRecentTranEvent;
            obs_pause = aVEHICLE.OBSPAUSE;
            block_pause = aVEHICLE.BLOCKPAUSE;
            cmd_pause = aVEHICLE.CMDPAUSE;
            hid_pause = aVEHICLE.HIDPAUSE;
            error = aVEHICLE.ERROR;
            //earthquake_pause = aVEHICLE.;
            //safty_door_pause = aVEHICLE;
            //ohxc_obs_pause = aVEHICLE;


            this.MODE_STATUS = aVEHICLE.MODESTATUS;
            this.ACT_STATUS = aVEHICLE.ACTSTATUS;
            this.IS_PARKING = aVEHICLE.ISPARKING;
        }
    }
}