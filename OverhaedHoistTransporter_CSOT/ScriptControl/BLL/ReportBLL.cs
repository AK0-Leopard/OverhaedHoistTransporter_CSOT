﻿// ***********************************************************************
// Assembly         : ScriptControl
// Author           : 
// Created          : 03-31-2016
//
// Last Modified By : 
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="ReportBLL.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data.DAO;
using com.mirle.ibg3k0.sc.Data.VO;
using NLog;
using com.mirle.ibg3k0.sc.Data;
using com.mirle.ibg3k0.stc.Data.SecsData;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.ValueDefMapAction;
using com.mirle.ibg3k0.sc.Data.SECS;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using com.mirle.ibg3k0.sc.Data.SECSDriver;

namespace com.mirle.ibg3k0.sc.BLL
{
    /// <summary>
    /// Class ReportBLL.
    /// </summary>
    public class ReportBLL
    {
        /// <summary>
        /// The sc application
        /// </summary>
        private SCApplication scApp = null;
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The trace set DAO
        /// </summary>
        private TraceSetDao traceSetDao = null;
        private MCSReportQueueDao mcsReportQueueDao = null;
        private DataCollectionDao dataCollectionDao = null;
        private COSTMCSDefaultMapAction cost_mcsDefaultMapAction = null;
        private IBSEMDriver iBSEMDriver = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportBLL"/> class.
        /// </summary>
        public ReportBLL()
        {

        }

        /// <summary>
        /// Starts the specified sc application.
        /// </summary>
        /// <param name="scApp">The sc application.</param>
        public void start(SCApplication scApp)
        {
            this.scApp = scApp;
            traceSetDao = scApp.TraceSetDao;
            mcsReportQueueDao = scApp.MCSReportQueueDao;
            dataCollectionDao = scApp.DataCollectionDao;
        }
        public void startMapAction()
        {
            cost_mcsDefaultMapAction = scApp.getEQObjCacheManager().
                getLine().getMapActionByIdentityKey(typeof(COSTMCSDefaultMapAction).Name) as COSTMCSDefaultMapAction;
            if (cost_mcsDefaultMapAction != null)
            {
                iBSEMDriver = cost_mcsDefaultMapAction;
            }
            else
            {
                iBSEMDriver = new IBSEMDriverEmpty();
            }
        }

        /// <summary>
        /// Updates the trace set.
        /// </summary>
        /// <param name="trace_id">The trace_id.</param>
        /// <param name="smp_period">The smp_period.</param>
        /// <param name="total_smp_cnt">The total_smp_cnt.</param>
        /// <param name="svidList">The svid list.</param>
        public void updateTraceSet(string trace_id, string smp_period, int total_smp_cnt, List<string> svidList)
        {
            ATRACESET traceSet = new ATRACESET()
            {
                TRACE_ID = trace_id,
                SMP_PERIOD = smp_period,
                TOTAL_SMP_CNT = total_smp_cnt,
                TraceItemList = new List<ATRACEITEM>()
            };
            traceSet.calcNextSmpTime();
            List<ATRACEITEM> traceItems = new List<ATRACEITEM>();
            foreach (string svid in svidList)
            {
                ATRACEITEM tItem = new ATRACEITEM();
                tItem.TRACE_ID = trace_id;
                tItem.SVID = svid;
                traceItems.Add(tItem);
            }
            updateTraceSet(traceSet, traceItems);
        }

        /// <summary>
        /// Updates the trace set.
        /// </summary>
        /// <param name="traceSet">The trace set.</param>
        /// <param name="traceItems">The trace items.</param>
        public void updateTraceSet(ATRACESET traceSet, List<ATRACEITEM> traceItems)
        {
            DBConnection_EF conn = null;
            try
            {
                conn = DBConnection_EF.GetContext();
                conn.BeginTransaction();
                ATRACESET sv_traceSet = null;
                sv_traceSet = traceSetDao.getTraceSet(conn, true, traceSet.TRACE_ID);
                if (sv_traceSet != null)
                {
                    sv_traceSet.SMP_PERIOD = traceSet.SMP_PERIOD;
                    sv_traceSet.TOTAL_SMP_CNT = traceSet.TOTAL_SMP_CNT;
                    sv_traceSet.SMP_CNT = 0;            //重新開始
                    sv_traceSet.calcNextSmpTime();
                    traceSetDao.updateTraceSet(conn, sv_traceSet);
                }
                else
                {
                    sv_traceSet = traceSet;
                    sv_traceSet.NX_SMP_TIME = DateTime.Now;
                    sv_traceSet.SMP_TIME = DateTime.Now;
                    traceSetDao.insertTraceSet(conn, traceSet);
                }

                deleteTraceItem(traceSet.TRACE_ID);
                foreach (ATRACEITEM item in traceItems)
                {
                    traceSetDao.insertTraceItem(conn, item);
                }
                conn.Commit();
            }
            catch (Exception ex)
            {
                if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
                logger.Error(ex, "Exception:");
            }
            finally
            {
                if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception:"); } }
            }
        }

