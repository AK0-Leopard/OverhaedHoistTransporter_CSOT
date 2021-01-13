using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc
{
    public partial class ACMD_MCS
    {

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

    }
}
