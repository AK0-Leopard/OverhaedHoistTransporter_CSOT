using System;
using System.Collections.Generic;
using com.mirle.iibg3k0.ttc.Common;
using com.mirle.iibg3k0.ttc.Common.TCPIP;
using Google.Protobuf.Collections;
using com.mirle.iibg3k0.ttc.Common.TCPIP.DecodRawData;
using System.Threading.Tasks;
using System.Reflection;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System.Threading;

namespace Mirle.Agvc.Simulator
{
    public class MiddleAgent
    {
        #region Events
        public event EventHandler<string> OnTransferCancelEvent;
        public event EventHandler<string> OnTransferAbortEvent;

        public event EventHandler<string> OnConnected;
        public event EventHandler<string> OnDisConnected;
        public event EventHandler<string> OnCmdSend;
        public event EventHandler<string> OnCmdReceive;
        #endregion
        private EnumOperaType cmdOpera = 0;
        private int abnormalErrorCode = 0;
        private LoggerAgent theLoggerAgent;
        private Vehicle vehicle;
        private CmdQueue cmdQueue;
        private ID_144_STATUS_CHANGE_REP theVehicleInfo;
        private MiddlerConfigs middlerConfigs;

        private Opera operaObject;

        public TcpIpAgent ServerClientAgent { get; private set; }
        public Dictionary<string, TcpIpAgent> _tcpipAgentDic = new Dictionary<string, TcpIpAgent>();

        public bool AutoApplyReserve { get; set; } = true;

        public Form1 AForm { get; set; } = new Form1();

        public MiddleAgent(MiddlerConfigs middlerConfigs)
        {
            bool isSuccess = true;
            this.middlerConfigs = middlerConfigs;

            theLoggerAgent = LoggerAgent.Instance;

            vehicle = new Vehicle();
            operaObject = new Opera();

            isSuccess = isSuccess && vehicle.Vehicle_Initialize();
            theVehicleInfo = vehicle.Vehicle_Data;

            cmdQueue = new CmdQueue();

            //if (isSuccess)
            //{
            //    CreatTcpIpServerClientAgent();
            //}
            //SampleClientConnect();

        }

