﻿//*********************************************************************************
//      MESDefaultMapAction.cs
//*********************************************************************************
// File Name: MESDefaultMapAction.cs
// Description: 與EAP通訊的劇本
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
// 2019/10/08    Kevin Wei      N/A            A0.01   Bug fix:修正在手動點擊Force finish時，
//                                                     CST ID填成CMD ID的問題。
// 2019/10/09    Kevin Wei      N/A            A0.02   修改Confirm route的return code填入不正確的問題。
// 2019/10/11    Kevin Wei      N/A            A0.03   增加搬送命令狀態是Pre initial 的時候，無法接受Cancel命令。
//**********************************************************************************
using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Controller;
using com.mirle.ibg3k0.bcf.Data.ValueDefMapAction;
using com.mirle.ibg3k0.bcf.Data.VO;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.BLL;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.SECS.CSOT;
using com.mirle.ibg3k0.sc.Data.SECSDriver;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.stc.Common;
using com.mirle.ibg3k0.stc.Data.SecsData;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace com.mirle.ibg3k0.sc.Data.ValueDefMapAction
{
    public class COSTMCSDefaultMapAction : IBSEMDriver, IValueDefMapAction
    {
        const string DEVICE_NAME_MCS = "MCS";
        const string CALL_CONTEXT_KEY_WORD_SERVICE_ID_MCS = "MCS Service";

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger GlassTrnLogger = LogManager.GetLogger("GlassTransferRpt_EAP");
        protected static Logger logger_MapActionLog = LogManager.GetLogger("MapActioLog");
        private ReportBLL reportBLL = null;

        /// <summary>
        /// 僅在測試階段使用
        /// </summary>
        protected bool isOnlineWithMcs = false;


        public virtual string getIdentityKey()
        {
            return this.GetType().Name;
        }

        public virtual void setContext(BaseEQObject baseEQ)
        {
            this.line = baseEQ as ALINE;
        }
        public virtual void unRegisterEvent()
        {

        }
        public virtual void doShareMemoryInit(BCFAppConstants.RUN_LEVEL runLevel)
        {
            try
            {
                switch (runLevel)
                {
                    case BCFAppConstants.RUN_LEVEL.ZERO:
                        SECSConst.setDicCEIDAndRPTID(scApp.CEIDBLL.loadDicCEIDAndRPTID());
                        SECSConst.setDicRPTIDAndVID(scApp.CEIDBLL.loadDicRPTIDAndVID());
                        break;
                    case BCFAppConstants.RUN_LEVEL.ONE:
                        break;
                    case BCFAppConstants.RUN_LEVEL.TWO:
                        break;
                    case BCFAppConstants.RUN_LEVEL.NINE:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
            }
        }
        #region Receive 

        protected override void S2F17ReceiveDateAndTimeRequest(object sender, SECSEventArgs e)
        {
            try
            {
                S2F17 s2f17 = ((S2F17)e.secsHandler.Parse<S2F17>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s2f17);
                if (!isProcess(s2f17)) { return; }

                S2F18 s2f18 = null;
                s2f18 = new S2F18();
                s2f18.SystemByte = s2f17.SystemByte;
                s2f18.SECSAgentName = scApp.EAPSecsAgentName;
                s2f18.TIME = DateTime.Now.ToString(SCAppConstants.TimestampFormat_16);

                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s2f18);
                SCUtility.secsActionRecordMsg(scApp, false, s2f18);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S2F18 Error:{0}", rtnCode);
                }


                ////當收到S2F17如果TSC_State是在NONE, 之後再接續進行Auto Initial
                //if (line.TSC_state_machine.State == ALINE.TSCState.NONE)
                //    scApp.LineService.TSCStateToPause();


            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S2F17_Receive_Date_Time_Req", ex.ToString());
            }
        }

        protected override void S2F33ReceiveDefineReport(object sender, SECSEventArgs e)
        {
            try
            {
                S2F33 s2f33 = ((S2F33)e.secsHandler.Parse<S2F33>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s2f33);
                if (!isProcess(s2f33)) { return; }

                S2F34 s2f34 = null;
                s2f34 = new S2F34();
                s2f34.SystemByte = s2f33.SystemByte;
                s2f34.SECSAgentName = scApp.EAPSecsAgentName;
                s2f34.DRACK = "0";


                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s2f34);
                SCUtility.secsActionRecordMsg(scApp, false, s2f34);


                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S2F18 Error:{0}", rtnCode);
                }

                scApp.CEIDBLL.DeleteRptInfoByBatch();

                if (s2f33.RPTITEMS != null && s2f33.RPTITEMS.Length > 0)
                    scApp.CEIDBLL.buildReportIDAndVid(s2f33.ToDictionary());



                SECSConst.setDicRPTIDAndVID(scApp.CEIDBLL.loadDicRPTIDAndVID());

            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S2F17_Receive_Date_Time_Req", ex.ToString());
            }
        }
        protected override void S2F35ReceiveLinkEventReport(object sender, SECSEventArgs e)
        {
            try
            {
                S2F35 s2f35 = ((S2F35)e.secsHandler.Parse<S2F35>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s2f35);
                if (!isProcess(s2f35)) { return; }


                S2F36 s2f36 = null;
                s2f36 = new S2F36();
                s2f36.SystemByte = s2f35.SystemByte;
                s2f36.SECSAgentName = scApp.EAPSecsAgentName;
                s2f36.LRACK = "0";

                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s2f36);
                SCUtility.secsActionRecordMsg(scApp, false, s2f36);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S2F18 Error:{0}", rtnCode);
                }

                scApp.CEIDBLL.DeleteCEIDInfoByBatch();

                if (s2f35.RPTITEMS != null && s2f35.RPTITEMS.Length > 0)
                    scApp.CEIDBLL.buildCEIDAndReportID(s2f35.ToDictionary());

                SECSConst.setDicCEIDAndRPTID(scApp.CEIDBLL.loadDicCEIDAndRPTID());

            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S2F17_Receive_Date_Time_Req", ex.ToString());
            }
        }
        protected override void S2F49ReceiveEnhancedRemoteCommandExtension(object sender, SECSEventArgs e)
        {
            try
            {
                if (scApp.getEQObjCacheManager().getLine().ServerPreStop)
                    return;
                string errorMsg = string.Empty;
                S2F49 s2f49 = ((S2F49)e.secsHandler.Parse<S2F49>(e));

                switch (s2f49.RCMD)
                {
                    case "TRANSFER":
                        S2F49_TRANSFER s2f49_transfer = ((S2F49_TRANSFER)e.secsHandler.Parse<S2F49_TRANSFER>(e));
                        SCUtility.secsActionRecordMsg(scApp, true, s2f49_transfer);
                        //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                        //   Data: s2f49_transfer);

                        //if (!isProcessEAP(s2f49)) { return; }

                        S2F50 s2f50 = new S2F50();
                        s2f50.SystemByte = s2f49_transfer.SystemByte;
                        s2f50.SECSAgentName = scApp.EAPSecsAgentName;
                        s2f50.HCACK = SECSConst.HCACK_Confirm;

                        string cmdID = s2f49_transfer.REPITEMS.COMMINFO.COMMAINFO.COMMANDIDINFO.CommandID;
                        string priority = s2f49_transfer.REPITEMS.COMMINFO.COMMAINFO.PRIORITY.CPVAL;
                        string replace = s2f49_transfer.REPITEMS.COMMINFO.COMMAINFO.REPLACE.CPVAL;
                        string rtnStr = "";

                        SCUtility.RecodeReportInfo(s2f49, cmdID);
                        //檢查CST Size及Glass Data


                        //string cmdID = s2f49.REPITEMS.COMMINFO.COMMAINFO.COMMANDIDINFO.CommandID;
                        string cstID = s2f49_transfer.REPITEMS.TRANINFO.CARRINFO.CARRIERIDINFO.CarrierID;
                        string source = s2f49_transfer.REPITEMS.TRANINFO.CARRINFO.SOUINFO.Source;
                        //bool sourceNotPort = scApp.MapBLL.getPortByPortID(source) == null;
                        //if (sourceNotPort)
                        //{
                        //    source = string.Empty;
                        //}
                        string dest = s2f49_transfer.REPITEMS.TRANINFO.CARRINFO.DESTINFO.Dest;

                        //檢查搬送命令

                        s2f50.HCACK = scApp.CMDBLL.doCheckMCSCommand(cmdID, priority, cstID, source, dest, out rtnStr);
                        //if (s2f50.HCACK == SECSConst.HCACK_Confirm)
                        //{
                        using (TransactionScope tx = SCUtility.getTransactionScope())
                        {
                            using (DBConnection_EF con = DBConnection_EF.GetUContext())
                            {
                                bool isCreatScuess = true;
                                if (!SCUtility.isMatche(SECSConst.HCACK_Rejected_Already_Requested, s2f50.HCACK))
                                    isCreatScuess &= scApp.CMDBLL.doCreatMCSCommand(cmdID, priority, replace, cstID, source, dest, s2f50.HCACK);
                                if (s2f50.HCACK == SECSConst.HCACK_Confirm)
                                    isCreatScuess &= scApp.SysExcuteQualityBLL.creatSysExcuteQuality(cmdID, cstID, source, dest);
                                if (isCreatScuess)
                                {
                                    TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s2f50);
                                    SCUtility.secsActionRecordMsg(scApp, false, s2f50);
                                    //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                                    //   Data: s2f50);
                                    SCUtility.RecodeReportInfo(s2f50, cmdID);
                                    if (rtnCode != TrxSECS.ReturnCode.Normal)
                                    {
                                        logger_MapActionLog.Warn("Reply EQPT S2F50) Error:{0}", rtnCode);
                                        isCreatScuess = false;
                                    }
                                }
                                if (isCreatScuess)
                                {
                                    tx.Complete();
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        //}
                        if (s2f50.HCACK == SECSConst.HCACK_Confirm)
                            scApp.CMDBLL.checkMCS_TransferCommand();
                        else
                        {
                            BCFApplication.onWarningMsg(rtnStr);
                        }
                        break;
                    case "STAGE":
                        S2F49_STAGE s2f49_stage = ((S2F49_STAGE)e.secsHandler.Parse<S2F49_STAGE>(e));

                        S2F50 s2f50_stage = new S2F50();
                        s2f50_stage.SystemByte = s2f49_stage.SystemByte;
                        s2f50_stage.SECSAgentName = scApp.EAPSecsAgentName;
                        if (scApp.getEQObjCacheManager().getLine().SCStats == ALINE.TSCState.PAUSING
                            || scApp.getEQObjCacheManager().getLine().SCStats == ALINE.TSCState.PAUSED)
                        {
                            //s2f50_stage.HCACK = SECSConst.HCACK_Rejected_Already_Requested;
                            s2f50_stage.HCACK = SECSConst.HCACK_Current_Not_Able_Execute;
                        }
                        else
                        {
                            s2f50_stage.HCACK = SECSConst.HCACK_Confirm;
                        }

                        string source_port_id = s2f49_stage.REPITEMS.TRANSFERINFO.CPVALUE.SOURCEPORT_CP.CPVAL_ASCII;
                        TrxSECS.ReturnCode rtnCode_stage = ISECSControl.replySECS(bcfApp, s2f50_stage);
                        SCUtility.secsActionRecordMsg(scApp, false, s2f50_stage);
                        SCUtility.RecodeReportInfo(s2f50_stage);

                        //TODO Stage
                        //將收下來的Stage命令先放到Redis上
                        //等待Timer發現後會將此命令取下來並下命令給車子去執行
                        //(此處將再考慮是要透過Timer或是開Thread來監控這件事)
                        if (s2f50_stage.HCACK == SECSConst.HCACK_Confirm)
                        {
                            //暫時取消Stage的流程。
                            //var port = scApp.MapBLL.getPortByPortID(source_port_id);
                            //AVEHICLE vh_test = scApp.VehicleBLL.findBestSuitableVhStepByStepFromAdr(port.ADR_ID, port.LD_VH_TYPE);
                            //scApp.VehicleBLL.callVehicleToMove(vh_test, port.ADR_ID);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S2F49_Receive_Remote_Command", ex);
            }
        }


        protected override void S2F41ReceiveHostCommand(object sender, SECSEventArgs e)
        {
            try
            {
                S2F41 s2f41 = ((S2F41)e.secsHandler.Parse<S2F41>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s2f41);
                //if (!isProcessEAP(s2f37)) { return; }

                S2F42 s2f42 = null;
                s2f42 = new S2F42();
                s2f42.SystemByte = s2f41.SystemByte;
                s2f42.SECSAgentName = scApp.EAPSecsAgentName;
                string mcs_cmd_id = string.Empty;
                bool needToResume = false;
                bool needToPause = false;
                bool canCancelCmd = false;
                bool canAbortCmd = false;
                string cancel_abort_cmd_id = string.Empty;
                switch (s2f41.RCMD)
                {
                    case SECSConst.RCMD_Resume:
                        if (line.TSC_state_machine.State == ALINE.TSCState.PAUSED || line.TSC_state_machine.State == ALINE.TSCState.PAUSING)
                        {
                            s2f42.HCACK = SECSConst.HCACK_Confirm_Executed;
                            needToResume = true;
                        }
                        else
                        {
                            s2f42.HCACK = SECSConst.HCACK_Current_Not_Able_Execute;
                            needToResume = false;
                        }
                        break;
                    case SECSConst.RCMD_Pause:
                        if (line.TSC_state_machine.State == ALINE.TSCState.AUTO)
                        {
                            s2f42.HCACK = SECSConst.HCACK_Confirm_Executed;
                            needToPause = true;
                        }
                        else
                        {
                            s2f42.HCACK = SECSConst.HCACK_Current_Not_Able_Execute;
                            needToResume = false;
                        }
                        break;
                    case SECSConst.RCMD_Abort:
                        var abort_check_result = checkHostCommandAbort(s2f41);
                        canAbortCmd = abort_check_result.isOK;
                        s2f42.HCACK = abort_check_result.checkResult;
                        cancel_abort_cmd_id = abort_check_result.cmdID;
                        break;
                    case SECSConst.RCMD_Cancel:
                        var cancel_check_result = checkHostCommandCancel(s2f41);
                        canCancelCmd = cancel_check_result.isOK;
                        s2f42.HCACK = cancel_check_result.checkResult;
                        cancel_abort_cmd_id = cancel_check_result.cmdID;
                        break;
                    case SECSConst.RCMD_ConfirmRoute:
                        var confirm_route_check_result = checkHostCommandConfirmRoute(s2f41);
                        s2f42.HCACK = confirm_route_check_result.checkResult;
                        break;
                }
                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s2f42);
                SCUtility.secsActionRecordMsg(scApp, false, s2f42);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S2F18 Error:{0}", rtnCode);
                }
                if (needToResume)
                {
                    line.ResumeToAuto(reportBLL);
                }
                if (needToPause)
                {
                    //line.RequestToPause(reportBLL);
                    scApp.LineService.TSCStateToPause(SECSConst.PAUSE_REASON_MCSRequest);
                }
                if (canCancelCmd)
                {
                    scApp.VehicleService.doCancelOrAbortCommandByMCSCmdID(cancel_abort_cmd_id, ProtocolFormat.OHTMessage.CMDCancelType.CmdCancel);
                    //ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(cancel_abort_cmd_id);
                    //if (mcs_cmd.TRANSFERSTATE == E_TRAN_STATUS.Queue)
                    //{
                    //    scApp.CMDBLL.updateCMD_MCS_TranStatus2Canceling(cancel_abort_cmd_id);
                    //    S6F11SendTransferCancelInitial(mcs_cmd, null);
                    //    scApp.CMDBLL.updateCMD_MCS_TranStatus2Canceled(cancel_abort_cmd_id);
                    //    S6F11SendTransferCancelCompleted(mcs_cmd, null);
                    //}
                    //else
                    //{
                    //    S6F11SendTransferCancelInitial(cancel_abort_cmd_id, null);
                    //    bool is_success = scApp.VehicleService.cancleOrAbortCommandByMCSCmdID(cancel_abort_cmd_id, ProtocolFormat.OHTMessage.CMDCancelType.CmdCancel);
                    //    if (!is_success)
                    //    {
                    //        S6F11SendTransferCancelFailed(cancel_abort_cmd_id, null);
                    //    }
                    //}
                }
                if (canAbortCmd)
                {
                    scApp.VehicleService.doCancelOrAbortCommandByMCSCmdID(cancel_abort_cmd_id, ProtocolFormat.OHTMessage.CMDCancelType.CmdAbort);

                    //S6F11SendTransferAbortInitial(cancel_abort_cmd_id, null);
                    //bool is_success = scApp.VehicleService.cancleOrAbortCommandByMCSCmdID(cancel_abort_cmd_id, ProtocolFormat.OHTMessage.CMDCancelType.CmdAbout);
                    //if (!is_success)
                    //{
                    //    S6F11SendTransferAbortFailed(cancel_abort_cmd_id, null);
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S2F17_Receive_Date_Time_Req", ex.ToString());
            }
        }



        private (bool isOK, string checkResult, string cmdID) checkHostCommandAbort(S2F41 s2F41)
        {
            bool is_ok = true;
            string check_result = SECSConst.HCACK_Confirm;
            string command_id = string.Empty;
            var command_id_item = s2F41.REPITEMS.Where(item => SCUtility.isMatche(item.CPNAME, SECSConst.CPNAME_CommandID)).FirstOrDefault();
            if (command_id_item != null)
            {
                command_id = command_id_item.CPVAL;
                ACMD_MCS cmd_mcs = scApp.CMDBLL.getCMD_MCSByID(command_id);
                if (cmd_mcs != null)
                {
                    //if (cmd_mcs.TRANSFERSTATE < E_TRAN_STATUS.Transferring)
                    //如果命令在
                    if (cmd_mcs.COMMANDSTATE < ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE ||
                        cmd_mcs.COMMANDSTATE >= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_UNLOAD_ARRIVE)
                    {
                        check_result = SECSConst.HCACK_Current_Not_Able_Execute;
                        is_ok = false;
                        //string current_status = ACMD_MCS.COMMAND_STATUS_BIT_To_String(cmd_mcs.COMMANDSTATE);
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                           Data: $"Process mcs command [Abort] can't excute, because mcs command id:{command_id} current command status:{cmd_mcs.COMMANDSTATE}",
                           XID: command_id);
                    }
                }
                else
                {
                    check_result = SECSConst.HCACK_Obj_Not_Exist;
                    is_ok = false;
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                       Data: $"Process mcs command [Abort] can't excute, because mcs command id:{command_id} not exist",
                       XID: command_id);
                }
            }
            else
            {
                check_result = SECSConst.HCACK_Param_Invalid;
                is_ok = false;
            }
            return (is_ok, check_result, command_id);
        }
        private (bool isOK, string checkResult, string cmdID) checkHostCommandCancel(S2F41 s2F41)
        {
            bool is_ok = true;
            string check_result = SECSConst.HCACK_Confirm;
            string command_id = string.Empty;
            var command_id_item = s2F41.REPITEMS.Where(item => SCUtility.isMatche(item.CPNAME, SECSConst.CPNAME_CommandID)).FirstOrDefault();
            if (command_id_item != null)
            {
                command_id = command_id_item.CPVAL;
                ACMD_MCS cmd_mcs = scApp.CMDBLL.getCMD_MCSByID(command_id);
                if (cmd_mcs != null)
                {
                    if (cmd_mcs.TRANSFERSTATE == E_TRAN_STATUS.PreInitial)  //A0.03
                    {                                                       //A0.03
                        check_result = SECSConst.HCACK_Current_Not_Able_Execute;    //A0.03
                        is_ok = false;                                      //A0.03
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                           Data: $"Process mcs command [Cancle] can't excute, because mcs command id:{command_id} current transfer status:{cmd_mcs.TRANSFERSTATE},ohtc reply:{check_result}",
                           XID: command_id);
                    }                                                       //A0.03
                    else if (cmd_mcs.TRANSFERSTATE == E_TRAN_STATUS.Canceling)
                    {
                        check_result = SECSConst.HCACK_Rejected_Already_Requested;
                        is_ok = false;
                        string current_status = ACMD_MCS.COMMAND_STATUS_BIT_To_String(cmd_mcs.COMMANDSTATE);
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                           Data: $"Process mcs command [Cancel] can't excute, because mcs command id:{command_id} current transfer status:{cmd_mcs.TRANSFERSTATE},ohtc reply:{check_result}",
                           XID: command_id);
                    }
                    //else if (cmd_mcs.TRANSFERSTATE >= E_TRAN_STATUS.Transferring)
                    else if (cmd_mcs.COMMANDSTATE >= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_ARRIVE)
                    {
                        check_result = SECSConst.HCACK_Current_Not_Able_Execute;
                        is_ok = false;
                        string current_status = ACMD_MCS.COMMAND_STATUS_BIT_To_String(cmd_mcs.COMMANDSTATE);
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                           Data: $"Process mcs command [Cancel] can't excute, because mcs command id:{command_id} current command status:{current_status},ohtc reply:{check_result}",
                           XID: command_id);
                    }
                }
                else
                {
                    check_result = SECSConst.HCACK_Obj_Not_Exist;
                    is_ok = false;
                }
            }
            else
            {
                check_result = SECSConst.HCACK_Param_Invalid;
                is_ok = false;
            }
            return (is_ok, check_result, command_id);
        }
        private (bool isOK, string checkResult) checkHostCommandConfirmRoute(S2F41 s2F41)
        {
            bool is_ok = true;
            string check_result = SECSConst.HCACK_Confirm;
            var source_item = s2F41.REPITEMS.Where(item => SCUtility.isMatche(item.CPNAME, SECSConst.CPNAME_SourcePort)).FirstOrDefault();
            var dest_item = s2F41.REPITEMS.Where(item => SCUtility.isMatche(item.CPNAME, SECSConst.CPNAME_DestPort)).FirstOrDefault();
            string source_port_id = source_item?.CPVAL;
            string dest_port_id = dest_item?.CPVAL;

            string source_adr_id = "";
            string dest_adr_id = "";
            if (is_ok && !scApp.MapBLL.getAddressID(source_port_id, out source_adr_id))
            {
                check_result = SECSConst.HCACK_Obj_Not_Exist;
                is_ok = false;
            }
            if (!scApp.MapBLL.getAddressID(dest_port_id, out dest_adr_id))
            {
                check_result = SECSConst.HCACK_Obj_Not_Exist;
                is_ok = false;
            }
            if (is_ok)
            {
                var source_adr_sections = scApp.MapBLL.loadSectionByFromAdr(source_adr_id);
                var dest_adr_sections = scApp.MapBLL.loadSectionByFromAdr(dest_adr_id);
                ASEGMENT source_segment = scApp.MapBLL.getSegmentByID(source_adr_sections.First().SEG_NUM);
                ASEGMENT dest_segment = scApp.MapBLL.getSegmentByID(dest_adr_sections.First().SEG_NUM);

                if (source_segment.STATUS == E_SEG_STATUS.Closed || dest_segment.STATUS == E_SEG_STATUS.Closed)
                {
                    //A0.02 check_result = SECSConst.HCACK_Not_Able_Execute;
                    check_result = SECSConst.HCACK_Enabled_Route_Does_Not_Exist; //A0.02
                }
            }
            return (is_ok, check_result);
        }


        protected override void S1F3ReceiveSelectedEquipmentStatusRequest(object sender, SECSEventArgs e)
        {
            try
            {
                S1F3 s1f3 = ((S1F3)e.secsHandler.Parse<S1F3>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s1f3);

                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                //          Data: s1f3);
                bool is_monitored_vehicle = false;
                int count = s1f3.SVID.Count();
                S1F4 s1f4 = new S1F4();
                s1f4.SECSAgentName = scApp.EAPSecsAgentName;
                s1f4.SystemByte = s1f3.SystemByte;
                s1f4.SV = new SXFY[count];
                for (int i = 0; i < count; i++)
                {
                    if (s1f3.SVID[i] == SECSConst.VID_Current_Port_States)
                    {
                        line.CurrentPortStateChecked = true;
                        s1f4.SV[i] = buildCurrentPortStatesVIDItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_Control_State)
                    {
                        line.CurrentStateChecked = true;
                        s1f4.SV[i] = buildControlStateVIDItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_Enhanced_Vehicles)
                    {
                        line.EnhancedVehiclesChecked = true;
                        s1f4.SV[i] = buildEnhancedVehiclesVIDItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_TSC_State)
                    {
                        line.TSCStateChecked = true;
                        s1f4.SV[i] = buildTCSStateVIDItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_Unit_Alarm_Stat_List)
                    {
                        line.UnitAlarmStateListChecked = true;
                        s1f4.SV[i] = buildUnitAlarmStatListItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_Enhanced_Transfers)
                    {
                        line.EnhancedTransfersChecked = true;
                        s1f4.SV[i] = buildEnhancedTransfersVIDItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_Enhanced_Carriers)
                    {
                        line.EnhancedCarriersChecked = true;
                        s1f4.SV[i] = buildEnhancedCarriersVIDItem();
                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_Lane_CutInfo_List)
                    {
                        line.LaneCutListChecked = true;
                        s1f4.SV[i] = buildLaneCutInfoListVIDItem();

                    }
                    else if (s1f3.SVID[i] == SECSConst.VID_MonitoredVehicle)
                    {
                        is_monitored_vehicle = true;
                        s1f4.SV[i] = buildMonitoredVehilceInfo();

                    }
                    else
                    {
                        s1f4.SV[i] = new SXFY();
                    }
                }

                if (!is_monitored_vehicle)
                {
                    SCUtility.RecodeReportInfo(s1f3);
                }
                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                //          Data: s1f3);

                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s1f4);
                SCUtility.secsActionRecordMsg(scApp, false, s1f4);

                if (!is_monitored_vehicle)
                {
                    SCUtility.RecodeReportInfo(s1f4);
                }
                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                //          Data: s1f4);
            }
            catch (Exception ex)
            {
                logger.Error("AUOMCSDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}",
                    line.LINE_ID, "S1F3_Receive_Eqpt_Stat_Req", ex.ToString());
            }
        }
        #region Build VIDItem
        private S6F11.RPTINFO.RPTITEM.VIDITEM_06_SV buildControlStateVIDItem()
        {
            string control_state = SCAppConstants.LineHostControlState.convert2MES(line.Host_Control_State);
            S6F11.RPTINFO.RPTITEM.VIDITEM_06_SV viditem_06 = new S6F11.RPTINFO.RPTITEM.VIDITEM_06_SV()
            {
                CONTROLSTATE = control_state
            };
            return viditem_06;
        }

        private S6F11.RPTINFO.RPTITEM.VIDITEM_118_DVVAL buildCurrentPortStatesVIDItem()
        {
            List<APORTSTATION> port_station = scApp.getEQObjCacheManager().getALLPortStation();
            int port_count = port_station.Count;

            string control_state = SCAppConstants.LineHostControlState.convert2MES(line.Host_Control_State);
            S6F11.RPTINFO.RPTITEM.VIDITEM_118_DVVAL viditem_118 = new S6F11.RPTINFO.RPTITEM.VIDITEM_118_DVVAL();
            viditem_118.PORT_INFO = new S6F11.RPTINFO.RPTITEM.VIDITEM_PORT_INFO[port_count];
            for (int j = 0; j < port_count; j++)
            {
                viditem_118.PORT_INFO[j] = new S6F11.RPTINFO.RPTITEM.VIDITEM_PORT_INFO();
                viditem_118.PORT_INFO[j].PORT_ID = port_station[j].PORT_ID;
                viditem_118.PORT_INFO[j].PORT_TRANSFER_STATE = ((int)port_station[j].PORT_STATUS).ToString();
            }
            return viditem_118;
        }

        private S6F11.RPTINFO.RPTITEM.VIDITEM_119_SV buildEnhancedVehiclesVIDItem()
        {
            List<AVEHICLE> vhs = scApp.getEQObjCacheManager().getAllVehicle();
            int vhs_count = vhs.Count;
            S6F11.RPTINFO.RPTITEM.VIDITEM_119_SV viditem_119 = new S6F11.RPTINFO.RPTITEM.VIDITEM_119_SV();
            viditem_119.ENHANCED_VEHICLE_INFO_OBJ = new S6F11.RPTINFO.RPTITEM.VIDITEM_119_SV.ENHANCED_VEHICLE_INFO[vhs_count];
            for (int j = 0; j < vhs_count; j++)
            {
                string adr_or_port_id = "";
                scApp.MapBLL.getPortID(vhs[j].CUR_ADR_ID, out adr_or_port_id);
                viditem_119.ENHANCED_VEHICLE_INFO_OBJ[j] = new S6F11.RPTINFO.RPTITEM.VIDITEM_119_SV.ENHANCED_VEHICLE_INFO();
                viditem_119.ENHANCED_VEHICLE_INFO_OBJ[j].VehicleID = vhs[j].Real_ID;
                viditem_119.ENHANCED_VEHICLE_INFO_OBJ[j].VehicleState = ((int)vhs[j].State).ToString();
                viditem_119.ENHANCED_VEHICLE_INFO_OBJ[j].VehicleLocation = adr_or_port_id;
            }
            return viditem_119;
        }

        private S6F11.RPTINFO.RPTITEM.VIDITEM_73_SV buildTCSStateVIDItem()
        {

            string tsc_state = ((int)line.TSC_state_machine.State).ToString();
            if (line.TSC_state_machine.State == ALINE.TSCState.NONE)
            {
                tsc_state = "1";

            }
            else
            {
                tsc_state = ((int)line.TSC_state_machine.State).ToString();

            }
            S6F11.RPTINFO.RPTITEM.VIDITEM_73_SV viditem_73 = new S6F11.RPTINFO.RPTITEM.VIDITEM_73_SV()
            {
                TSCState = tsc_state
            };

            return viditem_73;
        }
        private S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV buildUnitAlarmStatListItem()
        {
            List<ALARM> occurred_alarms = scApp.AlarmBLL.getCurrentAlarmsFromRedis();
            List<ALARM> occurred_error_alarms = occurred_alarms.Where(alarm => alarm.ALAM_LVL == E_ALARM_LVL.Error&&alarm.EQPT_ID.StartsWith("OHx")).ToList();

            S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV viditem_254 = new S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV();
            viditem_254.UNIT_ALARMS_INFO_OBJ = new S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV.UNIT_ALARM_INFO[occurred_error_alarms.Count];
            for (int i = 0; i < occurred_error_alarms.Count; i++)
            {
                try
                {
                    AVEHICLE vh = scApp.getEQObjCacheManager().getVehicletByVHID(occurred_error_alarms[i].EQPT_ID);
                    int current_adr = 0;
                    int.TryParse(vh.CUR_ADR_ID, out current_adr);
                    string uint_id = vh.Real_ID;
                    string vh_current_position = current_adr.ToString();
                    string vh_next_position = "0";
                    string alarm_id = occurred_error_alarms[i].ALAM_CODE;
                    int ialarm_code = 0;
                    int.TryParse(alarm_id, out ialarm_code);
                    string alarm_code = (ialarm_code < 0 ? ialarm_code * -1 : ialarm_code).ToString();
                    string alarm_text = occurred_error_alarms[i].ALAM_DESC;
                    string vh_communication_state = SECSConst.VEHICLE_COMMUNICATION_STATE_Communicating;
                    string mainte_state = SECSConst.MAINTE_STATE_Undefined;
                    viditem_254.UNIT_ALARMS_INFO_OBJ[i] = new S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV.UNIT_ALARM_INFO()
                    {
                        UnitID = uint_id,
                        VehicleCurrentPosition = vh_current_position,
                        VehicleNextPosition = vh_next_position,
                        //AlarmID = alarm_id,
                        AlarmID = alarm_code,
                        AlarmText = alarm_text,
                        VehicleCommunicationState = vh_communication_state,
                        MainteState = mainte_state
                    };
                }
                catch (Exception ex)
                {
                    viditem_254 = new S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV();
                    viditem_254.UNIT_ALARMS_INFO_OBJ = new S6F11.RPTINFO.RPTITEM.VIDITEM_254_SV.UNIT_ALARM_INFO[0];
                    logger.Error("AUOMCSDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}",
                    line.LINE_ID, "buildUnitAlarmStatListItem", ex.ToString());
                    return viditem_254;
                }
            }

            return viditem_254;
        }

        private S6F11.RPTINFO.RPTITEM.VIDITEM_76_SV buildEnhancedTransfersVIDItem()
        {
            List<ACMD_MCS> mcs_cmds = scApp.CMDBLL.loadACMD_MCSIsUnfinished();
            int cmd_count = mcs_cmds.Count;
            S6F11.RPTINFO.RPTITEM.VIDITEM_76_SV viditem_63 = new S6F11.RPTINFO.RPTITEM.VIDITEM_76_SV();
            viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS = new S6F11.RPTINFO.RPTITEM.VIDITEM_205_SV[cmd_count];

            for (int k = 0; k < cmd_count; k++)
            {
                ACMD_MCS mcs_cmd = mcs_cmds[k];
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k] = new S6F11.RPTINFO.RPTITEM.VIDITEM_205_SV();
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].COMMAND_INFO_OBJ.COMMAND_ID_OBJ.COMMAND_ID = mcs_cmd.CMD_ID;
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].COMMAND_INFO_OBJ.PRIORITY_OBJ.PRIORITY = mcs_cmd.PRIORITY.ToString();
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].COMMAND_INFO_OBJ.REPLACE_OBJ.REPLACE = mcs_cmd.REPLACE.ToString();

                string transfer_state = SECSConst.convert2MES(mcs_cmd.TRANSFERSTATE);
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].TRANSFER_STATE_OBJ.TRANSFER_STATE = transfer_state;


                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].TRANSFER_INFO_OBJ = new S6F11.RPTINFO.RPTITEM.VIDITEM_67_SV[1];
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].TRANSFER_INFO_OBJ[0] = new S6F11.RPTINFO.RPTITEM.VIDITEM_67_SV();
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].TRANSFER_INFO_OBJ[0].CARRIER_ID = mcs_cmd.CARRIER_ID;
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].TRANSFER_INFO_OBJ[0].SOURCE_PORT = mcs_cmd.HOSTSOURCE;
                viditem_63.ENHANCED_TRANSFER_COMMAND_INFOS[k].TRANSFER_INFO_OBJ[0].DESTINATION_PORT = mcs_cmd.HOSTDESTINATION;

            }
            return viditem_63;
        }

        private S6F11.RPTINFO.RPTITEM.VIDITEM_91_SV buildEnhancedCarriersVIDItem()
        {
            List<AVEHICLE> has_carry_vhs = scApp.getEQObjCacheManager().getAllVehicle().Where(vh => vh.HAS_CST == 1).ToList();
            foreach (var v in has_carry_vhs.ToList())
            {
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(v.VEHICLE_ID);
                if (SCUtility.isEmpty(vid_info.CARRIER_ID) || SCUtility.isEmpty(vid_info.CARRIER_LOC))
                {
                    has_carry_vhs.Remove(v);
                }
            }
            int carry_vhs_count = has_carry_vhs.Count;
            S6F11.RPTINFO.RPTITEM.VIDITEM_91_SV viditem_91 = new S6F11.RPTINFO.RPTITEM.VIDITEM_91_SV();
            viditem_91.ENHANCED_CARRIER_INFOS = new S6F11.RPTINFO.RPTITEM.VIDITEM_75_DVVAL[carry_vhs_count];

            for (int k = 0; k < carry_vhs_count; k++)
            {
                AVEHICLE has_carray_vh = has_carry_vhs[k];
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(has_carray_vh.VEHICLE_ID);

                viditem_91.ENHANCED_CARRIER_INFOS[k] = new S6F11.RPTINFO.RPTITEM.VIDITEM_75_DVVAL();
                viditem_91.ENHANCED_CARRIER_INFOS[k].CARRIER_ID = vid_info.CARRIER_ID.Trim();
                viditem_91.ENHANCED_CARRIER_INFOS[k].VRHICLE_ID = has_carray_vh.Real_ID;
                viditem_91.ENHANCED_CARRIER_INFOS[k].CARRIER_LOC = vid_info.CARRIER_LOC;
                viditem_91.ENHANCED_CARRIER_INFOS[k].INSTALL_TIME = vid_info.CARRIER_INSTALLED_TIME?.ToString(SCAppConstants.TimestampFormat_16);
            }
            return viditem_91;
        }




        private S6F11.RPTINFO.RPTITEM.VIDITEM_360_SV buildLaneCutInfoListVIDItem()
        {
            List<string> nonActiveSeg = scApp.MapBLL.loadNonActiveSegmentNum();

            int non_active_seg = nonActiveSeg.Count;
            S6F11.RPTINFO.RPTITEM.VIDITEM_360_SV viditem_360 = new S6F11.RPTINFO.RPTITEM.VIDITEM_360_SV();
            viditem_360.LANE_CUT_INFO = new S6F11.RPTINFO.RPTITEM.VIDITEM_330_SV[non_active_seg];
            for (int k = 0; k < non_active_seg; k++)
            {
                string non_active_segment_id = nonActiveSeg[k];
                List<ASECTION> sections = scApp.MapBLL.loadSectionsBySegmentID(non_active_segment_id);
                string start_point = sections.First().FROM_ADR_ID;
                string end_point = sections.Last().TO_ADR_ID;
                string lane_cut_type = SECSConst.LANECUTTYPE_LaneCutOnHMI;
                viditem_360.LANE_CUT_INFO[k] = new S6F11.RPTINFO.RPTITEM.VIDITEM_330_SV();
                viditem_360.LANE_CUT_INFO[k].LANE_INFO_OBJ.StartPoint = start_point;
                viditem_360.LANE_CUT_INFO[k].LANE_INFO_OBJ.EndPoint = end_point;
                viditem_360.LANE_CUT_INFO[k].LANE_CUT_TYPE = lane_cut_type;
            }
            return viditem_360;
        }
        private S6F11.RPTINFO.RPTITEM.VIDITEM_252_SV buildMonitoredVehilceInfo()
        {
            List<string> nonActiveSeg = scApp.MapBLL.loadNonActiveSegmentNum();

            int non_active_seg = nonActiveSeg.Count;
            S6F11.RPTINFO.RPTITEM.VIDITEM_252_SV viditem_252 = new S6F11.RPTINFO.RPTITEM.VIDITEM_252_SV();
            viditem_252.MONITORED_VEHICLE_INFO_OBJ = new S6F11.RPTINFO.RPTITEM.VIDITEM_252_SV.MONITORED_VEHICLE_INFO[14];
            List<AVEHICLE> vhs = scApp.VehicleBLL.cache.loadVhs();
            for (int i = 0; i < vhs.Count; i++)
            {
                AVEHICLE vh = vhs[i];
                string vh_id = vh.Real_ID;
                string cur_postion = SCUtility.isEmpty(vh.CUR_ADR_ID) ? "0" : vh.CUR_ADR_ID;
                string distance = SCUtility.isEmpty(cur_postion) ? "0" : vh.VEHICLE_ACC_DIST.ToString();
                string next_position = "0";
                ASECTION cur_sec = scApp.SectionBLL.cache.GetSection(vh.CUR_SEC_ID);
                if (cur_sec != null)
                {
                    next_position = cur_sec.TO_ADR_ID;
                }
                string vh_operation_state = ((int)Convert2VehicleOperationState(vh)).ToString();
                string vh_communication_state = ((int)Convert2VehicleCommunication(vh)).ToString();
                string vh_control_state = ((int)Convert2VehicleControlMode(vh)).ToString();
                string vh_Jam_state = ((int)Convert2VehicleJamState(vh)).ToString();

                S6F11.RPTINFO.RPTITEM.VIDITEM_252_SV.MONITORED_VEHICLE_INFO vh_info =
                new S6F11.RPTINFO.RPTITEM.VIDITEM_252_SV.MONITORED_VEHICLE_INFO()
                {
                    VehicleID = vh_id,
                    VehicleCurrentPosition = cur_postion,
                    VehicleDistanceFromCurrentPosition = distance,
                    VehicleCurrentDomain = "",
                    VehicleNextPosition = next_position,
                    VehicleOperationState = vh_operation_state,
                    VehicleCommunicationState = vh_communication_state,
                    VehicleControlMode = vh_control_state,
                    VehicleJamState = vh_Jam_state
                };
                viditem_252.MONITORED_VEHICLE_INFO_OBJ[i] = vh_info;
            }
            return viditem_252;
        }

        VehicleOperationState Convert2VehicleOperationState(AVEHICLE vh)
        {
            if (!vh.isTcpIpConnect)
            {
                return VehicleOperationState.Disconnected;
            }
            else if (vh.IsError)
            {
                return VehicleOperationState.Error;
            }
            else if (vh.ACT_STATUS == ProtocolFormat.OHTMessage.VHActionStatus.NoCommand)
            {
                return VehicleOperationState.Stooped;
            }
            else if (vh.ACT_STATUS == ProtocolFormat.OHTMessage.VHActionStatus.Commanding)
            {
                return VehicleOperationState.Operating;
            }
            else
            {
                return VehicleOperationState.Stooped;
            }
        }

        VehicleCommunictionState Convert2VehicleCommunication(AVEHICLE vh)
        {
            if (!vh.isTcpIpConnect)
            {
                return VehicleCommunictionState.Disconnected;
            }
            else
            {
                if (vh.IsCommunication(scApp.getBCFApplication()))
                {
                    return VehicleCommunictionState.Communicating;
                }
                else
                {
                    return VehicleCommunictionState.NoCommunicating;
                }
            }
        }
        VehicleControlMode Convert2VehicleControlMode(AVEHICLE vh)
        {
            if (!vh.isTcpIpConnect) return VehicleControlMode.Manual;
            if (vh.MODE_STATUS >= ProtocolFormat.OHTMessage.VHModeStatus.AutoRemote)
            {
                return VehicleControlMode.Auto;
            }
            else
            {
                return VehicleControlMode.Manual;
            }
        }
        VehicleJamState Convert2VehicleJamState(AVEHICLE vh)
        {
            return vh.IsBlocking ? VehicleJamState.JamExists : VehicleJamState.NoJan;
        }
        enum VehicleOperationState
        {
            Disconnected,
            Operating,
            Stooped,
            Error,
            Detached
        }
        enum VehicleCommunictionState
        {
            Disconnected,
            Communicating,
            NoCommunicating
        }
        enum VehicleControlMode
        {
            Manual,
            Auto
        }
        enum VehicleJamState
        {
            NoJan,
            JamExists,
            Stuck
        }


        #endregion Build VIDItem
        protected override void S1F15ReceiveRequestOffLine(object sender, SECSEventArgs e)
        {
            try
            {
                string msg = string.Empty;
                S1F15 s1f15 = ((S1F15)e.secsHandler.Parse<S1F15>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s1f15);
                if (!isProcess(s1f15)) { return; }


                S1F16 s1f16 = new S1F16();
                s1f16.SystemByte = s1f15.SystemByte;
                s1f16.SECSAgentName = scApp.EAPSecsAgentName;

                s1f16.OFLACK = SECSConst.OFLACK_Accepted;


                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s1f16);
                SCUtility.secsActionRecordMsg(scApp, false, s1f16);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S1F18 Error:{0}", rtnCode);
                }

                if (SCUtility.isMatche(s1f16.OFLACK, SECSConst.OFLACK_Accepted))
                {
                    scApp.LineService.OfflineWithHostByHost();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S1F17_Receive_OnlineRequest", ex.ToString());
            }
        }


        protected override void S1F17ReceiveRequestOnLine(object sender, SECSEventArgs e)
        {
            try
            {
                string msg = string.Empty;
                S1F17 s1f17 = ((S1F17)e.secsHandler.Parse<S1F17>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s1f17);


                S1F18 s1f18 = new S1F18();
                s1f18.SystemByte = s1f17.SystemByte;
                s1f18.SECSAgentName = scApp.EAPSecsAgentName;


                //檢查狀態是否允許連線
                //if (DebugParameter.RejectEAPOnline)
                if (!scApp.LineService.canOnlineWithHost())
                {
                    s1f18.ONLACK = SECSConst.ONLACK_Not_Accepted;
                }
                else if (line.Host_Control_State == SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote)
                {
                    s1f18.ONLACK = SECSConst.ONLACK_Equipment_Already_On_Line;
                    msg = "OHS is online remote ready!!"; //A0.05
                }
                else
                {
                    s1f18.ONLACK = SECSConst.ONLACK_Accepted;
                }

                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s1f18);
                SCUtility.secsActionRecordMsg(scApp, false, s1f18);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S1F18 Error:{0}", rtnCode);
                }

                if (SCUtility.isMatche(s1f18.ONLACK, SECSConst.ONLACK_Accepted))
                {
                    scApp.LineService.OnlineWithHostByHost();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S1F17_Receive_OnlineRequest", ex.ToString());
            }
        }

        protected override void S5F5ReceiveListAlarmRequest(object sender, SECSEventArgs e)
        {
            try
            {
                S5F5 s5f5 = ((S5F5)e.secsHandler.Parse<S5F5>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s5f5);
                if (!isProcess(s5f5)) { return; }
                string[] alarm_codes = s5f5.ALID?.Split(',');

                List<AlarmMap> alarm_maps = scApp.AlarmBLL.loadAlarmMaps();
                string[] alarm_ids = scApp.AlarmBLL.getCurrentAlarmsFromRedis().Select(alarm => alarm.ALAM_CODE).ToArray();
                S5F6 s5f6 = null;
                s5f6 = new S5F6();
                s5f6.SystemByte = s5f5.SystemByte;
                s5f6.SECSAgentName = scApp.EAPSecsAgentName;
                if (alarm_codes == null)//填所有Alarm資料到S5F6
                {
                    s5f6.ALIDS = new S5F6.ALID_1[alarm_maps.Count];
                    for (int i = 0; i < alarm_maps.Count; i++)
                    {
                        string alarm_id = alarm_maps[i].ALARM_ID;
                        int ialarm_id = 0;
                        int.TryParse(alarm_id, out ialarm_id);
                        alarm_id = (ialarm_id < 0 ? ialarm_id * -1 : ialarm_id).ToString();
                        s5f6.ALIDS[i] = new S5F6.ALID_1();
                        bool is_set = alarm_ids.Contains(alarm_maps[i].ALARM_ID);
                        s5f6.ALIDS[i].ALCD = is_set ? "1" : "0";
                        s5f6.ALIDS[i].ALID = alarm_id;
                        s5f6.ALIDS[i].ALTX = alarm_maps[i].ALARM_DESC;
                    }
                }
                else
                {
                    s5f6.ALIDS = new S5F6.ALID_1[alarm_codes.Length];
                    for (int i = 0; i < alarm_codes.Length; i++)//填S5F6資料
                    {
                        s5f6.ALIDS[i] = new S5F6.ALID_1();
                        if (string.IsNullOrEmpty(alarm_codes[i]))
                        {
                            continue; //alarm_code空白不用填值
                        }
                        else
                        {
                            foreach (AlarmMap a in alarm_maps)
                            {
                                if (SCUtility.isMatche(a.ALARM_ID, alarm_codes[i]))
                                {
                                    string alarm_id = a.ALARM_ID;
                                    int ialarm_id = 0;
                                    int.TryParse(alarm_id, out ialarm_id);
                                    alarm_id = (ialarm_id < 0 ? ialarm_id * -1 : ialarm_id).ToString();
                                    bool is_set = alarm_ids.Contains(a.ALARM_ID);
                                    s5f6.ALIDS[i].ALCD = is_set ? "1" : "0";
                                    s5f6.ALIDS[i].ALID = alarm_id;
                                    s5f6.ALIDS[i].ALTX = a.ALARM_DESC;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }


                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s5f6);
                SCUtility.secsActionRecordMsg(scApp, false, s5f6);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S5F6 Error:{0}", rtnCode);
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S5F5ReceiveListAlarmRequest", ex.ToString());
            }
        }
        #endregion Receive 

        #region Send
        public override bool S1F13SendEstablishCommunicationRequest()
        {
            try
            {
                S1F13 s1f13 = new S1F13();
                s1f13.SECSAgentName = scApp.EAPSecsAgentName;
                s1f13.MDLN = scApp.getEQObjCacheManager().getLine().LINE_ID.Trim();
                s1f13.SOFTREV = SCApplication.getMessageString("SYSTEM_VERSION");

                S1F14 s1f14 = null;
                string rtnMsg = string.Empty;
                SXFY abortSecs = null;
                SCUtility.secsActionRecordMsg(scApp, false, s1f13);

                TrxSECS.ReturnCode rtnCode = ISECSControl.sendRecv<S1F14>(bcfApp, s1f13, out s1f14, out abortSecs, out rtnMsg, null);
                SCUtility.actionRecordMsg(scApp, s1f13.StreamFunction, line.Real_ID, "Establish Communication.", rtnCode.ToString());

                if (rtnCode == TrxSECS.ReturnCode.Normal)
                {
                    SCUtility.secsActionRecordMsg(scApp, true, s1f14);
                    if (SCUtility.isMatche(s1f14.COMMACK, SECSConst.COMMACK_ACK))
                    {
                        line.EstablishComm = true;
                    }
                    else
                    {
                        //當收到MCS的拒絕On 通訊以後，就把SECS的連線直接關閉
                        scApp.LineService.stopHostCommunication();
                    }
                    return true;
                }
                else
                {
                    line.EstablishComm = false;
                    logger.Warn("Send Establish Communication[S1F13] Error!");
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, " sendS1F13_Establish_Comm", ex.ToString());
            }
            return false;
        }
        public override bool S5F1SendAlarmReport(string alcd, string alid, string altx)
        {
            try
            {
                S5F1 s5f1 = new S5F1()
                {
                    SECSAgentName = scApp.EAPSecsAgentName,
                    ALCD = alcd,
                    ALID = alid,
                    ALTX = altx
                };
                S5F2 s5f2 = null;
                SXFY abortSecs = null;
                String rtnMsg = string.Empty;
                if (isSend())
                {

                    TrxSECS.ReturnCode rtnCode = ISECSControl.sendRecv<S5F2>(bcfApp, s5f1, out s5f2,
                        out abortSecs, out rtnMsg, null);
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GEMDriver), Device: DEVICE_NAME_MCS,
                       Data: s5f1);
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(GEMDriver), Device: DEVICE_NAME_MCS,
                       Data: s5f2);
                    SCUtility.actionRecordMsg(scApp, s5f1.StreamFunction, line.Real_ID,
                        "Send Alarm Report.", rtnCode.ToString());
                    if (rtnCode != TrxSECS.ReturnCode.Normal)
                    {
                        logger.Warn("Send Alarm Report[S5F1] Error![rtnCode={0}]", rtnCode);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
                return false;
            }
        }

        public override bool S2F17SendDateAndTimeRequest()
        {
            try
            {
                S2F17 s2f17 = new S2F17();
                s2f17.SECSAgentName = scApp.EAPSecsAgentName;

                S2F18 s2f18 = null;
                string rtnMsg = string.Empty;
                SXFY abortSecs = null;
                SCUtility.secsActionRecordMsg(scApp, false, s2f17);

                TrxSECS.ReturnCode rtnCode = ISECSControl.sendRecv<S2F18>(bcfApp, s2f17, out s2f18, out abortSecs, out rtnMsg, null);
                SCUtility.actionRecordMsg(scApp, s2f17.StreamFunction, line.Real_ID, "Date Time Request.", rtnCode.ToString());

                if (rtnCode == TrxSECS.ReturnCode.Normal)
                {
                    SCUtility.secsActionRecordMsg(scApp, true, s2f18);
                    string timeStr = s2f18.TIME;
                    DateTime mesDateTime = DateTime.Now;
                    try
                    {
                        mesDateTime = DateTime.ParseExact(timeStr.Trim(), SCAppConstants.TimestampFormat_16, CultureInfo.CurrentCulture);
                    }
                    catch (Exception dtEx)
                    {
                        logger.Error(dtEx, String.Format("Receive Date Time Set Request From MES. Format Error![Date Time:{0}]",
                            timeStr));
                    }

                    if (!DebugParameter.DisableSyncTime)
                    {
                        SCUtility.updateSystemTime(mesDateTime);
                    }
                    //todo 跟其他設備同步
                    return true;
                }
                else
                    logger.Warn("Send Date Time Request[S2F17] Error!");
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "sendS2F17_DateTimeReq", ex.ToString());
            }

            return false;
        }

        public override bool S6F11SendEquiptmentOffLine()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Equipment_OFF_LINE, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }
        public override bool S6F11SendControlStateLocal()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Control_Status_Local, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }
        public override bool S6F11SendControlStateRemote()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Control_Status_Remote, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTSCAutoInitiated()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_TSC_Auto_Initiated, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTSCPaused()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_TSC_Paused, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTSCAutoCompleted()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_TSC_Auto_Completed, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTSCPauseInitiated(string pausrReason)
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                Vids.VIDITEM_301_DVVAL_PauseReason.PAUSE_REASON = pausrReason;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_TSC_Pause_Initiated, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTSCPauseCompleted()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_TSC_Pause_Completed, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendAlarmCleared()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Alarm_Cleared, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendAlarmSet()
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Alarm_Set, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                S6F11SendMessage(mcs_queue);
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public bool S6F11SendUnitAlarmCleared(string unitID, string alarmID, string alarmTest, string vhCurrentPosition, string unitStatusCleable, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                Vids.VIDITEM_70_DVVAL_VehicleID.VEHILCE_ID = unitID;
                Vids.VIDITEM_01_DVVAL_AlarmID.ALARM_ID = alarmID;
                Vids.VIDITEM_211_DVVAL_UnitID.UNIT_ID = unitID;
                Vids.VIDITEM_212_DVVAL_AlarmText.ALARM_TEXT = alarmTest;
                Vids.VIDITEM_251_DVVAL_VehicleCurrentPosition.VEHICLE_CURRENT_POSITION = vhCurrentPosition;
                Vids.VIDITEM_120_DVVAL_UnitStatusCleable.UNIT_STATUS_CLEABLE = unitStatusCleable;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Unit_Alarm_Cleared, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public bool S6F11SendUnitAlarmSet(string unitID, string alarmID, string alarmTest, string vhCurrentPosition, string unitStatusCleable, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                Vids.VIDITEM_70_DVVAL_VehicleID.VEHILCE_ID = unitID;
                Vids.VIDITEM_01_DVVAL_AlarmID.ALARM_ID = alarmID;
                Vids.VIDITEM_211_DVVAL_UnitID.UNIT_ID = unitID;
                Vids.VIDITEM_212_DVVAL_AlarmText.ALARM_TEXT = alarmTest;
                Vids.VIDITEM_251_DVVAL_VehicleCurrentPosition.VEHICLE_CURRENT_POSITION = vhCurrentPosition;
                Vids.VIDITEM_120_DVVAL_UnitStatusCleable.UNIT_STATUS_CLEABLE = unitStatusCleable;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Unit_Alarm_Set, Vids);
                scApp.ReportBLL.insertMCSReport(mcs_queue);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;

        }


        public override bool sendS6F11_PortOutOfService(string port_id, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                Vids.VIDITEM_115_DVVAL_PortID.PORT_ID = port_id;

                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Port_Out_Of_Service, Vids);
                if (mcs_queue == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }


                return true;

            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }
        public override bool sendS6F11_PortInService(string port_id, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                Vids.VIDITEM_115_DVVAL_PortID.PORT_ID = port_id;

                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Port_In_Service, Vids);
                if (mcs_queue == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTransferInitial(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(cmdID);

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                Vids.VIDITEM_59_DVVAL_CommandInfo.COMMAND_ID = cmdID;
                Vids.VIDITEM_59_DVVAL_CommandInfo.PRIORITY = mcs_cmd.PRIORITY.ToString();
                Vids.VIDITEM_59_DVVAL_CommandInfo.REPLACE = mcs_cmd.REPLACE.ToString();//不知道Replace要填什麼 , For Kevin Wei to Confirm

                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.CARRIER_ID = mcs_cmd.CARRIER_ID;
                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.SOURCE_PORT = mcs_cmd.HOSTSOURCE;
                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.DESTINATION_PORT = mcs_cmd.HOSTDESTINATION;
                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].CARRIER_LOC = mcs_cmd.HOSTSOURCE;


                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Initiated, Vids);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendTransferring(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transferring, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }
        public override bool S6F11SendVehicleArrived(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Arrived, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleAcquireStarted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Acquire_Started, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleAcquireCompleted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Acquire_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleAssigned(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Assigned, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleDeparted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Departed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleDepositStarted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Deposit_Started, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleDepositCompleted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Deposit_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendCarrierInstalled(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Carrier_Installed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendCarrierRemoved(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Carrier_Removed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendVehicleUnassinged(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Unassigned, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferCompleted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }
        public override bool S6F11SendTransferCompleted(ACMD_MCS CMD_MCS, AVEHICLE vh, string resultCode, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;

                VIDCollection vid_collection = new VIDCollection();
                vid_collection.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                vid_collection.VIDITEM_59_DVVAL_CommandInfo.COMMAND_ID = CMD_MCS.CMD_ID.Trim();
                vid_collection.VIDITEM_59_DVVAL_CommandInfo.PRIORITY = CMD_MCS.PRIORITY.ToString();
                vid_collection.VIDITEM_59_DVVAL_CommandInfo.REPLACE = CMD_MCS.REPLACE.ToString();
                string carrier_loc = "";
                //if (CMD_MCS.TRANSFERSTATE >= E_TRAN_STATUS.Transferring)
                if (CMD_MCS.COMMANDSTATE >= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE)
                {
                    AVEHICLE carry_vh = scApp.VehicleBLL.cache.getVehicleByCSTID(CMD_MCS.CARRIER_ID);
                    if (carry_vh != null)
                        carrier_loc = carry_vh.Real_ID;
                }
                else
                {
                    carrier_loc = CMD_MCS.HOSTSOURCE;
                }

                //A0.01 vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.CARRIER_ID = CMD_MCS.CMD_ID.Trim();
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.CARRIER_ID = CMD_MCS.CARRIER_ID.Trim();//A0.01
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.SOURCE_PORT = CMD_MCS.HOSTSOURCE;
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.DESTINATION_PORT = CMD_MCS.HOSTDESTINATION;
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].CARRIER_LOC = carrier_loc;

                vid_collection.VIDITEM_64_DVVAL_ResultCode.RESULT_CODE = resultCode;

                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }
        public override bool S6F11SendTransferCancelCompleted(ACMD_MCS cmd, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                Vids.VIDITEM_59_DVVAL_CommandInfo.COMMAND_ID = cmd.CMD_ID;
                Vids.VIDITEM_59_DVVAL_CommandInfo.PRIORITY = cmd.PRIORITY.ToString();
                Vids.VIDITEM_59_DVVAL_CommandInfo.REPLACE = cmd.REPLACE.ToString();

                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.CARRIER_ID = cmd.CARRIER_ID;
                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.SOURCE_PORT = cmd.HOSTSOURCE;
                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.DESTINATION_PORT = cmd.HOSTDESTINATION;
                Vids.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].CARRIER_LOC = cmd.HOSTSOURCE;


                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Cancel_Completed, Vids);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }



        public override bool S6F11PortEventStateChanged(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Abort_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public bool S6F11LaneInService(string startPoint, string endPoint, string laneCutType, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                VIDCollection vid_collection = new VIDCollection();
                vid_collection.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_INFO_OBJ.StartPoint = startPoint;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_INFO_OBJ.EndPoint = endPoint;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_CUT_TYPE = laneCutType;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_CUT_TYPE = laneCutType;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_LaneInService, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }
        public bool S6F11LaneOutOfService(string startPoint, string endPoint, string laneCutType, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                VIDCollection vid_collection = new VIDCollection();
                vid_collection.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_INFO_OBJ.StartPoint = startPoint;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_INFO_OBJ.EndPoint = endPoint;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_CUT_TYPE = laneCutType;
                vid_collection.VIDITEM_330_DVVAL_LaneCutInfo.LANE_CUT_TYPE = laneCutType;
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_LaneOutOfService, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override AMCSREPORTQUEUE S6F11BulibMessage(string ceid, object vidCollection)
        {
            try
            {
                VIDCollection Vids = vidCollection as VIDCollection;
                string ceidOfname = string.Empty;
                SECSConst.CEID_Dictionary.TryGetValue(ceid, out ceidOfname);
                string ceid_name = $"CEID:[{ceidOfname}({ceid})]";
                S6F11 s6f11 = new S6F11()
                {
                    SECSAgentName = scApp.EAPSecsAgentName,
                    DATAID = "0",
                    CEID = ceid,
                    StreamFunctionName = ceid_name
                };
                //List<string> RPTIDs = SECSConst.DicCEIDAndRPTID[ceid];
                List<string> RPTIDs = SECSConst.getDicCEIDAndRPTID(ceid);
                s6f11.INFO.ITEM = new S6F11.RPTINFO.RPTITEM[RPTIDs.Count];

                for (int i = 0; i < RPTIDs.Count; i++)
                {
                    string rpt_id = RPTIDs[i];
                    s6f11.INFO.ITEM[i] = new S6F11.RPTINFO.RPTITEM();
                    List<ARPTID> AVIDs = SECSConst.getDicRPTIDAndVID(rpt_id);
                    List<string> VIDs = AVIDs.OrderBy(avid => avid.ORDER_NUM).Select(avid => avid.VID.Trim()).ToList();
                    s6f11.INFO.ITEM[i].RPTID = rpt_id;
                    s6f11.INFO.ITEM[i].VIDITEM = new SXFY[AVIDs.Count];
                    for (int j = 0; j < AVIDs.Count; j++)
                    {
                        string vid = VIDs[j];
                        SXFY vid_item = null;
                        switch (vid)
                        {
                            case SECSConst.VID_AlarmID:
                                vid_item = Vids.VIDITEM_01_DVVAL_AlarmID;
                                break;
                            case SECSConst.VID_Control_State:
                                vid_item = Vids.VIDITEM_06_SV_ControlState;
                                break;
                            case SECSConst.VID_Carrier_ID:
                                vid_item = Vids.VIDITEM_54_DVVAL_CarrierID;
                                break;
                            case SECSConst.VID_Carrier_Info:
                                vid_item = Vids.VIDITEM_55_DVVAL_CarrierInfo;
                                break;
                            case SECSConst.VID_Carrier_Loc:
                                vid_item = Vids.VIDITEM_56_DVVAL_CarrierLoc;
                                break;
                            case SECSConst.VID_Command_ID:
                                vid_item = Vids.VIDITEM_58_DVVAL_CommandID;
                                break;
                            case SECSConst.VID_Command_Info:
                                vid_item = Vids.VIDITEM_59_DVVAL_CommandInfo;
                                break;
                            case SECSConst.VID_Dest_Port:
                                vid_item = Vids.VIDITEM_60_DVVAL_DestPort;
                                break;
                            case SECSConst.VID_Eqp_Name:
                                vid_item = Vids.VIDITEM_61_ECV_EqpName;
                                break;
                            case SECSConst.VID_Priority:
                                vid_item = Vids.VIDITEM_62_DVVAL_Priority;
                                break;
                            case SECSConst.VID_Replace:
                                vid_item = Vids.VIDITEM_63_DVVAL_Replace;
                                break;
                            case SECSConst.VID_Result_Code:
                                vid_item = Vids.VIDITEM_64_DVVAL_ResultCode;
                                break;
                            case SECSConst.VID_65_Source_Port:
                                vid_item = Vids.VIDITEM_65_DVVAL_SourcePort;
                                break;
                            case SECSConst.VID_Transfer_Command:
                                vid_item = Vids.VIDITEM_66_DVVAL_TransferCommand;
                                break;
                            case SECSConst.VID_Transfer_Info:
                                vid_item = Vids.VIDITEM_67_DVVAL_TransferInfo;
                                break;
                            case SECSConst.VID_Transfer_Port:
                                vid_item = Vids.VIDITEM_68_DVVAL_TransferPort;
                                break;
                            case SECSConst.VID_Transfer_Port_List:
                                vid_item = Vids.VIDITEM_69_DVVAL_TransferPortList;
                                break;
                            case SECSConst.VID_Vehicle_ID:
                                vid_item = Vids.VIDITEM_70_DVVAL_VehicleID;
                                break;
                            case SECSConst.VID_Vehicle_Info:
                                vid_item = Vids.VIDITEM_71_DVVAL_VehicleInfo;
                                break;
                            case SECSConst.VID_Vehicle_State:
                                vid_item = Vids.VIDITEM_72_DVVAL_VehicleState;
                                break;
                            case SECSConst.VID_TSC_State:
                                vid_item = Vids.VIDITEM_73_SV_TSCState;
                                break;
                            case SECSConst.VID_Command_Type:
                                vid_item = Vids.VIDITEM_74_DVVAL_CommandType;
                                break;
                            case SECSConst.VID_EnhancedCarri_Info:
                                vid_item = Vids.VIDITEM_75_DVVAL_EnhancedCarriInfo;
                                break;
                            case SECSConst.VID_78_Source_Port:
                                vid_item = Vids.VIDITEM_78_SV_SourcePort;
                                break;
                            case SECSConst.VID_Spec_Version:
                                vid_item = Vids.VIDITEM_114_SV_SpecVersion;
                                break;
                            case SECSConst.VID_Port_ID:
                                vid_item = Vids.VIDITEM_115_DVVAL_PortID;
                                break;
                            case SECSConst.VID_Vehicle_Location:
                                vid_item = Vids.VIDITEM_117_DVVAL_VehicleLocation;
                                break;
                            case SECSConst.VID_Unit_Status_Cleable:
                                vid_item = Vids.VIDITEM_120_DVVAL_UnitStatusCleable;
                                break;
                            case SECSConst.VID_Install_Time:
                                vid_item = Vids.VIDITEM_204_DVVAL_InstallTime;
                                break;
                            case SECSConst.VID_Alarm_Text:
                                vid_item = Vids.VIDITEM_212_DVVAL_AlarmText;
                                break;
                            case SECSConst.VID_Vehicle_Current_Position:
                                vid_item = Vids.VIDITEM_251_DVVAL_VehicleCurrentPosition;
                                break;
                            case SECSConst.VID_Vehicle_Next_Position:
                                vid_item = Vids.VIDITEM_262_DVVAL_VehicleNextPosition;
                                break;
                            case SECSConst.VID_Transfer_Complete_Info:
                                vid_item = Vids.VIDITEM_79_DVVAL_TransferCompleteInfo;
                                break;
                            case SECSConst.VID_Pause_Reason:
                                vid_item = Vids.VIDITEM_301_DVVAL_PauseReason;
                                break;
                            case SECSConst.VID_Lane_Cut_Info:
                                vid_item = Vids.VIDITEM_330_DVVAL_LaneCutInfo;
                                break;
                            case SECSConst.VID_Unit_ID:
                                vid_item = Vids.VIDITEM_211_DVVAL_UnitID;
                                break;

                        }
                        s6f11.INFO.ITEM[i].VIDITEM[j] = vid_item;
                    }
                }

                return BuildMCSReport
                (s6f11,
                  Vids.VIDITEM_58_DVVAL_CommandID.COMMAND_ID
                , Vids.VH_ID
                , Vids.VIDITEM_115_DVVAL_PortID.PORT_ID);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return null;
            }
        }
        private AMCSREPORTQUEUE BuildMCSReport(S6F11 sxfy, string cmd_id, string vh_id, string port_id)
        {
            byte[] byteArray = SCUtility.ToByteArray(sxfy);
            DateTime reportTime = DateTime.Now;
            AMCSREPORTQUEUE queue = new AMCSREPORTQUEUE()
            {
                SERIALIZED_SXFY = byteArray,
                INTER_TIME = reportTime,
                REPORT_TIME = reportTime,
                STREAMFUNCTION_NAME = string.Concat(sxfy.StreamFunction, '-', sxfy.StreamFunctionName),
                STREAMFUNCTION_CEID = sxfy.CEID,
                MCS_CMD_ID = cmd_id,
                VEHICLE_ID = vh_id,
                PORT_ID = port_id
            };
            return queue;
        }
        protected override Boolean isSend(SXFY sxfy)
        {
            Boolean result = false;
            try
            {
                if (sxfy is S6F11)
                {
                    S6F11 s6f11 = (sxfy as S6F11);
                    if (s6f11.CEID == SECSConst.CEID_Equipment_OFF_LINE ||
                        s6f11.CEID == SECSConst.CEID_Control_Status_Local ||
                        s6f11.CEID == SECSConst.CEID_Control_Status_Remote)
                    {
                        return true;
                    }
                }
                result = scApp.getEQObjCacheManager().getLine().Host_Control_State == SCAppConstants.LineHostControlState.HostControlState.On_Line_Local ||
                    scApp.getEQObjCacheManager().getLine().Host_Control_State == SCAppConstants.LineHostControlState.HostControlState.On_Line_Remote;
                //if (bcf.Common.BCFUtility.isMatche(sxfy.StreamFunction, "S6F11"))
                //{
                //    S6F11 s6f11 = null;
                //    string ceid = (string)sxfy.getField(bcf.Common.BCFUtility.getPropertyName(() => s6f11.CEID));
                //    if (!eventBLL.isEnableReport(ceid))
                //    {
                //        return false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}",
                    line.LINE_ID, "isSendEAP", ex.ToString());
            }
            return result;
        }
        public override bool S6F11SendMessage(AMCSREPORTQUEUE queue)
        {
            try
            {

                LogHelper.setCallContextKey_ServiceID(CALL_CONTEXT_KEY_WORD_SERVICE_ID_MCS);

                S6F11 s6f11 = (S6F11)SCUtility.ToObject(queue.SERIALIZED_SXFY);

                S6F12 s6f12 = null;
                SXFY abortSecs = null;
                String rtnMsg = string.Empty;

                if (!isSend(s6f11)) return true;


                SCUtility.RecodeReportInfo(queue.VEHICLE_ID, queue.MCS_CMD_ID, s6f11, s6f11.CEID);
                SCUtility.secsActionRecordMsg(scApp, false, s6f11);
                TrxSECS.ReturnCode rtnCode = ISECSControl.sendRecv<S6F12>(bcfApp, s6f11, out s6f12,
                    out abortSecs, out rtnMsg, null);
                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                //   Data: s6f11,
                //   VehicleID: queue.VEHICLE_ID,
                //   XID: queue.MCS_CMD_ID);
                SCUtility.secsActionRecordMsg(scApp, false, s6f12);
                SCUtility.actionRecordMsg(scApp, s6f11.StreamFunction, line.Real_ID,
                            "sendS6F11_common.", rtnCode.ToString());
                SCUtility.RecodeReportInfo(queue.VEHICLE_ID, queue.MCS_CMD_ID, s6f12, s6f11.CEID, rtnCode.ToString());
                //LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                //   Data: s6f12,
                //   VehicleID: queue.VEHICLE_ID,
                //   XID: queue.MCS_CMD_ID);

                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger_MapActionLog.Warn("Send Transfer Initiated[S6F11] Error![rtnCode={0}]", rtnCode);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
                return false;
            }
        }

        #endregion Send


        #region VID Info
        private VIDCollection AVIDINFO2VIDCollection(AVIDINFO vid_info)
        {
            if (vid_info == null)
                return null;
            //string carrier_loc = string.Empty;
            //string port_id = string.Empty;
            //scApp.MapBLL.getPortID(vid_info.CARRIER_LOC, out carrier_loc);
            //scApp.MapBLL.getPortID(vid_info.PORT_ID, out port_id);
            var line = scApp.getEQObjCacheManager().getLine();
            VIDCollection vid_collection = new VIDCollection();
            vid_collection.VH_ID = vid_info.EQ_ID;


            AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vid_info.EQ_ID);
            //VID_01_AlarmID
            vid_collection.VIDITEM_01_DVVAL_AlarmID.ALARM_ID = vid_info.ALARM_ID;

            //VID_06_Control
            string control_state = SCAppConstants.LineHostControlState.convert2MES(line.Host_Control_State);
            vid_collection.VIDITEM_06_SV_ControlState.CONTROLSTATE = control_state;

            //VID_54_CarrierID
            vid_collection.VIDITEM_54_DVVAL_CarrierID.CARRIER_ID = vid_info.CARRIER_ID;

            //VID_55_CarrierInfo
            vid_collection.VIDITEM_55_DVVAL_CarrierInfo.CARRIER_ID = vid_info.CARRIER_ID;
            vid_collection.VIDITEM_55_DVVAL_CarrierInfo.VEHICLE_ID = vh.Real_ID;
            vid_collection.VIDITEM_55_DVVAL_CarrierInfo.CARRIER_LOC = vid_info.CARRIER_LOC;

            //VID_54_CarrierID
            vid_collection.VIDITEM_56_DVVAL_CarrierLoc.CARRIER_LOC = vid_info.CARRIER_LOC;

            //VID_57_Command Name
            //vid_collection.VIDITEM_57_DVVAL_CommandName.COMMAND_NAME = vid_info.COMMAND_ID; //todo 需確認Command Name要填入什麼?

            //VID_58_CommandID
            vid_collection.VIDITEM_58_DVVAL_CommandID.COMMAND_ID = vid_info.COMMAND_ID;

            //VID_59_CommandInfo
            vid_collection.VIDITEM_59_DVVAL_CommandInfo.COMMAND_ID = vid_info.COMMAND_ID;
            vid_collection.VIDITEM_59_DVVAL_CommandInfo.PRIORITY = vid_info.PRIORITY.ToString();
            vid_collection.VIDITEM_59_DVVAL_CommandInfo.REPLACE = vid_info.REPLACE.ToString();//不知道Replace要填什麼 , For Kevin Wei to Confirm

            //VIDITEM_60_DVVAL_DestPort
            vid_collection.VIDITEM_60_DVVAL_DestPort.DESTINATION_PORT = vid_info.DESTPORT;

            //VIDITEM_61_ECV_EqpName
            vid_collection.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

            //VIDITEM_62_DVVAL_Priority
            vid_collection.VIDITEM_62_DVVAL_Priority.PRIORITY = vid_info.PRIORITY.ToString();
            //VIDITEM_63_DVVAL_Replace
            vid_collection.VIDITEM_63_DVVAL_Replace.REPLACE = vid_info.REPLACE.ToString();

            //VIDITEM_64_DVVAL_ResultCode
            vid_collection.VIDITEM_64_DVVAL_ResultCode.RESULT_CODE = vid_info.RESULT_CODE.ToString();

            //VIDITEM_65_DVVAL_SourcePort
            vid_collection.VIDITEM_65_DVVAL_SourcePort.SOURCE_PORT = vid_info.SOURCEPORT;

            //VIDITEM_66_DVVAL_TransferCommand
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.COMMAND_INFO.COMMAND_ID = vid_info.COMMAND_ID;
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.COMMAND_INFO.PRIORITY = vid_info.PRIORITY.ToString();
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.COMMAND_INFO.REPLACE = string.Empty;//不知道Replace要填什麼 , For Kevin Wei to Confirm
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.TRANSFER_INFOS = new S6F11.RPTINFO.RPTITEM.VIDITEM_67_DVVAL[1];
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.TRANSFER_INFOS[0] = new S6F11.RPTINFO.RPTITEM.VIDITEM_67_DVVAL();
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.TRANSFER_INFOS[0].CARRIER_ID = vid_info.CARRIER_ID;
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.TRANSFER_INFOS[0].SOURCE_PORT = vid_info.SOURCEPORT;
            vid_collection.VIDITEM_66_DVVAL_TransferCommand.TRANSFER_INFOS[0].DESTINATION_PORT = vid_info.DESTPORT;

            //VIDITEM_67_DVVAL_TransferInfo
            vid_collection.VIDITEM_67_DVVAL_TransferInfo.CARRIER_ID = vid_info.CARRIER_ID;
            vid_collection.VIDITEM_67_DVVAL_TransferInfo.SOURCE_PORT = vid_info.SOURCEPORT;
            vid_collection.VIDITEM_67_DVVAL_TransferInfo.DESTINATION_PORT = vid_info.DESTPORT;

            //VIDITEM_68_DVVAL_TransferPort
            vid_collection.VIDITEM_68_DVVAL_TransferPort.TRANSFER_PORT = vid_info.PORT_ID;

            //VIDITEM_69_DVVAL_TransferPort
            vid_collection.VIDITEM_69_DVVAL_TransferPortList.TRANSFER_PORT_OBJ = new S6F11.RPTINFO.RPTITEM.VIDITEM_68_DVVAL[1]; //todo 要確認該欄位要填入什麼
            vid_collection.VIDITEM_69_DVVAL_TransferPortList.TRANSFER_PORT_OBJ[0] = new S6F11.RPTINFO.RPTITEM.VIDITEM_68_DVVAL();
            vid_collection.VIDITEM_69_DVVAL_TransferPortList.TRANSFER_PORT_OBJ[0].TRANSFER_PORT = vid_info.PORT_ID;
            //VIDITEM_70_DVVAL_VehicleID
            vid_collection.VIDITEM_70_DVVAL_VehicleID.VEHILCE_ID = vh.Real_ID;

            //VIDITEM_71_DVVAL_VehicleInfo
            vid_collection.VIDITEM_71_DVVAL_VehicleInfo.VEHICLE_ID = vh.Real_ID;
            vid_collection.VIDITEM_71_DVVAL_VehicleInfo.VEHICLE_STATE = ((int)vh.State).ToString();

            //VIDITEM_72_DVVAL_VehicleState
            vid_collection.VIDITEM_72_DVVAL_VehicleState.VEHICLE_STATE = ((int)vh.State).ToString();

            //VIDITEM_73_SV_TSCState
            string tsc_state = ((int)line.TSC_state_machine.State).ToString();
            vid_collection.VIDITEM_73_SV_TSCState.TSCState = tsc_state;

            //VIDITEM_74_DVVAL_CommandType
            vid_collection.VIDITEM_74_DVVAL_CommandType.COMMAND_TYPE = vid_info.COMMAND_TYPE;

            //VIDITEM_74_DVVAL_CommandType
            vid_collection.VIDITEM_75_DVVAL_EnhancedCarriInfo.CARRIER_ID = vid_info.CARRIER_ID;
            vid_collection.VIDITEM_75_DVVAL_EnhancedCarriInfo.VRHICLE_ID = vh.Real_ID;
            vid_collection.VIDITEM_75_DVVAL_EnhancedCarriInfo.CARRIER_LOC = vid_info.CARRIER_LOC;
            vid_collection.VIDITEM_75_DVVAL_EnhancedCarriInfo.INSTALL_TIME = vid_info.CARRIER_INSTALLED_TIME?.ToString(SCAppConstants.TimestampFormat_16);

            //VIDITEM_74_DVVAL_CommandType
            vid_collection.VIDITEM_78_SV_SourcePort.SOURCE_PORT = vid_info.SOURCEPORT;

            //VIDITEM_74_DVVAL_CommandType
            vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.CARRIER_ID = vid_info.CARRIER_ID;
            vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.SOURCE_PORT = vid_info.SOURCEPORT;
            vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.DESTINATION_PORT = vid_info.DESTPORT;
            vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].CARRIER_LOC = vid_info.CARRIER_LOC;

            //VIDITEM_114_SV_SpecVersion
            vid_collection.VIDITEM_114_SV_SpecVersion.SPEC_VERSION = "";//todo 要確認要填入什麼值

            //VIDITEM_115_DVVAL_PortID
            vid_collection.VIDITEM_115_DVVAL_PortID.PORT_ID = vid_info.PORT_ID;

            //VIDITEM_117_DVVAL_VehicleLocation
            vid_collection.VIDITEM_117_DVVAL_VehicleLocation.VEHICLE_LOCATION = vid_info.PORT_ID;

            //VIDITEM_120_DVVAL_UnitStatusCleable
            vid_collection.VIDITEM_120_DVVAL_UnitStatusCleable.UNIT_STATUS_CLEABLE = "Y";

            //VIDITEM_202_DVVAL_TransferState
            //vid_collection.VIDITEM_202_DVVAL_TransferState.TRANSFER_STATE =vid_info.

            //VIDITEM_204_DVVAL_InstallTime
            vid_collection.VIDITEM_204_DVVAL_InstallTime.INSTALL_TIME = vid_info.CARRIER_INSTALLED_TIME?
                                                                        .ToString(SCAppConstants.TimestampFormat_16);//要填入INSTALL_TIME

            //VIDITEM_251_DVVAL_VehicleCurrentPosition
            vid_collection.VIDITEM_251_DVVAL_VehicleCurrentPosition.VEHICLE_CURRENT_POSITION = SCUtility.isEmpty(vh.CUR_ADR_ID) ? "0" : vh.CUR_ADR_ID;

            //VIDITEM_262_DVVAL_VehicleNextPosition
            vid_collection.VIDITEM_262_DVVAL_VehicleNextPosition.VEHICLE_NEXT_POSITION = "";//todo 要確認要填入的資料



            return vid_collection;
        }
        #endregion VID Info

        public virtual void doInit()
        {
            string eapSecsAgentName = scApp.EAPSecsAgentName;
            reportBLL = scApp.ReportBLL;

            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S1F1", S1F1ReceiveAreYouThere);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S1F3", S1F3ReceiveSelectedEquipmentStatusRequest);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S1F13", S1F13ReceiveEstablishCommunicationRequest);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S1F15", S1F15ReceiveRequestOffLine);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S1F17", S1F17ReceiveRequestOnLine);

            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F17", S2F17ReceiveDateAndTimeRequest);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F31", S2F31ReceiveDateTimeSetReq);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F33", S2F33ReceiveDefineReport);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F35", S2F35ReceiveLinkEventReport);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F37", S2F37ReceiveEnableDisableEventReport);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F41", S2F41ReceiveHostCommand);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S2F49", S2F49ReceiveEnhancedRemoteCommandExtension);

            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S5F3", S5F3ReceiveEnableDisableAlarm);
            ISECSControl.addSECSReceivedHandler(bcfApp, eapSecsAgentName, "S5F5", S5F5ReceiveListAlarmRequest);

            ISECSControl.addSECSConnectedHandler(bcfApp, eapSecsAgentName, secsConnected);
            ISECSControl.addSECSDisconnectedHandler(bcfApp, eapSecsAgentName, secsDisconnected);
        }
        protected override void S2F37ReceiveEnableDisableEventReport(object sender, SECSEventArgs e)
        {
            try
            {
                S2F37 s2f37 = ((S2F37)e.secsHandler.Parse<S2F37>(e));
                SCUtility.secsActionRecordMsg(scApp, true, s2f37);
                if (!isProcess(s2f37)) { return; }
                Boolean isValid = true;
                //Boolean isEnable = SCUtility.isMatche(s2f37.CEED, SECSConst.CEED_Enable);
                Boolean isEnable = s2f37.CEED[0] == 255;

                int cnt = s2f37.CEIDS.Length;
                if (cnt == 0)
                {
                    isValid &= scApp.EventBLL.enableAllEventReport(isEnable);
                }
                else
                {
                    //Check Data
                    for (int ix = 0; ix < cnt; ++ix)
                    {
                        string ceid = s2f37.CEIDS[ix].PadLeft(3, '0');
                        Boolean isContain = SECSConst.CEID_ARRAY.Contains(ceid.Trim());
                        if (!isContain)
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (isValid)
                    {
                        for (int ix = 0; ix < cnt; ++ix)
                        {
                            string ceid = s2f37.CEIDS[ix].PadLeft(3, '0');
                            isValid &= scApp.EventBLL.enableEventReport(ceid, isEnable);
                        }
                    }
                }

                S2F38 s2f18 = null;
                s2f18 = new S2F38()
                {
                    SystemByte = s2f37.SystemByte,
                    SECSAgentName = scApp.EAPSecsAgentName,
                    ERACK = isValid ? SECSConst.ERACK_Accepted : SECSConst.ERACK_Denied_At_least_one_CEID_dose_not_exist
                };

                TrxSECS.ReturnCode rtnCode = ISECSControl.replySECS(bcfApp, s2f18);
                SCUtility.secsActionRecordMsg(scApp, false, s2f18);
                if (rtnCode != TrxSECS.ReturnCode.Normal)
                {
                    logger.Warn("Reply EQPT S2F18 Error:{0}", rtnCode);
                }
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}", line.LINE_ID, "S2F17_Receive_Date_Time_Req", ex.ToString());
            }
        }

        protected override void S2F31ReceiveDateTimeSetReq(object sender, SECSEventArgs e)
        {
            try
            {
                S2F31 s2f31 = ((S2F31)e.secsHandler.Parse<S2F31>(e));

                SCUtility.secsActionRecordMsg(scApp, true, s2f31);
                SCUtility.actionRecordMsg(scApp, s2f31.StreamFunction, line.Real_ID,
                        "Receive Date Time Set Request From MES.", "");
                if (!isProcess(s2f31)) { return; }

                S2F32 s2f32 = new S2F32();
                s2f32.SECSAgentName = scApp.EAPSecsAgentName;
                s2f32.SystemByte = s2f31.SystemByte;
                s2f32.TIACK = SECSConst.TIACK_Accepted;

                string timeStr = s2f31.TIME;
                DateTime mesDateTime = DateTime.Now;
                try
                {
                    mesDateTime = DateTime.ParseExact(timeStr.Trim(), SCAppConstants.TimestampFormat_16, CultureInfo.CurrentCulture);
                }
                catch (Exception dtEx)
                {
                    s2f32.TIACK = SECSConst.TIACK_Error_not_done;
                }

                SCUtility.secsActionRecordMsg(scApp, false, s2f32);
                ISECSControl.replySECS(bcfApp, s2f32);

                if (!DebugParameter.DisableSyncTime)
                {
                    SCUtility.updateSystemTime(mesDateTime);
                }

                //TODO 與設備同步
            }
            catch (Exception ex)
            {
                logger.Error("MESDefaultMapAction has Error[Line Name:{0}],[Error method:{1}],[Error Message:{2}",
                    line.LINE_ID, "S2F31_Receive_Date_Time_Set_Req", ex.ToString());
            }
        }

        public override bool S6F11SendTransferAbortCompleted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Abort_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferAbortFailed(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfoByMCSCmdID(cmdID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Abort_Failed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferAbortInitial(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfoByMCSCmdID(cmdID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Abort_Initiated, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferCancelCompleted(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfo(vhID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Cancel_Completed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferCancelFailed(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfoByMCSCmdID(cmdID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Cancel_Failed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferCancelInitial(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfoByMCSCmdID(cmdID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Cancel_Initiated, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }
        public override bool S6F11SendTransferCancelInitial(ACMD_MCS cmd, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                VIDCollection vid_collection = new VIDCollection();
                vid_collection.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                vid_collection.VIDITEM_59_DVVAL_CommandInfo.COMMAND_ID = cmd.CMD_ID;
                vid_collection.VIDITEM_59_DVVAL_CommandInfo.PRIORITY = cmd.PRIORITY.ToString();
                vid_collection.VIDITEM_59_DVVAL_CommandInfo.REPLACE = cmd.REPLACE.ToString();
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.CARRIER_ID = cmd.CARRIER_ID;
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.SOURCE_PORT = cmd.HOSTSOURCE;
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].TRANSFER_INFO_OBJ.DESTINATION_PORT = cmd.HOSTDESTINATION;
                vid_collection.VIDITEM_79_DVVAL_TransferCompleteInfo.TRANCOMPLETEINFO[0].CARRIER_LOC = cmd.HOSTSOURCE;


                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Cancel_Initiated, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferPaused(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfoByMCSCmdID(cmdID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Pause, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendTransferResumed(string cmdID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {
                if (!isSend()) return true;
                AVIDINFO vid_info = scApp.VIDBLL.getVIDInfoByMCSCmdID(cmdID);
                VIDCollection vid_collection = AVIDINFO2VIDCollection(vid_info);
                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Transfer_Resumed, vid_collection);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
                return false;
            }
        }

        public override bool S6F11SendCarrierInstalled(string vhID, string carrierID, string transferPort, List<AMCSREPORTQUEUE> reportQueues = null)
        {

            try
            {

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                //VID_54_CarrierID
                Vids.VIDITEM_54_DVVAL_CarrierID.CARRIER_ID = carrierID;

                //Vids.VIDITEM_68_DVVAL_TransferPort.TRANSFER_PORT = "";
                Vids.VIDITEM_68_DVVAL_TransferPort.TRANSFER_PORT = transferPort;
                Vids.VIDITEM_56_DVVAL_CarrierLoc.CARRIER_LOC = vhID;

                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Carrier_Installed, Vids);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendCarrierRemoved(string vhID, string carrierID, string transferPort, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                //VID_54_CarrierID
                Vids.VIDITEM_54_DVVAL_CarrierID.CARRIER_ID = carrierID;

                //Vids.VIDITEM_68_DVVAL_TransferPort.TRANSFER_PORT = "";
                Vids.VIDITEM_68_DVVAL_TransferPort.TRANSFER_PORT = transferPort;
                Vids.VIDITEM_56_DVVAL_CarrierLoc.CARRIER_LOC = vhID;

                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Carrier_Removed, Vids);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendVehicleInstalled(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                //VIDITEM_70_DVVAL_VehicleID
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vhID);
                Vids.VIDITEM_70_DVVAL_VehicleID.VEHILCE_ID = vh.Real_ID;

                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Installed, Vids);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }

        public override bool S6F11SendVehicleRemoved(string vhID, List<AMCSREPORTQUEUE> reportQueues = null)
        {
            try
            {

                VIDCollection Vids = new VIDCollection();
                Vids.VIDITEM_61_ECV_EqpName.EQPT_NAME = line.LINE_ID;

                //VIDITEM_70_DVVAL_VehicleID
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vhID);
                Vids.VIDITEM_70_DVVAL_VehicleID.VEHILCE_ID = vh.Real_ID;


                AMCSREPORTQUEUE mcs_queue = S6F11BulibMessage(SECSConst.CEID_Vehicle_Removed, Vids);
                if (reportQueues == null)
                {
                    S6F11SendMessage(mcs_queue);
                }
                else
                {
                    reportQueues.Add(mcs_queue);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(COSTMCSDefaultMapAction), Device: DEVICE_NAME_MCS,
                   Data: ex);
            }
            return true;
        }
    }
}