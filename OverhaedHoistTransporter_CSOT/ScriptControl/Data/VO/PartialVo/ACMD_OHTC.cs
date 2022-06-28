using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc
{
    public partial class ACMD_OHTC
    {
        public static ConcurrentDictionary<string, ACMD_OHTC> CMD_OHTC_InfoList { get; private set; } = new ConcurrentDictionary<string, ACMD_OHTC>();
        public static void tryAddCMD_OHTC_ToList(ACMD_OHTC cmdOHTC)
        {
            string cmd_id = sc.Common.SCUtility.Trim(cmdOHTC.CMD_ID, true);
            CMD_OHTC_InfoList.TryAdd(cmd_id, cmdOHTC);
        }
        public static void tryRemoveCMD_OHTC_ToList(string cmdID)
        {
            string cmd_id = sc.Common.SCUtility.Trim(cmdID, true);
            CMD_OHTC_InfoList.TryRemove(cmd_id, out ACMD_OHTC cmd_ohtc);
        }

        public static void tryUpdateCMD_OHTCStatus(string cmdID, E_CMD_STATUS status)
        {
            string cmd_id = sc.Common.SCUtility.Trim(cmdID, true);
            bool is_get = CMD_OHTC_InfoList.TryGetValue(cmd_id, out ACMD_OHTC cmd_ohtc);
            if (is_get)
            {
                cmd_ohtc.CMD_STAUS = status;
            }
        }
        public static List<ACMD_OHTC> tryGetCMD_OHTCSList()
        {
            var cmd_ohtc_Key_value_array = CMD_OHTC_InfoList.ToArray();
            var cmd_ohtc_list = cmd_ohtc_Key_value_array.Select(kv => kv.Value).ToList();
            return cmd_ohtc_list;
        }

        public string getSourcePortSegment(BLL.SectionBLL sectionBLL)
        {
            var sections = sectionBLL.cache.GetSectionsByAddress(SOURCE);
            if (sections == null || sections.Count == 0)
            {
                return "";
            }
            var section = sections[0];
            if (section == null)
            {
                return "";
            }
            return sections[0].SEG_NUM;
        }
        public bool IsTransferCmdByMCS
        {
            get { return !Common.SCUtility.isEmpty(CMD_ID_MCS); }
        }

        public HCMD_OHTC ToHCMD_OHTC()
        {
            return new HCMD_OHTC()
            {
                CMD_ID = this.CMD_ID,
                VH_ID = this.VH_ID,
                CARRIER_ID = this.CARRIER_ID,
                CMD_ID_MCS = this.CMD_ID_MCS,
                CMD_TPYE = this.CMD_TPYE,
                SOURCE = this.SOURCE,
                DESTINATION = this.DESTINATION,
                PRIORITY = this.PRIORITY,
                CMD_START_TIME = this.CMD_START_TIME,
                CMD_END_TIME = this.CMD_END_TIME.HasValue ? this.CMD_END_TIME.Value : DateTime.Now,
                CMD_STAUS = this.CMD_STAUS,
                CMD_PROGRESS = this.CMD_PROGRESS,
                INTERRUPTED_REASON = this.INTERRUPTED_REASON,
                ESTIMATED_TIME = this.ESTIMATED_TIME,
                ESTIMATED_EXCESS_TIME = this.ESTIMATED_EXCESS_TIME,
                REAL_CMP_TIME = this.REAL_CMP_TIME,
            };
        }

        public override string ToString()
        {
            return $"command id:{Common.SCUtility.Trim(CMD_ID, true)}, mcs cmd id:{Common.SCUtility.Trim(CMD_ID_MCS, true)}, cmd type:{CMD_TPYE}, source:{Common.SCUtility.Trim(SOURCE, true)}, dest:{Common.SCUtility.Trim(DESTINATION, true)}, status:{CMD_STAUS}," +
                   $" start time:{CMD_START_TIME?.ToString(App.SCAppConstants.DateTimeFormat_19)}, end time:{CMD_END_TIME?.ToString(App.SCAppConstants.DateTimeFormat_19)}";
        }

        internal bool put(ACMD_OHTC current_cmd)
        {
            CMD_ID = current_cmd.CMD_ID;
            VH_ID = current_cmd.VH_ID;
            CARRIER_ID = current_cmd.CARRIER_ID;
            CMD_ID_MCS = current_cmd.CMD_ID_MCS;
            CMD_TPYE = current_cmd.CMD_TPYE;
            SOURCE = current_cmd.SOURCE;
            DESTINATION = current_cmd.DESTINATION;
            PRIORITY = current_cmd.PRIORITY;
            CMD_START_TIME = current_cmd.CMD_START_TIME;
            CMD_END_TIME = current_cmd.CMD_END_TIME.HasValue ? current_cmd.CMD_END_TIME.Value : DateTime.Now;
            CMD_STAUS = current_cmd.CMD_STAUS;
            CMD_PROGRESS = current_cmd.CMD_PROGRESS;
            INTERRUPTED_REASON = current_cmd.INTERRUPTED_REASON;
            ESTIMATED_TIME = current_cmd.ESTIMATED_TIME;
            ESTIMATED_EXCESS_TIME = current_cmd.ESTIMATED_EXCESS_TIME;
            REAL_CMP_TIME = current_cmd.REAL_CMP_TIME;
            return true;
        }
    }
}
