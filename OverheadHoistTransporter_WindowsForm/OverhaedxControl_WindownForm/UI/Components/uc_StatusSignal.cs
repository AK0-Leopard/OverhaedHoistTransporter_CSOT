//**********************************************************************************************************
//      uc_StatusSignal.cs
//**********************************************************************************************************
// File Name: uc_StatusSignal.cs
// Description: 用於uc_MTLMTSMaint狀態列的Network、Alive、Mode使用者控件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                  Author               Request No.          Tag            Description
// ------------------   ------------------   ------------------   ------------   ------------------
// 2019/06/14            Xenia Tseng          N/A                   N/A           Initial Release
//**********************************************************************************************************

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

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    public partial class uc_StatusSignal : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        public uc_StatusSignal()
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

        //更新MTL Network連線狀態(連線/斷線)
        public void SetMTLNetworkConnStatus(bool MTLNetworkConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTLNetworkConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTS1 Network連線狀態(連線/斷線)
        public void SetMTS1NetworkConnStatus(bool MTS1NetworkConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTS1NetworkConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTS2 Network連線狀態(連線/斷線)
        public void SetMTS2NetworkConnStatus(bool MTS2NetworkConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTS2NetworkConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTL Alive連線狀態(連線/斷線)
        public void SetMTLAliveConnStatus(bool MTLAliveConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTLAliveConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTS1 Alive連線狀態(連線/斷線)
        public void SetMTS1AliveConnStatus(bool MTS1AliveConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTS1AliveConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTS2 Alive連線狀態(連線/斷線)
        public void SetMTS2AliveConnStatus(bool MTS2AliveConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTS2AliveConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTL Mode連線狀態(連線/斷線)
        public void SetMTLModeConnStatus(bool MTLModeConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTLModeConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTS1 Mode連線狀態(連線/斷線)
        public void SetMTS1ModeConnStatus(bool MTS1ModeConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTS1ModeConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //更新MTS2 Mode連線狀態(連線/斷線)
        public void SetMTS2ModeConnStatus(bool MTS2ModeConnectionStatus)
        {
            try
            {
                pic_StatusSignal_Value.Image = MTS2ModeConnectionStatus ?
                    SystemIcon.icon_Link_ON :
                    SystemIcon.icon_Link_OFF;     //A0.01     
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public Image pImage
        {
            set { pic_StatusSignal_Value.Image = value; }
            get { return pic_StatusSignal_Value.Image; }
        }

    }
}
