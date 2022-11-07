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
using com.mirle.ibg3k0.bcf.Data;
using com.mirle.ibg3k0.bcf.Schedule;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using NLog;
using System;

namespace com.mirle.ibg3k0.sc.Data
{
    /// <summary>
    /// Class BackgroundWorkSample.
    /// </summary>
    /// <seealso cref="com.mirle.ibg3k0.bcf.Schedule.IBackgroundWork" />
    public class BackgroundWorkProcAlarmReport : IBackgroundWork
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

        public void doWork(string workKey, BackgroundWorkItem item)
        {
            try
            {
                App.SCApplication scapp = item.Param[0] as App.SCApplication;
                string node_id = item.Param[1] as string;
                string eq_id = item.Param[2] as string;
                string eq_real_id = item.Param[3] as string;
                string current_adr_id = item.Param[4] as string;
                string err_code = item.Param[5] as string;
                ErrorStatus status = (ErrorStatus)item.Param[6];

                scapp.LineService.ProcessAlarmReport(
                    node_id,
                    eq_id,
                    eq_real_id,
                    current_adr_id,
                    err_code,
                    status);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception:");
            }
        }
    }

}
