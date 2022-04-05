using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.Data.BLL;
using com.mirle.iibg3k0.ids.ohxc.PartialObject;
using StackExchange.Redis;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mirle.iibg3k0.ids.ohxc.Data.Service
{
    public class CheckService : ICheckServer
    {
        CheckBLL checkBLL = null;
        VehicleBLL vehicleBLL = null;
        EventBLL eventBLL = null;
        DataObjCacheManager dataObjCacheManager = null;
        RedisCacheManager redisCacheManager = null;
        const string CHANNEL_NAME_POSITION_REPORT = "POSITION_REPORT_*";
        CSApplication app;


        public CheckService()
        {

        }

        public void Start(CSApplication _app)
        {
            app = _app;
            checkBLL = app.CheckBLL;
            vehicleBLL = app.VehicleBLL;
            eventBLL = _app.EventBLL;
            dataObjCacheManager = app.DataObjCacheManger;
            redisCacheManager = app.RedisCacheManager;
        }



        public void CheckVehiclePosition()
        {
            foreach (var vh in app.DataObjCacheManger.VehiclesInfo.Values)
            {
                string vh_id = vh.VEHICLE_ID;
                string crt_sec_id = vh.CUR_SEC_ID;
                string crt_adr_id = vh.CUR_ADR_ID;
                int vh_sec_distance = (int)vh.ACC_SEC_DIST;

                //1.確認VH所在的Block是否合法
                //ChcekBlockControl(vh, crt_sec_id);

                //2.確認各個VH即將行走的路徑，前方N公尺是否有VH
                ChcekFrontVehicleDistance(vh_id, vh_sec_distance);
            }
        }
        public void ChcekVhStatus()
        {
            foreach (AVEHICLE vh in dataObjCacheManager.VehiclesInfo.Values)
            {
                bool hasWarning = false;

                if (vh.MODE_STATUS != VHModeStatus.AutoRemote ||
                    vh.VhRecentTranEvent == EventType.Vhloading ||
                    vh.VhRecentTranEvent == EventType.Vhunloading) continue;

                if (vh.ACT_STATUS == VHActionStatus.Commanding)
                {
                    if (vh.OHXC_OBS_PAUSE == VhStopSingle.StopSingleOn)
                    {
                        AVEHICLE blocked_ActionVh = null;
                        List<AVEHICLE> blocked_nonActionVhs = new List<AVEHICLE>();
                        checkBLL.SearchFrontVehicle(vh, (int)vh.ACC_SEC_DIST, ref blocked_ActionVh, ref blocked_nonActionVhs);
                        if (blocked_ActionVh == null)
                        {
                            eventBLL.PublishEvent
                            (CSAppConstants.REDIS_EVENT_CODE_NOTICE_OBSTRUCTED_VH_CONTINUE, vh.VEHICLE_ID, "");
                        }
                    }

                    if (!checkBLL.IsNormalPause(vh))
                    {
                        hasWarning = checkBLL.hasIndleTimeWarning(vh.FromTheLastSectionUpdateTimer, CSAppConstants.VH_IDLE_WARNING_TIME_ms);
                    }
                }
                else
                {
                    if (!vh.IS_PARKING)
                    {
                        hasWarning = checkBLL.hasIndleTimeWarning(vh.FromTheLastSectionUpdateTimer, CSAppConstants.VH_IDLE_WARNING_TIME_ms);
                    }
                }
                vh.IsIdleWarning = hasWarning;

                vh.IsLoadUnloadTooLong = checkBLL.hasIndleTimeWarning(vh.LoadUnloadTimer, CSAppConstants.VH_LOADUNLOAD_WARNING_TIME_ms);

                if (vh.ERROR == VhStopSingle.StopSingleOn)
                {
                    List<AVEHICLE> willPassErrorVhOnTheSection = app.VehicleBLL.loadWillPassTheRoadSectionOfVhs(vh.CUR_SEC_ID);
                    if (willPassErrorVhOnTheSection != null && willPassErrorVhOnTheSection.Count > 0)
                    {
                        TimeSpan event_effective_time = new TimeSpan(0, 0, 10);
                        foreach (var vh_obj in willPassErrorVhOnTheSection)
                        {
                            if (vh == vh_obj) continue;
                            eventBLL.PublishEvent
                        (CSAppConstants.REDIS_EVENT_CODE_NOTICE_THE_VH_NEEDS_TO_CHANGE_THE_PATH, vh_obj.VEHICLE_ID, "", event_effective_time);
                        }

                    }

                }

            }
        }

        public void ChcekBlockStatus()
        {

        }

        int mcs_cmd_queue_time_second = 2;
        HashSet<string> QusesTooLongCmd = new HashSet<string>();
        public void ChcekMCSCommandStatus()
        {
            var mcs_cmd_infos = checkBLL.GetAllExcuteMCSCommand();
            if (mcs_cmd_infos == null) return;
            foreach (var mcs_cmd_info in mcs_cmd_infos)
            {
                if (mcs_cmd_info != null)
                {
                    if (!string.IsNullOrWhiteSpace(mcs_cmd_info.VH_ID)) continue;
                    double cmd_queue_time = checkBLL.getWithNowDifferenceSeconds(mcs_cmd_info.CMD_INSERT_TIME);
                    if (cmd_queue_time > 10)
                    {
                        string cmd_id = mcs_cmd_info.CMD_ID_MCS.Trim();
                        if (QusesTooLongCmd.Contains(cmd_id)) continue;

                        // eventBLL.PublishEvent(CSAppConstants.REDIS_EVENT_CODE_EARTHQUAKE_ON, "MCP", true.ToString());
                        Console.WriteLine($"{mcs_cmd_info.CMD_ID_MCS.Trim()},Queue too long");
                        QusesTooLongCmd.Add(cmd_id);
                    }
                }
            }

            Reconfirm_QueueTooLongCmdConllection(mcs_cmd_infos);
        }

        private void Reconfirm_QueueTooLongCmdConllection(ibg3k0.sc.ASYSEXCUTEQUALITY[] current_mcs_cmd_infos)
        {
            string[] current_mcs_cmd = current_mcs_cmd_infos.Select(cmd => cmd.CMD_ID_MCS).ToArray();
            var expectedList = QusesTooLongCmd.ToArray().Except(current_mcs_cmd);
            foreach (string expected_value in expectedList)
            {
                QusesTooLongCmd.Remove(expected_value);
            }
        }

        private void ChcekFrontVehicleDistance(string vh_id, int vh_sec_distance)
        {
            AVEHICLE obstruct_ActionVh = null;
            List<AVEHICLE> obstruct_nonActionVhs = new List<AVEHICLE>();
            AVEHICLE Moving_vh = dataObjCacheManager.GetVehicleInfo(vh_id);
            if (Moving_vh != null &&
                !vehicleBLL.IsPause(Moving_vh) &&
                !string.IsNullOrWhiteSpace(Moving_vh.OHTC_CMD) &&
                Moving_vh.WillPassSectionID != null)
            {
                checkBLL.SearchFrontVehicle(Moving_vh, vh_sec_distance, ref obstruct_ActionVh, ref obstruct_nonActionVhs);
                if (obstruct_ActionVh != null)
                {
                    //obstruct_ActionVh.DistanceChanged -= Moving_vh.ObstacleVh_DistanceChanged;
                    //obstruct_ActionVh.DistanceChanged += Moving_vh.ObstacleVh_DistanceChanged;
                    //eventBLL.PublishEvent(CSAppConstants.REDIS_EVENT_CODE_ADVANCE_NOTICE_OBSTRUCTED_VH, vh_id, obstruct_ActionVh.VEHICLE_ID);
                }
                else if (obstruct_nonActionVhs.Count > 0)
                    eventBLL.PublishEvent
                        (CSAppConstants.REDIS_EVENT_CODE_ADVANCE_NOTICE_OBSTRUCT_VH, vh_id, obstruct_nonActionVhs.Select(vh => vh.VEHICLE_ID).ToList());
            }
        }


        int block_recheck_max_times = 10;
        private void ChcekBlockControl(AVEHICLE vh, string crt_sec_id)
        {
            List<string> entry_secs = null;
            if (checkBLL.CheckCondition_IsInBlockZone(vh.VEHICLE_ID, crt_sec_id, out entry_secs))
            {
                if (!checkBLL.CheckCondition_HasRequestBlockZone(vh.VEHICLE_ID, entry_secs))
                {
                    if (++vh.Block_Recheck_Times > block_recheck_max_times)
                    {
                        //checkBLL.SetEventIllegalBlockZoneFlag(vh_id, entry_secs);
                        eventBLL.PublishEvent(CSAppConstants.REDIS_EVENT_CODE_ILLEGAL_ENTRY_BLOCK_ZONE, vh.VEHICLE_ID, entry_secs);
                    }
                }
            }
            else
            {
                if (vh.Block_Recheck_Times != 0)
                {
                    vh.Block_Recheck_Times = 0;
                }
            }
        }

        const int BLOCK_BLOCKING_TIMEOUT_SECOND = 40;
        const string REDIS_BLOCK_CONTROL_HASHKEY = "CURRENT_BLOCK_CONTROL_INFO";
        const string REDIS_BLOCK_CONTROL_KEY_VHID_BLOCKID = "BLOCKCONTROL_{0}_{1}";
        const string REDIS_BLOCK_CONTROL_VALUE_STATUS_CHANGE_TIME_AND_STATUS = "{0},{1}";
        public void ChcekBlockControlBlockingTimeout()
        {
            var hash_entrys = redisCacheManager.HashGetAll(REDIS_BLOCK_CONTROL_HASHKEY);
            foreach (var hash_entry in hash_entrys)
            {
                var block_info = getBlockInfo(hash_entry);
                if (block_info.blockStatus == ibg3k0.sc.App.SCAppConstants.BlockQueueState.Blocking ||
                   block_info.blockStatus == ibg3k0.sc.App.SCAppConstants.BlockQueueState.Through)
                {
                    if (DateTime.Now > block_info.statusChangeTime.AddSeconds(BLOCK_BLOCKING_TIMEOUT_SECOND))
                    {
                        List<string> notify_info = new List<string>()
                        { block_info.vhID,
                          block_info.blockID,
                          block_info.blockStatus,
                          block_info.statusChangeTime.ToString(ibg3k0.sc.App.SCAppConstants.TimestampFormat_17) };
                        eventBLL.PublishEvent(CSAppConstants.REDIS_EVENT_CODE_NOTICE_BLOCKING_RELEASE_TIMEOUT, block_info.vhID, notify_info);
                        redisCacheManager.HashDelete(REDIS_BLOCK_CONTROL_HASHKEY, hash_entry.Name);
                    }
                }
            }
        }
        private (string vhID, string blockID, string blockStatus, DateTime statusChangeTime) getBlockInfo(HashEntry blockInfo)
        {
            string key = blockInfo.Name;
            string value = blockInfo.Value;

            string[] key_group = key.Split('_');
            string vh_id = key_group[1];
            string block_id = key_group[2];

            string[] value_group = value.Split(',');
            string status_change_time = value_group[0];
            DateTime dt_status_change_time = default(DateTime);
            DateTime.TryParseExact(status_change_time, ibg3k0.sc.App.SCAppConstants.TimestampFormat_17, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt_status_change_time);

            string status = value_group[1];
            return (vh_id, block_id, status, dt_status_change_time);
        }

        public void EarthquakeHappendHandler(bool isHappend)
        {
            eventBLL.PublishEvent(CSAppConstants.REDIS_EVENT_CODE_EARTHQUAKE_ON, "MCP", isHappend.ToString());
        }




    }
}
