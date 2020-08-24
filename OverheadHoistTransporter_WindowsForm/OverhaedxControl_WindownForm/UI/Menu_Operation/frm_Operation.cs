//*********************************************************************************
//      frm_Operation.cs
//*********************************************************************************
// File Name: frm_Operation.cs
// Description: Operation form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/09/16           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.Common;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform;
using NLog;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public enum OperationType
    {
        SystemModeControl,
        TransferManagement,
        RoadControl
    }

    public partial class frm_Operation : Form
    {
        #region 公用參數設定
        OHxCMainForm mainForm = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool isClick_SystemModeControl = false;
        bool isClick_TransferManagement = false;
        bool isClick_RoadControl = false;
        OperationType _operation_type;
        #endregion 公用參數設定

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="_mainForm"></param>
        public frm_Operation(OHxCMainForm _mainForm)
        {
            try
            {
                InitializeComponent();
                mainForm = _mainForm;
                uc_SP_TransferManagement1.CloseFormEvent += uc_SubPages_CloseFormEvent;
                uc_SP_SystemModeControl1.CloseFormEvent += uc_SubPages_CloseFormEvent;
                uc_SP_PathControlList1.CloseFormEvent += uc_SubPages_CloseFormEvent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void frm_Operation_Load(object sender, EventArgs e)
        {
            try
            {
                UASUtility.UpdateUIDisplayByAuthority(mainForm.app, this);
                uc_SP_TransferManagement1.initUI();
                uc_SP_SystemModeControl1.initUI();
                uc_SP_PathControlList1.initUI();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //private void frm_Operation_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    try
        //    {
        //        //uc_SP_TransferManagement1.unregisterEvent();
        //        //uc_SP_SystemModeControl1.unregisterEvent();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //    }
        //}

        private void uc_SubPages_CloseFormEvent(object sender, EventArgs e)
        {
            try
            {
                uc_SP_TransferManagement1.unregisterEvent();
                uc_SP_SystemModeControl1.unregisterEvent();
                uc_SP_PathControlList1.unregisterEvent();
                mainForm.removeForm(typeof(frm_Operation).Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 指定開啟的查詢控件
        /// </summary>
        /// <param name="_operation_type"></param>
        public void setFormType(OperationType _operation_type)
        {
            try
            {
                this._operation_type = _operation_type;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_SystemModeControl_Click(object sender, EventArgs e)
        {
            try
            {
                SystemModeControl_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void SystemModeControl_Click()
        {
            try
            {
                lab_FormTitle.Text = "System Mode Control";
                uc_SP_SystemModeControl.Visible = true;
                uc_SP_TransferManagement.Visible = false;
                uc_SP_PathControlList.Visible = false;
                isClick_SystemModeControl = true;
                isClick_TransferManagement = false;
                isClick_RoadControl = false;
                resetTabLabelColor();
                setLabIsClickColor(lab_SystemModeControl);
                setTabPageMouseEnterAndLeaveColor(lab_SystemModeControl, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_TransferManagement_Click(object sender, EventArgs e)
        {
            try
            {
                TransferManagement_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void TransferManagement_Click()
        {
            try
            {
                lab_FormTitle.Text = "Transfer Management";
                uc_SP_SystemModeControl.Visible = false;
                uc_SP_TransferManagement.Visible = true;
                uc_SP_PathControlList.Visible = false;
                isClick_SystemModeControl = false;
                isClick_TransferManagement = true;
                isClick_RoadControl = false;
                resetTabLabelColor();
                setLabIsClickColor(lab_TransferManagement);
                setTabPageMouseEnterAndLeaveColor(lab_TransferManagement, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_SystemModeControl_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_SystemModeControl, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_SystemModeControl_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_SystemModeControl)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_SystemModeControl, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_TransferManagement_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_TransferManagement, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_TransferManagement_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_TransferManagement)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_TransferManagement, false);
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
                    lab_SystemModeControl.BackColor = Color.FromArgb(0, 51, 102);
                    lab_SystemModeControl.ForeColor = Color.White;
                    lab_TransferManagement.BackColor = Color.FromArgb(0, 51, 102);
                    lab_TransferManagement.ForeColor = Color.White;
                    lab_RoadControl.BackColor = Color.FromArgb(0, 51, 102);
                    lab_RoadControl.ForeColor = Color.White;

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

        public void switchTabControl(int fromtype)
        {
            try
            {
                switch (fromtype)
                {
                    case 0:
                        SystemModeControl_Click();
                        break;
                    case 1:
                        TransferManagement_Click();
                        break;
                    case 2:
                        RoadControl_Click();
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

        private void lab_RoadControl_Click(object sender, EventArgs e)
        {
            RoadControl_Click();
        }
        private void RoadControl_Click()
        {
            try
            {
                lab_FormTitle.Text = "Road Control";
                uc_SP_SystemModeControl.Visible = false;
                uc_SP_TransferManagement.Visible = false;
                uc_SP_PathControlList.Visible = true;

                isClick_SystemModeControl = false;
                isClick_TransferManagement = false;
                isClick_RoadControl = true;
                resetTabLabelColor();
                setLabIsClickColor(lab_RoadControl);
                setTabPageMouseEnterAndLeaveColor(lab_RoadControl, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void lab_RoadControl_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_RoadControl, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_RoadControl_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_RoadControl)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_RoadControl, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void frm_Operation_VisibleChanged(object sender, EventArgs e)
        {
            UASUtility.UpdateUIDisplayByAuthority(mainForm.app, this);
        }
    }
}