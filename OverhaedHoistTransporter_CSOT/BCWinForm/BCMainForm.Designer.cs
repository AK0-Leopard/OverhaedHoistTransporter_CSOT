// ***********************************************************************
// Assembly         : BC
// Author           : 
// Created          : 03-31-2016
//
// Last Modified By : 
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="BCMainForm.Designer.cs" company="Mirle">
//     Copyright ©2014 MIRLE.3K0
// </copyright>
// <summary></summary>
// ***********************************************************************
using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bc.winform.Common;
using System;
namespace com.mirle.ibg3k0.bc.winform
{
    /// <summary>
    /// Class BCMainForm.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    partial class BCMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCMainForm));
            this.startConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Version_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Version_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Build_Date_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Build_Date_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_LoginUser_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_LoginUser_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.CMS_OnLineMode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onLineReToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onLineLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sendS2F31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tip_CtrlSTAT = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startConnectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopConnectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.operatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.engineerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roadControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zhTwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectionDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintainDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.transferCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reserveInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zh_twToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zh_chToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.communectionStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.engineerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.engineeringModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferCommandHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alarmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.CMS_OnLineMode.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssl_Version_Name,
            this.tssl_Version_Value,
            this.tssl_Build_Date_Name,
            this.tssl_Build_Date_Value,
            this.tssl_LoginUser_Name,
            this.tssl_LoginUser_Value});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tssl_Version_Name
            // 
            this.tssl_Version_Name.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Version_Name.Name = "tssl_Version_Name";
            resources.ApplyResources(this.tssl_Version_Name, "tssl_Version_Name");
            // 
            // tssl_Version_Value
            // 
            resources.ApplyResources(this.tssl_Version_Value, "tssl_Version_Value");
            this.tssl_Version_Value.Name = "tssl_Version_Value";
            // 
            // tssl_Build_Date_Name
            // 
            this.tssl_Build_Date_Name.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_Build_Date_Name.Name = "tssl_Build_Date_Name";
            resources.ApplyResources(this.tssl_Build_Date_Name, "tssl_Build_Date_Name");
            // 
            // tssl_Build_Date_Value
            // 
            resources.ApplyResources(this.tssl_Build_Date_Value, "tssl_Build_Date_Value");
            this.tssl_Build_Date_Value.Name = "tssl_Build_Date_Value";
            // 
            // tssl_LoginUser_Name
            // 
            this.tssl_LoginUser_Name.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssl_LoginUser_Name.Name = "tssl_LoginUser_Name";
            resources.ApplyResources(this.tssl_LoginUser_Name, "tssl_LoginUser_Name");
            // 
            // tssl_LoginUser_Value
            // 
            resources.ApplyResources(this.tssl_LoginUser_Value, "tssl_LoginUser_Value");
            this.tssl_LoginUser_Value.Name = "tssl_LoginUser_Value";
            // 
            // CMS_OnLineMode
            // 
            this.CMS_OnLineMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onLineReToolStripMenuItem,
            this.onLineLocalToolStripMenuItem,
            this.offLineToolStripMenuItem});
            this.CMS_OnLineMode.Name = "CMS_OnLineMode";
            resources.ApplyResources(this.CMS_OnLineMode, "CMS_OnLineMode");
            // 
            // onLineReToolStripMenuItem
            // 
            this.onLineReToolStripMenuItem.Name = "onLineReToolStripMenuItem";
            resources.ApplyResources(this.onLineReToolStripMenuItem, "onLineReToolStripMenuItem");
            // 
            // onLineLocalToolStripMenuItem
            // 
            this.onLineLocalToolStripMenuItem.Name = "onLineLocalToolStripMenuItem";
            resources.ApplyResources(this.onLineLocalToolStripMenuItem, "onLineLocalToolStripMenuItem");
            // 
            // offLineToolStripMenuItem
            // 
            this.offLineToolStripMenuItem.Name = "offLineToolStripMenuItem";
            resources.ApplyResources(this.offLineToolStripMenuItem, "offLineToolStripMenuItem");
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
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.toolStripMenuItem2,
            this.operatorToolStripMenuItem,
            this.mataToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.tipMessageToolStripMenuItem,
            this.monitorToolStripMenuItem,
            this.engineerToolStripMenuItem1,
            this.queryToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDoubleClick);
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startConnectionToolStripMenuItem1,
            this.stopConnectionToolStripMenuItem1,
            this.sEToolStripMenuItem,
            this.dToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            resources.ApplyResources(this.systemToolStripMenuItem, "systemToolStripMenuItem");
            // 
            // startConnectionToolStripMenuItem1
            // 
            this.startConnectionToolStripMenuItem1.Name = "startConnectionToolStripMenuItem1";
            resources.ApplyResources(this.startConnectionToolStripMenuItem1, "startConnectionToolStripMenuItem1");
            this.startConnectionToolStripMenuItem1.Click += new System.EventHandler(this.startConnectionToolStripMenuItem1_Click);
            // 
            // stopConnectionToolStripMenuItem1
            // 
            this.stopConnectionToolStripMenuItem1.Name = "stopConnectionToolStripMenuItem1";
            resources.ApplyResources(this.stopConnectionToolStripMenuItem1, "stopConnectionToolStripMenuItem1");
            this.stopConnectionToolStripMenuItem1.Click += new System.EventHandler(this.stopConnectionToolStripMenuItem1_Click);
            // 
            // sEToolStripMenuItem
            // 
            this.sEToolStripMenuItem.Name = "sEToolStripMenuItem";
            resources.ApplyResources(this.sEToolStripMenuItem, "sEToolStripMenuItem");
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            resources.ApplyResources(this.dToolStripMenuItem, "dToolStripMenuItem");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // operatorToolStripMenuItem
            // 
            this.operatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logInToolStripMenuItem,
            this.logOutToolStripMenuItem,
            this.engineerToolStripMenuItem,
            this.languageChangeToolStripMenuItem,
            this.editToolStripMenuItem,
            this.hostConnectionToolStripMenuItem,
            this.maintainDeviceToolStripMenuItem});
            this.operatorToolStripMenuItem.Name = "operatorToolStripMenuItem";
            resources.ApplyResources(this.operatorToolStripMenuItem, "operatorToolStripMenuItem");
            // 
            // logInToolStripMenuItem
            // 
            this.logInToolStripMenuItem.Name = "logInToolStripMenuItem";
            resources.ApplyResources(this.logInToolStripMenuItem, "logInToolStripMenuItem");
            this.logInToolStripMenuItem.Click += new System.EventHandler(this.logInToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            resources.ApplyResources(this.logOutToolStripMenuItem, "logOutToolStripMenuItem");
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // engineerToolStripMenuItem
            // 
            this.engineerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem,
            this.roadControlToolStripMenuItem});
            this.engineerToolStripMenuItem.Name = "engineerToolStripMenuItem";
            resources.ApplyResources(this.engineerToolStripMenuItem, "engineerToolStripMenuItem");
            this.engineerToolStripMenuItem.Click += new System.EventHandler(this.engineerToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            resources.ApplyResources(this.debugToolStripMenuItem, "debugToolStripMenuItem");
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // roadControlToolStripMenuItem
            // 
            this.roadControlToolStripMenuItem.Name = "roadControlToolStripMenuItem";
            resources.ApplyResources(this.roadControlToolStripMenuItem, "roadControlToolStripMenuItem");
            this.roadControlToolStripMenuItem.Click += new System.EventHandler(this.roadControlToolStripMenuItem_Click);
            // 
            // languageChangeToolStripMenuItem
            // 
            this.languageChangeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zhTwToolStripMenuItem,
            this.enUSToolStripMenuItem});
            this.languageChangeToolStripMenuItem.Name = "languageChangeToolStripMenuItem";
            resources.ApplyResources(this.languageChangeToolStripMenuItem, "languageChangeToolStripMenuItem");
            // 
            // zhTwToolStripMenuItem
            // 
            this.zhTwToolStripMenuItem.Name = "zhTwToolStripMenuItem";
            resources.ApplyResources(this.zhTwToolStripMenuItem, "zhTwToolStripMenuItem");
            this.zhTwToolStripMenuItem.Click += new System.EventHandler(this.zhTwToolStripMenuItem_Click);
            // 
            // enUSToolStripMenuItem
            // 
            this.enUSToolStripMenuItem.Name = "enUSToolStripMenuItem";
            resources.ApplyResources(this.enUSToolStripMenuItem, "enUSToolStripMenuItem");
            this.enUSToolStripMenuItem.Click += new System.EventHandler(this.enUSToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sectionDataToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // sectionDataToolStripMenuItem
            // 
            this.sectionDataToolStripMenuItem.Name = "sectionDataToolStripMenuItem";
            resources.ApplyResources(this.sectionDataToolStripMenuItem, "sectionDataToolStripMenuItem");
            this.sectionDataToolStripMenuItem.Click += new System.EventHandler(this.sectionDataToolStripMenuItem_Click);
            // 
            // hostConnectionToolStripMenuItem
            // 
            this.hostConnectionToolStripMenuItem.Name = "hostConnectionToolStripMenuItem";
            resources.ApplyResources(this.hostConnectionToolStripMenuItem, "hostConnectionToolStripMenuItem");
            this.hostConnectionToolStripMenuItem.Click += new System.EventHandler(this.hostConnectionToolStripMenuItem_Click);
            // 
            // maintainDeviceToolStripMenuItem
            // 
            this.maintainDeviceToolStripMenuItem.Name = "maintainDeviceToolStripMenuItem";
            resources.ApplyResources(this.maintainDeviceToolStripMenuItem, "maintainDeviceToolStripMenuItem");
            this.maintainDeviceToolStripMenuItem.Click += new System.EventHandler(this.maintainDeviceToolStripMenuItem_Click);
            // 
            // mataToolStripMenuItem
            // 
            this.mataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.uASToolStripMenuItem,
            this.toolStripSeparator1,
            this.transferCommandToolStripMenuItem,
            this.toolStripMenuItem1,
            this.reserveInfoToolStripMenuItem,
            this.bufferPortToolStripMenuItem});
            this.mataToolStripMenuItem.Name = "mataToolStripMenuItem";
            resources.ApplyResources(this.mataToolStripMenuItem, "mataToolStripMenuItem");
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // uASToolStripMenuItem
            // 
            this.uASToolStripMenuItem.Name = "uASToolStripMenuItem";
            resources.ApplyResources(this.uASToolStripMenuItem, "uASToolStripMenuItem");
            this.uASToolStripMenuItem.Click += new System.EventHandler(this.uASToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // transferCommandToolStripMenuItem
            // 
            this.transferCommandToolStripMenuItem.Name = "transferCommandToolStripMenuItem";
            resources.ApplyResources(this.transferCommandToolStripMenuItem, "transferCommandToolStripMenuItem");
            this.transferCommandToolStripMenuItem.Click += new System.EventHandler(this.transferCommandToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // reserveInfoToolStripMenuItem
            // 
            this.reserveInfoToolStripMenuItem.Name = "reserveInfoToolStripMenuItem";
            resources.ApplyResources(this.reserveInfoToolStripMenuItem, "reserveInfoToolStripMenuItem");
            this.reserveInfoToolStripMenuItem.Click += new System.EventHandler(this.reserveInfoToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.zh_twToolStripMenuItem,
            this.zh_chToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // zh_twToolStripMenuItem
            // 
            this.zh_twToolStripMenuItem.Name = "zh_twToolStripMenuItem";
            resources.ApplyResources(this.zh_twToolStripMenuItem, "zh_twToolStripMenuItem");
            this.zh_twToolStripMenuItem.Click += new System.EventHandler(this.zh_twToolStripMenuItem_Click);
            // 
            // zh_chToolStripMenuItem
            // 
            this.zh_chToolStripMenuItem.Name = "zh_chToolStripMenuItem";
            resources.ApplyResources(this.zh_chToolStripMenuItem, "zh_chToolStripMenuItem");
            this.zh_chToolStripMenuItem.Click += new System.EventHandler(this.zh_chToolStripMenuItem_Click);
            // 
            // tipMessageToolStripMenuItem
            // 
            this.tipMessageToolStripMenuItem.Name = "tipMessageToolStripMenuItem";
            resources.ApplyResources(this.tipMessageToolStripMenuItem, "tipMessageToolStripMenuItem");
            this.tipMessageToolStripMenuItem.Click += new System.EventHandler(this.tipMessageToolStripMenuItem_Click);
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.communectionStatusToolStripMenuItem});
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            resources.ApplyResources(this.monitorToolStripMenuItem, "monitorToolStripMenuItem");
            // 
            // communectionStatusToolStripMenuItem
            // 
            this.communectionStatusToolStripMenuItem.Name = "communectionStatusToolStripMenuItem";
            resources.ApplyResources(this.communectionStatusToolStripMenuItem, "communectionStatusToolStripMenuItem");
            this.communectionStatusToolStripMenuItem.Click += new System.EventHandler(this.communectionStatusToolStripMenuItem_Click);
            // 
            // engineerToolStripMenuItem1
            // 
            this.engineerToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem1,
            this.engineeringModeToolStripMenuItem});
            this.engineerToolStripMenuItem1.Name = "engineerToolStripMenuItem1";
            resources.ApplyResources(this.engineerToolStripMenuItem1, "engineerToolStripMenuItem1");
            // 
            // debugToolStripMenuItem1
            // 
            this.debugToolStripMenuItem1.Name = "debugToolStripMenuItem1";
            resources.ApplyResources(this.debugToolStripMenuItem1, "debugToolStripMenuItem1");
            this.debugToolStripMenuItem1.Click += new System.EventHandler(this.debugToolStripMenuItem1_Click);
            // 
            // engineeringModeToolStripMenuItem
            // 
            this.engineeringModeToolStripMenuItem.Name = "engineeringModeToolStripMenuItem";
            resources.ApplyResources(this.engineeringModeToolStripMenuItem, "engineeringModeToolStripMenuItem");
            this.engineeringModeToolStripMenuItem.Click += new System.EventHandler(this.engineeringModeToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transferCommandHistoryToolStripMenuItem,
            this.alarmToolStripMenuItem});
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            resources.ApplyResources(this.queryToolStripMenuItem, "queryToolStripMenuItem");
            // 
            // transferCommandHistoryToolStripMenuItem
            // 
            this.transferCommandHistoryToolStripMenuItem.Name = "transferCommandHistoryToolStripMenuItem";
            resources.ApplyResources(this.transferCommandHistoryToolStripMenuItem, "transferCommandHistoryToolStripMenuItem");
            // 
            // alarmToolStripMenuItem
            // 
            this.alarmToolStripMenuItem.Name = "alarmToolStripMenuItem";
            resources.ApplyResources(this.alarmToolStripMenuItem, "alarmToolStripMenuItem");
            this.alarmToolStripMenuItem.Click += new System.EventHandler(this.alarmToolStripMenuItem_Click);
            // 
            // bufferPortToolStripMenuItem
            // 
            this.bufferPortToolStripMenuItem.Name = "bufferPortToolStripMenuItem";
            resources.ApplyResources(this.bufferPortToolStripMenuItem, "bufferPortToolStripMenuItem");
            this.bufferPortToolStripMenuItem.Click += new System.EventHandler(this.bufferPortToolStripMenuItem_Click);
            // 
            // BCMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BCMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BCMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BCMainForm_FormClosed);
            this.Load += new System.EventHandler(this.BCMainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.CMS_OnLineMode.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// The status strip1
        /// </summary>
        private System.Windows.Forms.StatusStrip statusStrip1;
        /// <summary>
        /// The tool strip status label1
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        /// <summary>
        /// The TSSL_ version_ name
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel tssl_Version_Name;
        /// <summary>
        /// The TSSL_ version_ value
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel tssl_Version_Value;
        /// <summary>
        /// The TSSL_ login user_ name
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel tssl_LoginUser_Name;
        /// <summary>
        /// The TSSL_ login user_ value
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel tssl_LoginUser_Value;
        /// <summary>
        /// The start connection tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem startConnectionToolStripMenuItem;
        /// <summary>
        /// The stop connection tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem stopConnectionToolStripMenuItem;
        /// <summary>
        /// The test tool strip menu item1
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem1;
        /// <summary>
        /// The send s2 F31 tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem sendS2F31ToolStripMenuItem;
        /// <summary>
        /// The cm s_ on line mode
        /// </summary>
        private System.Windows.Forms.ContextMenuStrip CMS_OnLineMode;
        /// <summary>
        /// The on line re tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem onLineReToolStripMenuItem;
        /// <summary>
        /// The on line local tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem onLineLocalToolStripMenuItem;
        /// <summary>
        /// The off line tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem offLineToolStripMenuItem;
        /// <summary>
        /// The tip_ control stat
        /// </summary>
        private System.Windows.Forms.ToolTip Tip_CtrlSTAT;
        /// <summary>
        /// The TSSL_ build_ date_ name
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel tssl_Build_Date_Name;
        /// <summary>
        /// The TSSL_ build_ date_ value
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel tssl_Build_Date_Value;
        /// <summary>
        /// The system tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        /// <summary>
        /// The start connection tool strip menu item1
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem startConnectionToolStripMenuItem1;
        /// <summary>
        /// The stop connection tool strip menu item1
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem stopConnectionToolStripMenuItem1;
        /// <summary>
        /// The s e tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem sEToolStripMenuItem;
        /// <summary>
        /// The d tool strip menu item
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        /// <summary>
        /// The tool strip menu item2
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        /// <summary>
        /// The menu strip1
        /// </summary>
        private System.Windows.Forms.MenuStrip menuStrip1;
        
        private System.Windows.Forms.ToolStripMenuItem mataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.System_Function.FUNC_ACCOUNT_MANAGEMENT)]
        private System.Windows.Forms.ToolStripMenuItem uASToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zh_twToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zh_chToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem communectionStatusToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem engineerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem engineerToolStripMenuItem1;
        [AuthorityCheck(FUNCode = BCAppConstants.Debug_Function.FUNC_DEBUG)]
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem1;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem engineeringModeToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem roadControlToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem languageChangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zhTwToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enUSToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sectionDataToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_SYSTEM_CONCROL_MODE)]
        private System.Windows.Forms.ToolStripMenuItem hostConnectionToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_MTS_MTL_MAINTENANCE)]
        private System.Windows.Forms.ToolStripMenuItem maintainDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem transferCommandToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reserveInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferCommandHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alarmToolStripMenuItem;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.ToolStripMenuItem bufferPortToolStripMenuItem;
    }

    /// <summary>
    /// 來用排序Alarm His 在Data Grid View 的順序
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class AuthorityCheck : Attribute
    {
        //public AlarmOrder();

        /// <summary>
        /// Gets or sets the fun code.
        /// </summary>
        /// <value>The fun code.</value>
        public string FUNCode { get; set; }

    }
}

