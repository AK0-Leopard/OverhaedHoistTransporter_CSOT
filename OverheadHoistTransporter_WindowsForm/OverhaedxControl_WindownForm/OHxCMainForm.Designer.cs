using com.mirle.ibg3k0.bc.winform.App;
using System;
namespace com.mirle.ibg3k0.ohxc.winform
{
    partial class OHxCMainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OHxCMainForm));
            this.startConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_OnLineMode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sendS2F31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tip_CtrlSTAT = new System.Windows.Forms.ToolTip(this.components);
            this.pic_Login = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.passwordChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.systemModeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roadControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mCSCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.communicationLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintenanceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vehicleManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portMaintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mTLMTSMaintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.alarmMaintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPECLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSignalBackGround2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uc_LineID = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_Expander = new System.Windows.Forms.PictureBox();
            this.pic_Home = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_LoginUserValue = new System.Windows.Forms.Label();
            this.uc_Alarm = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uc_SystemSignal();
            this.panel4 = new System.Windows.Forms.Panel();
            this.uc_HostConnect = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uc_SystemSignal();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.uc_VehicleStatus = new System.Windows.Forms.Integration.ElementHost();
            this.uc_VehicleStatus1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_UserControl.uc_StatusInfo();
            this.uc_McsQStatus = new System.Windows.Forms.Integration.ElementHost();
            this.uc_McsQStatus1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_UserControl.uc_StatusInfo_Default();
            this.uc_CstStatus = new System.Windows.Forms.Integration.ElementHost();
            this.uc_CstStatus1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.WPF_UserControl.uc_StatusInfo_Default();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_hourlyProcess_Value = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_RunningTime_Value = new System.Windows.Forms.Label();
            this.lab_RunningTime_Name = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_Version_Value = new System.Windows.Forms.Label();
            this.lab_Version_Name = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_BuildDate_Value = new System.Windows.Forms.Label();
            this.lab_BuildDate_Name = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_TodayProcess_Value = new System.Windows.Forms.Label();
            this.lab_TodayProcess_Name = new System.Windows.Forms.Label();
            this.tssl_Total_Process_WIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Total_Process_WIP_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Today_Process_WIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Today_Process_WIP_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Running_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Running_Time_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Version_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Version_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Build_Date_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Build_Date_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_LoginUser_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_LoginUser_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pic_MirleLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.reserveInfoMaintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Login)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.MainSignalBackGround2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Expander)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Home)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_MirleLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // startConnectionToolStripMenuItem
            // 
            this.startConnectionToolStripMenuItem.Name = "startConnectionToolStripMenuItem";
            resources.ApplyResources(this.startConnectionToolStripMenuItem, "startConnectionToolStripMenuItem");
            // 
            // stopConnectionToolStripMenuItem
            // 
            this.stopConnectionToolStripMenuItem.Name = "stopConnectionToolStripMenuItem";
            resources.ApplyResources(this.stopConnectionToolStripMenuItem, "stopConnectionToolStripMenuItem");
            // 
            // CMS_OnLineMode
            // 
            this.CMS_OnLineMode.Name = "CMS_OnLineMode";
            resources.ApplyResources(this.CMS_OnLineMode, "CMS_OnLineMode");
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendS2F31ToolStripMenuItem});
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            resources.ApplyResources(this.testToolStripMenuItem1, "testToolStripMenuItem1");
            // 
            // sendS2F31ToolStripMenuItem
            // 
            this.sendS2F31ToolStripMenuItem.Name = "sendS2F31ToolStripMenuItem";
            resources.ApplyResources(this.sendS2F31ToolStripMenuItem, "sendS2F31ToolStripMenuItem");
            // 
            // pic_Login
            // 
            this.pic_Login.BackColor = System.Drawing.Color.Transparent;
            this.pic_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Login.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.SystemIcon.icon_LoginAndout;
            resources.ApplyResources(this.pic_Login, "pic_Login");
            this.pic_Login.Name = "pic_Login";
            this.pic_Login.TabStop = false;
            this.Tip_CtrlSTAT.SetToolTip(this.pic_Login, resources.GetString("pic_Login.ToolTip"));
            this.pic_Login.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem1,
            this.operationToolStripMenuItem1,
            this.queryToolStripMenuItem1,
            this.maintenanceToolStripMenuItem1,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // systemToolStripMenuItem1
            // 
            resources.ApplyResources(this.systemToolStripMenuItem1, "systemToolStripMenuItem1");
            this.systemToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.systemToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.toolStripSeparator8,
            this.passwordChangeToolStripMenuItem,
            this.accountManagementToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.systemToolStripMenuItem1.Margin = new System.Windows.Forms.Padding(29, 0, 0, 0);
            this.systemToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.systemToolStripMenuItem1.Name = "systemToolStripMenuItem1";
            this.systemToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // loginToolStripMenuItem
            // 
            resources.ApplyResources(this.loginToolStripMenuItem, "loginToolStripMenuItem");
            this.loginToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.loginToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            resources.ApplyResources(this.logoutToolStripMenuItem, "logoutToolStripMenuItem");
            this.logoutToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.logoutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // passwordChangeToolStripMenuItem
            // 
            resources.ApplyResources(this.passwordChangeToolStripMenuItem, "passwordChangeToolStripMenuItem");
            this.passwordChangeToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.passwordChangeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.passwordChangeToolStripMenuItem.Name = "passwordChangeToolStripMenuItem";
            this.passwordChangeToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // accountManagementToolStripMenuItem
            // 
            resources.ApplyResources(this.accountManagementToolStripMenuItem, "accountManagementToolStripMenuItem");
            this.accountManagementToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.accountManagementToolStripMenuItem.Name = "accountManagementToolStripMenuItem";
            this.accountManagementToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.BackColor = System.Drawing.Color.White;
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // operationToolStripMenuItem1
            // 
            resources.ApplyResources(this.operationToolStripMenuItem1, "operationToolStripMenuItem1");
            this.operationToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.operationToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemModeControlToolStripMenuItem,
            this.transferManagementToolStripMenuItem,
            this.roadControlToolStripMenuItem});
            this.operationToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.operationToolStripMenuItem1.Name = "operationToolStripMenuItem1";
            this.operationToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // systemModeControlToolStripMenuItem
            // 
            this.systemModeControlToolStripMenuItem.Name = "systemModeControlToolStripMenuItem";
            resources.ApplyResources(this.systemModeControlToolStripMenuItem, "systemModeControlToolStripMenuItem");
            this.systemModeControlToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // transferManagementToolStripMenuItem
            // 
            this.transferManagementToolStripMenuItem.Name = "transferManagementToolStripMenuItem";
            resources.ApplyResources(this.transferManagementToolStripMenuItem, "transferManagementToolStripMenuItem");
            this.transferManagementToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // roadControlToolStripMenuItem
            // 
            this.roadControlToolStripMenuItem.Name = "roadControlToolStripMenuItem";
            resources.ApplyResources(this.roadControlToolStripMenuItem, "roadControlToolStripMenuItem");
            this.roadControlToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem1
            // 
            resources.ApplyResources(this.queryToolStripMenuItem1, "queryToolStripMenuItem1");
            this.queryToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.queryToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCSCommandToolStripMenuItem,
            this.communicationLogToolStripMenuItem});
            this.queryToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.queryToolStripMenuItem1.Name = "queryToolStripMenuItem1";
            this.queryToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // mCSCommandToolStripMenuItem
            // 
            this.mCSCommandToolStripMenuItem.Name = "mCSCommandToolStripMenuItem";
            resources.ApplyResources(this.mCSCommandToolStripMenuItem, "mCSCommandToolStripMenuItem");
            this.mCSCommandToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // communicationLogToolStripMenuItem
            // 
            this.communicationLogToolStripMenuItem.Name = "communicationLogToolStripMenuItem";
            resources.ApplyResources(this.communicationLogToolStripMenuItem, "communicationLogToolStripMenuItem");
            this.communicationLogToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // maintenanceToolStripMenuItem1
            // 
            resources.ApplyResources(this.maintenanceToolStripMenuItem1, "maintenanceToolStripMenuItem1");
            this.maintenanceToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.maintenanceToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vehicleManagementToolStripMenuItem,
            this.portMaintenanceToolStripMenuItem,
            this.mTLMTSMaintenanceToolStripMenuItem,
            this.advancedSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.alarmMaintenanceToolStripMenuItem,
            this.reserveInfoMaintenanceToolStripMenuItem});
            this.maintenanceToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.maintenanceToolStripMenuItem1.Name = "maintenanceToolStripMenuItem1";
            this.maintenanceToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // vehicleManagementToolStripMenuItem
            // 
            this.vehicleManagementToolStripMenuItem.Name = "vehicleManagementToolStripMenuItem";
            resources.ApplyResources(this.vehicleManagementToolStripMenuItem, "vehicleManagementToolStripMenuItem");
            this.vehicleManagementToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // portMaintenanceToolStripMenuItem
            // 
            this.portMaintenanceToolStripMenuItem.Name = "portMaintenanceToolStripMenuItem";
            resources.ApplyResources(this.portMaintenanceToolStripMenuItem, "portMaintenanceToolStripMenuItem");
            this.portMaintenanceToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // mTLMTSMaintenanceToolStripMenuItem
            // 
            this.mTLMTSMaintenanceToolStripMenuItem.Name = "mTLMTSMaintenanceToolStripMenuItem";
            resources.ApplyResources(this.mTLMTSMaintenanceToolStripMenuItem, "mTLMTSMaintenanceToolStripMenuItem");
            this.mTLMTSMaintenanceToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // advancedSettingsToolStripMenuItem
            // 
            this.advancedSettingsToolStripMenuItem.Name = "advancedSettingsToolStripMenuItem";
            resources.ApplyResources(this.advancedSettingsToolStripMenuItem, "advancedSettingsToolStripMenuItem");
            this.advancedSettingsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // alarmMaintenanceToolStripMenuItem
            // 
            this.alarmMaintenanceToolStripMenuItem.Name = "alarmMaintenanceToolStripMenuItem";
            resources.ApplyResources(this.alarmMaintenanceToolStripMenuItem, "alarmMaintenanceToolStripMenuItem");
            this.alarmMaintenanceToolStripMenuItem.Click += new System.EventHandler(this.alarmMaintenanceToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            resources.ApplyResources(this.helpToolStripMenuItem1, "helpToolStripMenuItem1");
            this.helpToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.sPECLinkToolStripMenuItem});
            this.helpToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // sPECLinkToolStripMenuItem
            // 
            this.sPECLinkToolStripMenuItem.Name = "sPECLinkToolStripMenuItem";
            resources.ApplyResources(this.sPECLinkToolStripMenuItem, "sPECLinkToolStripMenuItem");
            this.sPECLinkToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // MainSignalBackGround2
            // 
            resources.ApplyResources(this.MainSignalBackGround2, "MainSignalBackGround2");
            this.MainSignalBackGround2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.MainSignalBackGround2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.MainSignalBackGround2.Name = "MainSignalBackGround2";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.tableLayoutPanel1.Controls.Add(this.uc_LineID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 11, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 18, 0);
            this.tableLayoutPanel1.Controls.Add(this.uc_Alarm, 12, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.uc_HostConnect, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.uc_VehicleStatus, 16, 0);
            this.tableLayoutPanel1.Controls.Add(this.uc_McsQStatus, 15, 0);
            this.tableLayoutPanel1.Controls.Add(this.uc_CstStatus, 14, 0);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // uc_LineID
            // 
            resources.ApplyResources(this.uc_LineID, "uc_LineID");
            this.uc_LineID.ForeColor = System.Drawing.Color.White;
            this.uc_LineID.Name = "uc_LineID";
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.panel6.Name = "panel6";
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.panel5.Name = "panel5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pic_Expander);
            this.panel1.Controls.Add(this.pic_Home);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pic_Expander
            // 
            resources.ApplyResources(this.pic_Expander, "pic_Expander");
            this.pic_Expander.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Expander.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.icon_Hamburger;
            this.pic_Expander.Name = "pic_Expander";
            this.pic_Expander.TabStop = false;
            this.pic_Expander.Click += new System.EventHandler(this.pic_Expander_Click);
            // 
            // pic_Home
            // 
            resources.ApplyResources(this.pic_Home, "pic_Home");
            this.pic_Home.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Home.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.icon_Home;
            this.pic_Home.Name = "pic_Home";
            this.pic_Home.TabStop = false;
            this.pic_Home.Click += new System.EventHandler(this.pic_Home_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.panel3.Controls.Add(this.pic_Login);
            this.panel3.Controls.Add(this.lab_LoginUserValue);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // lab_LoginUserValue
            // 
            resources.ApplyResources(this.lab_LoginUserValue, "lab_LoginUserValue");
            this.lab_LoginUserValue.BackColor = System.Drawing.Color.Transparent;
            this.lab_LoginUserValue.ForeColor = System.Drawing.Color.White;
            this.lab_LoginUserValue.Name = "lab_LoginUserValue";
            // 
            // uc_Alarm
            // 
            resources.ApplyResources(this.uc_Alarm, "uc_Alarm");
            this.uc_Alarm.Name = "uc_Alarm";
            this.uc_Alarm.pImage = ((System.Drawing.Image)(resources.GetObject("uc_Alarm.pImage")));
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.panel4.Name = "panel4";
            // 
            // uc_HostConnect
            // 
            resources.ApplyResources(this.uc_HostConnect, "uc_HostConnect");
            this.uc_HostConnect.Name = "uc_HostConnect";
            this.uc_HostConnect.pImage = ((System.Drawing.Image)(resources.GetObject("uc_HostConnect.pImage")));
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.panel2.Name = "panel2";
            // 
            // panel7
            // 
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.panel7.Name = "panel7";
            // 
            // uc_VehicleStatus
            // 
            resources.ApplyResources(this.uc_VehicleStatus, "uc_VehicleStatus");
            this.uc_VehicleStatus.Name = "uc_VehicleStatus";
            this.uc_VehicleStatus.Child = this.uc_VehicleStatus1;
            // 
            // uc_McsQStatus
            // 
            resources.ApplyResources(this.uc_McsQStatus, "uc_McsQStatus");
            this.uc_McsQStatus.Name = "uc_McsQStatus";
            this.uc_McsQStatus.Child = this.uc_McsQStatus1;
            // 
            // uc_CstStatus
            // 
            resources.ApplyResources(this.uc_CstStatus, "uc_CstStatus");
            this.uc_CstStatus.Name = "uc_CstStatus";
            this.uc_CstStatus.Child = this.uc_CstStatus1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tableLayoutPanel7.Controls.Add(this.lab_hourlyProcess_Value, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // lab_hourlyProcess_Value
            // 
            this.lab_hourlyProcess_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_hourlyProcess_Value, "lab_hourlyProcess_Value");
            this.lab_hourlyProcess_Value.ForeColor = System.Drawing.Color.White;
            this.lab_hourlyProcess_Value.Name = "lab_hourlyProcess_Value";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.lab_RunningTime_Value, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lab_RunningTime_Name, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // lab_RunningTime_Value
            // 
            this.lab_RunningTime_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_RunningTime_Value, "lab_RunningTime_Value");
            this.lab_RunningTime_Value.ForeColor = System.Drawing.Color.White;
            this.lab_RunningTime_Value.Name = "lab_RunningTime_Value";
            // 
            // lab_RunningTime_Name
            // 
            this.lab_RunningTime_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_RunningTime_Name, "lab_RunningTime_Name");
            this.lab_RunningTime_Name.ForeColor = System.Drawing.Color.White;
            this.lab_RunningTime_Name.Name = "lab_RunningTime_Name";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.lab_Version_Value, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.lab_Version_Name, 0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // lab_Version_Value
            // 
            this.lab_Version_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_Version_Value, "lab_Version_Value");
            this.lab_Version_Value.ForeColor = System.Drawing.Color.White;
            this.lab_Version_Value.Name = "lab_Version_Value";
            // 
            // lab_Version_Name
            // 
            this.lab_Version_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_Version_Name, "lab_Version_Name");
            this.lab_Version_Name.ForeColor = System.Drawing.Color.White;
            this.lab_Version_Name.Name = "lab_Version_Name";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.lab_BuildDate_Value, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lab_BuildDate_Name, 0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // lab_BuildDate_Value
            // 
            this.lab_BuildDate_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_BuildDate_Value, "lab_BuildDate_Value");
            this.lab_BuildDate_Value.ForeColor = System.Drawing.Color.White;
            this.lab_BuildDate_Value.Name = "lab_BuildDate_Value";
            // 
            // lab_BuildDate_Name
            // 
            this.lab_BuildDate_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_BuildDate_Name, "lab_BuildDate_Name");
            this.lab_BuildDate_Name.ForeColor = System.Drawing.Color.White;
            this.lab_BuildDate_Name.Name = "lab_BuildDate_Name";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.lab_TodayProcess_Value, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lab_TodayProcess_Name, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // lab_TodayProcess_Value
            // 
            this.lab_TodayProcess_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_TodayProcess_Value, "lab_TodayProcess_Value");
            this.lab_TodayProcess_Value.ForeColor = System.Drawing.Color.White;
            this.lab_TodayProcess_Value.Name = "lab_TodayProcess_Value";
            // 
            // lab_TodayProcess_Name
            // 
            this.lab_TodayProcess_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.lab_TodayProcess_Name, "lab_TodayProcess_Name");
            this.lab_TodayProcess_Name.ForeColor = System.Drawing.Color.White;
            this.lab_TodayProcess_Name.Name = "lab_TodayProcess_Name";
            // 
            // tssl_Total_Process_WIP
            // 
            resources.ApplyResources(this.tssl_Total_Process_WIP, "tssl_Total_Process_WIP");
            this.tssl_Total_Process_WIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Total_Process_WIP.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Total_Process_WIP.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Total_Process_WIP.Name = "tssl_Total_Process_WIP";
            // 
            // tssl_Total_Process_WIP_Value
            // 
            resources.ApplyResources(this.tssl_Total_Process_WIP_Value, "tssl_Total_Process_WIP_Value");
            this.tssl_Total_Process_WIP_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Total_Process_WIP_Value.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tssl_Total_Process_WIP_Value.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Total_Process_WIP_Value.Name = "tssl_Total_Process_WIP_Value";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tssl_Today_Process_WIP
            // 
            resources.ApplyResources(this.tssl_Today_Process_WIP, "tssl_Today_Process_WIP");
            this.tssl_Today_Process_WIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Today_Process_WIP.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Today_Process_WIP.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tssl_Today_Process_WIP.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Today_Process_WIP.Name = "tssl_Today_Process_WIP";
            // 
            // tssl_Today_Process_WIP_Value
            // 
            resources.ApplyResources(this.tssl_Today_Process_WIP_Value, "tssl_Today_Process_WIP_Value");
            this.tssl_Today_Process_WIP_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Today_Process_WIP_Value.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Today_Process_WIP_Value.Name = "tssl_Today_Process_WIP_Value";
            // 
            // tssl_Running_Time
            // 
            resources.ApplyResources(this.tssl_Running_Time, "tssl_Running_Time");
            this.tssl_Running_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Running_Time.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Running_Time.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tssl_Running_Time.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Running_Time.Name = "tssl_Running_Time";
            // 
            // tssl_Running_Time_Value
            // 
            resources.ApplyResources(this.tssl_Running_Time_Value, "tssl_Running_Time_Value");
            this.tssl_Running_Time_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Running_Time_Value.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tssl_Running_Time_Value.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Running_Time_Value.Name = "tssl_Running_Time_Value";
            // 
            // tssl_Version_Name
            // 
            this.tssl_Version_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Version_Name.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Version_Name.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            resources.ApplyResources(this.tssl_Version_Name, "tssl_Version_Name");
            this.tssl_Version_Name.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Version_Name.Name = "tssl_Version_Name";
            // 
            // tssl_Version_Value
            // 
            resources.ApplyResources(this.tssl_Version_Value, "tssl_Version_Value");
            this.tssl_Version_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Version_Value.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Version_Value.Name = "tssl_Version_Value";
            // 
            // tssl_Build_Date_Name
            // 
            this.tssl_Build_Date_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Build_Date_Name.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Build_Date_Name.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            resources.ApplyResources(this.tssl_Build_Date_Name, "tssl_Build_Date_Name");
            this.tssl_Build_Date_Name.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Build_Date_Name.Name = "tssl_Build_Date_Name";
            // 
            // tssl_Build_Date_Value
            // 
            resources.ApplyResources(this.tssl_Build_Date_Value, "tssl_Build_Date_Value");
            this.tssl_Build_Date_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_Build_Date_Value.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tssl_Build_Date_Value.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_Build_Date_Value.Name = "tssl_Build_Date_Value";
            // 
            // tssl_LoginUser_Name
            // 
            this.tssl_LoginUser_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_LoginUser_Name.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_LoginUser_Name.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            resources.ApplyResources(this.tssl_LoginUser_Name, "tssl_LoginUser_Name");
            this.tssl_LoginUser_Name.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_LoginUser_Name.Name = "tssl_LoginUser_Name";
            // 
            // tssl_LoginUser_Value
            // 
            resources.ApplyResources(this.tssl_LoginUser_Value, "tssl_LoginUser_Value");
            this.tssl_LoginUser_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            this.tssl_LoginUser_Value.ForeColor = System.Drawing.SystemColors.Control;
            this.tssl_LoginUser_Value.Name = "tssl_LoginUser_Value";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(50)))), ((int)(((byte)(98)))));
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Total_Process_WIP,
            this.tssl_Total_Process_WIP_Value,
            this.toolStripStatusLabel1,
            this.tssl_Today_Process_WIP,
            this.tssl_Today_Process_WIP_Value,
            this.tssl_Running_Time,
            this.tssl_Running_Time_Value,
            this.tssl_Version_Name,
            this.tssl_Version_Value,
            this.tssl_Build_Date_Name,
            this.tssl_Build_Date_Value,
            this.tssl_LoginUser_Name,
            this.tssl_LoginUser_Value});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // pic_MirleLogo
            // 
            this.pic_MirleLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.pic_MirleLogo.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.icon_MirleLogo;
            resources.ApplyResources(this.pic_MirleLogo, "pic_MirleLogo");
            this.pic_MirleLogo.Name = "pic_MirleLogo";
            this.pic_MirleLogo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.pictureBox1.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.icon_CSOTLogo;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // reserveInfoMaintenanceToolStripMenuItem
            // 
            this.reserveInfoMaintenanceToolStripMenuItem.Name = "reserveInfoMaintenanceToolStripMenuItem";
            resources.ApplyResources(this.reserveInfoMaintenanceToolStripMenuItem, "reserveInfoMaintenanceToolStripMenuItem");
            this.reserveInfoMaintenanceToolStripMenuItem.Click += new System.EventHandler(this.reserveInfoMaintenanceToolStripMenuItem_Click);
            // 
            // OHxCMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(94)))), ((int)(((byte)(184)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pic_MirleLogo);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainSignalBackGround2);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OHxCMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OHxCMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OHxCMainForm_FormClosed);
            this.Load += new System.EventHandler(this.OHxCMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Login)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainSignalBackGround2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Expander)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Home)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_MirleLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem startConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sendS2F31ToolStripMenuItem;
        //[AuthorityCheck(FUNCode = BCAppConstants.FUNC_DEBUG_FORM)]
        private System.Windows.Forms.ContextMenuStrip CMS_OnLineMode;
        private System.Windows.Forms.ToolTip Tip_CtrlSTAT;
        private System.Windows.Forms.MenuStrip menuStrip1;

        //System
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passwordChangeToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem userAccountManagementToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.System_Function.FUNC_CLOSE_SYSTEM)]
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

        //Operation        
        private System.Windows.Forms.ToolStripMenuItem operationToolStripMenuItem1;

        //Query
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem1;


        //Maintenance
        private System.Windows.Forms.ToolStripMenuItem maintenanceToolStripMenuItem1;

        //Help
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;





        private System.Windows.Forms.TableLayoutPanel MainSignalBackGround2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;





        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pic_Login;
        private System.Windows.Forms.Label lab_LoginUserValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lab_TodayProcess_Value;
        private System.Windows.Forms.Label lab_TodayProcess_Name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lab_RunningTime_Value;
        private System.Windows.Forms.Label lab_RunningTime_Name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lab_BuildDate_Value;
        private System.Windows.Forms.Label lab_BuildDate_Name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lab_Version_Value;
        private System.Windows.Forms.Label lab_Version_Name;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Total_Process_WIP;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Total_Process_WIP_Value;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Today_Process_WIP;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Today_Process_WIP_Value;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Running_Time;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Running_Time_Value;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Version_Name;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Version_Value;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Build_Date_Name;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Build_Date_Value;
        private System.Windows.Forms.ToolStripStatusLabel tssl_LoginUser_Name;
        private System.Windows.Forms.ToolStripStatusLabel tssl_LoginUser_Value;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label lab_hourlyProcess_Value;
        private System.Windows.Forms.Label label2;
        private UI.Components.uc_SystemSignal uc_Alarm;
        private System.Windows.Forms.ToolStripMenuItem mCSCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem communicationLogToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_MTS_MTL_MAINTENANCE)]
        private System.Windows.Forms.ToolStripMenuItem mTLMTSMaintenanceToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_SYSTEM_CONCROL_MODE)]
        private System.Windows.Forms.ToolStripMenuItem systemModeControlToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.System_Function.FUNC_ACCOUNT_MANAGEMENT)]
        private System.Windows.Forms.ToolStripMenuItem accountManagementToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pic_Expander;
        private System.Windows.Forms.PictureBox pic_Home;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label uc_LineID;
        private UI.Components.uc_SystemSignal uc_HostConnect;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Integration.ElementHost uc_VehicleStatus;
        private UI.Components.WPF_UserControl.uc_StatusInfo uc_VehicleStatus1;
        private System.Windows.Forms.Integration.ElementHost uc_McsQStatus;
        private UI.Components.WPF_UserControl.uc_StatusInfo_Default uc_McsQStatus1;
        private System.Windows.Forms.Integration.ElementHost uc_CstStatus;
        private UI.Components.WPF_UserControl.uc_StatusInfo_Default uc_CstStatus1;
        [AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_TRANSFER_MANAGEMENT)]
        private System.Windows.Forms.ToolStripMenuItem transferManagementToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_PORT_MAINTENANCE)]
        private System.Windows.Forms.ToolStripMenuItem portMaintenanceToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_VEHICLE_MANAGEMENT)]
        private System.Windows.Forms.ToolStripMenuItem vehicleManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPECLinkToolStripMenuItem;
        private System.Windows.Forms.PictureBox pic_MirleLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem advancedSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alarmMaintenanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        //[AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_ROAD_CONTROL)]
        private System.Windows.Forms.ToolStripMenuItem roadControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reserveInfoMaintenanceToolStripMenuItem;
    }

    /// <summary>
    /// 來用排序Alarm His 在Data Grid View 的順序
    /// </summary>
    public class AuthorityCheck : Attribute
    {
        //public AlarmOrder();

        public string FUNCode { get; set; }

    }
}

