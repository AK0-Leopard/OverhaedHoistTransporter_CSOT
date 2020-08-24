//*****************************************************************************************************
//      uc_SP_MCSCommandLog.xaml.cs
//*****************************************************************************************************
// File Name: uc_SP_MCSCommandLog.xaml.cs
// Description: MCS Command Log Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
//  Date           Author           Request No.      Tag                 Description
// -------------  ---------------  ---------------  ---------------     ------------------------------
//  2019/08/22     XeniaTseng        N/A              N/A                Initial Release
//  2019/11/05     XeniaTseng        N/A              N/A                1.新增btn_Search判斷條件
//                                                                                                        2.加上try{}catch{}
//*****************************************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.UI;
using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.sc.Common;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASYSEXCUTEQUALITY = com.mirle.ibg3k0.ohxc.winform.Service.ASYSEXCUTEQUALITY;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_MCSCommandLog.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_MCSCommandLog : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private frm_Query frmquery;
        SysExcuteQualityQueryService sysExcuteQualityQueryService;
        public event EventHandler CloseFormEvent;
        public event EventHandler SendCmdIDEvent;
        #endregion 公用參數設定

        public uc_SP_MCSCommandLog()
        {
            try
            {
                InitializeComponent();
                cb_HrsInterval.Items.Add("");
                for (int i = 1; i <= 24; i++)
                {
                    cb_HrsInterval.Items.Add("Last " + i + " Hours");
                }
                uctl_ElasticQuery_CMDExcute_1.QueryCommandDetailEnevt += Uctl_ElasticQuery_CMDExcute_1_QueryCommandDetailEnevt;
                uctl_ElasticQuery_CMDExcute_1.QueryVehicleDetailEnevt += Uctl_ElasticQuery_CMDExcute_1_QueryVehivleDetailEnevt;

                //設定日期的最大與最小值
                m_StartDTCbx.Minimum = DateTime.Now.AddYears(-3);
                m_StartDTCbx.Maximum = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                m_EndDTCbx.Minimum = DateTime.Now.AddYears(-3);
                m_EndDTCbx.Maximum = DateTime.Today.AddDays(1).AddMilliseconds(-1);

                //預設起訖日期
                m_StartDTCbx.Value = DateTime.Now.AddDays(-1);
                m_EndDTCbx.Value = DateTime.Now.AddMilliseconds(-1);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void startupUI(frm_Query _frmquery)
        {
            try
            {
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                cb_HrsInterval.MouseWheel += new MouseWheelEventHandler(cb_HrsInterval_MouseWheel);
                this.frmquery = _frmquery;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void cb_HrsInterval_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                e.Handled = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void Uctl_ElasticQuery_CMDExcute_1_QueryCommandDetailEnevt(object sender, string cmd_id)
        {
            try
            {
                DateTime dateTimeFrom = System.Convert.ToDateTime(m_StartDTCbx.Value);
                DateTime dateTimeTo = DateTime.Now;
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                List<RecordReportInfo> record_report_info = null;
                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, null, cmd_id));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                frmquery.CommunicationLog_Click();
                frmquery._CommunicationLog_Click(cmd_id);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void Uctl_ElasticQuery_CMDExcute_1_QueryVehivleDetailEnevt(object sender, string vh_id)
        {
            try
            {
                DateTime dateTimeFrom = System.Convert.ToDateTime(m_StartDTCbx.Value);
                DateTime dateTimeTo = DateTime.Now;
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                List<RecordReportInfo> record_report_info = null;
                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, null));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                frmquery.CommunicationLog_Click();
                frmquery._VhIDDetail_Click(vh_id);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async Task search()
        {
            try
            {
                if (SCUtility.isEmpty(m_StartDTCbx.Value.ToString()))
                {
                    TipMessage_Type_Light.Show("Failure", String.Format("Please select start time."), BCAppConstants.WARN_MSG);
                    return;
                }
                if (SCUtility.isEmpty(m_EndDTCbx.Value.ToString()))
                {
                    TipMessage_Type_Light.Show("Failure", String.Format("Please select end time."), BCAppConstants.WARN_MSG);
                    return;
                }
                if (DateTime.Compare(System.Convert.ToDateTime(m_StartDTCbx.Value), System.Convert.ToDateTime(m_EndDTCbx.Value)) == 1)
                {
                    TipMessage_Type_Light.Show("Failure", String.Format("Start time must occur earlier than end time. Please re-enter start time and end time."), BCAppConstants.WARN_MSG);
                    return;
                }
                if (DateTime.Compare(System.Convert.ToDateTime(m_StartDTCbx.Value), System.Convert.ToDateTime(m_EndDTCbx.Value)) < 0)
                {
                    btn_Search.IsEnabled = false;
                    DateTime dateTimeFrom = System.Convert.ToDateTime(m_StartDTCbx.Value);
                    DateTime dateTimeTo = System.Convert.ToDateTime(m_EndDTCbx.Value);
                    string keyword = string.Empty;
                    if (SCUtility.isMatche(McsCmdLog_CstID.Text, ""))
                    {
                        keyword = string.Empty;
                    }
                    else
                    {
                        keyword = McsCmdLog_CstID.Text.Trim();
                    }

                    dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                    dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                    List<ASYSEXCUTEQUALITY> system_qualitys = null;
                    await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo));

                    if (system_qualitys.Count <= 0)
                    {
                        TipMessage_Type_Light.Show("Warning", "There is no matching data for your query.", BCAppConstants.WARN_MSG);
                    }
                    else
                    {
                        system_qualitys = system_qualitys.OrderBy(info => info.CMD_INSERT_TIME).ToList();
                        uctl_ElasticQuery_CMDExcute_1.setDataItemsSource(system_qualitys);
                    }
                    btn_Search.IsEnabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cb_HrsInterval.SelectedIndex = -1;
                m_EndDTCbx.Value = DateTime.Now;

                if (sender.Equals(HypL30mins))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddMinutes(-30);
                    await search();
                }
                else if (sender.Equals(HypL1hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddHours(-1);
                    await search();
                }
                else if (sender.Equals(HypL4hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddHours(-4);
                    await search();
                }
                else if (sender.Equals(HypL12hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddHours(-12);
                    await search();
                }
                else if (sender.Equals(HypL24hours))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddDays(-1);
                    await search();
                }
                else if (sender.Equals(HypL2days))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddDays(-2);
                    await search();
                }
                else if (sender.Equals(HypL3days))
                {
                    m_StartDTCbx.Value = DateTime.Now.AddDays(-3);
                    await search();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void cb_HrsInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                m_EndDTCbx.Value = DateTime.Now;
                switch (cb_HrsInterval.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-1);
                        break;
                    case 2:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-2);
                        break;
                    case 3:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-3);
                        break;
                    case 4:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-4);
                        break;
                    case 5:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-5);
                        break;
                    case 6:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-6);
                        break;
                    case 7:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-7);
                        break;
                    case 8:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-8);
                        break;
                    case 9:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-9);
                        break;
                    case 10:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-10);
                        break;
                    case 11:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-11);
                        break;
                    case 12:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-12);
                        break;
                    case 13:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-13);
                        break;
                    case 14:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-14);
                        break;
                    case 15:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-15);
                        break;
                    case 16:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-16);
                        break;
                    case 17:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-17);
                        break;
                    case 18:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-18);
                        break;
                    case 19:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-19);
                        break;
                    case 20:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-20);
                        break;
                    case 21:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-21);
                        break;
                    case 22:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-22);
                        break;
                    case 23:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-23);
                        break;
                    case 24:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-24);
                        break;
                    default:
                        break;
                }
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
                CloseFormEvent?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
