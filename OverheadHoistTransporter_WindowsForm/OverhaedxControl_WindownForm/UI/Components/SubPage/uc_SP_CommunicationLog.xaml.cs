//*********************************************************************************
//      uc_SP_CommunicationLog.cs
//*********************************************************************************
// File Name: uc_SP_CommunicationLog.cs
// Description: Communication Log
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/09/16           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
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


namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_SP_CommunicationLog.xaml 的互動邏輯
    /// </summary>
    public partial class uc_SP_CommunicationLog : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        SysExcuteQualityQueryService sysExcuteQualityQueryService;
        public event EventHandler CloseFormEvent;
        #endregion 公用參數設定

        public uc_SP_CommunicationLog()
        {
            try
            {
                InitializeComponent();
                cb_HrsInterval.Items.Add("");
                for (int i = 1; i <= 24; i++)
                {
                    cb_HrsInterval.Items.Add("Last " + i + " Hours");
                }
                uctl_ElasticQuery_sys_process_log.QueryCommandDetailEnevt += Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1;

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

        public async void startupUI(string cmdid, string vhid)
        {
            try
            {
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                CommunLog_MCSCmdID.Text = cmdid;
                CommunLog_VhID.Text = vhid;
                if (!(this.CommunLog_MCSCmdID.Text == string.Empty) || !(this.CommunLog_VhID.Text == string.Empty))
                {
                    await search();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1(string vh_id, DateTime query_start_time)
        {
            try
            {
                DateTime dateTimeFrom = query_start_time.AddSeconds(-30);
                DateTime dateTimeTo = query_start_time.AddHours(1);
                m_StartDTCbx.Value = dateTimeFrom;
                m_EndDTCbx.Value = dateTimeTo;
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                List<RecordReportInfo> record_report_info = null;
                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, null));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                CommunLog_VhID.Text = vh_id;
                uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
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
                    //string keyword = string.Empty;
                    //if (SCUtility.isMatche(McsCmdLog_CstID.Text, ""))
                    //{
                    //    keyword = string.Empty;
                    //}
                    //else
                    //{
                    //    keyword = McsCmdLog_CstID.Text.Trim();
                    //}
                    dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                    dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                    string cmd_id = CommunLog_MCSCmdID.Text;
                    string vh_id = CommunLog_VhID.Text;
                    List<RecordReportInfo> record_report_info = null;
                    await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, cmd_id));

                    if (record_report_info.Count <= 0)
                    {
                        TipMessage_Type_Light.Show("Warning", "There is no matching data for your query.", BCAppConstants.WARN_MSG);
                    }
                    else
                    {
                        record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                        uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
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
    }
}
