//*********************************************************************************
//      uc_ChangePriority.cs
//*********************************************************************************
// File Name: uc_ChangePriority.cs
// Description: Change Priority
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_ChangePriority.xaml 的互動邏輯
    /// </summary>
    public partial class uc_ChangePriority : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ohxc.winform.App.WindownApplication app = null;
        string mcs_cmd_id = string.Empty;
        public event EventHandler CloseFormEvent;
        public event EventHandler<MCSCommandPriortyChangeEventArgs> mSCCommandPriority;
        #endregion 公用參數設定

        public uc_ChangePriority()
        {
            try
            {
                InitializeComponent();
                num_PriSum.Focus();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void initUI(string cmd_id)
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                mcs_cmd_id = cmd_id;
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
                mSCCommandPriority += MSCCommandPriorityChange;
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
                mSCCommandPriority -= MSCCommandPriorityChange;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SetIsInputMethodEnabled();
                if (app.ObjCacheManager.GetMCS_CMD().Count > 0)
                {
                    txt_CurMaxPriSum.Text = app.CmdBLL.getCMD_MCSMaxProritySum().ToString();
                    txt_CurMinPriSum.Text = app.CmdBLL.getCMD_MCSMinProritySum().ToString();
                    ACMD_MCS mcs_cmd = app.CmdBLL.GetCmd_MCSByID(mcs_cmd_id);
                    txt_McsCmdID.Text = mcs_cmd_id;
                    txt_McsPri.Text = mcs_cmd.PRIORITY.ToString();
                    txt_PortPri.Text = mcs_cmd.PORT_PRIORITY.ToString();
                    txt_TimePri.Text = mcs_cmd.TIME_PRIORITY.ToString();
                    num_PriSum.Value = mcs_cmd.PRIORITY_SUM;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //設置IME和輸入是否可以是中文
        private void SetIsInputMethodEnabled()
        {
            try
            {
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_CurMaxPriSum, false);
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_CurMinPriSum, false);
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_McsCmdID, false);
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_McsPri, false);
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_PortPri, false);
                System.Windows.Input.InputMethod.SetIsInputMethodEnabled(txt_TimePri, false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(() => mSCCommandPriority?.Invoke(this, new MCSCommandPriortyChangeEventArgs(mcs_cmd_id.Trim(), num_PriSum.Value.ToString())));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MSCCommandPriorityChange(object sender, MCSCommandPriortyChangeEventArgs e)
        {
            try
            {
                if (!app.LineBLL.SendMCSCommandChangePriority(e.mcs_cmd, e.priority, out string result))
                {
                    //MessageBox.Show(result);
                    TipMessage_Type_Light.Show("", result, BCAppConstants.WARN_MSG);
                }
                else
                {
                    Adapter.Invoke(new SendOrPostCallback((o1) =>
                    {
                        CloseFormEvent?.Invoke(this, e);
                    }), null);
                    TipMessage_Type_Light.Show("", "Priority Change Succeed", BCAppConstants.INFO_MSG);
                }
                //app.LineBLL.SendHostModeChange(e.host_mode);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public class MCSCommandPriortyChangeEventArgs : EventArgs
        {
            public MCSCommandPriortyChangeEventArgs(string mcs_cmd, string priority)
            {
                this.mcs_cmd = mcs_cmd;
                this.priority = priority;
            }
            public string mcs_cmd { get; private set; }
            public string priority { get; private set; }
        }

    }
}
