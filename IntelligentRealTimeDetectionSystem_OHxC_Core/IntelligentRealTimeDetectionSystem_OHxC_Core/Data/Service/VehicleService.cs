using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.iibg3k0.ids.ohxc.Data.Service
{
    class VehicleService
    {

        private void VehiclePositionChangeHandler(RedisChannel channel, RedisValue value)
        {
            byte[] SerializationPoitionReport = value;
            ID_134_TRANS_EVENT_REP recive_str = trans_event_rep_parser.ParseFrom(SerializationPoitionReport);
            string vh_id = channel.ToString().Split('_')[2];
            string crt_sec_id = recive_str.CurrentSecID.Trim();
            string crt_adr_id = recive_str.CurrentAdrID.Trim();
            int vh_sec_distance = recive_str.SecDistance;

            //0.更新該VH到所有ADR的距離
            checkBLL.updateVhAndEachAdrDistanceHashTable(vh_id, crt_adr_id);

            //1.確認VH所在的Block是否合法
            ChcekBlockControl(vh_id, crt_sec_id);

            //2.確認各個VH即將行走的路徑，前方N公尺是否有VH
            ChcekFrontVehicleDistance(vh_id, vh_sec_distance);
        }
    }
}
