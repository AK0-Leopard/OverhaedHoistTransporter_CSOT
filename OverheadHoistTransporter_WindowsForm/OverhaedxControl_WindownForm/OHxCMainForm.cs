//*****************************************************************************************
//      OHxCMainForm.cs
//*****************************************************************************************
// File Name: OHxCMainForm.cs
// Description: OHxC Main Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date            Author              Request No.      Tag        Description
// ------------   -----------------   -------------   ---------   --------------------------
// 2019/05/16      Kevin                N/A             N/A        Initial Release
// 2019/11/05      Boan                 N/A             A0.01      新增Try Catch。
// 2019/11/05      Mark                 N/A             A0.02      修改讀取資料錯誤的問題。
// 2019/11/20      Xenia                N/A             A0.03      新增關閉程式時需做權限管控。
//*****************************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.Common;
using com.mirle.ibg3k0.bc.winform.UI;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.UI.Menu_Help;
using com.mirle.ibg3k0.ohxc.winform.UI.Menu_System;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform
{
    public partial class OHxCMainForm : Form
    {
        public event EventHandler<bool> MonitorRoadContorlStatusChanged;

        #region 公用參數設定
        Dictionary<String, Form> openForms = new Dictionary<string, Form>();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private System.Windows.Forms.Timer uiTimer = null;
        private UserAccountMgt uasMainForm = null;
        bool isExpend = true;

        public WindownApplication app { get; private set; } = null;
        public event EventHandler ExtendFormEvent;
        private string sLineID = string.Empty;
        #endregion 公用參數設定

        #region 使用者登入登出屬性
        bool islogin;
        bool IsLogIn
        {
            get { return islogin; }
            set
            {
                islogin = value;
                if (value)
                {
                    loginToolStripMenuItem.Enabled = !islogin;
                    logoutToolStripMenuItem.Enabled = islogin;
                }
                else
                {
                    loginToolStripMenuItem.Enabled = !islogin;
                    logoutToolStripMenuItem.Enabled = islogin;
                }
            }
        }
        #endregion 使用者登入登出屬性

        //建構子
        public OHxCMainForm()
        {
            try
            {
                InitializeComponent();

                Adapter.Initialize();

                Application.AddMessageFilter(new GlobalMouseHandler());

                startTime = DateTime.Now;

                initUI(null, null);
                menuStrip1.Renderer = new MyRenderer();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void iniEvent()
        {
            try
            {
                ALINE line = app.ObjCacheManager.GetLine();
                line.LineStatusChange += OHxCMainForm_LineStatusChange;
                app.ObjCacheManager.LogInUserChanged += ObjCacheManager_LoginChanged;
                app.ObjCacheManager.MCSCommandUpdateComplete += ObjCacheManager_MCSCMDUpdateComplete;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void OHxCMainForm_LineStatusChange(object sender, EventArgs e)
        {
            try
            {
                sc.ALINE line = sender as sc.ALINE;
                Adapter.Invoke((obj) =>
                {
                    //uc_VehicleStatus1.RemoteCount = line.CurrntVehicleModeAutoRemoteCount.ToString();
                    //uc_VehicleStatus1.LocalCount = line.CurrntVehicleModeAutoLoaclCount.ToString();
                    //uc_VehicleStatus1.ErrorCount = line.CurrntVehicleStatusErrorCount.ToString();
                    //uc_VehicleStatus1.IdleCount = line.CurrntVehicleStatusIdelCount.ToString();
                    //uc_cst_transfer_status.TransferCount = line.CurrntCSTStatueTransferCount.ToString();
                    //uc_cst_transfer_status.WaitingCount = line.CurrntCSTStatueWaitingCount.ToString();
                    //uc_MCSQueueStatus1.AssignedCount = line.CurrntHostCommandTransferStatueAssignedCount.ToString();
                    //uc_MCSQueueStatus1.WaitingCount = line.CurrntHostCommandTransferStatueWaitingCounr.ToString();
                    uc_VehicleStatus1.AutoRemote = line.CurrntVehicleModeAutoRemoteCount.ToString();
                    uc_VehicleStatus1.AutoLocal = line.CurrntVehicleModeAutoLoaclCount.ToString();
                    uc_VehicleStatus1.Idle = line.CurrntVehicleStatusIdelCount.ToString();
                    uc_VehicleStatus1.Error = line.CurrntVehicleStatusErrorCount.ToString();
                    uc_CstStatus1.WaitingCount = line.CurrntCSTStatueWaitingCount.ToString();
                    uc_CstStatus1.TransferCount = line.CurrntCSTStatueTransferCount.ToString();     //A0.02
                    uc_McsQStatus1.WatingCount = line.CurrntHostCommandTransferStatueWaitingCounr.ToString();
                    uc_McsQStatus1.AssignedCount = line.CurrntHostCommandTransferStatueAssignedCount.ToString();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        //初始化UI
        private void initUI(object sender, EventArgs e)
        {
            try
            {
                //uc_LineID.Text = "OHTC01";
                //uc_PLCConnect_MTL.SetTitle("MTL");
                uc_HostConnect.SetTitle("Host");
                //uc_PLC_MTS.SetTitle("MTS");
                //uc_LineStatus.SetTitle("Line Status");
                //uc_PLC_HID.SetTitle("HID");
                uc_Alarm.SetTitle("Alarm");
                uc_VehicleStatus1.setTitleName("Auto R", "Auto L", "Idle", "Error", "Vehicle");
                uc_CstStatus1.setTitleName("Transfer", "Waiting", "CST");
                uc_McsQStatus1.setTitleName("Assigned", "Waiting Assigned", "MCS Queue");

                //設定系統資訊區-版號
                lab_Version_Value.Text = BCAppConstants.getMainFormVersion("");
                //設定系統資訊區-系統建立日期
                lab_BuildDate_Value.Text = BCAppConstants.GetBuildDateTime().ToString("yyyy-MM-dd hh:mm:ss");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        private void errorLogHandler(Object sender, LogEventArgs args)
        {
        }
        private void warnLogHandler(Object sender, LogEventArgs args)
        {
        }
        private void infoLogHandler(Object sender, LogEventArgs args)
        {
        }


        #region 更新系統運行時間
        DateTime startTime = new DateTime();
        private void changeRunningTime(Object myObject, EventArgs myEventArgs)
        {
            try
            {
                String m_minute;
                String m_hour;
                String m_day;

                DateTime now = DateTime.Now;

                TimeSpan diffSpan = now.Subtract(startTime);
                if (diffSpan.Minutes < 10)
                {
                    m_minute = "0" + diffSpan.Minutes.ToString();
                }
                else
                {
                    m_minute = diffSpan.Minutes.ToString();
                }

                if (diffSpan.Hours < 10)
                {
                    m_hour = "0" + diffSpan.Hours.ToString();
                }
                else
                {
                    m_hour = diffSpan.Hours.ToString();
                }

                if (diffSpan.Days < 10)
                {
                    m_day = "0" + diffSpan.Days.ToString();
                }
                else
                {
                    m_day = diffSpan.Days.ToString();
                }

                //tssl_Running_Time_Value.Text = m_day + "d " + m_hour + "h " + m_minute + "m";     //A0.08
                lab_RunningTime_Value.Text = m_day + "d " + m_hour + "h " + m_minute + "m";     //A0.08
            }
            catch (Exception ex)
            {
            }
        }
        #endregion 更新系統運行時間

        /// <summary>
        /// 開啟一般視窗 所使用
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="isPopUp"></param>     //ispopup using new form, others using MDIfrom. 
        /// <param name="forceConfirm"></param>   //?? Require user react the form at first.
        //public void openForm(String formID, Boolean isPopUp, Boolean forceConfirm)
        //public void openForm(String formID, Boolean isPopUp, Boolean forceConfirm)
        //{
        //    Form form;

        //    if (openForms.ContainsKey(formID))  //Form is Opened.
        //    {

        //        form = (Form)openForms[formID];
        //        if (isPopUp)
        //        {
        //            form.Activate();
        //            if (forceConfirm)
        //            {
        //                form.Close();
        //                if (form != null && !form.IsDisposed) { form.Dispose(); }   //Disposed form
        //                removeForm(formID);
        //                openForm(formID, isPopUp, forceConfirm);   // reopen the form
        //                return;
        //            }
        //            else
        //            {
        //                form.Show();
        //            }
        //            form.Focus();
        //        }
        //        else  //not popup form
        //        {
        //            //if ((form is frm_Query))
        //            //{
        //            //    (form as frm_Query).setFormType((QueryType)arg);
        //            //}

        //            //if ((form is frm_Maintenance))
        //            //{
        //            //    (form as frm_Maintenance).setFormType((FormType)arg);
        //            //}

        //            //if ((form is frm_Operation))
        //            //{
        //            //    (form as frm_Operation).setFormType((OperationType)arg);
        //            //}

        //            form.Activate();
        //            form.Show();
        //            form.Focus();
        //            form.AutoScroll = true;
        //            //form.WindowState = FormWindowState.Normal;
        //            form.WindowState = FormWindowState.Maximized;
        //        }

        //        if (form.MdiParent != null)
        //        {
        //            form.MdiParent.Refresh();
        //        }
        //    }
        //    else   //Form not Opened.
        //    {
        //        try
        //        {
        //            Type t = Type.GetType(String.Format("com.mirle.ibg3k0.bc.winform.UI.{0}", formID));
        //            Object[] args = { this };
        //            form = (Form)Activator.CreateInstance(t, args);
        //            openForms.Add(formID, form);
        //            if (isPopUp)
        //            {
        //                if (forceConfirm)
        //                {
        //                    form.ShowDialog();
        //                }
        //                else
        //                {
        //                    form.Show();
        //                }
        //                form.Focus();
        //            }
        //            else
        //            {
        //                if (!form.IsMdiContainer)
        //                {
        //                    form.MdiParent = this;
        //                }

        //                form.Visible = false;
        //                form.Show();
        //                form.Focus();
        //                form.AutoScroll = true;
        //                form.WindowState = FormWindowState.Normal;
        //                form.WindowState = FormWindowState.Maximized;
        //                form.Visible = true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error("BCMainForm.cs.cs has Error[Error method:{0}],[Error Message:{1}",
        //            "openForm", ex.ToString());
        //            //TipMessage_Type.Show("This fuction is not enable", BCAppConstants.WARN_MSG);

        //        }
        //    }
        //}


        public class TypeSwitch
        {
            Dictionary<Type, Action<object, bool>> matches = new Dictionary<Type, Action<object, bool>>();
            public TypeSwitch Case<T>(Action<T, bool> action) { matches.Add(typeof(T), (x, enabled) => action((T)x, enabled)); return this; }
            public void Switch(object x, bool enabled)
            {
                if (x == null)
                {
                }
                else
                {
                    if (matches.ContainsKey(x.GetType()))
                        matches[x.GetType()](x, enabled);
                    else
                        logger.Warn("Switch Type:[{0}], Not exist!!!", x.GetType().Name);
                }
            }
        }

        /// <summary>
        /// 開啟一般視窗 所使用
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="isPopUp"></param>     //ispopup using new form, others using MDIfrom. 
        /// <param name="forceConfirm"></param>   //?? Require user react the form at first.
        public void openForm(String formID, Boolean isPopUp, Boolean forceConfirm)
        {
            Form form;

            if (openForms.ContainsKey(formID))  //Form is Opened.
            {

                form = (Form)openForms[formID];
                if (isPopUp)
                {
                    form.Activate();
                    if (forceConfirm)
                    {
                        form.Close();
                        if (form != null && !form.IsDisposed) { form.Dispose(); }   //Disposed form
                        removeForm(formID);
                        openForm(formID, isPopUp, forceConfirm);   // reopen the form
                        return;
                    }
                    else
                    {
                        form.Show();
                    }
                    form.Focus();
                }
                else  //not popup form
                {
                    //if ((form is frm_Query))
                    //{
                    //    (form as frm_Query).setFormType((QueryType)arg);
                    //}

                    //if ((form is frm_Maintenance))
                    //{
                    //    (form as frm_Maintenance).setFormType((FormType)arg);
                    //}

                    //if ((form is frm_Operation))
                    //{
                    //    (form as frm_Operation).setFormType((OperationType)arg);
                    //}

                    form.Activate();
                    form.Show();
                    form.Focus();
                    form.AutoScroll = true;
                    //form.WindowState = FormWindowState.Normal;
                    form.WindowState = FormWindowState.Maximized;
                }

                if (form.MdiParent != null)
                {
                    form.MdiParent.Refresh();
                }
            }
            else   //Form not Opened.
            {
                try
                {
                    Type t = Type.GetType(String.Format("com.mirle.ibg3k0.bc.winform.UI.{0}", formID));
                    Object[] args = { this };
                    form = (Form)Activator.CreateInstance(t, args);
                    openForms.Add(formID, form);
                    if (isPopUp)
                    {
                        if (forceConfirm)
                        {
                            form.ShowDialog();
                        }
                        else
                        {
                            form.Show();
                        }
                        form.Focus();
                    }
                    else
                    {
                        if (!form.IsMdiContainer)
                        {
                            form.MdiParent = this;
                        }

                        form.Visible = false;
                        form.Show();
                        form.Focus();
                        form.AutoScroll = true;
                        form.WindowState = FormWindowState.Normal;
                        form.WindowState = FormWindowState.Maximized;
                        form.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("BCMainForm.cs.cs has Error[Error method:{0}],[Error Message:{1}",
                    "openForm", ex.ToString());
                    TipMessage_Type.Show("This fuction is not enable", BCAppConstants.WARN_MSG);

                }
            }
        }

        public void openForm(String formID, Boolean isPopUp, Boolean forceConfirm, object arg = null)
        {
            Form form;
            int fromtype = 0;
            if (openForms.ContainsKey(formID))  //Form is Opened.
            {
                form = (Form)openForms[formID];
                if (isPopUp)
                {
                    form.Activate();
                    if (forceConfirm)
                    {
                        form.Close();
                        if (form != null && !form.IsDisposed) { form.Dispose(); }   //Disposed form
                        removeForm(formID);
                        openForm(formID, isPopUp, forceConfirm);   // reopen the form
                        return;
                    }
                    else
                    {
                        form.Show();
                    }
                    form.Focus();
                }
                else  //not popup form
                {
                    if ((form is frm_Operation))
                    {
                        (form as frm_Operation).setFormType((OperationType)arg);

                        if (SCUtility.isMatche(arg.ToString(), OperationType.SystemModeControl))      //A0.10
                        {
                            fromtype = 0;
                            MonitorRoadContorlStatusChanged?.Invoke(this, true);
                        }
                        else if (SCUtility.isMatche(arg.ToString(), OperationType.TransferManagement))
                        {
                            fromtype = 1;
                            MonitorRoadContorlStatusChanged?.Invoke(this, true);
                        }
                        else if (SCUtility.isMatche(arg.ToString(), OperationType.RoadControl))
                        {
                            fromtype = 2;
                            MonitorRoadContorlStatusChanged?.Invoke(this, true);
                        }
                        ((frm_Operation)form).switchTabControl(fromtype);
                    }
                    if ((form is frm_Query))
                    {
                        (form as frm_Query).setFormType((QueryType)arg);

                        if (SCUtility.isMatche(arg.ToString(), QueryType.McsCommandLog))      //A0.10
                        {
                            fromtype = 0;
                        }
                        else if (SCUtility.isMatche(arg.ToString(), QueryType.CommunicationLog))
                        {
                            fromtype = 1;
                        }
                    ((frm_Query)form).switchTabControl(fromtype);
                    }
                    if ((form is frm_Maintenance))
                    {
                        (form as frm_Maintenance).setFormType((MaintenanceType)arg);

                        if (SCUtility.isMatche(arg.ToString(), MaintenanceType.MTLMTSMaint))      //A0.10
                        {
                            fromtype = 0;
                        }
                        else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.PortMaint))
                        {
                            fromtype = 1;
                        }
                        else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.VehicleManagement))
                        {
                            fromtype = 2;
                        }
                        else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.AdvancedSettings))
                        {
                            fromtype = 3;
                        }
                        else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.AlarmMaintenance))
                        {
                            fromtype = 4;
                        }
                        ((frm_Maintenance)form).switchTabControl(fromtype);
                    }
                    form.Activate();
                    form.Show();
                    form.Focus();
                    form.AutoScroll = true;
                    //form.WindowState = FormWindowState.Normal;
                    form.WindowState = FormWindowState.Maximized;
                }
                if (form.MdiParent != null)
                {
                    form.MdiParent.Refresh();
                }
            }
            else   //Form not Opened.
            {
                try
                {
                    Type t = Type.GetType(String.Format("com.mirle.ibg3k0.bc.winform.UI.{0}", formID));
                    // Object[] args = { this };
                    if (arg == null)
                    {
                        form = (Form)Activator.CreateInstance(t, this);
                    }
                    else
                    {
                        form = (Form)Activator.CreateInstance(t, this);
                    }
                    openForms.Add(formID, form);
                    if (isPopUp)
                    {
                        if (forceConfirm)
                        {
                            form.ShowDialog();
                        }
                        else
                        {
                            form.Show();
                        }
                        form.Focus();
                    }
                    else
                    {
                        if (!form.IsMdiContainer)
                        {
                            form.MdiParent = this;
                        }

                        if ((form is frm_Operation))
                        {
                            (form as frm_Operation).setFormType((OperationType)arg);

                            if (SCUtility.isMatche(arg.ToString(), OperationType.SystemModeControl))      //A0.10
                            {
                                fromtype = 0;
                                MonitorRoadContorlStatusChanged?.Invoke(this, true);
                            }
                            else if (SCUtility.isMatche(arg.ToString(), OperationType.TransferManagement))
                            {
                                fromtype = 1;
                                MonitorRoadContorlStatusChanged?.Invoke(this, true);

                            }
                            else if (SCUtility.isMatche(arg.ToString(), OperationType.RoadControl))
                            {
                                fromtype = 2;
                                MonitorRoadContorlStatusChanged?.Invoke(this, true);
                            }
                            ((frm_Operation)form).switchTabControl(fromtype);
                        }
                        if ((form is frm_Query))
                        {
                            (form as frm_Query).setFormType((QueryType)arg);

                            if (SCUtility.isMatche(arg.ToString(), QueryType.McsCommandLog))      //A0.10
                            {
                                fromtype = 0;
                            }
                            else if (SCUtility.isMatche(arg.ToString(), QueryType.CommunicationLog))
                            {
                                fromtype = 1;
                            }
                            ((frm_Query)form).switchTabControl(fromtype);
                        }
                        if ((form is frm_Maintenance))
                        {
                            (form as frm_Maintenance).setFormType((MaintenanceType)arg);

                            if (SCUtility.isMatche(arg.ToString(), MaintenanceType.MTLMTSMaint))      //A0.10
                            {
                                fromtype = 0;
                            }
                            else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.PortMaint))
                            {
                                fromtype = 1;
                            }
                            else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.VehicleManagement))
                            {
                                fromtype = 2;
                            }
                            else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.AdvancedSettings))
                            {
                                fromtype = 3;
                            }
                            else if (SCUtility.isMatche(arg.ToString(), MaintenanceType.AlarmMaintenance))
                            {
                                fromtype = 4;
                            }
                            ((frm_Maintenance)form).switchTabControl(fromtype);
                        }
                        form.Visible = false;
                        form.Show();
                        form.Focus();
                        form.AutoScroll = true;
                        //form.WindowState = FormWindowState.Normal;
                        form.WindowState = FormWindowState.Maximized;
                        form.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("BCMainForm.cs.cs has Error[Error method:{0}],[Error Message:{1}",
                    "openForm", ex.ToString());
                    //TipMessage_Type.Show("This fuction is not enable", BCAppConstants.WARN_MSG);

                }
            }
        }

        public void removeForm(String formID)
        {
            try
            {
                if (openForms.ContainsKey(formID))
                {
                    if ((openForms[formID] is frm_Operation))
                    {
                        MonitorRoadContorlStatusChanged?.Invoke(this, false);
                    }
                    openForms[formID].Dispose();
                    openForms.Remove(formID);

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public string getMessageString(string key, params object[] args)
        {
            return SCApplication.getMessageString(key, args);
        }

        private void OHxCMainForm_FormClosing(object sender, FormClosingEventArgs e) //A0.03
        {
            try
            {
                #region 1.初步詢問是否要關閉MPC
                var confirmResult = TipMessage_Request_Light.Show("Are you sure to close the system now?");
                if (confirmResult != DialogResult.Yes)
                {
                    e.Cancel = true;    //取消關閉
                    return;
                }
                #endregion

                #region 2.判斷是否登入
                if (!UASUtility.isLogin(app))
                {
                    if (!UASUtility.doLogin(this, app, BCAppConstants.System_Function.FUNC_CLOSE_SYSTEM, true, UASUtility.LoginType.LogIn))
                    {
                        e.Cancel = true;
                        TipMessage_Type_Light.Show("", "Close OHTC system failed !!", BCAppConstants.WARN_MSG);
                        return;
                    }
                    TipMessage_Type_Light.Show("Close System, Authority Check...", "Success !!", BCAppConstants.INFO_MSG);
                }
                else
                {
                    if (!UASUtility.doLogin(this, app, BCAppConstants.System_Function.FUNC_CLOSE_SYSTEM, true, UASUtility.LoginType.ExitCheck))
                    {
                        e.Cancel = true;
                        TipMessage_Type_Light.Show("Close System, Authority Check...", "Failed !!", BCAppConstants.WARN_MSG);
                        return;
                    }
                    TipMessage_Type_Light.Show("Close System, Authority Check...", "Success !!", BCAppConstants.INFO_MSG);
                }
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void OHxCMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                app.GetNatsManager()?.close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private async void OHxCMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(() => app = WindownApplication.getInstance());

                //oHx_Form = new OHxC_Form(this);
                //oHx_Form.MdiParent = this;
                //oHx_Form.Show();
                //oHx_Form.Focus();
                //oHx_Form.AutoScroll = true;
                //oHx_Form.WindowState = FormWindowState.Maximized;
                openForm(typeof(OHxC_Form).Name, false, true);

                //app.addRefreshUIDisplayFun(delegate (object o) { Adapter.Invoke(new SendOrPostCallback((o1) => { refreshAuthority(); }), null); });

                uiTimer = new System.Windows.Forms.Timer();
                uiTimer.Interval = 60000;
                uiTimer.Tick += new EventHandler(changeRunningTime);
                uiTimer.Tick += new EventHandler(refresh_CMDCount);
                uiTimer.Start();

                OHxCMainForm_LineStatusChange(app.ObjCacheManager.GetLine(), null);
                lab_hourlyProcess_Value.Text = app.ObjCacheManager.getHourlyCMDCount().ToString();
                lab_TodayProcess_Value.Text = app.ObjCacheManager.getTodayCMDCount().ToString();
                iniEvent();
                setLineStatus();
                app.login("");
                LoginPopupForm login = new LoginPopupForm();
                //login.StartPosition = FormStartPosition.CenterScreen;
                login.ShowDialog();
                //Form frm = openForms[typeof(OHxC_Form).Name];
                //((OHxC_Form)frm).setTabButtonActivate(UASUtility.isLogin(app));
                //app.addRefreshUIDisplayFun(this.Name, delegate (object o) { UASUtility.UpdateUIDisplayByAuthority(app, this); });
                app.addRefreshUIDisplayFun(delegate (object o) { Adapter.Invoke(new SendOrPostCallback((o1) => { refreshAuthority(); }), null); });
                UASUtility.UpdateUIDisplayByAuthority(app, this);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        object lineStatLock = new object();
        private void setLineStatus()
        {
            try
            {
                string subject = BCAppConstants.NATSTopics.NATS_SUBJECT_LINE_INFO;

                //指定要執行的動作
                EventHandler<StanMsgHandlerArgs> msgHandler = (senders, args) =>
                {
                    lock (lineStatLock)
                    {
                        byte[] arrayByte = args.Message.Data;
                        if (arrayByte == null)
                            return;

                        //反序列化  saveLineInfo
                        Google.Protobuf.MessageParser<LINE_INFO> parser = new Google.Protobuf.MessageParser<LINE_INFO>(() => new LINE_INFO());
                        LINE_INFO lineInfo = parser.ParseFrom(arrayByte);

                        if (SCUtility.isEmpty(sLineID))
                        {
                            sLineID = lineInfo.LineID;
                            //更新Line ID
                            Adapter.Invoke(new SendOrPostCallback((o1) =>
                                {
                                    uc_LineID.Text = lineInfo.LineID;
                                }), null);
                        }

                        //更新Host Connection
                        bool bHost = false;
                        if (lineInfo.Host == LinkStatus.LinkOk)
                        {
                            bHost = true;
                        }

                        Adapter.Invoke(new SendOrPostCallback((o1) =>
                        {
                            uc_HostConnect.SetHostConnStatus("Host", bHost);

                            uc_CstStatus1.WaitingCount = lineInfo.CurrntCSTStatueWaitingCount.ToString();
                            uc_CstStatus1.TransferCount = lineInfo.CurrntHostCommandTransferStatueWaitingCounr.ToString();

                            uc_McsQStatus1.WatingCount = lineInfo.CurrntHostCommandTransferStatueWaitingCounr.ToString();
                            uc_McsQStatus1.AssignedCount = lineInfo.CurrntHostCommandTransferStatueAssignedCount.ToString();

                            uc_VehicleStatus1.AutoRemote = lineInfo.CurrntVehicleModeAutoRemoteCount.ToString();
                            uc_VehicleStatus1.AutoLocal = lineInfo.CurrntVehicleModeAutoLoaclCount.ToString();
                            uc_VehicleStatus1.Idle = lineInfo.CurrntVehicleStatusIdelCount.ToString();
                            uc_VehicleStatus1.Error = lineInfo.CurrntVehicleStatusErrorCount.ToString();

                            uc_Alarm.SetAlarmStatus(lineInfo.AlarmHappen);
                        }), null);
                    }
                };

                //訂閱
                app.GetNatsManager().Subscriber(subject, msgHandler, false, true, 0, null);       //當subject有變化，則進行msgHandler的動作
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //去除子畫面白框的問題
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                var mdiclient = this.Controls.OfType<MdiClient>().Single();
                this.SuspendLayout();
                mdiclient.SuspendLayout();
                var hdiff = mdiclient.Size.Width - mdiclient.ClientSize.Width;
                var vdiff = mdiclient.Size.Height - mdiclient.ClientSize.Height;
                var size = new Size(mdiclient.Width + hdiff, mdiclient.Height + vdiff);
                var location = new Point(mdiclient.Left - (hdiff / 2), mdiclient.Top - (vdiff / 2));
                mdiclient.Dock = DockStyle.None;
                mdiclient.Size = size;
                mdiclient.Location = location;
                mdiclient.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                mdiclient.ResumeLayout(true);
                this.ResumeLayout(true);
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_MCSCMDUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                refresh_CMDCount();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void refresh_CMDCount(Object myObject, EventArgs myEventArgs)
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    lab_hourlyProcess_Value.Text = app.ObjCacheManager.getHourlyCMDCount().ToString();
                    lab_TodayProcess_Value.Text = app.ObjCacheManager.getTodayCMDCount().ToString();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void refresh_CMDCount()
        {
            try
            {
                Adapter.Invoke((obj) =>
                {
                    lab_hourlyProcess_Value.Text = app.ObjCacheManager.getHourlyCMDCount().ToString();
                    lab_TodayProcess_Value.Text = app.ObjCacheManager.getTodayCMDCount().ToString();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_LoginChanged(object sender, EventArgs e)
        {
            try
            {
                refresh_LogInUserInfo();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void refresh_LogInUserInfo()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(app.LoginUserID))
                {
                    lab_LoginUserValue.Text = app.LoginUserID;
                }
                else
                {
                    lab_LoginUserValue.Text = "Login";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void refreshAuthority()
        {
            try
            {
                UASUtility.UpdateUIDisplayByAuthority(this.app, this);
                IsLogIn = !BCFUtility.isEmpty(app.LoginUserID);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //MenuBar_System
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItemClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                lock (openForms)
                {
                    //MenuBar_System
                    if (sender.Equals(loginToolStripMenuItem))
                    {
                        LoginPopupForm login = new LoginPopupForm();
                        login.ShowDialog();
                    }
                    else if (sender.Equals(pic_Login))
                    {
                        if (UASUtility.isLogin(app) == true)
                        {
                            ToolStripMenuItemClick(logoutToolStripMenuItem, e);
                        }
                        else
                        {
                            ToolStripMenuItemClick(loginToolStripMenuItem, e);
                        }
                    }
                    else if (sender.Equals(logoutToolStripMenuItem))
                    {
                        try
                        {
                            lock (openForms)
                            {
                                var confirmResult = TipMessage_Request_Light.Show("Are you sure to log out now?");
                                if (confirmResult != DialogResult.Yes)
                                {
                                    return;
                                }
                                else
                                {
                                    UASUtility.doLogout(app);
                                    closeAllOpenForm();
                                    closeUasMainForm();
                                    Refresh();
                                    TipMessage_Type_Light_woBtn.Show("", "Logout Sucessful.", BCAppConstants.INFO_MSG);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Exception");
                        }
                    }
                    else if (sender.Equals(exitToolStripMenuItem))
                    {
                        this.Close();
                    }
                    else if (sender.Equals(passwordChangeToolStripMenuItem))
                    {
                        if (UASUtility.isLogin(app))
                        {
                            ChangePwdForm changepwdFrom = new ChangePwdForm(this);
                            changepwdFrom.ShowDialog();
                        }
                        else
                        {
                            ToolStripMenuItemClick(loginToolStripMenuItem, e);
                            if (UASUtility.isLogin(app))
                            {
                                ChangePwdForm changepwdFrom1 = new ChangePwdForm(this);
                                changepwdFrom1.ShowDialog();
                            }
                        }
                    }
                    else if (sender.Equals(accountManagementToolStripMenuItem))
                    {
                        UserAccountMgt userAccountMgt = new UserAccountMgt();
                        userAccountMgt.ShowDialog();
                    }
                    //MenuBar_Operation
                    else if (sender.Equals(systemModeControlToolStripMenuItem))
                    {
                        openForm(typeof(frm_Operation).Name, false, false, OperationType.SystemModeControl);
                    }
                    else if (sender.Equals(transferManagementToolStripMenuItem))
                    {
                        openForm(typeof(frm_Operation).Name, false, false, OperationType.TransferManagement);
                    }
                    else if (sender.Equals(roadControlToolStripMenuItem))
                    {
                        openForm(typeof(frm_Operation).Name, false, false, OperationType.RoadControl);
                    }
                    //MenuBar_Query
                    else if (sender.Equals(mCSCommandToolStripMenuItem))
                    {
                        openForm(typeof(frm_Query).Name, false, false, QueryType.McsCommandLog);
                    }
                    else if (sender.Equals(communicationLogToolStripMenuItem))
                    {
                        openForm(typeof(frm_Query).Name, false, false, QueryType.CommunicationLog);
                    }
                    //MenuBar_Maint
                    else if (sender.Equals(vehicleManagementToolStripMenuItem))
                    {
                        openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.VehicleManagement);
                    }
                    else if (sender.Equals(portMaintenanceToolStripMenuItem))
                    {
                        openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.PortMaint);
                    }
                    else if (sender.Equals(mTLMTSMaintenanceToolStripMenuItem))
                    {
                        openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.MTLMTSMaint);
                    }
                    else if (sender.Equals(advancedSettingsToolStripMenuItem))
                    {
                        openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.AdvancedSettings);
                    }
                    else if (sender.Equals(alarmMaintenanceToolStripMenuItem))
                    {
                        openForm(typeof(frm_Maintenance).Name, false, false, MaintenanceType.AlarmMaintenance);
                    }
                    //MenuBar_Help
                    else if (sender.Equals(aboutToolStripMenuItem))
                    {
                        AboutPopupForm about = new AboutPopupForm();
                        about.ShowDialog();
                    }
                    else if (sender.Equals(sPECLinkToolStripMenuItem))
                    {
                        string s1 = Environment.CurrentDirectory + @"\Doc\";
                        string keyWord = string.Empty;
                        keyWord = "*Communication*";

                        ReadSPECFile(s1, keyWord);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void pic_Home_Click(object sender, EventArgs e)
        {
            try
            {
                openForm(typeof(OHxC_Form).Name, false, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void pic_Expander_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<String, Form> openformsTemp = new Dictionary<string, Form>(openForms);
                foreach (KeyValuePair<string, Form> item in openformsTemp)
                {
                    if (SCUtility.isMatche(item.Key, "OHxC_Form"))
                    {
                        //OHxC_Form ohxcForm = null;
                        OHxC_Form ohxcForm = item.Value as OHxC_Form;
                        if (ohxcForm != null)
                        {
                            if (isExpend)
                            {
                                ohxcForm.showAndHideTabPanel(false);
                                isExpend = false;
                            }
                            else
                            {
                                ohxcForm.showAndHideTabPanel(true);
                                isExpend = true;
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private static void ReadSPECFile(string s1, string fileKeyWord)
        {
            try
            {
                string[] files = null;
                files = System.IO.Directory.GetFiles(s1, fileKeyWord);

                foreach (string s in files)
                {
                    // Create the FileInfo object only when needed to ensure
                    // the information is as current as possible.
                    System.IO.FileInfo fi = null;
                    try
                    {
                        fi = new System.IO.FileInfo(s);
                    }
                    catch (System.IO.FileNotFoundException ex)
                    {
                        // To inform the user and continue is
                        // sufficient for this demonstration.
                        // Your application may require different behavior.
                        //Console.WriteLine(ex.Message);
                        continue;
                    }
                    //Console.WriteLine("{0} : {1}", fi.Name, fi.Directory);
                    //fi.op
                    System.Diagnostics.Process.Start(fi.FullName);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void closeAllOpenForm()
        {
            try
            {
                lock (openForms)
                {
                    Dictionary<String, Form> openformsTemp = new Dictionary<string, Form>(openForms);
                    foreach (KeyValuePair<string, Form> item in openformsTemp)
                    {
                        if (SCUtility.isMatche(item.Key, "OHxC_Form"))
                        {
                            item.Value.Visible = true;
                            continue;
                        }
                        item.Value.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void closeUasMainForm()
        {
            try
            {
                if (uasMainForm != null && !uasMainForm.IsDisposed)
                {
                    uasMainForm.Close();
                    uasMainForm.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void setInfo(Label DisplayUserID, Color DisplayUserIDColor, string LoginUserID)
        {
            try
            {
                Adapter.Invoke(new SendOrPostCallback((o1) =>
                {
                    DisplayUserID.Text = LoginUserID;
                    DisplayUserID.ForeColor = DisplayUserIDColor;
                }), null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void alarmMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItemClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void reserveInfoMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm(typeof(ReserveSectionInfoForm).Name, true, false);
        }
    }

    //變更系統菜單介面風格
    class MyRenderer : ToolStripProfessionalRenderer
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            try
            {
                //宣告1個矩形物件，並定義起始位置與大小
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                //Color c = e.Item.Selected ? Color.FromArgb(103, 173, 249) : Color.FromArgb(28, 132, 247);

                ToolStripItem item = e.Item;
                ToolStrip toolstrip = e.ToolStrip;

                if (toolstrip is MenuStrip)
                {
                    Color c = e.Item.Selected ? BCAppConstants.RGBColor.Menu_SelectedColor : BCAppConstants.RGBColor.Menu_BackColor;
                    //使用筆刷來填滿矩形
                    using (SolidBrush brush = new SolidBrush(c))
                        e.Graphics.FillRectangle(brush, rc);
                }
                else if (toolstrip is ToolStripDropDown)
                {
                    Color c;
                    //Color c = e.Item.Selected ? BCAppConstants.RGBColor.SubMenu_SelectedColor : BCAppConstants.RGBColor.SubMenu_BackColor;

                    if (e.Item.Enabled)
                    {
                        if (e.Item.Selected)
                        {
                            c = BCAppConstants.RGBColor.SubMenu_SelectedColor;
                        }
                        else
                        {
                            c = BCAppConstants.RGBColor.SubMenu_BackColor;
                        }
                    }
                    else
                    {
                        c = Color.White;
                    }


                    using (SolidBrush brush = new SolidBrush(c))
                        e.Graphics.FillRectangle(brush, rc);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        //變更Menu子項目文字顏色 (不變更Menu父項目文字顏色)
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            try
            {
                var toolstrip = e.Item;
                if (toolstrip is ToolStripMenuItem)
                {
                    if ((toolstrip as ToolStripMenuItem).Owner is MenuStrip)
                    {
                    }
                    else
                    {
                        if (toolstrip.Selected)
                        {
                            toolstrip.ForeColor = Color.White;
                        }
                        else
                        {
                            toolstrip.ForeColor = Color.FromArgb(27, 35, 56);
                        }
                    }
                }
                base.OnRenderItemText(e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }

    public delegate void MouseMovedEvent();

    public class GlobalMouseHandler : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;
        private System.Drawing.Point previousMousePosition = new System.Drawing.Point();
        public static event EventHandler<MouseEventArgs> MouseMovedEvent = delegate { };

        #region IMessageFilter Members

        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                System.Drawing.Point currentMousePoint = Control.MousePosition;
                if (previousMousePosition != currentMousePoint)
                {
                    previousMousePosition = currentMousePoint;
                    MouseMovedEvent(this, new MouseEventArgs(MouseButtons.None, 0, currentMousePoint.X, currentMousePoint.Y, 0));
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }

}