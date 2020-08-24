//*********************************************************************************
//      uc_TabSystemControl.cs
//*********************************************************************************
// File Name: uc_TabSystemControl.cs
// Description: 用於uc_SystemModeControl的System Control使用者控件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date            Author       Request No.    Tag     Description
// ------------ -------------  -------------  ------  -----------------------------
// 2019/07/11    Xenia Tseng       N/A         N/A     Initial Release
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
    public partial class uc_StatusSignal_1 : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        public uc_StatusSignal_1()
        {
            InitializeComponent();

        }

        //更新連線狀態(連線/斷線)
        public void SetConnSignal(string title, bool ConnectionStatus)
        {
            try
            {
                lab_Title_Name.Text = title;
                pl_signal_value.BackColor = ConnectionStatus ?
                    Color.FromArgb(0, 204, 0):
                    Color.FromArgb(255, 0, 0);
                lab_Title_Name.ForeColor = ConnectionStatus ?
                    Color.White:
                    Color.White;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
