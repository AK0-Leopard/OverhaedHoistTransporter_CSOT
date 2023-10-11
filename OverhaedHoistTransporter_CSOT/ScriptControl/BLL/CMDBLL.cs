//*********************************************************************************
//      MESDefaultMapAction.cs
//*********************************************************************************
// File Name: MESDefaultMapAction.cs
// Description: Type 1 Function
//
//(c) Copyright 2018, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
// 2020/01/15    KevinWei       N/A            A0.01   增加當有故障車在路上時，也直接回傳產生命令失敗。
// 2020/01/15    KevinWei       N/A            A0.02   改由呼叫端去結束命令。
// 2020/06/30    MarkChou       N/A            A0.03   檢查命令路徑是否有重複的Section，有的話就去前面那個。
// 2020/07/28    MarkChou       N/A            A0.04   派送命令前，先檢查車輛的ACT_STATUS是否為NoCommand
//**********************************************************************************

using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data;
using com.mirle.ibg3k0.sc.Data.DAO;
using com.mirle.ibg3k0.sc.Data.DAO.EntityFramework;
using com.mirle.ibg3k0.sc.Data.SECS.CSOT;
using com.mirle.ibg3k0.sc.Data.ValueDefMapAction;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.ibg3k0.sc.Service;
using Mirle.Protos.ReserveModule;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace com.mirle.ibg3k0.sc.BLL
{
    public class CMDBLL
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CMD_OHTCDao cmd_ohtcDAO = null;
        CMD_OHTC_DetailDao cmd_ohtc_detailDAO = null;
        CMD_MCSDao cmd_mcsDao = null;
        HCMD_MCSDao hcmd_mcsDao = null;
        HCMD_OHTCDao hcmd_ohtcDao = null;
        TestTranTaskDao testTranTaskDao = null;
        ReturnCodeMapDao return_code_mapDao = null;

        protected static Logger logger_VhRouteLog = LogManager.GetLogger("VhRoute");
        private string[] ByPassSegment = null;
        ParkZoneTypeDao parkZoneTypeDao = null;
        private SCApplication scApp = null;

        public Cache cache { get; private set; }


        public CMDBLL()
        {

        }
        public void start(SCApplication app)
        {
            scApp = app;
            cmd_ohtcDAO = scApp.CMD_OHTCDao;
            cmd_ohtc_detailDAO = scApp.CMD_OHT_DetailDao;
            cmd_mcsDao = scApp.CMD_MCSDao;
            parkZoneTypeDao = scApp.ParkZoneTypeDao;
            testTranTaskDao = scApp.TestTranTaskDao;
            return_code_mapDao = scApp.ReturnCodeMapDao;
            hcmd_mcsDao = scApp.HCMD_MCSDao;
            hcmd_ohtcDao = scApp.HCMD_OHTCDao;
            initialByPassSegment();
            cache = new Cache(scApp);
        }

        private void initialByPassSegment()
        {
            if (SCUtility.isMatche(scApp.BC_ID, SCAppConstants.WorkVersion.VERSION_NAME_OHS100))
            {
                ByPassSegment = new string[] { "003", "030", "025-026-027", "043", "042", "038", "034" };
            }
            else if (SCUtility.isMatche(scApp.BC_ID, SCAppConstants.WorkVersion.VERSION_NAME_TAICHUNG))
            {
                ByPassSegment = new string[] { };
            }
            else
            {
                ByPassSegment = new string[] { };
            }
        }
        public void initialMapAction()
        {
        }



        #region CMD_MCS
        public string doCheckMCSCommand(string command_id, string Priority, string carrier_id, string HostSource, string HostDestination, out string check_result)
        {
            try
            {
                check_result = string.Empty;
                string checkcode = SECSConst.HCACK_Confirm;
                bool isSuccess = true;
                int ipriority = 0;
                string from_adr = string.Empty;
                string to_adr = string.Empty;
                E_VH_TYPE vh_type = E_VH_TYPE.None;
                //確認命令是否已經執行中

                if (isSuccess)
                {
                    var cmd_obj = scApp.CMDBLL.getCMD_MCSByID(command_id);
                    if (cmd_obj != null)
                    {
                        check_result = $"MCS command id:{command_id} already exist.";
                        return SECSConst.HCACK_Rejected_Already_Requested;
                    }
                }

                if (isSuccess)
                {
                    if (SCUtility.isMatche(HostSource, HostDestination))
                    {
                        check_result = $"MCS command of source port:{HostSource} and destination port:{HostDestination} is same.";
                        return SECSConst.HCACK_Param_Invalid;
                    }
                }

                bool isSourceOnVehicle = scApp.VehicleBLL.getVehicleByRealID(HostSource) != null;
                if (isSuccess)
                {
                    if (isSourceOnVehicle)
                    {
                        AVEHICLE carray_vh = scApp.VehicleBLL.getVehicleByRealID(HostSource);
                        if (carray_vh.HAS_CST == 0)
                        {
                            check_result = $"Vh:{HostSource.Trim()},not carray cst.";
                            return SECSConst.HCACK_Current_Not_Able_Execute;
                        }
                        else if (!SCUtility.isMatche(carray_vh.CST_ID, carrier_id))
                        {
                            check_result = $"Vh:{HostSource.Trim()}, current carray cst id:{carray_vh.CST_ID} ,not matche host carrier id:{carrier_id}.";
                            return SECSConst.HCACK_Current_Not_Able_Execute;
                        }
                        else
                        {
                            if (scApp.CMDBLL.getCMD_MCSIsUnfinishedCountByCarrierID(carrier_id) > 0)
                            {
                                check_result = $"Host carrier:{carrier_id} already have command.";
                                return SECSConst.HCACK_Current_Not_Able_Execute;
                            }
                        }
                    }
                    else
                    {
                        if (!scApp.MapBLL.getAddressID(HostSource, out from_adr, out vh_type))
                        {
                            isSuccess = false;
                            checkcode = SECSConst.HCACK_Param_Invalid;
                            check_result = $"No find {nameof(HostSource)}={HostSource} of adr.";
                        }
                        //多判斷同樣的Source是否已經有命令存在且沒還被搬走，有的話要拒絕MCS命令
                        if (isSuccess)
                        {
                            //ACMD_MCS waitting_cmd_mcs = getWatingCMD_MCSByFrom(HostSource);
                            //bool has_waittring_transfer_cmd_mcs = waitting_cmd_mcs != null;
                            bool has_waittring_transfer_cmd_mcs = hasWatingCmdMcsByFrom(HostSource);
                            if (has_waittring_transfer_cmd_mcs)
                            {
                                isSuccess = false;
                                checkcode = SECSConst.HCACK_Rejected_Already_Requested;
                                check_result = $"MCS command id:{command_id} is same as orther mcs command of load port.";
                            }
                        }
                    }
                    //if (!isSourceOnVehicle &&
                    //!scApp.MapBLL.getAddressID(HostSource, out from_adr, out vh_type))
                    //{
                    //    isSuccess = false;
                    //    checkcode = SECSConst.HCACK_Param_Invalid;
                    //    check_result = $"No find {nameof(HostSource)}={HostSource} of adr.";
                    //}

                    if (!scApp.MapBLL.getAddressID(HostDestination, out to_adr))
                    {
                        isSuccess = false;
                        checkcode = SECSConst.HCACK_Param_Invalid;
                        check_result = $"No find {nameof(HostDestination)}={HostDestination} of adr.";
                    }
                }
                if (isSuccess)
                {
                    if (!int.TryParse(Priority, out ipriority))
                    {
                        isSuccess = false;
                        checkcode = SECSConst.HCACK_Param_Invalid;
                        check_result = $"The {nameof(Priority)}:[{Priority}] is invalid ";
                    }
                }
                if (isSuccess)
                {
                    bool IsSegmentInActive_Source = true;
                    bool IsSegmentInActive_Destination = true;
                    if (!isSourceOnVehicle)
                    {
                        IsSegmentInActive_Source = scApp.MapBLL.CheckSegmentInActiveByPortID(HostSource);
                        if (!IsSegmentInActive_Source)
                        {
                            check_result = $"{nameof(HostSource)} : {HostSource} of segment is disable";
                        }
                    }
                    IsSegmentInActive_Destination = scApp.MapBLL.CheckSegmentInActiveByPortID(HostDestination);
                    if (!IsSegmentInActive_Destination)
                    {
                        if (!SCUtility.isEmpty(check_result))
                            check_result += "\n";
                        check_result += $"{nameof(HostDestination)} : {HostDestination} of segment is disable";
                    }
                    if (!IsSegmentInActive_Source || !IsSegmentInActive_Destination)
                    {
                        isSuccess = false;
                        checkcode = SECSConst.HCACK_Enabled_Route_Does_Not_Exist;
                    }
                }
                //確認有VH可以派送
                if (!isSourceOnVehicle && isSuccess)
                {
                    scApp.MapBLL.getAddressID(HostSource, out from_adr, out vh_type);
                    //AVEHICLE may_be_can_carry_vh = scApp.VehicleBLL.findBestSuitableVhStepByStepFromAdr(from_adr, vh_type
                    //                                                                                    , is_check_has_vh_carry: true);
                    AVEHICLE may_be_can_carry_vh = scApp.VehicleBLL.findBestSuitableVhStepByNearest(from_adr, vh_type
                                                                                                        , is_check_has_vh_carry: true);
                    if (may_be_can_carry_vh == null)
                    {
                        isSuccess = false;
                        checkcode = SECSConst.HCACK_Current_Not_Able_Execute;
                        check_result = "Can't find vehicle to carry";
                    }
                }
                //確認Source 2 Traget可以通
                if (!isSourceOnVehicle && isSuccess)
                {
                    if (!scApp.RouteGuide.checkRoadIsWalkableForMCSCommand(from_adr, to_adr))
                    {
                        isSuccess = false;
                        checkcode = SECSConst.HCACK_Enabled_Route_Does_Not_Exist;
                        check_result = "The road is not walkable, source to destination ";
                    }
                }
                return checkcode;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
                check_result = $"check MCS command id:{command_id} ,has exception happend";
                return SECSConst.HCACK_Current_Not_Able_Execute;
            }
        }

        public bool doCreatMCSCommand(string command_id, string Priority, string replace, string carrier_id, string HostSource, string HostDestination, string checkcode)
        {
            bool isSuccess = true;
            int ipriority = 0;
            try
            {
                if (!int.TryParse(Priority, out ipriority))
                {
                    logger.Warn("command id :{0} of priority parse fail. priority valus:{1}"
                                , command_id
                                , Priority);
                }
                int ireplace = 0;
                if (!int.TryParse(replace, out ireplace))
                {
                    logger.Warn("command id :{0} of priority parse fail. priority valus:{1}"
                                , command_id
                                , replace);
                }


                //ACMD_MCS mcs_com = creatCommand_MCS(command_id, ipriority, carrier_id, HostSource, HostDestination, checkcode);
                creatCommand_MCS(command_id, ipriority, ireplace, carrier_id, HostSource, HostDestination, checkcode);
                //if (mcs_com != null)
                //{
                //    isSuccess = true;
                //    scApp.SysExcuteQualityBLL.creatSysExcuteQuality(mcs_com);
                //    //mcsDefaultMapAction.sendS6F11_TranInit(command_id);
                //    scApp.ReportBLL.doReportTransferInitial(command_id);
                //    checkMCS_TransferCommand();
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;

        }
        int MAX_RETYIES = 2;
        public bool doCreatMCSCommandRetryHelp(string command_id, string Priority, string replace, string carrier_id, string HostSource, string HostDestination, string checkcode)
        {
            bool isSuccess = true;
            int ipriority = 0;
            int retry_count = 0;
            while (retry_count < MAX_RETYIES)
            {
                try
                {
                    retry_count++;
                    if (!int.TryParse(Priority, out ipriority))
                    {
                        logger.Warn("command id :{0} of priority parse fail. priority valus:{1}"
                                    , command_id
                                    , Priority);
                    }
                    int ireplace = 0;
                    if (!int.TryParse(replace, out ireplace))
                    {
                        logger.Warn("command id :{0} of priority parse fail. priority valus:{1}"
                                    , command_id
                                    , replace);
                    }
                    creatCommand_MCS(command_id, ipriority, ireplace, carrier_id, HostSource, HostDestination, checkcode);
                    isSuccess = true;
                    break;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {

                    logger.Error(ex, $"SqlException-Numer:{ex.Number}:creat command id :{command_id} dead lock happend. retry count:{retry_count}");
                    isSuccess = false;
                    if (retry_count >= MAX_RETYIES)
                    {
                        logger.Error(ex, $"SqlException:retry creat command id :{command_id} dead lock happend.over retry count:{retry_count} break retry action");
                        break;
                    }
                    //改成不判斷是否為Dead lock，只要是DB失敗，就直接retry 20221117
                    //if (ex.Number == DBConnection_EF.SQL_SERVER_ERROR_CODE_DEAD_LOCK)
                    //{
                    //    //Not thing...
                    //    logger.Warn($"creat command id :{command_id} dead lock happend. retry count:{retry_count}");
                    //    isSuccess = false;
                    //}
                    //else
                    //{
                    //    isSuccess = false;
                    //    break;
                    //}
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exection:");
                    isSuccess = false;
                    break;
                }
            }
            return isSuccess;
        }

        public ACMD_MCS creatCommand_MCS(string command_id, int Priority, int replace, string carrier_id, string HostSource, string HostDestination, string checkcode)
        {
            int port_priority = 0;
            if (!SCUtility.isEmpty(HostSource))
            {
                APORTSTATION source_portStation = scApp.getEQObjCacheManager().getPortStation(HostSource);

                if (source_portStation == null)
                {
                    logger.Warn($"MCS cmd of hostsource port[{HostSource} not exist.]");
                }
                else
                {
                    port_priority = source_portStation.PRIORITY;
                }
            }
            E_TRAN_STATUS transferState = E_TRAN_STATUS.Queue;
            if (checkcode != SECSConst.HCACK_Confirm)
            {
                transferState = E_TRAN_STATUS.Reject;
            }

            ACMD_MCS cmd = new ACMD_MCS()
            {
                CARRIER_ID = carrier_id,
                CMD_ID = command_id,
                TRANSFERSTATE = transferState,
                //COMMANDSTATE = SCAppConstants.TaskCmdStatus.Queue,
                COMMANDSTATE = 0,
                HOSTSOURCE = HostSource,
                HOSTDESTINATION = HostDestination,
                PRIORITY = Priority,
                CHECKCODE = checkcode,
                //PAUSEFLAG = "0",
                PAUSEFLAG = string.Empty,
                CMD_INSER_TIME = DateTime.Now,
                TIME_PRIORITY = 0,
                PORT_PRIORITY = port_priority,
                PRIORITY_SUM = Priority + port_priority,
                REPLACE = replace
            };
            if (creatCommand_MCS(cmd))
            {
                return cmd;
            }
            else
            {
                return null;
            }
        }
        public bool creatCommand_MCS(ACMD_MCS cmd_mcs)
        {
            bool isSuccess = true;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_mcsDao.add(con, cmd_mcs);
                con.Entry(cmd_mcs).State = EntityState.Detached;
            }
            return isSuccess;
        }

        public bool updateCMD_MCS_TranStatus(string cmd_id, E_TRAN_STATUS status)
        {
            bool isSuccess = true;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                cmd.TRANSFERSTATE = status;

                if (status == E_TRAN_STATUS.Queue)
                {
                    cmd.COMMANDSTATE = 0;
                    cmd.PAUSEFLAG = "";
                }
                cmd_mcsDao.update(con, cmd);
            }
            return isSuccess;
        }

        public bool updateCMD_MCS_TranStatus2Initial(string cmd_id)
        {
            bool isSuccess = true;
            //using (DBConnection_EF con = new DBConnection_EF())
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                    cmd.TRANSFERSTATE = E_TRAN_STATUS.Initial;
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_ENROUTE;
                    cmd.CMD_START_TIME = DateTime.Now;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_TranStatus2PreInitial(string cmd_id)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                    cmd.TRANSFERSTATE = E_TRAN_STATUS.PreInitial;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_PauseFlag(string cmd_id, string pauseFlag)
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                    cmd.PAUSEFLAG = pauseFlag;
                    cmd_mcsDao.update(con, cmd);
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                return false;
            }
        }


        public bool updateCMD_MCS_TranStatus2Transferring(string cmd_id)
        {
            bool isSuccess = true;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                cmd.TRANSFERSTATE = E_TRAN_STATUS.Transferring;
                cmd_mcsDao.update(con, cmd);
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_TranStatus2Canceling(string cmd_id)
        {
            bool isSuccess = true;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                cmd.TRANSFERSTATE = E_TRAN_STATUS.Canceling;
                cmd_mcsDao.update(con, cmd);
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_TranStatus2Canceled(string cmd_id)
        {
            bool isSuccess = true;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                cmd.TRANSFERSTATE = E_TRAN_STATUS.Canceled;
                cmd.CMD_FINISH_TIME = DateTime.Now;
                cmd_mcsDao.update(con, cmd);
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_TranStatus2Aborting(string cmd_id)
        {
            bool isSuccess = true;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                cmd.TRANSFERSTATE = E_TRAN_STATUS.Aborting;
                cmd_mcsDao.update(con, cmd);
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_TranStatus2Queue(string cmd_id)
        {
            bool isSuccess = true;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                cmd.TRANSFERSTATE = E_TRAN_STATUS.Queue;
                cmd.COMMANDSTATE = 0;
                cmd_mcsDao.update(con, cmd);
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_TranStatus2Complete(string cmd_id, E_TRAN_STATUS tran_status)
        {
            bool isSuccess = true;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmd_id);
                if (cmd != null)
                {
                    cmd.TRANSFERSTATE = tran_status;
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_COMMNAD_FINISH;
                    cmd.CMD_FINISH_TIME = DateTime.Now;
                    cmd_mcsDao.update(con, cmd);
                }
                else
                {
                    //isSuccess = false;
                }
            }
            return isSuccess;
        }
        const int MAX_TOP_PRIORITY_CMD_MCS_COUNT = 3;
        public (bool isSuccess, string reeason) tryUpdateCMD_MCS_PrioritySUMBoostToTopPriority(string cmdID)
        {
            try
            {
                int top_priority_cmd_cmd_count = getQueueCMD_MCSTopPriorityCount();
                if (top_priority_cmd_cmd_count >= MAX_TOP_PRIORITY_CMD_MCS_COUNT)
                {
                    return (false, $"top priority cmd count:{top_priority_cmd_cmd_count} reach max count, can't update priority");
                }

                var cmd_mcs = getCMD_MCSByID(cmdID);
                if (cmd_mcs == null)
                {
                    return (false, $"can't find mcs cmd:{cmdID}");
                }
                if (cmd_mcs.TRANSFERSTATE > E_TRAN_STATUS.Queue)
                {
                    return (false, $"currnet state:{cmd_mcs.TRANSFERSTATE} , can't update priority");
                }
                if (cmd_mcs.PRIORITY_SUM == CMD_MCS_TOP_PRIORITY)
                {
                    return (true, $"already top priority");
                }
                if (updateCMD_MCS_PrioritySUM(cmd_mcs, CMD_MCS_TOP_PRIORITY))
                {
                    return (true, $"update to top priority success");
                }
                else
                {
                    return (false, $"update to top priority fail");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
                return (false, $"update to top priority fail, exception happend");
            }
        }
        public const int CMD_MCS_TOP_PRIORITY = 9999;

        public bool updateCMD_MCS_PrioritySUMRestoreToOriginalPriority(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    int priority_sum = cmd.PRIORITY + cmd.PORT_PRIORITY + cmd.TIME_PRIORITY;
                    cmd.PRIORITY_SUM = priority_sum;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool updateCMD_MCS_PrioritySUM(ACMD_MCS mcs_cmd, int priority_sum)
        {
            bool isSuccess = true;
            //using (DBConnection_EF con = new DBConnection_EF())
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    con.ACMD_MCS.Attach(mcs_cmd);
                    mcs_cmd.PRIORITY_SUM = priority_sum;
                    cmd_mcsDao.update(con, mcs_cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }

        public void remoteCMD_MCSByBatch(List<ACMD_MCS> mcs_cmds)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_mcsDao.RemoteByBatch(con, mcs_cmds);
            }
        }



        public bool updateCMD_MCS_Priority(ACMD_MCS mcs_cmd, int priority)
        {
            bool isSuccess = true;
            //using (DBConnection_EF con = new DBConnection_EF())
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    con.ACMD_MCS.Attach(mcs_cmd);
                    mcs_cmd.PRIORITY = priority;
                    cmd_mcsDao.update(con, mcs_cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool updateCMD_MCS_TimePriority(ACMD_MCS mcs_cmd, int time_priority)
        {
            bool isSuccess = true;
            //using (DBConnection_EF con = new DBConnection_EF())
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    con.ACMD_MCS.Attach(mcs_cmd);
                    mcs_cmd.TIME_PRIORITY = time_priority;
                    cmd_mcsDao.update(con, mcs_cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }



        public bool updateCMD_MCS_CmdStatus2LoadArrivals(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_ARRIVE;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_CmdStatus2Loading(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOADING;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_CmdStatus2LoadComplete(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_CmdStatus2UnloadArrive(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_UNLOAD_ARRIVE;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_CmdStatus2Unloading(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_UNLOADING;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool updateCMD_MCS_CmdStatus2UnloadComplete(string cmdID)
        {
            bool isSuccess = true;
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    ACMD_MCS cmd = cmd_mcsDao.getByID(con, cmdID);
                    cmd.COMMANDSTATE = cmd.COMMANDSTATE | ACMD_MCS.COMMAND_STATUS_BIT_INDEX_UNLOAD_COMPLETE;
                    cmd_mcsDao.update(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                isSuccess = false;
            }
            return isSuccess;
        }





        public ACMD_MCS getCMD_MCSByID(string cmd_id)
        {
            ACMD_MCS cmd_mcs = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_mcs = cmd_mcsDao.getByID(con, cmd_id);
            }
            return cmd_mcs;
        }
        public ACMD_MCS getWatingCMD_MCSByFrom(string hostSource)
        {
            ACMD_MCS cmd_mcs = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_mcs = cmd_mcsDao.getWatingCMDMCSByFrom(con, hostSource);
            }
            return cmd_mcs;
        }

        public bool hasWatingCmdMcsByFrom(string hostSource)
        {
            try
            {
                int count = 0;
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    count = cmd_mcsDao.getWatingCMDMCSByFromOfCount(con, hostSource);
                }
                return count > 0;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exection:");
                throw ex;
            }
        }

        public int getCMD_MCSIsQueueCount()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsQueueCount(con);
            }
        }
        public int getQueueCMD_MCSTopPriorityCount()
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getQueueCMD_MCSTopPriorityCount(con, CMD_MCS_TOP_PRIORITY);
            }
        }


        public int getCMD_MCSIsRunningCount()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsExcuteCount(con);
            }
        }

        public int getCMD_MCSIsRunningCount(DateTime befor_time)
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsExcuteCount(con, befor_time);
            }
        }
        public int getCMD_MCSIsUnfinishedCount(List<string> port_ids)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsUnfinishedCount(con, port_ids);
            }
        }
        public int getCMD_MCSIsUnfinishedCountByCarrierID(string carrier_id)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsUnfinishedCountByCarrierID(con, carrier_id);
            }
        }
        public int getCMD_MCSIsUnfinishedCountByPortID(string portID)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsUnfinishedCountByPortID(con, portID);
            }
        }
        public ACMD_MCS getACMD_MCSUnfinishedByCmdID(string cmdID)
        {
            List<ACMD_MCS> cmds_mcs = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmds_mcs = cmd_mcsDao.loadACMD_MCSIsUnfinished(con);
            }
            return cmds_mcs.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, cmdID)).
                            FirstOrDefault();
        }
        public bool isTransferStatusReady(string cmdID, int status)
        {
            List<ACMD_MCS> cmds_mcs = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmds_mcs = cmd_mcsDao.loadACMD_MCSIsUnfinished(con);
            }
            var cmd = cmds_mcs.Where(c => SCUtility.isMatche(c.CMD_ID, cmdID)).
                               FirstOrDefault();
            if (cmd == null) return false;
            int check_value = cmd.COMMANDSTATE & status;
            bool is_status_ready = check_value == status;
            return is_status_ready;
        }
        public List<ACMD_MCS> loadACMD_MCSIsUnfinished()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.loadACMD_MCSIsUnfinished(con);
            }
        }
        public List<ACMD_MCS> loadExcuteAndNonLoaded()
        {
            List<ACMD_MCS> cmds_mcs = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmds_mcs = cmd_mcsDao.loadACMD_MCSIsUnfinished(con);
            }
            //1.找出目前所有的Transfer command且尚未載到貨的
            //2.如果準備去執行的OHT已經在Load Port的Segment就不執行
            //3.要過濾掉已經在執行command shift的命令(Pause flag = S)
            cmds_mcs =
                cmds_mcs.Where(cmd =>
                //cmd.TRANSFERSTATE >= E_TRAN_STATUS.Initial &&
                cmd.TRANSFERSTATE == E_TRAN_STATUS.Initial &&
                cmd.COMMANDSTATE >= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_ENROUTE &&
                //cmd.COMMANDSTATE <= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE &&
                cmd.COMMANDSTATE < ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_ARRIVE &&
                //!SCUtility.isMatche(cmd.PAUSEFLAG, ACMD_MCS.COMMAND_PAUSE_FLAG_COMMAND_SHIFT)).
                SCUtility.isEmpty(cmd.PAUSEFLAG)).
                ToList();
            return cmds_mcs;
        }

        public List<ACMD_MCS> loadFinishCMD_MCS()
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.loadFinishCMD_MCS(con);
            }
        }


        public List<ACMD_MCS> loadMCS_Command_Queue()
        {
            List<ACMD_MCS> ACMD_MCSs = list();
            return ACMD_MCSs;
        }


        private List<ACMD_MCS> list()
        {
            List<ACMD_MCS> ACMD_MCSs = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCSs = cmd_mcsDao.loadACMD_MCSIsQueue(con);
            }
            return ACMD_MCSs;
        }

        private List<ACMD_MCS> Sort(List<ACMD_MCS> list_cmd_mcs)
        {
            list_cmd_mcs = list_cmd_mcs.OrderByDescending(cmd => cmd.PRIORITY_SUM)
                                       .OrderBy(cmd => cmd.CMD_INSER_TIME)
                                       .ToList();
            return list_cmd_mcs;

        }


        public int getCMD_MCSInserCountLastHour(int hours)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSInserCountLastHour(con, hours);
            }
        }
        public int getCMD_MCSFinishCountLastHour(int hours)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSFinishCountLastHours(con, hours);
            }
        }


        private long syncTranCmdPoint = 0;
        public void checkMCS_TransferCommand()
        {
            if (System.Threading.Interlocked.Exchange(ref syncTranCmdPoint, 1) == 0)
            {
                try
                {
                    if (scApp.getEQObjCacheManager().getLine().ServiceMode
                        != SCAppConstants.AppServiceMode.Active)
                        return;

                    List<ACMD_MCS> unfinish_mcs_cmd = scApp.CMDBLL.loadACMD_MCSIsUnfinished();
                    unfinish_mcs_cmd.ForEach(cmd => cmd.setDestPortGroupID(scApp.PortStationBLL));
                    //ALINE line = scApp.getEQObjCacheManager().getLine();
                    //line.CurrentExcuteMCSCommands = unfinish_mcs_cmd == null ? new List<ACMD_MCS>() : unfinish_mcs_cmd;
                    refreshACMD_MCSInfoList(unfinish_mcs_cmd);

                    bool is_pause = IsPauseStateCheckAndChangeToPauseWhenPausing(unfinish_mcs_cmd);
                    if (is_pause)
                    {
                        return;
                    }
                    //if (scApp.getEQObjCacheManager().getLine().SCStats == ALINE.TSCState.PAUSING
                    //|| scApp.getEQObjCacheManager().getLine().SCStats == ALINE.TSCState.PAUSED)
                    //    return;

                    if (!scApp.getEQObjCacheManager().getLine().MCSCommandAutoAssign)
                        return;

                    if (unfinish_mcs_cmd == null || unfinish_mcs_cmd.Count == 0) return;
                    List<ACMD_MCS> queue_cmd_mcs = unfinish_mcs_cmd.Where(mcs_cmd => mcs_cmd.TRANSFERSTATE == E_TRAN_STATUS.Queue).
                                               ToList();
                    List<ACMD_MCS> excute_cmd_mcs = unfinish_mcs_cmd.Where(mcs_cmd => mcs_cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue).
                                               ToList();


                    if (queue_cmd_mcs != null && queue_cmd_mcs.Count > 0)
                    {
                        foreach (ACMD_MCS waitting_excute_mcs_cmd in queue_cmd_mcs)
                        {
                            //由於要避免同時多台車前往同一個Bay/群組進行搬送貨物造成堵塞，
                            //因此加入Port group的管理
                            var chcek_desc_port_group_enough_result = IsPortGroupCarryingCapacityEnough(waitting_excute_mcs_cmd, excute_cmd_mcs);
                            if (DebugParameter.isOpenPortGroupLimit && !chcek_desc_port_group_enough_result.isEnough)
                            {
                                LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: "OHx",
                                   Data: $"Cmd ID:{SCUtility.Trim(waitting_excute_mcs_cmd.CMD_ID, true)} source:{SCUtility.Trim(waitting_excute_mcs_cmd.HOSTSOURCE, true)} dest:{SCUtility.Trim(waitting_excute_mcs_cmd.HOSTDESTINATION, true)} will pass excute," +
                                         $"because dest group:{waitting_excute_mcs_cmd.DestPortGroupID} is over load.reason:{chcek_desc_port_group_enough_result.reason}");
                                SetTransferCommandNGReason(waitting_excute_mcs_cmd.CMD_ID, chcek_desc_port_group_enough_result.reason);
                                continue;
                            }
                            else
                            {
                                SetTransferCommandNGReason(waitting_excute_mcs_cmd.CMD_ID, "");
                            }
                            //ACMD_MCS excute_cmd = ACMD_MCSs[0];
                            string hostsource = waitting_excute_mcs_cmd.HOSTSOURCE;
                            string hostdest = waitting_excute_mcs_cmd.HOSTDESTINATION;
                            string from_adr = string.Empty;
                            string to_adr = string.Empty;
                            AVEHICLE bestSuitableVh = null;
                            E_VH_TYPE vh_type = E_VH_TYPE.None;
                            E_CMD_TYPE cmd_type = default(E_CMD_TYPE);

                            //      bool sourceIsVh = scApp.getEQObjCacheManager().getVehicletByVHID(hostsource) != null;
                            bool isSourceOnVehicle = scApp.VehicleBLL.getVehicleByRealID(hostsource) != null;
                            if (isSourceOnVehicle)
                            {
                                bestSuitableVh = scApp.VehicleBLL.getVehicleByRealID(hostsource);
                                //A0.04 if (bestSuitableVh.IsError || bestSuitableVh.MODE_STATUS != VHModeStatus.AutoRemote) 
                                if (bestSuitableVh.IsError || bestSuitableVh.MODE_STATUS != VHModeStatus.AutoRemote || bestSuitableVh.ACT_STATUS != VHActionStatus.NoCommand) //A0.04
                                {
                                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(VehicleBLL), Device: "OHxC",
                                       Data: $"vh id:{bestSuitableVh.VEHICLE_ID} current mode status is {bestSuitableVh.MODE_STATUS},is error flag:{bestSuitableVh.IsError},act status is {bestSuitableVh.ACT_STATUS}." +
                                             $"can't excute mcs command:{SCUtility.Trim(waitting_excute_mcs_cmd.CMD_ID)}",
                                       VehicleID: bestSuitableVh.VEHICLE_ID,
                                       CarrierID: bestSuitableVh.CST_ID);
                                    continue;
                                }
                                //當OHT狀態已恢復為Idel模式後，若準備要下unload給他的命令而他身上沒有該顆CST時，則直接將該命令取消
                                if (bestSuitableVh.HAS_CST == 0)
                                {
                                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(VehicleBLL), Device: "OHxC",
                                       Data: $"vh id:{bestSuitableVh.VEHICLE_ID} has mcs unload command id:{waitting_excute_mcs_cmd.CMD_ID}," +
                                             $" but vh current no cst in car (has cst flag:{bestSuitableVh.HAS_CST})," +
                                             $"force finish this transfer command",
                                       VehicleID: bestSuitableVh.VEHICLE_ID,
                                       CarrierID: bestSuitableVh.CST_ID);

                                    waitting_excute_mcs_cmd.COMMANDSTATE = ACMD_MCS.COMMAND_STATUS_BIT_CARRIER_UNKNOW_LOCATION;
                                    scApp.TransferService.forceFinishTransferCommand(waitting_excute_mcs_cmd, CompleteStatus.CmpStatusUnloadCommandCstnotExist);
                                    continue;
                                }
                                cmd_type = E_CMD_TYPE.Unload;
                            }
                            else
                            {
                                scApp.MapBLL.getAddressID(hostsource, out from_adr, out vh_type);
                                //bestSuitableVh = scApp.VehicleBLL.findBestSuitableVhStepByStepFromAdr(from_adr, vh_type);
                                bestSuitableVh = scApp.VehicleBLL.findBestSuitableVhStepByNearest(from_adr, vh_type);
                                cmd_type = E_CMD_TYPE.LoadUnload;
                            }
                            scApp.MapBLL.getAddressID(hostdest, out to_adr);

                            string vehicleId = string.Empty;

                            if (bestSuitableVh != null)
                                vehicleId = bestSuitableVh.VEHICLE_ID.Trim();
                            else
                            {
                                //int AccumulateTime_minute = 1;
                                int AccumulateTime_minute = SystemParameter.MCSCommandAccumulateTimePriority;
                                //int current_time_priority = (DateTime.Now - waitting_excute_mcs_cmd.CMD_INSER_TIME).Minutes / AccumulateTime_minute;
                                int current_time_priority = (DateTime.Now - waitting_excute_mcs_cmd.CMD_INSER_TIME).Minutes * AccumulateTime_minute;
                                if (current_time_priority != waitting_excute_mcs_cmd.TIME_PRIORITY)
                                {
                                    int change_priority = current_time_priority - waitting_excute_mcs_cmd.TIME_PRIORITY;
                                    updateCMD_MCS_TimePriority(waitting_excute_mcs_cmd, current_time_priority);
                                    if (waitting_excute_mcs_cmd.PRIORITY_SUM < CMDBLL.CMD_MCS_TOP_PRIORITY)
                                        updateCMD_MCS_PrioritySUM(waitting_excute_mcs_cmd, waitting_excute_mcs_cmd.PRIORITY_SUM + change_priority);
                                }
                                continue;
                            }


                            List<AMCSREPORTQUEUE> reportqueues = null;
                            using (TransactionScope tx = SCUtility.getTransactionScope())
                            {
                                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                                {

                                    bool isSuccess = true;
                                    //int total_priority = waitting_excute_mcs_cmd.PRIORITY + waitting_excute_mcs_cmd.TIME_PRIORITY + waitting_excute_mcs_cmd.PORT_PRIORITY;
                                    isSuccess &= scApp.CMDBLL.doCreatTransferCommand(vehicleId, waitting_excute_mcs_cmd.CMD_ID, waitting_excute_mcs_cmd.CARRIER_ID,
                                                       cmd_type,
                                                       from_adr,
                                                       to_adr, waitting_excute_mcs_cmd.PRIORITY_SUM, 0);
                                    //在找到車子後先把它改成PreInitial，防止Timer再找到該筆命令
                                    if (isSuccess)
                                    {
                                        //isSuccess &= scApp.CMDBLL.updateCMD_MCS_TranStatus2Initial(waitting_excute_mcs_cmd.CMD_ID);
                                        //isSuccess &= scApp.ReportBLL.newReportTransferInitial(waitting_excute_mcs_cmd.CMD_ID, reportqueues);
                                        isSuccess &= scApp.CMDBLL.updateCMD_MCS_TranStatus2PreInitial(waitting_excute_mcs_cmd.CMD_ID);

                                    }
                                    if (isSuccess && !SCUtility.isEmpty(bestSuitableVh.OHTC_CMD))
                                    {
                                        //AVEHICLE VhCatchObj = scApp.getEQObjCacheManager().getVehicletByVHID(bestSuitableVh.VEHICLE_ID);
                                        isSuccess = bestSuitableVh.sned_Str37(bestSuitableVh.OHTC_CMD, CMDCancelType.CmdCancel);
                                        //再命令取消失敗後，要去確認一下目前VH的AVEHICLE Table跟ACMD_OHTC Table是否有發生已無ACMD_OHTC
                                        //但AVEHICLE Table卻還有殘留的資料，
                                        //如果沒有匹配則需要強制更新AVEHICLE Table，使它資料一致
                                        if (!isSuccess)
                                        {
                                            Task.Run(() => scApp.VehicleService.vhCommandExcuteStatusCheck(bestSuitableVh.VEHICLE_ID));
                                        }
                                    }
                                    if (isSuccess)
                                    {
                                        tx.Complete();
                                        break;
                                    }
                                    else
                                    {
                                        //return;
                                        continue;
                                    }
                                }
                                //bool isSuccess = scApp.CMDBLL.creatCommand_OHTC(vehicleId, excute_cmd.CMD_ID, excute_cmd.CARRIER_ID,
                                //                    E_CMD_TYPE.LoadUnload,
                                //                    fromadr,
                                //                    toAdr, 0, 0,
                                //                    out cmd);
                                //if (isSuccess)
                                //{
                                //    updateCMD_MCS_TranStatus2Initial(excute_cmd.CMD_ID);
                                //    //scApp.CMDBLL.updateCMD_MCS_TranStatus2Initial(excute_cmd.CMD_ID);
                                //    Task.Run(() => { scApp.CMDBLL.generateCmd_OHTC_Details(); });
                                //}

                            }
                            checkOHxC_TransferCommand();
                        }
                    }

                    recordQueueCommandInfo(queue_cmd_mcs);

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                finally
                {
                    System.Threading.Interlocked.Exchange(ref syncTranCmdPoint, 0);
                }
            }
        }

        string lastRecordQueueCommandInfo = "";
        private void recordQueueCommandInfo(List<ACMD_MCS> queue_cmd_mcs)
        {
            StringBuilder sb = scApp.stringBuilder.GetObject();
            try
            {
                queue_cmd_mcs.ForEach(cmd => sb.AppendLine(cmd.ToString()));
                string sb_result = sb.ToString();
                if (!SCUtility.isMatche(lastRecordQueueCommandInfo, sb_result))
                {
                    lastRecordQueueCommandInfo = sb_result;
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: "OHx",
                       Data: $"command order:{lastRecordQueueCommandInfo}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
            finally
            {
                scApp.stringBuilder.PutObject(sb);
            }
        }

        private bool IsPauseStateCheckAndChangeToPauseWhenPausing(List<ACMD_MCS> unfinish_mcs_cmd)
        {
            ALINE.TSCState sc_stats = scApp.getEQObjCacheManager().getLine().SCStats;
            if (sc_stats == ALINE.TSCState.PAUSING)
            {
                int transferring_cmd_count = unfinish_mcs_cmd.Where(cmd_mcs => cmd_mcs.TRANSFERSTATE > E_TRAN_STATUS.Queue).Count();
                if (transferring_cmd_count == 0)
                {
                    scApp.LineService.TSCStateToPause("");
                }
                return true;
            }
            else if (sc_stats == ALINE.TSCState.PAUSED)
            {
                return true;
            }
            return false;
        }

        private void refreshACMD_MCSInfoList(List<ACMD_MCS> currentExcuteMCSCmd)
        {
            try
            {
                bool has_change = false;
                List<string> new_current_excute_mcs_cmd = currentExcuteMCSCmd.Select(cmd => SCUtility.Trim(cmd.CMD_ID, true)).ToList();
                List<string> old_current_excute_mcs_cmd = ACMD_MCS.MCS_CMD_InfoList.Keys.ToList();

                List<string> new_add_mcs_cmds = new_current_excute_mcs_cmd.Except(old_current_excute_mcs_cmd).ToList();
                //1.新增多出來的命令
                foreach (string new_cmd in new_add_mcs_cmds)
                {
                    ACMD_MCS new_cmd_obj = new ACMD_MCS();
                    var current_cmd = currentExcuteMCSCmd.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, new_cmd)).FirstOrDefault();
                    if (current_cmd == null) continue;
                    new_cmd_obj.put(current_cmd);
                    ACMD_MCS.MCS_CMD_InfoList.TryAdd(new_cmd, new_cmd_obj);
                    has_change = true;
                }
                //2.刪除已經結束的命令
                List<string> will_del_mcs_cmds = old_current_excute_mcs_cmd.Except(new_current_excute_mcs_cmd).ToList();
                foreach (string old_cmd in will_del_mcs_cmds)
                {
                    ACMD_MCS.MCS_CMD_InfoList.TryRemove(old_cmd, out ACMD_MCS cmd_mcs);
                    has_change = true;
                }
                //3.更新現有命令
                foreach (var mcs_cmd_item in ACMD_MCS.MCS_CMD_InfoList)
                {
                    string cmd_mcs_id = mcs_cmd_item.Key;
                    ACMD_MCS cmd_mcs = currentExcuteMCSCmd.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, cmd_mcs_id)).FirstOrDefault();
                    if (cmd_mcs == null)
                    {
                        continue;
                    }
                    if (mcs_cmd_item.Value.put(cmd_mcs))
                    {
                        has_change = true;
                    }
                    if (mcs_cmd_item.Value.TRANSFERSTATE != E_TRAN_STATUS.Queue &&
                       !SCUtility.isEmpty(mcs_cmd_item.Value.CanNotServiceReason))
                    {
                        mcs_cmd_item.Value.CanNotServiceReason = string.Empty;
                        has_change = true;
                    }
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Exception");
            }
        }
        private void SetTransferCommandNGReason(string cmdID, string reason)
        {
            bool is_exist = ACMD_MCS.MCS_CMD_InfoList.TryGetValue(SCUtility.Trim(cmdID), out var cmd_mcs_obj);
            if (is_exist)
            {
                cmd_mcs_obj.setCanNotServiceReason(reason);
            }
        }

        private (bool isEnough, string reason) IsPortGroupCarryingCapacityEnough(ACMD_MCS waitting_excute_mcs_cmd, List<ACMD_MCS> excute_cmd_mcs)
        {
            var get_watting_excute_cmd_group_id_result = waitting_excute_mcs_cmd.tryGetDestPortGroupID(scApp.PortStationBLL);
            if (!get_watting_excute_cmd_group_id_result.isExist)
                return (true, "");

            string will_excute_cmd_group_id = get_watting_excute_cmd_group_id_result.portGroupID;
            var current_excute_mcs_cmds = excute_cmd_mcs.ToList();
            int current_same_group_excute_cmd_count = current_excute_mcs_cmds.
                                                      Where(cmd => IsDescSamePortGroup(cmd, will_excute_cmd_group_id)).
                                                      Count();

            var get_info_result = scApp.PortStationBLL.OperateCatch.tryGetPortGroupInfo(will_excute_cmd_group_id);
            if (get_info_result.isExist)
            {
                var group_port_info = get_info_result.portGroupInfo;
                if (current_same_group_excute_cmd_count < group_port_info.MAX_COUNT)
                {
                    return (true, "");
                }
                else
                {
                    return (false, $"目前已有{current_same_group_excute_cmd_count}笔命令准备前往Group:{group_port_info.GROUP_ID},等待命令结束...");
                }
            }
            else
            {
                return (true, "");
            }
        }
        private bool IsDescSamePortGroup(ACMD_MCS currentExcuteCmd, string waitExcuteGroupPortID)
        {
            var get_current_excute_cmd_group_id_result = currentExcuteCmd.tryGetDestPortGroupID(scApp.PortStationBLL);
            if (!get_current_excute_cmd_group_id_result.isExist) return false;
            return SCUtility.isMatche(get_current_excute_cmd_group_id_result.portGroupID, waitExcuteGroupPortID);
        }

        public bool assignCommnadToVehicleByCommandShift(string vh_id, ACMD_MCS cmdMCS)
        {
            string cmd_mcs_id = SCUtility.Trim(cmdMCS.CMD_ID, true);
            int rpiority_sum = cmdMCS.PRIORITY_SUM;
            string carrier_id = cmdMCS.CARRIER_ID;
            string hostsource = cmdMCS.HOSTSOURCE;
            string hostdest = cmdMCS.HOSTDESTINATION;
            scApp.MapBLL.getAddressID(hostsource, out string from_adr);
            scApp.MapBLL.getAddressID(hostdest, out string to_adr);

            bool isSuccess = true;
            using (TransactionScope tx = SCUtility.getTransactionScope())
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {

                    isSuccess &= scApp.CMDBLL.doCreatTransferCommand(vh_id, cmd_mcs_id, carrier_id,
                                        E_CMD_TYPE.LoadUnload,
                                        from_adr,
                                        to_adr,
                                        rpiority_sum, 0);
                    //在找到車子後先把它改成PreInitial，防止Timer再找到該筆命令
                    if (isSuccess)
                    {
                        isSuccess &= scApp.CMDBLL.updateCMD_MCS_TranStatus2PreInitial(cmd_mcs_id);
                    }
                    if (isSuccess)
                    {
                        tx.Complete();
                    }
                }
            }
            //如果最後Assign沒有成功的話，就要將他改回queue的狀態
            if (!isSuccess)
                scApp.CMDBLL.updateCMD_MCS_TranStatus2Queue(cmd_mcs_id);
            return isSuccess;
        }
        public bool assignCommnadToVehicleByCommandShiftNew(string vh_id, ACMD_MCS cmdMCS)
        {
            string cmd_mcs_id = SCUtility.Trim(cmdMCS.CMD_ID, true);
            int rpiority_sum = cmdMCS.PRIORITY_SUM;
            string carrier_id = cmdMCS.CARRIER_ID;
            string hostsource = cmdMCS.HOSTSOURCE;
            string hostdest = cmdMCS.HOSTDESTINATION;
            scApp.MapBLL.getAddressID(hostsource, out string from_adr);
            scApp.MapBLL.getAddressID(hostdest, out string to_adr);

            bool isSuccess = true;
            isSuccess = isSuccess && scApp.CMDBLL.doCreatTransferCommand(vh_id, cmd_mcs_id, carrier_id,
                                E_CMD_TYPE.LoadUnload,
                                from_adr,
                                to_adr,
                                rpiority_sum, 0);
            //在找到車子後先把它改成PreInitial，防止Timer再找到該筆命令
            isSuccess = isSuccess && scApp.CMDBLL.updateCMD_MCS_TranStatus2PreInitial(cmd_mcs_id);
            return isSuccess;
        }
        public bool assignCommnadToVehicleForCmdShift(string mcs_id, string vh_id, out string result)
        {
            try
            {
                ACMD_MCS ACMD_MCS = scApp.CMDBLL.getCMD_MCSByID(mcs_id);
                if (ACMD_MCS != null)
                {
                    bool check_result = true;
                    result = "OK";
                    //ACMD_MCS excute_cmd = ACMD_MCSs[0];
                    string hostsource = ACMD_MCS.HOSTSOURCE;
                    string hostdest = ACMD_MCS.HOSTDESTINATION;
                    string from_adr = string.Empty;
                    string to_adr = string.Empty;
                    AVEHICLE vh = null;
                    E_VH_TYPE vh_type = E_VH_TYPE.None;
                    E_CMD_TYPE cmd_type = default(E_CMD_TYPE);

                    //確認 source 是否為Port
                    bool source_is_a_port = scApp.PortStationBLL.OperateCatch.IsExist(hostsource);
                    if (source_is_a_port)
                    {
                        scApp.MapBLL.getAddressID(hostsource, out from_adr, out vh_type);
                        vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                        cmd_type = E_CMD_TYPE.LoadUnload;
                    }
                    else
                    {
                        result = "Source must be a port.";
                        return false;
                    }
                    scApp.MapBLL.getAddressID(hostdest, out to_adr);
                    if (vh != null)
                    {
                        string vehicleId = vh.VEHICLE_ID.Trim();
                        List<AMCSREPORTQUEUE> reportqueues = null;
                        using (TransactionScope tx = SCUtility.getTransactionScope())
                        {
                            using (DBConnection_EF con = DBConnection_EF.GetUContext())
                            {

                                bool isSuccess = true;
                                int total_priority = ACMD_MCS.PRIORITY + ACMD_MCS.TIME_PRIORITY + ACMD_MCS.PORT_PRIORITY;
                                isSuccess &= scApp.CMDBLL.doCreatTransferCommand(vehicleId, ACMD_MCS.CMD_ID, ACMD_MCS.CARRIER_ID,
                                                    cmd_type,
                                                    from_adr,
                                                    to_adr, total_priority, 0);

                                if (isSuccess && !SCUtility.isEmpty(vh.OHTC_CMD))
                                {
                                    AVEHICLE VhCatchObj = scApp.getEQObjCacheManager().getVehicletByVHID(vh.VEHICLE_ID);
                                    isSuccess = vh.sned_Str37(vh.OHTC_CMD, CMDCancelType.CmdCancel);
                                }
                                if (isSuccess)
                                {
                                    tx.Complete();
                                }
                                else
                                {
                                    result = "Assign command to vehicle failed.";
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        result = $"Can not find vehicle:{vh_id}.";
                        return false;
                    }
                    return true;
                }
                else
                {
                    result = $"Can not find command:{mcs_id}.";
                    return false;
                }
            }
            finally
            {
                System.Threading.Interlocked.Exchange(ref syncTranCmdPoint, 0);
            }
        }

        public bool assignCommnadToVehicle(string mcs_id, string vh_id, out string result)
        {
            try
            {
                ACMD_MCS ACMD_MCS = scApp.CMDBLL.getCMD_MCSByID(mcs_id);
                if (ACMD_MCS != null)
                {
                    bool check_result = true;
                    result = "OK";
                    //ACMD_MCS excute_cmd = ACMD_MCSs[0];
                    string hostsource = ACMD_MCS.HOSTSOURCE;
                    string hostdest = ACMD_MCS.HOSTDESTINATION;
                    string from_adr = string.Empty;
                    string to_adr = string.Empty;
                    AVEHICLE vh = null;
                    E_VH_TYPE vh_type = E_VH_TYPE.None;
                    E_CMD_TYPE cmd_type = default(E_CMD_TYPE);

                    //確認 source 是否為Port
                    bool source_is_a_port = scApp.PortStationBLL.OperateCatch.IsExist(hostsource);
                    if (source_is_a_port)
                    {
                        scApp.MapBLL.getAddressID(hostsource, out from_adr, out vh_type);
                        vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                        cmd_type = E_CMD_TYPE.LoadUnload;
                    }
                    else
                    {
                        result = "Source must be a port.";
                        return false;
                    }
                    scApp.MapBLL.getAddressID(hostdest, out to_adr);
                    if (vh != null)
                    {

                        string vehicleId = vh.VEHICLE_ID.Trim();



                        List<AMCSREPORTQUEUE> reportqueues = null;
                        using (TransactionScope tx = SCUtility.getTransactionScope())
                        {
                            using (DBConnection_EF con = DBConnection_EF.GetUContext())
                            {

                                bool isSuccess = true;
                                int total_priority = ACMD_MCS.PRIORITY + ACMD_MCS.TIME_PRIORITY + ACMD_MCS.PORT_PRIORITY;
                                isSuccess &= scApp.CMDBLL.doCreatTransferCommand(vehicleId, ACMD_MCS.CMD_ID, ACMD_MCS.CARRIER_ID,
                                                    cmd_type,
                                                    from_adr,
                                                    to_adr, total_priority, 0);
                                //在找到車子後先把它改成PreInitial，防止Timer再找到該筆命令
                                if (isSuccess)
                                {
                                    //isSuccess &= scApp.CMDBLL.updateCMD_MCS_TranStatus2Initial(waitting_excute_mcs_cmd.CMD_ID);
                                    //isSuccess &= scApp.ReportBLL.newReportTransferInitial(waitting_excute_mcs_cmd.CMD_ID, reportqueues);
                                    isSuccess &= scApp.CMDBLL.updateCMD_MCS_TranStatus2PreInitial(ACMD_MCS.CMD_ID);

                                }
                                if (isSuccess && !SCUtility.isEmpty(vh.OHTC_CMD))
                                {
                                    AVEHICLE VhCatchObj = scApp.getEQObjCacheManager().getVehicletByVHID(vh.VEHICLE_ID);
                                    isSuccess = vh.sned_Str37(vh.OHTC_CMD, CMDCancelType.CmdCancel);
                                }
                                if (isSuccess)
                                {
                                    tx.Complete();
                                }
                                else
                                {
                                    result = "Assign command to vehicle failed.";
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        result = $"Can not find vehicle:{vh_id}.";
                        return false;
                    }
                    return true;
                }
                else
                {
                    result = $"Can not find command:{mcs_id}.";
                    return false;
                }
            }
            finally
            {
                System.Threading.Interlocked.Exchange(ref syncTranCmdPoint, 0);
            }
        }

        public bool commandShift(string mcs_id, string vh_id, out string result)
        {
            //result = "Not implement yet.";
            //return false;
            try
            {
                //1. Cancel命令
                CMDCancelType cnacel_type = default(CMDCancelType);
                cnacel_type = CMDCancelType.CmdCancel;
                bool btemp = scApp.VehicleService.doCancelCommandByMCSCmdIDWithNoReport(mcs_id, cnacel_type, out string ohxc_cmd_id);
                if (btemp)
                {
                    //2. 等命令Cancel完成
                    int loop_time = 20;
                    for (int i = 0; i < loop_time; i++)
                    {
                        ACMD_OHTC cmd = getCMD_OHTCByID(ohxc_cmd_id);
                        if (cmd == null)
                        {
                            result = $"Can not find vehicle command:{ohxc_cmd_id}.";
                            return false;
                        }
                        else if (cmd.CMD_STAUS == E_CMD_STATUS.CancelEndByOHTC)
                        {
                            break;//表示該命令已經被Cancel了，可以進行下一步Assign命令的部分
                        }
                        else if (i == loop_time - 1)
                        {
                            //已到迴圈最後一次，命令仍未被cancel，視為timeout。
                            result = $"Cancel command timeout. Command ID:{ohxc_cmd_id}.";
                            return false;
                        }
                        Thread.Sleep(500);//等500毫秒再跑下一輪
                    }
                    //3. 分派命令給新車(不能報command initial)
                    ACMD_MCS ACMD_MCS = scApp.CMDBLL.getCMD_MCSByID(mcs_id);
                    if (ACMD_MCS != null)
                    {
                        assignCommnadToVehicleForCmdShift(mcs_id, vh_id, out result);
                        if (result == "OK")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        result = $"Can not find command:{mcs_id}.";
                        return false;
                    }
                }
                else
                {
                    result = $"Transfer command:[{mcs_id}] cancel failed.";
                    return false;
                }

            }
            finally
            {
                System.Threading.Interlocked.Exchange(ref syncTranCmdPoint, 0);
            }
        }


        public List<TranTask> loadTranTasks()
        {
            return testTranTaskDao.loadTransferTasks_ACycle(scApp.PortStationBLL, scApp.TranCmdPeriodicDataSet.Tables[0]);
        }

        public Dictionary<int, List<TranTask>> loadTranTaskSchedule_24Hour()
        {
            List<TranTask> allTranTaskType = testTranTaskDao.loadTransferTasks_24Hour(scApp.TranCmdPeriodicDataSet.Tables[1]);
            Dictionary<int, List<TranTask>> dicTranTaskSchedule = new Dictionary<int, List<TranTask>>();
            var query = from tranTask in allTranTaskType
                        group tranTask by tranTask.Min;

            dicTranTaskSchedule = query.OrderBy(item => item.Key).ToDictionary(item => item.Key, item => item.ToList());

            return dicTranTaskSchedule;
        }
        public Dictionary<string, List<TranTask>> loadTranTaskSchedule_Clear_Dirty()
        {
            List<TranTask> allTranTaskType = testTranTaskDao.loadTransferTasks_24Hour(scApp.TranCmdPeriodicDataSet.Tables[1]);
            Dictionary<string, List<TranTask>> dicTranTaskSchedule = new Dictionary<string, List<TranTask>>();
            var query = from tranTask in allTranTaskType
                        group tranTask by tranTask.CarType;

            dicTranTaskSchedule = query.OrderBy(item => item.Key).ToDictionary(item => item.Key, item => item.ToList());

            return dicTranTaskSchedule;
        }

        public void ForceExpireTagCmdMCS()
        {
            cmd_mcsDao.ForceExpireTag();
        }


        #endregion CMD_MCS

        #region CMD_OHTC
        public const string CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT = "OHTC_CMD_CHECK_RESULT";
        public class OHTCCommandCheckResult
        {
            public OHTCCommandCheckResult()
            {
                Num = DateTime.Now.ToString(SCAppConstants.TimestampFormat_19);
                IsSuccess = true;
            }
            public string Num { get; private set; }
            public bool IsSuccess = false;
            public StringBuilder Result = new StringBuilder();
            public override string ToString()
            {
                string message = "Alarm No.:" + Num + Environment.NewLine + Environment.NewLine + Result.ToString();
                return message;
            }
        }

        public bool doCreatTransferCommand(string vh_id, string cmd_id_mcs = "", string carrier_id = "", E_CMD_TYPE cmd_type = E_CMD_TYPE.Move,
                                   string source = "", string destination = "", int priority = 0, int estimated_time = 0, SCAppConstants.GenOHxCCommandType gen_cmd_type = SCAppConstants.GenOHxCCommandType.Auto)
        {
            ACMD_OHTC cmd_obj = null;
            return doCreatTransferCommand(vh_id, out cmd_obj, cmd_id_mcs, carrier_id, cmd_type,
                                    source, destination, priority, estimated_time,
                                    gen_cmd_type);
        }
        public bool doCreatTransferCommand(string vh_id, out ACMD_OHTC cmd_obj, string cmd_id_mcs = "", string carrier_id = "", E_CMD_TYPE cmd_type = E_CMD_TYPE.Move,
                                       string source = "", string destination = "", int priority = 0, int estimated_time = 0, SCAppConstants.GenOHxCCommandType gen_cmd_type = SCAppConstants.GenOHxCCommandType.Auto)
        {
            OHTCCommandCheckResult check_result = getOrSetCallContext<OHTCCommandCheckResult>(CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT);
            cmd_obj = null;
            //不是MCS Cmd，要檢查檢查有沒有在執行中的，有則不能Creat

            AVEHICLE vh = scApp.getEQObjCacheManager().getVehicletByVHID(vh_id);
            lock (vh.DoCreatTransferCommand_Sync)
            {
                string vh_current_adr = vh.CUR_ADR_ID;
                string vh_current_section = vh.CUR_SEC_ID;
                if (cmd_type == E_CMD_TYPE.MTLHome ||
                   cmd_type == E_CMD_TYPE.MoveToMTL ||
                   cmd_type == E_CMD_TYPE.SystemOut ||
                   cmd_type == E_CMD_TYPE.SystemIn)
                {
                    //not thing...
                }
                else
                {
                    if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, source))
                    {
                        check_result.Result.AppendLine($"vh:{vh_id} want to excute:{cmd_type} ,but source is maintain device range of address:{source}");
                        check_result.Result.AppendLine("");
                        check_result.IsSuccess &= false;
                    }
                    if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, destination))
                    {
                        check_result.Result.AppendLine($"vh:{vh_id} want to excute:{cmd_type} ,but destination is maintain device range of address:{destination}");
                        check_result.Result.AppendLine("");
                        check_result.IsSuccess &= false;
                    }
                    if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfAddress(scApp.SegmentBLL, vh.CUR_ADR_ID))
                    {
                        check_result.Result.AppendLine($"vh:{vh_id} want to excute:{cmd_type} ,but current vh in maintain device range of address:{vh.CUR_ADR_ID}");
                        check_result.Result.AppendLine("");
                        check_result.IsSuccess &= false;
                    }
                    if (scApp.EquipmentBLL.cache.IsInMaintainDeviceRangeOfSection(scApp.SegmentBLL, vh.CUR_SEC_ID))
                    {
                        check_result.Result.AppendLine($"vh:{vh_id} want to excute:{cmd_type} ,but current vh in maintain device range of section:{vh.CUR_SEC_ID}");
                        check_result.Result.AppendLine("");
                        check_result.IsSuccess &= false;
                    }
                }

                if (vh == null)
                {
                    check_result.Result.AppendLine($" please check vh id.");
                    check_result.Result.AppendLine("");
                    check_result.IsSuccess &= false;
                }

                if (!vh.isTcpIpConnect)
                {
                    check_result.Result.AppendLine($" vh:{vh_id} no connection");
                    check_result.Result.AppendLine($" please check IPC.");
                    check_result.Result.AppendLine("");
                    check_result.IsSuccess &= false;
                }

                if (vh.MODE_STATUS == VHModeStatus.Manual || vh.MODE_STATUS == VHModeStatus.None)
                {
                    check_result.Result.AppendLine($" vh:{vh_id} not is auto mode");
                    check_result.Result.AppendLine($" please change to auto mode.");
                    check_result.Result.AppendLine("");
                    check_result.IsSuccess &= false;
                }

                //A0.04 Start
                if (vh.ACT_STATUS != VHActionStatus.NoCommand && vh.MODE_STATUS != VHModeStatus.AutoMts && vh.MODE_STATUS != VHModeStatus.AutoMtl)
                {
                    check_result.Result.AppendLine($" vh:{vh_id} act status is not no command.");
                    check_result.Result.AppendLine($" please wait privious commnad finish.");
                    check_result.Result.AppendLine("");
                    check_result.IsSuccess &= false;
                }
                //A0.04 End

                if (SCUtility.isEmpty(vh_current_adr))
                {
                    check_result.Result.AppendLine($" vh:{vh_id} current address is empty");
                    check_result.Result.AppendLine($" please excute home command.");
                    check_result.Result.AppendLine("");
                    check_result.IsSuccess &= false;
                }
                else
                {
                    string result = "";
                    if (!IsCommandWalkable(vh_id, cmd_type, vh_current_adr, source, destination, out result))
                    {
                        check_result.Result.AppendLine(result);
                        check_result.Result.AppendLine($" please check the segment is enable or has error vh on the way.");
                        check_result.Result.AppendLine("");
                        check_result.IsSuccess &= false;
                    }
                }
                //如果該筆Command是MCS Cmd，只需要檢查有沒有已經在Queue中的，有則不能Creat
                if (!SCUtility.isEmpty(cmd_id_mcs) ||
                    cmd_type == E_CMD_TYPE.Move_MTPort)
                {
                    //if (isCMD_OHTCQueueByVh(vh_id))
                    if (isCMD_OHTCWillSending(vh_id))
                    {
                        check_result.IsSuccess &= false;
                        check_result.Result.AppendLine($" want to creat mcs transfer command:{cmd_id_mcs} of ACMD_OHTC, " +
                                                       $"but vh:{vh_id} has ACMD_OHTC will sending");
                        check_result.Result.AppendLine("");
                    }
                }
                else
                {
                    if (isCMD_OHTCExcuteByVh(vh_id))
                    {
                        check_result.IsSuccess &= false;
                        check_result.Result.AppendLine($" want to creat non mcs transfer command " +
                                                       $"but vh:{vh_id} has ACMD_OHTC in excute");
                        check_result.Result.AppendLine("");
                    }
                }
                if (check_result.IsSuccess)
                {
                    check_result.IsSuccess &= creatCommand_OHTC(vh_id, cmd_id_mcs, carrier_id, cmd_type, source, destination, priority, estimated_time, gen_cmd_type, out cmd_obj);
                    if (!check_result.IsSuccess)
                    {
                        check_result.Result.AppendLine($" vh:{vh_id} creat command to db unsuccess.");
                        check_result.Result.AppendLine("");
                    }
                }
                if (!check_result.IsSuccess)
                {
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(CMDBLL), Device: string.Empty,
                                  Data: check_result.Result.ToString(),
                                  XID: check_result.Num);
                }
                setCallContext(CALL_CONTEXT_KEY_WORD_OHTC_CMD_CHECK_RESULT, check_result);
            }
            return check_result.IsSuccess;
        }




        private bool IsCommandWalkable(string vh_id, E_CMD_TYPE cmd_type, string vh_current_adr, string source, string destination, out string result)
        {
            bool is_walk_able = true;
            switch (cmd_type)
            {
                case E_CMD_TYPE.Move:
                case E_CMD_TYPE.Unload:
                    if (!scApp.RouteGuide.checkRoadIsWalkable(vh_current_adr, destination))
                    {
                        result = $" vh:{vh_id},want excute cmd type:{cmd_type}, current address:[{vh_current_adr}] to destination address:[{destination}] no find path";
                        is_walk_able = false;
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case E_CMD_TYPE.Move_Park:
                    //case E_CMD_TYPE.MoveToMTL:
                    //case E_CMD_TYPE.MTLHome:
                    //case E_CMD_TYPE.SystemIn:
                    //case E_CMD_TYPE.SystemOut:
                    if (!scApp.RouteGuide.checkRoadIsWalkable(vh_current_adr, destination))
                    {
                        result = $" vh:{vh_id},want excute park cmd type:{cmd_type}, current address:[{vh_current_adr}] to destination address:[{destination}] no find path";
                        is_walk_able = false;
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case E_CMD_TYPE.MoveToMTL:
                case E_CMD_TYPE.MTLHome:
                case E_CMD_TYPE.SystemIn:
                case E_CMD_TYPE.SystemOut:
                    if (!scApp.RouteGuide.checkRoadIsWalkable(vh_current_adr, destination, true))
                    {
                        result = $" vh:{vh_id},want excute cmd type:{cmd_type}, current address:[{vh_current_adr}] to destination address:[{destination}] no find path(Maintain device command)";
                        is_walk_able = false;
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case E_CMD_TYPE.Load:
                    if (!scApp.RouteGuide.checkRoadIsWalkable(vh_current_adr, source))
                    {
                        result = $" vh:{vh_id},want excute cmd type:{cmd_type}, current address:[{vh_current_adr}] to destination address:[{source}] no find path";
                        is_walk_able = false;
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                case E_CMD_TYPE.LoadUnload:
                    if (!scApp.RouteGuide.checkRoadIsWalkable(vh_current_adr, source))
                    {
                        result = $" vh:{vh_id},want excute cmd type:{cmd_type}, current address:{vh_current_adr} to source address:{source} no find path";
                        is_walk_able = false;
                    }
                    else if (!scApp.RouteGuide.checkRoadIsWalkable(source, destination))
                    {
                        result = $" vh:{vh_id},want excute cmd type:{cmd_type}, source address:{source} to destination address:{destination} no find path";
                        is_walk_able = false;
                    }
                    else
                    {
                        result = "";
                    }
                    break;
                default:
                    result = $"Incorrect of command type:{cmd_type}";
                    is_walk_able = false;
                    break;
            }

            return is_walk_able;
        }



        public ACMD_OHTC doCreatTransferCommandObj(string vh_id, string cmd_id_mcs, string carrier_id, E_CMD_TYPE cmd_type,
                                    string source, string destination, int priority, int estimated_time, SCAppConstants.GenOHxCCommandType gen_cmd_type)
        {
            if (SCUtility.isEmpty(vh_id))
            {
                return null;
            }
            else if (SCUtility.isEmpty(cmd_id_mcs))
            {

                if (isCMD_OHTCExcuteByVh(vh_id))
                {
                    return null;
                }
            }
            //如果該筆Command是MCS Cmd，只需要檢查有沒有已經在Queue中的，有則不能Creat
            else
            {
                //if (isCMD_OHTCQueueByVh(vh_id))
                if (isCMD_OHTCWillSending(vh_id))
                {
                    return null;
                }
            }

            return buildCommand_OHTC(vh_id, cmd_id_mcs, carrier_id, cmd_type, source, destination, priority, estimated_time, gen_cmd_type, false);
        }



        private bool creatCommand_OHTC(string vh_id, string cmd_id_mcs, string carrier_id, E_CMD_TYPE cmd_type,
                                              string source, string destination, int priority, int estimated_time, SCAppConstants.GenOHxCCommandType gen_cmd_type, out ACMD_OHTC cmd_ohtc)
        {
            cmd_ohtc = buildCommand_OHTC(vh_id, cmd_id_mcs, carrier_id, cmd_type, source, destination, priority, estimated_time, gen_cmd_type);

            return creatCommand_OHTC(cmd_ohtc);
        }



        private bool creatCommand_OHTC(ACMD_OHTC cmd)
        {
            bool isSuccess = true;
            try
            {
                //DBConnection_EF con = DBConnection_EF.GetContext();
                //using (DBConnection_EF con = new DBConnection_EF())
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    //同步修改至台車資料
                    //AVEHICLE vh = scApp.VehicleDao.getByID(con, cmd.VH_ID);
                    //if (vh != null)
                    //    vh.OHTC_CMD = cmd.CMD_ID;
                    cmd_ohtcDAO.add(con, cmd);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                isSuccess = false;
            }
            return isSuccess;
        }

        private ACMD_OHTC buildCommand_OHTC(string vh_id, string cmd_id_mcs, string carrier_id, E_CMD_TYPE cmd_type,
                                            string source, string destination, int priority, int estimated_time,
                                            SCAppConstants.GenOHxCCommandType gen_cmd_type, bool is_generate_cmd_id = true)
        {
            string _source = string.Empty;
            string commandID = string.Empty;
            if (is_generate_cmd_id)
            {
                commandID = scApp.SequenceBLL.getCommandID(gen_cmd_type);
            }
            if (cmd_type == E_CMD_TYPE.LoadUnload
                || cmd_type == E_CMD_TYPE.Load)
            {
                _source = source;
            }
            ACMD_OHTC cmd = new ACMD_OHTC
            {
                CMD_ID = commandID,
                VH_ID = vh_id,
                CARRIER_ID = carrier_id,
                CMD_ID_MCS = cmd_id_mcs,
                CMD_TPYE = cmd_type,
                SOURCE = _source,
                DESTINATION = destination,
                PRIORITY = priority,
                //CMD_START_TIME = DateTime.Now,
                CMD_STAUS = E_CMD_STATUS.Queue,
                CMD_PROGRESS = 0,
                ESTIMATED_TIME = estimated_time,
                ESTIMATED_EXCESS_TIME = estimated_time
            };
            return cmd;
        }
        /// <summary>
        /// 根據Command ID更新OHTC的Command狀態
        /// </summary>
        /// <param name="cmd_id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool updateCommand_OHTC_StatusByCmdID(string cmd_id, E_CMD_STATUS status)
        {
            bool isSuccess = false;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_OHTC cmd = cmd_ohtcDAO.getByID(con, cmd_id);
                if (cmd != null)
                {
                    if (status == E_CMD_STATUS.Execution)
                    {
                        cmd.CMD_START_TIME = DateTime.Now;
                    }
                    else if (status >= E_CMD_STATUS.NormalEnd)
                    {
                        cmd.CMD_END_TIME = DateTime.Now;
                        cmd_ohtc_detailDAO.DeleteByBatch(con, cmd.CMD_ID);
                    }
                    cmd.CMD_STAUS = status;
                    cmd_ohtcDAO.Update(con, cmd);

                    if (status >= E_CMD_STATUS.NormalEnd)
                        scApp.VehicleBLL.updateVehicleExcuteCMD(cmd.VH_ID, string.Empty, string.Empty);

                }
                isSuccess = true;
            }
            ACMD_OHTC.tryUpdateCMD_OHTCStatus(cmd_id, status);
            return isSuccess;
        }
        public bool updateCommand_OHTC_Interrupted(string cmd_id, int interrupFlag)
        {
            bool isSuccess = false;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_OHTC cmd = cmd_ohtcDAO.getByID(con, cmd_id);
                if (cmd != null)
                {
                    cmd.INTERRUPTED_REASON = interrupFlag;
                    cmd_ohtcDAO.Update(con, cmd);
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool DeleteCommand_OHTC_DetailByCmdID(string cmd_id)
        {
            bool isSuccess = false;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_ohtc_detailDAO.DeleteByBatch(con, cmd_id);
            }
            return isSuccess;
        }

        public bool updateCMD_OHxC_Status2ReadyToReWirte(string cmd_id, out ACMD_OHTC cmd_ohtc)
        {
            bool isSuccess = false;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_ohtc = cmd_ohtcDAO.getByID(con, cmd_id);
                //if (cmd != null)
                //{
                //    cmd_ohtc = cmd;
                //    //cmd.CMD_STAUS = E_CMD_STATUS.Queue;
                //    //cmd.CMD_TPYE = E_CMD_TYPE.Override;
                //    //cmd.CMD_TPYE = E_CMD_TYPE.
                //}
                //else
                isSuccess = true;
            }
            return isSuccess;
        }

        public List<string> loadAllCMDID()
        {
            List<string> acmd_ohtcids = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                acmd_ohtcids = cmd_ohtcDAO.loadAllCMDID(con);
            }
            return acmd_ohtcids;
        }
        public List<ACMD_OHTC> loadCMD_OHTCMDStatusIsQueue()
        {
            List<ACMD_OHTC> acmd_ohtcs = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                acmd_ohtcs = cmd_ohtcDAO.loadAllQueue_Auto(con);
            }
            return acmd_ohtcs;
        }
        public List<ACMD_OHTC> loadFinishCMD_OHTC()
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_ohtcDAO.loadFinishCMD_OHT(con);
            }
        }
        public List<ACMD_OHTC> loadUnfinishCMD_OHT()
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_ohtcDAO.loadUnfinishCMD_OHT(con);
            }
        }

        public bool hasCmdOhtcExcute(string vhID)
        {
            int cmd_ohtc_count = 0;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_ohtc_count = cmd_ohtcDAO.getUnfinishCMD_OHT(con, vhID);
            }
            return cmd_ohtc_count > 0;
        }

        public void remoteCMD_OHTCByBatch(List<ACMD_OHTC> cmds)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_ohtcDAO.RemoteByBatch(con, cmds);
            }
        }
        public ACMD_OHTC geExecutedCMD_OHTCByVehicleID(string vh_id)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
            }
            return cmd_ohtc;
        }

        public ACMD_OHTC getCMD_OHTCByStatusSending()
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getCMD_OHTCByStatusSending(con);
            }
            return cmd_ohtc;
        }
        public ACMD_OHTC getCMD_OHTCByVehicleID(string vh_id)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getCMD_OHTCByVehicleID(con, vh_id);
            }
            return cmd_ohtc;
        }
        public ACMD_OHTC getExcuteCMD_OHTCByCmdID(string cmd_id)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getExcuteCMD_OHTCByCmdID(con, cmd_id);
            }
            return cmd_ohtc;
        }
        public ACMD_OHTC getCMD_OHTCByID(string cmdID)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getByID(con, cmdID);
            }
            return cmd_ohtc;
        }
        public ACMD_OHTC getExcuteCMD_OHTCAndNoInterruptedByMCSID(string cmdMcsID)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getExcuteCMD_OHTCAndNoInterruptedByMCSID(con, cmdMcsID);
            }
            return cmd_ohtc;
        }



        public bool isCMD_OHTCQueueByVh(string vh_id)
        {
            int count = 0;

            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                count = cmd_ohtcDAO.getVhQueueCMDConut(con, vh_id);
            }
            return count != 0;
        }
        public bool isCMD_OHTCWillSending(string vhID)
        {
            int count = 0;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                count = cmd_ohtcDAO.getVhWillSendingCMDConut(con, vhID);
            }
            return count != 0;
        }

        public bool checkCanCreatTransferCommand(string vhID)
        {
            try
            {
                List<ACMD_OHTC> cmds_ohtc = null;
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    cmds_ohtc = cmd_ohtcDAO.loadExecuteCmd(con, vhID);
                }
                if (cmds_ohtc.Count == 0)
                {
                    return true;
                }
                if (cmds_ohtc.Count == 1)
                {
                    var cmd_ohtc = cmds_ohtc.FirstOrDefault();
                    if (cmd_ohtc.IsTransferCmdByMCS)
                    {
                        return false;
                    }
                    else
                    {
                        //如果是一般的移動命令在Sending都不允許生成新的
                        if (cmd_ohtc.CMD_STAUS <= E_CMD_STATUS.Sending)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return false;
            }

        }

        public bool isCMD_OHTCExcuteByVh(string vh_id)
        {
            int count = 0;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                count = cmd_ohtcDAO.getVhExcuteCMDConut(con, vh_id);
            }
            return count != 0;
        }
        public bool isCMD_OHTCExcutedByVh(string vh_id)
        {
            int count = 0;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                count = cmd_ohtcDAO.getVhExcutedCMDConut(con, vh_id);
            }
            return count != 0;
        }

        public bool hasExcuteCMDFromToAdrIsParkInSpecifyParkZoneID(string park_zone_id, out int ready_come_to_count)
        {
            ready_come_to_count = 0;
            bool hasCarComeTo = false;
            List<APARKZONEDETAIL> park_zone_detail = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                park_zone_detail = scApp.ParkZoneDetailDao.loadByParkZoneID(con, park_zone_id);
                if (park_zone_detail != null && park_zone_detail.Count > 0)
                {
                    foreach (APARKZONEDETAIL detail in park_zone_detail)
                    {
                        int cmd_ohtc_count = cmd_ohtcDAO.getExecuteByFromAdrIsParkAdr(con, detail.ADR_ID);
                        if (cmd_ohtc_count > 0)
                        {
                            ready_come_to_count++;
                            hasCarComeTo = true;
                            continue;
                        }
                        cmd_ohtc_count = cmd_ohtcDAO.getExecuteByToAdrIsParkAdr(con, detail.ADR_ID);
                        if (cmd_ohtc_count > 0)
                        {
                            ready_come_to_count++;
                            hasCarComeTo = true;
                            continue;
                        }
                    }
                }
            }
            return hasCarComeTo;
        }


        public (bool has, ACMD_OHTC cmd_ohtc) hasCMD_OHTCInQueue(string vhID)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getQueueByVhID(con, vhID);
            }
            return (cmd_ohtc != null, cmd_ohtc);

        }

        public bool hasExcuteCMDWantToAdr(string adr_id)
        {
            int count = 0;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                count = cmd_ohtcDAO.getExecuteByToAdr(con, adr_id);
            }
            return count != 0;

        }
        public bool hasExcuteCMDWantToParkAdr(string adr_id)
        {
            int count = 0;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                count = cmd_ohtcDAO.getExecuteByToAdrIsPark(con, adr_id);
            }
            return count != 0;
        }

        public bool forceUpdataCmdStatus2FnishByVhID(string vh_id)
        {
            int count = 0;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                List<ACMD_OHTC> cmds = cmd_ohtcDAO.loadExecuteCmd(con, vh_id);
                if (cmds != null && cmds.Count > 0)
                {
                    foreach (ACMD_OHTC cmd in cmds)
                    {
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: "OHT",
                           Data: $"Fource finish command by op, cmd info:{cmd.ToString()}",
                           VehicleID: vh_id);

                        if (cmd.CMD_STAUS > E_CMD_STATUS.Queue)
                        {
                            updateCommand_OHTC_StatusByCmdID(cmd.CMD_ID, E_CMD_STATUS.AbnormalEndByOHTC);
                            //if (!SCUtility.isEmpty(cmd.CMD_ID_MCS))
                            if (IsMcsCommandAndExcuting(cmd))
                            {
                                scApp.CMDBLL.updateCMD_MCS_TranStatus2Complete(cmd.CMD_ID_MCS, E_TRAN_STATUS.Aborted);
                                scApp.SysExcuteQualityBLL.doCommandFinish(cmd.CMD_ID_MCS, CompleteStatus.CmpStatusForceFinishByOp, E_CMD_STATUS.AbnormalEndByOHTC);
                            }
                        }
                        else
                        {
                            ACMD_OHTC queue_cmd = cmd;
                            updateCommand_OHTC_StatusByCmdID(queue_cmd.CMD_ID, E_CMD_STATUS.AbnormalEndByOHTC);
                            //if (!SCUtility.isEmpty(queue_cmd.CMD_ID_MCS))
                            if (IsMcsCommandAndExcuting(queue_cmd))
                            {
                                ACMD_MCS pre_initial_cmd_mcs = getCMD_MCSByID(queue_cmd.CMD_ID_MCS);
                                if (pre_initial_cmd_mcs != null &&
                                    pre_initial_cmd_mcs.TRANSFERSTATE == E_TRAN_STATUS.PreInitial)
                                {
                                    scApp.CMDBLL.updateCMD_MCS_TranStatus2Queue(pre_initial_cmd_mcs.CMD_ID);
                                }
                            }
                        }
                    }
                    cmd_ohtcDAO.Update(con, cmds);
                }
            }
            return count != 0;
        }
        public bool IsMcsCommandAndExcuting(ACMD_OHTC cmdOHTC)
        {
            //如果原本就不是在執行MCS命令，就直接回false
            if (SCUtility.isEmpty(cmdOHTC.CMD_ID_MCS))
            {
                return false;
            }
            else
            {
                //如果是在執行MCS命令則需要看是否該命令被中斷了
                if (cmdOHTC.INTERRUPTED_REASON.HasValue &&
                    (cmdOHTC.INTERRUPTED_REASON.Value == ACMD_OHTC.COMMAND_INTERRUPTED_CANCELLING_FLAG || cmdOHTC.INTERRUPTED_REASON.Value == ACMD_OHTC.COMMAND_INTERRUPTED_COMPLETE_FLAG))
                {
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: "OHxC",
                       Data: $"vh id:{cmdOHTC.VH_ID} 正在執行cmd_mcs:{cmdOHTC.CMD_ID_MCS}，但該命令:{cmdOHTC.CMD_ID} 目前的interrupted valus:{cmdOHTC.INTERRUPTED_REASON.Value}，代表已被中斷",
                       VehicleID: cmdOHTC.VH_ID);
                    return false;
                }
                return true;
            }
        }
        public bool isCMCD_OHTCFinish(string cmdID)
        {
            ACMD_OHTC cmd_ohtc = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtc = cmd_ohtcDAO.getByID(con, cmdID);
            }
            return cmd_ohtc != null &&
                   cmd_ohtc.CMD_STAUS >= E_CMD_STATUS.NormalEnd;
        }
        public bool isCMCD_OHTCFinishFromCache(string cmdID)
        {
            List<ACMD_OHTC> cmd_ohtcs = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtcs = cmd_ohtcDAO.loadUnfinishCMD_OHT(con);
            }
            ACMD_OHTC cmd_ohtc = cmd_ohtcs.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, cmdID)).FirstOrDefault();
            return cmd_ohtc == null ||
                   cmd_ohtc.CMD_STAUS >= E_CMD_STATUS.NormalEnd;
        }
        public ACMD_OHTC getExcuteCMD_OHTCByMCSID(string cmdMCSID)
        {
            List<ACMD_OHTC> cmd_ohtcs = null;
            using (DBConnection_EF con = new DBConnection_EF())
            {
                cmd_ohtcs = cmd_ohtcDAO.loadUnfinishCMD_OHT(con);
            }
            if (cmd_ohtcs == null) return null;

            var cmd_ohtc = cmd_ohtcs.
                           Where(cmd => SCUtility.isMatche(cmd.CMD_ID_MCS, cmdMCSID) && cmd.INTERRUPTED_REASON == null).
                           FirstOrDefault();
            return cmd_ohtc;
        }

        //public bool FourceResetVhCmd()
        //{
        //    int count = 0;
        //    using (DBConnection_EF con = new DBConnection_EF())
        //    {
        //        count = cmd_ohtcDAO.getExecuteByToAdrIsPark(con, adr_id);
        //    }
        //    return count != 0;

        //}

        private long ohxc_cmd_SyncPoint = 0;
        public void checkOHxC_TransferCommand()
        {
            LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: string.Empty,
             Data: $"Start check OHxC transfer command begin");
            if (System.Threading.Interlocked.Exchange(ref ohxc_cmd_SyncPoint, 1) == 0)
            {
                try
                {
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: string.Empty,
                    Data: $"check OHxC transfer command into critical zone");

                    if (scApp.getEQObjCacheManager().getLine().ServiceMode
                        != SCAppConstants.AppServiceMode.Active)
                        return;
                    List<ACMD_OHTC> unfinish_ohtc_cmd = scApp.CMDBLL.loadUnfinishCMD_OHT();
                    ALINE line = scApp.getEQObjCacheManager().getLine();
                    line.CurrentExcuteOHTCCommands = unfinish_ohtc_cmd.ToList();
                    refreshACMD_OHTCInfoList(unfinish_ohtc_cmd);
                    //List<ACMD_OHTC> CMD_OHTC_Queues = scApp.CMDBLL.loadCMD_OHTCMDStatusIsQueue();
                    List<ACMD_OHTC> CMD_OHTC_Queues = unfinish_ohtc_cmd.Where(cmd => cmd.CMD_STAUS == E_CMD_STATUS.Queue).ToList();
                    if (CMD_OHTC_Queues == null || CMD_OHTC_Queues.Count == 0)
                        return;
                    foreach (ACMD_OHTC cmd in CMD_OHTC_Queues)
                    {
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: string.Empty,
                           Data: $"Start process ohxc of command ,id:{SCUtility.Trim(cmd.CMD_ID)},vh id:{SCUtility.Trim(cmd.VH_ID)},from:{SCUtility.Trim(cmd.SOURCE)},to:{SCUtility.Trim(cmd.DESTINATION)}");

                        string vehicle_id = cmd.VH_ID.Trim();
                        AVEHICLE assignVH = scApp.VehicleBLL.getVehicleByID(vehicle_id);
                        if (cmd.CMD_TPYE != E_CMD_TYPE.Override)
                        {
                            if (!assignVH.isTcpIpConnect || assignVH.IsError || !SCUtility.isEmpty(assignVH.OHTC_CMD))
                            {
                                LogHelper.Log(logger: logger, LogLevel: LogLevel.Debug, Class: nameof(CMDBLL), Device: string.Empty,
                                   Data: $"can't send command ,id:{SCUtility.Trim(cmd.CMD_ID)},vh id:{SCUtility.Trim(cmd.VH_ID)} current status not allowed." +
                                   $"is connect:{assignVH.isTcpIpConnect},is error:{assignVH.IsError}, current assign ohtc cmd id:{assignVH.OHTC_CMD}.");
                                continue;
                            }

                            bool is_send_success = scApp.VehicleService.doSendOHxCCmdToVh(assignVH, cmd);
                            if (is_send_success)
                            {
                                assignVH.AssignCommandFailTimes = 0;
                            }
                            else
                            {
                                assignVH.AssignCommandFailTimes++;
                            }
                        }
                        //else
                        //{
                        //    if (!assignVH.isTcpIpConnect)
                        //    {
                        //        continue;
                        //    }
                        //    scApp.VehicleService.doSendOHxCOverrideCmdToVh(assignVH, cmd);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(CMDBLL), Device: "OHxC",
                       Data: ex);
                    throw ex;
                }
                finally
                {
                    System.Threading.Interlocked.Exchange(ref ohxc_cmd_SyncPoint, 0);
                }
            }
        }
        private void refreshACMD_OHTCInfoList(List<ACMD_OHTC> currentExcuteCmdOhtc)
        {
            try
            {
                bool has_change = false;
                List<string> new_current_excute_cmd_ohtc = currentExcuteCmdOhtc.Select(cmd => SCUtility.Trim(cmd.CMD_ID, true)).ToList();
                List<string> old_current_excute_cmd_ohtc = ACMD_OHTC.CMD_OHTC_InfoList.Keys.ToList();

                List<string> new_add_cmds_ohtc = new_current_excute_cmd_ohtc.Except(old_current_excute_cmd_ohtc).ToList();
                //1.新增多出來的命令
                foreach (string new_cmd in new_add_cmds_ohtc)
                {
                    ACMD_OHTC new_cmd_obj = new ACMD_OHTC();
                    var current_cmd = currentExcuteCmdOhtc.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, new_cmd)).FirstOrDefault();
                    if (current_cmd == null) continue;
                    new_cmd_obj.put(current_cmd);
                    ACMD_OHTC.CMD_OHTC_InfoList.TryAdd(new_cmd, new_cmd_obj);
                    has_change = true;
                }
                //2.刪除以結束的命令
                List<string> will_del_mcs_cmds = old_current_excute_cmd_ohtc.Except(new_current_excute_cmd_ohtc).ToList();
                foreach (string old_cmd in will_del_mcs_cmds)
                {
                    ACMD_OHTC.CMD_OHTC_InfoList.TryRemove(old_cmd, out ACMD_OHTC cmd_ohtc);
                    has_change = true;
                }
                //3.更新現有命令
                foreach (var cmd_ohtc_item in ACMD_OHTC.CMD_OHTC_InfoList)
                {
                    string cmd_ohtc_id = cmd_ohtc_item.Key;
                    ACMD_OHTC cmd_ohtc = currentExcuteCmdOhtc.Where(cmd => SCUtility.isMatche(cmd.CMD_ID, cmd_ohtc_id)).FirstOrDefault();
                    if (cmd_ohtc == null)
                    {
                        continue;
                    }
                    if (cmd_ohtc_item.Value.put(cmd_ohtc))
                    {
                        has_change = true;
                    }
                }
                if (has_change)
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
        }

        public static T getOrSetCallContext<T>(string key)
        {
            //object obj = System.Runtime.Remoting.Messaging.CallContext.GetData(key);
            //if (obj == null)
            //{
            //    obj = Activator.CreateInstance(typeof(T));
            //    System.Runtime.Remoting.Messaging.CallContext.SetData(key, obj);
            //}
            object obj = Activator.CreateInstance(typeof(T));
            System.Runtime.Remoting.Messaging.CallContext.SetData(key, obj);
            return (T)obj;
        }
        public static T getCallContext<T>(string key)
        {
            object obj = System.Runtime.Remoting.Messaging.CallContext.GetData(key);
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }
        public static void setCallContext<T>(string key, T obj)
        {
            if (obj != null)
            {
                System.Runtime.Remoting.Messaging.CallContext.SetData(key, obj);
            }
        }

        public string GetCmdMCSPauseFlag(string cmdMcsID)
        {
            try
            {
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    return cmd_mcsDao.GetCmdPauseFlag(con, cmdMcsID);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return "";
            }
        }

        public void ForceExpireTagCmdOHTC()
        {
            cmd_ohtcDAO.ForceExpireTag();
        }


        #endregion CMD_OHTC

        #region CMD_OHTC_DETAIL
        public bool tryGenerateCmd_OHTC_Details(ACMD_OHTC acmd_ohtc, out ActiveType active_type, out string[] route_sections, out string[] cycle_run_sections
                                                                                            , out string[] minRouteSeg_Vh2From, out string[] minRouteSeg_From2To)
        {
            active_type = default(ActiveType);
            route_sections = null;
            cycle_run_sections = null;
            minRouteSeg_Vh2From = null;
            minRouteSeg_From2To = null;
            bool is_maintain_command = isMaintainCommand(acmd_ohtc.CMD_TPYE);

            try
            {
                if (acmd_ohtc == null)
                {
                    return false;
                }
                //Equipment eq = scApp.getEQObjCacheManager().getEquipmentByEQPTID(acmd_ohtc.VH_ID);
                //if (eq == null)
                //    return false;
                //lock (eq)
                //{
                //scApp.CMDBLL.updateCommand_OHTC_StatusByCmdID(acmd_ohtc.CMD_ID, E_CMD_STATUS.Sending);
                string Reason = string.Empty;
                SCUtility.TrimAllParameter(acmd_ohtc);
                scApp.VehicleBLL.getAndProcPositionReportFromRedis(acmd_ohtc.VH_ID);
                AVEHICLE vehicle = scApp.VehicleBLL.getVehicleByID(acmd_ohtc.VH_ID);
                SCUtility.TrimAllParameter(vehicle);

                bool routeCheckOk = true;
                List<string> minRouteSeg = new List<string>();
                string[] minRouteSeg_Vh2FromTemp = null;
                string[] minRouteSeg_From2ToTemp = null;


                do
                {
                    string[] ReutrnVh2FromAdr = null;
                    string[] ReutrnFromAdr2ToAdr = null;
                    string source_adr = string.Empty;
                    string destivation_adr = string.Empty;

                    /*這邊要判斷需不需要忽略Segment的狀態，是由於就算該路段被Disable時也要讓他可以Move出來*/
                    //bool isIgnoreSegmentStatus = acmd_ohtc.CMD_TPYE == E_CMD_TYPE.Move;
                    bool isIgnoreSegmentStatus = false;
                    findTransferRoute(acmd_ohtc, vehicle, ref ReutrnVh2FromAdr, ref ReutrnFromAdr2ToAdr, isIgnoreSegmentStatus);


                    //string[] passSegment = null;
                    //if (ReutrnVh2FromAdr != null)
                    //{
                    //    string[] minRoute = ReutrnVh2FromAdr[0].Split('=');
                    //    minRouteSeg_Vh2From = minRoute[0].Split(',');
                    //}


                    //string[] minRoute_Vh2From = ReutrnVh2FromAdr != null ? ReutrnVh2FromAdr[0].Split('=') : null;
                    //string[] minRoute_From2To = ReutrnFromAdr2ToAdr != null ? ReutrnFromAdr2ToAdr[0].Split('=') : null;
                    //minRouteSeg_Vh2From = minRoute_Vh2From != null ? minRoute_Vh2From[0].Split(',') : null;
                    //minRouteSeg_From2To = minRoute_From2To != null ? minRoute_From2To[0].Split(',') : null;

                    if (ReutrnVh2FromAdr != null)
                    {
                        minRouteSeg_Vh2FromTemp = findBestFitRoute(vehicle.CUR_SEC_ID, ReutrnVh2FromAdr, acmd_ohtc.SOURCE, is_maintain_command);
                        filterDuplicateAddress(ref minRouteSeg_Vh2FromTemp);
                    }
                    if (ReutrnFromAdr2ToAdr != null)
                    {
                        minRouteSeg_From2ToTemp = findBestFitRoute(vehicle.CUR_SEC_ID, ReutrnFromAdr2ToAdr, acmd_ohtc.DESTINATION, is_maintain_command);
                        filterDuplicateAddress(ref minRouteSeg_From2ToTemp);
                    }

                    if (minRouteSeg_Vh2FromTemp != null && !SCUtility.isEmpty(minRouteSeg_Vh2FromTemp[0]))
                    {
                        minRouteSeg.AddRange(minRouteSeg_Vh2FromTemp);
                        //tryFilterFirstSection(vehicle, ref minRouteSeg);
                    }
                    if (minRouteSeg_From2ToTemp != null)
                    {
                        minRouteSeg.AddRange(minRouteSeg_From2ToTemp);
                    }
                    else
                    {
                        if (acmd_ohtc.CMD_TPYE == E_CMD_TYPE.Load ||
                            acmd_ohtc.CMD_TPYE == E_CMD_TYPE.Unload ||
                            (acmd_ohtc.CMD_TPYE == E_CMD_TYPE.LoadUnload && SCUtility.isMatche(acmd_ohtc.SOURCE, acmd_ohtc.DESTINATION)))
                        {
                            //notthing...
                        }
                        else
                        {
                            throw new Exception(string.Format("can't find from to of route.cmd id:{0}", acmd_ohtc.CMD_ID));
                        }
                    }
                    if (minRouteSeg != null && minRouteSeg.Count > 0)
                    {
                        tryFilterFirstSection(vehicle, ref minRouteSeg);
                        ASECTION finialSec = scApp.MapBLL.getSectiontByID(minRouteSeg.Last());
                        if (finialSec != null)
                        {
                            if ((acmd_ohtc.CMD_TPYE == E_CMD_TYPE.Load && SCUtility.isMatche(finialSec.TO_ADR_ID, acmd_ohtc.SOURCE)) ||
                                SCUtility.isMatche(finialSec.TO_ADR_ID, acmd_ohtc.DESTINATION))
                            {
                                //Nothing...
                            }
                            else
                            {
                                routeCheckOk = false;
                                minRouteSeg.Clear();
                                int[] section_count = scApp.RouteGuide.getCatchSectionCount();
                                logger_VhRouteLog.Warn(string.Format("section count:{0} ,section index count:{1}",
                                                                    section_count[0],
                                                                    section_count[1]));
                            }
                        }
                        else
                        {
                            int[] section_count = scApp.RouteGuide.getCatchSectionCount();
                            logger_VhRouteLog.Warn(string.Format("section count:{0} ,section index count:{1}",
                                                                section_count[0],
                                                                section_count[1]));
                        }
                    }
                } while (!routeCheckOk);


                //if (creatCmd_OHTC_Details(acmd_ohtc.CMD_ID, minRouteSeg))
                if (creatCmd_OHTC_LoadAndTargetDetailByBatch(acmd_ohtc.CMD_ID, minRouteSeg_Vh2FromTemp, minRouteSeg_From2ToTemp))
                {
                    scApp.CMDBLL.updateCommand_OHTC_StatusByCmdID(acmd_ohtc.CMD_ID, E_CMD_STATUS.Sending);
                }
                //Equipment eqpt = scApp.getEQObjCacheManager().getEquipmentByEQPTID(acmd_ohtc.VH_ID);
                //AVEHICLE eqpt = scApp.getEQObjCacheManager().getVehicletByVHID(acmd_ohtc.VH_ID);
                string cmd_id = acmd_ohtc.CMD_ID;
                string[] cycleRunSecs = null;

                ActiveType activeType = ActiveType.Move;
                activeType = convert_E_CMD_TYPE2ActiveType(acmd_ohtc, activeType);
                if (acmd_ohtc.CMD_TPYE == E_CMD_TYPE.Round)
                {
                    cycleRunSecs = scApp.CycleBLL.loadCycleRunSecsByEntryAdr(acmd_ohtc.DESTINATION);
                    if (cycleRunSecs == null)
                    {
                        throw new Exception(string.Format("cmd id:{0} of command type is {1},but no cycle run section guide"
                                                         , acmd_ohtc.CARRIER_ID
                                                         , acmd_ohtc.CMD_TPYE.ToString()));
                    }
                }

                active_type = activeType;
                if (active_type == ActiveType.Move)
                {
                    if (SCUtility.isMatche(vehicle.CUR_ADR_ID, acmd_ohtc.DESTINATION))
                    {
                        throw new Exception($"vehicle of start adr:{vehicle.CUR_ADR_ID} and dest:{acmd_ohtc.DESTINATION} is same, " +
                                            $"can't generate cmd !");
                    }
                }

                route_sections = minRouteSeg.ToArray();
                cycle_run_sections = cycleRunSecs;
                minRouteSeg_Vh2From = minRouteSeg_Vh2FromTemp;
                minRouteSeg_From2To = minRouteSeg_From2ToTemp;
                return true;
                //}
            }
            catch (VehicleBLL.BlockedByTheErrorVehicleException blockedException)
            {
                //A0.02 updateCommand_OHTC_StatusByCmdID(acmd_ohtc.CMD_ID, E_CMD_STATUS.AbnormalEndByOHTC);
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(CMDBLL), Device: "OHT",
                   Data: blockedException,
                   VehicleID: acmd_ohtc?.VH_ID,
                   CarrierID: acmd_ohtc.CARRIER_ID,
                   Details: $"tryGenerateCmd_OHTC_Details fail.vh id:{acmd_ohtc.VH_ID} ,cmd id:{acmd_ohtc.CMD_ID} " +
                            $",source:{acmd_ohtc.SOURCE} destination:{acmd_ohtc.DESTINATION}");
                return false; //A0.01
                //A0.01throw blockedException;
            }
            catch (Exception ex)
            {
                //A0.02 updateCommand_OHTC_StatusByCmdID(acmd_ohtc.CMD_ID, E_CMD_STATUS.AbnormalEndByOHTC);
                //logger_VhRouteLog.Error(ex, "generateCmd_OHTC_Details happend");
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(CMDBLL), Device: "OHT",
                   Data: ex,
                   VehicleID: acmd_ohtc?.VH_ID,
                   CarrierID: acmd_ohtc.CARRIER_ID,
                   Details: $"tryGenerateCmd_OHTC_Details fail.vh id:{acmd_ohtc.VH_ID} ,cmd id:{acmd_ohtc.CMD_ID} " +
                            $",source:{acmd_ohtc.SOURCE} destination:{acmd_ohtc.DESTINATION}");
                return false;
            }
        }
        //A0.03 start
        private void filterDuplicateAddress(ref string[] RouteSectionArr)
        {
            if (RouteSectionArr != null && RouteSectionArr.Length >= 2)
            {

                List<string> addressList = new List<string>();
                //List<ASECTION> sections = scApp.SectionBLL.cache.GetSections(RouteSectionArr.ToList());
                for (int i = 0; i < RouteSectionArr.Length; i++)
                {
                    ASECTION section = scApp.SectionBLL.cache.GetSection(RouteSectionArr[0]);
                    if (i == 0)
                    {
                        addressList.Add(section.FROM_ADR_ID);
                    }
                    addressList.Add(section.TO_ADR_ID);
                }
                //string fisrtSec = minRouteSeg_Vh2FromTemp[0];
                string fisrtAddr = addressList[0];

                for (int i = 1; i < addressList.Count; i++)
                {
                    if (addressList[i] == fisrtAddr)
                    {
                        string[] tempArr = new string[RouteSectionArr.Length];
                        for (int j = 0; j < RouteSectionArr.Length; j++)
                        {
                            tempArr[j] = RouteSectionArr[j];
                        }
                        RouteSectionArr = new string[tempArr.Length - 1];
                        for (int j = 0; j < RouteSectionArr.Length; j++)
                        {
                            RouteSectionArr[j] = tempArr[j + 1];
                        }
                        break;
                    }
                }
            }

        }
        //A0.03 end
        private bool isMaintainCommand(E_CMD_TYPE cmdType)
        {
            switch (cmdType)
            {
                case E_CMD_TYPE.MoveToMTL:
                case E_CMD_TYPE.MTLHome:
                case E_CMD_TYPE.SystemIn:
                case E_CMD_TYPE.SystemOut:
                    return true;
                default:
                    return false;
            }
        }

        private void tryFilterFirstSection(AVEHICLE vehicle, ref List<string> minRouteSeg)
        {
            if (minRouteSeg == null || minRouteSeg.Count == 0)
            {
                return;
            }
            string first_sec = minRouteSeg[0];
            ASECTION first_sec_obj = scApp.MapBLL.getSectiontByID(first_sec);
            if (first_sec_obj != null &&
                //vehicle.ACC_SEC_DIST == vh_current_section.SEC_DIS)
                SCUtility.isMatche(vehicle.CUR_ADR_ID, first_sec_obj.TO_ADR_ID))
            {
                minRouteSeg.RemoveAt(0);
            }
        }

        public string[] findBestFitRoute(string vh_crt_sec, string[] AllRouteInfo, string targetAdr, bool isMaintainDeviceCommand)
        {
            string[] FitRouteSec = null;
            //try
            //{
            List<string> crtByPassSeg = ByPassSegment.ToList();
            filterByPassSec_VhAlreadyOnSec(vh_crt_sec, crtByPassSeg);
            filterByPassSec_TargetAdrOnSec(targetAdr, crtByPassSeg);
            string[] AllRoute = AllRouteInfo[1].Split(';');
            List<KeyValuePair<string[], double>> routeDetailAndDistance = PaserRoute2SectionsAndDistance(AllRoute);
            //if (scApp.getEQObjCacheManager().getLine().SegmentPreDisableExcuting)
            //{
            //    List<string> nonActiveSeg = scApp.MapBLL.loadNonActiveSegmentNum();
            //filterByPassSec_VhAlreadyOnSec(vh_crt_sec, nonActiveSeg);
            //filterByPassSec_TargetAdrOnSec(targetAdr, nonActiveSeg);

            //判斷是該次的命令是否為Maintain device 的Command，如果不是則不能有要通過該Device所在的Segment
            if (!isMaintainDeviceCommand)
            {
                foreach (var routeDetial in routeDetailAndDistance.ToList())
                {
                    List<string> maintain_device_ids = scApp.EquipmentBLL.cache.GetAllMaintainDeviceSegments();
                    List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                    string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                    bool is_include_maintain_device_segment = secOfSegments.Where(seg => maintain_device_ids.Contains(seg)).Count() != 0;
                    if (is_include_maintain_device_segment)
                    {
                        routeDetailAndDistance.Remove(routeDetial);
                    }
                }
            }
            foreach (var routeDetial in routeDetailAndDistance.ToList())
            {
                List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                if (scApp.getEQObjCacheManager().getLine().SegmentPreDisableExcuting)
                {
                    List<string> nonActiveSeg = scApp.MapBLL.loadNonActiveSegmentNum();
                    string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                    bool isIncludePassSeg = secOfSegments.Where(seg => nonActiveSeg.Contains(seg)).Count() != 0;
                    if (isIncludePassSeg)
                    {
                        routeDetailAndDistance.Remove(routeDetial);
                    }
                }
            }

            //foreach (var routeDetial in routeDetailAndDistance.ToList())
            //{
            //    List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
            //    List<AVEHICLE> vhs = scApp.VehicleBLL.loadAllErrorVehicle();
            //    foreach (AVEHICLE vh in vhs)
            //    {
            //        bool IsErrorVhOnPassSection = lstSec.Where(sec => sec.SEC_ID.Trim() == vh.CUR_SEC_ID.Trim()).Count() > 0;
            //        if (IsErrorVhOnPassSection)
            //        {
            //            routeDetailAndDistance.Remove(routeDetial);
            //            if (routeDetailAndDistance.Count == 0)
            //            {
            //                throw new VehicleBLL.BlockedByTheErrorVehicleException
            //                    ($"Can't find the way to transfer.Because block by error vehicle [{vh.VEHICLE_ID}] on sec [{vh.CUR_SEC_ID}]");
            //            }
            //        }
            //    }
            //}
            //}

            if (routeDetailAndDistance.Count == 0)
            {
                return null;
            }

            foreach (var routeDetial in routeDetailAndDistance)
            {
                List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeDetial.Key.ToList());
                string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
                bool isIncludePassSeg = secOfSegments.Where(seg => crtByPassSeg.Contains(seg)).Count() != 0;
                if (isIncludePassSeg)
                {
                    continue;
                }
                else
                {
                    FitRouteSec = routeDetial.Key;
                    break;
                }
            }
            if (FitRouteSec == null)
            {
                routeDetailAndDistance = routeDetailAndDistance.OrderBy(o => o.Value).ToList();
                FitRouteSec = routeDetailAndDistance.First().Key;
            }
            //}
            //catch (Exception ex)
            //{
            //    logger_VhRouteLog.Error(ex, "Exception");
            //}
            return FitRouteSec;
        }

        //public string[] findBestFitRoute(string vh_crt_sec, string[] AllRouteInfo, string targetAdr)
        //{
        //    string[] FitRouteSec = null;
        //    try
        //    {
        //        List<string> crtByPassSeg = ByPassSegment.ToList();
        //        ASECTION vh_current_sec = scApp.MapBLL.getSectiontByID(vh_crt_sec);
        //        if (vh_current_sec != null)
        //        {
        //            if (crtByPassSeg.Contains(vh_current_sec.SEG_NUM))
        //            {
        //                crtByPassSeg.Remove(vh_current_sec.SEG_NUM);
        //            }
        //        }
        //        List<ASECTION> adrOfSecs = scApp.MapBLL.loadSectionsByFromOrToAdr(targetAdr);
        //        string[] adrSecOfSegments = adrOfSecs.Select(s => s.SEG_NUM).Distinct().ToArray();
        //        if (adrSecOfSegments != null && adrSecOfSegments.Count() > 0)
        //        {
        //            foreach (string seg in adrSecOfSegments)
        //            {
        //                if (crtByPassSeg.Contains(seg))
        //                {
        //                    crtByPassSeg.Remove(seg);
        //                }
        //            }
        //        }

        //        string[] AllRoute = AllRouteInfo[1].Split(';');
        //        foreach (string routeDetial in AllRoute)
        //        {
        //            string route = routeDetial.Split('=')[0];
        //            string[] routeSection = route.Split(',');
        //            List<ASECTION> lstSec = scApp.MapBLL.loadSectionBySecIDs(routeSection.ToList());
        //            //if (passSegment.Contains(lstSec[0].SEG_NUM.Trim()))
        //            //{
        //            //    FitRouteSec = routeSection;
        //            //    break;
        //            //}
        //            string[] secOfSegments = lstSec.Select(s => s.SEG_NUM).Distinct().ToArray();
        //            bool isIncludePassSeg = secOfSegments.Where(seg => crtByPassSeg.Contains(seg)).Count() != 0;
        //            if (isIncludePassSeg)
        //            {
        //                //List<ASECTION> adrOfSecs = scApp.MapBLL.loadSectionsByFromOrToAdr(targetAdr);
        //                //string[] adrSecOfSegments = adrOfSecs.Select(s => s.SEG_NUM).Distinct().ToArray();
        //                //isIncludePassSeg = adrSecOfSegments.Where(seg => crtByPassSeg.Contains(seg)).Count() != 0;
        //                //if (isIncludePassSeg)
        //                //{
        //                //    FitRouteSec = routeSection;
        //                //    break;
        //                //}
        //                //else
        //                //{
        //                continue;
        //                //}
        //            }
        //            else
        //            {
        //                FitRouteSec = routeSection;
        //                break;
        //            }
        //        }
        //        if (FitRouteSec == null)
        //        {
        //            string[] minRoute = AllRouteInfo[0].Split('=');
        //            FitRouteSec = minRoute[0].Split(',');
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger_VhRouteLog.Error(ex, "Exception");
        //        string[] minRoute = AllRouteInfo[0].Split('=');
        //        FitRouteSec = minRoute[0].Split(',');
        //    }
        //    return FitRouteSec;
        //}

        private void filterByPassSec_TargetAdrOnSec(string targetAdr, List<string> crtByPassSeg)
        {
            if (SCUtility.isEmpty(targetAdr)) return;
            List<ASECTION> adrOfSecs = scApp.MapBLL.loadSectionsByFromOrToAdr(targetAdr);
            string[] adrSecOfSegments = adrOfSecs.Select(s => s.SEG_NUM).Distinct().ToArray();
            if (adrSecOfSegments != null && adrSecOfSegments.Count() > 0)
            {
                foreach (string seg in adrSecOfSegments)
                {
                    if (crtByPassSeg.Contains(seg))
                    {
                        crtByPassSeg.Remove(seg);
                    }
                }
            }
        }

        private void filterByPassSec_VhAlreadyOnSec(string vh_crt_sec, List<string> crtByPassSeg)
        {
            ASECTION vh_current_sec = scApp.MapBLL.getSectiontByID(vh_crt_sec);
            if (vh_current_sec != null)
            {
                if (crtByPassSeg.Contains(vh_current_sec.SEG_NUM))
                {
                    crtByPassSeg.Remove(vh_current_sec.SEG_NUM);
                }
            }
        }

        private List<KeyValuePair<string[], double>> PaserRoute2SectionsAndDistance(string[] AllRoute)
        {
            List<KeyValuePair<string[], double>> routeDetailAndDistance = new List<KeyValuePair<string[], double>>();
            foreach (string routeDetial in AllRoute)
            {
                string route = routeDetial.Split('=')[0];
                string[] routeSection = route.Split(',');
                string distance = routeDetial.Split('=')[1];
                double idistance = double.MaxValue;
                if (!double.TryParse(distance, out idistance))
                {
                    logger.Warn($"fun:{nameof(PaserRoute2SectionsAndDistance)},parse distance fail.Route:{route},distance:{distance}");
                }
                routeDetailAndDistance.Add(new KeyValuePair<string[], double>(routeSection, idistance));
            }
            return routeDetailAndDistance;
        }

        public (string[] routeSection, double distance) getShortestRouteSection(string startAdr, string endAdr)
        {
            string[] shortest_route = scApp.RouteGuide.DownstreamSearchSection(startAdr, endAdr, 0);
            return PaserRoute2SectionsAndDistance(shortest_route[0]);
        }


        private (string[] routeSection, double distance) PaserRoute2SectionsAndDistance(string minRouteInfo)
        {
            if (SCUtility.isEmpty(minRouteInfo))
            {
                return (null, double.MaxValue);
            }
            string route = minRouteInfo.Split('=')[0];
            string[] routeSection = route.Split(',');
            string distance = minRouteInfo.Split('=')[1];
            double idistance = double.MaxValue;
            if (!double.TryParse(distance, out idistance))
            {
                logger.Warn($"fun:{nameof(PaserRoute2SectionsAndDistance)},parse distance fail.Route:{route},distance:{distance}");
            }
            return (routeSection, idistance);
        }


        //private static void setVhExcuteCmdToShow(ACMD_OHTC acmd_ohtc, AVEHICLE vehicle, Equipment eqpt, string[] min_route_seq)
        public void setVhExcuteCmdToShow(ACMD_OHTC acmd_ohtc, AVEHICLE vehicle, string[] min_route_seq, string[] cycle_run_sections)
        {
            AVEHICLE _vhCatchObject = scApp.getEQObjCacheManager().getVehicletByVHID(acmd_ohtc.VH_ID);
            _vhCatchObject.MCS_CMD = acmd_ohtc.CMD_ID_MCS;
            _vhCatchObject.OHTC_CMD = acmd_ohtc.CMD_ID;
            _vhCatchObject.startAdr = vehicle.CUR_ADR_ID;
            _vhCatchObject.FromAdr = acmd_ohtc.SOURCE;
            _vhCatchObject.ToAdr = acmd_ohtc.DESTINATION;
            _vhCatchObject.CMD_CST_ID = acmd_ohtc.CARRIER_ID;
            _vhCatchObject.CMD_Priority = acmd_ohtc.PRIORITY;
            _vhCatchObject.CmdType = acmd_ohtc.CMD_TPYE;

            _vhCatchObject.PredictPath = min_route_seq;
            var min_route_temp = min_route_seq.Select(route => SCUtility.Trim(route, true));
            //_vhCatchObject.WillPassSectionID = min_route_seq.ToList();
            _vhCatchObject.WillPassSectionID = min_route_temp.ToList();
            _vhCatchObject.CyclingPath = cycle_run_sections;
            _vhCatchObject.vh_CMD_Status = E_CMD_STATUS.Execution;
            _vhCatchObject.Action();
            _vhCatchObject.NotifyVhExcuteCMDStatusChange();
            //_vhCatchObject.VID_Collection.VID_58_CommandID.COMMAND_ID = acmd_ohtc.CMD_ID_MCS;
        }
        public void initialVhExcuteCmdToShow(AVEHICLE vehicle)
        {
            //vehicle.startAdr = null;
            vehicle.FromAdr = null;
            vehicle.ToAdr = null;
            vehicle.CMD_CST_ID = null;
            vehicle.CMD_Priority = 0;
            vehicle.CmdType = E_CMD_TYPE.Home;

            vehicle.PredictPath = null;
            vehicle.WillPassSectionID = null;
            vehicle.CyclingPath = null;
            vehicle.vh_CMD_Status = E_CMD_STATUS.NormalEnd;
            vehicle.NotifyVhExcuteCMDStatusChange();
        }

        private static ActiveType convert_E_CMD_TYPE2ActiveType(ACMD_OHTC acmd_ohtc, ActiveType activeType)
        {
            switch (acmd_ohtc.CMD_TPYE)
            {
                case E_CMD_TYPE.Move:
                case E_CMD_TYPE.Move_Park:
                case E_CMD_TYPE.Move_MTPort:
                    activeType = ActiveType.Move;
                    break;
                case E_CMD_TYPE.MoveToMTL:
                    activeType = ActiveType.Movetomtl;
                    break;
                case E_CMD_TYPE.SystemIn:
                    activeType = ActiveType.Systemin;
                    break;
                case E_CMD_TYPE.SystemOut:
                    activeType = ActiveType.Systemout;
                    break;
                case E_CMD_TYPE.Load:
                    activeType = ActiveType.Load;
                    break;
                case E_CMD_TYPE.Unload:
                    activeType = ActiveType.Unload;
                    break;
                case E_CMD_TYPE.LoadUnload:
                    activeType = ActiveType.Loadunload;
                    break;
                case E_CMD_TYPE.Teaching:
                    activeType = ActiveType.Home;
                    break;
                case E_CMD_TYPE.MTLHome:
                    activeType = ActiveType.Mtlhome;
                    break;
                //case E_CMD_TYPE.Round:
                //    activeType = ActiveType.Round;
                //    break;
                case E_CMD_TYPE.Override:
                    activeType = ActiveType.Override;
                    break;

                default:
                    throw new Exception(string.Format("OHT Command type:{0} , not in the definition"
                                                     , acmd_ohtc.CMD_TPYE.ToString()));
            }
            return activeType;
        }

        private void findTransferRoute_New(ACMD_OHTC acmd_ohtc, AVEHICLE vehicle, ref string[] ReutrnVh2FromAdr, ref string[] ReutrnFromAdr2ToAdr, bool isIgnoreSegStatus)
        {
            AADDRESS from_adr = null;

        }

        private void findTransferRoute(ACMD_OHTC acmd_ohtc, AVEHICLE vehicle, ref string[] ReutrnVh2FromAdr, ref string[] ReutrnFromAdr2ToAdr, bool isIgnoreSegStatus)
        {
            AADDRESS from_adr = null;
            if (SCUtility.isEmpty(acmd_ohtc.SOURCE) ||
                (!SCUtility.isEmpty(acmd_ohtc.SOURCE) && vehicle.HAS_CST == 1))
            {
                from_adr = scApp.MapBLL.getAddressByID(vehicle.CUR_ADR_ID);
            }
            else
            {
                from_adr = scApp.MapBLL.getAddressByID(acmd_ohtc.SOURCE);
            }

            AADDRESS to_adr = scApp.MapBLL.getAddressByID(acmd_ohtc.DESTINATION);
            switch (from_adr.ADRTYPE)
            {
                case E_ADR_TYPE.Address:
                case E_ADR_TYPE.Port:
                    //if (!SCUtility.isEmpty(acmd_ohtc.SOURCE) && !SCUtility.isMatche(acmd_ohtc.SOURCE, vehicle.CUR_ADR_ID))
                    //if (needVh2FromAddressOfGuide(acmd_ohtc, vehicle))
                    if (needVh2FromAddressOfGuideNew(acmd_ohtc, vehicle))
                    //if (!SCUtility.isMatche(acmd_ohtc.SOURCE, vehicle.CUR_ADR_ID) ||
                    //    (!SCUtility.isEmpty(acmd_ohtc.SOURCE) && vehicle.HAS_CST == 0))
                    {
                        //ReutrnVh2FromAdr = scApp.RouteGuide.DownstreamSearchSection_FromSecToAdr
                        //(vehicle.CUR_SEC_ID, from_adr.ADR_ID, 1, isIgnoreSegStatus);
                        if (vehicle.ACC_SEC_DIST == 0)
                        {
                            LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                               Data: $"由於Section dist:{vehicle.ACC_SEC_DIST}，因此在計算路徑時，使用Vehicle current adr 計算:{vehicle.CUR_ADR_ID} _1",
                               VehicleID: vehicle?.VEHICLE_ID,
                               CarrierID: vehicle?.CST_ID);

                            ReutrnVh2FromAdr = tryGetDownstreamSearchSection
                            (vehicle.CUR_ADR_ID, from_adr.ADR_ID, 1, isIgnoreSegStatus);
                        }
                        else
                        {
                            ReutrnVh2FromAdr = tryGetDownstreamSearchSection_FromSecToAdr
                            (vehicle.CUR_SEC_ID, from_adr.ADR_ID, 1, isIgnoreSegStatus);
                        }
                    }
                    if (to_adr != null)
                    {
                        switch (to_adr.ADRTYPE)
                        {
                            case E_ADR_TYPE.Address:
                            case E_ADR_TYPE.Port:
                                if (acmd_ohtc.CMD_TPYE == E_CMD_TYPE.Move)
                                {
                                    //ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection_FromSecToAdr
                                    //(vehicle.CUR_SEC_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                    if (vehicle.ACC_SEC_DIST == 0)
                                    {
                                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                                           Data: $"由於Section dist:{vehicle.ACC_SEC_DIST}，因此在計算路徑時，使用Vehicle current adr 計算:{vehicle.CUR_ADR_ID} _2",
                                           VehicleID: vehicle?.VEHICLE_ID,
                                           CarrierID: vehicle?.CST_ID);

                                        ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection
                                        (vehicle.CUR_ADR_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                    }
                                    else
                                    {
                                        ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection_FromSecToAdr
                                        (vehicle.CUR_SEC_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                    }
                                }
                                else
                                {
                                    if (!SCUtility.isMatche(from_adr.ADR_ID, to_adr.ADR_ID))
                                    {
                                        //ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection
                                        //(from_adr.ADR_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                        ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection
                                        (from_adr.ADR_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                    }
                                    else
                                    {
                                        //如果to adr 剛好是vh的current adr，則需要判斷車子的距離是否為0
                                        //是的話才可以下達原地unload，不然就需要規劃繞一圈的命令
                                        if (SCUtility.isMatche(vehicle.CUR_ADR_ID, to_adr.ADR_ID))
                                        {
                                            if (vehicle.ACC_SEC_DIST == 0)
                                            {
                                                //不用規劃From 到 To的路線
                                            }
                                            else
                                            {
                                                ASECTION vh_on_section_of_obj = scApp.SectionBLL.cache.GetSection(vehicle.CUR_SEC_ID);
                                                if (vh_on_section_of_obj != null && SCUtility.isMatche(vh_on_section_of_obj.TO_ADR_ID, to_adr.ADR_ID))
                                                {
                                                    //不用規劃From 到 To的路線
                                                }
                                                else
                                                {
                                                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                                                       Data: $"由於Section dist:{vehicle.ACC_SEC_DIST}，因此在計算路徑時，使用Vehicle current section 計算:{vehicle.CUR_SEC_ID} _3",
                                                       VehicleID: vehicle?.VEHICLE_ID,
                                                       CarrierID: vehicle?.CST_ID);

                                                    ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection_FromSecToAdr
                                                    (vehicle.CUR_SEC_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                                }
                                            }
                                        }
                                    }
                                }
                                //if (vehicle.HAS_CST == 0)
                                //{
                                //    ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection
                                //        (from_adr.ADR_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                //}
                                //else
                                //{
                                //    ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection_FromSecToAdr
                                //        (vehicle.CUR_SEC_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                //}
                                break;
                            case E_ADR_TYPE.Control:
                                ASECTION to_sec = scApp.MapBLL.getSectiontByID(to_adr.SEC_ID);
                                //ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection
                                //    (from_adr.ADR_ID, to_sec.TO_ADR_ID, 1, isIgnoreSegStatus);
                                ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection
                                    (from_adr.ADR_ID, to_sec.TO_ADR_ID, 1, isIgnoreSegStatus);
                                break;
                        }
                    }
                    break;
                case E_ADR_TYPE.Control:
                    if (!SCUtility.isEmpty(acmd_ohtc.SOURCE) && !SCUtility.isMatche(acmd_ohtc.SOURCE, vehicle.CUR_ADR_ID))
                    //if (!SCUtility.isMatche(acmd_ohtc.SOURCE, vehicle.CUR_ADR_ID) ||
                    //    (!SCUtility.isEmpty(acmd_ohtc.SOURCE) && vehicle.HAS_CST == 0))
                    {
                        //ReutrnVh2FromAdr = scApp.RouteGuide.DownstreamSearchSection_FromSecToSec
                        //    (vehicle.CUR_SEC_ID, from_adr.SEC_ID, 1, false, isIgnoreSegStatus);
                        ReutrnVh2FromAdr = tryGetDownstreamSearchSection_FromSecToSec
                            (vehicle.CUR_SEC_ID, from_adr.SEC_ID, 1, false, isIgnoreSegStatus);
                    }
                    if (to_adr != null)
                    {
                        switch (to_adr.ADRTYPE)
                        {
                            case E_ADR_TYPE.Address:
                            case E_ADR_TYPE.Port:
                                //ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection_FromSecToAdr
                                //    (from_adr.SEC_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection_FromSecToAdr
                                    (from_adr.SEC_ID, to_adr.ADR_ID, 1, isIgnoreSegStatus);
                                break;
                            case E_ADR_TYPE.Control:
                                //ReutrnFromAdr2ToAdr = scApp.RouteGuide.DownstreamSearchSection_FromSecToSec
                                //    (from_adr.SEC_ID, to_adr.SEC_ID, 1, false, isIgnoreSegStatus);
                                ReutrnFromAdr2ToAdr = tryGetDownstreamSearchSection_FromSecToSec
                                    (from_adr.SEC_ID, to_adr.SEC_ID, 1, false, isIgnoreSegStatus);
                                break;
                        }
                    }
                    break;
            }
            string vh_id = vehicle.VEHICLE_ID;
            string vh_crt_adr = vehicle.CUR_ADR_ID;
            string sfrom_adr = from_adr == null ? string.Empty : from_adr.ADR_ID;
            string sto_adr = to_adr == null ? string.Empty : to_adr.ADR_ID;
            string svh2FromAdr = (ReutrnVh2FromAdr != null && ReutrnVh2FromAdr.Count() > 0) ? ReutrnVh2FromAdr[0] : string.Empty;
            string sFromAdr2ToAdr = (ReutrnFromAdr2ToAdr != null && ReutrnFromAdr2ToAdr.Count() > 0) ? ReutrnFromAdr2ToAdr[0] : string.Empty;
            logger_VhRouteLog.Debug(string.Format("Vh ID [{0}], vh crt adr[{1}] ,from adr [{2}],to adr [{3}] \r vh2FromAdr sec[{4}]\r FromAdr2ToAdr sec[{5}]"
                , vh_id
                , vh_crt_adr
                , sfrom_adr
                , sto_adr
                , svh2FromAdr
                , sFromAdr2ToAdr));

        }


        private string[] tryGetDownstreamSearchSection_FromSecToAdr
            (string fromSec, string toAdr, int flag, bool isIgnoreStatus = false)
        {
            //List<string> busy_segment = scApp.CMDBLL.cache.loadBusySection();
            //List<string> busy_segment = scApp.CMDBLL.cache.loadUnwalkableSections();
            var unwalkable_sections = scApp.CMDBLL.cache.loadUnwalkableSections();
            string[] route = scApp.RouteGuide.DownstreamSearchSection_FromSecToAdr
                             (fromSec, toAdr, flag, isIgnoreStatus, unwalkable_sections.errorVhAndBusySections);
            if (route == null || route.Count() == 0 || SCUtility.isEmpty(route[0]))
            {
                route = scApp.RouteGuide.DownstreamSearchSection_FromSecToAdr
                             (fromSec, toAdr, flag, isIgnoreStatus, unwalkable_sections.errorVhSections);
            }
            return route;
        }
        private string[] tryGetDownstreamSearchSection
           (string startAdr, string endAdr, int flag, bool isIgnoreStatus = false)
        {
            //List<string> busy_segment = scApp.CMDBLL.cache.loadBusySection();
            //List<string> busy_segment = scApp.CMDBLL.cache.loadUnwalkableSections();
            var unwalkable_sections = scApp.CMDBLL.cache.loadUnwalkableSections();
            string[] route = scApp.RouteGuide.DownstreamSearchSection
                             (startAdr, endAdr, flag, isIgnoreStatus, unwalkable_sections.errorVhAndBusySections);

            if (route == null || route.Count() == 0 || SCUtility.isEmpty(route[0]))
            {
                route = scApp.RouteGuide.DownstreamSearchSection
                             (startAdr, endAdr, flag, isIgnoreStatus, unwalkable_sections.errorVhSections);
            }
            return route;
        }
        private string[] tryGetDownstreamSearchSection_FromSecToSec
           (string fromSec, string toSec, int flag, bool isIncludeLastSec, bool isIgnoreStatus = false)
        {
            //List<string> busy_segment = scApp.CMDBLL.cache.loadBusySection();
            //List<string> busy_segment = scApp.CMDBLL.cache.loadUnwalkableSections();
            var unwalkable_sections = scApp.CMDBLL.cache.loadUnwalkableSections();

            string[] route = scApp.RouteGuide.DownstreamSearchSection_FromSecToSec
                             (fromSec, toSec, flag, false, isIgnoreStatus, unwalkable_sections.errorVhAndBusySections);

            if (route == null || route.Count() == 0 || SCUtility.isEmpty(route[0]))
            {
                route = scApp.RouteGuide.DownstreamSearchSection_FromSecToSec
                             (fromSec, toSec, flag, false, isIgnoreStatus, unwalkable_sections.errorVhSections);
            }
            return route;
        }

        private bool needVh2FromAddressOfGuide(ACMD_OHTC acmd_ohtc, AVEHICLE vehicle)
        {
            bool is_need = true;
            string cmd_source_adr = acmd_ohtc.SOURCE;
            string vh_current_adr = vehicle.CUR_ADR_ID;
            string vh_current_sec = vehicle.CUR_SEC_ID;
            double vh_sec_dist = vehicle.ACC_SEC_DIST;

            //if (SCUtility.isEmpty(acmd_ohtc.SOURCE) ||
            //    (!SCUtility.isEmpty(acmd_ohtc.SOURCE) && vehicle.HAS_CST == 1))

            if (SCUtility.isEmpty(cmd_source_adr)
            || (!SCUtility.isEmpty(cmd_source_adr) && vehicle.HAS_CST == 1))
            {
                is_need = false;
            }

            if (is_need && SCUtility.isMatche(cmd_source_adr, vh_current_adr))
            {
                is_need = false;
                //var last_and_next_sections = scApp.MapBLL.loadSectionsByFromOrToAdr(vh_current_adr);
                //foreach (ASECTION sec in last_and_next_sections)
                //{
                //    //如果車子在該段Section的開頭時，就可以不用給他From到Source的Sec
                //    if (SCUtility.isMatche(sec.FROM_ADR_ID, vh_current_adr))
                //    {
                //        if (SCUtility.isMatche(sec.SEC_ID, vh_current_sec))
                //        {
                //            if (vh_sec_dist == 0)
                //                is_need = false;
                //        }
                //    }
                //}
            }
            return is_need;
        }
        private bool needVh2FromAddressOfGuideNew(ACMD_OHTC acmd_ohtc, AVEHICLE vehicle)
        {
            string cmd_source_adr = acmd_ohtc.SOURCE;
            string vh_current_adr = vehicle.CUR_ADR_ID;
            double vh_sec_dist = vehicle.ACC_SEC_DIST;


            if (SCUtility.isEmpty(cmd_source_adr)
           || (!SCUtility.isEmpty(cmd_source_adr) && vehicle.HAS_CST == 1))
            {
                return false;
            }

            if (SCUtility.isMatche(cmd_source_adr, vh_current_adr))
            {
                if (vh_sec_dist == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        //private bool isVhInSectionStartingPoint(AVEHICLE vh)
        //{

        //}

        public bool creatCmd_OHTC_Details(string cmd_id, List<string> sec_ids)
        {
            bool isSuccess = false;
            ASECTION section = null;
            try
            {
                //List<ASECTION> lstSce = scApp.MapBLL.loadSectionBySecIDs(sec_ids);
                for (int i = 0; i < sec_ids.Count; i++)
                {
                    section = scApp.MapBLL.getSectiontByID(sec_ids[i]);
                    creatCommand_OHTC_Detail(cmd_id, i + 1, section.FROM_ADR_ID, section.SEC_ID, section.SEG_NUM, 0);
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                logger_VhRouteLog.Error(ex, "Exception");
                throw ex;
            }
            return isSuccess;
        }

        public bool creatCommand_OHTC_Detail(string cmd_id, int seq_no, string add_id,
                                      string sec_id, string seg_num, int estimated_time)
        {
            bool isSuccess = false;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_OHTC_DETAIL cmd = new ACMD_OHTC_DETAIL
                {
                    CMD_ID = cmd_id,
                    SEQ_NO = seq_no,
                    ADD_ID = add_id,
                    SEC_ID = sec_id,
                    SEG_NUM = seg_num,
                    ESTIMATED_TIME = estimated_time
                };
                cmd_ohtc_detailDAO.add(con, cmd);
            }
            return isSuccess;
        }

        public bool creatCmd_OHTC_DetailByBatch(string cmd_id, List<string> sec_ids)
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            int start_seq_no = 0;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_OHTC_DETAIL last_cmd_detail = cmd_ohtc_detailDAO.getLastByID(con, cmd_id);
                if (last_cmd_detail != null)
                {
                    start_seq_no = last_cmd_detail.SEQ_NO;
                }
            }
            List<ACMD_OHTC_DETAIL> cmd_details = new List<ACMD_OHTC_DETAIL>();
            foreach (string sec_id in sec_ids)
            {
                ASECTION section = scApp.MapBLL.getSectiontByID(sec_id);
                ACMD_OHTC_DETAIL cmd_detail = new ACMD_OHTC_DETAIL()
                {
                    CMD_ID = cmd_id,
                    SEQ_NO = ++start_seq_no,
                    ADD_ID = section.FROM_ADR_ID,
                    SEC_ID = section.SEC_ID,
                    SEG_NUM = section.SEG_NUM,
                    ESTIMATED_TIME = 0
                };
                cmd_details.Add(cmd_detail);

            }
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_ohtc_detailDAO.AddByBatch(con, cmd_details);
            }
            return true;
        }
        public bool creatCmd_OHTC_LoadAndTargetDetailByBatch(string cmd_id, string[] minRouteSeg_Vh2From, string[] minRouteSeg_From2To)
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            int start_seq_no = 0;
            //using (DBConnection_EF con = DBConnection_EF.GetUContext())
            //{
            //    ACMD_OHTC_DETAIL last_cmd_detail = cmd_ohtc_detailDAO.getLastByID(con, cmd_id);
            //    if (last_cmd_detail != null)
            //    {
            //        start_seq_no = last_cmd_detail.SEQ_NO;
            //    }
            //}
            List<ACMD_OHTC_DETAIL> cmd_details = new List<ACMD_OHTC_DETAIL>();
            if (minRouteSeg_Vh2From != null && minRouteSeg_Vh2From.Length > 0)
            {
                foreach (string sec_id in minRouteSeg_Vh2From)
                {
                    ASECTION section = scApp.MapBLL.getSectiontByID(sec_id);
                    ACMD_OHTC_DETAIL cmd_detail = new ACMD_OHTC_DETAIL()
                    {
                        CMD_ID = cmd_id,
                        SEQ_NO = ++start_seq_no,
                        ADD_ID = section.FROM_ADR_ID,
                        SEC_ID = section.SEC_ID,
                        SEG_NUM = section.SEG_NUM,
                        ESTIMATED_TIME = 0
                    };
                    cmd_details.Add(cmd_detail);
                }
            }
            start_seq_no = 10000;
            if (minRouteSeg_From2To != null && minRouteSeg_From2To.Length > 0)
            {
                foreach (string sec_id in minRouteSeg_From2To)
                {
                    ASECTION section = scApp.MapBLL.getSectiontByID(sec_id);
                    ACMD_OHTC_DETAIL cmd_detail = new ACMD_OHTC_DETAIL()
                    {
                        CMD_ID = cmd_id,
                        SEQ_NO = ++start_seq_no,
                        ADD_ID = section.FROM_ADR_ID,
                        SEC_ID = section.SEC_ID,
                        SEG_NUM = section.SEG_NUM,
                        ESTIMATED_TIME = 0
                    };
                    cmd_details.Add(cmd_detail);
                }
            }

            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd_ohtc_detailDAO.AddByBatch(con, cmd_details);
            }
            return true;
        }

        public bool update_CMD_DetailEntryTime(string cmd_id,
                                               string add_id,
                                               string sec_id)
        {
            bool isSuccess = false;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                //ACMD_OHTC cmd_oht = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
                ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.
                    getByCmdIDSECIDAndEntryTimeEmpty(con, cmd_id, sec_id);
                if (cmd_detail != null)
                {
                    DateTime nowTime = DateTime.Now;
                    cmd_detail.ADD_ENTRY_TIME = nowTime;
                    cmd_detail.SEC_ENTRY_TIME = nowTime;
                    cmd_ohtc_detailDAO.Update(con, cmd_detail);
                    isSuccess = true;
                }
            }
            return isSuccess;
        }
        public bool update_CMD_DetailLeaveTime(string cmd_id,
                                              string add_id,
                                              string sec_id)
        {
            bool isSuccess = false;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                //ACMD_OHTC cmd_oht = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
                //if (cmd_oht != null)
                //{
                ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.
                    getByCmdIDSECIDAndLeaveTimeEmpty(con, cmd_id, sec_id);
                if (cmd_detail == null)
                {
                    return false;
                }
                DateTime nowTime = DateTime.Now;
                cmd_detail.SEC_LEAVE_TIME = nowTime;

                cmd_ohtc_detailDAO.Update(con, cmd_detail);
                cmd_ohtc_detailDAO.UpdateIsPassFlag(con, cmd_detail.CMD_ID, cmd_detail.SEQ_NO);
                isSuccess = true;
                //}
            }
            return isSuccess;
        }

        public bool update_CMD_Detail_LoadStartTime(string vh_id,
                                                   string add_id,
                                                   string sec_id)
        {
            bool isSuccess = true;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                //ACMD_OHTC cmd_oht = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                if (!SCUtility.isEmpty(vh.OHTC_CMD))
                {
                    //ACMD_OHTC_DETAL cmd_detal = cmd_ohtc_detalDAO.getByCmdIDAndAdrID(con, cmd_oht.CMD_ID, add_id);
                    //ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getByCmdIDAndSecID(con, cmd_oht.CMD_ID, sec_id);
                    ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getByCmdIDAndSecID(con, vh.OHTC_CMD, sec_id);
                    if (cmd_detail == null)
                        return false;
                    DateTime nowTime = DateTime.Now;
                    cmd_detail.LOAD_START_TIME = nowTime;
                    cmd_ohtc_detailDAO.Update(con, cmd_detail);
                }
                else
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }
        public bool update_CMD_Detail_LoadEndTime(string vh_id,
                                         string add_id,
                                         string sec_id)
        {
            bool isSuccess = true;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                //ACMD_OHTC cmd_oht = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                if (!SCUtility.isEmpty(vh.OHTC_CMD))
                {
                    //ACMD_OHTC_DETAL cmd_detal = cmd_ohtc_detalDAO.getByCmdIDAndAdrID(con, cmd_oht.CMD_ID, add_id);
                    //ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getByCmdIDAndSecID(con, cmd_oht.CMD_ID, sec_id);
                    ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getByCmdIDAndSecID(con, vh.OHTC_CMD, sec_id);
                    if (cmd_detail == null)
                        return false;
                    DateTime nowTime = DateTime.Now;
                    cmd_detail.LOAD_END_TIME = nowTime;
                    cmd_ohtc_detailDAO.Update(con, cmd_detail);
                    //}
                }
                else
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }
        public bool update_CMD_Detail_UnloadStartTime(string vh_id,
                                       string add_id,
                                       string sec_id)
        {
            bool isSuccess = true;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                //ACMD_OHTC cmd_oht = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                if (!SCUtility.isEmpty(vh.OHTC_CMD))
                {
                    ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getByCmdIDAndSecID(con, vh.OHTC_CMD, sec_id);
                    if (cmd_detail == null)
                        return false;
                    DateTime nowTime = DateTime.Now;
                    cmd_detail.UNLOAD_START_TIME = nowTime;
                    cmd_ohtc_detailDAO.Update(con, cmd_detail);
                }
                else
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }


        public bool update_CMD_Detail_UnloadEndTime(string vh_id)
        {
            bool isSuccess = true;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                //ACMD_OHTC cmd_oht = cmd_ohtcDAO.getExecuteByVhID(con, vh_id);
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                if (!SCUtility.isEmpty(vh.OHTC_CMD))
                {
                    ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getLastByID(con, vh.OHTC_CMD);
                    if (cmd_detail != null)
                    {
                        cmd_detail.UNLOAD_END_TIME = DateTime.Now;
                        cmd_ohtc_detailDAO.Update(con, cmd_detail);
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
                else
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }

        //public bool update_CMD_Detail_2AbnormalFinsh(string cmd_id, List<string> sec_ids)
        //{
        //    bool isSuccess = false;
        //    using (DBConnection_EF con = DBConnection_EF.GetUContext())
        //    {
        //        foreach (string sec_id in sec_ids)
        //        {
        //            ACMD_OHTC_DETAIL cmd_detail = new ACMD_OHTC_DETAIL();
        //            cmd_detail.CMD_ID = cmd_id;
        //            con.ACMD_OHTC_DETAIL.Attach(cmd_detail);
        //            cmd_detail.SEC_ID = sec_id;
        //            cmd_detail.SEC_ENTRY_TIME = DateTime.MaxValue;
        //            cmd_detail.SEC_LEAVE_TIME = DateTime.MaxValue;
        //            cmd_detail.ADD_ID = "";
        //            cmd_detail.SEG_NUM = "";

        //            //con.Entry(cmd_detail).Property(p => p.CMD_ID).IsModified = true;
        //            //con.Entry(cmd_detail).Property(p => p.SEC_ID).IsModified = true;
        //            con.Entry(cmd_detail).Property(p => p.SEC_ENTRY_TIME).IsModified = true;
        //            con.Entry(cmd_detail).Property(p => p.SEC_LEAVE_TIME).IsModified = true;
        //            con.Entry(cmd_detail).Property(p => p.ADD_ID).IsModified = false;
        //            con.Entry(cmd_detail).Property(p => p.SEG_NUM).IsModified = false;
        //            cmd_ohtc_detailDAO.Update(con, cmd_detail);
        //            con.Entry(cmd_detail).State = EntityState.Detached;
        //        }
        //        isSuccess = true;
        //    }
        //    return isSuccess;
        //}
        public bool update_CMD_Detail_2AbnormalFinsh(string cmd_id, List<string> sec_ids)
        {
            bool isSuccess = false;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                foreach (string sec_id in sec_ids)
                {
                    ACMD_OHTC_DETAIL cmd_detail = cmd_ohtc_detailDAO.getByCmdIDSECIDAndEntryTimeEmpty(con, cmd_id, sec_id);
                    if (cmd_detail != null)
                    {
                        cmd_detail.SEC_ENTRY_TIME = DateTime.MaxValue;
                        cmd_detail.SEC_LEAVE_TIME = DateTime.MaxValue;
                        cmd_detail.IS_PASS = true;

                        cmd_ohtc_detailDAO.Update(con, cmd_detail);
                    }
                }
                isSuccess = true;
            }
            return isSuccess;
        }
        public int getAndUpdateVhCMDProgress(string vh_id, out List<string> willPassSecID)
        {
            int procProgress_percen = 0;
            willPassSecID = null;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                AVEHICLE vh = scApp.VehicleBLL.getVehicleByID(vh_id);
                if (!SCUtility.isEmpty(vh.OHTC_CMD))
                {
                    ACMD_OHTC cmd_oht = cmd_ohtcDAO.getByID(con, vh.OHTC_CMD);
                    if (cmd_oht == null) return 0;
                    double totalDetailCount = 0;
                    double procDetailCount = 0;
                    //List<ACMD_OHTC_DETAIL> lstcmd_detail = cmd_ohtc_detailDAO.loadAllByCmdID(con, cmd_oht.CMD_ID);
                    //totalDetalCount = lstcmd_detail.Count();
                    //procDetalCount = lstcmd_detail.Where(cmd => cmd.ADD_ENTRY_TIME != null).Count();
                    totalDetailCount = cmd_ohtc_detailDAO.getAllDetailCountByCmdID(con, cmd_oht.CMD_ID);
                    procDetailCount = cmd_ohtc_detailDAO.getAllPassDetailCountByCmdID(con, cmd_oht.CMD_ID);
                    willPassSecID = cmd_ohtc_detailDAO.loadAllNonPassDetailSecIDByCmdID(con, cmd_oht.CMD_ID);
                    procProgress_percen = (int)((procDetailCount / totalDetailCount) * 100);
                    cmd_oht.CMD_PROGRESS = procProgress_percen;
                    cmd_ohtcDAO.Update(con, cmd_oht);
                }
            }
            return procProgress_percen;
        }

        public List<ACMD_OHTC_DETAIL> LoadAllCMDDetail()
        {
            List<ACMD_OHTC_DETAIL> cmdDetailList = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmdDetailList = cmd_ohtc_detailDAO.LoadAllDetail(con);
            }
            return cmdDetailList;
        }

        public bool HasCmdWillPassSegment(string segment_num, out List<string> will_pass_cmd_id)
        {
            bool hasCmd = false;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                will_pass_cmd_id = cmd_ohtc_detailDAO.loadAllWillPassDetailCmdID(con, segment_num);
            }
            hasCmd = will_pass_cmd_id != null && will_pass_cmd_id.Count > 0;
            return hasCmd;
        }

        public string[] loadPassSectionByCMDID(string cmd_id)
        {
            string[] sections = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                sections = cmd_ohtc_detailDAO.loadAllSecIDByCmdID(con, cmd_id);
            }
            return sections;
        }

        public (bool isSuccess, string[] PredictPath, string[] StartToFromPath, string[] FromToTargetPath)
            loadPassSectionAll_StartToFromAndFromToTargetByCMDID(string cmd_id)
        {
            List<ACMD_OHTC_DETAIL> details = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                details = cmd_ohtc_detailDAO.loadAllCMD_OHTC_DetailByCmdID(con, cmd_id);
            }
            if (details == null || details.Count == 0)
            {
                return (false, null, null, null);
            }
            else
            {
                var all_path = details.Select(d => SCUtility.Trim(d.SEC_ID, true)).ToArray();
                var start_to_from = details.Where(d => d.SEQ_NO < 10000).Select(d => SCUtility.Trim(d.SEC_ID, true)).ToArray();
                var from_to_target = details.Where(d => d.SEQ_NO >= 10000).Select(d => SCUtility.Trim(d.SEC_ID, true)).ToArray();
                return (true, all_path, start_to_from, from_to_target);
            }
        }

        #endregion CMD_OHTC_DETAIL

        #region Return Code Map
        public ReturnCodeMap getReturnCodeMap(string eq_id, string return_code)
        {
            return return_code_mapDao.getReturnCodeMap(scApp, eq_id, return_code);
        }
        #endregion Return Code Map

        #region HCMD_MCS
        public void CreatHCMD_MCSs(List<HCMD_MCS> HCMD_MCS)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_mcsDao.AddByBatch(con, HCMD_MCS);
            }
        }
        public List<HCMD_MCS> loadHCMD_MCSBefore6Months()
        {
            List<HCMD_MCS> AMCSREPORTQUEUEs;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                AMCSREPORTQUEUEs = hcmd_mcsDao.loadBefore6Months(con);
            }
            return AMCSREPORTQUEUEs;
        }

        public void RemoteHCMD_MCSByBatch(List<HCMD_MCS> hCmdMcs)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_mcsDao.RemoteByBatch(con, hCmdMcs);
            }
        }
        public void RemoteHCMD_MCSBefore6MonthByBatch()
        {
            DateTime date_teime_before_6_month = DateTime.Now.AddMonths(-6);
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_mcsDao.RemoteByBatch(con, date_teime_before_6_month);
            }
        }
        #endregion HCMD_MCS
        #region HCMD_OHTC
        public void CreatHCMD_OHTCs(List<HCMD_OHTC> HCMD_OHTC)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_ohtcDao.AddByBatch(con, HCMD_OHTC);
            }
        }
        public List<HCMD_OHTC> loadHCMD_OHTCBefore6Months()
        {
            List<HCMD_OHTC> hcmd_ohtc;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_ohtc = hcmd_ohtcDao.loadBefore6Months(con);
            }
            return hcmd_ohtc;
        }

        public void RemoteHCMD_OHTCByBatch(List<HCMD_OHTC> hcmdOHTC)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_ohtcDao.RemoteByBatch(con, hcmdOHTC);
            }
        }

        public void RemoteHCMD_OHTCBefore6MonthByBatch()
        {
            DateTime date_teime_before_6_month = DateTime.Now.AddMonths(-6);
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                hcmd_ohtcDao.RemoteByBatch(con, date_teime_before_6_month);
            }
        }





        #endregion HCMD_OHTC


        public class Cache
        {
            SCApplication app;
            public Cache(SCApplication _app)
            {
                app = _app;
            }
            const int CAN_PASS_VH_LIMIT_COUNT = 3;
            private List<string> loadBusySection()
            {
                SegmentBLL segmentBLL = app.SegmentBLL;
                PortStationBLL portStationBLL = app.PortStationBLL;
                SectionBLL sectionBLL = app.SectionBLL;
                List<string> busy_segment = new List<string>();
                if (sc.App.SystemParameter.ChangePathCommandCount == 0) return busy_segment;
                //var current_acmd_mcs = app.getEQObjCacheManager().getLine().CurrentExcuteMCSCommands;
                var current_acmd_mcs = ACMD_MCS.MCS_CMD_InfoList.Values.ToList();

                //1.先找出準備要去Load的 vh 路段
                var will_to_load_mcs_cmd =
                    current_acmd_mcs.Where(cmd => !cmd.IsCVPort_LoadPort() &&
                                                  cmd.COMMANDSTATE >= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_ENROUTE &&
                                                  cmd.COMMANDSTATE < ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE);
                List<string> will_to_segments =
                    will_to_load_mcs_cmd.Select(cmd => cmd.getLoadPortSegment(portStationBLL, sectionBLL)).
                                      ToList();
                //2.找出準備前往unload的
                var will_to_unload_mcs_cmd =
                    current_acmd_mcs.Where(cmd => !cmd.IsCVPort_UnloadPort() &&
                                                  cmd.COMMANDSTATE >= ACMD_MCS.COMMAND_STATUS_BIT_INDEX_LOAD_COMPLETE &&
                                                  cmd.COMMANDSTATE < ACMD_MCS.COMMAND_STATUS_BIT_INDEX_UNLOAD_COMPLETE);
                List<string> will_to_unload_segments =
                    will_to_unload_mcs_cmd.Select(cmd => cmd.getUnloadPortSegment(portStationBLL, sectionBLL)).
                                      ToList();
                will_to_segments.AddRange(will_to_unload_segments);
                var group_cmd_mcs = will_to_segments.GroupBy(cmd => cmd).ToList();

                foreach (var group in group_cmd_mcs.ToList())
                {
                    //if (group.Count() < CAN_PASS_VH_LIMIT_COUNT)
                    if (group.Count() < sc.App.SystemParameter.ChangePathCommandCount)
                    {
                        group_cmd_mcs.Remove(group);
                    }
                }
                busy_segment = group_cmd_mcs.Select(g => g.Key).ToList();
                List<string> busy_section = busy_segment.Select(seg_id =>
                {
                    ASEGMENT seg = segmentBLL.cache.GetSegment(seg_id);
                    if (seg == null) return "";
                    if (seg.Sections == null || seg.Sections.Count == 0) return "";
                    return seg.Sections[0].SEC_ID;
                }).ToList();
                recordCurrentBusySection(busy_section);
                return busy_section;
            }
            private void recordCurrentBusySection(List<string> busySections)
            {
                if (busySections == null || !busySections.Any())
                {
                    return;
                }
                string s_busy_section_ids = string.Join(",", busySections);
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                Data: $"目前busy Section:{s_busy_section_ids}");
            }

            private List<string> loadErrorVhSections()
            {
                var error_vhs = app.VehicleBLL.cache.loadErrorVhs();
                recordCurrentErrorVhAndSection(error_vhs);
                var error_vh_on_sections = error_vhs.Select(v => SCUtility.Trim(v.CUR_SEC_ID, true)).ToList();
                return error_vh_on_sections;
            }

            private void recordCurrentErrorVhAndSection(List<AVEHICLE> error_vhs)
            {
                if (error_vhs == null || !error_vhs.Any())
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                foreach (AVEHICLE errorVh in error_vhs)
                {
                    sb.AppendLine($"error vh:{errorVh.VEHICLE_ID} on section:{errorVh.CUR_SEC_ID}");
                }
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(VehicleService), Device: "OHx",
                Data: $"目前異常車輛與所在Section:{sb.ToString()}");
            }

            public (List<string> errorVhAndBusySections, List<string> errorVhSections) loadUnwalkableSections()
            {
                List<string> error_vh_sections = new List<string>();
                List<string> unwalkable_sections = new List<string>();
                if (DebugParameter.IsOpneChangeGuideSection)
                {
                    var error_vh_on_sections = loadErrorVhSections();
                    if (error_vh_on_sections.Any())
                    {
                        unwalkable_sections.AddRange(error_vh_on_sections);
                        error_vh_sections.AddRange(error_vh_on_sections);
                    }
                }

                var busy_sections = loadBusySection();
                if (busy_sections.Any())
                {
                    unwalkable_sections.AddRange(busy_sections);
                }
                return (unwalkable_sections, error_vh_sections);
            }



            public bool hasCMD_OHTC_Excute(string vhID)
            {
                var current_ohtc_cmds_temp = app.getEQObjCacheManager().getLine().getCurrentExcuteOHTCCommands();
                if (current_ohtc_cmds_temp == null || current_ohtc_cmds_temp.Count == 0)
                {
                    return false;
                }
                int cmd_count = current_ohtc_cmds_temp.Where(cmd => SCUtility.isMatche(cmd.VH_ID, vhID)).Count();
                if (cmd_count > 0) return true;
                return false;
            }

        }

    }



}
