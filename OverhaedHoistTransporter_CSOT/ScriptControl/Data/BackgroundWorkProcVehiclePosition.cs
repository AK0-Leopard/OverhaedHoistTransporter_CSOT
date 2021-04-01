//*********************************************************************************
//      BackgroundPLCWorkProcessData.cs
//*********************************************************************************
// File Name: BackgroundPLCWorkProcessData.cs
// Description: 背景執行上報Process Data至MES的實際作業
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
//
//**********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.bcf.Common.MPLC;
using com.mirle.ibg3k0.bcf.Controller;
using com.mirle.ibg3k0.bcf.Data;
using com.mirle.ibg3k0.bcf.Schedule;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using NLog;

namespace com.mirle.ibg3k0.sc.Data
{
    /// <summary>
    /// Class BackgroundWorkSample.
    /// </summary>
    /// <seealso cref="com.mirle.ibg3k0.bcf.Schedule.IBackgroundWork" />
    public class BackgroundWorkProcVehiclePosition : IBackgroundWork
    {
        /// <summary>
        /// The logger
        /// </summary>
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets the maximum background queue count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long getMaxBackgroundQueueCount()
        {
            return 1000;
        }

        /// <summary>
        /// Gets the name of the driver.
        /// </summary>
        /// <returns>System.String.</returns>
        public string getDriverName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="workKey">The work key.</param>
        /// <param name="item">The item.</param>
        public void doWorkOld(string workKey, BackgroundWorkItem item)
        {
            try
            {
                App.SCApplication scapp = item.Param[0] as App.SCApplication;
                AVEHICLE vh = item.Param[1] as AVEHICLE;
                ProtocolFormat.OHTMessage.ID_134_TRANS_EVENT_REP recive_str = item.Param[2] as ProtocolFormat.OHTMessage.ID_134_TRANS_EVENT_REP;

                //Do something.
                EventType eventType = recive_str.EventType;
                string current_adr_id = SCUtility.isEmpty(recive_str.CurrentAdrID) ? string.Empty : recive_str.CurrentAdrID;
                string current_sec_id = SCUtility.isEmpty(recive_str.CurrentSecID) ? string.Empty : recive_str.CurrentSecID;
                ASECTION current_sec = scapp.SectionBLL.cache.GetSection(current_sec_id);
                string current_seg_id = current_sec == null ? string.Empty : current_sec.SEG_NUM;

                string last_adr_id = vh.CUR_ADR_ID;
                string last_sec_id = vh.CUR_SEC_ID;
                ASECTION lase_sec = scapp.SectionBLL.cache.GetSection(last_sec_id);
                string last_seg_id = lase_sec == null ? string.Empty : lase_sec.SEG_NUM;
                uint sec_dis = recive_str.SecDistance;

                scapp.VehicleService.doUpdateVheiclePositionAndCmdSchedule
                    (vh, current_adr_id, current_sec_id, current_seg_id, last_adr_id, last_sec_id, last_seg_id, sec_dis, eventType);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
        }
        const int IGNORE_SECTION_DISTANCE = 60;
        public void doWork(string workKey, BackgroundWorkItem item)
        {
            try
            {
                App.SCApplication scapp = item.Param[0] as App.SCApplication;
                AVEHICLE vh = item.Param[1] as AVEHICLE;
                iibg3k0.ttc.Common.TcpIpEventArgs e = item.Param[2] as iibg3k0.ttc.Common.TcpIpEventArgs;
                ID_134_TRANS_EVENT_REP recive_str = (ID_134_TRANS_EVENT_REP)e.objPacket;
                if (recive_str.SecDistance > IGNORE_SECTION_DISTANCE)
                {
                    if (recive_str.CurrentSecID != vh.PRE_SEC_ID)
                    {
                        bool need_process_position = true;
                        int pre_position_seq_num = 0;
                        int current_seq_num = 0;
                        lock (vh.PositionRefresh_Sync)
                        {
                            current_seq_num = e.iSeqNum;
                            pre_position_seq_num = vh.PrePositionSeqNum;
                            //bool need_process_position = true;
                            //lock (eqpt.PositionRefresh_Sync)
                            //{
                            need_process_position = checkPositionSeqNum(current_seq_num, pre_position_seq_num);
                            vh.PrePositionSeqNum = current_seq_num;
                        }
                        if (!need_process_position)
                        {
                            LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(BackgroundWorkProcVehiclePosition), Device: Service.VehicleService.DEVICE_NAME_OHx,
                               Data: $"The vehicles updata position report of seq num is old,by pass this one.old seq num;{pre_position_seq_num},current seq num:{current_seq_num}",
                               VehicleID: vh.VEHICLE_ID,
                               CarrierID: vh.CST_ID);
                            return;
                        }
                        scapp.VehicleBLL.setAndPublishPositionReportInfo2Redis(vh.VEHICLE_ID, recive_str);
                    }
                    else
                    {
                        LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(BackgroundWorkProcVehiclePosition), Device: "OHxC",
                        Data: $"ignore vh:{vh.VEHICLE_ID} of position report,because current section {recive_str.CurrentSecID} is the same with privious section id.",
                        VehicleID: vh.VEHICLE_ID);
                    }
                }
                else
                {
                    LogHelper.Log(logger: logger, LogLevel: LogLevel.Info, Class: nameof(BackgroundWorkProcVehiclePosition), Device: "OHxC",
                             Data: $"ignore vh:{vh.VEHICLE_ID} of position report,because current section distance:{recive_str.SecDistance} less then {IGNORE_SECTION_DISTANCE}",
                             VehicleID: vh.VEHICLE_ID);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
        }
        const int TOLERANCE_SCOPE = 50;
        private const ushort SEQNUM_MAX = 999;
        private bool checkPositionSeqNum(int currnetNum, int preNum)
        {
            try
            {
                int lower_limit = preNum - TOLERANCE_SCOPE;
                if (lower_limit >= 0)
                {
                    //如果該次的Num介於上次的值減去容錯值(TOLERANCE_SCOPE = 50) 至 上次的值
                    //就代表是舊的資料
                    if (currnetNum > (lower_limit) && currnetNum < preNum)
                    {
                        return false;
                    }
                }
                else
                {
                    //如果上次的值減去容錯值變成負的，代表要再由SENDSEQNUM_MAX往回推
                    lower_limit = SEQNUM_MAX + lower_limit;
                    if (currnetNum > (lower_limit) && currnetNum < preNum)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "(checkPositionSeqNum) Exception");
                return true;
            }
        }



    }
}
