//*********************************************************************************
//      uc_Login.cs
//*********************************************************************************
// File Name: uc_Login.cs
// Description: Login
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/07/29           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Windows;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_Login.xaml 的互動邏輯
    /// </summary>
    /// 

    public interface ILoginInfo
    {
        string getLoginUserID();
        string getLoginPassword();
    }

    public partial class uc_Login : UserControl
    {
        #region 公用參數設定
        public event EventHandler CloseFormEvent;
        public new bool? DialogResult { get; set; }
        private string function_code = null;
        ohxc.winform.App.WindownApplication app = null;
        public event EventHandler<LogInRequestEventArgs> LogInRequest;
        //System.Windows.Forms.DialogResult dialogResult = new System.Windows.Forms.DialogResult();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public uc_Login()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_UserID.Focus(); //將游標指定在密碼位置
                app = ohxc.winform.App.WindownApplication.getInstance();
                registerEvent();
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_UserID, false); //設置IME和輸入是否可以是中文
                //app.ObjCacheManager.PortStationUpdateComplete += ObjCacheManager_PortStationUpdateComplete;
            }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        public void registerEvent()
        {
            try
            {
                LogInRequest += uc_Login_SendLogInRequest;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void unRegisterEvent()
        {
            try
            {
                LogInRequest -= uc_Login_SendLogInRequest;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { ButtonClick(sender, e); }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender.Equals(btn_Login))
                {
                    //System.Windows.Forms.DialogResult dialog = new System.Windows.Forms.DialogResult();
                    //dialog = System.Windows.Forms.DialogResult.OK;
                    DialogResult = true;

                    string userID = txt_UserID.Text;
                    string password = password_box.Password;
                    LogInRequest?.Invoke(this, new LogInRequestEventArgs(userID, password));
                }
            }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        private void uc_Login_SendLogInRequest(object sender, LogInRequestEventArgs e)
        {
            try
            {
                string result = string.Empty;
                if (app.LineBLL.SendLogInRequest(e.userID, e.password, out result))
                {
                    CloseFormEvent?.Invoke(this, e);
                    TipMessage_Type_Light_woBtn.Show("", "Login Successful.", BCAppConstants.INFO_MSG);
                    app.login(e.userID);
                }
                else
                {
                    TipMessage_Type_Light.Show("", "Login Fail.", BCAppConstants.WARN_MSG);
                }
            }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        public class LogInRequestEventArgs : EventArgs
        {
            public LogInRequestEventArgs(string userID, string password)
            {
                this.userID = userID;
                this.password = password;
            }
            public string userID { get; private set; }
            public string password { get; private set; }
        }
    }
}
