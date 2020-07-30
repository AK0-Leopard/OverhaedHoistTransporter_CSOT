using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.iibg3k0.ttc.Common;
using com.mirle.iibg3k0.ttc.Common.TCPIP;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Mirle.Agvc.Simulator
{
    class Opera
    {
        private EventHandler<string> OnCmdSend;
        private EnumOperaType operaType;
        private LoggerAgent theLoggerAgent;
        private CMDCancelType cmdCanceltype = CMDCancelType.CmdNone;
        private ID_144_STATUS_CHANGE_REP theVehicleInfo;
        private TcpIpAgent ServerClientAgent;
        private ID_36_TRANS_EVENT_RESPONSE recent36Response;
        private ID_31_TRANS_REQUEST recent31Cmd;
        private ID_37_TRANS_CANCEL_REQUEST iD_37_TRANS_Cmd;
        private MiddlerConfigs middlerConfigs;

        object sendRecv_LockObj = new object();

        public bool Selected_Operate_Type(EnumOperaType selectedOpera)
        {
            bool isSuccess = true;
            operaType = selectedOpera;
            isSuccess = isSuccess && operaType == selectedOpera;
            return isSuccess;
        }

        public bool StartFeedBack31(ID_31_TRANS_REQUEST transRequest, ID_144_STATUS_CHANGE_REP theVehicleInfo,
                                    LoggerAgent theLoggerAgent, TcpIpAgent ServerClientAgent, EventHandler<string> OnCmdSend)
        {
            try
            {
                cmdCanceltype = CMDCancelType.CmdNone;
                recent31Cmd = transRequest;
                WaitForOneSecond();
                SetBasicInform(transRequest, theVehicleInfo, theLoggerAgent, ServerClientAgent, OnCmdSend);
                switch (operaType)
                {
                    case EnumOperaType.NormalComplete:
                        NormalComplete(transRequest);
                        break;
                    case EnumOperaType.CancelComplete:
                        CancelComplete(transRequest);
                        break;
                    case EnumOperaType.AbortComplete:
                        AbortComplete(transRequest);
                        break;
                    case EnumOperaType.Abnormal_BcrReadFail:
                        Abnormal_BcrReadFail(transRequest);
                        break;
                    case EnumOperaType.Abnormal_BcrMismatch:
                        Abnormal_BcrMismatch(transRequest);
                        break;
                    case EnumOperaType.Abnormal_BcrDuplicate:
                        Abnormal_BcrDuplicate(transRequest);
                        break;
                    case EnumOperaType.InterlockError:
                        InterlockError(transRequest);
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool StartFeedBack37(ID_37_TRANS_CANCEL_REQUEST transRequest, ID_144_STATUS_CHANGE_REP theVehicleInfo,
                                    LoggerAgent theLoggerAgent, TcpIpAgent ServerClientAgent, EventHandler<string> OnCmdSend)
        {
            try
            {
                iD_37_TRANS_Cmd = Deep_Clone<ID_37_TRANS_CANCEL_REQUEST>(transRequest);
                switch (iD_37_TRANS_Cmd.ActType)
                {
                    case CMDCancelType.CmdCancel:
                        cmdCanceltype = CMDCancelType.CmdCancel;
                        break;
                    case CMDCancelType.CmdAbout:
                        cmdCanceltype = CMDCancelType.CmdAbout;
                        break;
                }
                WaitForOneSecond();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode snedRecv<TSource>(WrapperMessage wrapper, out TSource stRecv, out string rtnMsg)
        {
            lock (sendRecv_LockObj)
            {
                TrxTcpIp.ReturnCode returncode;
                bool retrySendRecv = true;
                stRecv = default(TSource);
                rtnMsg = "";
                returncode = TrxTcpIp.ReturnCode.SendDataFail;
                while (retrySendRecv == true)
                {
                    returncode = ServerClientAgent.TrxTcpIp.sendRecv_Google(wrapper, out stRecv, out rtnMsg);
                    retrySendRecv = false;
                }
                return returncode;
            }
        }

        private static void WaitForOneSecond()
        {
            SpinWait.SpinUntil(() => false, 1000);
        }

        private void SetBasicInform(ID_31_TRANS_REQUEST transRequest, ID_144_STATUS_CHANGE_REP theVehicleInfo, LoggerAgent theLoggerAgent, TcpIpAgent ServerClientAgent, EventHandler<string> OnCmdSend)
        {
            this.theLoggerAgent = theLoggerAgent;
            this.theVehicleInfo = theVehicleInfo;
            this.ServerClientAgent = ServerClientAgent;
            this.OnCmdSend = OnCmdSend;

            theVehicleInfo.CmdID = transRequest.CmdID;
            theVehicleInfo.CSTID = transRequest.CSTID;
        }
        // All OK
        private void NormalComplete(ID_31_TRANS_REQUEST transRequest)
        {
            switch (transRequest.ActType)
            {
                case ActiveType.Move:
                    ChangeTheAddressSectionToUnloadAddress(transRequest);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Load:
                    LoadProcess(transRequest);
                    Send_Cmd136_TransferEventReport(EventType.Bcrread, true, transRequest);
                    SpinWait.SpinUntil(() => false, 1000);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Unload:
                    UnloadProcess(transRequest);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Loadunload:
                    SetVehicleStatus(VHActionStatus.Commanding);
                    Send_Cmd144_StatusChangeReport(false);
                    LoadProcess(transRequest);
                    Send_Cmd136_TransferEventReport(EventType.Bcrread, true, transRequest);
                    SpinWait.SpinUntil(() => false, 200);
                    UnloadProcess(transRequest);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    SetVehicleStatus(VHActionStatus.NoCommand);
                    Send_Cmd144_StatusChangeReport(false);
                    break;
            }

        }

        private void SetVehicleStatus(VHActionStatus vhActionStatus)
        {
            switch (vhActionStatus)
            {
                case VHActionStatus.Commanding:
                    theVehicleInfo.ActionStatus = VHActionStatus.Commanding;
                    break;
                case VHActionStatus.NoCommand:
                    theVehicleInfo.ActionStatus = VHActionStatus.NoCommand;
                    theVehicleInfo.CmdID = "";
                    theVehicleInfo.CSTID = "";
                    break;
            }

        }

        //TESTING
        private void CancelComplete(ID_31_TRANS_REQUEST transRequest)
        {
            switch (transRequest.ActType)
            {
                case ActiveType.Move:
                    ChangeTheAddressSectionToUnloadAddress(transRequest);
                    while (cmdCanceltype == CMDCancelType.CmdNone)
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }

                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Load:
                    ChangeTheAddressSectionToUnloadAddress(transRequest);
                    while (cmdCanceltype == CMDCancelType.CmdNone)
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Unload:
                    while (cmdCanceltype == CMDCancelType.CmdNone)
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                    UnloadProcess(transRequest);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Loadunload:
                    while (cmdCanceltype == CMDCancelType.CmdNone)
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
            }
        }
        //TESTING
        private void AbortComplete(ID_31_TRANS_REQUEST transRequest)
        {
            switch (transRequest.ActType)
            {
                case ActiveType.Unload:
                    while (cmdCanceltype == CMDCancelType.CmdNone)
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Loadunload:
                    LoadProcess(transRequest);
                    Send_Cmd136_TransferEventReport(EventType.Bcrread, true, transRequest);
                    SpinWait.SpinUntil(() => false, 1000);
                    while (cmdCanceltype == CMDCancelType.CmdNone)
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
            }
        }
        // Testing
        private void InterlockError(ID_31_TRANS_REQUEST transRequest)
        {
            switch (transRequest.ActType)
            {
                case ActiveType.Move:
                    ChangeTheAddressSectionToUnloadAddress(transRequest);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Load:
                    ChangeTheAddressSectionToLoadAddress(transRequest);
                    Send_Cmd136_TransferEventReport(EventType.LoadArrivals, true, transRequest);
                    SpinWait.SpinUntil(() => false, 1000);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Unload:
                    ChangeTheAddressSectionToUnloadAddress(transRequest);
                    Send_Cmd136_TransferEventReport(EventType.UnloadArrivals, true, transRequest);
                    SpinWait.SpinUntil(() => false, 1000);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
                case ActiveType.Loadunload:
                    ChangeTheAddressSectionToLoadAddress(transRequest);
                    Send_Cmd136_TransferEventReport(EventType.LoadArrivals, true, transRequest);
                    SpinWait.SpinUntil(() => false, 1000);
                    Send_Cmd132_TransferCompleteReport(transRequest);
                    break;
            }

        }
        // Testing
        private void Abnormal_BcrReadFail(ID_31_TRANS_REQUEST transRequest)
        {
            LoadProcess(transRequest);
            Send_Cmd136_TransferEventReport(EventType.Bcrread, true, transRequest, BCRReadResult.BcrReadFail);
            ID_36_TRANS_EVENT_RESPONSE the36Response = Deep_Clone<ID_36_TRANS_EVENT_RESPONSE>(recent36Response);
            if (the36Response.ReplyActiveType == CMDCancelType.CmdNone)
            {
                UnloadProcess(transRequest);
                Send_Cmd132_TransferCompleteReport(transRequest);
            }
            else if (the36Response.ReplyActiveType == CMDCancelType.CmdCancelIdReadFailed)
            {
                Send_Cmd132_TransferCompleteReport(transRequest, CMDCancelType.CmdCancelIdReadFailed);
            }
        }

        private void Abnormal_BcrMismatch(ID_31_TRANS_REQUEST transRequest)
        {
            LoadProcess(transRequest);
            Send_Cmd136_TransferEventReport(EventType.Bcrread, true, transRequest, BCRReadResult.BcrMisMatch);
            ID_36_TRANS_EVENT_RESPONSE the36Response = Deep_Clone<ID_36_TRANS_EVENT_RESPONSE>(recent36Response);
            if (the36Response.ReplyActiveType == CMDCancelType.CmdNone)
            {
                UnloadProcess(transRequest);
                Send_Cmd132_TransferCompleteReport(transRequest);
            }
            else if (the36Response.ReplyActiveType == CMDCancelType.CmdCancelIdMismatch)
            {
                Send_Cmd132_TransferCompleteReport(transRequest, CMDCancelType.CmdCancelIdMismatch);
            }
        }

        private void Abnormal_BcrDuplicate(ID_31_TRANS_REQUEST transRequest)
        {
            // Here has nothing to do.
        }





        private void LoadProcess(ID_31_TRANS_REQUEST transRequest)
        {
            ChangeTheAddressSectionToLoadAddress(transRequest);
            Send_Cmd136_TransferEventReport(EventType.LoadArrivals, true, transRequest);
            SpinWait.SpinUntil(() => false, 300);
            Send_Cmd136_TransferEventReport(EventType.Vhloading, true, transRequest);
            SpinWait.SpinUntil(() => false, 300);
            Send_Cmd136_TransferEventReport(EventType.LoadComplete, true, transRequest);
            SpinWait.SpinUntil(() => false, 300);
            Send_Cmd144_StatusChangeReport(true);
        }

        private void UnloadProcess(ID_31_TRANS_REQUEST transRequest)
        {
            ChangeTheAddressSectionToUnloadAddress(transRequest);
            Send_Cmd136_TransferEventReport(EventType.UnloadArrivals, true, transRequest);
            SpinWait.SpinUntil(() => false, 300);
            Send_Cmd136_TransferEventReport(EventType.Vhunloading, true, transRequest);
            SpinWait.SpinUntil(() => false, 300);
            Send_Cmd136_TransferEventReport(EventType.UnloadComplete, true, transRequest);
            SpinWait.SpinUntil(() => false, 300);
            Send_Cmd144_StatusChangeReport(false);
        }

        private void ChangeTheAddressSectionToUnloadAddress(ID_31_TRANS_REQUEST transRequest)
        {
            if (transRequest.GuideSections.Count > 0)
            {
                for (int sectionAddressCount = 0; sectionAddressCount < transRequest.GuideSections.Count; sectionAddressCount++)
                {
                    theVehicleInfo.CurrentSecID = transRequest.GuideSections[sectionAddressCount];
                    Send_Cmd134_TransferEventReport();
                    SpinWait.SpinUntil(() => false, 500);
                }
                theVehicleInfo.CurrentAdrID = transRequest.ToAdr;
                Send_Cmd134_TransferEventReport();
                SpinWait.SpinUntil(() => false, 500);
            }
        }

        private void ChangeTheAddressSectionToLoadAddress(ID_31_TRANS_REQUEST transRequest)
        {
            //if (transRequest.GuideSectionsStartToLoad.Count > 0)
            //{
            //    for (int sectionAddressCount = 0; sectionAddressCount < transRequest.GuideSectionsStartToLoad.Count; sectionAddressCount++)
            //    {
            //        theVehicleInfo.CurrentSecID = transRequest.GuideSectionsStartToLoad[sectionAddressCount];
            //        Send_Cmd134_TransferEventReport();
            //        SpinWait.SpinUntil(() => false, 250);
            //    }
            //    theVehicleInfo.CurrentAdrID = transRequest.LoadAdr;
            //    Send_Cmd134_TransferEventReport();
            //    SpinWait.SpinUntil(() => false, 250);
            //}
        }
        public void Send_Cmd144_StatusChangeReport(bool hasCstBoxorNot)
        {
            try
            {
                ID_144_STATUS_CHANGE_REP iD_144_SendToOHTC = new ID_144_STATUS_CHANGE_REP();
                iD_144_SendToOHTC = Deep_Clone<ID_144_STATUS_CHANGE_REP>(theVehicleInfo);
                if (hasCstBoxorNot == true)
                {
                    setHaveCstBox144(iD_144_SendToOHTC);
                }
                else if (hasCstBoxorNot == false)
                {
                    setEmptyCstBox144(iD_144_SendToOHTC);
                }
                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.StatueChangeRepFieldNumber;
                wrappers.StatueChangeRep = iD_144_SendToOHTC;

                ServerClientAgent.TrxTcpIp.SendGoogleMsg(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }

        }
        public void Send_Cmd134_TransferEventReport()
        {
            try
            {
                ID_134_TRANS_EVENT_REP iD_134_TRANS_EVENT_REP = new ID_134_TRANS_EVENT_REP();
                iD_134_TRANS_EVENT_REP.EventType = EventType.AdrPass;
                iD_134_TRANS_EVENT_REP.CurrentAdrID = theVehicleInfo.CurrentAdrID;
                iD_134_TRANS_EVENT_REP.CurrentSecID = theVehicleInfo.CurrentSecID;
                iD_134_TRANS_EVENT_REP.SecDistance = 0;

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.TransEventRepFieldNumber;
                wrappers.TransEventRep = iD_134_TRANS_EVENT_REP;

                ServerClientAgent.TrxTcpIp.SendGoogleMsg(wrappers);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }
        private void Send_Cmd136_TransferEventReport(EventType eventType, bool isCmd = false,
                                ID_31_TRANS_REQUEST now31Cmd = null, BCRReadResult bCRReadResult = BCRReadResult.BcrNormal)
        {
            try
            {

                ID_136_TRANS_EVENT_REP iD_136_TRANS_EVENT_REP = new ID_136_TRANS_EVENT_REP();
                ID_36_TRANS_EVENT_RESPONSE iD_36_TRANS_EVENT_RESPONSE = new ID_36_TRANS_EVENT_RESPONSE();
                Set136Inform(eventType, bCRReadResult, iD_136_TRANS_EVENT_REP);

                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.ImpTransEventRepFieldNumber;
                wrappers.ImpTransEventRep = iD_136_TRANS_EVENT_REP;

                SendCommandWrapper(wrappers, out iD_36_TRANS_EVENT_RESPONSE, false);
                recent36Response = iD_36_TRANS_EVENT_RESPONSE;
                //if (iD_36_TRANS_EVENT_RESPONSE.ReplyActiveType != CMDCancelType.CmdNone)
                //{
                //    switch (iD_36_TRANS_EVENT_RESPONSE.ReplyActiveType)
                //    {
                //        case CMDCancelType.CmdCancelIdReadFailed:
                //            Send_Cmd132_TransferCompleteReport(recent31Cmd);
                //            break;
                //        case CMDCancelType.CmdCancelIdMismatch:
                //            Send_Cmd132_TransferCompleteReport(recent31Cmd);
                //            break;
                //        case CMDCancelType.CmdCancelIdReadDuplicate:

                //            break;
                //        case CMDCancelType.CmdCancelIdReadForceFinish:

                //            break;
                //        case CMDCancelType.CmdAbort:

                //            break;
                //        case CMDCancelType.CmdCancel:

                //            break;
                //        case CMDCancelType.CmdRetry:
                //            //recent36Response = Deep_Clone<ID_36_TRANS_EVENT_RESPONSE>(iD_36_TRANS_EVENT_RESPONSE);
                //            //recent36Response = iD_36_TRANS_EVENT_RESPONSE;
                //            break;
                //        case CMDCancelType.CmdNone:
                //            //recent36Response = Deep_Clone<ID_36_TRANS_EVENT_RESPONSE>(iD_36_TRANS_EVENT_RESPONSE);
                //            //recent36Response = iD_36_TRANS_EVENT_RESPONSE;
                //            break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        private void Set136Inform(EventType eventType, BCRReadResult bCRReadResult, ID_136_TRANS_EVENT_REP iD_136_TRANS_EVENT_REP)
        {
            string Mismatch_ID = Global_Param.MismatchID;
            string CSTID = "";
            switch (bCRReadResult)
            {
                case BCRReadResult.BcrMisMatch:
                    CSTID = Mismatch_ID;
                    break;
                case BCRReadResult.BcrReadFail:
                    CSTID = "";
                    break;
                case BCRReadResult.BcrNormal:
                    CSTID = theVehicleInfo.CSTID;
                    break;
            }
            iD_136_TRANS_EVENT_REP.EventType = eventType;
            iD_136_TRANS_EVENT_REP.CurrentAdrID = theVehicleInfo.CurrentAdrID;
            iD_136_TRANS_EVENT_REP.CurrentSecID = theVehicleInfo.CurrentSecID;
            iD_136_TRANS_EVENT_REP.CSTID = theVehicleInfo.CSTID;

            iD_136_TRANS_EVENT_REP.SecDistance = 0;
            iD_136_TRANS_EVENT_REP.BCRReadResult = bCRReadResult;
        }

        private void Send_Cmd132_TransferCompleteReport(ID_31_TRANS_REQUEST transRequest, CMDCancelType cMDCancelType = CMDCancelType.CmdNone)
        {
            try
            {
                ID_132_TRANS_COMPLETE_REPORT iD_132_TRANS_COMPLETE_REPORT = new ID_132_TRANS_COMPLETE_REPORT();
                ID_32_TRANS_COMPLETE_RESPONSE iD_32_TRANS_COMPLETE_RESPONSE = new ID_32_TRANS_COMPLETE_RESPONSE();
                if (operaType == EnumOperaType.InterlockError)
                {
                    iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusInterlockError;
                }
                else if (cmdCanceltype == CMDCancelType.CmdCancel || cmdCanceltype == CMDCancelType.CmdAbout)
                {
                    if (cmdCanceltype == CMDCancelType.CmdCancel)
                    {
                        iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusCancel;
                    }
                    if (cmdCanceltype == CMDCancelType.CmdAbout)
                    {
                        iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusAbort;
                    }
                }
                else
                {
                    switch (transRequest.ActType)
                    {
                        case ActiveType.Move:
                            if (cMDCancelType == CMDCancelType.CmdNone)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusMove;
                                setEmptyCarrier132(iD_132_TRANS_COMPLETE_REPORT);
                            }
                            break;
                        case ActiveType.Load:
                            if (cMDCancelType == CMDCancelType.CmdNone)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusLoad;
                                setHaveCstBox132(iD_132_TRANS_COMPLETE_REPORT, cMDCancelType);
                            }
                            else if (cMDCancelType == CMDCancelType.CmdCancelIdReadFailed)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusIdreadFailed;
                                setHaveCstBox132(iD_132_TRANS_COMPLETE_REPORT);
                                iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;
                            }
                            else if (cMDCancelType == CMDCancelType.CmdCancelIdMismatch)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusIdmisMatch;
                                setHaveCstBox132(iD_132_TRANS_COMPLETE_REPORT);
                                iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;
                            }
                            break;
                        case ActiveType.Unload:
                            if (cMDCancelType == CMDCancelType.CmdNone)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusUnload;
                                setEmptyCarrier132(iD_132_TRANS_COMPLETE_REPORT);
                            }
                            break;
                        case ActiveType.Loadunload:
                            if (cMDCancelType == CMDCancelType.CmdNone)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusLoadunload;
                                setEmptyCarrier132(iD_132_TRANS_COMPLETE_REPORT);
                            }
                            else if (cMDCancelType == CMDCancelType.CmdCancelIdReadFailed)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusIdreadFailed;
                                setHaveCstBox132(iD_132_TRANS_COMPLETE_REPORT);
                                iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;
                            }
                            else if (cMDCancelType == CMDCancelType.CmdCancelIdMismatch)
                            {
                                iD_132_TRANS_COMPLETE_REPORT.CmpStatus = CompleteStatus.CmpStatusIdmisMatch;
                                setHaveCstBox132(iD_132_TRANS_COMPLETE_REPORT);
                                iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;
                            }
                            break;
                    }
                }
                iD_132_TRANS_COMPLETE_REPORT.CurrentSecID = theVehicleInfo.CurrentSecID;
                iD_132_TRANS_COMPLETE_REPORT.CurrentAdrID = theVehicleInfo.CurrentAdrID;
                iD_132_TRANS_COMPLETE_REPORT.CmdDistance = 0;
                iD_132_TRANS_COMPLETE_REPORT.CmdID = theVehicleInfo.CmdID;
                iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;


                WrapperMessage wrappers = new WrapperMessage();
                wrappers.ID = WrapperMessage.TranCmpRepFieldNumber;
                wrappers.TranCmpRep = iD_132_TRANS_COMPLETE_REPORT;

                SendCommandWrapper(wrappers, out iD_32_TRANS_COMPLETE_RESPONSE);
            }
            catch (Exception ex)
            {
                var msg = ex.StackTrace;
            }
        }

        private void setHaveCstBox132(ID_132_TRANS_COMPLETE_REPORT iD_132_TRANS_COMPLETE_REPORT, CMDCancelType cMDCancelType = CMDCancelType.CmdNone)
        {
            iD_132_TRANS_COMPLETE_REPORT.HasCST = VhLoadCSTStatus.Exist;
            iD_132_TRANS_COMPLETE_REPORT.CSTID = theVehicleInfo.CSTID;
        }

        private static void setEmptyCarrier132(ID_132_TRANS_COMPLETE_REPORT iD_132_TRANS_COMPLETE_REPORT)
        {
            iD_132_TRANS_COMPLETE_REPORT.HasCST = VhLoadCSTStatus.NotExist;
            iD_132_TRANS_COMPLETE_REPORT.CSTID = "";
        }

        private void setHaveCstBox144(ID_144_STATUS_CHANGE_REP iD_144_STATUS_CHANGE_REP)
        {
            iD_144_STATUS_CHANGE_REP.HasCST = VhLoadCSTStatus.Exist;
            iD_144_STATUS_CHANGE_REP.CSTID = theVehicleInfo.CSTID;
        }
        private static void setEmptyCstBox144(ID_144_STATUS_CHANGE_REP iD_144_STATUS_CHANGE_REP)
        {
            iD_144_STATUS_CHANGE_REP.HasCST = VhLoadCSTStatus.NotExist;
            iD_144_STATUS_CHANGE_REP.CSTID = "";
        }

        private void SendCommandWrapper<TSource>(WrapperMessage wrapper, out TSource stRecv, bool isReply = false)
        {
            string rtnMsg = string.Empty;
            string msg = $"[SEND] [ID = {wrapper.ID}][SeqNum = {wrapper.SeqNum}] " + wrapper.ToString();
            OnCmdSend?.Invoke(this, msg);
            theLoggerAgent.LogMsg("Comm", new LogFormat("Comm", "1", GetType().Name + ":" + MethodBase.GetCurrentMethod().Name, "Device", "CarrierID"
                 , msg));
            com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
        }
        /// <summary>
        /// Deep_Clone : Using for clone all the object by [Serializable]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public T Deep_Clone<T>(T RealObject)
        {
            try
            {
                using (Stream objectStream = new MemoryStream())
                {
                    //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制     
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(objectStream, RealObject);
                    objectStream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(objectStream);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                return RealObject;
            }
        }
    }
}
