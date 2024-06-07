using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.ObjectRelay
{
    public class CMDObjToShow
    {
        public HCMD_MCS cmd_obj = null;
        public CMDObjToShow(HCMD_MCS cmdObj)
        {
            cmd_obj = cmdObj;
        }
        [Description("ID")]
        public string CMD_ID { get { return cmd_obj.CMD_ID; } }
        [Description("Carrier ID")]
        public string CMD_CARRIER { get { return cmd_obj.CARRIER_ID; } }
        [Description("Source")]
        public string CMD_SOURCE { get { return cmd_obj.HOSTSOURCE; } }
        [Description("Destination")]
        public string CMD_DESTINATION { get { return cmd_obj.HOSTDESTINATION; } }
        [Description("Transfer state")]
        public E_TRAN_STATUS CMD_STATE { get { return cmd_obj.TRANSFERSTATE; } }
        [Description("Insert time")]
        public System.DateTime CMD_INSERT_TIME { get { return cmd_obj.CMD_INSER_TIME; } }
        [Description("Start time")]
        public Nullable<System.DateTime> CMD_START_TIME { get { return cmd_obj.CMD_START_TIME; } }
        [Description("End time")]
        public Nullable<System.DateTime> CMD_FINISH_TIME { get { return cmd_obj.CMD_FINISH_TIME; } }
    }
}
