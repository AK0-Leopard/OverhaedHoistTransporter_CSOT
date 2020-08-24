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
using com.mirle.ibg3k0.ohxc.winform.Vo.ObjectRelayVo;
using com.mirle.ibg3k0.sc;
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
    public partial class uc_SP_AlarmMaintenance : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ohxc.winform.App.WindownApplication app = null;
        ohxc.winform.BLL.AlarmBLL AlarmBLL = null;
        public event EventHandler CloseFormEvent;
        #endregion 公用參數設定

        public uc_SP_AlarmMaintenance()
        {
            try
            {
                InitializeComponent();

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

        public void initUI()
        {
            try
            {
                start();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start()
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                AlarmBLL = app.AlarmBLL;
                //將Vh ID加入 Combobox
                List<string> vh_ids = app.VehicleBLL.cache.GetVEHICLEs().Select(vh => vh.VEHICLE_ID).ToList();
                List<string> maintain_device_ids = app.ObjCacheManager.GetMaintainDevice().
                                                                       Select(maintain_eq => maintain_eq.EQPT_ID).
                                                                       ToList();
                cb_VehicleIDs.Items.Add("");
                vh_ids.ForEach(vh_id => cb_VehicleIDs.Items.Add(vh_id));
                maintain_device_ids.ForEach(maintain_eq_id => cb_VehicleIDs.Items.Add(maintain_eq_id));
                //registerEvent();
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

                    dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                    dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                    string every_hour_interval = EveryHourInterval.Text;
                    string alarm_code = AlarmCode.Text;
                    string vh_id = cb_VehicleIDs.Text;
                    bool include_set = ck_IsSet.IsChecked.HasValue ? ck_IsSet.IsChecked.Value : false;
                    bool include_clear = ck_IsClear.IsChecked.HasValue ? ck_IsClear.IsChecked.Value : false;
                    List<AlarmObjToShow> alarms_obj_to_show = null;
                    await Task.Run(() =>
                    {
                        var alarms = AlarmBLL.loadAlarmByConditions(dateTimeFrom, dateTimeTo, include_set, include_clear, vh_id, alarm_code);
                        alarms_obj_to_show = alarms.Select(alarm => new AlarmObjToShow(alarm, AlarmBLL)).ToList();
                    });

                    if (alarms_obj_to_show.Count <= 0)
                    {
                        TipMessage_Type_Light.Show("Warning", "There is no matching data for your query.", BCAppConstants.WARN_MSG);
                    }
                    else
                    {
                        uctl_AlarmList.setDataItemsSource(alarms_obj_to_show);
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


    }
}
