//*********************************************************************************
//      frm_Query.cs
//*********************************************************************************
// File Name: frm_Query.cs
// Description: Query form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/06/04           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform;
using NLog;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    public enum QueryType
    {
        McsCommandLog,
        CommunicationLog
    }

    public partial class frm_Query : Form
    {
        #region 公用參數設定
        OHxCMainForm mainForm = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool isClick_McsCommandLog = false;
        bool isClick_CommunicationLog = false;
        uc_McsCommandLog McsCommandLog = null;
        uc_CommunicationLog CommunicationLog = null;
        QueryType _qurey_type;
        #endregion 公用參數設定

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="_mainForm"></param>
        public frm_Query(OHxCMainForm _mainForm)
        {
            try
            {
                InitializeComponent();
                mainForm = _mainForm;
                uc_SP_MCSCommandLog1.SendCmdIDEvent += Uc_SP_MCSCommandLog1_SendCmdIDEvent;
                uc_SP_MCSCommandLog1.CloseFormEvent += uc_SubPages_CloseFormEvent;
                uc_SP_CommunicationLog1.CloseFormEvent += uc_SubPages_CloseFormEvent;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_SP_MCSCommandLog1_SendCmdIDEvent(object sender, EventArgs e)
        {
            try
            {
                lab_CommunicationLog_Click(null, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_SubPages_CloseFormEvent(object sender, EventArgs e)
        {
            try
            {
                mainForm.removeForm(typeof(frm_Query).Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Uc_McsCommandLog_SendCmdIDEvent(object sender, EventArgs e)
        {
            try
            {
                lab_CommunicationLog_Click(null, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 指定開啟的查詢控件
        /// </summary>
        /// <param name="_qurey_type"></param>
        public void setFormType(QueryType _qurey_type)
        {
            try
            {
                this._qurey_type = _qurey_type;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 介面載入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Query_Load(object sender, EventArgs e)
        {
            try
            {
                this.uc_SP_MCSCommandLog1.startupUI(this);
                this.uc_SP_CommunicationLog1.startupUI("", "");
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Exception");
            }
        }

        private void setInfo(Label setText, string sValue)
        {
            try
            {
                setText.Text = sValue;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_CommunicationLog_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_CommunicationLog, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_CommunicationLog_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_CommunicationLog)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_CommunicationLog, false);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_McsCommandLog_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                setTabPageMouseEnterAndLeaveColor(lab_McsCommandLog, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void lab_McsCommandLog_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (isClick_McsCommandLog)
                {
                    return;
                }
                else
                {
                    setTabPageMouseEnterAndLeaveColor(lab_McsCommandLog, false);
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

        private void lab_McsCommandLog_Click(object sender, EventArgs e)
        {
            try
            {
                McsCommandLog_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void McsCommandLog_Click()
        {
            try
            {
                lab_FormTitle.Text = "MCS Command Log";
                uc_SP_MCSCommandLog.Visible = true;
                uc_SP_CommunicationLog.Visible = false;
                lab_Descripstion.Text = "* Right-click on the item to link to Communication Log.";
                isClick_McsCommandLog = true;
                isClick_CommunicationLog = false;
                resetTabLabelColor();
                setLabIsClickColor(lab_McsCommandLog);
                setTabPageMouseEnterAndLeaveColor(lab_McsCommandLog, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void lab_CommunicationLog_Click(object sender, EventArgs e)
        {
            try
            {
                CommunicationLog_Click();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void CommunicationLog_Click()
        {
            try
            {
                lab_FormTitle.Text = "Communication Log";
                uc_SP_MCSCommandLog.Visible = false;
                uc_SP_CommunicationLog.Visible = true;
                lab_Descripstion.Text = "* Double-click on the item to show detail information.";
                isClick_McsCommandLog = false;
                isClick_CommunicationLog = true;
                resetTabLabelColor();
                setLabIsClickColor(this.lab_CommunicationLog);
                setTabPageMouseEnterAndLeaveColor(this.lab_CommunicationLog, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void _CommunicationLog_Click(string cmdid)
        {
            try
            {
                uc_SP_CommunicationLog1.startupUI(cmdid, "");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void _VhIDDetail_Click(string vhid)
        {
            try
            {
                uc_SP_CommunicationLog1.startupUI("", vhid);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        /// <summary>
        /// 設定所有isClick的flag為false;
        /// </summary>
        private void resetTabIsClick()
        {

            try
            {
                isClick_McsCommandLog = false;
                isClick_CommunicationLog = false;
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
                    lab_McsCommandLog.BackColor = Color.FromArgb(0, 51, 102);
                    lab_CommunicationLog.BackColor = Color.FromArgb(0, 51, 102);
                    lab_McsCommandLog.ForeColor = Color.White;
                    lab_CommunicationLog.ForeColor = Color.White;

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
                        McsCommandLog_Click();
                        break;
                    case 1:
                        CommunicationLog_Click();
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

        private void btn_Close_Button_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.removeForm(typeof(frm_Query).Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}







