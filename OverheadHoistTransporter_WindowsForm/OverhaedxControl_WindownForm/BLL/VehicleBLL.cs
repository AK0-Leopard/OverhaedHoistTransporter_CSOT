using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data;
using STAN.Client;
using System;
using System.Collections.Generic;
using com.mirle.ibg3k0.sc.Data.DAO;
using System.Text;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System.IO;
using System.IO.Compression;
using NLog;
using System.Linq;
using com.mirle.ibg3k0.ohxc.winform.Schedule;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class VehicleBLL
    {

        const string WEB_API_ACTION_VIEWERUPDATE = "ViewerUpdate";
        private static Logger logger = LogManager.GetLogger("OperationLogger");
        WindownApplication app = null;
        VehicleDao vehicleDAO = null;
        public Cache cache { get; private set; }
        public VehicleBLL(WindownApplication _app)
        {
            app = _app;
            cache = new Cache(app.ObjCacheManager);
            vehicleDAO = app.VehicleDao;
        }

        public void SubscriberVehicleInfo(string subject, EventHandler<StanMsgHandlerArgs> handler)
        {
            app.GetNatsManager().Subscriber(subject, handler, is_last: true);
            //app.GetNatsManager().Subscriber(subject, handler);
        }


        public void ProcVehicleInfo(object sender, StanMsgHandlerArgs handler)
        {

            var bytes = handler.Message.Data;
            //var workItem = new BackgroundWorkItem
            //    (app, bytes);
            //app.BackgroundWork_ProcessVhPositionUpdate.triggerBackgroundWork("ProcVehicleInfo", workItem);

            sc.ProtocolFormat.OHTMessage.VEHICLE_INFO vh_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);

            app.ObjCacheManager.PutVehicle(vh_info);
        }

        public void ProcVehicleInfo(sc.ProtocolFormat.OHTMessage.VEHICLE_INFO vh_obj)
        {
            app.ObjCacheManager.PutVehicle(vh_obj);
        }

        public void filterVh(ref List<AVEHICLE> vhs, E_VH_TYPE vh_type)
        {
            if (vh_type != E_VH_TYPE.None)
            {
                foreach (AVEHICLE vh in vhs.ToList())
                {
                    if (vh.VEHICLE_TYPE != E_VH_TYPE.None
                        && vh.VEHICLE_TYPE != vh_type)
                    {
                        vhs.Remove(vh);
                    }
                }
            }

            foreach (AVEHICLE vh in vhs.ToList())
            {
                if (vh.IsError)
                {
                    vhs.Remove(vh);
                }
            }
            foreach (AVEHICLE vh in vhs.ToList())
            {
                //if (!SCUtility.isEmpty(vh.OHTC_CMD))
                if (!sc.Common.SCUtility.isEmpty(vh.MCS_CMD))
                {
                    vhs.Remove(vh);
                }
            }
            foreach (AVEHICLE vh in vhs.ToList())
            {
                if (vh.HAS_CST == 1)
                {
                    vhs.Remove(vh);
                }
            }
            foreach (AVEHICLE vh in vhs.ToList())
            {
                if (vh.MODE_STATUS != VHModeStatus.AutoRemote)
                {
                    vhs.Remove(vh);
                }
            }
            foreach (AVEHICLE vh in vhs.ToList())
            {
                if (sc.Common.SCUtility.isEmpty(vh.CUR_ADR_ID))
                {
                    vhs.Remove(vh);
                }
            }
            foreach (AVEHICLE vh in vhs.ToList())
            {
                if (app.CmdBLL.isCMD_OHTCQueueByVh(vh.VEHICLE_ID))
                {
                    vhs.Remove(vh);
                }
            }
        }


        public void ReguestViewerUpdate()
        {
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "ViewerUpdate"
            };
            string post_data = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(post_data);
            app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
        }

        public bool SendVehicleResetToControl(string vh_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "SendReset",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendVehicleCMDCancelAbortToControl(string vh_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "SendCancelAbort",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog: TransferManagement/SendCancelAbort");
            return result == "OK";
        }

        public bool SendCmdToControl(string vh_id, E_CMD_TYPE cmd_type, string carrier_id, string from_port_id, string to_port_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "SendCommand",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            sb.Append($"{nameof(cmd_type)}={cmd_type.ToString()}").Append("&");
            sb.Append($"{nameof(carrier_id)}={carrier_id}").Append("&");
            sb.Append($"{nameof(from_port_id)}={from_port_id}").Append("&");
            sb.Append($"{nameof(to_port_id)}={to_port_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog:SendCmdToControl- AVEHICLES/SendCommand");
            return result == "OK";
        }

        public bool SendCmdToControl(string vh_id, E_CMD_TYPE cmd_type, string from_port_id = "", string to_port_id = "")
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "SendCommand",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            sb.Append($"{nameof(cmd_type)}={cmd_type.ToString()}").Append("&");
            sb.Append($"{nameof(from_port_id)}={from_port_id}").Append("&");
            sb.Append($"{nameof(to_port_id)}={to_port_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog:SendCmdToControl-2 AVEHICLES/SendCommand");
            return result == "OK";
        }

        public bool SendPaserToControl(string vh_id, sc.ProtocolFormat.OHTMessage.PauseEvent event_type)
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "PauseEvent",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            sb.Append($"{nameof(event_type)}={event_type.ToString()}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog: AVEHICLES/PauseEvent");
            return result == "OK";
        }

        public bool SendPauseStatusChange(string vh_id, sc.App.SCAppConstants.OHxCPauseType pauseType, sc.ProtocolFormat.OHTMessage.PauseEvent event_type, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "PauseStatusChange",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            sb.Append($"{nameof(pauseType)}={pauseType.ToString()}").Append("&");
            sb.Append($"{nameof(event_type)}={event_type.ToString()}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog: AVEHICLES/PauseStatusChange");
            return result == "OK";
        }

        public bool SendModeStatusChange(string vh_id, VHModeStatus modeStatus, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "ModeStatusChange",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            sb.Append($"{nameof(modeStatus)}={modeStatus.ToString()}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog: AVEHICLES/ModeStatusChange");
            return result == "OK";
        }

        public bool SendVehicleAlarmResetRequest(string vh_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "AVEHICLES",
                "ResetAlarm",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            logger.Info(string.Format("{0}Time: {1}", new string(' ', 5), DateTime.Now) + " , TestLog: AVEHICLES/ResetAlarm");
            return result == "OK";
        }

        public class Cache
        {
            ObjCacheManager objCache;
            public Cache(ObjCacheManager _objCache)
            {
                objCache = _objCache;
            }

            public List<AVEHICLE> GetVEHICLEs()
            {
                return objCache.GetVEHICLEs();
            }

        }

    }
}
