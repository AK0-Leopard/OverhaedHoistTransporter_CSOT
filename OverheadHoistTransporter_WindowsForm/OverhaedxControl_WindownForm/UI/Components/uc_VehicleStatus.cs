//*********************************************************************************
//      uc_WIPandAlarm.cs
//*********************************************************************************
// File Name: uc_WIPandAlarm.cs
// Description: 用於系統主畫面訊號列的的WIP與Alarm使用者控件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                 Author             Request No.    Tag                  Description
// ------------------   ------------------   ------------------   ------------------   ------------------
// 2018/09/12     Boan Chen      N/A                   N/A                  Initial Release
//**********************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    public partial class uc_VehicleStatus : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        //建構子
        public uc_VehicleStatus()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //組件載入
        private void MainSignalBackGround_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public string RemoteCount
        {
            set { lab_auto_remote_value.Text = value; }
        }
        public string IdleCount
        {
            set { lbl_idle_value.Text = value; }
        }
        public string LocalCount
        {
            set { lbl_auto_local_value.Text = value; }
        }
        public string ErrorCount
        {
            set { lbl_error_value.Text = value; }
        }
    }
}
