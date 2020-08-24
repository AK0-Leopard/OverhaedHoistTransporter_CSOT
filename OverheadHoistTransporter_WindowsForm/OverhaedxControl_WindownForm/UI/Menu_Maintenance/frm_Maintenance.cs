//*********************************************************************************
//      frm_Maintenance.cs
//*********************************************************************************
// File Name: frm_Maintenance.cs
// Description: Maintenance
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/07/03          Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform;
using com.mirle.ibg3k0.sc;
using NLog;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static com.mirle.ibg3k0.bc.winform.UI.uc_CommunicationLog;
using com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage;
using com.mirle.ibg3k0.bc.winform.Common;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public enum MaintenanceType
    {
        MTLMTSMaint,
        PortMaint,
        VehicleManagement,
        AdvancedSettings,
        AlarmMaintenance
    }

    public partial class frm_Maintenance : Form
    {
        #region 公用參數設定
        OHxCMainForm mainForm = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool isClick_MTLMTSMaint = false;
        bool isClick_PortMaint = false;
        bool isClick_VhManage = false;
        bool isClick_AdvSet = false;
        bool isClick_AlarmMaint = false;
        uc_SP_MTLMTSMaintenance MTLMTSMaint = null;
        uc_CommunicationLog CommunicationLog = null;
        MaintenanceType _maintenance_type;
        #endregion 公用參數設定

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="_mainForm"></param>
        public frm_Maintenance(OHxCMainForm _mainForm)
        {
            try
            {
                InitializeComponent();
                mainForm = _mainForm;
                uc_SP_MTLMTSMaintenance1.CloseFormEvent += uc_SP_MTLMTSMaintenance_CloseFormEvent;
                btn_Close1.Button_Click += Btn_Close_Button_Click;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Btn_Close_Button_Click(object sender, EventArgs e)
        {
            try
            {
                uc_SP_PortMaint1.unRegisterEvent();
                uc_SP_VehicleManagement1.unRegisterEvent();
                uc_SP_MTLMTSMaintenance1.unRegisterEvent();
                mainForm.removeForm(typeof(frm_Maintenance).Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void frm_Maintenance_Load(object sender, EventArgs e)
        {
            try
            {
                UASUtility.UpdateUIDisplayByAuthority(mainForm.app, this);
                uc_SP_MTLMTSMaintenance1.initUI();
                uc_SP_VehicleManagement1.initUI();
                uc_SP_PortMaint1.initUI();
                uc_SP_AlarmMaintenance1.initUI();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_SP_MTLMTSMaintenance_CloseFormEvent(object sender, EventArgs e)
        {
            try
            {
                uc_SP_PortMaint1.unRegisterEvent();
                uc_SP_VehicleManagement1.unRegisterEvent();
                uc_SP_MTLMTSMaintenance1.unRegisterEvent();
                mainForm.removeForm(typeof(frm_Maintenance).Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 指定開啟的查詢控件
        /// </summary>
        /// <param name="_maintenance_type"></param>
        public void setFormType(MaintenanceType _maintenance_type)
        {
            try
            {
                this._maintenance_type = _maintenance_type;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(lab_MTLMTSMaint))
                {
                    lab_FormTitle.Text = "MTL/MTS Maintenance";
                    pl_MTLMTS.Visible = true;
                    pl_Others.Visible = false;
                    uc_SP_MTLMTSMaintenance.Visible = true;
                    uc_SP_PortMaint.Visible = false;
                    uc_SP_VehicleManagement.Visible = false;
                    uc_SP_AdvancedSettings.Visible = false;
                    uc_SP_AlarmMaintenance.Visible = false;
                    isClick_MTLMTSMaint = true;
                    isClick_PortMaint = false;
                    isClick_VhManage = false;
                    isClick_AdvSet = false;
                    isClick_AlarmMaint = false;
                    resetTabLabelColor();
                    setLabIsClickColor(lab_MTLMTSMaint);
                    setTabPageMouseEnterAndLeaveColor(lab_MTLMTSMaint, true);
                }
                else if (sender.Equals(lab_PortMaint))
                {
                    lab_FormTitle.Text = "Port Maintenance";
                    pl_MTLMTS.Visible = true;
                    pl_Others.Visible = true;
                    uc_SP_MTLMTSMaintenance.Visible = false;
                    uc_SP_PortMaint.Visible = true;
                    uc_SP_VehicleManagement.Visible = false;
                    uc_SP_AdvancedSettings.Visible = false;
                    uc_SP_AlarmMaintenance.Visible = false;
                    isClick_MTLMTSMaint = false;
                    isClick_PortMaint = true;
                    isClick_VhManage = false;
                    isClick_AdvSet = false;
                    isClick_AlarmMaint = false;
                    resetTabLabelColor();
                    setLabIsClickColor(lab_PortMaint);
                    setTabPageMouseEnterAndLeaveColor(lab_PortMaint, true);
                }
                else if (sender.Equals(lab_VhManage))
                {
                    lab_FormTitle.Text = "Vehicle Management";
                    pl_MTLMTS.Visible = true;
                    pl_Others.Visible = true;
                    uc_SP_MTLMTSMaintenance.Visible = false;
                    uc_SP_PortMaint.Visible = false;
                    uc_SP_VehicleManagement.Visible = true;
                    uc_SP_AdvancedSettings.Visible = false;
                    uc_SP_AlarmMaintenance.Visible = false;
                    isClick_MTLMTSMaint = false;
                    isClick_PortMaint = false;
                    isClick_VhManage = true;
                    isClick_AdvSet = false;
                    isClick_AlarmMaint = false;
                    resetTabLabelColor();
                    setLabIsClickColor(lab_VhManage);
                    setTabPageMouseEnterAndLeaveColor(lab_VhManage, true);
                }
                else if (sender.Equals(lab_AdvSet))
                {
                    lab_FormTitle.Text = "Advanced Settings";
                    pl_MTLMTS.Visible = true;
                    pl_Others.Visible = true;
                    uc_SP_MTLMTSMaintenance.Visible = false;
                    uc_SP_PortMaint.Visible = false;
                    uc_SP_VehicleManagement.Visible = false;
                    uc_SP_AdvancedSettings.Visible = true;
                    uc_SP_AlarmMaintenance.Visible = false;
                    isClick_MTLMTSMaint = false;
                    isClick_PortMaint = false;
                    isClick_VhManage = false;
                    isClick_AdvSet = true;
                    isClick_AlarmMaint = false;
                    resetTabLabelColor();
                    setLabIsClickColor(lab_AdvSet);
                    setTabPageMouseEnterAndLeaveColor(lab_AdvSet, true);
                }
                else if (sender.Equals(lab_AlarmMaint))
                {
                    lab_FormTitle.Text = "Alarm Maintenance";
                    pl_MTLMTS.Visible = true;
                    pl_Others.Visible = true;
                    uc_SP_MTLMTSMaintenance.Visible = false;
                    uc_SP_PortMaint.Visible = false;
                    uc_SP_VehicleManagement.Visible = false;
                    uc_SP_AdvancedSettings.Visible = false;
                    uc_SP_AlarmMaintenance.Visible = true;
                    isClick_MTLMTSMaint = false;
                    isClick_PortMaint = false;
                    isClick_VhManage = false;
                    isClick_AdvSet = false;
                    isClick_AlarmMaint = true;
                    resetTabLabelColor();
                    setLabIsClickColor(lab_AlarmMaint);
                    setTabPageMouseEnterAndLeaveColor(lab_AlarmMaint, true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTLMTSMaint_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_MTLMTSMaint, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTLMTSMaint_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_MTLMTSMaint)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_MTLMTSMaint, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_PortMaint_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_PortMaint, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_PortMaint_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_PortMaint)
                {
                    return;
                }

                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_PortMaint, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_VhManage_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_VhManage, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_VhManage_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_VhManage)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_VhManage, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_AdvSet_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_AdvSet, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_AdvSet_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_AdvSet)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_AdvSet, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 設定標籤在滑鼠進入與離開時的顏色
        /// </summary>
        /// <param name="_lab"></param>
        /// <param name="_mouseIsEnter"></param>
        private void setTabPageMouseEnterAndLeaveColor(Label _lab, bool _mouseIsEnter)
        {
            try
            {
                if (_mouseIsEnter)
                {
                    _lab.BackColor = Color.FromArgb(246, 246, 246);
                    _lab.ForeColor = Color.FromArgb(27, 35, 56);
                }
                else
                {
                    _lab.BackColor = Color.FromArgb(0, 51, 102);
                    _lab.ForeColor = Color.FromArgb(255, 255, 255);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 恢復標籤顏色
        /// </summary>
        private void resetTabLabelColor()
        {
            try
            {
                Adapter.Invoke(new SendOrPostCallback((o1) =>
                {
                    lab_MTLMTSMaint.BackColor = Color.FromArgb(0, 51, 102);
                    lab_MTLMTSMaint.ForeColor = Color.White;
                    lab_PortMaint.BackColor = Color.FromArgb(0, 51, 102);
                    lab_PortMaint.ForeColor = Color.White;
                    lab_VhManage.BackColor = Color.FromArgb(0, 51, 102);
                    lab_VhManage.ForeColor = Color.White;
                    lab_AdvSet.BackColor = Color.FromArgb(0, 51, 102);
                    lab_AdvSet.ForeColor = Color.White;
                    lab_AlarmMaint.BackColor = Color.FromArgb(0, 51, 102);
                    lab_AlarmMaint.ForeColor = Color.White;

                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 設定標籤點選或選擇後的顏色
        /// </summary>
        /// <param name="_lab"></param>
        private void setLabIsClickColor(Label _lab)
        {
            try
            {
                Adapter.Invoke(new SendOrPostCallback((o1) =>
                {
                    _lab.BackColor = Color.FromArgb(246, 246, 246);
                    _lab.ForeColor = Color.FromArgb(27, 35, 56);
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void switchTabControl(int fromtype)
        {
            try
            {
                switch (fromtype)
                {
                    case 0:
                        lab_Click(lab_MTLMTSMaint, null);
                        break;
                    case 1:
                        lab_Click(lab_PortMaint, null);
                        break;
                    case 2:
                        lab_Click(lab_VhManage, null);
                        break;
                    case 3:
                        lab_Click(lab_AdvSet, null);
                        break;
                    case 4:
                        lab_Click(lab_AlarmMaint, null);
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

        private void frm_Maintenance_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                uc_SP_PortMaint1.unRegisterEvent();
                uc_SP_VehicleManagement1.unRegisterEvent();
                uc_SP_MTLMTSMaintenance1.unRegisterEvent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_AlarmMaint_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_AlarmMaint, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_AlarmMaint_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_AlarmMaint)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_AlarmMaint, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void frm_Maintenance_Shown(object sender, EventArgs e)
        {
        }

        private void frm_Maintenance_VisibleChanged(object sender, EventArgs e)
        {
            UASUtility.UpdateUIDisplayByAuthority(mainForm.app, this);
        }
    }
}







