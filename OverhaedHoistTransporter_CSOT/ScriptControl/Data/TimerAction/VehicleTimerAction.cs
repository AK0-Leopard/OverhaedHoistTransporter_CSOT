//*********************************************************************************
//      IdleVehicleTimerAction.cs
//*********************************************************************************
// File Name: IdleVehicleTimerAction.cs
// Description: 
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
//**********************************************************************************
using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Controller;
using com.mirle.ibg3k0.bcf.Data.TimerAction;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.TimerAction
{
    /// <summary>
    /// Class IdleVehicleTimerAction.
    /// </summary>
    /// <seealso cref="com.mirle.ibg3k0.bcf.Data.TimerAction.ITimerAction" />
    class VehicleTimerAction : ITimerAction
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// The sc application
        /// </summary>
        protected SCApplication scApp = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="IdleVehicleTimerAction"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="intervalMilliSec">The interval milli sec.</param>
        public VehicleTimerAction(string name, long intervalMilliSec)
            : base(name, intervalMilliSec)
        {

        }

        /// <summary>
        /// Initializes the start.
        /// </summary>
        public override void initStart()
        {
            //do nothing
            scApp = SCApplication.getInstance();

        }

        private long syncPoint = 0;

        /// <summary>
        /// Timer Action的執行動作
        /// </summary>
        /// <param name="obj">The object.</param>
        public override void doProcess(object obj)
        {

            if (System.Threading.Interlocked.Exchange(ref syncPoint, 1) == 0)
            {
                try
                {
                    var vhs = scApp.VehicleBLL.cache.loadVhs();
                    foreach (var vh in vhs)
                    {
                        vehicleStatusCheck(vh);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                finally
                {
                    System.Threading.Interlocked.Exchange(ref syncPoint, 0);
                }
            }


        }

        private void vehicleStatusCheck(AVEHICLE vh)
        {
            try
            {
                if (!vh.isTcpIpConnect) return;
                //如果車子沒有installed就也不用幫忙檢查是否有通訊
                if (!vh.IS_INSTALLED) return;
                //1.檢查是否已經大於一定時間沒有進行通訊
                double from_last_comm_time = vh.getFromTheLastCommTime(scApp.getBCFApplication());
                if (from_last_comm_time > AVEHICLE.MAX_ALLOW_NO_COMMUNICATION_TIME_SECOND)
                {
                    Task.Run(() => vh.onLongTimeNoCommuncation());
                }
                //double action_time = vh.CurrentCommandExcuteTime.Elapsed.TotalSeconds;
                ////if (action_time > AVEHICLE.MAX_ALLOW_ACTION_TIME_SECOND)
                //if (action_time > SystemParameter.MaxAllowActionTimeSec)
                //{
                //    vh.onLongTimeInaction(vh.OHTC_CMD);
                //}
                //var cmds = scApp.CMDBLL.loadUnfinishCMD_OHT();
                var cmds = ACMD_OHTC.tryGetCMD_OHTCSList();
                if (!vh.isLongTimeInaction && vh.CurrentCommandExcuteTime.ElapsedMilliseconds > AVEHICLE.MAX_ALLOW_ACTION_TIME_MILLISECOND)
                {
                    var currnet_excute_ids = getVhCurrentExcuteCommandID(vh, cmds);
                    Task.Run(() => vh.onLongTimeInaction(currnet_excute_ids));
                    vh.isLongTimeInaction = true;
                }
                else
                {
                    if (vh.isLongTimeInaction)
                    {
                        Task.Run(() => vh.onNonLongTimeInaction());
                    }
                    vh.isLongTimeInaction = false;
                }
                //當車子是Commnading狀態，但cmd Table並無該筆命令時，則將車子的下達cancel
                bool has_command_excute_in_db = hasCommandActionTimeCheck(vh, cmds);
                if (vh.ACT_STATUS == VHActionStatus.Commanding &&
                    has_command_excute_in_db == false)
                {
                    if (!vh.isCommandStateOutOfSyncHappend)
                    {
                        Task.Run(() => vh.onCommandStateOutOfSyncHappend());
                        vh.isCommandStateOutOfSyncHappend = true;
                    }
                }
                else
                {
                    vh.isCommandStateOutOfSyncHappend = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logger: logger, LogLevel: LogLevel.Warn, Class: nameof(AVEHICLE), Device: "OHTC",
                   Data: ex,
                   VehicleID: vh.VEHICLE_ID,
                   CarrierID: vh.CST_ID);
            }
        }
        private bool hasCommandActionTimeCheck(AVEHICLE vh, List<ACMD_OHTC> cmds)
        {
            if (cmds == null) return false;
            bool has_command_excute = getVhCurrentExcuteCommandCount(vh, cmds);
            if (has_command_excute)
            {
                if (!vh.CurrentCommandExcuteTime.IsRunning)
                {
                    vh.CurrentCommandExcuteTime.Restart();
                }
            }
            else
            {
                if (vh.CurrentCommandExcuteTime.IsRunning)
                {
                    vh.CurrentCommandExcuteTime.Reset();
                }
            }
            return has_command_excute;
        }

        private bool getVhCurrentExcuteCommandCount(AVEHICLE vh, List<ACMD_OHTC> cmds)
        {
            return cmds.Where(cmd => SCUtility.isMatche(cmd.VH_ID, vh.VEHICLE_ID) &&
                                     cmd.CMD_STAUS > E_CMD_STATUS.Queue)
                       .Count() > 0;
        }
        private string getVhCurrentExcuteCommandID(AVEHICLE vh, List<ACMD_OHTC> cmds)
        {
            return cmds.Where(cmd => SCUtility.isMatche(cmd.VH_ID, vh.VEHICLE_ID))
                       .Select(cmd => SCUtility.Trim(cmd.CMD_ID, true))
                       .FirstOrDefault();
        }
    }
}