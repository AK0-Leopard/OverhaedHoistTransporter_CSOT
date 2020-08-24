using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.ObjectRelay
{
    public class VehicleObjToShow : INotifyPropertyChanged
    {
        double distance2millimeter = 1000;
        double km2millimeter = 1000000;
        Common.ObjCacheManager objCacheManager = null;

        //public static BindingList<VehicleObjToShow> ObjectToShow_list = new BindingList<VehicleObjToShow>();
        AVEHICLE vehicle = null;
        public VehicleObjToShow(Common.ObjCacheManager _objCacheManager, AVEHICLE myDatabaseObject, double distance_scale)
        {
            objCacheManager = _objCacheManager;
            this.vehicle = myDatabaseObject;
            distance2millimeter = distance_scale;
        }
        public string VEHICLE_ID
        {
            get { return vehicle.VEHICLE_ID; }
        }
        public VehicleState State
        {
            get { return vehicle.State; }
        }
        //[DisplayName("Address ID")]
        //public string cUR_ADR_ID
        //{
        //    get { return vehicle.CUR_ADR_ID; }
        //    set
        //    {
        //        this.vehicle.CUR_ADR_ID = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.cUR_ADR_ID));
        //    }
        //}
        //[DisplayName("Section ID")]
        //public string cUR_SEC_ID
        //{
        //    get { return vehicle.CUR_SEC_ID; }
        //    set
        //    {
        //        this.vehicle.CUR_SEC_ID = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.cUR_SEC_ID));
        //    }
        //}

        public string CUR_SEC_ID
        {
            get { return vehicle.CUR_SEC_ID; }
        }
        public string CST_ID
        {
            get
            {
                if (vehicle.HAS_CST == 1)
                {
                    return vehicle.CST_ID;

                }
                else
                {
                    return "";
                }
            }
        }
        public string CMD_CST_ID
        {
            get { return vehicle.CMD_CST_ID; }
        }
        public int CMD_Priority
        {
            get { return vehicle.CMD_Priority; }
        }
        public string FROM_PORT_ID
        {
            get
            {
                string from_adr = vehicle.FromAdr;
                return objCacheManager.GetPortStation(from_adr)?.PORT_ID;
            }
        }
        public string TO_PORT_ID
        {
            get
            {
                string to_adr = vehicle.ToAdr;
                return objCacheManager.GetPortStation(to_adr)?.PORT_ID;
            }
        }


        public string MODE_STATUS
        {
            get
            {
                switch (vehicle.MODE_STATUS)
                {
                    case VHModeStatus.AutoLocal:
                        return "Auto-L";
                    case VHModeStatus.AutoRemote:
                        return "Auto-R";
                    case VHModeStatus.InitialPowerOff:
                        return "Power Off";
                    case VHModeStatus.InitialPowerOn:
                        return "Power On";
                    case VHModeStatus.Manual:
                        return "Manual";
                    default:
                        return "None";
                }
            }
        }

        public VHActionStatus ACT_STATUS
        {
            get { return vehicle.ACT_STATUS; }
        }
        public string MCS_CMD
        {
            get
            {
                return vehicle.MCS_CMD == null ?
                    string.Empty : vehicle.MCS_CMD.Trim();
            }
        }
        public string OHTC_CMD
        {
            get
            {
                return vehicle.OHTC_CMD == null ?
                    string.Empty : vehicle.OHTC_CMD.Trim();
            }
        }
        //[DisplayName("CST ID")]
        //public string cST_ID
        //{
        //    get { return vehicle.CST_ID; }
        //    set
        //    {
        //        vehicle.CST_ID = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.cST_ID));
        //    }
        //}

        public bool bLOCK_PAUSE2Show
        {
            get
            {
                return vehicle.BLOCK_PAUSE == VhStopSingle.StopSingleOn;
            }
        }
        public VhStopSingle BLOCK_PAUSE
        {
            set
            {
                vehicle.BLOCK_PAUSE = value;
                NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.bLOCK_PAUSE2Show));
            }
        }

        public bool cMD_PAUSE2Show
        {
            get
            {
                return vehicle.CMD_PAUSE == VhStopSingle.StopSingleOn;
            }
        }
        public VhStopSingle CMD_PAUSE
        {
            set
            {
                vehicle.CMD_PAUSE = value;
                NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.cMD_PAUSE2Show));
            }
        }

        public bool oBS_PAUSE2Show
        {
            get
            {
                return vehicle.OBS_PAUSE == VhStopSingle.StopSingleOn;
            }
        }
        public VhStopSingle OBS_PAUSE
        {
            set
            {
                vehicle.OBS_PAUSE = value;
                NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.oBS_PAUSE2Show));
            }
        }
        public bool hID_PAUSE2Show
        {
            get
            {
                return vehicle.HID_PAUSE == VhStopSingle.StopSingleOn;
            }
        }
        public VhStopSingle HID_PAUSE
        {
            set
            {
                vehicle.HID_PAUSE = value;
                NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.hID_PAUSE2Show));
            }
        }


        public double oBS_DIST2Show
        {
            get
            {
                return Math.Round((vehicle.OBS_DIST / distance2millimeter), 2, MidpointRounding.AwayFromZero);
            }
        }

        public int OBS_DIST
        {
            get { return (int)(vehicle.OBS_DIST / distance2millimeter); }
            set
            {
                vehicle.OBS_DIST = value;
                NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.oBS_DIST2Show));
            }
        }
        //[DisplayName("Has CST")]
        //public int hAS_CST
        //{
        //    get { return vehicle.HAS_CST; }
        //    set
        //    {
        //        vehicle.HAS_CST = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.hAS_CST));
        //    }
        //}

        public double vEHICLE_ACC_DIST2Show
        {
            get
            {
                return Math.Round((vehicle.VEHICLE_ACC_DIST / km2millimeter), 2, MidpointRounding.AwayFromZero);
            }
        }
        //[DisplayName("ODO(km)")]
        public int VEHICLE_ACC_DIST
        {
            get
            {
                return (int)(vehicle.VEHICLE_ACC_DIST / km2millimeter);
            }
        }
        //[DisplayName("Maintain ODO")]
        //public int mANT_ACC_DIST
        //{
        //    get { return vehicle.MANT_ACC_DIST; }
        //    set
        //    {
        //        vehicle.MANT_ACC_DIST = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.mANT_ACC_DIST));
        //    }
        //}
        //[DisplayName("Last Maintain Date")]
        //public DateTime? mANT_DATE
        //{
        //    get { return vehicle.MANT_DATE ?? DateTime.MinValue; }
        //    set
        //    {
        //        vehicle.MANT_DATE = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.mANT_DATE));
        //    }
        //}
        //[DisplayName("GRIP count")]
        //public int gRIP_COUNT
        //{
        //    get { return vehicle.GRIP_COUNT; }
        //    set
        //    {
        //        vehicle.GRIP_COUNT = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.gRIP_COUNT));
        //    }
        //}
        //[DisplayName("GRIP Mant count")]
        //public int gRIP_MANT_COUNT
        //{
        //    get { return vehicle.GRIP_MANT_COUNT; }
        //    set
        //    {
        //        vehicle.GRIP_MANT_COUNT = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.gRIP_MANT_COUNT));
        //    }
        //}
        //[DisplayName("Last GRIP Mant date")]
        //public DateTime? gRIP_MANT_DATE
        //{
        //    get { return vehicle.GRIP_MANT_DATE ?? DateTime.MinValue; }
        //    set
        //    {
        //        vehicle.GRIP_MANT_DATE = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.gRIP_MANT_DATE));
        //    }
        //}
        //[DisplayName("Mode Adr")]
        //public string nODE_ADR
        //{
        //    get { return vehicle.NODE_ADR; }
        //    set
        //    {
        //        vehicle.NODE_ADR = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.nODE_ADR));
        //    }
        //}
        public bool IS_PARKING
        {
            get { return vehicle.IS_PARKING; }
        }
        public DateTime? PARK_TIME
        {
            get { return vehicle.PARK_TIME ?? DateTime.MinValue; }
        }

        public bool IS_CYCLING
        {
            get { return vehicle.IS_CYCLING; }
        }
        public DateTime? CYCLERUN_TIME
        {
            get { return vehicle.CYCLERUN_TIME ?? DateTime.MinValue; }
        }

        public double ACC_SEC_DIST2Show
        {
            get
            {
                return Math.Round((vehicle.ACC_SEC_DIST / distance2millimeter), 2, MidpointRounding.AwayFromZero);
            }
        }
        [Browsable(false)]
        public double ACC_SEC_DIST
        {
            get { return (vehicle.ACC_SEC_DIST / distance2millimeter); }
        }
        public DateTime? UPD_TIME
        {
            get { return vehicle.UPD_TIME ?? DateTime.MinValue; }
        }
        //[DisplayName("Cycle Zone ID")]
        //public string cYCLERUN_ID
        //{
        //    get { return vehicle.CYCLERUN_ID; }
        //    set
        //    {
        //        vehicle.CYCLERUN_ID = value;
        //        NotifyPropertyChanged(BCFUtility.getPropertyName(() => this.cYCLERUN_ID));
        //    }
        //}
        //This is to notify the changes made to the object directly and not from the control. This refreshes the datagridview.
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            try
            {
                if (PropertyChanged != null)
                {
                    Adapter.Invoke((obj) =>
                    {
                        {
                            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                        }
                    }, null);

                }
            }
            catch
            {

            }
        }
    }
}
