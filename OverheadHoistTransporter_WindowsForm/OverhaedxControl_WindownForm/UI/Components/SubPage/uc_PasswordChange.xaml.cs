//*********************************************************************************
//      uc_PasswordChange.cs
//*********************************************************************************
// File Name: uc_PasswordChange.cs
// Description: Password Change
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/07/29           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.Common;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Windows;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_PasswordChange.xaml 的互動邏輯
    /// </summary>
    public partial class uc_PasswordChange : UserControl
    {
        #region 公用參數設定
        //ohxc.winform.App.WindownApplication app { get; } = null;
        public ohxc.winform.App.WindownApplication app { get; private set; } = null;
        public event EventHandler<PasswordChangeEventArgs> PasswordChange;
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        public event EventHandler CloseFormEvent;
        #endregion 公用參數設定

        public uc_PasswordChange()
        {
            try
            {
                InitializeComponent();
                ////系統無USER登入時，不允許開啟密碼變更介面
                //if (!UASUtility.isLogin(app))
                //{
                //    return;
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                //TODO
                //系統無USER登入時，不允許開啟密碼變更介面
                if (!UASUtility.isLogin(app))
                {
                    return;
                }
                //app = ohxc.winform.App.WindownApplication.getInstance();
                txt_UserID.Text = app.LoginUserID; //顯示當前登入者ID
                old_password_box.Focus(); //將游標指定在密碼位置
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_UserID, false); //設置IME和輸入是否可以是中文
                //app = ohxc.winform.App.WindownApplication.getInstance();
                registerEvent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        public void registerEvent()
        {
            try
            {
                PasswordChange += uc_PasswordChange_SendPasswordChange;
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
                PasswordChange -= uc_PasswordChange_SendPasswordChange;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        //private void recordAction(string tipMessage, string confirmResult)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine(tipMessage);
        //        //sb.AppendLine(string.Format("{0}ConfirmResult:{1}", new string(' ', 5), confirmResult));
        //        sb.AppendLine("ConfirmResult : " + confirmResult);

        //        App.SCApplication.BCSystemBLL.addOperationHis(bcApp.LoginUserID, this.Name, sb.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender.Equals(btn_Change))
                {
                    string userID = txt_UserID.Text;
                    string old_password = old_password_box.Password;
                    string new_password = new_password_box.Password;
                    string verify_password = verify_password_box.Password;
                    PasswordChange?.Invoke(this, new PasswordChangeEventArgs(userID, old_password, new_password, verify_password));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
            }
        }

        private void uc_PasswordChange_SendPasswordChange(object sender, uc_PasswordChange.PasswordChangeEventArgs e)
        {
            try
            {
                string result = string.Empty;
                if (app.LineBLL.SendPasswordChange(e.userID, e.password_o, e.password_n, e.password_v, out result))
                {
                    //變更成功
                    TipMessage_Type_Light_woBtn.Show("", "Update Successful.", BCAppConstants.INFO_MSG);
                    CloseFormEvent?.Invoke(this, e);
                }
                else
                {
                    //變更失敗
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public class PasswordChangeEventArgs : EventArgs
        {
            public PasswordChangeEventArgs(string userID, string password_o, string password_n, string password_v)
            {
                this.userID = userID;
                this.password_o = password_o;
                this.password_n = password_n;
                this.password_v = password_v;
            }

            public string userID { get; private set; }
            public string password_o { get; private set; }
            public string password_n { get; private set; }
            public string password_v { get; private set; }
        }

    }
}
