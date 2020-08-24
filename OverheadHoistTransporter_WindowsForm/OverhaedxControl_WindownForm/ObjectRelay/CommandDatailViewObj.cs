using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.ObjectRelay
{
    public class CommandDatailViewObj : INotifyPropertyChanged
    {
        AVEHICLE vhehicle;
        App.WindownApplication app = null;
        public CommandDatailViewObj(AVEHICLE myDatabaseObject)
        {
            this.vhehicle = myDatabaseObject;
            app = App.WindownApplication.getInstance();
        }



        public string vehicleID
        {
            get { return vhehicle.VEHICLE_ID; }
        }

        public string mode
        {
            get { return vhehicle.MODE_STATUS.ToString(); }
        }

        public string commandType
        {
            get
            {
                if (vhehicle.ACT_STATUS != VHActionStatus.NoCommand)
                {
                    return vhehicle.CmdType.ToString();
                }
                else
                {
                    return "-";
                }
            }
        }


        public string systemInstalled
        {
            get
            {
                if (vhehicle.IS_INSTALLED)
                {
                    return "Installed";
                }
                else
                {
                    return "Removed";
                }
            }
        }
        public string distance
        {
            get
            {
                if ((vhehicle.ToAdr == BCAppConstants.MTLMTS_Address.MTL_address || 
                    vhehicle.ToAdr == BCAppConstants.MTLMTS_Address.MTS_address || 
                    vhehicle.ToAdr == BCAppConstants.MTLMTS_Address.MTS2_address))
                {
                    string id = string.Empty;
                    AEQPT mtlmts;
                    if (vhehicle.ToAdr == BCAppConstants.MTLMTS_Address.MTL_address)
                    {
                        id = "MTL";
                        mtlmts = app.ObjCacheManager.GetMTLMTSByID(id);
                        return (mtlmts as MaintainLift).CurrentPreCarOurDistance.ToString(); 
                    }
                    else if (vhehicle.ToAdr == BCAppConstants.MTLMTS_Address.MTS_address)
                    {
                        id = "MTS";
                        mtlmts = app.ObjCacheManager.GetMTLMTSByID(id);
                        return (mtlmts as MaintainSpace).CurrentPreCarOurDistance.ToString();
                    }
                    else if (vhehicle.ToAdr == BCAppConstants.MTLMTS_Address.MTS2_address)
                    {
                        id = "MTS2";
                        mtlmts = app.ObjCacheManager.GetMTLMTSByID(id);
                        return (mtlmts as MaintainSpace).CurrentPreCarOurDistance.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
                else
                {
                    return "-";
                }
            }
        }
        public string syschronizeTime
        {
            get
            {
                if (vhehicle.UPD_TIME != null)
                {
                    return ((DateTime)vhehicle.UPD_TIME).ToString("yyyy/MM/dd hh:mm:ss");
                }
                else
                {
                    return "-";
                }

            }
        }

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
