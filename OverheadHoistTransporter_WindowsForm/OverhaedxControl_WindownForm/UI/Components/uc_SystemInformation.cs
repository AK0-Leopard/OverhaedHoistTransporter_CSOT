//*********************************************************************************
//      uc_SystemInformation.cs
//*********************************************************************************
// File Name: uc_SystemInformation.cs
// Description: 用於系統主畫面訊號列的Line ID、Run Mode、PPID的使用者控件
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
    public partial class uc_SystemInformation : UserControl
    {
        //*******************公用參數設定*******************
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //*******************公用參數設定*******************

        //建構子
        public uc_SystemInformation()
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

        //設定標題&值
        public void setTitleandValuer(string TitleName, string TitleValue)
        {
            try
            {
                lab_Title_Name.Text = TitleName;
                lab_Title_Value.Text = TitleValue;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
