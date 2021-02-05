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

namespace com.mirle.ibg3k0.sc.Data
{
    /// <summary>
    /// Class BackgroundWorkSample.
    /// </summary>
    /// <seealso cref="com.mirle.ibg3k0.bcf.Schedule.IBackgroundWork" />
    public class BackgroundWorkProcFindTheParkZone : IBackgroundWork
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
            return 100;
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
        public void doWork(string workKey, BackgroundWorkItem item)
        {
            try
            {
                App.SCApplication scapp = item.Param[0] as App.SCApplication;
                BLL.VehicleBLL.FindTheParkZoneTheWay find_the_way = (BLL.VehicleBLL.FindTheParkZoneTheWay)item.Param[1];
                AVEHICLE obstacleVh = item.Param[2] as AVEHICLE;
                switch (find_the_way)
                {
                    case BLL.VehicleBLL.FindTheParkZoneTheWay.IsOnParkZone:
                        //scapp.VehicleBLL.ExcuteAndFindParkZoneForDriveAway(obstacleVh);
                        AVEHICLE blockedVh = item.Param[3] as AVEHICLE;

                        scapp.VehicleBLL.ExcuteAndFindParkZoneForDriveAway(obstacleVh, blockedVh);
                        break;
                    case BLL.VehicleBLL.FindTheParkZoneTheWay.NotOnParkZone:
                        scapp.VehicleBLL.FindParkZoneOrCycleRunZoneNew(obstacleVh);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
        }
    }
}
