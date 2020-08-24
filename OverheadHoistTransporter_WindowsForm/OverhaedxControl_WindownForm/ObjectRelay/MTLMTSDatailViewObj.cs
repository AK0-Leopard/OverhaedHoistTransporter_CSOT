using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.Data.VO.Interface;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.ObjectRelay
{
    public class MTLMTSDatailViewObj : INotifyPropertyChanged
    {
        AEQPT eqpt;
        public MTLMTSDatailViewObj(AEQPT myDatabaseObject)
        {
            this.eqpt = myDatabaseObject;
        }
        public string stationID
        {
            get { return eqpt.EQPT_ID; }
        }

        public SCAppConstants.LinkStatus Plc_Link_Stat
        {
            get { return eqpt.Plc_Link_Stat; }
        }

        public bool Alive
        {
            get { return eqpt.Is_Eq_Alive; }
        }

        public MTxMode Mode
        {
            get { return eqpt.MTxMode; }
        }

        public bool Interlock
        {
            get { return eqpt.Interlock; }
        }
        public bool CarInInterlock
        {
            get
            {
                if (eqpt is IMaintainDevice)
                {
                    return (eqpt as IMaintainDevice).CarInMoving;
                }
                return false;
            }
        }
        public bool CarOutInterlock
        {
            get
            {
                if (eqpt is IMaintainDevice)
                {
                    return (eqpt as IMaintainDevice).CarOutInterlock;
                }
                return false;
            }
        }


        public string CarID
        {
            get { return eqpt.CurrentCarID; }
        }

        public MTLLocation MTLLocation
        {
            get { return eqpt.MTLLocation; }
        }
        public DateTime SyncronizeTime
        {
            get { return eqpt.SynchronizeTime; }
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
