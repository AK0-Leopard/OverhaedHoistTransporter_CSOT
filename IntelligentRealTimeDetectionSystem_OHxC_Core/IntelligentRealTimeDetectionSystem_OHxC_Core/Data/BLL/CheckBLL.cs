using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.PartialObject;
using RouteKit;
using StackExchange.Redis;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace com.mirle.iibg3k0.ids.ohxc.Data.BLL
{
    public class CheckBLL
    {
        DataObjCacheManager dataObjCacheManager = null;
        RedisCacheManager redisCacheManager = null;
        private CSApplication app = null;
        Guide guide = null;

        public CheckBLL()
        {

        }
        public void Start(CSApplication _app)
        {
            app = _app;
            dataObjCacheManager = app.DataObjCacheManger;
            redisCacheManager = app.RedisCacheManager;
            guide = app.Guide;
        }

        public void SubscriberVehicleInfo(string subject, EventHandler<StanMsgHandlerArgs> handler)
        {
            // CSApplication.getInstance().NatsManager.Subscriber(subject, handler, is_last: true);
            CSApplication.getInstance().NatsManager.Subscriber(subject, handler);
            //app.GetNatsManager().Subscriber(subject, handler);
        }

        #region Check Is Active
        public void CheckCanBeCurrentObserver()
        {
            if (CanActive())
            {
                app.AsCurrentObserver();
            }
            else
            {
                app.RetiredCurrentObserver();
            }
        }

        string redlock_resource_lock = "redlock_key";
        TimeSpan timeOut_10Sec = new TimeSpan(0, 0, 10);
        public bool CanActive()
        {
            try
            {
                //1.確認Current host是不是自己
                //string current_ohxc_id = redisCacheManager.StringGet(CSAppConstants.REDIS_KEY_WORD_CURRENT_MASTER);
                //if (!string.IsNullOrWhiteSpace(current_ohxc_id) && CSApplication.ServerName == current_ohxc_id.Trim())
                //{
                //    return false;
                //}
                //  b.否:回傳false，並確認目前的Master是不是存在,如果不存在的話將自己設置為Mater
                //else
                //{
                using (var redLock = redisCacheManager.RedLockFactory.CreateLock(redlock_resource_lock, timeOut_10Sec))
                {
                    if (redLock.IsAcquired)
                    {
                        string current_observer = redisCacheManager.StringGet(CSAppConstants.REDIS_KEY_WORD_CURRENT_OBSERVER);
                        if (!string.IsNullOrWhiteSpace(current_observer) && CSApplication.ServerName == current_observer.Trim())
                        {
                            SetObserverHeartbeat();
                            return true;
                        }
                        else
                        {
                            string current_observer_name = string.Format(CSAppConstants.REDIS_KEY_WORD_OBSERVER_TITLE, current_observer);
                            if (redisCacheManager.KeyExists(current_observer_name))
                            {
                                return false;
                            }
                            else
                            {
                                Set2OHxCObserver();
                                SetObserverHeartbeat();
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return app.IsCurrentObserver;
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                return app.IsCurrentObserver;
            }
        }

        private void Set2OHxCObserver()
        {
            redisCacheManager.stringSetAsync(CSAppConstants.REDIS_KEY_WORD_CURRENT_OBSERVER
                                           , CSApplication.ServerName);
        }

        private void SetObserverHeartbeat()
        {
            string current_observer_name = string.Format(CSAppConstants.REDIS_KEY_WORD_OBSERVER_TITLE, CSApplication.ServerName);
            redisCacheManager.stringSetAsync(current_observer_name
                                           , string.Empty
                                           , timeOut_10Sec);
        }


        #endregion Check Is Active


        #region Block Control Check
        public bool CheckCondition_IsInBlockZone(string vh_id, string crt_sec_id, out List<string> entry_secs)
        {
            bool isIn = false;
            if (dataObjCacheManager.BlockZoneDetails.ContainsKey(crt_sec_id))
            {
                isIn = true;
                entry_secs = dataObjCacheManager.BlockZoneDetails[crt_sec_id];
            }
            else
            {
                entry_secs = null;
            }
            return isIn;
        }

        public bool CheckCondition_HasRequestBlockZone(string vh_id, List<string> entry_secs)
        {
            bool hasReq = false;
            string blockInfo = redisCacheManager.StringGet(string.Format(CSAppConstants.REDIS_BLOCK_CONTROL_KEY_VHID, vh_id));

            if (string.IsNullOrWhiteSpace(blockInfo))
            {
                //getCurrentRequestBlockID(vh_id);
                hasReq = false;
            }
            else
            {
                string[] blockInfos = ((string)blockInfo).Split(',');
                string sec_id = blockInfos[0];
                string status = blockInfos[1];
                hasReq = true;
                //hasReq = entry_secs.Contains(sec_id);
            }
            return hasReq;
        }

        private void getCurrentRequestBlockID(string vh_id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Vh ID:{vh_id}");
            IEnumerable<RedisKey> keys = redisCacheManager.KeysFromServer(string.Format(CSAppConstants.REDIS_BLOCK_CONTROL_KEY_VHID, "*"));
            foreach (var key in keys)
            {
                sb.Append(key.ToString()).AppendLine(",");
            }
            Console.WriteLine((sb.ToString()));
        }

        //REDIS_EVENT_ILLEGAL_ENTRY_BLOCK_ZONE
        //public void SetEventIllegalBlockZoneFlag(string vh_id, List<string> entry_sec_ids)
        //{
        //    string event_key = CSAppConstants.REDIS_EVENT_KEY;
        //    string time = DateTime.Now.ToLongTimeString();
        //    string section_id = string.Join("-", entry_sec_ids);

        //    string event_value = $"{vh_id},{CSAppConstants.REDIS_EVENT_CODE_ILLEGAL_ENTRY_BLOCK_ZONE},{time},{section_id}";

        //    redisCacheManager.ListRightPushAsync(event_key, event_value);
        //    redisCacheManager.PublishEvent(event_key, event_value);
        //}
        #endregion Block Control Check

        #region Advance_Notice_Blocked_Vh

        public void SearchFrontVehicle(AVEHICLE moving_vh, int vh_sec_distance, ref AVEHICLE blocked_ActionVh, ref List<AVEHICLE> blocked_nonActionVhs)
        {
            double safe_dis = 12000;
            List<string> will_pass_sec_id = moving_vh.WillPassSectionID;
            int index = 0;
            foreach (string sec_id in will_pass_sec_id)
            {
                double sec_dis = dataObjCacheManager.GetSectionDistance(sec_id);

                List<AVEHICLE> on_sec_vhs = dataObjCacheManager.GetOnSectionVehicle(sec_id);
                if (index == 0)
                {
                    double remaining_distance = sec_dis - vh_sec_distance;
                    if (on_sec_vhs == null) continue;
                    foreach (AVEHICLE on_sec_vh in on_sec_vhs)
                    {
                        if (on_sec_vh.VEHICLE_ID.Trim() == moving_vh.VEHICLE_ID.Trim()) continue;

                        if (vh_sec_distance < on_sec_vh.ACC_SEC_DIST && on_sec_vh.ACC_SEC_DIST < vh_sec_distance + safe_dis)
                        {
                            if (moving_vh.OHXC_OBS_PAUSE != VhStopSingle.StopSingleOn &&
                                moving_vh.PauseStatus != VhStopSingle.StopSingleOn &&
                                isActionObstacle(on_sec_vh))
                            {
                                blocked_ActionVh = on_sec_vh;
                                break;
                            }
                            else
                            {
                                blocked_nonActionVhs.Add(on_sec_vh);
                            }
                        }
                    }
                    safe_dis = safe_dis - remaining_distance;
                }
                else
                {
                    if (sec_dis > safe_dis)
                    {
                        if (on_sec_vhs == null) continue;
                        foreach (AVEHICLE on_sec_vh in on_sec_vhs)
                        {
                            if (on_sec_vh.VEHICLE_ID.Trim() == moving_vh.VEHICLE_ID.Trim()) continue;

                            if (on_sec_vh.ACC_SEC_DIST < safe_dis)
                            {
                                if (isActionObstacle(on_sec_vh))
                                {
                                    blocked_ActionVh = on_sec_vh;
                                    break;
                                }
                                else
                                {
                                    blocked_nonActionVhs.Add(on_sec_vh);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (on_sec_vhs == null) continue;
                        foreach (AVEHICLE on_sec_vh in on_sec_vhs)
                        {
                            if (on_sec_vh.VEHICLE_ID.Trim() == moving_vh.VEHICLE_ID.Trim()) continue;

                            if (isActionObstacle(on_sec_vh))
                            {
                                blocked_ActionVh = on_sec_vh;
                                break;
                            }
                            else
                            {
                                blocked_nonActionVhs.Add(on_sec_vh);
                            }
                        }
                    }
                    safe_dis = safe_dis - sec_dis;
                }
                if (blocked_ActionVh != null || safe_dis < 0) break;
                index++;
            }
        }
        private bool isActionObstacle(AVEHICLE vh)
        {
            return vh.ACT_STATUS == VHActionStatus.Commanding &&
                (
                vh.VhRecentTranEvent == EventType.Vhloading ||
                vh.VhRecentTranEvent == EventType.Vhunloading ||
                vh.OBS_PAUSE == VhStopSingle.StopSingleOn ||
                vh.CMD_PAUSE == VhStopSingle.StopSingleOn ||
                vh.BLOCK_PAUSE == VhStopSingle.StopSingleOn ||
                vh.HID_PAUSE == VhStopSingle.StopSingleOn ||
                vh.ERROR == VhStopSingle.StopSingleOn
                );
        }

        //public void SetEventAdvanceNoticeObstructVh(string blocked_vh_id, List<string> block_vhs_id)
        //{
        //    string event_key = CSAppConstants.REDIS_EVENT_KEY;
        //    string time = DateTime.Now.ToLongTimeString();
        //    string sblocked_vhs = string.Join("-", block_vhs_id);

        //    string event_value = $"{blocked_vh_id},{CSAppConstants.REDIS_EVENT_CODE_ADVANCE_NOTICE_OBSTRUCT_VH},{time},{sblocked_vhs}";

        //    redisCacheManager.ListRightPushAsync(event_key, event_value);
        //    redisCacheManager.PublishEvent(event_key, event_value);
        //}

        //public void SetEventAdvanceNoticeObstructedVh(string blocked_vh_id, string block_vhs_id)
        //{
        //    string event_key = CSAppConstants.REDIS_EVENT_KEY;
        //    string time = DateTime.Now.ToLongTimeString();

        //    string event_value = $"{blocked_vh_id},{CSAppConstants.REDIS_EVENT_CODE_ADVANCE_NOTICE_OBSTRUCTED_VH},{time},{block_vhs_id}";

        //    redisCacheManager.ListRightPushAsync(event_key, event_value);
        //    redisCacheManager.PublishEvent(event_key, event_value);
        //}
        //public void SetEventNoticeObstructedVhToContinue(string blocked_vh_id)
        //{
        //    string event_key = CSAppConstants.REDIS_EVENT_KEY;
        //    string time = DateTime.Now.ToLongTimeString();

        //    string event_value = $"{blocked_vh_id},{CSAppConstants.REDIS_EVENT_CODE_NOTICE_OBSTRUCTED_VH_CONTINUE},{time},";

        //    redisCacheManager.ListRightPushAsync(event_key, event_value);
        //    redisCacheManager.PublishEvent(event_key, event_value);
        //}

        #endregion Advance_Notice_Blocked_Vh

        #region Check Vehicle Status        

        public bool IsNormalPause(AVEHICLE vh)
        {
            return vh.OBS_PAUSE == VhStopSingle.StopSingleOn ||
                vh.BLOCK_PAUSE == VhStopSingle.StopSingleOn ||
                vh.CMD_PAUSE == VhStopSingle.StopSingleOn;
        }

        public bool hasIndleTimeWarning(Stopwatch from_last_time, int idle_warning_time_ms)
        {
            bool hasWarning = false;
            if (from_last_time.ElapsedMilliseconds > idle_warning_time_ms)
            {
                hasWarning = true;
            }
            return hasWarning;
        }
        #endregion Check Vehicle Status

        #region Check Block Status        
        #endregion Check Block Status


        #region Check MCS Command
        public ibg3k0.sc.ASYSEXCUTEQUALITY[] GetAllExcuteMCSCommand()
        {
            ibg3k0.sc.ASYSEXCUTEQUALITY[] aSYSEXCUTEQUALITies = null;
            IEnumerable<RedisKey> keys = redisCacheManager.KeysFromServer("201808*");
            RedisValue[] redisValues = redisCacheManager.StringGet(keys.ToArray());
            if (redisValues != null)
            {
                aSYSEXCUTEQUALITies = new ibg3k0.sc.ASYSEXCUTEQUALITY[redisValues.Length];
                for (int i = 0; i < redisValues.Length; i++)
                {
                    aSYSEXCUTEQUALITies[i] = ibg3k0.sc.Common.SCUtility.ToObject(redisValues[i]) as ibg3k0.sc.ASYSEXCUTEQUALITY;
                }
            }
            return aSYSEXCUTEQUALITies;
        }

        public double getWithNowDifferenceSeconds(DateTime? dateTime)
        {
            double diffSec = 0;
            if (dateTime.HasValue)
            {
                diffSec = DateTime.Now.Subtract(dateTime.Value).TotalSeconds;

                diffSec = Math.Round(diffSec, 1);
            }
            return diffSec;
        }


        #endregion Check MCS Command

        //public void SetEvent2Redis(string event_code, string vh_id, List<string> message)
        //{
        //    string event_key = CSAppConstants.REDIS_EVENT_KEY;
        //    string time = DateTime.Now.ToLongTimeString();
        //    string smessage = string.Join("-", message);

        //    string event_value = $"{vh_id},{event_code},{time},{smessage}";

        //    redisCacheManager.ListRightPushAsync(event_key, event_value);
        //    redisCacheManager.PublishEvent(event_key, event_value);
        //}


    }
}