        public void ReConnect()
        {
            try
            {
                DisConnect();
                CreatTcpIpServerClientAgent();
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void DisConnect()
        {
            try
            {
                if (ServerClientAgent != null)
                {
                    ServerClientAgent.stop();
                    ServerClientAgent = null;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void SetAlarmToOHBC()
        {
            Send_Cmd194_AlarmReport("312001", ErrorStatus.ErrSet);
        }

        private void CreatTcpIpServerClientAgent()
        {
            IDecodReceiveRawData RawDataDecoder = new DecodeRawData_Google(unPackWrapperMsg);

            int clientNum = middlerConfigs.ClientNum;
            string clientName = middlerConfigs.ClientName;
            string sRemoteIP = middlerConfigs.RemoteIp;
            int iRemotePort = middlerConfigs.RemotePort;
            string sLocalIP = middlerConfigs.LocalIp;
            int iLocalPort = middlerConfigs.LocalPort;

            int recv_timeout_ms = middlerConfigs.RecvTimeoutMs;                         //等待sendRecv Reply的Time out時間(milliseconds)
            int send_timeout_ms = middlerConfigs.SendTimeoutMs;                         //暫時無用
            int max_readSize = middlerConfigs.MaxReadSize;                              //暫時無用
            int reconnection_interval_ms = middlerConfigs.ReconnectionIntervalMs;       //斷線多久之後再進行一次嘗試恢復連線的動作
            int max_reconnection_count = middlerConfigs.MaxReconnectionCount;           //斷線後最多嘗試幾次重新恢復連線 (若設定為0則不進行自動重新連線)
            int retry_count = middlerConfigs.RetryCount;                                //SendRecv Time out後要再重複發送的次數

            try
            {


                if (middlerConfigs.IsServer)
                {

                    ServerClientAgent = new TcpIpAgent(clientNum, clientName, sLocalIP, iLocalPort, sRemoteIP, iRemotePort, TcpIpAgent.TCPIP_AGENT_COMM_MODE.SERVER_MODE, 10000, 10, 1000, 2, 5, 2, AppConstants.FrameBuilderType.PC_TYPE_MIRLE);
                    ServerClientAgent.injectDecoder(RawDataDecoder);
                    _tcpipAgentDic.Add(clientName, ServerClientAgent);
                    //_tcpipAgentDic.Add("OHT01", new TcpIpAgent(1, "OHT01", "127.0.0.1", 5000, "127.0.0.1", 5001, TcpIpAgent.TCPIP_AGENT_COMM_MODE.SERVER_MODE
                    //    , 10000, 10, 1000, 2, 5, 2, AppConstants.FrameBuilderType.PC_TYPE_MIRLE));
                    // _tcpipAgentDic["OHT01"].injectDecoder(RawDataDecoder);
                    EventInitial();

                    TcpIpServer server = new TcpIpServer(iLocalPort, ref _tcpipAgentDic, AppConstants.FrameBuilderType.PC_TYPE_MIRLE, false);
                    server.Listen();
                }
                else
                {
                    ServerClientAgent = new TcpIpAgent(clientNum, clientName, sLocalIP, iLocalPort, sRemoteIP, iRemotePort, TcpIpAgent.TCPIP_AGENT_COMM_MODE.CLINET_MODE, recv_timeout_ms, send_timeout_ms, max_readSize, reconnection_interval_ms, max_reconnection_count, retry_count, AppConstants.FrameBuilderType.PC_TYPE_MIRLE);

                    EventInitial();

                    ServerClientAgent.injectDecoder(RawDataDecoder);

                    Task.Run(() =>
                    {
                        ServerClientAgent.start();
                    });

                    //ServerClientAgent.clientConnection();                    
                }
            }
            catch (Exception ex)
            {

                var temp = ex.StackTrace;
            }
        }

        public static Google.Protobuf.IMessage unPackWrapperMsg(byte[] raw_data)
        {
            WrapperMessage WarpperMsg = ToObject<WrapperMessage>(raw_data);
            return WarpperMsg;
        }

        public static T ToObject<T>(byte[] buf) where T : Google.Protobuf.IMessage<T>, new()
        {
            if (buf == null)
                return default(T);

            Google.Protobuf.MessageParser<T> parser = new Google.Protobuf.MessageParser<T>(() => new T());
            return parser.ParseFrom(buf);
        }

        /// <summary>
        /// 註冊要監聽的事件
        /// </summary>
        void EventInitial()
        {
            // Add Event Handlers for all the recieved messages
            try
            {
                WriteLogForEachCmdRecieved();
                SetNecessaryCmdHandler();
                //Here need to be careful for the TCPIP

                ServerClientAgent.addTcpIpConnectedHandler(DoConnection);       //連線時的通知
                ServerClientAgent.addTcpIpDisconnectedHandler(DoDisconnection); //斷線時的通知
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        private void SetNecessaryCmdHandler()
        {
            ServerClientAgent.addTcpIpReceivedHandler(WrapperMessage.TransReqFieldNumber, Receive_Cmd31_TransferRequest);
            ServerClientAgent.addTcpIpReceivedHandler(WrapperMessage.TransCancelReqFieldNumber, Receive_Cmd37_TransferCancelRequest);
            ServerClientAgent.addTcpIpReceivedHandler(WrapperMessage.PauseReqFieldNumber, Receive_Cmd39_PauseRequest);
            ServerClientAgent.addTcpIpReceivedHandler(WrapperMessage.ModeChangeReqFieldNumber, Receive_Cmd41_ModeChange);
            ServerClientAgent.addTcpIpReceivedHandler(WrapperMessage.StatusReqFieldNumber, Receive_Cmd43_StatusRequest);
            ServerClientAgent.addTcpIpReceivedHandler(WrapperMessage.AlarmResetReqFieldNumber, Receive_Cmd91_AlarmResetRequest);
        }

        private void WriteLogForEachCmdRecieved()
        {
            foreach (var item in Enum.GetValues(typeof(EnumCmdNums)))
            {
                ServerClientAgent.addTcpIpReceivedHandler((int)item, RecieveCmdShowOnForm1);
            }
        }

        private void SendCommandWrapper(WrapperMessage wrapper, bool isReply = false)
        {
            string msg = $"[SEND] [ID = {wrapper.ID}][SeqNum = {wrapper.SeqNum}] " + wrapper.ToString();
            OnCmdSend?.Invoke(this, msg);
            theLoggerAgent.LogMsg("Comm", new LogFormat("Comm", "1", GetType().Name + ":" + MethodBase.GetCurrentMethod().Name, "Device", "CarrierID"
                 , msg));

            Task.Run(() => ServerClientAgent.TrxTcpIp.SendGoogleMsg(wrapper, isReply));
        }

        private void RecieveCmdShowOnForm1(object sender, TcpIpEventArgs e)
        {
            string msg = $"[RECEIVE] [PacketID = {e.iPacketID}][SeqNum = {e.iSeqNum}][Pt = {e.iPt}][ObjPacket = {e.objPacket}]";
            OnCmdReceive?.Invoke(this, msg);
            theLoggerAgent.LogMsg("Comm", new LogFormat("Comm", "1", GetType().Name + ":" + MethodBase.GetCurrentMethod().Name, "Device", "CarrierID"
                , msg));
        }

        private void MiddleAgent_OnMsgFromAgvcEvent(object sender, string e)
        {
            string className = GetType().Name;
            string methodName = sender.ToString(); //System.Reflection.MethodBase.GetCurrentMethod().Name;
            string classMethodName = className + ":" + methodName;
            LogFormat logFormat = new LogFormat("Debug", "3", classMethodName, "Device", "CarrierID", e);
            theLoggerAgent.LogMsg("Debug", logFormat);
        }

        protected void DoConnection(object sender, TcpIpEventArgs e)
        {
            TcpIpAgent agent = sender as TcpIpAgent;
            Console.WriteLine("Vh ID:{0}, connection.", agent.Name);
            OnConnected?.Invoke(this, "Connected");
        }
        protected void DoDisconnection(object sender, TcpIpEventArgs e)
        {
            TcpIpAgent agent = sender as TcpIpAgent;
            Console.WriteLine("Vh ID:{0}, disconnection.", agent.Name);
            OnDisConnected?.Invoke(this, "Dis-Connect");

        }

        public void SendCommand(int cmdNum, Dictionary<string, string> pairs)
        {
            try
            {
                WrapperMessage wrappers = new WrapperMessage();

                var cmdType = (EnumCmdNums)cmdNum;
                switch (cmdType)
                {
                    case EnumCmdNums.Cmd31_TransferRequest:
                        {
                            ID_31_TRANS_REQUEST aCmd = new ID_31_TRANS_REQUEST();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = ActiveTypeConverter(pairs["ActType"]);
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.SecDistance = uint.Parse(pairs["SecDistance"]);
                            AddRangeToGuideList(aCmd.GuideSections, pairs["GuideSections"]);
                            //AddRangeToGuideList(aCmd.GuideAddressStartToLoad, pairs["GuideAddressesStartToLoad"]);
                            aCmd.LoadAdr = pairs["LoadAdr"];

                            //AddRangeToGuideList(aCmd.GuideSectionsToDestination, pairs["GuideSectionsToDestination"]);
                            //AddRangeToGuideList(aCmd.GuideAddressToDestination, pairs["GuideAddressesToDestination"]);
                            aCmd.ToAdr = pairs["DestinationAdr"];

                            wrappers.ID = WrapperMessage.TransReqFieldNumber;
                            wrappers.TransReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd32_TransferCompleteResponse:
                        {
                            ID_32_TRANS_COMPLETE_RESPONSE aCmd = new ID_32_TRANS_COMPLETE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.TranCmpRespFieldNumber;
                            wrappers.TranCmpResp = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd33_ControlZoneCancelRequest:
                        {
                            ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST aCmd = new ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST();
                            aCmd.CancelSecID = pairs["CancelSecID"];
                            aCmd.ControlType = ControlTypeConverter(pairs["ControlType"]);

                            wrappers.ID = WrapperMessage.ControlZoneReqFieldNumber;
                            wrappers.ControlZoneReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd36_TransferEventResponse:
                        {
                            ID_36_TRANS_EVENT_RESPONSE aCmd = new ID_36_TRANS_EVENT_RESPONSE();
                            aCmd.IsBlockPass = PassTypeConverter(pairs["IsBlockPass"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.ImpTransEventRespFieldNumber;
                            wrappers.ImpTransEventResp = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd37_TransferCancelRequest:
                        {
                            ID_37_TRANS_CANCEL_REQUEST aCmd = new ID_37_TRANS_CANCEL_REQUEST();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = CMDCancelTypeConverter(pairs["ActType"]);

                            wrappers.ID = WrapperMessage.TransCancelReqFieldNumber;
                            wrappers.TransCancelReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd39_PauseRequest:
                        {
                            ID_39_PAUSE_REQUEST aCmd = new ID_39_PAUSE_REQUEST();
                            aCmd.EventType = PauseEventConverter(pairs["EventType"]);
                            aCmd.PauseType = PauseTypeConverter(pairs["PauseType"]);

                            wrappers.ID = WrapperMessage.PauseReqFieldNumber;
                            wrappers.PauseReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd41_ModeChange:
                        {
                            ID_41_MODE_CHANGE_REQ aCmd = new ID_41_MODE_CHANGE_REQ();
                            aCmd.OperatingVHMode = OperatingVHModeConverter(pairs["OperatingVHMode"]);

                            wrappers.ID = WrapperMessage.ModeChangeReqFieldNumber;
                            wrappers.ModeChangeReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd43_StatusRequest:
                        {
                            ID_43_STATUS_REQUEST aCmd = new ID_43_STATUS_REQUEST();

                            wrappers.ID = WrapperMessage.StatusReqFieldNumber;
                            wrappers.StatusReq = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd44_StatusRequest:
                        {
                            ID_44_STATUS_CHANGE_RESPONSE aCmd = new ID_44_STATUS_CHANGE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.StatusChangeRespFieldNumber;
                            wrappers.StatusChangeResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd45_PowerOnoffRequest:
                        {
                            ID_45_POWER_OPE_REQ aCmd = new ID_45_POWER_OPE_REQ();
                            aCmd.OperatingPowerMode = OperatingPowerModeConverter(pairs["OperatingPowerMode"]);

                            wrappers.ID = WrapperMessage.PowerOpeReqFieldNumber;
                            wrappers.PowerOpeReq = aCmd;

                            break;
                        }

                    case EnumCmdNums.Cmd71_RangeTeachRequest:
                        {
                            ID_71_RANGE_TEACHING_REQUEST aCmd = new ID_71_RANGE_TEACHING_REQUEST();
                            aCmd.FromAdr = pairs["FromAdr"];
                            aCmd.ToAdr = pairs["ToAdr"];

                            wrappers.ID = WrapperMessage.RangeTeachingReqFieldNumber;
                            wrappers.RangeTeachingReq = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd72_RangeTeachCompleteResponse:
                        {
                            ID_72_RANGE_TEACHING_COMPLETE_RESPONSE aCmd = new ID_72_RANGE_TEACHING_COMPLETE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.RangeTeachingCmpRespFieldNumber;
                            wrappers.RangeTeachingCmpResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd74_AddressTeachResponse:
                        {
                            ID_74_ADDRESS_TEACH_RESPONSE aCmd = new ID_74_ADDRESS_TEACH_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.AddressTeachRespFieldNumber;
                            wrappers.AddressTeachResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd91_AlarmResetRequest:
                        {
                            ID_91_ALARM_RESET_REQUEST aCmd = new ID_91_ALARM_RESET_REQUEST();

                            wrappers.ID = WrapperMessage.AlarmResetReqFieldNumber;
                            wrappers.AlarmResetReq = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd94_AlarmResponse:
                        {
                            ID_94_ALARM_RESPONSE aCmd = new ID_94_ALARM_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            break;
                        }
                    case EnumCmdNums.Cmd131_TransferResponse:
                        {
                            ID_131_TRANS_RESPONSE aCmd = new ID_131_TRANS_RESPONSE();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = ActiveTypeConverter(pairs["ActType"]);
                            aCmd.NgReason = pairs["NgReason"];
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.TransRespFieldNumber;
                            wrappers.TransResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd132_TransferCompleteReport:
                        {
                            ID_132_TRANS_COMPLETE_REPORT aCmd = new ID_132_TRANS_COMPLETE_REPORT();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.CmdDistance = int.Parse(pairs["CmdDistance"]);
                            aCmd.CmpStatus = CompleteStatusConverter(pairs["CmpStatus"]);
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];
                            aCmd.CurrentSecID = pairs["CurrentSecID"];

                            wrappers.ID = WrapperMessage.TranCmpRepFieldNumber;
                            wrappers.TranCmpRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd133_ControlZoneCancelResponse:
                        {
                            ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE aCmd = new ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE();
                            aCmd.CancelSecID = pairs["CancelSecID"];
                            aCmd.ControlType = ControlTypeConverter(pairs["ControlType"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.ControlZoneRespFieldNumber;
                            wrappers.ControlZoneResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd134_TransferEventReport:
                        {
                            ID_134_TRANS_EVENT_REP aCmd = new ID_134_TRANS_EVENT_REP();
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];
                            aCmd.CurrentSecID = pairs["CurrentSecID"];
                            aCmd.EventType = EventTypeConverter(pairs["EventType"]);

                            wrappers.ID = WrapperMessage.TransEventRepFieldNumber;
                            wrappers.TransEventRep = aCmd;

                            break;
                        }

                    case EnumCmdNums.Cmd136_TransferEventReport:
                        {
                            ID_136_TRANS_EVENT_REP aCmd = new ID_136_TRANS_EVENT_REP();
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];
                            aCmd.CurrentSecID = pairs["CurrentSecID"];
                            aCmd.EventType = EventTypeConverter(pairs["EventType"]);

                            wrappers.ID = WrapperMessage.ImpTransEventRepFieldNumber;
                            wrappers.ImpTransEventRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd137_TransferCancelResponse:
                        {
                            ID_137_TRANS_CANCEL_RESPONSE aCmd = new ID_137_TRANS_CANCEL_RESPONSE();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = CMDCancelTypeConverter(pairs["ActType"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.TransCancelRespFieldNumber;
                            wrappers.TransCancelResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd139_PauseResponse:
                        {
                            ID_139_PAUSE_RESPONSE aCmd = new ID_139_PAUSE_RESPONSE();
                            aCmd.EventType = PauseEventConverter(pairs["EventType"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.PauseRespFieldNumber;
                            wrappers.PauseResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd141_ModeChangeResponse:
                        {
                            ID_141_MODE_CHANGE_RESPONSE aCmd = new ID_141_MODE_CHANGE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.ModeChangeRespFieldNumber;
                            wrappers.ModeChangeResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd143_StatusResponse:
                        {
                            //TODO: 補完屬性
                            ID_143_STATUS_RESPONSE aCmd = new ID_143_STATUS_RESPONSE();
                            aCmd.ActionStatus = VHActionStatusConverter(pairs["ActionStatus"]);
                            //aCmd.BatteryCapacity = uint.Parse(pairs["BatteryCapacity"]);
                            //aCmd.BatteryTemperature = int.Parse(pairs["BatteryTemperature"]);
                            aCmd.BlockingStatus = VhStopSingleConverter(pairs["BlockingStatus"]);
                            //aCmd.ChargeStatus = VhChargeStatusConverter(pairs["ChargeStatus"]);
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];

                            wrappers.ID = WrapperMessage.StatusReqRespFieldNumber;
                            wrappers.StatusReqResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd144_StatusReport:
                        {
                            //TODO: 補完屬性
                            ID_144_STATUS_CHANGE_REP aCmd = new ID_144_STATUS_CHANGE_REP();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActionStatus = VHActionStatusConverter(pairs["ActionStatus"]);
                            //aCmd.BatteryCapacity = uint.Parse(pairs["BatteryCapacity"]);
                            //aCmd.BatteryTemperature = int.Parse(pairs["BatteryTemperature"]);
                            aCmd.BlockingStatus = VhStopSingleConverter(pairs["BlockingStatus"]);
                            //aCmd.ChargeStatus = VhChargeStatusConverter(pairs["ChargeStatus"]);
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.CSTID = pairs["CSTID"];

                            wrappers.ID = WrapperMessage.StatueChangeRepFieldNumber;
                            wrappers.StatueChangeRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd145_PowerOnoffResponse:
                        {
                            ID_145_POWER_OPE_RESPONSE aCmd = new ID_145_POWER_OPE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.PowerOpeRespFieldNumber;
                            wrappers.PowerOpeResp = aCmd;

                            break;
                        }

                    case EnumCmdNums.Cmd171_RangeTeachResponse:
                        {
                            ID_171_RANGE_TEACHING_RESPONSE aCmd = new ID_171_RANGE_TEACHING_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.RangeTeachingRespFieldNumber;
                            wrappers.RangeTeachingResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd172_RangeTeachCompleteReport:
                        {
                            ID_172_RANGE_TEACHING_COMPLETE_REPORT aCmd = new ID_172_RANGE_TEACHING_COMPLETE_REPORT();
                            aCmd.CompleteCode = int.Parse(pairs["CompleteCode"]);
                            aCmd.FromAdr = pairs["FromAdr"];
                            aCmd.SecDistance = uint.Parse(pairs["SecDistance"]);
                            aCmd.ToAdr = pairs["ToAdr"];

                            wrappers.ID = WrapperMessage.RangeTeachingCmpRepFieldNumber;
                            wrappers.RangeTeachingCmpRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd174_AddressTeachReport:
                        {
                            ID_174_ADDRESS_TEACH_REPORT aCmd = new ID_174_ADDRESS_TEACH_REPORT();
                            aCmd.Addr = pairs["Addr"];
                            aCmd.Position = int.Parse(pairs["Position"]);

                            wrappers.ID = WrapperMessage.AddressTeachRepFieldNumber;
                            wrappers.AddressTeachRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd191_AlarmResetResponse:
                        {
                            ID_191_ALARM_RESET_RESPONSE aCmd = new ID_191_ALARM_RESET_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.AlarmResetRespFieldNumber;
                            wrappers.AlarmResetResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd194_AlarmReport:
                        {
                            ID_194_ALARM_REPORT aCmd = new ID_194_ALARM_REPORT();
                            aCmd.ErrCode = pairs["ErrCode"];
                            aCmd.ErrDescription = pairs["ErrDescription"];
                            aCmd.ErrStatus = ErrorStatusConverter(pairs["ErrStatus"]);

                            wrappers.ID = WrapperMessage.AlarmRepFieldNumber;
                            wrappers.AlarmRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd000_EmptyCommand:
                    default:
                        {
                            ID_1_HOST_BASIC_INFO_VERSION_REP aCmd = new ID_1_HOST_BASIC_INFO_VERSION_REP();

                            wrappers.ID = WrapperMessage.HostBasicInfoRepFieldNumber;
                            wrappers.HostBasicInfoRep = aCmd;

                            break;
                        }
                }

                SendCommandWrapper(wrappers);  //似乎是SendFunction底層會咬住等待回應所以開THD去發  

                //OnCmdSend?.Invoke(this, wrappers.ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

        }
        public void SendCommand(int cmdNum, Dictionary<string, string> pairs, ushort seqNum)
        {
            try
            {
                WrapperMessage wrappers = new WrapperMessage();
                wrappers.SeqNum = seqNum;
                var cmdType = (EnumCmdNums)cmdNum;
                switch (cmdType)
                {
                    case EnumCmdNums.Cmd31_TransferRequest:
                        {
                            ID_31_TRANS_REQUEST aCmd = new ID_31_TRANS_REQUEST();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = ActiveTypeConverter(pairs["ActType"]);
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.SecDistance = uint.Parse(pairs["SecDistance"]);
                            AddRangeToGuideList(aCmd.GuideSections, pairs["GuideSections"]);
                            //AddRangeToGuideList(aCmd.GuideAddressesStartToLoad, pairs["GuideAddressesStartToLoad"]);
                            aCmd.LoadAdr = pairs["LoadAdr"];

                            //AddRangeToGuideList(aCmd.GuideSectionsToDestination, pairs["GuideSectionsToDestination"]);
                            //AddRangeToGuideList(aCmd.GuideAddressesToDestination, pairs["GuideAddressesToDestination"]);
                            //aCmd.DestinationAdr = pairs["DestinationAdr"];

                            wrappers.ID = WrapperMessage.TransReqFieldNumber;
                            wrappers.TransReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd32_TransferCompleteResponse:
                        {
                            ID_32_TRANS_COMPLETE_RESPONSE aCmd = new ID_32_TRANS_COMPLETE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.TranCmpRespFieldNumber;
                            wrappers.TranCmpResp = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd33_ControlZoneCancelRequest:
                        {
                            ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST aCmd = new ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST();
                            aCmd.CancelSecID = pairs["CancelSecID"];
                            aCmd.ControlType = ControlTypeConverter(pairs["ControlType"]);

                            wrappers.ID = WrapperMessage.ControlZoneReqFieldNumber;
                            wrappers.ControlZoneReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd35_CarrierIdRenameRequest:
                        {
                            //ID_35_CST_ID_RENAME_REQUEST aCmd = new ID_35_CST_ID_RENAME_REQUEST();
                            //aCmd.NEWCSTID = pairs["NEWCSTID"];
                            //aCmd.OLDCSTID = pairs["OLDCSTID"];

                            //wrappers.ID = WrapperMessage.CSTIDRenameReqFieldNumber;
                            //wrappers.CSTIDRenameReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd36_TransferEventResponse:
                        {
                            ID_36_TRANS_EVENT_RESPONSE aCmd = new ID_36_TRANS_EVENT_RESPONSE();
                            aCmd.IsBlockPass = PassTypeConverter(pairs["IsBlockPass"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);
                            wrappers.ID = WrapperMessage.ImpTransEventRespFieldNumber;
                            wrappers.ImpTransEventResp = aCmd;
                            break;
                        }
                    case EnumCmdNums.Cmd37_TransferCancelRequest:
                        {
                            ID_37_TRANS_CANCEL_REQUEST aCmd = new ID_37_TRANS_CANCEL_REQUEST();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = CMDCancelTypeConverter(pairs["ActType"]);

                            wrappers.ID = WrapperMessage.TransCancelReqFieldNumber;
                            wrappers.TransCancelReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd39_PauseRequest:
                        {
                            ID_39_PAUSE_REQUEST aCmd = new ID_39_PAUSE_REQUEST();
                            aCmd.EventType = PauseEventConverter(pairs["EventType"]);
                            aCmd.PauseType = PauseTypeConverter(pairs["PauseType"]);

                            wrappers.ID = WrapperMessage.PauseReqFieldNumber;
                            wrappers.PauseReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd41_ModeChange:
                        {
                            ID_41_MODE_CHANGE_REQ aCmd = new ID_41_MODE_CHANGE_REQ();
                            aCmd.OperatingVHMode = OperatingVHModeConverter(pairs["OperatingVHMode"]);

                            wrappers.ID = WrapperMessage.ModeChangeReqFieldNumber;
                            wrappers.ModeChangeReq = aCmd;


                            break;
                        }
                    case EnumCmdNums.Cmd43_StatusRequest:
                        {
                            ID_43_STATUS_REQUEST aCmd = new ID_43_STATUS_REQUEST();

                            wrappers.ID = WrapperMessage.StatusReqFieldNumber;
                            wrappers.StatusReq = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd44_StatusRequest:
                        {
                            ID_44_STATUS_CHANGE_RESPONSE aCmd = new ID_44_STATUS_CHANGE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.StatusChangeRespFieldNumber;
                            wrappers.StatusChangeResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd45_PowerOnoffRequest:
                        {
                            ID_45_POWER_OPE_REQ aCmd = new ID_45_POWER_OPE_REQ();
                            aCmd.OperatingPowerMode = OperatingPowerModeConverter(pairs["OperatingPowerMode"]);

                            wrappers.ID = WrapperMessage.PowerOpeReqFieldNumber;
                            wrappers.PowerOpeReq = aCmd;

                            break;
                        }

                    case EnumCmdNums.Cmd71_RangeTeachRequest:
                        {
                            ID_71_RANGE_TEACHING_REQUEST aCmd = new ID_71_RANGE_TEACHING_REQUEST();
                            aCmd.FromAdr = pairs["FromAdr"];
                            aCmd.ToAdr = pairs["ToAdr"];

                            wrappers.ID = WrapperMessage.RangeTeachingReqFieldNumber;
                            wrappers.RangeTeachingReq = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd72_RangeTeachCompleteResponse:
                        {
                            ID_72_RANGE_TEACHING_COMPLETE_RESPONSE aCmd = new ID_72_RANGE_TEACHING_COMPLETE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.RangeTeachingCmpRespFieldNumber;
                            wrappers.RangeTeachingCmpResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd74_AddressTeachResponse:
                        {
                            ID_74_ADDRESS_TEACH_RESPONSE aCmd = new ID_74_ADDRESS_TEACH_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.AddressTeachRespFieldNumber;
                            wrappers.AddressTeachResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd91_AlarmResetRequest:
                        {
                            ID_91_ALARM_RESET_REQUEST aCmd = new ID_91_ALARM_RESET_REQUEST();

                            wrappers.ID = WrapperMessage.AlarmResetReqFieldNumber;
                            wrappers.AlarmResetReq = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd94_AlarmResponse:
                        {
                            ID_94_ALARM_RESPONSE aCmd = new ID_94_ALARM_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            break;
                        }
                    case EnumCmdNums.Cmd131_TransferResponse:
                        {
                            ID_131_TRANS_RESPONSE aCmd = new ID_131_TRANS_RESPONSE();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = ActiveTypeConverter(pairs["ActType"]);
                            aCmd.NgReason = pairs["NgReason"];
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.TransRespFieldNumber;
                            wrappers.TransResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd132_TransferCompleteReport:
                        {
                            ID_132_TRANS_COMPLETE_REPORT aCmd = new ID_132_TRANS_COMPLETE_REPORT();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.CmdDistance = int.Parse(pairs["CmdDistance"]);
                            //aCmd.CmdPowerConsume = uint.Parse(pairs["CmdPowerConsume"]);
                            aCmd.CmpStatus = CompleteStatusConverter(pairs["CmpStatus"]);
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];
                            aCmd.CurrentSecID = pairs["CurrentSecID"];

                            wrappers.ID = WrapperMessage.TranCmpRepFieldNumber;
                            wrappers.TranCmpRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd133_ControlZoneCancelResponse:
                        {
                            ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE aCmd = new ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE();
                            aCmd.CancelSecID = pairs["CancelSecID"];
                            aCmd.ControlType = ControlTypeConverter(pairs["ControlType"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.ControlZoneRespFieldNumber;
                            wrappers.ControlZoneResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd134_TransferEventReport:
                        {
                            ID_134_TRANS_EVENT_REP aCmd = new ID_134_TRANS_EVENT_REP();
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];
                            aCmd.CurrentSecID = pairs["CurrentSecID"];
                            aCmd.EventType = EventTypeConverter(pairs["EventType"]);
                            //aCmd.DrivingDirection = DriveDirctionConverter(pairs["DrivingDirection"]);

                            wrappers.ID = WrapperMessage.TransEventRepFieldNumber;
                            wrappers.TransEventRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd135_CarrierIdRenameResponse:
                        {
                            //ID_135_CST_ID_RENAME_RESPONSE aCmd = new ID_135_CST_ID_RENAME_RESPONSE();
                            //aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            //wrappers.ID = WrapperMessage.CSTIDRenameRespFieldNumber;
                            //wrappers.CSTIDRenameResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd136_TransferEventReport:
                        {
                            ID_136_TRANS_EVENT_REP aCmd = new ID_136_TRANS_EVENT_REP();
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];
                            aCmd.CurrentSecID = pairs["CurrentSecID"];
                            aCmd.EventType = EventTypeConverter(pairs["EventType"]);

                            wrappers.ID = WrapperMessage.ImpTransEventRepFieldNumber;
                            wrappers.ImpTransEventRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd137_TransferCancelResponse:
                        {
                            ID_137_TRANS_CANCEL_RESPONSE aCmd = new ID_137_TRANS_CANCEL_RESPONSE();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActType = CMDCancelTypeConverter(pairs["ActType"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.TransCancelRespFieldNumber;
                            wrappers.TransCancelResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd139_PauseResponse:
                        {
                            ID_139_PAUSE_RESPONSE aCmd = new ID_139_PAUSE_RESPONSE();
                            aCmd.EventType = PauseEventConverter(pairs["EventType"]);
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.PauseRespFieldNumber;
                            wrappers.PauseResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd141_ModeChangeResponse:
                        {
                            ID_141_MODE_CHANGE_RESPONSE aCmd = new ID_141_MODE_CHANGE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.ModeChangeRespFieldNumber;
                            wrappers.ModeChangeResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd143_StatusResponse:
                        {
                            //TODO: 補完屬性
                            ID_143_STATUS_RESPONSE aCmd = new ID_143_STATUS_RESPONSE();
                            aCmd.ActionStatus = VHActionStatusConverter(pairs["ActionStatus"]);
                            //aCmd.BatteryCapacity = uint.Parse(pairs["BatteryCapacity"]);
                            //aCmd.BatteryTemperature = int.Parse(pairs["BatteryTemperature"]);
                            //aCmd.BlockingStatus = VhStopSingleConverter(pairs["BlockingStatus"]);
                            //aCmd.ChargeStatus = VhChargeStatusConverter(pairs["ChargeStatus"]);
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.CSTID = pairs["CSTID"];
                            aCmd.CurrentAdrID = pairs["CurrentAdrID"];

                            wrappers.ID = WrapperMessage.StatusReqRespFieldNumber;
                            wrappers.StatusReqResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd144_StatusReport:
                        {
                            //TODO: 補完屬性
                            ID_144_STATUS_CHANGE_REP aCmd = new ID_144_STATUS_CHANGE_REP();
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.ActionStatus = VHActionStatusConverter(pairs["ActionStatus"]);
                            //aCmd.BatteryCapacity = uint.Parse(pairs["BatteryCapacity"]);
                            //aCmd.BatteryTemperature = int.Parse(pairs["BatteryTemperature"]);
                            //aCmd.BlockingStatus = VhStopSingleConverter(pairs["BlockingStatus"]);
                            //aCmd.ChargeStatus = VhChargeStatusConverter(pairs["ChargeStatus"]);
                            aCmd.CmdID = pairs["CmdID"];
                            aCmd.CSTID = pairs["CSTID"];

                            wrappers.ID = WrapperMessage.StatueChangeRepFieldNumber;
                            wrappers.StatueChangeRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd145_PowerOnoffResponse:
                        {
                            ID_145_POWER_OPE_RESPONSE aCmd = new ID_145_POWER_OPE_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.PowerOpeRespFieldNumber;
                            wrappers.PowerOpeResp = aCmd;

                            break;
                        }

                    case EnumCmdNums.Cmd171_RangeTeachResponse:
                        {
                            ID_171_RANGE_TEACHING_RESPONSE aCmd = new ID_171_RANGE_TEACHING_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.RangeTeachingRespFieldNumber;
                            wrappers.RangeTeachingResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd172_RangeTeachCompleteReport:
                        {
                            ID_172_RANGE_TEACHING_COMPLETE_REPORT aCmd = new ID_172_RANGE_TEACHING_COMPLETE_REPORT();
                            aCmd.CompleteCode = int.Parse(pairs["CompleteCode"]);
                            aCmd.FromAdr = pairs["FromAdr"];
                            aCmd.SecDistance = uint.Parse(pairs["SecDistance"]);
                            aCmd.ToAdr = pairs["ToAdr"];

                            wrappers.ID = WrapperMessage.RangeTeachingCmpRepFieldNumber;
                            wrappers.RangeTeachingCmpRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd174_AddressTeachReport:
                        {
                            ID_174_ADDRESS_TEACH_REPORT aCmd = new ID_174_ADDRESS_TEACH_REPORT();
                            aCmd.Addr = pairs["Addr"];
                            aCmd.Position = int.Parse(pairs["Position"]);

                            wrappers.ID = WrapperMessage.AddressTeachRepFieldNumber;
                            wrappers.AddressTeachRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd191_AlarmResetResponse:
                        {
                            ID_191_ALARM_RESET_RESPONSE aCmd = new ID_191_ALARM_RESET_RESPONSE();
                            aCmd.ReplyCode = int.Parse(pairs["ReplyCode"]);

                            wrappers.ID = WrapperMessage.AlarmResetRespFieldNumber;
                            wrappers.AlarmResetResp = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd194_AlarmReport:
                        {
                            ID_194_ALARM_REPORT aCmd = new ID_194_ALARM_REPORT();
                            aCmd.ErrCode = pairs["ErrCode"];
                            aCmd.ErrDescription = pairs["ErrDescription"];
                            aCmd.ErrStatus = ErrorStatusConverter(pairs["ErrStatus"]);

                            wrappers.ID = WrapperMessage.AlarmRepFieldNumber;
                            wrappers.AlarmRep = aCmd;

                            break;
                        }
                    case EnumCmdNums.Cmd000_EmptyCommand:
                    default:
                        {
                            ID_1_HOST_BASIC_INFO_VERSION_REP aCmd = new ID_1_HOST_BASIC_INFO_VERSION_REP();

                            wrappers.ID = WrapperMessage.HostBasicInfoRepFieldNumber;
                            wrappers.HostBasicInfoRep = aCmd;

                            break;
                        }
                }

                SendCommandWrapper(wrappers);  //似乎是SendFunction底層會咬住等待回應所以開THD去發  

                //OnCmdSend?.Invoke(this, wrappers.ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

        }


        private void AddRangeToGuideList(RepeatedField<string> guideAddressesToDestination, string pairValue)
        {
            pairValue = pairValue.Trim(new char[] { ' ', '[', ']', ',' });

            if (string.IsNullOrWhiteSpace(pairValue))
            {
                guideAddressesToDestination = new RepeatedField<string>();
            }
            else
            {
                var pairValues = pairValue.Split(',');
                for (int i = 0; i < pairValues.Length; i++)
                {
                    pairValues[i] = pairValues[i].Trim();
                }

                guideAddressesToDestination.AddRange(pairValues);
            }
        }

        //private VhChargeStatus VhChargeStatusConverter(string v)
        //{
        //    try
        //    {
        //        v = v.Trim();

        //        return (VhChargeStatus)Enum.Parse(typeof(VhChargeStatus), v);
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.StackTrace;
        //        return VhChargeStatus.ChargeStatusCharging;
        //    }
        //}

        private VhStopSingle VhStopSingleConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (VhStopSingle)Enum.Parse(typeof(VhStopSingle), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return VhStopSingle.StopSingleOff;
            }
        }

        private VHActionStatus VHActionStatusConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (VHActionStatus)Enum.Parse(typeof(VHActionStatus), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return VHActionStatus.Commanding;
            }
        }

        private EventType EventTypeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (EventType)Enum.Parse(typeof(EventType), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return EventType.AdrOrMoveArrivals;
            }
        }

        private CompleteStatus CompleteStatusConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (CompleteStatus)Enum.Parse(typeof(CompleteStatus), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return CompleteStatus.CmpStatusAbort;
            }
        }

        private OperatingPowerMode OperatingPowerModeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (OperatingPowerMode)Enum.Parse(typeof(OperatingPowerMode), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return OperatingPowerMode.OperatingPowerOff;
            }
        }

        private OperatingVHMode OperatingVHModeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (OperatingVHMode)Enum.Parse(typeof(OperatingVHMode), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return OperatingVHMode.OperatingAuto;
            }
        }

        private PauseType PauseTypeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (PauseType)Enum.Parse(typeof(PauseType), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return PauseType.None;
            }
        }

        private PauseEvent PauseEventConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (PauseEvent)Enum.Parse(typeof(PauseEvent), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return PauseEvent.Pause;
            }
        }

        private CMDCancelType CMDCancelTypeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (CMDCancelType)Enum.Parse(typeof(CMDCancelType), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return CMDCancelType.CmdAbout;
            }
        }


        private PassType PassTypeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (PassType)Enum.Parse(typeof(PassType), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return PassType.Pass;
            }
        }

        private ErrorStatus ErrorStatusConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (ErrorStatus)Enum.Parse(typeof(ErrorStatus), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return ErrorStatus.ErrReset;
            }
        }

        private ActiveType ActiveTypeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (ActiveType)Enum.Parse(typeof(ActiveType), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return ActiveType.Home;
            }
        }

        private ControlType ControlTypeConverter(string v)
        {
            try
            {
                v = v.Trim();

                return (ControlType)Enum.Parse(typeof(ControlType), v);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
                return ControlType.Nothing;
            }
        }

        public void Receive_Cmd94_AlarmResponse(object sender, TcpIpEventArgs e)
        {
            ID_94_ALARM_RESPONSE receive = (ID_94_ALARM_RESPONSE)e.objPacket;

        }
        public void Send_Cmd194_AlarmReport(string alarmCode, ErrorStatus status)
        {
            try
            {
                //TODO: Report alram
                ID_194_ALARM_REPORT iD_194_ALARM_REPORT = new ID_194_ALARM_REPORT();
                iD_194_ALARM_REPORT.ErrCode = alarmCode;
                iD_194_ALARM_REPORT.ErrStatus = status;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.AlarmRepFieldNumber;
                wrappers.AlarmRep = iD_194_ALARM_REPORT;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }
        public void Receive_Cmd194_AlarmReport(object sender, TcpIpEventArgs e)
        {
            try
            {
                ID_94_ALARM_RESPONSE reply = new ID_94_ALARM_RESPONSE();
                reply.ReplyCode = 0;

                WrapperMessage wrapper = new WrapperMessage();
                wrapper.ID = WrapperMessage.AlarmRespFieldNumber;
                wrapper.SeqNum = e.iSeqNum;
                wrapper.AlarmResp = reply;

                SendCommandWrapper(wrapper, true);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Receive_Cmd91_AlarmResetRequest(object sender, TcpIpEventArgs e)
        {
            ID_91_ALARM_RESET_REQUEST receive = (ID_91_ALARM_RESET_REQUEST)e.objPacket;
            //TODO: Reset alarm

            int replyCode = 0;
            Send_Cmd191_AlarmResetResponse(e.iSeqNum, replyCode);
            SpinWait.SpinUntil(() => false, 1000);
            Send_Cmd194_AlarmReport("0", ErrorStatus.ErrReset);
        }
        public void Send_Cmd191_AlarmResetResponse(ushort seqNum, int replyCode)
        {
            try
            {
                ID_191_ALARM_RESET_RESPONSE iD_191_ALARM_RESET_RESPONSE = new ID_191_ALARM_RESET_RESPONSE();
                iD_191_ALARM_RESET_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.AlarmResetRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.AlarmResetResp = iD_191_ALARM_RESET_RESPONSE;

                SendCommandWrapper(wrappers, true);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Receive_Cmd74_AddressTeachResponse(object sender, TcpIpEventArgs e)
        {
            ID_74_ADDRESS_TEACH_RESPONSE receive = (ID_74_ADDRESS_TEACH_RESPONSE)e.objPacket;


        }
        public void Send_Cmd174_AddressTeachReport(string addressId, int position)
        {
            try
            {
                //TODO: Teaching port address

                ID_174_ADDRESS_TEACH_REPORT iD_174_ADDRESS_TEACH_REPORT = new ID_174_ADDRESS_TEACH_REPORT();
                iD_174_ADDRESS_TEACH_REPORT.Addr = addressId;
                iD_174_ADDRESS_TEACH_REPORT.Position = position;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.AddressTeachRepFieldNumber;
                wrappers.AddressTeachRep = iD_174_ADDRESS_TEACH_REPORT;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Receive_Cmd72_RangeTeachCompleteResponse(object sender, TcpIpEventArgs e)
        {
            ID_72_RANGE_TEACHING_COMPLETE_RESPONSE receive = (ID_72_RANGE_TEACHING_COMPLETE_RESPONSE)e.objPacket;


        }
        public void Send_Cmd172_RangeTeachCompleteReport(int completeCode)
        {
            //VehLocation vehLocation = theVehicle.GetVehLoacation();

            try
            {
                //TODO: After Teaching Complete

                ID_172_RANGE_TEACHING_COMPLETE_REPORT iD_172_RANGE_TEACHING_COMPLETE_REPORT = new ID_172_RANGE_TEACHING_COMPLETE_REPORT();
                iD_172_RANGE_TEACHING_COMPLETE_REPORT.CompleteCode = completeCode;
                //iD_172_RANGE_TEACHING_COMPLETE_REPORT.FromAdr = theVehicle.TeachingFromAddress;
                //iD_172_RANGE_TEACHING_COMPLETE_REPORT.ToAdr = theVehicle.TeachingToAddress;
                //iD_172_RANGE_TEACHING_COMPLETE_REPORT.SecDistance = (uint)vehLocation.Section.Distance;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.RangeTeachingCmpRepFieldNumber;
                wrappers.RangeTeachingCmpRep = iD_172_RANGE_TEACHING_COMPLETE_REPORT;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Receive_Cmd71_RangeTeachRequest(object sender, TcpIpEventArgs e)
        {
            ID_71_RANGE_TEACHING_REQUEST receive = (ID_71_RANGE_TEACHING_REQUEST)e.objPacket;
            //TODO: Teaching Section Address Head/End

            int replyCode = 0;
            Send_Cmd171_RangeTeachResponse(e.iSeqNum, replyCode);
        }
        public void Send_Cmd171_RangeTeachResponse(ushort seqNum, int replyCode)
        {
            try
            {
                ID_171_RANGE_TEACHING_RESPONSE iD_171_RANGE_TEACHING_RESPONSE = new ID_171_RANGE_TEACHING_RESPONSE();
                iD_171_RANGE_TEACHING_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.RangeTeachingRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.RangeTeachingResp = iD_171_RANGE_TEACHING_RESPONSE;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }


        public void Receive_Cmd45_PowerOnoffRequest(object sender, TcpIpEventArgs e)
        {
            ID_45_POWER_OPE_REQ receive = (ID_45_POWER_OPE_REQ)e.objPacket;
            //TODO: PowerOn/PowerOff

            int replyCode = 0;
            Send_Cmd145_PowerOnoffResponse(e.iSeqNum, replyCode);
        }
        public void Send_Cmd145_PowerOnoffResponse(ushort seqNum, int replyCode)
        {
            try
            {
                ID_145_POWER_OPE_RESPONSE iD_145_POWER_OPE_RESPONSE = new ID_145_POWER_OPE_RESPONSE();
                iD_145_POWER_OPE_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.PowerOpeRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.PowerOpeResp = iD_145_POWER_OPE_RESPONSE;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Send_Cmd144_StatusChangeReport(int sequenceNum)
        {
            try
            {
                ID_144_STATUS_CHANGE_REP iD_144_SendToOHTC = new ID_144_STATUS_CHANGE_REP();
                iD_144_SendToOHTC = vehicle.Vehicle_Data;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.StatueChangeRepFieldNumber;
                wrappers.StatueChangeRep = iD_144_SendToOHTC;
                wrappers.SeqNum = sequenceNum;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

        }

        private void Receive_Cmd43_StatusRequest(object sender, TcpIpEventArgs e)
        {
            ID_43_STATUS_REQUEST receive = (ID_43_STATUS_REQUEST)e.objPacket; // Cmd43's object is empty

            Send_Cmd143_StatusResponse(e.iSeqNum);
        }
        public void Send_Cmd143_StatusResponse(ushort seqNum)
        {
            //Battery battery = theVehicle.GetBattery();
            //TransCmd transCmd = theVehicle.GetTransCmd();
            //VehLocation vehLocation = theVehicle.GetVehLoacation();

            try
            {
                ID_143_STATUS_RESPONSE iD_143_STATUS_RESPONSE = new ID_143_STATUS_RESPONSE();
                iD_143_STATUS_RESPONSE.ActionStatus = theVehicleInfo.ActionStatus;
                iD_143_STATUS_RESPONSE.BlockingStatus = theVehicleInfo.BlockingStatus;
                iD_143_STATUS_RESPONSE.CmdID = theVehicleInfo.CmdID;
                iD_143_STATUS_RESPONSE.CSTID = theVehicleInfo.CSTID;
                iD_143_STATUS_RESPONSE.CurrentAdrID = theVehicleInfo.CurrentAdrID;
                iD_143_STATUS_RESPONSE.CurrentSecID = theVehicleInfo.CurrentSecID;
                iD_143_STATUS_RESPONSE.ErrorStatus = theVehicleInfo.ErrorStatus;
                iD_143_STATUS_RESPONSE.ModeStatus = theVehicleInfo.ModeStatus;
                iD_143_STATUS_RESPONSE.ObstacleStatus = theVehicleInfo.ObstacleStatus;
                iD_143_STATUS_RESPONSE.ObstDistance = theVehicleInfo.ObstDistance;
                iD_143_STATUS_RESPONSE.ObstVehicleID = theVehicleInfo.ObstVehicleID;
                iD_143_STATUS_RESPONSE.PauseStatus = theVehicleInfo.PauseStatus;
                iD_143_STATUS_RESPONSE.PowerStatus = theVehicleInfo.PowerStatus;
                iD_143_STATUS_RESPONSE.SecDistance = theVehicleInfo.SecDistance;
                iD_143_STATUS_RESPONSE.StoppedBlockID = theVehicleInfo.StoppedBlockID;
                iD_143_STATUS_RESPONSE.HasCST = theVehicleInfo.HasCST;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.StatusReqRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.StatusReqResp = iD_143_STATUS_RESPONSE;

                SendCommandWrapper(wrappers, true);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

        }

        public void Receive_Cmd41_ModeChange(object sender, TcpIpEventArgs e)
        {
            string msg = $"[Cmd_41_RECEIVE] [PacketID = {e.iPacketID}][SeqNum = {e.iSeqNum}][Pt = {e.iPt}][ObjPacket = {e.objPacket}]";
            OnCmdReceive?.Invoke(this, msg);
            theLoggerAgent.LogMsg("Comm", new LogFormat("Comm", "1", GetType().Name + ":" + MethodBase.GetCurrentMethod().Name, "Device", "CarrierID"
                , msg));


            ID_41_MODE_CHANGE_REQ receive = (ID_41_MODE_CHANGE_REQ)e.objPacket;
            //TODO: Auto/Manual
            switch (receive.OperatingVHMode)
            {
                case OperatingVHMode.OperatingAuto:
                    theVehicleInfo.ModeStatus = VHModeStatus.AutoRemote;
                    vehicle.ChangeDataOfVehicle = theVehicleInfo;
                    break;
                case OperatingVHMode.OperatingManual:
                    theVehicleInfo.ModeStatus = VHModeStatus.AutoRemote;
                    vehicle.ChangeDataOfVehicle = theVehicleInfo;
                    break;
            }
            int replyCode = 0;
            Send_Cmd141_ModeChangeResponse(e.iSeqNum, replyCode);
            Send_Cmd144_StatusChangeReport(e.iSeqNum);
        }
        public void Send_Cmd141_ModeChangeResponse(ushort seqNum, int replyCode)
        {
            try
            {
                ID_141_MODE_CHANGE_RESPONSE iD_141_MODE_CHANGE_RESPONSE = new ID_141_MODE_CHANGE_RESPONSE();
                iD_141_MODE_CHANGE_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.ModeChangeRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.ModeChangeResp = iD_141_MODE_CHANGE_RESPONSE;

                SendCommandWrapper(wrappers, true);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }
        public void Receive_Cmd141_ModeChange(object sender, TcpIpEventArgs e)
        {
            ID_141_MODE_CHANGE_RESPONSE receive = (ID_141_MODE_CHANGE_RESPONSE)e.objPacket;
            //TODO: Auto/Manual


            //Send_Cmd141_ModeChangeResponse(e.iSeqNum, receive.ReplyCode);
        }

        public void Receive_Cmd39_PauseRequest(object sender, TcpIpEventArgs e)
        {
            ID_39_PAUSE_REQUEST receive = (ID_39_PAUSE_REQUEST)e.objPacket;
            //TODO: Pause/Continue+/Reserve

            int replyCode = 0;
            Send_Cmd139_PauseResponse(e.iSeqNum, replyCode);
        }
        public void Send_Cmd139_PauseResponse(ushort seqNum, int replyCode)
        {
            try
            {
                ID_139_PAUSE_RESPONSE iD_139_PAUSE_RESPONSE = new ID_139_PAUSE_RESPONSE();
                //iD_139_PAUSE_RESPONSE.EventType = theVehicle.Cmd139EventType;
                iD_139_PAUSE_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.PauseRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.PauseResp = iD_139_PAUSE_RESPONSE;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Receive_Cmd37_TransferCancelRequest(object sender, TcpIpEventArgs e)
        {

            ID_37_TRANS_CANCEL_REQUEST receive = (ID_37_TRANS_CANCEL_REQUEST)e.objPacket;
            bool result = true;
            //switch (receive.ActType)
            //{
            //    case CMDCancelType.CmdCancel:
            //        if (CanVehCancel())
            //        {
            //            result = true;
            //            if (OnTransferCancelEvent != null)
            //            {
            //                OnTransferCancelEvent(this, receive.CmdID);
            //            }
            //        }
            //        break;
            //    case CMDCancelType.CmdAbort:
            //        if (CanVehAbort())
            //        {
            //            result = true;
            //            if (OnTransferAbortEvent != null)
            //            {
            //                OnTransferAbortEvent(this, receive.CmdID);
            //            }
            //        }
            //        break;
            //    default:
            //        break;
            //}

            int replyCode = result ? 0 : 1;
            Send_Cmd137_TransferCancelResponse(e.iSeqNum, replyCode);
            operaObject.StartFeedBack37(receive, theVehicleInfo, theLoggerAgent, ServerClientAgent, OnCmdSend);
        }
        public void Send_Cmd137_TransferCancelResponse(ushort seqNum, int replyCode)
        {
            try
            {
                ID_137_TRANS_CANCEL_RESPONSE iD_137_TRANS_CANCEL_RESPONSE = new ID_137_TRANS_CANCEL_RESPONSE();
                //iD_137_TRANS_CANCEL_RESPONSE.CmdID = theVehicle.CmdID;
                //iD_137_TRANS_CANCEL_RESPONSE.ActType = theVehicle.Cmd137ActType;
                iD_137_TRANS_CANCEL_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.TransCancelRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.TransCancelResp = iD_137_TRANS_CANCEL_RESPONSE;

                SendCommandWrapper(wrappers, true);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        private bool CanVehAbort()
        {
            throw new NotImplementedException();
        }
        private bool CanVehCancel()
        {
            throw new NotImplementedException();
        }

        public void Receive_Cmd36_TransferEventResponse(object sender, TcpIpEventArgs e)
        {
            ID_36_TRANS_EVENT_RESPONSE receive = (ID_36_TRANS_EVENT_RESPONSE)e.objPacket;
            //Get reserve, block, 
        }
        public void Send_Cmd36_ReservePass(string sectionId, bool isSuccess)
        {
            ID_36_TRANS_EVENT_RESPONSE response = new ID_36_TRANS_EVENT_RESPONSE();
            //FitReserveInfos(response.ReserveInfos, sectionId);
            //response. = EventType.ReserveReq;

            WrapperMessage wrappers = new WrapperMessage();
            wrappers.ID = WrapperMessage.ImpTransEventRespFieldNumber;
            wrappers.ImpTransEventResp = response;

            SendCommandWrapper(wrappers);
        }


        private void Receive_Cmd136_TransferEventReport(object sender, TcpIpEventArgs e)
        {
            ID_136_TRANS_EVENT_REP receive = (ID_136_TRANS_EVENT_REP)e.objPacket;

            try
            {
                ID_36_TRANS_EVENT_RESPONSE response = new ID_36_TRANS_EVENT_RESPONSE();
                //response. = receive.EventType;
                if (receive.EventType == EventType.Bcrread)
                {
                    if (receive.BCRReadResult == BCRReadResult.BcrMisMatch)
                    {
                        response.ReplyActiveType = CMDCancelType.CmdCancelIdMismatch;
                        //response.RenameCarrierID = "TestMismatch";
                    }
                    else if (receive.BCRReadResult == BCRReadResult.BcrReadFail)
                    {
                        response.ReplyActiveType = CMDCancelType.CmdCancelIdReadFailed;
                        //response.RenameCarrierID = "TestFailXXX";
                    }
                    else
                    {
                        response.ReplyActiveType = CMDCancelType.CmdNone;
                    }
                }

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.ImpTransEventRespFieldNumber;
                wrappers.SeqNum = e.iSeqNum;
                wrappers.ImpTransEventResp = response;

                //if (receive.EventType!= EventType.ReserveReq)
                //{
                //    SendCommandWrapper(wrappers, true);
                //}
                SendCommandWrapper(wrappers, true);
                //SendCommandWrapper(wrappers, true);
                //SendCommandWrapper(wrappers, true);
                //SendCommandWrapper(wrappers, true);

            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Send_Cmd136_TransferEventReport(EventType eventType, bool isCmd = false, ID_31_TRANS_REQUEST now31Cmd = null)
        {
            //VehLocation vehLocation = theVehicle.GetVehLoacation();
            try
            {
                ID_136_TRANS_EVENT_REP iD_136_TRANS_EVENT_REP = new ID_136_TRANS_EVENT_REP();

                iD_136_TRANS_EVENT_REP.EventType = eventType;

                iD_136_TRANS_EVENT_REP.CSTID = theVehicleInfo.CSTID;
                iD_136_TRANS_EVENT_REP.CurrentAdrID = theVehicleInfo.CurrentAdrID;
                iD_136_TRANS_EVENT_REP.CurrentSecID = theVehicleInfo.CurrentSecID;

                iD_136_TRANS_EVENT_REP.SecDistance = 0;
                iD_136_TRANS_EVENT_REP.BCRReadResult = BCRReadResult.BcrNormal;


                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.ImpTransEventRepFieldNumber;
                wrappers.ImpTransEventRep = iD_136_TRANS_EVENT_REP;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

        }
        public void Send_Cmd136_TransferEventReport(EventType eventType, string requestBlockID, string releaseBlockAdrID)
        {
            //VehLocation vehLocation = theVehicle.GetVehLoacation();

            try
            {
                ID_136_TRANS_EVENT_REP iD_136_TRANS_EVENT_REP = new ID_136_TRANS_EVENT_REP();
                iD_136_TRANS_EVENT_REP.EventType = eventType;
                iD_136_TRANS_EVENT_REP.RequestBlockID = requestBlockID;
                //iD_136_TRANS_EVENT_REP.CSTID = theVehicle.CarrierID;
                iD_136_TRANS_EVENT_REP.ReleaseBlockAdrID = releaseBlockAdrID;
                //iD_136_TRANS_EVENT_REP.CurrentAdrID = vehLocation.Address.Id;
                //iD_136_TRANS_EVENT_REP.CurrentSecID = vehLocation.Section.Id;
                //iD_136_TRANS_EVENT_REP.SecDistance = (uint)vehLocation.Section.Distance;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.ImpTransEventRepFieldNumber;
                wrappers.ImpTransEventRep = iD_136_TRANS_EVENT_REP;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        //public void Receive_Cmd35_CarrierIdRenameRequest(object sender, TcpIpEventArgs e)
        //{
        //    ID_35_CST_ID_RENAME_REQUEST receive = (ID_35_CST_ID_RENAME_REQUEST)e.objPacket;
        //}
        //public void Send_Cmd135_CarrierIdRenameResponse(ushort seqNum, int replyCode)
        //{
        //    try
        //    {
        //        ID_135_CST_ID_RENAME_RESPONSE iD_135_CST_ID_RENAME_RESPONSE = new ID_135_CST_ID_RENAME_RESPONSE();
        //        iD_135_CST_ID_RENAME_RESPONSE.ReplyCode = replyCode;

        //        WrapperMessage wrappers = new WrapperMessage();
        //        wrappers.ID = WrapperMessage.CSTIDRenameRespFieldNumber;
        //        wrappers.SeqNum = seqNum;
        //        wrappers.CSTIDRenameResp = iD_135_CST_ID_RENAME_RESPONSE;

        //        SendCommandWrapper(wrappers);
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.StackTrace;
        //    }
        //}

        public void Send_Cmd134_TransferEventReport()
        {
            //VehLocation vehLocation = theVehicle.GetVehLoacation();

            try
            {
                ID_134_TRANS_EVENT_REP iD_134_TRANS_EVENT_REP = new ID_134_TRANS_EVENT_REP();
                //iD_134_TRANS_EVENT_REP.EventType = theVehicle.Cmd134EventType;
                //iD_134_TRANS_EVENT_REP.CurrentAdrID = vehLocation.Address.Id;
                //iD_134_TRANS_EVENT_REP.CurrentSecID = vehLocation.Section.Id;
                //iD_134_TRANS_EVENT_REP.SecDistance = (uint)vehLocation.Section.Distance;
                //iD_134_TRANS_EVENT_REP.DrivingDirection = theVehicle.DrivingDirection;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.TransEventRepFieldNumber;
                wrappers.TransEventRep = iD_134_TRANS_EVENT_REP;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }
        public void Receive_Cmd134_TransferEventReport(object sender, TcpIpEventArgs e)
        {
            ID_134_TRANS_EVENT_REP receive = (ID_134_TRANS_EVENT_REP)e.objPacket;
            switch (receive.EventType)
            {
                case EventType.LoadArrivals:
                    break;
                case EventType.LoadComplete:
                    break;
                case EventType.UnloadArrivals:
                    AForm.UnloadArrival();
                    break;
                case EventType.UnloadComplete:
                    break;
                case EventType.AdrOrMoveArrivals:
                    break;
                case EventType.AdrPass:
                    break;
                case EventType.BlockReq:
                    break;
                case EventType.Vhloading:
                    break;
                case EventType.Vhunloading:
                    break;
                case EventType.Bcrread:
                    break;
                case EventType.BlockRelease:
                    break;
                default:
                    break;
            }
        }

        public void Receive_Cmd33_ControlZoneCancelRequest(object sender, TcpIpEventArgs e)
        {

            ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST receive = (ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST)e.objPacket;

            switch (receive.ControlType)
            {
                case ControlType.Nothing:
                    break;
                case ControlType.Block:
                    break;
                case ControlType.Hid:
                    break;
                default:
                    break;
            }

            int replyCode = 1;
            Send_Cmd133_ControlZoneCancelResponse(e.iSeqNum, receive.ControlType, receive.CancelSecID, replyCode);
        }
        public void Send_Cmd133_ControlZoneCancelResponse(ushort seqNum, ControlType controlType, string cancelSecID, int replyCode)
        {
            try
            {
                ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE iD_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE = new ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE();
                iD_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE.ControlType = controlType;
                iD_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE.CancelSecID = cancelSecID;
                iD_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE.ReplyCode = replyCode;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.ControlZoneRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.ControlZoneResp = iD_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        public void Receive_Cmd32_TransferCompleteResponse(object sender, TcpIpEventArgs e)
        {
            ID_32_TRANS_COMPLETE_RESPONSE receive = (ID_32_TRANS_COMPLETE_RESPONSE)e.objPacket;
        }
        public void Send_Cmd132_TransferCompleteReport()
        {
            //TransCmd transCmd = theVehicle.GetTransCmd();
            //VehLocation vehLocation = theVehicle.GetVehLoacation();

            try
            {
                ID_132_TRANS_COMPLETE_REPORT iD_132_TRANS_COMPLETE_REPORT = new ID_132_TRANS_COMPLETE_REPORT();

                iD_132_TRANS_COMPLETE_REPORT.CurrentSecID = theVehicleInfo.CurrentSecID;
                iD_132_TRANS_COMPLETE_REPORT.CurrentAdrID = theVehicleInfo.CurrentAdrID;
                iD_132_TRANS_COMPLETE_REPORT.CmdDistance = 0;
                iD_132_TRANS_COMPLETE_REPORT.CmdID = theVehicleInfo.CmdID;
                iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;
                iD_132_TRANS_COMPLETE_REPORT.HasCST = VhLoadCSTStatus.NotExist;
                iD_132_TRANS_COMPLETE_REPORT.CarCSTID = "";

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.TranCmpRepFieldNumber;
                wrappers.TranCmpRep = iD_132_TRANS_COMPLETE_REPORT;

                SendCommandWrapper(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }
        private void Receive_Cmd132_TransferEventReport(object sender, TcpIpEventArgs e)
        {
            ID_132_TRANS_COMPLETE_REPORT receive = (ID_132_TRANS_COMPLETE_REPORT)e.objPacket;
            switch (receive.CmpStatus)
            {
                case CompleteStatus.CmpStatusMove:
                    break;
                case CompleteStatus.CmpStatusLoad:
                    break;
                case CompleteStatus.CmpStatusUnload:
                    break;
                case CompleteStatus.CmpStatusLoadunload:
                    AForm.LoopNext();
                    break;
                case CompleteStatus.CmpStatusHome:
                    break;
                case CompleteStatus.CmpStatusOverride:
                    break;
                case CompleteStatus.CmpStatusCstIdrenmae:
                    break;
                case CompleteStatus.CmpStatusMtlhome:
                    break;
                case CompleteStatus.CmpStatusSystemOut:
                    break;
                case CompleteStatus.CmpStatusSystemIn:
                    break;
                case CompleteStatus.CmpStatusTechingMove:
                    break;
                case CompleteStatus.CmpStatusCancel:
                    break;
                case CompleteStatus.CmpStatusAbort:
                    break;
                case CompleteStatus.CmpStatusVehicleAbort:
                    break;
                case CompleteStatus.CmpStatusIdmisMatch:
                    break;
                case CompleteStatus.CmpStatusIdreadFailed:
                    break;
                case CompleteStatus.CmpStatusInterlockError:
                    break;
                default:
                    break;
            }

        }

        public void Receive_Cmd31_TransferRequest(object sender, TcpIpEventArgs e)
        {
            ID_31_TRANS_REQUEST transRequest = (ID_31_TRANS_REQUEST)e.objPacket;
            //Need to check reply ok or no
            Send_Cmd131_TransferResponse(transRequest, e.iSeqNum, 0);
            operaObject.StartFeedBack31(transRequest, theVehicleInfo, theLoggerAgent, ServerClientAgent, OnCmdSend);
        }

        public void Send_Cmd131_TransferResponse(ID_31_TRANS_REQUEST iD_31_TRANS_REQUEST, ushort seqNum, int replyCode, string reason = "")
        {
            //TransCmd transCmd = theVehicle.GetTransCmd();

            try
            {
                ID_131_TRANS_RESPONSE iD_131_TRANS_RESPONSE = new ID_131_TRANS_RESPONSE();
                iD_131_TRANS_RESPONSE.CmdID = iD_31_TRANS_REQUEST.CmdID;
                //iD_131_TRANS_RESPONSE.ActType = theVehicle.Cmd131ActType;
                iD_131_TRANS_RESPONSE.ReplyCode = replyCode;
                iD_131_TRANS_RESPONSE.NgReason = reason;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.TransRespFieldNumber;
                wrappers.SeqNum = seqNum;
                wrappers.TransResp = iD_131_TRANS_RESPONSE;

                SendCommandWrapper(wrappers, true);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        //private bool CanVehDoTransfer(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    if (theVehicle.GetBattery().IsBatteryLowPower())
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle is in low power can not do transfer command.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (theVehicle.GetBattery().IsBatteryHighTemperature())
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle is in battery temperature too hight can not do transfer command.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }

        //    var type = transRequest.ActType;
        //    switch (type)
        //    {
        //        case ActiveType.Move:
        //            return CanVehMove(transRequest, seqNum);
        //        case ActiveType.Load:
        //            return CanVehLoad(transRequest, seqNum);
        //        case ActiveType.Unload:
        //            return CanVehUnload(transRequest, seqNum);
        //        case ActiveType.Loadunload:
        //            return CanVehLoadunload(transRequest, seqNum);
        //        case ActiveType.Home:
        //            return CanVehHome(transRequest, seqNum);
        //        case ActiveType.Override:
        //            return CanVehOverride(transRequest, seqNum);
        //        case ActiveType.Mtlhome:
        //        case ActiveType.Movetomtl:
        //        case ActiveType.Systemout:
        //        case ActiveType.Systemin:
        //        case ActiveType.Techingmove:
        //        case ActiveType.Round:
        //        default:
        //            int replyCode = 0; // OK
        //            string reason = "Empty";
        //            Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //            return true;
        //    }
        //}

        //private bool CanVehOverride(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    if (VehInOverrideSection(transRequest))
        //    {
        //        int replyCode = 0; // OK
        //        string reason = "Empty";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return true;
        //    }
        //    else
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle current section not in override guideSections.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //}

        //private bool VehInOverrideSection(ID_31_TRANS_REQUEST transRequest)
        //{
        //    var location = theVehicle.GetVehLoacation();
        //    var curSectionId = location.Section.Id;

        //    var isInToLoadSections = false;
        //    if (transRequest.GuideAddressesStartToLoad != null)
        //    {
        //        var toLoadSections = transRequest.GuideSectionsStartToLoad.ToList();
        //        isInToLoadSections = toLoadSections.Contains(curSectionId);
        //    }

        //    var isInToUnloadSections = false;
        //    if (transRequest.GuideSectionsToDestination != null)
        //    {
        //        var toUnloadSections = transRequest.GuideSectionsToDestination.ToList();
        //        isInToUnloadSections = toUnloadSections.Contains(curSectionId);
        //    }

        //    return isInToLoadSections || isInToUnloadSections;
        //}

        //private bool CanVehHome(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    int replyCode = 0; // OK
        //    string reason = "Empty";
        //    Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //    return true;
        //}

        //private bool CanVehLoadunload(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    if (theVehicle.ActionStatus != VHActionStatus.NoCommand)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle is not idle can not do loadunload.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (theVehicle.HasCst == VhLoadCSTStatus.Exist)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle has a carrier can not do loadunload.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (string.IsNullOrEmpty(transRequest.LoadAdr))
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Transfer command has no load address can not do load.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (string.IsNullOrEmpty(transRequest.DestinationAdr))
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Transfer command has no unload address can not do unload.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else
        //    {
        //        int replyCode = 0; // OK
        //        string reason = "Empty";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return true;
        //    }
        //}

        //private bool CanVehUnload(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    if (theVehicle.ActionStatus != VHActionStatus.NoCommand)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle is not idle can not do unload.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (theVehicle.HasCst == VhLoadCSTStatus.NotExist)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle has no carrier can not do unload.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (string.IsNullOrEmpty(transRequest.DestinationAdr))
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Transfer command has no unload address can not do unload.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }

        //    else
        //    {
        //        int replyCode = 0; // OK
        //        string reason = "Empty";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return true;
        //    }
        //}

        //private bool CanVehLoad(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    if (theVehicle.ActionStatus != VHActionStatus.NoCommand)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle is not idle can not do load.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (theVehicle.HasCst == VhLoadCSTStatus.Exist)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle has a carrier can not do load.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (string.IsNullOrEmpty(transRequest.LoadAdr))
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Transfer command has no load address can not do load.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else
        //    {
        //        int replyCode = 0; // OK
        //        string reason = "Empty";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return true;
        //    }
        //}

        //private bool CanVehMove(ID_31_TRANS_REQUEST transRequest, ushort seqNum)
        //{
        //    if (theVehicle.ActionStatus != VHActionStatus.NoCommand)
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Vehicle is not idle can not do move.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else if (string.IsNullOrEmpty(transRequest.DestinationAdr))
        //    {
        //        int replyCode = 1; // NG
        //        string reason = "Transfer command has no move-end address can not do move.";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return false;
        //    }
        //    else
        //    {
        //        int replyCode = 0; // OK
        //        string reason = "Empty";
        //        Send_Cmd131_TransferResponse(seqNum, replyCode, reason);
        //        return true;
        //    }
        //}

        //private AgvcTransCmd ConvertAgvcTransCmdIntoPackage(ID_31_TRANS_REQUEST transRequest, ushort iSeqNum)
        //{
        //    //解析收到的ID_31_TRANS_REQUEST並且填入AgvcTransCmd 
        //    switch (transRequest.ActType)
        //    {
        //        case ActiveType.Move:
        //            return new AgvcMoveCmd(transRequest, iSeqNum);
        //        case ActiveType.Load:
        //            return new AgvcLoadCmd(transRequest, iSeqNum);
        //        case ActiveType.Unload:
        //            return new AgvcUnloadCmd(transRequest, iSeqNum);
        //        case ActiveType.Loadunload:
        //            return new AgvcLoadunloadCmd(transRequest, iSeqNum);
        //        case ActiveType.Home:
        //            return new AgvcHomeCmd(transRequest, iSeqNum);
        //        case ActiveType.Override:
        //            return new AgvcOverrideCmd(transRequest, iSeqNum);
        //        case ActiveType.Mtlhome:
        //        case ActiveType.Movetomtl:
        //        case ActiveType.Systemout:
        //        case ActiveType.Systemin:
        //        case ActiveType.Techingmove:
        //        case ActiveType.Round:
        //        default:
        //            return new AgvcTransCmd(transRequest, iSeqNum);
        //    }
        //}

        public bool GetReserveFromAgvc(string sectionId)
        {
            throw new NotImplementedException();
        }

        //public void OnMapBarcodeValuesChangedEvent(object sender, MapBarcodeReader mapBarcodeValues)
        //{
        //    //vehLocation.SetMapBarcodeValues(mapBarcodeValues);
        //    //TODO: Make a Position change report from mapBarcode and send to AGVC
        //}

        //public void OnTransCmdsFinishedEvent(object sender, EnumCompleteStatus status)
        //{
        //    //Send Transfer Command Complete Report to Agvc
        //    theVehicle.CompleteStatus = (CompleteStatus)(int)status;
        //    Send_Cmd132_TransferCompleteReport();
        //}

        public void TestInvoke()
        {
            AForm.SendNextRandomLoadunloadCmd();
        }

        public bool OperatypeSet(EnumOperaType operaType)
        {
            cmdOpera = operaType;
            operaObject.Selected_Operate_Type(operaType);
            return true;
        }

    }

}
