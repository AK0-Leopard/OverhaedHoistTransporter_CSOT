using com.mirle.ibg3k0.sc.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc
{
    public partial class ACMD_MCS
    {
        public static ConcurrentDictionary<string, ACMD_MCS> MCS_CMD_InfoList { get; private set; } = new ConcurrentDictionary<string, ACMD_MCS>();
        public static (bool isSuccess, ACMD_MCS tranCmd) tryMCS_CMDByID(string id)
        {

            bool is_get = MCS_CMD_InfoList.TryGetValue(SCUtility.Trim(id, true), out var tran);
            return (is_get, tran);
        }


        public const string COMMAND_PAUSE_FLAG_EMPTY = "";
        public const string COMMAND_PAUSE_FLAG_COMMAND_SHIFT = "S";
        public const string COMMAND_PAUSE_FLAG_COMMAND_INTERRUPT_THEN_TO_QUEUE = "I";
        /// <summary>
        /// 1 2 4 8 16 32 64 128
        /// 1 1 1 1 1  1  1  1
        /// 1 0 0 0 ...
        /// 1 1 0 0 ....
        /// 1 1 1 0 ....
        /// </summary>
        public const int COMMAND_STATUS_BIT_INDEX_ENROUTE = 1;
        public const int COMMAND_STATUS_BIT_INDEX_LOAD_ARRIVE = 2;
        public const int COMMAND_STATUS_BIT_INDEX_LOADING = 4;
        public const int COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE = 8;
        public const int COMMAND_STATUS_BIT_INDEX_UNLOAD_ARRIVE = 16;
        public const int COMMAND_STATUS_BIT_INDEX_UNLOADING = 32;
        public const int COMMAND_STATUS_BIT_INDEX_UNLOAD_COMPLETE = 64;
        public const int COMMAND_STATUS_BIT_INDEX_COMMNAD_FINISH = 128;

        public const int COMMAND_STATUS_BIT_CARRIER_UNKNOW_LOCATION = 256;

        public string CanNotServiceReason = "";
        public static string COMMAND_STATUS_BIT_To_String(int commandStatus)
        {
            switch (commandStatus)
            {
                case COMMAND_STATUS_BIT_INDEX_ENROUTE:
                    return "Enroute";
                case COMMAND_STATUS_BIT_INDEX_LOAD_ARRIVE:
                    return "Load arrive";
                case COMMAND_STATUS_BIT_INDEX_LOADING:
                    return "Loading";
                case COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE:
                    return "Load complete";
                case COMMAND_STATUS_BIT_INDEX_UNLOAD_ARRIVE:
                    return "Unload arrive";
                case COMMAND_STATUS_BIT_INDEX_UNLOADING:
                    return "Unloading";
                case COMMAND_STATUS_BIT_INDEX_UNLOAD_COMPLETE:
                    return "Unload complete";
                case COMMAND_STATUS_BIT_INDEX_COMMNAD_FINISH:
                    return "Command finish";
            }
            return "";
        }

        const string KEY_WORD_STK = "STK";
        public bool IsCVPort_LoadPort()
        {
            return HOSTSOURCE.Contains(KEY_WORD_STK);
        }
        public string getLoadPortSegment(BLL.PortStationBLL portStationBLL, BLL.SectionBLL sectionBLL)
        {
            APORTSTATION port_station = portStationBLL.OperateCatch.getPortStation(HOSTSOURCE);
            if (port_station == null) return "";
            var sections = sectionBLL.cache.GetSectionsByAddress(port_station.ADR_ID);
            if (sections == null || sections.Count == 0) return "";
            var one_part_section = sections[0];
            return one_part_section.SEG_NUM;
        }

        public bool IsCVPort_UnloadPort()
        {
            return HOSTDESTINATION.Contains(KEY_WORD_STK);
        }
        public string getUnloadPortSegment(BLL.PortStationBLL portStationBLL, BLL.SectionBLL sectionBLL)
        {
            APORTSTATION port_station = portStationBLL.OperateCatch.getPortStation(HOSTDESTINATION);
            if (port_station == null) return "";
            var sections = sectionBLL.cache.GetSectionsByAddress(port_station.ADR_ID);
            if (sections == null || sections.Count == 0) return "";
            var one_part_section = sections[0];
            return one_part_section.SEG_NUM;
        }

        public string getSourceAdrID(BLL.PortStationBLL portStationBLL)
        {
            string adr_id = portStationBLL.OperateCatch.getPortStationAdr(HOSTSOURCE);
            return adr_id;
        }
        public ACMD_OHTC getExcuteCMD_OHTC(BLL.CMDBLL cmdBLL)
        {
            var excuting_ohtc_cmd = cmdBLL.getCMD_OHTCByMCSID(CMD_ID);
            return excuting_ohtc_cmd;
        }


        public HCMD_MCS ToHCMD_MCS()
        {
            return new HCMD_MCS()
            {
                CMD_ID = this.CMD_ID,
                CARRIER_ID = this.CARRIER_ID,
                TRANSFERSTATE = this.TRANSFERSTATE,
                COMMANDSTATE = this.COMMANDSTATE,
                HOSTSOURCE = this.HOSTSOURCE,
                HOSTDESTINATION = this.HOSTDESTINATION,
                PRIORITY = this.PRIORITY,
                CHECKCODE = this.CHECKCODE,
                PAUSEFLAG = this.PAUSEFLAG,
                CMD_INSER_TIME = this.CMD_INSER_TIME,
                CMD_START_TIME = this.CMD_START_TIME,
                CMD_FINISH_TIME = this.CMD_FINISH_TIME,
                TIME_PRIORITY = this.TIME_PRIORITY,
                PORT_PRIORITY = this.PORT_PRIORITY,
                PRIORITY_SUM = this.PRIORITY_SUM,
                REPLACE = this.REPLACE,
            };
        }
        public string DestPortGroupID { get; set; } = "";
        public void setDestPortGroupID(BLL.PortStationBLL portStationBLL)
        {
            var get_group_id_result = portStationBLL.OperateCatch.tryGetPortGroupID(HOSTDESTINATION);
            if (get_group_id_result.isExist)
            {
                DestPortGroupID = get_group_id_result.portGroupID;
            }
            else
            {
                //not thing....
            }
        }

        public (bool isExist, string portGroupID) tryGetDestPortGroupID(BLL.PortStationBLL portStationBLL)
        {
            if (!sc.Common.SCUtility.isEmpty(DestPortGroupID))
            {
                return (true, DestPortGroupID);
            }
            else
            {
                return (false, "");
            }

        }

        public bool put(ACMD_MCS ortherObj)
        {
            bool has_change = false;
            if (!sc.Common.SCUtility.isMatche(CMD_ID, ortherObj.CMD_ID))
            {
                CMD_ID = ortherObj.CMD_ID;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(CARRIER_ID, ortherObj.CARRIER_ID))
            {
                CARRIER_ID = ortherObj.CARRIER_ID;
                has_change = true;
            }
            if (TRANSFERSTATE != ortherObj.TRANSFERSTATE)
            {
                TRANSFERSTATE = ortherObj.TRANSFERSTATE;
                has_change = true;
            }
            if (COMMANDSTATE != ortherObj.COMMANDSTATE)
            {
                COMMANDSTATE = ortherObj.COMMANDSTATE;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(HOSTSOURCE, ortherObj.HOSTSOURCE))
            {
                HOSTSOURCE = ortherObj.HOSTSOURCE;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(HOSTDESTINATION, ortherObj.HOSTDESTINATION))
            {
                HOSTDESTINATION = ortherObj.HOSTDESTINATION;
                has_change = true;
            }
            if (PRIORITY != ortherObj.PRIORITY)
            {
                PRIORITY = ortherObj.PRIORITY;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(HOSTDESTINATION, ortherObj.HOSTDESTINATION))
            {
                HOSTDESTINATION = ortherObj.HOSTDESTINATION;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(HOSTDESTINATION, ortherObj.HOSTDESTINATION))
            {
                HOSTDESTINATION = ortherObj.HOSTDESTINATION;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(HOSTDESTINATION, ortherObj.HOSTDESTINATION))
            {
                HOSTDESTINATION = ortherObj.HOSTDESTINATION;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(CHECKCODE, ortherObj.CHECKCODE))
            {
                CHECKCODE = ortherObj.CHECKCODE;
                has_change = true;
            }
            if (!sc.Common.SCUtility.isMatche(PAUSEFLAG, ortherObj.PAUSEFLAG))
            {
                PAUSEFLAG = ortherObj.PAUSEFLAG;
                has_change = true;
            }
            if (CMD_INSER_TIME != ortherObj.CMD_INSER_TIME)
            {
                CMD_INSER_TIME = ortherObj.CMD_INSER_TIME;
                has_change = true;
            }
            if (CMD_START_TIME != ortherObj.CMD_START_TIME)
            {
                CMD_START_TIME = ortherObj.CMD_START_TIME;
                has_change = true;
            }
            if (CMD_FINISH_TIME != ortherObj.CMD_FINISH_TIME)
            {
                CMD_FINISH_TIME = ortherObj.CMD_FINISH_TIME;
                has_change = true;
            }
            if (TIME_PRIORITY != ortherObj.TIME_PRIORITY)
            {
                TIME_PRIORITY = ortherObj.TIME_PRIORITY;
                has_change = true;
            }
            if (PORT_PRIORITY != ortherObj.PORT_PRIORITY)
            {
                PORT_PRIORITY = ortherObj.PORT_PRIORITY;
                has_change = true;
            }
            if (PRIORITY_SUM != ortherObj.PRIORITY_SUM)
            {
                PRIORITY_SUM = ortherObj.PRIORITY_SUM;
                has_change = true;
            }
            if (REPLACE != ortherObj.REPLACE)
            {
                REPLACE = ortherObj.REPLACE;
                has_change = true;
            }
            if (DestPortGroupID != ortherObj.DestPortGroupID)
            {
                DestPortGroupID = ortherObj.DestPortGroupID;
                has_change = true;
            }
            return has_change;
        }

        public bool setCanNotServiceReason(string reason)
        {
            if (!sc.Common.SCUtility.isMatche(CanNotServiceReason, reason))
            {
                CanNotServiceReason = reason;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string cmd_id = sc.Common.SCUtility.Trim(CMD_ID, true);
            string source = sc.Common.SCUtility.Trim(HOSTSOURCE, true);
            string dest = sc.Common.SCUtility.Trim(HOSTDESTINATION, true);
            string inser_time = CMD_INSER_TIME.ToString(sc.App.SCAppConstants.DateTimeFormat_23);
            string prioirty = PRIORITY.ToString();
            string time_prioirty = TIME_PRIORITY.ToString();
            string port_prioirty_time = PORT_PRIORITY.ToString();
            string prioirty_sum = PRIORITY_SUM.ToString();
            return $"id:{cmd_id},l:{source},u:{dest},inser_time:{inser_time},priority:{prioirty},time_prioirty:{time_prioirty},port_prioirty:{port_prioirty_time},sum_prioirty:{prioirty_sum}";
        }
    }
}
