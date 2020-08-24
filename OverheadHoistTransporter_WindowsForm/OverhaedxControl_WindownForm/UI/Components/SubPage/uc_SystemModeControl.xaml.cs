//*********************************************************************************
//      uc_SystemModeControl.cs
//*********************************************************************************
// File Name: uc_SystemModeControl.cs
// Description: System Mode Control
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/07/29           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using NLog;
using System;
using System.Windows;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SystemModeControl.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SystemModeControl : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public uc_SystemModeControl()
        {
            try
            {
                InitializeComponent();
                refreshStatus();
                ButtonClick(btn_Off, null);
                ButtonClick(btn_Offline, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    try { TabItemMouseLeftButtonUp(sender, e); }
        //    catch (Exception ex) { }
        //}
        //private void TabItemMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (sender.Equals(Item1))
        //        {
        //            TitleName.Text = "System Mode Control";
        //        }
        //    }
        //    finally { }
        //}

        private void refreshStatus()
        {
            try
            {
                uc_CommunicationStatus.SetConnStatus("On", true);
                uc_ControlStatus.SetConnStatus("Offline", false);
                uc_TSCStatus.SetConnStatus("Pause", false);
                uc_ControlStatusSignal1.SetConnStatus("Current port states", true);
                uc_ControlStatusSignal2.SetConnStatus("Current state", true);
                uc_ControlStatusSignal3.SetConnStatus("Enhanced vehicles", true);
                uc_ControlStatusSignal4.SetConnStatus("TSC state", true);
                uc_ControlStatusSignal5.SetConnStatus("Unit alarm state list", true);
                uc_ControlStatusSignal6.SetConnStatus("Enhanced transfers", true);
                uc_ControlStatusSignal7.SetConnStatus("Enhanced carriers", false);
                uc_ControlStatusSignal8.SetConnStatus("Lane cut list", false);
                uc_MCS_Status.SetConnStatus("MCS", true);
                uc_Router_Status.SetConnStatus("Router", false);
                uc_VhLk_Status_OHT.SetConnStatus("OHT", true);
                uc_PLC_Status_Changer.SetConnStatus("Changer PLC", true);
                uc_AP_Status_1.SetConnStatus("Adam 6050-1", true);
                uc_AP_Status_2.SetConnStatus("Adam 6050-2", true);
                uc_AP_Status_3.SetConnStatus("Adam 6050-3", true);
                uc_AP_Status_4.SetConnStatus("Adam 6050-4", true);
                uc_AP_Status_5.SetConnStatus("Adam 6050-5", true);
                uc_AP_Status_6.SetConnStatus("Adam 6050-6", true);
                uc_AP_Status_7.SetConnStatus("Adam 6050-7", true);
                uc_AP_Status_8.SetConnStatus("Adam 6050-8", true);
                uc_AP_Status_9.SetConnStatus("Adam 6050-9", true);
                uc_AP_Status_10.SetConnStatus("Adam 6050-10", true);
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
                if (sender.Equals(btn_On))
                {
                    btn_On.IsEnabled = false;
                    btn_Off.IsEnabled = true;
                    uc_CommunicationStatus.SetConnStatus("On", true);
                }
                else if (sender.Equals(btn_Off))
                {
                    btn_On.IsEnabled = true;
                    btn_Off.IsEnabled = false;
                    uc_CommunicationStatus.SetConnStatus("Off", false);
                }
                else if (sender.Equals(btn_OnlineR))
                {
                    btn_OnlineR.IsEnabled = false;
                    btn_OnlineL.IsEnabled = true;
                    btn_Offline.IsEnabled = true;
                    uc_ControlStatus.SetConnStatus("Online Remote", true);
                    btn_Auto.IsEnabled = true;
                    if (btn_Pause.IsEnabled == false)
                    {
                        btn_Auto.IsEnabled = true;
                        btn_Pause.IsEnabled = false;
                        uc_TSCStatus.SetConnStatus("Pause", false);
                    }
                    else
                    {
                        btn_Auto.IsEnabled = false;
                        btn_Pause.IsEnabled = true;
                        uc_TSCStatus.SetConnStatus("Auto", true);
                    }
                }
                else if (sender.Equals(btn_OnlineL))
                {
                    btn_OnlineR.IsEnabled = true;
                    btn_OnlineL.IsEnabled = false;
                    btn_Offline.IsEnabled = true;
                    uc_ControlStatus.SetConnStatus("Online Local", true);
                }
                else if (sender.Equals(btn_Offline))
                {
                    btn_OnlineR.IsEnabled = true;
                    btn_OnlineL.IsEnabled = false;
                    btn_Offline.IsEnabled = false;
                    uc_ControlStatus.SetConnStatus("Offline", false);
                    pause_Button_alloff_Click();
                }
                else if (sender.Equals(btn_Auto))
                {
                    btn_Auto.IsEnabled = false;
                    btn_Pause.IsEnabled = true;
                    uc_TSCStatus.SetConnStatus("Auto", true);
                }
                else if (sender.Equals(btn_Pause))
                {
                    btn_Auto.IsEnabled = true;
                    btn_Pause.IsEnabled = false;
                    uc_TSCStatus.SetConnStatus("Pause", false);
                }
            }
            finally { }
        }

        private void pause_Button_alloff_Click()
        {
            try
            {
                btn_Auto.IsEnabled = false;
                btn_Pause.IsEnabled = false;
                uc_TSCStatus.SetConnStatus("Pause", false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
