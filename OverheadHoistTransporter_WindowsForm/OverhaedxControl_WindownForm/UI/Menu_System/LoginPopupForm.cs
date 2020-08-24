//*********************************************************************************
//      LoginPopupForm.cs
//*********************************************************************************
// File Name: LoginPopupForm.cs
// Description: Login System Popup Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author       Request No.     Tag           Description
// -----------------   -----------------   ------------   ----------   ------------------
// 2019/07/29           Xenia               N/A            N/A          Initial Release
// 2019/11/05           Boan                N/A            A0.01        新增Try Catch。
// 2019/11/20           Xenia                N/A           A0.02        新增DialogResult/在FormClosed時做Dispose/新增get function(ID/PWD)。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_System
{
    public partial class LoginPopupForm : Form, ILoginInfo
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string function_code = null;
        private App.WindownApplication app = null;
        #endregion 公用參數設定

        public LoginPopupForm()
        {
            try
            {
                InitializeComponent();
                uc_Login1.CloseFormEvent += Uc_Login1_CloseFormEvent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //建構子
        public LoginPopupForm(string function_code) : this(function_code, false)
        {
        }

        //建構子
        public LoginPopupForm(string function_code, Boolean withDifferentAccount)
        {
            try
            {
                InitializeComponent();
                this.function_code = function_code;
                app = App.WindownApplication.getInstance();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public string getLoginUserID() //A0.02
        {
            return uc_Login1.txt_UserID.Text;
        }
        public string getLoginPassword() //A0.02
        {
            if (BCFUtility.isMatche(uc_Login1.password_box.Password, ""))
            {
                TipMessage_Type_Light.Show("Failure", "Please input password.", BCAppConstants.WARN_MSG);
            }
            return uc_Login1.password_box.Password;
        }

        private void LoginPopupForm_Load(object sender, EventArgs e)
        {
            uc_Login1.btn_Login.Click += Btn_Login_Click;
        }

        private void Btn_Login_Click(object sender, System.Windows.RoutedEventArgs e)
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

        private void Uc_Login1_CloseFormEvent(object sender, EventArgs e)
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

        private void LoginPopupForm_FormClosed(object sender, FormClosedEventArgs e) //A0.02
        {
            try
            {
                uc_Login1.unRegisterEvent();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