        /// <summary>
        /// Deletes the trace item.
        /// </summary>
        /// <param name="trace_id">The trace_id.</param>
        public void deleteTraceItem(string trace_id)
        {
            DBConnection_EF conn = null;
            try
            {
                conn = DBConnection_EF.GetContext();
                conn.BeginTransaction();
                traceSetDao.deleteTraceItem(conn, trace_id);
                conn.Commit();
            }
            catch (Exception ex)
            {
                if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
                logger.Error(ex, "Exception:");
            }
            finally
            {
                if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception:"); } }
            }
        }

        /// <summary>
        /// Updates the trace set.
        /// </summary>
        /// <param name="traceSet">The trace set.</param>
        public void updateTraceSet(ATRACESET traceSet)
        {

            DBConnection_EF conn = null;
            try
            {
                conn = DBConnection_EF.GetContext();
                conn.BeginTransaction();
                traceSetDao.updateTraceSet(conn, traceSet);
                conn.Commit();
            }
            catch (Exception ex)
            {
                if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
                logger.Error(ex, "Exception:");
            }
            finally
            {
                if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception:"); } }
            }
        }

        /// <summary>
        /// Loads the active trace set data.
        /// </summary>
        /// <returns>List&lt;TraceSet&gt;.</returns>
        public List<ATRACESET> loadActiveTraceSetData()
        {
            List<ATRACESET> traceSetList = new List<ATRACESET>();
            DBConnection_EF conn = null;
            try
            {
                conn = DBConnection_EF.GetContext();

                traceSetList = traceSetDao.loadActiveTraceSet(conn);
                foreach (ATRACESET set in traceSetList)
                {
                    string trace_id = set.TRACE_ID;
                    List<ATRACEITEM> itemList = traceSetDao.loadTraceItem(conn, trace_id);
                    set.TraceItemList = itemList;
                }
            }
            catch (Exception ex)
            {
                if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
                logger.Error(ex, "Exception:");
            }
            finally
            {
                if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception:"); } }
            }
            return traceSetList;
        }

        /// <summary>
        /// Loads the report trace set data.
        /// </summary>
        /// <returns>List&lt;TraceSet&gt;.</returns>
        public List<ATRACESET> loadReportTraceSetData()
        {
            List<ATRACESET> traceSetList = new List<ATRACESET>();
            DBConnection_EF conn = null;
            try
            {
                conn = DBConnection_EF.GetContext();

                List<ATRACESET> tmpTraceSetList = traceSetDao.loadActiveTraceSet(conn);
                DateTime reportNowDate = DateTime.Now;
                //
                foreach (ATRACESET traceSet in tmpTraceSetList)
                {
                    if (traceSet.NX_SMP_TIME.CompareTo(reportNowDate) <= 0 && traceSet.SMP_TIME.CompareTo(reportNowDate) < 0)
                    {
                        traceSetList.Add(traceSet);
                    }
                }
                //
                foreach (ATRACESET set in traceSetList)
                {
                    string trace_id = set.TRACE_ID;
                    List<ATRACEITEM> itemList = traceSetDao.loadTraceItem(conn, trace_id);
                    set.TraceItemList = itemList;
                }
            }
            catch (Exception ex)
            {
                if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
                logger.Error(ex, "Exception:");
            }
            finally
            {
                if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception:"); } }
            }
            return traceSetList;
        }

