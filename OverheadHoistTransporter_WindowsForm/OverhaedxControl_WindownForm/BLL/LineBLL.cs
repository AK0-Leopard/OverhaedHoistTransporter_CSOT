using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data;
using STAN.Client;
using System;
using System.Collections.Generic;
using com.mirle.ibg3k0.sc.Data.DAO;
using System.Text;
using com.mirle.ibg3k0.ohxc.winform.Common;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class LineBLL
    {
        WindownApplication app = null;

        public LineBLL(WindownApplication _app)
        {
            app = _app;
        }

        public void SubscriberLineInfo(string subject, EventHandler<StanMsgHandlerArgs> handler)
        {
            app.GetNatsManager().Subscriber(subject, handler, is_last: true);
            // app.GetNatsManager().Subscriber(subject, handler);
        }

        public void UnsubscriberLineInfo(string subject)
        {
            app.GetNatsManager().Unsubscribe(subject);
        }

        public void ProcLineInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            //var vehicle_obj_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);
            // var vh_obj = ZeroFormatterSerializer.Deserialize<AVEHICLE>(bytes);
            sc.ProtocolFormat.OHTMessage.LINE_INFO line_info = sc.BLL.LineBLL.Convert2Object_LineInfo(bytes);

            app.ObjCacheManager.putLine(line_info);
        }

        public void OnlineCheckInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            //var vehicle_obj_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);
            // var vh_obj = ZeroFormatterSerializer.Deserialize<AVEHICLE>(bytes);
            sc.ProtocolFormat.OHTMessage.ONLINE_CHECK_INFO online_info = sc.BLL.LineBLL.Convert2Object_OnlineCheckInfo(bytes);

            app.ObjCacheManager.putOnlineCheckInfo(online_info);
        }


        public void TransferInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            //var vehicle_obj_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);
            // var vh_obj = ZeroFormatterSerializer.Deserialize<AVEHICLE>(bytes);
            sc.ProtocolFormat.OHTMessage.TRANSFER_INFO transfer_info = sc.BLL.LineBLL.Convert2Object_TransferInfo(bytes);

            app.ObjCacheManager.putTransferInfo(transfer_info);
        }
        public void SystemLogInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            //var vehicle_obj_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);
            // var vh_obj = ZeroFormatterSerializer.Deserialize<AVEHICLE>(bytes);
            sc.ProtocolFormat.OHTMessage.TRANSFER_INFO transfer_info = sc.BLL.LineBLL.Convert2Object_TransferInfo(bytes);

            app.ObjCacheManager.putTransferInfo(transfer_info);
        }

        public void PingCheckInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            sc.ProtocolFormat.OHTMessage.PING_CHECK_INFO ping_info = sc.BLL.LineBLL.Convert2Object_PingCheckInfo(bytes);

            app.ObjCacheManager.putPingCheckInfo(ping_info);
        }

        public void MTLMTSInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            sc.ProtocolFormat.OHTMessage.MTL_MTS_INFO mtlmts_info = sc.BLL.LineBLL.Convert2Object_MTLMTSInfo(bytes);

            app.ObjCacheManager.putMTL_MTSCheckInfo(mtlmts_info);
        }

        public void SubscriberTipMessageInfo(string subject, EventHandler<StanMsgHandlerArgs> handler)
        {
            app.GetNatsManager().Subscriber(subject, handler, is_last: true);
            // app.GetNatsManager().Subscriber(subject, handler);
        }

        public void ProcTipMessageInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            //var vehicle_obj_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);
            // var vh_obj = ZeroFormatterSerializer.Deserialize<AVEHICLE>(bytes);
            sc.ProtocolFormat.OHTMessage.TIP_MESSAGE_COLLECTION msg_collection = sc.BLL.LineBLL.Convert2Object_TipMsgInfoCollection(bytes);

            app.ObjCacheManager.putTipMessageInfos(msg_collection);
        }

        public void ConnectioneInfo(object sender, StanMsgHandlerArgs handler)
        {
            var bytes = handler.Message.Data;
            //var vehicle_obj_info = sc.BLL.VehicleBLL.Convert2Object_VehicleInfo(bytes);
            // var vh_obj = ZeroFormatterSerializer.Deserialize<AVEHICLE>(bytes);
            sc.ProtocolFormat.OHTMessage.LINE_INFO line_info = sc.BLL.LineBLL.Convert2Object_LineInfo(bytes);

            app.ObjCacheManager.putConnectionInfo(line_info);
        }


        public bool SendLinkStatusChange(string linkstatus, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "ConnectionInfo",
                "LinkStatusChange",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(linkstatus)}={linkstatus}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }


        public bool SendHostModeChange(string hostmode, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "ConnectionInfo",
                "HostModeChange",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(hostmode)}={hostmode}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }


        public bool SendTSCStateChange(string tscstate, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "ConnectionInfo",
                "TSCStateChange",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(tscstate)}={tscstate}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }
        public bool SendLogInRequest(string userID, string password, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "LogIn",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userID)}={userID}").Append("&");
            sb.Append($"{nameof(password)}={password}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendUserAccountAddRequest(string userID, string password, string userName, string isDisable, string userGrp, string badgeNo, string department, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UserAccountAdd",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userID)}={userID}").Append("&");
            sb.Append($"{nameof(password)}={password}").Append("&");
            sb.Append($"{nameof(userName)}={userName}").Append("&");
            sb.Append($"{nameof(isDisable)}={isDisable}").Append("&");
            sb.Append($"{nameof(userGrp)}={userGrp}").Append("&");
            sb.Append($"{nameof(badgeNo)}={badgeNo}").Append("&");
            sb.Append($"{nameof(department)}={department}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendUserAccountUpdateRequest(string userID, string password, string userName, string isDisable, string userGrp, string badgeNo, string department, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UserAccountUpdate",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userID)}={userID}").Append("&");
            sb.Append($"{nameof(password)}={password}").Append("&");
            sb.Append($"{nameof(userName)}={userName}").Append("&");
            sb.Append($"{nameof(isDisable)}={isDisable}").Append("&");
            sb.Append($"{nameof(userGrp)}={userGrp}").Append("&");
            sb.Append($"{nameof(badgeNo)}={badgeNo}").Append("&");
            sb.Append($"{nameof(department)}={department}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendUserAccountDeleteRequest(string userID, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UserAccountDelete",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userID)}={userID}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }


        public bool SendUserGroupAddRequest(string userGrp, string funcCloseSystem, string funcSystemControlMode, string funcLogin, string funcAccountManagement
            , string funcVehicleManagement, string funcTransferManagement, string funcMTLMTSMaintenance, string funcPortMaintenance, string funcDebug, string funcAdvancedSettings, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UserGroupAdd",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userGrp)}={userGrp}").Append("&");
            sb.Append($"{nameof(funcCloseSystem)}={funcCloseSystem}").Append("&");
            sb.Append($"{nameof(funcSystemControlMode)}={funcSystemControlMode}").Append("&");
            sb.Append($"{nameof(funcLogin)}={funcLogin}").Append("&");
            sb.Append($"{nameof(funcAccountManagement)}={funcAccountManagement}").Append("&");
            sb.Append($"{nameof(funcVehicleManagement)}={funcVehicleManagement}").Append("&");
            sb.Append($"{nameof(funcTransferManagement)}={funcTransferManagement}").Append("&");
            sb.Append($"{nameof(funcMTLMTSMaintenance)}={funcMTLMTSMaintenance}").Append("&");
            sb.Append($"{nameof(funcPortMaintenance)}={funcPortMaintenance}").Append("&");
            sb.Append($"{nameof(funcDebug)}={funcDebug}").Append("&");
            sb.Append($"{nameof(funcAdvancedSettings)}={funcAdvancedSettings}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendUserGroupUpdateRequest(string userGrp, string funcCloseSystem, string funcSystemControlMode, string funcLogin, string funcAccountManagement
    , string funcVehicleManagement, string funcTransferManagement, string funcMTLMTSMaintenance, string funcPortMaintenance, string funcDebug, string funcAdvancedSettings, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UserGroupUpdate",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userGrp)}={userGrp}").Append("&");
            sb.Append($"{nameof(funcCloseSystem)}={funcCloseSystem}").Append("&");
            sb.Append($"{nameof(funcSystemControlMode)}={funcSystemControlMode}").Append("&");
            sb.Append($"{nameof(funcLogin)}={funcLogin}").Append("&");
            sb.Append($"{nameof(funcAccountManagement)}={funcAccountManagement}").Append("&");
            sb.Append($"{nameof(funcVehicleManagement)}={funcVehicleManagement}").Append("&");
            sb.Append($"{nameof(funcTransferManagement)}={funcTransferManagement}").Append("&");
            sb.Append($"{nameof(funcMTLMTSMaintenance)}={funcMTLMTSMaintenance}").Append("&");
            sb.Append($"{nameof(funcPortMaintenance)}={funcPortMaintenance}").Append("&");
            sb.Append($"{nameof(funcDebug)}={funcDebug}").Append("&");
            sb.Append($"{nameof(funcAdvancedSettings)}={funcAdvancedSettings}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendUserGroupDeleteRequest(string userGrp, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UserGroupDelete",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userGrp)}={userGrp}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendExitRequest(string userID, string password, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "Exit",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userID)}={userID}").Append("&");
            sb.Append($"{nameof(password)}={password}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendPasswordChange(string userID, string password_o, string password_n, string password_v, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "UserControl",
                "UpdatePassword",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(userID)}={userID}").Append("&");
            sb.Append($"{nameof(password_o)}={password_o}").Append("&");
            sb.Append($"{nameof(password_n)}={password_n}").Append("&");
            sb.Append($"{nameof(password_v)}={password_v}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMCSCommandAutoAssignChange(string AutoAssign, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "MCSQueueSwitch",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(AutoAssign)}={AutoAssign}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }


        public bool SendMCSCommandCancelAbort(string mcs_cmd, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "CancelAbort",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(mcs_cmd)}={mcs_cmd}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMCSCommandForceFinish(string mcs_cmd, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "ForceFinish",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(mcs_cmd)}={mcs_cmd}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMCSCommandAssignVehicle(string mcs_cmd, string vh_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "AssignVehicle",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(mcs_cmd)}={mcs_cmd}").Append("&");
            sb.Append($"{nameof(vh_id)}={vh_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMCSCommandShift(string mcs_cmd, string vh_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "ShiftCommand",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(mcs_cmd)}={mcs_cmd}").Append("&");
            sb.Append($"{nameof(vh_id)}={vh_id}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMCSCommandChangeStatus(string mcs_cmd, string status, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "ChangeStatus",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(mcs_cmd)}={mcs_cmd}").Append("&");
            sb.Append($"{nameof(status)}={status}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMCSCommandChangePriority(string mcs_cmd, string priority, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "TransferManagement",
                "Priority",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(mcs_cmd)}={mcs_cmd}").Append("&");
            sb.Append($"{nameof(priority)}={priority}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }


        public bool SendMTSMTLCarOutInterlock(string station_id, string isSet, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "MTSMTLInfo",
                "InterlockRequest",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(station_id)}={station_id}").Append("&");
            sb.Append($"{nameof(isSet)}={isSet}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendMTSMTLCarInInterlock(string station_id, string isSet, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "MTSMTLInfo",
                "CarInInterlockRequest",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(station_id)}={station_id}").Append("&");
            sb.Append($"{nameof(isSet)}={isSet}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }
        public bool SendMTSMTLCarOurRequest(string vh_id, string station_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "MTSMTLInfo",
                "CarOutRequest",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            sb.Append($"{nameof(station_id)}={station_id}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }
        public bool SendMTSMTLCarOurCancel(string station_id, out string result)
        {
            result = string.Empty;
            string[] action_targets = new string[]
            {
                "MTSMTLInfo",
                "CarOutCancel",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(station_id)}={station_id}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

    }
}

