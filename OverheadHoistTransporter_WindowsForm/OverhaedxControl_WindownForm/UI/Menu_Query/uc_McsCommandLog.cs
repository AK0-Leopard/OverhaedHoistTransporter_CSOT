//*********************************************************************************
//      uc_McsCommandLog.cs
//*********************************************************************************
// File Name: uc_McsCommandLog.cs
// Description: 查詢MCS命令與CST ID介面
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/06/04           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.sc.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASYSEXCUTEQUALITY = com.mirle.ibg3k0.ohxc.winform.Service.ASYSEXCUTEQUALITY;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class uc_McsCommandLog : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Form form = null;
        string startDate = "";
        string endDate = "";
        Color mouse_enter_DGVcells = Color.FromArgb(0, 91, 168);
        #endregion 公用參數設定

        SysExcuteQualityQueryService sysExcuteQualityQueryService;
        private frm_Query frmquery;

        public event EventHandler CloseFormEvent;
        public event EventHandler SendCmdIDEvent;

        public uc_McsCommandLog()
        {
            try
            {
                InitializeComponent();
                cb_HrsInterval.Items.Add("");
                for (int i = 1; i <= 24; i++)
                {
                    cb_HrsInterval.Items.Add("Last " + i + " Hours");
                }
                //form = _form;
                uctl_ElasticQuery_CMDExcute_1.QueryCommandDetailEnevt += Uctl_ElasticQuery_CMDExcute_1_QueryCommandDetailEnevt;
                uctl_ElasticQuery_CMDExcute_1.QueryVehicleDetailEnevt += Uctl_ElasticQuery_CMDExcute_1_QueryVehivleDetailEnevt;

                //uctl_ElasticQuery_CMDExcute_1.QueryMCSCmdIDEnevt += test;
                //uctl_ElasticQuery_sys_process_log.QueryCommandDetailEnevt += Uctl_ElasticQuery_sys_process_log_QueryCommandDetailEnevt1;

                //List<string> lstVh = new List<string>() { "" };
                //List<sc.AVEHICLE> lstEq = ohxc.winform.App.WindownApplication.getInstance().ObjCacheManager.GetVEHICLEs();
                //lstVh.AddRange(lstEq.Select(vh => vh.VEHICLE_ID).ToList());
                //string[] allVh = lstVh.ToArray();
                //ohxc.winform.Common.WinFromUtility.setComboboxDataSource(cb_vh_id, allVh);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void test(object sender, string cmd_id)
        {
            try
            {
                MessageBox.Show(cmd_id.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public override string Text
        {
            get { return "MCS Command Log"; }
        }

        public void startupUI(frm_Query _frmquery)
        {
            try
            {
                this.Width = 1711;
                this.Height = 754;
                m_StartDTCbx.Value = DateTime.Now;
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                cb_HrsInterval.MouseWheel += new MouseEventHandler(cb_HrsInterval_MouseWheel);
                this.frmquery = _frmquery;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        void cb_HrsInterval_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                ((HandledMouseEventArgs)e).Handled = true;
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
                DateTime dateTimeFrom = m_StartDTCbx.Value;
                DateTime dateTimeTo = DateTime.Now;
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                List<RecordReportInfo> record_report_info = null;

                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, null, cmd_id));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                frmquery.CommunicationLog_Click();
                frmquery._CommunicationLog_Click(cmd_id);
                //uc_CommunicationLog.CommunLog_MCSCmdID.Text = cmd_id;            
                //uc_CommunicationLog.uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
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
                DateTime dateTimeFrom = m_StartDTCbx.Value;
                DateTime dateTimeTo = DateTime.Now;
                //uc_CommunicationLog.m_StartDTCbx.Value = dateTimeFrom;
                //uc_CommunicationLog.m_EndDTCbx.Value = dateTimeTo;
                dateTimeFrom = new DateTime(dateTimeFrom.Year, dateTimeFrom.Month, dateTimeFrom.Day, dateTimeFrom.Hour, dateTimeFrom.Minute, dateTimeFrom.Second, DateTimeKind.Local);
                dateTimeTo = new DateTime(dateTimeTo.Year, dateTimeTo.Month, dateTimeTo.Day, dateTimeTo.Hour, dateTimeTo.Minute, dateTimeTo.Second, DateTimeKind.Local);
                List<RecordReportInfo> record_report_info = null;

                await Task.Run(() => record_report_info = sysExcuteQualityQueryService.loadRecodeReportInfo(dateTimeFrom, dateTimeTo, vh_id, null));
                record_report_info = record_report_info.OrderBy(info => info.Timestamp).ToList();
                frmquery.CommunicationLog_Click();
                frmquery._VhIDDetail_Click(vh_id);
                //uc_CommunicationLog.CommunLog_MCSCmdID.Text = cmd_id;            
                //uc_CommunicationLog.uctl_ElasticQuery_sys_process_log.setDataItemsSource(record_report_info);
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
            //try
            //{
            //    btn_Search.Enabled = false;
            //    List<ASYSEXCUTEQUALITY> McsCmdLog = new List<ASYSEXCUTEQUALITY>();

            //    bool result = false;

            //    //從MES要帳
            //    List<ASYSEXCUTEQUALITY> workorderList_ByMES = new List<ASYSEXCUTEQUALITY>();
            //    MESDefaultMapAction mesDefaultMapAction =
            //        scApp.getEQObjCacheManager().getLine().getMapActionByIdentityKey(typeof(MESDefaultMapAction).Name) as MESDefaultMapAction;
            //    await Task.Run(() => result = mesDefaultMapAction.sendAPLSGRQY_RequestWorkorderList(out workorderList_ByMES));

            //    //檢查送出結果
            //    if (!result)
            //    {
            //        TipMessage_Type.Show("The result of send Job List request to MES is [" + result.ToString() + " ].", BCAppConstants.WARN_MSG);
            //        return;
            //    }

            //    //檢查MES來的資料是否為空
            //    if (workorderList_ByMES.Count == 0)
            //    {
            //        TipMessage_Type.Show("No Job ID List from MES", BCAppConstants.WARN_MSG);
            //        return;
            //    }

            //    List<WorkOrder> tmpfromDB = null;
            //    //檢查由MES來的資料是否存在於DB中，並更新Unassigned Job ID的Grid顯示
            //    foreach (WorkOrder wo in workorderList_ByMES)
            //    {

            //        await Task.Run(() => tmpfromDB = scApp.SheetBLL.getWorkOrderListByID(wo.WorkOrder_ID));

            //        if (tmpfromDB.Count == 0)
            //        {
            //            workorderTemp.Add(wo);
            //        }
            //    }
            //    dgv_unAssignWorkOrder.DataSource = workorderTemp;
            //    //JobListToShowOnUnassignJobIDDGV = workorderTemp.ToList();
            //}

            try
            {
                DateTime dateTimeFrom = m_StartDTCbx.Value;
                DateTime dateTimeTo = m_EndDTCbx.Value;
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
                //string cst_id = McsCmdLog_CstID.Text;
                List<ASYSEXCUTEQUALITY> system_qualitys = null;
                await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo));

                //await Task.Run(() => system_qualitys = sysExcuteQualityQueryService.loadSysexcutequalities(dateTimeFrom, dateTimeTo, keyword));
                system_qualitys = system_qualitys.OrderBy(info => info.CMD_INSERT_TIME).ToList();
                uctl_ElasticQuery_CMDExcute_1.setDataItemsSource(system_qualitys);
                //catch (Exception ex)
                //{
                //    logger.Error(ex, "Exception");
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                //form.Close();
                //this.Dispose();
                CloseFormEvent?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void m_EqptIDCbx_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btn_Search_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_30mins_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_30minsAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void VisitLast_30minsAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddMinutes(-30);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_1hrs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_1hrsAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void VisitLast_1hrsAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddHours(-1);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_4hrs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_4hrsAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void VisitLast_4hrsAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddHours(-4);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_12hrs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_12hrsAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void VisitLast_12hrsAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddHours(-12);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_24hrs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_24hrsAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void VisitLast_24hrsAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddHours(-24);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_2days_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_2daysAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private async void VisitLast_2daysAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddDays(-2);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Last_3days_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLast_3daysAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void VisitLast_3daysAsync()
        {
            try
            {
                Last_30mins.LinkVisited = true;
                m_StartDTCbx.Value = DateTime.Now.AddDays(-3);
                m_EndDTCbx.Value = DateTime.Now;
                sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                await search();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Width = this.Width + 1;
                button1.Text = this.Width.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Height = this.Height + 1;
                button2.Text = this.Height.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Width = this.Width - 1;
                button4.Text = this.Width.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Height = this.Height - 1;
                button3.Text = this.Height.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void cb_HrsInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cb_HrsInterval.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-2);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 3:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-3);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 4:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-4);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 5:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-5);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 6:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-6);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 7:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-7);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 8:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-8);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 9:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-9);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 10:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-10);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 11:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-11);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 12:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-12);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 13:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-13);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 14:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-14);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 15:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-15);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 16:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-16);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 17:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-17);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 18:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-18);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 19:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-19);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 20:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-20);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 21:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-21);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 22:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-22);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 23:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-23);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
                        break;
                    case 24:
                        m_StartDTCbx.Value = DateTime.Now.AddHours(-24);
                        m_EndDTCbx.Value = DateTime.Now;
                        sysExcuteQualityQueryService = ohxc.winform.App.WindownApplication.getInstance().GetSysExcuteQualityQueryService();
                        await search();
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

        private void McsCmdLog_CstID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (char.IsLower(e.KeyChar))
                {
                    McsCmdLog_CstID.SelectedText = char.ToUpper(e.KeyChar).ToString();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }
}
