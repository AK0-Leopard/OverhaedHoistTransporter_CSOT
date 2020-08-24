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
    public class TarnferCMDViewObj : INotifyPropertyChanged
    {
        VACMD_MCS cmd;
        public TarnferCMDViewObj(VACMD_MCS cmd)
        {
            this.cmd = cmd;
        }

        public string CMD_ID
        {
            get { return cmd.CMD_ID.Trim(); }
        }

        public E_TRAN_STATUS TRANSFERSTATE
        {
            get { return cmd.TRANSFERSTATE; }
        }

        public string CARRIER_ID
        {
            get { return cmd.CARRIER_ID.Trim(); }
        }

        public string HOSTSOURCE
        {
            get { return cmd.HOSTSOURCE.Trim(); }
        }
        public string HOSTDESTINATION
        {
            get { return cmd.HOSTDESTINATION.Trim(); }
        }
        public int? PRIORITY_SUM
        {
            get
            {
                return cmd.PRIORITY_SUM;
                //if (cmd.PRIORITY_SUM.HasValue)
                //return cmd.PRIORITY_SUM.Value;
                //else
                //    return 0;
            }
        }
        public int PRIORITY
        {
            get { return cmd.PRIORITY; }
        }
        public int PORT_PRIORITY
        {
            get { return cmd.PORT_PRIORITY; }
        }
        public int TIME_PRIORITY
        {
            get { return cmd.TIME_PRIORITY; }
        }

        public DateTime CMD_INSER_TIME
        {
            get { return cmd.CMD_INSER_TIME; }
        }
        public DateTime? CMD_START_TIME
        {
            get { return cmd.CMD_START_TIME; }
        }
        public DateTime? CMD_FINISH_TIME
        {
            get { return cmd.CMD_FINISH_TIME; }
        }
        public string OHTC_CMD
        {
            get { return cmd.OHTC_CMD?.Trim(); }
        }
        public string VH_ID
        {
            get { return cmd.VH_ID?.Trim(); }
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
