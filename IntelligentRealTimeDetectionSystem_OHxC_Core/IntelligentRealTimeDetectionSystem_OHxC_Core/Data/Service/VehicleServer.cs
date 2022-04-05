using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.Common;
using com.mirle.iibg3k0.ids.ohxc.Data.BLL;
using com.mirle.iibg3k0.ids.ohxc.PartialObject;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.iibg3k0.ids.ohxc.Data.Service
{
    public class VehicleServer
    {
        VehicleBLL vehicleBLL = null;
        EventBLL eventBLL = null;
        DataObjCacheManager DataObjCacheManger = null;
        NatsManager NatsManager = null;
        public VehicleServer(CSApplication _app)
        {
            vehicleBLL = _app.VehicleBLL;
            eventBLL = _app.EventBLL;
            DataObjCacheManger = _app.DataObjCacheManger;
            NatsManager = _app.NatsManager;
        }

        public void Start()
        {
            vehicleBLL.InitialAllVHAndAddressDistance();

            RegisterEvent();
        }
        public const string NATS_SUBJECT_VH_INFO_0 = "NATS_SUBJECT_KEY_VH_INFO_{0}_TEST";
        private void RegisterEvent()
        {
            foreach (AVEHICLE Vehicle in DataObjCacheManger.VehiclesInfo.Values)
            {
                Vehicle.IndleWarningFlagChanged += Vehicle_IdleWarningChanged;
                Vehicle.IsLoadUnloadTooLongFlagChanged += Vehicle_LoadUnloadTooLongFlagChanged;
                string subject_id = string.Format(NATS_SUBJECT_VH_INFO_0, Vehicle.VEHICLE_ID);
                SubscriberVehicleInfo(subject_id, VehiclePositionChangeHandler);
                System.Threading.Thread.Sleep(50);
            }

        }

        private void VehiclePositionChangeHandler(object sender, StanMsgHandlerArgs e)
        {
            var bytes = e.Message.Data;
            //var vh_obj = ZeroFormatterSerializer.Deserialize<com.mirle.ibg3k0.sc.AVEHICLE>(bytes);
            VEHICLE_INFO vh_info = ibg3k0.sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);

            DataObjCacheManger.VehiclesInfo[vh_info.VEHICLEID].setObject(vh_info);
        }
        public void SubscriberVehicleInfo(string subject, EventHandler<StanMsgHandlerArgs> handler)
        {
            //NatsManager.Subscriber(subject, handler, is_last: true);
            NatsManager.Subscriber(subject, handler);
        }


        private void Vehicle_IdleWarningChanged(object sender, bool isWaring)
        {
            AVEHICLE vh = sender as AVEHICLE;
            List<string> message = new List<string>();

            message.Add(vh.CUR_SEC_ID);
            message.Add(vh.FromTheLastSectionUpdateTimer.ElapsedMilliseconds.ToString());
            message.Add(vh.ACT_STATUS.ToString());
            message.Add(isWaring.ToString());

            eventBLL.PublishEvent
                (CSAppConstants.REDIS_EVENT_CODE_VEHICLE_IDEL_WARNING, vh.VEHICLE_ID, message);
        }
        private void Vehicle_LoadUnloadTooLongFlagChanged(object sender, bool isWaring)
        {
            AVEHICLE vh = sender as AVEHICLE;
            List<string> message = new List<string>();

            message.Add(vh.MCS_CMD?.Trim());
            message.Add(vh.CUR_SEC_ID);
            message.Add(vh.VhRecentTranEvent.ToString());
            message.Add(isWaring.ToString());

            eventBLL.PublishEvent
                (CSAppConstants.REDIS_EVENT_CODE_VEHICLE_LOADUNLOAD_TOO_LONG_WARNING, vh.VEHICLE_ID, message);
        }

        public void UpdateAllVhAndEachAdrDistanceToHashTable()
        {
            foreach (var vh in DataObjCacheManger.VehiclesInfo.Values)
            {
                string vh_id = vh.VEHICLE_ID;
                string crt_adr_id = vh.CUR_ADR_ID;

                vehicleBLL.updateVhAndEachAdrDistanceHashTable(vh_id, crt_adr_id);
            }
        }

    }


}
