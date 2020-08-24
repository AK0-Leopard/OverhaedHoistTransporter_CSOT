using com.mirle.ibg3k0.bc.winform.i18n;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.ObjectRelay
{
    public class VSection_100ObjToShow : INotifyPropertyChanged
    {
        public VSECTION_100 vSECTION_100;
        public VSection_100ObjToShow(VSECTION_100 myDatabaseObject)
        {
            vSECTION_100 = myDatabaseObject;
        }
        [LocalizedDisplayNameAttribute("SEC_ID")]
        public string SEC_ID
        {
            get
            {
                return vSECTION_100.SEC_ID.Trim();
            }
        }
        [LocalizedDisplayNameAttribute("DIRC_DRIV")]
        public E_DIRC_DRIV DIRC_DRIV
        {
            set
            {
                vSECTION_100.DIRC_DRIV = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.DIRC_DRIV;
            }
        }
        [LocalizedDisplayNameAttribute("DIRC_GUID")]
        public E_DIRC_GUID DIRC_GUID
        {
            set
            {
                vSECTION_100.DIRC_GUID = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.DIRC_GUID;
            }
        }

        [LocalizedDisplayNameAttribute("SEC_ORDER_NUM")]
        public int? SEC_ORDER_NUM
        {
            set
            {
                vSECTION_100.SEC_ORDER_NUM = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.SEC_ORDER_NUM;
            }
        }

        [LocalizedDisplayNameAttribute("SEG_NUM")]
        public string SEG_NUM
        {
            set
            {
                vSECTION_100.SEG_NUM = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.SEG_NUM;
            }
        }

        [LocalizedDisplayNameAttribute("FROM_ADR_ID")]
        public string FROM_ADR_ID
        {
            set
            {
                vSECTION_100.FROM_ADR_ID = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.FROM_ADR_ID;
            }
        }
        [LocalizedDisplayNameAttribute("TO_ADR_ID")]
        public string TO_ADR_ID
        {
            set
            {
                vSECTION_100.TO_ADR_ID = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.TO_ADR_ID;
            }
        }
        [LocalizedDisplayNameAttribute("SEC_DIS")]
        public double SEC_DIS
        {
            set
            {
                vSECTION_100.SEC_DIS = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.SEC_DIS;
            }
        }
        [LocalizedDisplayNameAttribute("LAST_TECH_TIME")]
        public Nullable<DateTime> LAST_TECH_TIME
        {
            set
            {
                vSECTION_100.LAST_TECH_TIME = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.LAST_TECH_TIME;
            }
        }
        [LocalizedDisplayNameAttribute("SEC_SPD")]
        public double? SEC_SPD
        {
            set
            {
                vSECTION_100.SEC_SPD = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.SEC_SPD;
            }
        }
        [LocalizedDisplayNameAttribute("CDOG_1")]
        public E_DIRC_GUID CDOG_1
        {
            set
            {
                vSECTION_100.CDOG_1 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CDOG_1;
            }
        }
        [LocalizedDisplayNameAttribute("CHG_SEG_NUM_1")]
        public string CHG_SEG_NUM_1
        {
            set
            {
                vSECTION_100.CHG_SEG_NUM_1 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CHG_SEG_NUM_1;
            }
        }
        [LocalizedDisplayNameAttribute("CDOG_2")]
        public E_DIRC_GUID CDOG_2
        {
            set
            {
                vSECTION_100.CDOG_2 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CDOG_2;
            }
        }
        [LocalizedDisplayNameAttribute("CHG_SEG_NUM_2")]
        public string CHG_SEG_NUM_2
        {
            set
            {
                vSECTION_100.CHG_SEG_NUM_2 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CHG_SEG_NUM_2;
            }
        }

        [LocalizedDisplayNameAttribute("PRE_BLO_REQ")]
        public int PRE_BLO_REQ
        {
            set
            {
                vSECTION_100.PRE_BLO_REQ = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.PRE_BLO_REQ;
            }
        }
        [LocalizedDisplayNameAttribute("SEC_TYPE")]
        public SectionType SEC_TYPE
        {
            set
            {
                vSECTION_100.SEC_TYPE = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.SEC_TYPE;
            }
        }
        [LocalizedDisplayNameAttribute("SEC_DIR")]
        public int SEC_DIR
        {
            set
            {
                vSECTION_100.SEC_DIR = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.SEC_DIR;
            }
        }
        [LocalizedDisplayNameAttribute("PADDING")]
        public int PADDING
        {
            set
            {
                vSECTION_100.PADDING = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.PADDING;
            }
        }
        [LocalizedDisplayNameAttribute("ENB_CHG_G_AREA")]
        public int ENB_CHG_G_AREA
        {
            set
            {
                vSECTION_100.ENB_CHG_G_AREA = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.ENB_CHG_G_AREA;
            }
        }
        [LocalizedDisplayNameAttribute("PRE_DIV")]
        public int PRE_DIV
        {
            set
            {
                vSECTION_100.PRE_DIV = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.PRE_DIV;
            }
        }
        [LocalizedDisplayNameAttribute("PRE_ADD_REPR")]
        public int PRE_ADD_REPR
        {
            set
            {
                vSECTION_100.PRE_ADD_REPR = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.PRE_ADD_REPR;
            }
        }
        [LocalizedDisplayNameAttribute("OBS_SENSOR")]
        public int OBS_SENSOR
        {
            set
            {
                vSECTION_100.OBS_SENSOR = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.OBS_SENSOR;
            }
        }
        [LocalizedDisplayNameAttribute("START_BC1")]
        public int START_BC1
        {
            set
            {
                vSECTION_100.START_BC1 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.START_BC1;
            }
        }
        [LocalizedDisplayNameAttribute("END_BC1")]
        public int END_BC1
        {
            set
            {
                vSECTION_100.END_BC1 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.END_BC1;
            }
        }
        [LocalizedDisplayNameAttribute("START_BC2")]
        public int START_BC2
        {
            set
            {
                vSECTION_100.START_BC2 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.START_BC2;
            }
        }
        [LocalizedDisplayNameAttribute("END_BC2")]
        public int END_BC2
        {
            set
            {
                vSECTION_100.END_BC2 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.END_BC2;
            }
        }
        [LocalizedDisplayNameAttribute("START_BC3")]
        public int START_BC3
        {
            set
            {
                vSECTION_100.START_BC3 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.START_BC3;
            }
        }
        [LocalizedDisplayNameAttribute("END_BC3")]
        public int END_BC3
        {
            set
            {
                vSECTION_100.END_BC3 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.END_BC3;
            }
        }
        [LocalizedDisplayNameAttribute("CHG_AREA_SECSOR_1")]
        public E_AreaSensorDir CHG_AREA_SECSOR_1
        {
            set
            {
                vSECTION_100.CHG_AREA_SECSOR_1 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CHG_AREA_SECSOR_1;
            }
        }
        [LocalizedDisplayNameAttribute("CHG_AREA_SECSOR_2")]
        public E_AreaSensorDir CHG_AREA_SECSOR_2
        {
            set
            {
                vSECTION_100.CHG_AREA_SECSOR_2 = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CHG_AREA_SECSOR_2;
            }
        }
        [LocalizedDisplayNameAttribute("OBS_SENSOR_F")]
        public int OBS_SENSOR_F
        {
            set
            {
                vSECTION_100.OBS_SENSOR_F = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.OBS_SENSOR_F;
            }
        }

        [LocalizedDisplayNameAttribute("OBS_SENSOR_R")]
        public int OBS_SENSOR_R
        {
            set
            {
                vSECTION_100.OBS_SENSOR_R = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.OBS_SENSOR_R;
            }
        }

        [LocalizedDisplayNameAttribute("OBS_SENSOR_L")]
        public int OBS_SENSOR_L
        {
            set
            {
                vSECTION_100.OBS_SENSOR_L = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.OBS_SENSOR_L;
            }
        }

        [LocalizedDisplayNameAttribute("RANGE_SENSOR_F")]
        public int RANGE_SENSOR_F
        {
            set
            {
                vSECTION_100.RANGE_SENSOR_F = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.RANGE_SENSOR_F;
            }
        }

        [LocalizedDisplayNameAttribute("IS_ADR_RPT")]
        public bool IS_ADR_RPT
        {
            set
            {
                vSECTION_100.IS_ADR_RPT = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.IS_ADR_RPT;
            }
        }
        [LocalizedDisplayNameAttribute("CAN_GUIDE_CHG")]
        public bool CAN_GUIDE_CHG
        {
            set
            {
                vSECTION_100.CAN_GUIDE_CHG = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.CAN_GUIDE_CHG;
            }
        }
        [LocalizedDisplayNameAttribute("HID_CONTROL")]
        public bool HID_CONTROL
        {
            set
            {
                vSECTION_100.HID_CONTROL = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.HID_CONTROL;
            }
        }
        [LocalizedDisplayNameAttribute("BRANCH_FLAG")]
        public bool BRANCH_FLAG
        {
            set
            {
                vSECTION_100.BRANCH_FLAG = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.BRANCH_FLAG;
            }
        }
        [LocalizedDisplayNameAttribute("AREA_SECSOR")]
        public Nullable<E_AreaSensorDir> AREA_SECSOR
        {
            set
            {
                vSECTION_100.AREA_SECSOR = value;
                NotifyPropertyChanged();
            }
            get
            {
                return vSECTION_100.AREA_SECSOR;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;
            try
            {
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception:");
            }
        }

    }
}