        #region MCS SXFY Report
        public bool AskAreYouThere()
        {
            return iBSEMDriver.S1F1SendAreYouThere();
        }
        public bool AskDateAndTimeRequest()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S2F17SendDateAndTimeRequest();
            return isSuccsess;
        }
        public bool ReportEquiptmentOffLine()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendEquiptmentOffLine();
            return isSuccsess;
        }
        public bool ReportControlStateRemote()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendControlStateRemote();
            return isSuccsess;
        }

        public bool ReportTSCAutoInitiated()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTSCAutoInitiated();
            return isSuccsess;
        }
        public bool ReportTSCPaused()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTSCPaused();
            return isSuccsess;
        }
        public bool ReportTSCAutoCompleted()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTSCAutoCompleted();
            return isSuccsess;
        }
        public bool ReportTSCPauseInitiated(string pausrReason)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTSCPauseInitiated(pausrReason);
            return isSuccsess;
        }
        public bool ReportTSCPauseCompleted()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTSCPauseCompleted();
            return isSuccsess;
        }


        public bool newReportTransferInitial(string cmdID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferInitial(cmdID, reportqueues);
            return isSuccsess;
        }
        public bool newReportBeginTransfer(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleAssigned(vhID, reportqueues);
            //isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferring(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportLoadArrivals(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleArrived(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportLoadComplete(string vhID, BCRReadResult bCRReadResult, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            //  isSuccsess = isSuccsess && iBSEMDriver.S6F11SendCarrierInstalled(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleAcquireCompleted(vhID, reportqueues);
            if (bCRReadResult == BCRReadResult.BcrNormal)
                isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleDeparted(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportCarrierIDReadReport(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendCarrierInstalled(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportCarrierInstalled(string vhID, string carrierID, string transferPort, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendCarrierInstalled(vhID, carrierID, transferPort, reportqueues);
            return isSuccsess;
        }
        public bool newReportCarrierRemoved(string vhID, string carrierID, string transferPort, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendCarrierRemoved(vhID, carrierID, transferPort, reportqueues);
            return isSuccsess;
        }

        public bool newReportUnloadArrivals(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleArrived(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportUnloadComplete(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendCarrierRemoved(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleDepositCompleted(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportLoading(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferring(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleAcquireStarted(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportUnloading(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleDepositStarted(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCommandNormalFinish(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleUnassinged(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCompleted(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCommandNormalFinish(ACMD_MCS CMD_MCS, AVEHICLE vh, string resultCode, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            if (vh != null)
            {
                isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleUnassinged(vh.VEHICLE_ID, reportqueues);
            }
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCompleted(CMD_MCS, vh, resultCode, reportqueues);
            return isSuccsess;
        }

        public bool newReportTransferCommandIDReadErrorFinish(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCompleted(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendCarrierRemoved(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleUnassinged(vhID, reportqueues);
            return isSuccsess;
        }

        public bool newReportTransferCommandCancelFinish(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCancelCompleted(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleUnassinged(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCancelCompleted(ACMD_MCS cmd, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCancelCompleted(cmd, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCancelInitial(string cmdID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCancelInitial(cmdID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCancelInitial(ACMD_MCS cmd, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCancelInitial(cmd, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCancelFailed(string cmdID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferCancelFailed(cmdID, reportqueues);
            return isSuccsess;
        }


        public bool newReportTransferAbortInitial(string cmdID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferAbortInitial(cmdID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferAbortFailed(string cmdID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferAbortFailed(cmdID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCommandAbortFinish(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferAbortCompleted(vhID, reportqueues);
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleUnassinged(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportTransferCommandPaused(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferPaused(vhID, reportqueues);
            return isSuccsess;
        }

        public bool newReportTransferCommandResumed(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendTransferResumed(vhID, reportqueues);
            return isSuccsess;
        }


        public bool newReportAlarmSet()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendAlarmSet();
            return isSuccsess;
        }
        public bool newReportAlarmClear()
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendAlarmCleared();
            return isSuccsess;
        }
        public bool newReportUnitAlarmSet(string vhID, string alarmID, string alarmTest, string vhCurrentPosition, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && (iBSEMDriver as COSTMCSDefaultMapAction).
                S6F11SendUnitAlarmSet(vhID, alarmID, alarmTest, vhCurrentPosition, "Y", reportqueues);
            return isSuccsess;
        }
        public bool newReportUnitAlarmClear(string vhID, string alarmID, string alarmTest, string vhCurrentPosition, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && (iBSEMDriver as COSTMCSDefaultMapAction).
                S6F11SendUnitAlarmCleared(vhID, alarmID, alarmTest, vhCurrentPosition, "Y", reportqueues);
            return isSuccsess;
        }
        public bool newReportLaneInService(string startPoint, string endPoint, string laneCutType, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && (iBSEMDriver as COSTMCSDefaultMapAction).
                S6F11LaneInService(startPoint, endPoint, laneCutType, reportqueues);
            return isSuccsess;
        }
        public bool newReportLaneOutOfService(string startPoint, string endPoint, string laneCutType, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && (iBSEMDriver as COSTMCSDefaultMapAction).
                S6F11LaneOutOfService(startPoint, endPoint, laneCutType, reportqueues);
            return isSuccsess;
        }
        public bool newReportPortInServeice(string portID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.sendS6F11_PortInService(portID, reportqueues);
            return isSuccsess;
        }
        public bool newReportPortOutOfService(string portID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.sendS6F11_PortOutOfService(portID, reportqueues);
            return isSuccsess;
        }

        public bool newReportVehicleInstalled(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleInstalled(vhID, reportqueues);
            return isSuccsess;
        }
        public bool newReportVehicleRemoved(string vhID, List<AMCSREPORTQUEUE> reportqueues)
        {
            bool isSuccsess = true;
            isSuccsess = isSuccsess && iBSEMDriver.S6F11SendVehicleRemoved(vhID, reportqueues);
            return isSuccsess;
        }


        public void newSendMCSMessage(List<AMCSREPORTQUEUE> reportqueues)
        {
            foreach (AMCSREPORTQUEUE queue in reportqueues)
                iBSEMDriver.S6F11SendMessage(queue);
        }

        public bool ReportAlarmHappend(ErrorStatus alarm_status, string error_code, string desc)
        {
            string alcd = SCAppConstants.AlarmStatus.convert2MCS(alarm_status);
            string alid = error_code;
            string altx = desc;
            return iBSEMDriver.S5F1SendAlarmReport(alcd, alid, altx);
        }








        #region 需重構
        private static bool checkNeedToReport(string ceid, VIDCollection vids)
        {
            bool isNeedToReport = true;
            switch (ceid)
            {
                case SECSConst.CEID_Transfer_Initiated:
                case SECSConst.CEID_Vehicle_Assigned:
                case SECSConst.CEID_Transferring:
                case SECSConst.CEID_Vehicle_Unassigned:
                case SECSConst.CEID_Transfer_Completed:
                    if (SCUtility.isEmpty(vids.VID_59_CommandInfo.COMMAND_ID.COMMAND_ID)
                        || SCUtility.isEmpty(vids.VID_58_CommandID.COMMAND_ID))
                    {
                        isNeedToReport = false;
                    }
                    break;
            }
            return isNeedToReport;
        }

        #endregion 需重構

        public AMCSREPORTQUEUE BuildMCSReport(S5F1 sxfy, string vehicle_id)
        {
            byte[] byteArray = SCUtility.ToByteArray(sxfy);
            DateTime reportTime = DateTime.Now;
            AMCSREPORTQUEUE queue = new AMCSREPORTQUEUE()
            {
                SERIALIZED_SXFY = byteArray,
                INTER_TIME = reportTime,
                REPORT_TIME = reportTime,
                VEHICLE_ID = vehicle_id,
                STREAMFUNCTION_NAME = string.Concat(sxfy.StreamFunction, '-', sxfy.StreamFunctionName),
            };
            return queue;
        }
        public AMCSREPORTQUEUE BuildMCSReport(S6F11 sxfy, string cmd_id, string vh_id, string port_id)
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

        public void insertMCSReport(List<AMCSREPORTQUEUE> mcsQueues)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                mcsReportQueueDao.AddByBatch(con, mcsQueues);
            }
        }

        public void insertMCSReport(AMCSREPORTQUEUE mcs_queue)
        {
            //lock (mcs_report_lock_obj)
            //{
            SCUtility.LockWithTimeout(mcs_report_lock_obj, SCAppConstants.LOCK_TIMEOUT_MS,
                () =>
                {
                    //DBConnection_EF con = DBConnection_EF.GetContext();
                    //using (DBConnection_EF con = new DBConnection_EF())
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        mcsReportQueueDao.add(con, mcs_queue);
                    }
                });
            //}
        }
        object mcs_report_lock_obj = new object();
        //public void insertMCSReport(S6F11 sxfy, string cmd_id, string vh_id, string port_id)
        //{
        //    lock (mcs_report_lock_obj)
        //    {
        //        byte[] byteArray = SCUtility.ToByteArray(sxfy);
        //        AMCSREPORTQUEUE queue = new AMCSREPORTQUEUE()
        //        {
        //            SERIALIZED_SXFY = byteArray,
        //            INTER_TIME = DateTime.Now,
        //            STREAMFUNCTION_NAME = sxfy.StreamFunction,
        //            STREAMFUNCTION_CEID = sxfy.CEID,
        //            MCS_CMD_ID = cmd_id,
        //            VEHICLE_ID = vh_id,
        //            PORT_ID = port_id
        //        };
        //        DBConnection_EF con = DBConnection_EF.GetContext();
        //        mcsReportQueueDao.add(con, queue);
        //    }
        //}
        public bool updateMCSReportTime2Empty(AMCSREPORTQUEUE ReportQueue)
        {
            bool isSuccess = false;
            //DBConnection_EF con = DBConnection_EF.GetContext();
            try
            {
                //using (DBConnection_EF con = new DBConnection_EF())
                using (DBConnection_EF con = DBConnection_EF.GetUContext())
                {
                    //con.BeginTransaction();
                    con.AMCSREPORTQUEUE.Attach(ReportQueue);
                    ReportQueue.REPORT_TIME = null;
                    con.Entry(ReportQueue).Property(p => p.REPORT_TIME).IsModified = true;
                    mcsReportQueueDao.Update(con, ReportQueue);
                    con.Entry(ReportQueue).State = System.Data.Entity.EntityState.Detached;

                    //con.Commit();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                //if (con != null) { try { con.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
                logger.Error(ex, "Exception");
                return isSuccess;
            }
            finally
            {
                //if (con != null) { try { con.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
            }
            return isSuccess;
        }


        public bool sendMCSMessage(AMCSREPORTQUEUE mcsMessageQueue)
        {
            return iBSEMDriver.S6F11SendMessage(mcsMessageQueue);
        }


        public List<AMCSREPORTQUEUE> loadNonReportEvent()
        {
            List<AMCSREPORTQUEUE> AMCSREPORTQUEUEs;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                AMCSREPORTQUEUEs = mcsReportQueueDao.loadByNonReport(con);
            }

            return AMCSREPORTQUEUEs;
        }


        public List<AMCSREPORTQUEUE> loadBefore10Min()
        {
            List<AMCSREPORTQUEUE> AMCSREPORTQUEUEs;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                AMCSREPORTQUEUEs = mcsReportQueueDao.loadBefore10Min(con);
            }
            return AMCSREPORTQUEUEs;
        }

        public void RemoteMcsReportQueueByBatch(List<AMCSREPORTQUEUE> mcsRepoetQueue)
        {
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                mcsReportQueueDao.RemoteByBatch(con, mcsRepoetQueue);
            }
        }


        #endregion MCS SXFY Report


        #region Zabbix Report


        public Tuple<string, int> getZabbixServerIPAndPort()
        {
            DataCollectionSetting setting = dataCollectionDao.getDataCollectionFirstItem(scApp);
            string ip = setting.IP;
            var remoteipAdr = System.Net.Dns.GetHostAddresses(setting.IP);
            if (remoteipAdr != null && remoteipAdr.Count() != 0)
            {
                ip = remoteipAdr[0].ToString();
            }
            return new Tuple<string, int>(ip, setting.Port);
        }
        public string getZabbixHostName()
        {
            //DataCollectionSetting setting = dataCollectionDao.getDataCollectionFirstItem(scApp);
            //return setting.Method;
            return SCApplication.ServerName;
        }

        public bool IsReportZabbixInfo(string item_name)
        {
            DataCollectionSetting setting = dataCollectionDao.getDataCollectionItemByMethodAndItemName(scApp, item_name);
            if (setting == null)
                return false;
            return setting.IsReport;
        }
        public void ZabbixPush(string key, int value)
        {
            ZabbixPush(key, value.ToString());
        }
        //[Conditional("Release")]
        public void ZabbixPush(string key, string value)
        {
            try
            {
                string zabbix_host_name = getZabbixHostName();
                if (!IsReportZabbixInfo(key))
                    return;
                //var response1 = scApp.ZabbixService.Send(zabbix_host_name, key, value);
                //if (response1.Failed != 0)
                //{
                //    logger.Error($"Push zabbix fail,key:{key},value:{value},info:{response1.Info},responsel:{response1.Response}");
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
        }
        #endregion Zabbix Report



        #region Mark
        //private bool isSendMCS()
        //{
        //    Dictionary<string, CommuncationInfo> dicCommunactionInfo =
        //      scApp.getEQObjCacheManager().CommonInfo.dicCommunactionInfo;
        //    if (dicCommunactionInfo.ContainsKey("MCS"))
        //    {
        //        return dicCommunactionInfo["MCS"].IsConnectinoSuccess;
        //    }
        //    return false;
        //}
        //public AMCSREPORTQUEUE BulidMCSReportMessage(string ceid, VIDCollection Vids)
        //{
        //    try
        //    {
        //        string ceidOfname = string.Empty;
        //        SECSConst.CEID_Dictionary.TryGetValue(ceid, out ceidOfname);
        //        string ceid_name = $"CEID:[{ceidOfname}({ceid})]";
        //        S6F11 s6f11 = new S6F11()
        //        {
        //            SECSAgentName = scApp.EAPSecsAgentName,
        //            CEID = ceid,
        //            StreamFunctionName = ceid_name
        //        };
        //        List<string> RPTIDs = SECSConst.DicCEIDAndRPTID[ceid];
        //        s6f11.INFO.ITEM = new S6F11.RPTINFO.RPTITEM[RPTIDs.Count];

        //        for (int i = 0; i < RPTIDs.Count; i++)
        //        {
        //            string rpt_id = RPTIDs[i];
        //            s6f11.INFO.ITEM[i] = new S6F11.RPTINFO.RPTITEM();
        //            List<ARPTID> AVIDs = SECSConst.DicRPTIDAndVID[rpt_id];
        //            List<string> VIDs = AVIDs.OrderBy(avid => avid.ORDER_NUM).Select(avid => avid.VID.Trim()).ToList();
        //            s6f11.INFO.ITEM[i].RPTID = rpt_id;
        //            s6f11.INFO.ITEM[i].VIDITEM = new SXFY[AVIDs.Count];
        //            for (int j = 0; j < AVIDs.Count; j++)
        //            {
        //                string vid = VIDs[j];
        //                SXFY vid_item = null;
        //                switch (vid)
        //                {
        //                    case SECSConst.VID_ControlState:
        //                        vid_item = Vids.VID_06_ControlState;
        //                        break;
        //                    case SECSConst.VID_EnhancedCarrierInfo:
        //                        vid_item = Vids.VID_10_EnhancedCarrierInfo;
        //                        break;
        //                    case SECSConst.VID_CommandInfo:
        //                        vid_item = Vids.VID_11_CommandInfo;
        //                        break;
        //                    case SECSConst.VID_EnhancedTransferCmd:
        //                        vid_item = Vids.VID_13_EnhancedTransferCmd;
        //                        break;
        //                    case SECSConst.VID_ActiveVehicles:
        //                        vid_item = Vids.VID_53_ActiveVehicles;
        //                        break;
        //                    case SECSConst.VID_Carrier_ID:
        //                        vid_item = Vids.VID_54_CarrierID;
        //                        break;
        //                    case SECSConst.VID_Carrier_Loc:
        //                        vid_item = Vids.VID_56_CarrierLoc;
        //                        break;
        //                    case SECSConst.VID_Command_ID:
        //                        vid_item = Vids.VID_58_CommandID;
        //                        break;
        //                    case SECSConst.VID_Command_Info:
        //                        vid_item = Vids.VID_59_CommandInfo;
        //                        break;
        //                    case SECSConst.VID_Destination_Port:
        //                        vid_item = Vids.VID_60_DestinationPort;
        //                        break;
        //                    case SECSConst.VID_Priority:
        //                        vid_item = Vids.VID_62_Priotity;
        //                        break;
        //                    case SECSConst.VID_Result_Code:
        //                        vid_item = Vids.VID_64_ResultCode;
        //                        break;
        //                    case SECSConst.VID_Source_Port:
        //                        vid_item = Vids.VID_65_SourcePort;
        //                        break;
        //                    case SECSConst.VID_Carrier_ID_Read:
        //                        vid_item = Vids.VID_67_IDReadStatus;
        //                        break;
        //                    case SECSConst.VID_Vehicle_ID:
        //                        vid_item = Vids.VID_70_VehicleID;
        //                        break;
        //                    case SECSConst.VID_Vehicle_Info:
        //                        vid_item = Vids.VID_71_VehicleInfo;
        //                        break;
        //                    case SECSConst.VID_Vehicle_State:
        //                        vid_item = Vids.VID_72_VehicleStatus;
        //                        break;
        //                    case SECSConst.VID_TranCmpInfo:
        //                        vid_item = Vids.VID_77_TranCmpInfo;
        //                        break;
        //                    case SECSConst.VID_Command_Type:
        //                        vid_item = Vids.VID_80_CommmandType;
        //                        break;
        //                    case SECSConst.VID_Alarm_ID:
        //                        vid_item = Vids.VID_81_AlarmID;
        //                        break;
        //                    case SECSConst.VID_Alarm_Text:
        //                        vid_item = Vids.VID_82_AlarmText;
        //                        break;
        //                    case SECSConst.VID_Unit_ID:
        //                        vid_item = Vids.VID_83_UnitID;
        //                        break;
        //                    case SECSConst.VID_TransferInfo:
        //                        vid_item = Vids.VID_84_TransferInfo;
        //                        break;
        //                    case SECSConst.VID_Spec_Version:
        //                        vid_item = Vids.VID_114_SpecVersion;
        //                        break;
        //                    case SECSConst.VID_Port_ID:
        //                        vid_item = Vids.VID_115_PortID;
        //                        break;
        //                    case SECSConst.VID_Eq_Presence_Status:
        //                        vid_item = Vids.VID_353_EqPresenceStatus;
        //                        break;
        //                    case SECSConst.VID_Port_Info:
        //                        vid_item = Vids.VID_354_PortInfo;
        //                        break;
        //                    case SECSConst.VID_Port_Transfer_State:
        //                        vid_item = Vids.VID_355_PortTransferState;
        //                        break;
        //                    case SECSConst.VID_Unit_Alarm_Info:
        //                        vid_item = Vids.VID_361_UnitAlarmInfo;
        //                        break;
        //                    case SECSConst.VID_Maint_State:
        //                        vid_item = Vids.VID_362_MainState;
        //                        break;
        //                    case SECSConst.VID_Vehicle_Current_Position:
        //                        vid_item = Vids.VID_363_VehicleCurrenyPosition;
        //                        break;
        //                    case SECSConst.VID_TransferState:
        //                        vid_item = Vids.VID_722_TransferState;
        //                        break;
        //                }
        //                s6f11.INFO.ITEM[i].VIDITEM[j] = vid_item;
        //            }
        //        }
        //        return BuildMCSReport
        //        (s6f11,
        //          Vids.VID_58_CommandID.COMMAND_ID
        //        , Vids.VID_70_VehicleID.VEHILCE_ID
        //        , Vids.VID_115_PortID.PORT_ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //        return null;
        //    }
        //}
        //private bool tryCreatReportMCSMessage(string ceid, string eq_id, out AMCSREPORTQUEUE report_queue)
        //{
        //    bool isSuccess = false;
        //    VIDCollection vids = null;
        //    if (scApp.VIDBLL.tryGetVIOInfoVIDCollectionByEQID(eq_id, out vids))
        //    {
        //        bool isNeedToReport = checkNeedToReport(ceid, vids);
        //        if (isNeedToReport)
        //        {
        //            report_queue = mcsDefaultMapAction.S6F11BulibMessage(ceid, vids);
        //            insertMCSReport(report_queue);
        //            isSuccess = true;
        //        }
        //        else
        //            report_queue = null;
        //    }
        //    else
        //    {
        //        report_queue = null;
        //    }
        //    return isSuccess;
        //}
        //public bool ReportTransferInitial(string cmd_id, out List<AMCSREPORTQUEUE> reportqueues)
        //{


        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    reportqueues = new List<AMCSREPORTQUEUE>();
        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn.BeginTransaction();
        //        //using (DBConnection_EF con = new DBConnection_EF())
        //        using (DBConnection_EF con = DBConnection_EF.GetUContext())
        //        {
        //            VIDCollection vids = new VIDCollection();
        //            vids.VID_58_CommandID.COMMAND_ID = cmd_id;
        //            //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Transfer_Initiated, vids);
        //            queueTemp = mcsDefaultMapAction.S6F11BulibMessage(SECSConst.CEID_Transfer_Initiated, vids);
        //            insertMCSReport(queueTemp);
        //            reportqueues.Add(queueTemp);
        //        }
        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);

        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;

        //}
        //public bool ReportBeginTransfer(string eq_id, out List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;

        //    AMCSREPORTQUEUE queueTemp = null;
        //    reportqueues = new List<AMCSREPORTQUEUE>();
        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn.BeginTransaction();
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Assigned, eq_id);
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Transferring, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Assigned, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Transferring, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);

        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;

        //}
        //public bool ReportArrivalOrComplete(string vh_id, EventType report_event_type, string current_adr_id, string current_sec_id, string current_cst_id, string mcs_cmd_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    try
        //    {
        //        switch (report_event_type)
        //        {
        //            case EventType.LoadArrivals:
        //                isSuccess = doReportLoadArrivals(vh_id, current_adr_id, current_sec_id, reportqueues);
        //                break;
        //            case EventType.LoadComplete:
        //                isSuccess = doReportLoadComplete(vh_id, current_adr_id, current_sec_id, reportqueues);
        //                isSuccess = doReportCarrierIDReadReport(current_cst_id, vh_id, mcs_cmd_id, reportqueues);
        //                isSuccess = doReportLoadComplete_Departed(vh_id, current_adr_id, current_sec_id, reportqueues);
        //                break;
        //            case EventType.UnloadArrivals:
        //                isSuccess = doReportUnloadArrivals(vh_id, current_adr_id, current_sec_id, reportqueues);
        //                break;
        //            case EventType.UnloadComplete:
        //                isSuccess = doReportUnloadComplete(vh_id, reportqueues);
        //                break;
        //            default:
        //                reportqueues = null;
        //                isSuccess = true;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // reportqueues = null;
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //    }
        //    return isSuccess;

        //}
        //public bool ReportLoadingUnloading(string vh_id, EventType actionStat, out List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    bool isReportSuccess = true;
        //    try
        //    {
        //        switch (actionStat)
        //        {
        //            case EventType.Vhloading:
        //                isReportSuccess = doReportLoading(vh_id, out reportqueues);
        //                break;
        //            case EventType.Vhunloading:
        //                isReportSuccess = doReportUnloading(vh_id, out reportqueues);
        //                break;
        //            default:
        //                isReportSuccess = true;
        //                reportqueues = null;
        //                logger.Warn("Enter error status. status:{0}", actionStat.ToString());
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        reportqueues = null;
        //        isReportSuccess = false;
        //        logger.Error(ex, "Exception");
        //    }
        //    return isReportSuccess;
        //}
        //public bool ReportTransferCommandNormalFinish(AVEHICLE eqpt, out List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    reportqueues = new List<AMCSREPORTQUEUE>();

        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn.BeginTransaction();

        //        string eq_id = eqpt.VEHICLE_ID;
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Unassigned, eq_id);
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Transfer_Completed, eq_id);

        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Transfer_Completed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Unassigned, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        scApp.VIDBLL.initialVIDCommandInfo(eq_id);
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //public bool ReportTransferCommandAbortFinish(AVEHICLE eqpt, out List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    reportqueues = new List<AMCSREPORTQUEUE>();
        //    try
        //    {

        //        string eq_id = eqpt.VEHICLE_ID;
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Transfer_Abort_Completed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Unassigned, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);

        //        scApp.VIDBLL.initialVIDCommandInfo(eq_id);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //    }
        //    return isSuccess;
        //}
        //private bool doReportLoadArrivals(string eq_id, string current_adr_id, string current_sec_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    //reportqueues = new List<AMCSREPORTQUEUE>();
        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //using (DBConnection_EF con = new DBConnection_EF())
        //        //{
        //        //conn = new DBConnection_EF();
        //        scApp.VIDBLL.upDateVIDPortID(eq_id, current_adr_id);

        //        //conn.BeginTransaction();
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Arrived, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Arrived, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        //}
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //private bool doReportLoading(string eq_id, out List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    reportqueues = new List<AMCSREPORTQUEUE>();

        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn.BeginTransaction();
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Acquire_Started, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Acquire_Started, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;

        //}
        //private bool doReportLoadComplete(string eq_id, string current_adr_id, string current_sec_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    // reportqueues = new List<AMCSREPORTQUEUE>();

        //    try
        //    {

        //        //TODO_Kevin 要將VID 的 Carrier Loc更新到Vehicle上

        //        //conn = DBConnection_EF.GetContext();
        //        //conn = new DBConnection_EF();
        //        //conn.BeginTransaction();
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Carrier_Installed, eq_id);
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Acquire_Completed, eq_id);
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Departed, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Carrier_Installed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Acquire_Completed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);

        //        //if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Departed, eq_id, out queueTemp))
        //        //    reportqueues.Add(queueTemp);

        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //private bool doReportLoadComplete_Departed(string eq_id, string current_adr_id, string current_sec_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    // reportqueues = new List<AMCSREPORTQUEUE>();

        //    try
        //    {

        //        //TODO_Kevin 要將VID 的 Carrier Loc更新到Vehicle上

        //        //conn = DBConnection_EF.GetContext();
        //        //conn = new DBConnection_EF();
        //        //conn.BeginTransaction();
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Carrier_Installed, eq_id);
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Acquire_Completed, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Departed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);

        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //private bool doReportUnloadArrivals(string eq_id, string current_adr_id, string current_sec_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    //    reportqueues = new List<AMCSREPORTQUEUE>();
        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn = new DBConnection_EF();
        //        scApp.VIDBLL.upDateVIDPortID(eq_id, current_adr_id);

        //        //conn.BeginTransaction();
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Arrived, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Arrived, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //private bool doReportUnloading(string eq_id, out List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    reportqueues = new List<AMCSREPORTQUEUE>();
        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn.BeginTransaction();

        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Deposit_Started, eq_id);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Deposit_Started, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //private bool doReportUnloadComplete(string eq_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    //DBConnection_EF conn = null;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    //  reportqueues = new List<AMCSREPORTQUEUE>();

        //    try
        //    {
        //        //conn = DBConnection_EF.GetContext();
        //        //conn.BeginTransaction();

        //        //TODO_Kevin 要將VID 的 Carrier Loc更新到Port上

        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Removed, eq_id);
        //        //mcsDefaultMapAction.sendS6F11_common(SECSConst.CEID_Vehicle_Deposit_Completed, eq_id);
        //        //if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Removed, eq_id, out queueTemp))
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Carrier_Removed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        if (tryCreatReportMCSMessage(SECSConst.CEID_Vehicle_Deposit_Completed, eq_id, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        //conn.Commit();
        //        //sendMCSS6F11MessageAsyn(reportqueues);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (conn != null) { try { conn.Rollback(); } catch (Exception ex_rollback) { logger.Error(ex_rollback, "Exception"); } }
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //        //if (conn != null) { try { conn.Close(); } catch (Exception ex_close) { logger.Error(ex_close, "Exception"); } }
        //    }
        //    return isSuccess;
        //}
        //private bool doReportCarrierIDReadReport(string carrier_id, string carrier_loc, string current_mcs_cmd_id, List<AMCSREPORTQUEUE> reportqueues)
        //{
        //    Boolean isSuccess = false;
        //    AMCSREPORTQUEUE queueTemp = null;
        //    // reportqueues = new List<AMCSREPORTQUEUE>();

        //    try
        //    {
        //        string id_read_status = SECSConst.IDREADSTATUS_Successful;
        //        ACMD_MCS mcs_cmd = scApp.CMDBLL.getCMD_MCSByID(current_mcs_cmd_id);
        //        if (mcs_cmd != null)
        //        {
        //            if (SCUtility.isEmpty(carrier_id))
        //            {
        //                id_read_status = SECSConst.IDREADSTATUS_Failed;
        //            }
        //            else if (!SCUtility.isMatche(carrier_id, mcs_cmd.CARRIER_ID))
        //            {
        //                id_read_status = SECSConst.IDREADSTATUS_Mismatch;
        //            }
        //        }
        //        if (tryCreatReportMCSMessage_CarrierIDRead(SECSConst.CEID_Carrier_ID_Read, new string[] { current_mcs_cmd_id, carrier_id, carrier_loc, id_read_status }, out queueTemp))
        //            reportqueues.Add(queueTemp);
        //        isSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //        return isSuccess;
        //    }
        //    finally
        //    {
        //    }
        //    return isSuccess;
        //}


        #endregion Mark


    }
}
