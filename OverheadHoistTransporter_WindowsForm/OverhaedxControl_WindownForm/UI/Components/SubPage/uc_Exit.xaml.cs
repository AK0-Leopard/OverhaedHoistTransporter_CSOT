//*********************************************************************************
//      uc_Exit.cs
//*********************************************************************************
// File Name: uc_Exit.cs
// Description: Exit
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
using com.mirle.ibg3k0.bcf.Common;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_Exit.xaml 的互動邏輯
    /// </summary>
    public partial class uc_Exit : System.Windows.Controls.UserControl
    {
        #region 公用參數設定
        public ohxc.winform.App.WindownApplication app { get; private set; } = null;
        public event EventHandler<ExitRequestEventArgs> ExitRequest;
        public event EventHandler CloseFormEvent;
        public new bool? DialogResult { get; set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Form form;
        #endregion 公用參數設定

        public uc_Exit()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                if (!UASUtility.isLogin(app)) //系統無USER登入時，不允許開啟密碼變更介面
                {
                    return;
                }
                txt_UserID.Text = app.LoginUserID; //顯示當前登入者ID
                password_box.Focus(); //將游標指定在密碼位置
                registerEvent();
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_UserID, false); //設置IME和輸入是否可以是中文
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
                ExitRequest += uc_Exit_SendExitRequest;
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
                ExitRequest -= uc_Exit_SendExitRequest;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
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
                if (sender.Equals(btn_Exit))
                {
                    //this.DialogResult = true;
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    string userID = txt_UserID.Text;
                    string password = password_box.Password;
                    ExitRequest?.Invoke(this, new ExitRequestEventArgs(userID, password));
                }
                else if (sender.Equals(btn_Cancel))
                {
                    CloseFormEvent?.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_Exit_SendExitRequest(object sender, uc_Exit.ExitRequestEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean exitSuccess = app.LineBLL.SendExitRequest(e.userID, e.password, out result);
                if (exitSuccess)
                {
                    //驗證成功
                    //System.Windows.Forms.DialogResult dialog = new System.Windows.Forms.DialogResult();
                    //dialog = System.Windows.Forms.DialogResult.OK;
                    //DialogResult = true;
                    CloseFormEvent?.Invoke(this, e);
                    TipMessage_Type_Light_woBtn.Show("Succeed", "Exit Successful.", BCAppConstants.INFO_MSG);
                    //form.Close();
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Exit Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public string getLoginUserID()
        {
            return txt_UserID.Text;
        }

        public string getLoginPassword()
        {
            if (BCFUtility.isMatche(password_box.Password, ""))
            {
                //TipMessage.Show("Please input password.");
                TipMessage_Type.Show("Please input password.", BCAppConstants.WARN_MSG);
            }

            return password_box.Password;
        }

        public class ExitRequestEventArgs : EventArgs
        {
            public ExitRequestEventArgs(string userID, string password)
            {
                this.userID = userID;
                this.password = password;
            }

            public string userID { get; private set; }
            public string password { get; private set; }
        }
    }
}
