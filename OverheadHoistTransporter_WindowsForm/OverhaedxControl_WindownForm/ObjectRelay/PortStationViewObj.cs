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
    public class PortStationViewObj : INotifyPropertyChanged
    {
        APORTSTATION port_station = null;
        List<VACMD_MCS> aCMD_MCs = null;
        public PortStationViewObj(APORTSTATION myDatabaseObject, List<VACMD_MCS> cmds)
        {
            this.port_station = myDatabaseObject;
            this.aCMD_MCs = cmds;
        }

        public int PORT_TYPE
        {
            get { return port_station.PORT_TYPE; }
        }

        public string PORT_ID
        {
            get { return port_station.PORT_ID; }
        }

        public string CST_ID
        {
            get
            {
                VACMD_MCS aCMD_MCS = aCMD_MCs.Where(cmd => cmd.HOSTSOURCE.Trim() == port_station.PORT_ID.Trim()&&cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue&&cmd.TRANSFERSTATE<E_TRAN_STATUS.Transferring).FirstOrDefault();
                return aCMD_MCS == null ? "" : aCMD_MCS.CARRIER_ID.Trim();
            }
        }

        public string ADR_ID
        {
            get { return port_station.ADR_ID; }
        }

        public int PRIORITY
        {
            get { return port_station.PRIORITY; }
        }

        public E_PORT_STATUS PORT_STATUS
        {
            get { return port_station.PORT_STATUS; }
        }
        public string ALARM_STATUS
        {
            get
            {
                switch (port_station.PORT_STATUS)
                {
                    case E_PORT_STATUS.OutOfService:
                        return "Down";
                    default:
                        return "-";
                }
            }
        }

        public enum Status
        {
            Enable,
            Disable
        }
        public Status STATUS
        {
            get
            {
                switch (port_station.PORT_SERVICE_STATUS)
                {
                    case (int)PortStationServiceStatus.InService:
                        return Status.Enable;
                    case (int)PortStationServiceStatus.OutOfService:
                        return Status.Disable;
                    default:
                        return Status.Disable;
                }

            }
        }

        public DateTime UP_DATE_TIME = DateTime.Now;
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
