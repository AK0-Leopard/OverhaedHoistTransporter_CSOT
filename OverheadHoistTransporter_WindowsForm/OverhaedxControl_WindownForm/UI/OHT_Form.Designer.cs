﻿namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class OHT_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer_refreshVhInfo = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Map = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uctl_Map = new com.mirle.ibg3k0.bc.winform.UI.Components.uctl_Map();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uctl_Dashboard1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uctl_Dashboard();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_detectionSystemExist = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_earthqualeHappend = new System.Windows.Forms.Label();
            this.lbl_hostconn = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_RediStat = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grp_ControlTable = new System.Windows.Forms.GroupBox();
            this.btn_parkZoneTypeChange = new System.Windows.Forms.Button();
            this.btn_recover_to_autoremote = new System.Windows.Forms.Button();
            this.btn_st1 = new System.Windows.Forms.Button();
            this.cb_parkZoneType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gb_PortNameType = new System.Windows.Forms.GroupBox();
            this.Raid_PortNameType_AdrID = new System.Windows.Forms.RadioButton();
            this.Raid_PortNameType_PortID = new System.Windows.Forms.RadioButton();
            this.cmb_cycRunZone = new System.Windows.Forms.ComboBox();
            this.btn_pause = new System.Windows.Forms.Button();
            this.cb_sectionThroughTimes = new System.Windows.Forms.CheckBox();
            this.btn_continuous = new System.Windows.Forms.Button();
            this.ck_montor_vh = new System.Windows.Forms.CheckBox();
            this.btn_AutoMove = new System.Windows.Forms.Button();
            this.cb_autoTip = new System.Windows.Forms.CheckBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.cmb_fromAddress = new System.Windows.Forms.ComboBox();
            this.cmb_toAddress = new System.Windows.Forms.ComboBox();
            this.cbm_Action = new System.Windows.Forms.ComboBox();
            this.cmb_Vehicle = new System.Windows.Forms.ComboBox();
            this.cmb_fromSection = new System.Windows.Forms.ComboBox();
            this.lbl_sourceName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_destinationName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbcList = new System.Windows.Forms.TabControl();
            this.tab_vhStatus = new System.Windows.Forms.TabPage();
            this.dgv_vhStatus = new System.Windows.Forms.DataGridView();
            this.vEHICLEIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mCSCMDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oHTCCMDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oBSDIST2ShowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBSDISTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vEHICLEACCDISTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSPARKINGDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pARKTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSCYCLINGDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cYCLERUNTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uPDTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicleObjToShowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tapTrace = new System.Windows.Forms.TabPage();
            this.listTrace = new System.Windows.Forms.ListBox();
            this.tapTransferCmd = new System.Windows.Forms.TabPage();
            this.dgv_TransferCommand = new System.Windows.Forms.DataGridView();
            this.tapDetail = new System.Windows.Forms.TabPage();
            this.dgv_TaskCommand = new System.Windows.Forms.DataGridView();
            this.tapCurrentAlarm = new System.Windows.Forms.TabPage();
            this.tlp_crtAlarm = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_Alarm = new System.Windows.Forms.DataGridView();
            this.eqpt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_lvl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.report_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPLC = new System.Windows.Forms.TabPage();
            this.tab_SECS = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mODESTATUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aCTSTATUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_Map.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grp_ControlTable.SuspendLayout();
            this.gb_PortNameType.SuspendLayout();
            this.tbcList.SuspendLayout();
            this.tab_vhStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vhStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleObjToShowBindingSource)).BeginInit();
            this.tapTrace.SuspendLayout();
            this.tapTransferCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TransferCommand)).BeginInit();
            this.tapDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TaskCommand)).BeginInit();
            this.tapCurrentAlarm.SuspendLayout();
            this.tlp_crtAlarm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Alarm)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_refreshVhInfo
            // 
            this.timer_refreshVhInfo.Interval = 1000;
            this.timer_refreshVhInfo.Tick += new System.EventHandler(this.timer_refreshVhInfo_Tick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "MODE_STATUS";
            this.dataGridViewTextBoxColumn1.HeaderText = "Mode";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 76;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ACT_STATUS";
            this.dataGridViewTextBoxColumn2.HeaderText = "Action";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 83;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "MODE_STATUS";
            this.dataGridViewTextBoxColumn3.HeaderText = "Mode";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 76;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ACT_STATUS";
            this.dataGridViewTextBoxColumn4.HeaderText = "Action";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 83;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbcList);
            this.splitContainer1.Size = new System.Drawing.Size(1924, 1042);
            this.splitContainer1.SplitterDistance = 908;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.54678F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.45322F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_Map, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grp_ControlTable, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 745F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1924, 908);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnl_Map
            // 
            this.pnl_Map.AutoScroll = true;
            this.pnl_Map.Controls.Add(this.tabControl1);
            this.pnl_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Map.Location = new System.Drawing.Point(3, 3);
            this.pnl_Map.Name = "pnl_Map";
            this.tableLayoutPanel1.SetRowSpan(this.pnl_Map, 2);
            this.pnl_Map.Size = new System.Drawing.Size(1486, 902);
            this.pnl_Map.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1486, 902);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uctl_Map);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1478, 870);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Map";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uctl_Map
            // 
            this.uctl_Map.AutoScroll = true;
            this.uctl_Map.BackColor = System.Drawing.Color.MidnightBlue;
            this.uctl_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uctl_Map.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uctl_Map.Location = new System.Drawing.Point(3, 3);
            this.uctl_Map.Margin = new System.Windows.Forms.Padding(0);
            this.uctl_Map.Name = "uctl_Map";
            this.uctl_Map.Size = new System.Drawing.Size(1472, 864);
            this.uctl_Map.TabIndex = 0;
            this.uctl_Map.Load += new System.EventHandler(this.uctl_Map_Load);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.uctl_Dashboard1);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1478, 870);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dashboard";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // uctl_Dashboard1
            // 
            this.uctl_Dashboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uctl_Dashboard1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uctl_Dashboard1.Location = new System.Drawing.Point(3, 3);
            this.uctl_Dashboard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uctl_Dashboard1.Name = "uctl_Dashboard1";
            this.uctl_Dashboard1.Size = new System.Drawing.Size(1472, 864);
            this.uctl_Dashboard1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_detectionSystemExist);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lbl_earthqualeHappend);
            this.panel1.Controls.Add(this.lbl_hostconn);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lbl_RediStat);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1495, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 157);
            this.panel1.TabIndex = 1;
            // 
            // lbl_detectionSystemExist
            // 
            this.lbl_detectionSystemExist.AutoSize = true;
            this.lbl_detectionSystemExist.BackColor = System.Drawing.Color.Gray;
            this.lbl_detectionSystemExist.Location = new System.Drawing.Point(11, 135);
            this.lbl_detectionSystemExist.Name = "lbl_detectionSystemExist";
            this.lbl_detectionSystemExist.Size = new System.Drawing.Size(54, 19);
            this.lbl_detectionSystemExist.TabIndex = 10;
            this.lbl_detectionSystemExist.Text = "     ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(71, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 19);
            this.label10.TabIndex = 11;
            this.label10.Text = ":Detection System";
            // 
            // lbl_earthqualeHappend
            // 
            this.lbl_earthqualeHappend.AutoSize = true;
            this.lbl_earthqualeHappend.BackColor = System.Drawing.Color.Gray;
            this.lbl_earthqualeHappend.Location = new System.Drawing.Point(11, 6);
            this.lbl_earthqualeHappend.Name = "lbl_earthqualeHappend";
            this.lbl_earthqualeHappend.Size = new System.Drawing.Size(54, 19);
            this.lbl_earthqualeHappend.TabIndex = 9;
            this.lbl_earthqualeHappend.Text = "     ";
            // 
            // lbl_hostconn
            // 
            this.lbl_hostconn.AutoSize = true;
            this.lbl_hostconn.BackColor = System.Drawing.Color.Gray;
            this.lbl_hostconn.Location = new System.Drawing.Point(11, 92);
            this.lbl_hostconn.Name = "lbl_hostconn";
            this.lbl_hostconn.Size = new System.Drawing.Size(54, 19);
            this.lbl_hostconn.TabIndex = 4;
            this.lbl_hostconn.Text = "     ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(71, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = ":Host Conn";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(71, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 19);
            this.label9.TabIndex = 8;
            this.label9.Text = ":Earthquake Happend";
            // 
            // lbl_RediStat
            // 
            this.lbl_RediStat.AutoSize = true;
            this.lbl_RediStat.BackColor = System.Drawing.Color.Gray;
            this.lbl_RediStat.Location = new System.Drawing.Point(11, 49);
            this.lbl_RediStat.Name = "lbl_RediStat";
            this.lbl_RediStat.Size = new System.Drawing.Size(54, 19);
            this.lbl_RediStat.TabIndex = 7;
            this.lbl_RediStat.Text = "     ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(71, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = ":Redis";
            // 
            // grp_ControlTable
            // 
            this.grp_ControlTable.Controls.Add(this.button3);
            this.grp_ControlTable.Controls.Add(this.button2);
            this.grp_ControlTable.Controls.Add(this.btn_parkZoneTypeChange);
            this.grp_ControlTable.Controls.Add(this.btn_recover_to_autoremote);
            this.grp_ControlTable.Controls.Add(this.btn_st1);
            this.grp_ControlTable.Controls.Add(this.cb_parkZoneType);
            this.grp_ControlTable.Controls.Add(this.label6);
            this.grp_ControlTable.Controls.Add(this.gb_PortNameType);
            this.grp_ControlTable.Controls.Add(this.cmb_cycRunZone);
            this.grp_ControlTable.Controls.Add(this.btn_pause);
            this.grp_ControlTable.Controls.Add(this.cb_sectionThroughTimes);
            this.grp_ControlTable.Controls.Add(this.btn_continuous);
            this.grp_ControlTable.Controls.Add(this.ck_montor_vh);
            this.grp_ControlTable.Controls.Add(this.btn_AutoMove);
            this.grp_ControlTable.Controls.Add(this.cb_autoTip);
            this.grp_ControlTable.Controls.Add(this.btn_start);
            this.grp_ControlTable.Controls.Add(this.cmb_fromAddress);
            this.grp_ControlTable.Controls.Add(this.cmb_toAddress);
            this.grp_ControlTable.Controls.Add(this.cbm_Action);
            this.grp_ControlTable.Controls.Add(this.cmb_Vehicle);
            this.grp_ControlTable.Controls.Add(this.cmb_fromSection);
            this.grp_ControlTable.Controls.Add(this.lbl_sourceName);
            this.grp_ControlTable.Controls.Add(this.label5);
            this.grp_ControlTable.Controls.Add(this.lbl_destinationName);
            this.grp_ControlTable.Controls.Add(this.label3);
            this.grp_ControlTable.Controls.Add(this.label1);
            this.grp_ControlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_ControlTable.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.grp_ControlTable.Location = new System.Drawing.Point(1495, 166);
            this.grp_ControlTable.Name = "grp_ControlTable";
            this.grp_ControlTable.Size = new System.Drawing.Size(426, 739);
            this.grp_ControlTable.TabIndex = 0;
            this.grp_ControlTable.TabStop = false;
            this.grp_ControlTable.Text = "Specify Path";
            // 
            // btn_parkZoneTypeChange
            // 
            this.btn_parkZoneTypeChange.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_parkZoneTypeChange.Location = new System.Drawing.Point(181, 564);
            this.btn_parkZoneTypeChange.Name = "btn_parkZoneTypeChange";
            this.btn_parkZoneTypeChange.Size = new System.Drawing.Size(44, 27);
            this.btn_parkZoneTypeChange.TabIndex = 8;
            this.btn_parkZoneTypeChange.Text = "OK";
            this.btn_parkZoneTypeChange.UseVisualStyleBackColor = true;
            this.btn_parkZoneTypeChange.Click += new System.EventHandler(this.btn_parkZoneTypeChange_Click);
            // 
            // btn_recover_to_autoremote
            // 
            this.btn_recover_to_autoremote.ForeColor = System.Drawing.Color.Black;
            this.btn_recover_to_autoremote.Location = new System.Drawing.Point(8, 516);
            this.btn_recover_to_autoremote.Name = "btn_recover_to_autoremote";
            this.btn_recover_to_autoremote.Size = new System.Drawing.Size(211, 23);
            this.btn_recover_to_autoremote.TabIndex = 9;
            this.btn_recover_to_autoremote.Text = "Recover to AutoRemote";
            this.btn_recover_to_autoremote.UseVisualStyleBackColor = true;
            this.btn_recover_to_autoremote.Visible = false;
            this.btn_recover_to_autoremote.Click += new System.EventHandler(this.btn_recover_to_autoremote_Click);
            // 
            // btn_st1
            // 
            this.btn_st1.ForeColor = System.Drawing.Color.Black;
            this.btn_st1.Location = new System.Drawing.Point(10, 489);
            this.btn_st1.Name = "btn_st1";
            this.btn_st1.Size = new System.Drawing.Size(209, 23);
            this.btn_st1.TabIndex = 9;
            this.btn_st1.Text = "Move to MT Port";
            this.btn_st1.UseVisualStyleBackColor = true;
            this.btn_st1.Click += new System.EventHandler(this.btn_st1_Click);
            // 
            // cb_parkZoneType
            // 
            this.cb_parkZoneType.FormattingEnabled = true;
            this.cb_parkZoneType.Location = new System.Drawing.Point(17, 564);
            this.cb_parkZoneType.Name = "cb_parkZoneType";
            this.cb_parkZoneType.Size = new System.Drawing.Size(158, 27);
            this.cb_parkZoneType.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(22, 542);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Park Zone Type";
            // 
            // gb_PortNameType
            // 
            this.gb_PortNameType.Controls.Add(this.Raid_PortNameType_AdrID);
            this.gb_PortNameType.Controls.Add(this.Raid_PortNameType_PortID);
            this.gb_PortNameType.ForeColor = System.Drawing.Color.White;
            this.gb_PortNameType.Location = new System.Drawing.Point(10, 143);
            this.gb_PortNameType.Name = "gb_PortNameType";
            this.gb_PortNameType.Size = new System.Drawing.Size(162, 90);
            this.gb_PortNameType.TabIndex = 5;
            this.gb_PortNameType.TabStop = false;
            this.gb_PortNameType.Text = "Port ID Type";
            // 
            // Raid_PortNameType_AdrID
            // 
            this.Raid_PortNameType_AdrID.AutoSize = true;
            this.Raid_PortNameType_AdrID.Checked = true;
            this.Raid_PortNameType_AdrID.Location = new System.Drawing.Point(6, 25);
            this.Raid_PortNameType_AdrID.Name = "Raid_PortNameType_AdrID";
            this.Raid_PortNameType_AdrID.Size = new System.Drawing.Size(81, 23);
            this.Raid_PortNameType_AdrID.TabIndex = 0;
            this.Raid_PortNameType_AdrID.TabStop = true;
            this.Raid_PortNameType_AdrID.Text = "Adr ID";
            this.Raid_PortNameType_AdrID.UseVisualStyleBackColor = true;
            this.Raid_PortNameType_AdrID.CheckedChanged += new System.EventHandler(this.Raid_PortNameType_CheckedChanged);
            // 
            // Raid_PortNameType_PortID
            // 
            this.Raid_PortNameType_PortID.AutoSize = true;
            this.Raid_PortNameType_PortID.Location = new System.Drawing.Point(6, 54);
            this.Raid_PortNameType_PortID.Name = "Raid_PortNameType_PortID";
            this.Raid_PortNameType_PortID.Size = new System.Drawing.Size(90, 23);
            this.Raid_PortNameType_PortID.TabIndex = 0;
            this.Raid_PortNameType_PortID.TabStop = true;
            this.Raid_PortNameType_PortID.Text = "Port ID";
            this.Raid_PortNameType_PortID.UseVisualStyleBackColor = true;
            this.Raid_PortNameType_PortID.CheckedChanged += new System.EventHandler(this.Raid_PortNameType_CheckedChanged);
            // 
            // cmb_cycRunZone
            // 
            this.cmb_cycRunZone.FormattingEnabled = true;
            this.cmb_cycRunZone.Location = new System.Drawing.Point(11, 310);
            this.cmb_cycRunZone.Name = "cmb_cycRunZone";
            this.cmb_cycRunZone.Size = new System.Drawing.Size(162, 27);
            this.cmb_cycRunZone.TabIndex = 4;
            // 
            // btn_pause
            // 
            this.btn_pause.ForeColor = System.Drawing.Color.Black;
            this.btn_pause.Location = new System.Drawing.Point(24, 381);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(133, 29);
            this.btn_pause.TabIndex = 3;
            this.btn_pause.Text = "Pause";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // cb_sectionThroughTimes
            // 
            this.cb_sectionThroughTimes.AutoSize = true;
            this.cb_sectionThroughTimes.ForeColor = System.Drawing.Color.Transparent;
            this.cb_sectionThroughTimes.Location = new System.Drawing.Point(16, 690);
            this.cb_sectionThroughTimes.Name = "cb_sectionThroughTimes";
            this.cb_sectionThroughTimes.Size = new System.Drawing.Size(217, 23);
            this.cb_sectionThroughTimes.TabIndex = 3;
            this.cb_sectionThroughTimes.Text = "Section Through Times";
            this.cb_sectionThroughTimes.UseVisualStyleBackColor = true;
            this.cb_sectionThroughTimes.CheckedChanged += new System.EventHandler(this.cb_sectionThroughTimes_CheckedChanged);
            this.cb_sectionThroughTimes.Click += new System.EventHandler(this.cb_sectionThroughTimes_Click);
            // 
            // btn_continuous
            // 
            this.btn_continuous.ForeColor = System.Drawing.Color.Black;
            this.btn_continuous.Location = new System.Drawing.Point(24, 416);
            this.btn_continuous.Name = "btn_continuous";
            this.btn_continuous.Size = new System.Drawing.Size(133, 30);
            this.btn_continuous.TabIndex = 3;
            this.btn_continuous.Text = "Continuous";
            this.btn_continuous.UseVisualStyleBackColor = true;
            this.btn_continuous.Click += new System.EventHandler(this.btn_continuous_Click);
            // 
            // ck_montor_vh
            // 
            this.ck_montor_vh.AutoSize = true;
            this.ck_montor_vh.ForeColor = System.Drawing.Color.Transparent;
            this.ck_montor_vh.Location = new System.Drawing.Point(17, 665);
            this.ck_montor_vh.Name = "ck_montor_vh";
            this.ck_montor_vh.Size = new System.Drawing.Size(118, 23);
            this.ck_montor_vh.TabIndex = 2;
            this.ck_montor_vh.Text = "Monitor Vh";
            this.ck_montor_vh.UseVisualStyleBackColor = true;
            this.ck_montor_vh.CheckedChanged += new System.EventHandler(this.ck_montor_vh_CheckedChanged);
            // 
            // btn_AutoMove
            // 
            this.btn_AutoMove.ForeColor = System.Drawing.Color.Black;
            this.btn_AutoMove.Location = new System.Drawing.Point(24, 452);
            this.btn_AutoMove.Name = "btn_AutoMove";
            this.btn_AutoMove.Size = new System.Drawing.Size(73, 31);
            this.btn_AutoMove.TabIndex = 2;
            this.btn_AutoMove.Text = "Auto Move";
            this.btn_AutoMove.UseVisualStyleBackColor = true;
            this.btn_AutoMove.Click += new System.EventHandler(this.btn_AutoMove_Click);
            // 
            // cb_autoTip
            // 
            this.cb_autoTip.AutoSize = true;
            this.cb_autoTip.Checked = true;
            this.cb_autoTip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_autoTip.ForeColor = System.Drawing.Color.Transparent;
            this.cb_autoTip.Location = new System.Drawing.Point(17, 643);
            this.cb_autoTip.Name = "cb_autoTip";
            this.cb_autoTip.Size = new System.Drawing.Size(127, 23);
            this.cb_autoTip.TabIndex = 1;
            this.cb_autoTip.Text = "Tip Message";
            this.cb_autoTip.UseVisualStyleBackColor = true;
            this.cb_autoTip.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_start
            // 
            this.btn_start.Enabled = false;
            this.btn_start.ForeColor = System.Drawing.Color.Black;
            this.btn_start.Location = new System.Drawing.Point(24, 348);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(133, 27);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // cmb_fromAddress
            // 
            this.cmb_fromAddress.FormattingEnabled = true;
            this.cmb_fromAddress.Location = new System.Drawing.Point(10, 258);
            this.cmb_fromAddress.Name = "cmb_fromAddress";
            this.cmb_fromAddress.Size = new System.Drawing.Size(162, 27);
            this.cmb_fromAddress.TabIndex = 1;
            // 
            // cmb_toAddress
            // 
            this.cmb_toAddress.FormattingEnabled = true;
            this.cmb_toAddress.Location = new System.Drawing.Point(10, 310);
            this.cmb_toAddress.Name = "cmb_toAddress";
            this.cmb_toAddress.Size = new System.Drawing.Size(162, 27);
            this.cmb_toAddress.TabIndex = 1;
            // 
            // cbm_Action
            // 
            this.cbm_Action.FormattingEnabled = true;
            this.cbm_Action.Location = new System.Drawing.Point(11, 110);
            this.cbm_Action.Name = "cbm_Action";
            this.cbm_Action.Size = new System.Drawing.Size(162, 27);
            this.cbm_Action.TabIndex = 1;
            this.cbm_Action.SelectedIndexChanged += new System.EventHandler(this.cbm_Action_SelectedIndexChanged);
            // 
            // cmb_Vehicle
            // 
            this.cmb_Vehicle.FormattingEnabled = true;
            this.cmb_Vehicle.Location = new System.Drawing.Point(6, 58);
            this.cmb_Vehicle.Name = "cmb_Vehicle";
            this.cmb_Vehicle.Size = new System.Drawing.Size(162, 27);
            this.cmb_Vehicle.TabIndex = 1;
            // 
            // cmb_fromSection
            // 
            this.cmb_fromSection.FormattingEnabled = true;
            this.cmb_fromSection.Location = new System.Drawing.Point(16, 610);
            this.cmb_fromSection.Name = "cmb_fromSection";
            this.cmb_fromSection.Size = new System.Drawing.Size(162, 27);
            this.cmb_fromSection.TabIndex = 1;
            this.cmb_fromSection.Visible = false;
            // 
            // lbl_sourceName
            // 
            this.lbl_sourceName.AutoSize = true;
            this.lbl_sourceName.Location = new System.Drawing.Point(6, 236);
            this.lbl_sourceName.Name = "lbl_sourceName";
            this.lbl_sourceName.Size = new System.Drawing.Size(117, 19);
            this.lbl_sourceName.TabIndex = 0;
            this.lbl_sourceName.Text = "From Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Action";
            // 
            // lbl_destinationName
            // 
            this.lbl_destinationName.AutoSize = true;
            this.lbl_destinationName.Location = new System.Drawing.Point(6, 288);
            this.lbl_destinationName.Name = "lbl_destinationName";
            this.lbl_destinationName.Size = new System.Drawing.Size(99, 19);
            this.lbl_destinationName.TabIndex = 0;
            this.lbl_destinationName.Text = "To Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Vehicle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 593);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Section";
            this.label1.Visible = false;
            // 
            // tbcList
            // 
            this.tbcList.Controls.Add(this.tab_vhStatus);
            this.tbcList.Controls.Add(this.tapTrace);
            this.tbcList.Controls.Add(this.tapTransferCmd);
            this.tbcList.Controls.Add(this.tapDetail);
            this.tbcList.Controls.Add(this.tapCurrentAlarm);
            this.tbcList.Controls.Add(this.tabPLC);
            this.tbcList.Controls.Add(this.tab_SECS);
            this.tbcList.Controls.Add(this.tabPage3);
            this.tbcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.tbcList.ItemSize = new System.Drawing.Size(160, 23);
            this.tbcList.Location = new System.Drawing.Point(0, 0);
            this.tbcList.Name = "tbcList";
            this.tbcList.SelectedIndex = 0;
            this.tbcList.Size = new System.Drawing.Size(1924, 128);
            this.tbcList.TabIndex = 2;
            // 
            // tab_vhStatus
            // 
            this.tab_vhStatus.Controls.Add(this.dgv_vhStatus);
            this.tab_vhStatus.Location = new System.Drawing.Point(4, 27);
            this.tab_vhStatus.Name = "tab_vhStatus";
            this.tab_vhStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tab_vhStatus.Size = new System.Drawing.Size(1916, 97);
            this.tab_vhStatus.TabIndex = 5;
            this.tab_vhStatus.Text = "Vehicle Status            ";
            this.tab_vhStatus.UseVisualStyleBackColor = true;
            // 
            // dgv_vhStatus
            // 
            this.dgv_vhStatus.AllowUserToAddRows = false;
            this.dgv_vhStatus.AllowUserToDeleteRows = false;
            this.dgv_vhStatus.AutoGenerateColumns = false;
            this.dgv_vhStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_vhStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_vhStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_vhStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vEHICLEIDDataGridViewTextBoxColumn,
            this.mCSCMDDataGridViewTextBoxColumn,
            this.oHTCCMDDataGridViewTextBoxColumn,
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn,
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn,
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn,
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn,
            this.oBSDIST2ShowDataGridViewTextBoxColumn,
            this.oBSDISTDataGridViewTextBoxColumn,
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn,
            this.vEHICLEACCDISTDataGridViewTextBoxColumn,
            this.iSPARKINGDataGridViewCheckBoxColumn,
            this.pARKTIMEDataGridViewTextBoxColumn,
            this.iSCYCLINGDataGridViewCheckBoxColumn,
            this.cYCLERUNTIMEDataGridViewTextBoxColumn,
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn,
            this.uPDTIMEDataGridViewTextBoxColumn,
            this.mODESTATUSDataGridViewTextBoxColumn,
            this.aCTSTATUSDataGridViewTextBoxColumn});
            this.dgv_vhStatus.DataSource = this.vehicleObjToShowBindingSource;
            this.dgv_vhStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_vhStatus.Location = new System.Drawing.Point(3, 3);
            this.dgv_vhStatus.MultiSelect = false;
            this.dgv_vhStatus.Name = "dgv_vhStatus";
            this.dgv_vhStatus.ReadOnly = true;
            this.dgv_vhStatus.RowTemplate.Height = 24;
            this.dgv_vhStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_vhStatus.Size = new System.Drawing.Size(1910, 91);
            this.dgv_vhStatus.TabIndex = 0;
            this.dgv_vhStatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_vhStatus_CellClick);
            this.dgv_vhStatus.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_vhStatus_DataError);
            // 
            // vEHICLEIDDataGridViewTextBoxColumn
            // 
            this.vEHICLEIDDataGridViewTextBoxColumn.DataPropertyName = "VEHICLE_ID";
            this.vEHICLEIDDataGridViewTextBoxColumn.HeaderText = "Vh ID";
            this.vEHICLEIDDataGridViewTextBoxColumn.Name = "vEHICLEIDDataGridViewTextBoxColumn";
            this.vEHICLEIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.vEHICLEIDDataGridViewTextBoxColumn.Width = 75;
            // 
            // mCSCMDDataGridViewTextBoxColumn
            // 
            this.mCSCMDDataGridViewTextBoxColumn.DataPropertyName = "MCS_CMD";
            this.mCSCMDDataGridViewTextBoxColumn.HeaderText = "MCS CMD";
            this.mCSCMDDataGridViewTextBoxColumn.Name = "mCSCMDDataGridViewTextBoxColumn";
            this.mCSCMDDataGridViewTextBoxColumn.ReadOnly = true;
            this.mCSCMDDataGridViewTextBoxColumn.Width = 111;
            // 
            // oHTCCMDDataGridViewTextBoxColumn
            // 
            this.oHTCCMDDataGridViewTextBoxColumn.DataPropertyName = "OHTC_CMD";
            this.oHTCCMDDataGridViewTextBoxColumn.HeaderText = "OHTC CMD";
            this.oHTCCMDDataGridViewTextBoxColumn.Name = "oHTCCMDDataGridViewTextBoxColumn";
            this.oHTCCMDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oHTCCMDDataGridViewTextBoxColumn.Width = 121;
            // 
            // bLOCKPAUSE2ShowDataGridViewCheckBoxColumn
            // 
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn.DataPropertyName = "bLOCK_PAUSE2Show";
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn.HeaderText = "Block pause";
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn.Name = "bLOCKPAUSE2ShowDataGridViewCheckBoxColumn";
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn.ReadOnly = true;
            this.bLOCKPAUSE2ShowDataGridViewCheckBoxColumn.Width = 110;
            // 
            // cMDPAUSE2ShowDataGridViewCheckBoxColumn
            // 
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn.DataPropertyName = "cMD_PAUSE2Show";
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn.HeaderText = "CMD pause";
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn.Name = "cMDPAUSE2ShowDataGridViewCheckBoxColumn";
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn.ReadOnly = true;
            this.cMDPAUSE2ShowDataGridViewCheckBoxColumn.Width = 103;
            // 
            // oBSPAUSE2ShowDataGridViewCheckBoxColumn
            // 
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn.DataPropertyName = "oBS_PAUSE2Show";
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn.HeaderText = "OBS pause";
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn.Name = "oBSPAUSE2ShowDataGridViewCheckBoxColumn";
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn.ReadOnly = true;
            this.oBSPAUSE2ShowDataGridViewCheckBoxColumn.Width = 101;
            // 
            // hIDPAUSE2ShowDataGridViewCheckBoxColumn
            // 
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn.DataPropertyName = "hID_PAUSE2Show";
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn.HeaderText = "HID pause";
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn.Name = "hIDPAUSE2ShowDataGridViewCheckBoxColumn";
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn.ReadOnly = true;
            this.hIDPAUSE2ShowDataGridViewCheckBoxColumn.Width = 94;
            // 
            // oBSDIST2ShowDataGridViewTextBoxColumn
            // 
            this.oBSDIST2ShowDataGridViewTextBoxColumn.DataPropertyName = "oBS_DIST2Show";
            this.oBSDIST2ShowDataGridViewTextBoxColumn.HeaderText = "OBS DIST(m)";
            this.oBSDIST2ShowDataGridViewTextBoxColumn.Name = "oBSDIST2ShowDataGridViewTextBoxColumn";
            this.oBSDIST2ShowDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBSDIST2ShowDataGridViewTextBoxColumn.Width = 134;
            // 
            // oBSDISTDataGridViewTextBoxColumn
            // 
            this.oBSDISTDataGridViewTextBoxColumn.DataPropertyName = "OBS_DIST";
            this.oBSDISTDataGridViewTextBoxColumn.HeaderText = "OBS_DIST";
            this.oBSDISTDataGridViewTextBoxColumn.Name = "oBSDISTDataGridViewTextBoxColumn";
            this.oBSDISTDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBSDISTDataGridViewTextBoxColumn.Visible = false;
            this.oBSDISTDataGridViewTextBoxColumn.Width = 115;
            // 
            // vEHICLEACCDIST2ShowDataGridViewTextBoxColumn
            // 
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn.DataPropertyName = "vEHICLE_ACC_DIST2Show";
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn.HeaderText = "ODO(km)";
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn.Name = "vEHICLEACCDIST2ShowDataGridViewTextBoxColumn";
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn.ReadOnly = true;
            this.vEHICLEACCDIST2ShowDataGridViewTextBoxColumn.Width = 103;
            // 
            // vEHICLEACCDISTDataGridViewTextBoxColumn
            // 
            this.vEHICLEACCDISTDataGridViewTextBoxColumn.DataPropertyName = "VEHICLE_ACC_DIST";
            this.vEHICLEACCDISTDataGridViewTextBoxColumn.HeaderText = "VEHICLE_ACC_DIST";
            this.vEHICLEACCDISTDataGridViewTextBoxColumn.Name = "vEHICLEACCDISTDataGridViewTextBoxColumn";
            this.vEHICLEACCDISTDataGridViewTextBoxColumn.ReadOnly = true;
            this.vEHICLEACCDISTDataGridViewTextBoxColumn.Visible = false;
            this.vEHICLEACCDISTDataGridViewTextBoxColumn.Width = 195;
            // 
            // iSPARKINGDataGridViewCheckBoxColumn
            // 
            this.iSPARKINGDataGridViewCheckBoxColumn.DataPropertyName = "IS_PARKING";
            this.iSPARKINGDataGridViewCheckBoxColumn.HeaderText = "Parking";
            this.iSPARKINGDataGridViewCheckBoxColumn.Name = "iSPARKINGDataGridViewCheckBoxColumn";
            this.iSPARKINGDataGridViewCheckBoxColumn.ReadOnly = true;
            this.iSPARKINGDataGridViewCheckBoxColumn.Width = 74;
            // 
            // pARKTIMEDataGridViewTextBoxColumn
            // 
            this.pARKTIMEDataGridViewTextBoxColumn.DataPropertyName = "PARK_TIME";
            this.pARKTIMEDataGridViewTextBoxColumn.HeaderText = "Park time";
            this.pARKTIMEDataGridViewTextBoxColumn.Name = "pARKTIMEDataGridViewTextBoxColumn";
            this.pARKTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.pARKTIMEDataGridViewTextBoxColumn.Width = 105;
            // 
            // iSCYCLINGDataGridViewCheckBoxColumn
            // 
            this.iSCYCLINGDataGridViewCheckBoxColumn.DataPropertyName = "IS_CYCLING";
            this.iSCYCLINGDataGridViewCheckBoxColumn.HeaderText = "Cycling";
            this.iSCYCLINGDataGridViewCheckBoxColumn.Name = "iSCYCLINGDataGridViewCheckBoxColumn";
            this.iSCYCLINGDataGridViewCheckBoxColumn.ReadOnly = true;
            this.iSCYCLINGDataGridViewCheckBoxColumn.Width = 73;
            // 
            // cYCLERUNTIMEDataGridViewTextBoxColumn
            // 
            this.cYCLERUNTIMEDataGridViewTextBoxColumn.DataPropertyName = "CYCLERUN_TIME";
            this.cYCLERUNTIMEDataGridViewTextBoxColumn.HeaderText = "Cycling time";
            this.cYCLERUNTIMEDataGridViewTextBoxColumn.Name = "cYCLERUNTIMEDataGridViewTextBoxColumn";
            this.cYCLERUNTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.cYCLERUNTIMEDataGridViewTextBoxColumn.Width = 128;
            // 
            // aCCSECDIST2ShowDataGridViewTextBoxColumn
            // 
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn.DataPropertyName = "ACC_SEC_DIST2Show";
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn.HeaderText = "Sec DIST(m)";
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn.Name = "aCCSECDIST2ShowDataGridViewTextBoxColumn";
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn.ReadOnly = true;
            this.aCCSECDIST2ShowDataGridViewTextBoxColumn.Width = 128;
            // 
            // uPDTIMEDataGridViewTextBoxColumn
            // 
            this.uPDTIMEDataGridViewTextBoxColumn.DataPropertyName = "UPD_TIME";
            this.uPDTIMEDataGridViewTextBoxColumn.HeaderText = "UPD_TIME";
            this.uPDTIMEDataGridViewTextBoxColumn.Name = "uPDTIMEDataGridViewTextBoxColumn";
            this.uPDTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.uPDTIMEDataGridViewTextBoxColumn.Width = 116;
            // 
            // vehicleObjToShowBindingSource
            // 
            this.vehicleObjToShowBindingSource.DataSource = typeof(com.mirle.ibg3k0.ohxc.winform.ObjectRelay.VehicleObjToShow);
            // 
            // tapTrace
            // 
            this.tapTrace.Controls.Add(this.listTrace);
            this.tapTrace.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tapTrace.Location = new System.Drawing.Point(4, 27);
            this.tapTrace.Name = "tapTrace";
            this.tapTrace.Size = new System.Drawing.Size(1916, 97);
            this.tapTrace.TabIndex = 3;
            this.tapTrace.Text = "System Log            ";
            this.tapTrace.UseVisualStyleBackColor = true;
            // 
            // listTrace
            // 
            this.listTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTrace.Font = new System.Drawing.Font("Arial", 12F);
            this.listTrace.FormattingEnabled = true;
            this.listTrace.HorizontalScrollbar = true;
            this.listTrace.ItemHeight = 18;
            this.listTrace.Location = new System.Drawing.Point(0, 0);
            this.listTrace.Name = "listTrace";
            this.listTrace.Size = new System.Drawing.Size(1916, 97);
            this.listTrace.TabIndex = 0;
            // 
            // tapTransferCmd
            // 
            this.tapTransferCmd.BackColor = System.Drawing.SystemColors.Control;
            this.tapTransferCmd.Controls.Add(this.dgv_TransferCommand);
            this.tapTransferCmd.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tapTransferCmd.Location = new System.Drawing.Point(4, 27);
            this.tapTransferCmd.Margin = new System.Windows.Forms.Padding(0);
            this.tapTransferCmd.Name = "tapTransferCmd";
            this.tapTransferCmd.Size = new System.Drawing.Size(1916, 97);
            this.tapTransferCmd.TabIndex = 0;
            this.tapTransferCmd.Text = "Transfer Command            ";
            // 
            // dgv_TransferCommand
            // 
            this.dgv_TransferCommand.AllowUserToAddRows = false;
            this.dgv_TransferCommand.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_TransferCommand.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_TransferCommand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TransferCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_TransferCommand.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_TransferCommand.Location = new System.Drawing.Point(0, 0);
            this.dgv_TransferCommand.Name = "dgv_TransferCommand";
            this.dgv_TransferCommand.ReadOnly = true;
            this.dgv_TransferCommand.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_TransferCommand.RowTemplate.Height = 24;
            this.dgv_TransferCommand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_TransferCommand.Size = new System.Drawing.Size(1916, 97);
            this.dgv_TransferCommand.TabIndex = 0;
            // 
            // tapDetail
            // 
            this.tapDetail.Controls.Add(this.dgv_TaskCommand);
            this.tapDetail.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tapDetail.Location = new System.Drawing.Point(4, 27);
            this.tapDetail.Margin = new System.Windows.Forms.Padding(0);
            this.tapDetail.Name = "tapDetail";
            this.tapDetail.Size = new System.Drawing.Size(1916, 97);
            this.tapDetail.TabIndex = 1;
            this.tapDetail.Text = "Command Detail            ";
            this.tapDetail.UseVisualStyleBackColor = true;
            // 
            // dgv_TaskCommand
            // 
            this.dgv_TaskCommand.AllowUserToAddRows = false;
            this.dgv_TaskCommand.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_TaskCommand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TaskCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_TaskCommand.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_TaskCommand.Location = new System.Drawing.Point(0, 0);
            this.dgv_TaskCommand.Name = "dgv_TaskCommand";
            this.dgv_TaskCommand.ReadOnly = true;
            this.dgv_TaskCommand.RowTemplate.Height = 24;
            this.dgv_TaskCommand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_TaskCommand.Size = new System.Drawing.Size(1916, 97);
            this.dgv_TaskCommand.TabIndex = 0;
            // 
            // tapCurrentAlarm
            // 
            this.tapCurrentAlarm.Controls.Add(this.tlp_crtAlarm);
            this.tapCurrentAlarm.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tapCurrentAlarm.Location = new System.Drawing.Point(4, 27);
            this.tapCurrentAlarm.Name = "tapCurrentAlarm";
            this.tapCurrentAlarm.Size = new System.Drawing.Size(1916, 97);
            this.tapCurrentAlarm.TabIndex = 2;
            this.tapCurrentAlarm.Text = "Current  Alarm            ";
            this.tapCurrentAlarm.UseVisualStyleBackColor = true;
            // 
            // tlp_crtAlarm
            // 
            this.tlp_crtAlarm.ColumnCount = 1;
            this.tlp_crtAlarm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_crtAlarm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_crtAlarm.Controls.Add(this.dgv_Alarm, 0, 0);
            this.tlp_crtAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_crtAlarm.Location = new System.Drawing.Point(0, 0);
            this.tlp_crtAlarm.Name = "tlp_crtAlarm";
            this.tlp_crtAlarm.RowCount = 1;
            this.tlp_crtAlarm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_crtAlarm.Size = new System.Drawing.Size(1916, 97);
            this.tlp_crtAlarm.TabIndex = 1;
            // 
            // dgv_Alarm
            // 
            this.dgv_Alarm.AllowUserToAddRows = false;
            this.dgv_Alarm.AllowUserToDeleteRows = false;
            this.dgv_Alarm.AllowUserToOrderColumns = true;
            this.dgv_Alarm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Alarm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Alarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Alarm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eqpt_id,
            this.alarm_code,
            this.alarm_lvl,
            this.report_time,
            this.alarm_desc});
            this.dgv_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Alarm.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_Alarm.Location = new System.Drawing.Point(3, 3);
            this.dgv_Alarm.Name = "dgv_Alarm";
            this.dgv_Alarm.ReadOnly = true;
            this.dgv_Alarm.RowTemplate.Height = 24;
            this.dgv_Alarm.Size = new System.Drawing.Size(1910, 91);
            this.dgv_Alarm.TabIndex = 0;
            // 
            // eqpt_id
            // 
            this.eqpt_id.DataPropertyName = "EQPT_ID";
            this.eqpt_id.HeaderText = "EQPT ID";
            this.eqpt_id.Name = "eqpt_id";
            this.eqpt_id.ReadOnly = true;
            // 
            // alarm_code
            // 
            this.alarm_code.DataPropertyName = "ALAM_CODE";
            this.alarm_code.HeaderText = "Code";
            this.alarm_code.Name = "alarm_code";
            this.alarm_code.ReadOnly = true;
            // 
            // alarm_lvl
            // 
            this.alarm_lvl.DataPropertyName = "ALAM_LVL";
            this.alarm_lvl.HeaderText = "Level";
            this.alarm_lvl.Name = "alarm_lvl";
            this.alarm_lvl.ReadOnly = true;
            // 
            // report_time
            // 
            this.report_time.DataPropertyName = "RPT_DATE_TIME";
            this.report_time.HeaderText = "Time";
            this.report_time.Name = "report_time";
            this.report_time.ReadOnly = true;
            // 
            // alarm_desc
            // 
            this.alarm_desc.DataPropertyName = "ALAM_DESC";
            this.alarm_desc.FillWeight = 200F;
            this.alarm_desc.HeaderText = "Description";
            this.alarm_desc.Name = "alarm_desc";
            this.alarm_desc.ReadOnly = true;
            // 
            // tabPLC
            // 
            this.tabPLC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tabPLC.Location = new System.Drawing.Point(4, 27);
            this.tabPLC.Name = "tabPLC";
            this.tabPLC.Size = new System.Drawing.Size(1916, 97);
            this.tabPLC.TabIndex = 4;
            this.tabPLC.Text = "PLC Communication      ";
            this.tabPLC.UseVisualStyleBackColor = true;
            // 
            // tab_SECS
            // 
            this.tab_SECS.Location = new System.Drawing.Point(4, 27);
            this.tab_SECS.Name = "tab_SECS";
            this.tab_SECS.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SECS.Size = new System.Drawing.Size(1916, 97);
            this.tab_SECS.TabIndex = 6;
            this.tab_SECS.Text = "SECS Communcation    ";
            this.tab_SECS.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1916, 97);
            this.tabPage3.TabIndex = 7;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "MODE_STATUS";
            this.dataGridViewTextBoxColumn5.HeaderText = "Mode";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 76;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ACT_STATUS";
            this.dataGridViewTextBoxColumn6.HeaderText = "Action";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 83;
            // 
            // mODESTATUSDataGridViewTextBoxColumn
            // 
            this.mODESTATUSDataGridViewTextBoxColumn.DataPropertyName = "MODE_STATUS";
            this.mODESTATUSDataGridViewTextBoxColumn.HeaderText = "Mode";
            this.mODESTATUSDataGridViewTextBoxColumn.Name = "mODESTATUSDataGridViewTextBoxColumn";
            this.mODESTATUSDataGridViewTextBoxColumn.ReadOnly = true;
            this.mODESTATUSDataGridViewTextBoxColumn.Width = 76;
            // 
            // aCTSTATUSDataGridViewTextBoxColumn
            // 
            this.aCTSTATUSDataGridViewTextBoxColumn.DataPropertyName = "ACT_STATUS";
            this.aCTSTATUSDataGridViewTextBoxColumn.HeaderText = "Action";
            this.aCTSTATUSDataGridViewTextBoxColumn.Name = "aCTSTATUSDataGridViewTextBoxColumn";
            this.aCTSTATUSDataGridViewTextBoxColumn.ReadOnly = true;
            this.aCTSTATUSDataGridViewTextBoxColumn.Width = 83;
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(105, 452);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 31);
            this.button2.TabIndex = 10;
            this.button2.Text = "Auto Local";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(225, 452);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 31);
            this.button3.TabIndex = 11;
            this.button3.Text = "Manual";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // OHT_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1924, 1042);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "OHT_Form";
            this.Load += new System.EventHandler(this.OHT_Form_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl_Map.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grp_ControlTable.ResumeLayout(false);
            this.grp_ControlTable.PerformLayout();
            this.gb_PortNameType.ResumeLayout(false);
            this.gb_PortNameType.PerformLayout();
            this.tbcList.ResumeLayout(false);
            this.tab_vhStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vhStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleObjToShowBindingSource)).EndInit();
            this.tapTrace.ResumeLayout(false);
            this.tapTransferCmd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TransferCommand)).EndInit();
            this.tapDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TaskCommand)).EndInit();
            this.tapCurrentAlarm.ResumeLayout(false);
            this.tlp_crtAlarm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Alarm)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tbcList;
        private System.Windows.Forms.TabPage tapTrace;
        private System.Windows.Forms.ListBox listTrace;
        private System.Windows.Forms.TabPage tapTransferCmd;
        private System.Windows.Forms.DataGridView dgv_TransferCommand;
        private System.Windows.Forms.TabPage tapDetail;
        private System.Windows.Forms.DataGridView dgv_TaskCommand;
        private System.Windows.Forms.TabPage tapCurrentAlarm;
        private System.Windows.Forms.TableLayoutPanel tlp_crtAlarm;
        private System.Windows.Forms.DataGridView dgv_Alarm;
        private System.Windows.Forms.TabPage tabPLC;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_Map;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grp_ControlTable;
        private System.Windows.Forms.ComboBox cmb_fromSection;
        private System.Windows.Forms.Label lbl_destinationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_toAddress;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Vehicle;
        private System.Windows.Forms.ComboBox cmb_fromAddress;
        private System.Windows.Forms.Label lbl_sourceName;
        private System.Windows.Forms.TabPage tab_vhStatus;
        private System.Windows.Forms.DataGridView dgv_vhStatus;
        private System.Windows.Forms.Button btn_continuous;
        private System.Windows.Forms.Button btn_pause;
        private System.Windows.Forms.CheckBox cb_autoTip;
        private System.Windows.Forms.TabPage tab_SECS;
        private System.Windows.Forms.ComboBox cbm_Action;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_cycRunZone;
        private System.Windows.Forms.RadioButton Raid_PortNameType_PortID;
        private System.Windows.Forms.GroupBox gb_PortNameType;
        private System.Windows.Forms.RadioButton Raid_PortNameType_AdrID;
        private System.Windows.Forms.CheckBox ck_montor_vh;
        private System.Windows.Forms.Button btn_AutoMove;
        private System.Windows.Forms.CheckBox cb_sectionThroughTimes;
        private System.Windows.Forms.Button btn_st1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_hostconn;
        private System.Windows.Forms.ComboBox cb_parkZoneType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_parkZoneTypeChange;
        private System.Windows.Forms.Button btn_recover_to_autoremote;
        private System.Windows.Forms.Label lbl_RediStat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_earthqualeHappend;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn eqpt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_lvl;
        private System.Windows.Forms.DataGridViewTextBoxColumn report_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_desc;
        private System.Windows.Forms.Label lbl_detectionSystemExist;
        private System.Windows.Forms.Label label10;
        private Components.uctl_Map uctl_Map;
        private System.Windows.Forms.DataGridViewTextBoxColumn vEHICLEIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mODESTATUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aCTSTATUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mCSCMDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oHTCCMDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn bLOCKPAUSE2ShowDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cMDPAUSE2ShowDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn oBSPAUSE2ShowDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hIDPAUSE2ShowDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBSDIST2ShowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBSDISTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vEHICLEACCDIST2ShowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vEHICLEACCDISTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iSPARKINGDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pARKTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iSCYCLINGDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cYCLERUNTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aCCSECDIST2ShowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uPDTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource vehicleObjToShowBindingSource;
        private System.Windows.Forms.Timer timer_refreshVhInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private ohxc.winform.UI.Components.uctl_Dashboard uctl_Dashboard1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}