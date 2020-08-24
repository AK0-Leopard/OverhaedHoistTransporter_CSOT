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
    public partial class uc_MTLMTSInfo : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        //建構子
        public uc_MTLMTSInfo()
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
        public void start(App.WindownApplication _app)
        {
            dgv_vehicle_status.DataSource = _app.ObjCacheManager.GetVehicleObjToShows();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        //public string Title
        //{
        //    set { lbl_title.DisplayName = value; }
        //    get { return lbl_title.DisplayName; }
        //}

    }
}
