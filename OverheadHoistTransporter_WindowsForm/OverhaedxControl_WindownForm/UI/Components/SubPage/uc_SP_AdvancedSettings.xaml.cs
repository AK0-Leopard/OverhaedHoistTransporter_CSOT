//*********************************************************************************
//      uc_SP_AdvancedSettings.cs
//*********************************************************************************
// File Name: uc_SP_AdvancedSettings.cs
// Description: Advanced Settings
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/09/16           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.sc;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_AdvancedSettings.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_AdvancedSettings : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ALINE line;
        WindownApplication app;
        #endregion 公用參數設定

        public uc_SP_AdvancedSettings()
        {
            try
            {
                app = WindownApplication.getInstance();
                line = app.ObjCacheManager.GetLine();
                InitializeComponent();
                CPMS_txt_Value.Text = line.CMDLoopIntervalSetting.ToString();
                CIS_txt_Value.Text = line.CMDIndiPriortySetting.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //預設radio button是否勾選, 需在修改之
            if (line.DisplayLoopSetting == ALINE.CMDIndiSettings.All)
            {
                CIS_radbtn1.IsChecked = true;
            }
            else if (line.DisplayLoopSetting == ALINE.CMDIndiSettings.MCS)
            {
                CIS_radbtn2.IsChecked = true;
            }
            else if (line.DisplayLoopSetting == ALINE.CMDIndiSettings.OHxC)
            {
                CIS_radbtn3.IsChecked = true;
            }
            else if (line.DisplayLoopSetting == ALINE.CMDIndiSettings.Priority)
            {
                CIS_radbtn4.IsChecked = true;
            }
            else
            {
                line.DisplayLoopSetting = ALINE.CMDIndiSettings.All;
                CIS_radbtn1.IsChecked = true;
            }

            if (line.isDisplayLastCMD)
            {
                CPMS_radbtn1.IsChecked = true;
            }
            else
            {
                CPMS_radbtn2.IsChecked = true;
            }

        }

        //private void txt_Value_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        //{
        //    //e.Handled = new System.Text.RegularExpressions.Regex("[^0-9]+").IsMatch(e.Text);
        //    //line.CMDIndiPriortySetting = value;
        //    if (sender == CPMS_txt_Value)
        //    {
        //        if (IsNumeric(CPMS_txt_Value.Text.Trim()))
        //        {
        //            line.CMDLoopIntervalSetting = int.Parse(CPMS_txt_Value.Text.Trim());
        //            line.isCMDIndiSetChanged = true;
        //        }
        //    }
        //    else if (sender == CIS_txt_Value)
        //    {
        //        if (IsNumeric(CIS_txt_Value.Text.Trim()))
        //        {
        //            line.CMDIndiPriortySetting = int.Parse(CIS_txt_Value.Text.Trim());
        //            line.isCMDIndiSetChanged = true;
        //        }
        //    }

        //    //if (e.Handled)
        //    //{
        //    //    if (sender == CPMS_txt_Value)
        //    //    {
        //    //        line.CMDLoopIntervalSetting = int.Parse(e.Text.Trim());
        //    //        line.isCMDIndiSetChanged = true;

        //    //    }
        //    //    else if (sender == CIS_txt_Value)
        //    //    {
        //    //        line.CMDIndiPriortySetting = int.Parse(e.Text.Trim());
        //    //        line.isCMDIndiSetChanged = true;

        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (sender == CPMS_txt_Value)
        //    //    {
        //    //        line.CMDLoopIntervalSetting = 4;
        //    //        line.isCMDIndiSetChanged = true;

        //    //    }
        //    //    else if (sender == CIS_txt_Value)
        //    //    {
        //    //        line.CMDIndiPriortySetting = 0;
        //    //        line.isCMDIndiSetChanged = true;
        //    //    }
        //    //}
        //}
        //public bool IsNumeric(String strNumber)
        //{
        //    Regex NumberPattern = new Regex("[^0-9.-]");
        //    return !NumberPattern.IsMatch(strNumber);
        //}
        private void CIS_radbtn1_Checked(object sender, System.Windows.RoutedEventArgs e)//All Command
        {
            line.DisplayLoopSetting = ALINE.CMDIndiSettings.All;
            line.isCMDIndiSetChanged = true;
        }

        private void CIS_radbtn2_Checked(object sender, System.Windows.RoutedEventArgs e)//Only MCS Command
        {
            line.DisplayLoopSetting = ALINE.CMDIndiSettings.MCS;
            line.isCMDIndiSetChanged = true;
        }

        private void CIS_radbtn3_Click(object sender, System.Windows.RoutedEventArgs e)//Only OHxC Command
        {
            line.DisplayLoopSetting = ALINE.CMDIndiSettings.OHxC;
            line.isCMDIndiSetChanged = true;
        }

        private void CIS_radbtn4_Checked(object sender, System.Windows.RoutedEventArgs e)//Only Priority
        {
            line.DisplayLoopSetting = ALINE.CMDIndiSettings.Priority;
            line.isCMDIndiSetChanged = true;
        }

        private void CPMS_radbtn1_Checked(object sender, System.Windows.RoutedEventArgs e)//Last Command
        {
            line.isDisplayLastCMD = true;
            line.isCMDIndiSetChanged = true;
        }

        private void CPMS_radbtn2_Checked(object sender, System.Windows.RoutedEventArgs e)//Loop Command
        {
            line.isDisplayLastCMD = false;
            line.isCMDIndiSetChanged = true;
        }

        private void txt_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == CPMS_txt_Value)
            {
                int value;
                bool result = int.TryParse(CPMS_txt_Value.Text.Trim(), out value);
                if (result)
                {
                    line.CMDLoopIntervalSetting = value;
                    line.isCMDIndiSetChanged = true;
                }
            }
            else if (sender == CIS_txt_Value)
            {
                int value;
                bool result = int.TryParse(CIS_txt_Value.Text.Trim(), out value);
                if (result)
                {
                    line.CMDIndiPriortySetting = value;
                    line.isCMDIndiSetChanged = true;
                }
            }

        }



        //private void CPMS_txt_Value_TextChanged(object sender, TextChangedEventArgs e)//Loop Interval
        //{
        //    int value = 0;
        //    bool result = int.TryParse(CPMS_txt_Value.Text.Trim(),  out value);
        //    if (result)
        //    {
        //        line.CMDLoopIntervalSetting = value;
        //        line.isCMDIndiSetChanged = true;
        //    }
        //    else
        //    {
        //        TipMessage_Type_Light.Show("", "Input value is invalid", BCAppConstants.WARN_MSG);
        //    }
        //}

        //private void CIS_txt_Value_TextChanged(object sender, TextChangedEventArgs e)//Priority
        //{
        //    int value = 0;
        //    bool result = int.TryParse(CIS_txt_Value.Text.Trim(), out value);
        //    if (result)
        //    {
        //        line.CMDIndiPriortySetting = value;
        //        line.isCMDIndiSetChanged = true;
        //    }
        //    else
        //    {
        //        TipMessage_Type_Light.Show("", "Input value is invalid", BCAppConstants.WARN_MSG);
        //    }
        //}


    }
}
