//*********************************************************************************
//      uc_MTLMTSMaint.cs
//*********************************************************************************
// File Name: uc_MTLMTSMaint.cs
// Description: uc_MTLMTSMaint Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date            Author       Request No.    Tag     Description
// ------------ -------------  -------------  ------  -----------------------------
// 2019/06/13    Xenia Tseng       N/A         N/A     Initial Release
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.Service;
using com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl;
using com.mirle.ibg3k0.sc.Common;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public partial class uc_MTLMTSMaint : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Form form = null;
        //Color mouse_enter_DGVcells = Color.FromArgb(0, 91, 168);
        bool isClick_lab_MTLMTS1 = false;
        bool isClick_lab_MTS2 = false;
        bool isClick_btn_AutoL = false;
        //bool isClick_btn_AutoR = false;
        #endregion 公用參數設定

        public event EventHandler CloseFormEvent;

        public uc_MTLMTSMaint()
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

        private void uc_MTLMTSMaint_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public override string Text
        {
            get { return "MTL/MTS Maintenance"; }
        }

        public void startupUI()
        {
            try
            {
                this.Width = 1711;
                this.Height = 754;
                MTLMTS1_Click();
                cb_VhID.MouseWheel += new MouseEventHandler(cb_VhID_MouseWheel);
                cb_MvToStation.MouseWheel += new MouseEventHandler(cb_MvToStation_MouseWheel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        void cb_VhID_MouseWheel(object sender, MouseEventArgs e)
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

        void cb_MvToStation_MouseWheel(object sender, MouseEventArgs e)
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

        private void btn_Close_Button_Click(object sender, EventArgs e)
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

        private void btn_Cmd_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SCUtility.isMatche(cb_MvToStation.Text, ""))
                {
                    TipMessage_Type_Light.Show("", "Please select Station.", BCAppConstants.INFO_MSG);
                    return;
                }
                else
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Station Mode ?");

                    if (confirmResult != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("Successfully Command", "Successfully command to vehicle.", BCAppConstants.INFO_MSG);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_CancelCmd_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = TipMessage_Request_Light.Show("Cancel the vehicle command?");
                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    TipMessage_Type_Light.Show("Successfully Cancel Command", "Successfully cancel the vehicle command.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_AutoL_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SCUtility.isMatche(cb_VhID.Text, ""))
                {
                    TipMessage_Type_Light.Show("", "Please select Vehicle ID.", BCAppConstants.INFO_MSG);
                    return;
                }
                else
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Vehicle Mode ?");

                    if (confirmResult != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("Successfully Set Auto Local", "Successfully the vehicle was set auto local.", BCAppConstants.INFO_MSG);
                        btn_AutoL.Enabled = false;
                        btn_AutoR.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_AutoR_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (SCUtility.isMatche(cb_VhID.Text, ""))
                {
                    TipMessage_Type_Light.Show("", "Please select Vehicle ID.", BCAppConstants.INFO_MSG);
                    return;
                }
                else
                {
                    var confirmResult = TipMessage_Request_Light.Show("Are you sure to change Vehicle Mode ?");

                    if (confirmResult != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        TipMessage_Type_Light.Show("Successfully Set Auto Remote", "Successfully the vehicle was set auto remote.", BCAppConstants.INFO_MSG);
                        btn_AutoL.Enabled = true;
                        btn_AutoR.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTLMTS1_Click(object sender, EventArgs e)
        {
            try
            {
                MTLMTS1_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MTLMTS1_Click()
        {
            try
            {
                uc_MTLMTS1Status.Visible = true;
                uc_MTS2Status.Visible = false;
                isClick_lab_MTLMTS1 = true;
                isClick_lab_MTS2 = false;
                resetTabLabelColor();
                setLabIsClickColor(lab_MTLMTS1);
                setTabPageMouseEnterAndLeaveColor(lab_MTLMTS1, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTS2_Click(object sender, EventArgs e)
        {
            try
            {
                MTS2_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void MTS2_Click()
        {
            try
            {
                uc_MTLMTS1Status.Visible = false;
                uc_MTS2Status.Visible = true;
                isClick_lab_MTLMTS1 = false;
                isClick_lab_MTS2 = true;
                resetTabLabelColor();
                setLabIsClickColor(lab_MTS2);
                setTabPageMouseEnterAndLeaveColor(lab_MTS2, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void lab_MTLMTS1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_MTLMTS1, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTLMTS1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_lab_MTLMTS1)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_MTLMTS1, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTS2_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_MTS2, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_MTS2_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_lab_MTS2)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_MTS2, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        // <summary>
        // 設定標籤在滑鼠進入與離開時的顏色
        // </summary>
        private void setTabPageMouseEnterAndLeaveColor(Label _lab, bool _mouseIsEnter)
        {
            try
            {
                if (_mouseIsEnter)
                {
                    _lab.BackColor = Color.FromArgb(0, 91, 168);
                    _lab.ForeColor = Color.White;
                }
                else
                {
                    _lab.BackColor = Color.White;
                    _lab.ForeColor = Color.FromArgb(27, 35, 56);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        // <summary>
        // 設定標籤點選或選擇後的顏色
        // </summary>
        private void setLabIsClickColor(Label _lab)
        {
            try
            {
                Adapter.Invoke(new SendOrPostCallback((o1) =>
                {
                    _lab.BackColor = Color.FromArgb(0, 91, 168);
                    _lab.ForeColor = Color.White;
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        // <summary>
        // 恢復標籤顏色
        // </summary>
        private void resetTabLabelColor()
        {
            try
            {
                Adapter.Invoke(new SendOrPostCallback((o1) =>
                {
                    lab_MTLMTS1.BackColor = Color.White;
                    lab_MTLMTS1.ForeColor = Color.FromArgb(27, 35, 56);
                    lab_MTS2.BackColor = Color.White;
                    lab_MTS2.ForeColor = Color.FromArgb(27, 35, 56);

                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void btn_Cmd_Load(object sender, EventArgs e)
        {

        }
    }
}
