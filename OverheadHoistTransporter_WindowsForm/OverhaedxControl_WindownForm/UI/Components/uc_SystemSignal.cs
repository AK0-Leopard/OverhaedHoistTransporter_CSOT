//*********************************************************************************
//      uc_SystemSignal.cs
//*********************************************************************************
// File Name: uc_SystemSignal.cs
// Description: 用於系統主畫面訊號列的PLC、HOST、Host Status、Line Status使用者控件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                 Author             Request No.    Tag                  Description
// ------------------   ------------------   ------------------   ------------------   ------------------
// 2018/09/12     Boan Chen      N/A                   N/A                  Initial Release
// 2018/11/14     Boan Chen      N/A                   A0.01               PdM與UI會議後，將PLC與HOST斷線之ICON改為紅色。
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
using com.mirle.ibg3k0.ohxc.winform.Properties;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    public partial class uc_SystemSignal : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        //建構子
        public uc_SystemSignal()
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
        public void SetTitle(string title)
        {
            lab_Title_Name.Text = title;
        }

        //更新PLC連線狀態(連線/斷線)
        public void SetIMSConnStatus(string title, bool IMSConnectionStatus)
        {
            try
            {
                lab_Title_Name.Text = title;
                pic_Signal_Value.Image = IMSConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新Host連線狀態(連線/斷線)
        public void SetHostConnStatus(string title, bool HostConnectionStatus)
        {
            try
            {
                lab_Title_Name.Text = title;
                pic_Signal_Value.Image = HostConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新Alarm狀態
        public void SetAlarmStatus(bool AlarmStatus)
        {
            try
            {
                pic_Signal_Value.Image = AlarmStatus ?
                    SystemIcon.icon_Link_OFF :
                    SystemIcon.icon_Link_ON;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新Host Mode連線狀態(Online Remote / Online Local / Offline)
        public void SetHostMode(string title, string HostMode)
        {
            try
            {
                lab_Title_Name.Text = title;
                switch (HostMode)
                {
                    case "Online-Remote":
                        pic_Signal_Value.Image = SystemIcon.icon_MainMenu_Remote;
                        break;
                    case "Online-Local":
                        pic_Signal_Value.Image = SystemIcon.icon_MainMenu_Local;
                        break;
                    case "Offline":
                        pic_Signal_Value.Image = SystemIcon.icon_Link_OFF;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新Line狀態(IDLE / RUN / DOWN / MAINT / OTHER)
        public void SetLineStatus(string title, string lineStatus)
        {
            try
            {
                lab_Title_Name.Text = title;
                switch (lineStatus)
                {
                    case "IDLE":
                        pic_Signal_Value.Image = SystemIcon.icon_IDLE;
                        break;
                    case "RUN":
                        pic_Signal_Value.Image = SystemIcon.icon_Link_ON;
                        break;
                    case "DOWN":
                        pic_Signal_Value.Image = SystemIcon.icon_Shut_Down;
                        break;
                    case "MAINT":
                        pic_Signal_Value.Image = SystemIcon.icon_Maintenance;
                        break;
                    case "OTHER":
                        pic_Signal_Value.Image = SystemIcon.icon_Others;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public Image pImage
        {
            set { pic_Signal_Value.Image = value; }
            get { return pic_Signal_Value.Image; }
        }
    }
}
