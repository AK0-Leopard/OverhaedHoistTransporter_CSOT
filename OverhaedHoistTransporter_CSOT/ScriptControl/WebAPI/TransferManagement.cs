﻿using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Threading;
namespace com.mirle.ibg3k0.sc.WebAPI
{
    public class TransferManagement : NancyModule
    {
        //SCApplication app = null;
        const string restfulContentType = "application/json; charset=utf-8";
        const string urlencodedContentType = "application/x-www-form-urlencoded; charset=utf-8";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        Timer ThreadTimer = null;
        public TransferManagement()
        {
            //app = SCApplication.getInstance();
            RegisterTransferManagementEvent();
            After += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");

        }

        private void RegisterTransferManagementEvent()
        {

            Post["TransferManagement/MCSQueueSwitch"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                string result = string.Empty;

                try
                {
                    string AutoAssign = Request.Query.AutoAssign.Value ?? Request.Form.AutoAssign.Value ?? string.Empty;
                    bool isAutoAssign = Convert.ToBoolean(AutoAssign);
                    scApp.getEQObjCacheManager().getLine().MCSCommandAutoAssign = isAutoAssign;
                    result = "OK";
                }
                catch (Exception ex)
                {
                    result = "MCS Queue Switch update failed with exception happened";
                }

                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };

            Post["TransferManagement/CancelAbort"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                bool isSuccess = true;
                string result = string.Empty;

                CMDCancelType cnacel_type = default(CMDCancelType);
                string mcs_cmd_id = Request.Query.mcs_cmd.Value ?? Request.Form.mcs_cmd.Value ?? string.Empty;
                try
                {
                    ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                    if (mcs_cmd == null)
                    {
                        result = $"Can not find transfer command:[{mcs_cmd_id}].";
                    }
                    else
                    {
                        if (mcs_cmd.TRANSFERSTATE < sc.E_TRAN_STATUS.Transferring)
                        {
                            cnacel_type = CMDCancelType.CmdCancel;
                            bool btemp = scApp.VehicleService.doCancelOrAbortCommandByMCSCmdID(mcs_cmd_id, cnacel_type);
                            if (btemp)
                            {
                                result = "OK";
                            }
                            else
                            {
                                result = $"Transfer command:[{mcs_cmd_id}] cancel failed.";
                            }
                        }
                        else if (mcs_cmd.TRANSFERSTATE < sc.E_TRAN_STATUS.Canceling)
                        {
                            cnacel_type = CMDCancelType.CmdAbort;
                            bool btemp = scApp.VehicleService.doCancelOrAbortCommandByMCSCmdID(mcs_cmd_id, cnacel_type);
                            if (btemp)
                            {
                                result = "OK";
                            }
                            else
                            {
                                result = $"Transfer command:[{mcs_cmd_id}] adort failed.";
                            }
                        }
                        else
                        {
                            result = $"Command ID:{mcs_cmd.CMD_ID.Trim()} can't excute cancel / abort,\r\ncurrent state:{mcs_cmd.TRANSFERSTATE}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    result = "Execption happend!";
                    logger.Error(ex, "Execption:");
                }
                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };


            Post["TransferManagement/ForceFinish"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                bool isSuccess = true;
                string result = string.Empty;
                string mcs_cmd_id = Request.Query.mcs_cmd.Value ?? Request.Form.mcs_cmd.Value ?? string.Empty;
                AVEHICLE excute_cmd_of_vh = scApp.VehicleBLL.cache.getVehicleByMCSCmdID(mcs_cmd_id);
                ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                try
                {
                    if (excute_cmd_of_vh != null)
                    {
                        scApp.VehicleBLL.doTransferCommandFinish(excute_cmd_of_vh.VEHICLE_ID, excute_cmd_of_vh.OHTC_CMD, CompleteStatus.CmpStatusForceFinishByOp);
                        scApp.VIDBLL.initialVIDCommandInfo(excute_cmd_of_vh.VEHICLE_ID);
                    }
                    scApp.CMDBLL.updateCMD_MCS_TranStatus2Complete(mcs_cmd.CMD_ID, E_TRAN_STATUS.Aborted);
                    scApp.ReportBLL.newReportTransferCommandNormalFinish(mcs_cmd, excute_cmd_of_vh, sc.Data.SECS.CSOT.SECSConst.CMD_Result_Unsuccessful, null);
                    scApp.SysExcuteQualityBLL.doCommandFinish(mcs_cmd.CMD_ID, CompleteStatus.CmpStatusForceFinishByOp, E_CMD_STATUS.AbnormalEndByOHTC);
                    result = "OK";
                }
                catch
                {
                    result = "ForceFinish failed.";
                }

                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };

            Post["TransferManagement/AssignVehicle"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                string result = string.Empty;

                string mcs_cmd_id = Request.Query.mcs_cmd.Value ?? Request.Form.mcs_cmd.Value ?? string.Empty;
                string vh_id = Request.Query.vh_id.Value ?? Request.Form.vh_id.Value ?? string.Empty;
                try
                {
                    ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                    scApp.CMDBLL.assignCommnadToVehicle(mcs_cmd_id, vh_id, out result);
                }
                catch (Exception ex)
                {
                    result = "Assign command to  vehicle failed with exception happened.";
                }

                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };
            Post["TransferManagement/ShiftCommand"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                string result = string.Empty;

                string mcs_cmd_id = Request.Query.mcs_cmd.Value ?? Request.Form.mcs_cmd.Value ?? string.Empty;
                string vh_id = Request.Query.vh_id.Value ?? Request.Form.vh_id.Value ?? string.Empty;

                try
                {
                    ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                    scApp.CMDBLL.commandShift(mcs_cmd_id, vh_id, out result);
                }
                catch (Exception ex)
                {
                    result = "Shift command failed with exception happened.";
                }

                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };

            Post["TransferManagement/ChangeStatus"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                string result = string.Empty;
                bool isSuccess = true;
                string mcs_cmd_id = Request.Query.mcs_cmd.Value ?? Request.Form.mcs_cmd.Value ?? string.Empty;
                string sstatus = Request.Query.status.Value ?? Request.Form.status.Value ?? string.Empty;
                try
                {
                    ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                    E_TRAN_STATUS status = (E_TRAN_STATUS)Enum.Parse(typeof(E_TRAN_STATUS), sstatus, false);

                    if (mcs_cmd != null)
                    {
                        isSuccess = scApp.CMDBLL.updateCMD_MCS_TranStatus(mcs_cmd_id, status);
                        if (isSuccess)
                        {
                            result = "OK";
                        }
                        else
                        {
                            result = "Update status failed.";
                        }
                    }
                    else
                    {
                        result = $"Can not find MCS Command[{mcs_cmd_id}].";
                    }
                }
                catch (Exception ex)
                {
                    result = "Update status failed with exception happened.";
                }

                //Todo by Mark

                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };

            Post["TransferManagement/Priority"] = (p) =>
            {
                var scApp = SCApplication.getInstance();
                string result = string.Empty;
                bool isSuccess = true;
                string mcs_cmd_id = Request.Query.mcs_cmd.Value ?? Request.Form.mcs_cmd.Value ?? string.Empty;
                string priority = Request.Query.priority.Value ?? Request.Form.priority.Value ?? string.Empty;
                try
                {
                    ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(mcs_cmd_id);
                    int iPriority = Convert.ToInt32(priority);
                    if (mcs_cmd != null)
                    {
                        isSuccess = scApp.CMDBLL.updateCMD_MCS_PrioritySUM(mcs_cmd, iPriority);
                        if (isSuccess)
                        {
                            result = "OK";
                        }
                        else
                        {
                            result = "Update priority failed.";
                        }
                    }
                    else
                    {
                        result = $"Can not find MCS Command[{mcs_cmd_id}].";
                    }
                }
                catch (Exception ex)
                {
                    result = "Update priority failed with exception happened.";
                }

                var response = (Response)result;
                response.ContentType = restfulContentType;
                return response;
            };




        }
    }
}