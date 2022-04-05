using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ids.ohxc.App
{
    public class CSAppConstants
    {
        public const string REDIS_SERVER_CONFIGURATION = "redis.ohxc.mirle.com.tw:6379";
        public const string REDIS_LIST_KEY_VEHICLES = "Redis_List_Vehicles";
        public const string REDIS_BLOCK_CONTROL_KEY_VHID = "BLOCK_CONTROL_{0}";

        public const string REDIS_EVENT_KEY = "REDIS_EVENT_KEY";
        public const string REDIS_EVENT_CODE_ILLEGAL_ENTRY_BLOCK_ZONE = "0001";
        public const string REDIS_EVENT_CODE_ADVANCE_NOTICE_OBSTRUCT_VH = "0002";
        public const string REDIS_EVENT_CODE_VEHICLE_IDEL_WARNING = "0003";
        public const string REDIS_EVENT_CODE_VEHICLE_LOADUNLOAD_TOO_LONG_WARNING = "0004";
        public const string REDIS_EVENT_CODE_EARTHQUAKE_ON = "0005";
        public const string REDIS_EVENT_CODE_ADVANCE_NOTICE_OBSTRUCTED_VH = "0006";
        public const string REDIS_EVENT_CODE_NOTICE_OBSTRUCTED_VH_CONTINUE = "0007";
        public const string REDIS_EVENT_CODE_NOTICE_THE_VH_NEEDS_TO_CHANGE_THE_PATH = "0008";
        public const string REDIS_EVENT_CODE_NOTICE_BLOCKING_RELEASE_TIMEOUT = "0009";


        public const string REDIS_KEY_VH_TO_ADR_DISTANCE = "VH_EACH_ADR_DISTANCE";

        public const string REDIS_KEY_CHECK_SYSTEM_EXIST_FLAG = "CHECK_SYSTEM_EXIST";

        public const string REDIS_KEY_WORD_CURRENT_MASTER = "OHxC CURRENT MASTER";
        public static string REDIS_KEY_WORD_MASTER_TITLE = "MASTER_NAME_{0}";

        public const string REDIS_KEY_WORD_CURRENT_OBSERVER = "OHxC CURRENT OBSERVER";
        public static string REDIS_KEY_WORD_OBSERVER_TITLE = "OBSERVER_NAME_{0}";

        public const int VH_IDLE_WARNING_TIME_ms = 60000;
        public const int VH_LOADUNLOAD_WARNING_TIME_ms = 60000;



        public enum E_SEG_STATUS : int
        {
            Active = 1,
            Inactive = 2,
            Closed = 3
        }
        public enum E_VH_TYPE : int
        {
            None = 0,
            Clean = 1,
            Dirty = 2
        }



    }
}
