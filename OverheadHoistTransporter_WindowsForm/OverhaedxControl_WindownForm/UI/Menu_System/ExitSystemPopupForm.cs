//*********************************************************************************
//      ExitSystemPopupForm.cs
//*********************************************************************************
// File Name: ExitSystemPopupForm.cs
// Description: Exit System Popup Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author       Request No.     Tag           Description
// -----------------   -----------------   ------------   ----------   ------------------
// 2019/07/29           Xenia               N/A            N/A          Initial Release
// 2019/11/05           Boan                N/A            A0.01        新增Try Catch。
// 2019/11/20           Xenia               N/A            A0.02        新增DialogResult/在FormClosed時做Dispose/新增get function(ID/PWD)。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Threading;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_System
{
    public partial class ExitSystemPopupForm : Form, ILoginInfo
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string function_code = null;
        private App.WindownApplication app = null;
        #endregion 公用參數設定

        public ExitSystemPopupForm(string function_code, Boolean withDifferentAccount)
        {
            try
            {
                InitializeComponent();
                uc_Exit.CloseFormEvent += Uc_Exit_CloseFormEvent;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; //移除Winform Title icon
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public string getLoginUserID() //A0.02
        {
            return uc_Exit.txt_UserID.Text;
        }
        public string getLoginPassword() //A0.02
        {
            if (BCFUtility.isMatche(uc_Exit.password_box.Password, ""))
            {
                TipMessage_Type_Light.Show("Failure", "Please input password.", BCAppConstants.WARN_MSG);
            }
            return uc_Exit.password_box.Password;
        }

        private void ExitSystemPopupForm_Load(object sender, EventArgs e)
        {
            try
            {
                uc_Exit.btn_Exit.Click += Btn_Exit_Click;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Btn_Exit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK; //A0.02
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_Exit_CloseFormEvent(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ExitSystemPopupForm_FormClosed(object sender, FormClosedEventArgs e) //A0.02
        {
            try
            {
                uc_Exit.unRegisterEvent();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
