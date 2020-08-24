using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class uc_CommunicationLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_CommunicationLog));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_Close = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.btn_Search = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_HrsInterval = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CommunLog_MCSCmdID = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_StartDTCbx = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CommunLog_VhID = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.m_EndDTCbx = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.m_AlarmSetTimeCb = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.Last_3days = new System.Windows.Forms.LinkLabel();
            this.Last_2days = new System.Windows.Forms.LinkLabel();
            this.Last_24hrs = new System.Windows.Forms.LinkLabel();
            this.Last_12hrs = new System.Windows.Forms.LinkLabel();
            this.Last_4hrs = new System.Windows.Forms.LinkLabel();
            this.Last_1hrs = new System.Windows.Forms.LinkLabel();
            this.Last_30mins = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.uctl_ElasticQuery_sys_process_log = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uctl_ElasticQuery_System_Process();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1448701926_Exit.ico");
            this.imageList1.Images.SetKeyName(1, "export.ico");
            this.imageList1.Images.SetKeyName(2, "export.png");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_AlarmSetTimeCb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Controls.Add(this.btn_Close);
            this.panel6.Controls.Add(this.btn_Search);
            this.panel6.Name = "panel6";
            this.tableLayoutPanel1.SetRowSpan(this.panel6, 2);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.ButtonForeColor = System.Drawing.Color.White;
            this.btn_Close.ButtonName = "Close";
            resources.ApplyResources(this.btn_Close, "btn_Close");
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Button_Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.Transparent;
            this.btn_Search.ButtonForeColor = System.Drawing.Color.White;
            this.btn_Search.ButtonName = "Search";
            resources.ApplyResources(this.btn_Search, "btn_Search");
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Button_Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel9, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // tableLayoutPanel9
            // 
            resources.ApplyResources(this.tableLayoutPanel9, "tableLayoutPanel9");
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel11, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            // 
            // tableLayoutPanel11
            // 
            resources.ApplyResources(this.tableLayoutPanel11, "tableLayoutPanel11");
            this.tableLayoutPanel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tableLayoutPanel11.Controls.Add(this.cb_HrsInterval, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            // 
            // cb_HrsInterval
            // 
            resources.ApplyResources(this.cb_HrsInterval, "cb_HrsInterval");
            this.cb_HrsInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_HrsInterval.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.cb_HrsInterval.FormattingEnabled = true;
            this.cb_HrsInterval.Name = "cb_HrsInterval";
            this.cb_HrsInterval.SelectedIndexChanged += new System.EventHandler(this.cb_HrsInterval_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label2.Name = "label2";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel10, "tableLayoutPanel10");
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CommunLog_MCSCmdID);
            this.panel1.Name = "panel1";
            // 
            // CommunLog_MCSCmdID
            // 
            this.CommunLog_MCSCmdID.BackColor = System.Drawing.Color.White;
            this.CommunLog_MCSCmdID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.CommunLog_MCSCmdID, "CommunLog_MCSCmdID");
            this.CommunLog_MCSCmdID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.CommunLog_MCSCmdID.Name = "CommunLog_MCSCmdID";
            this.CommunLog_MCSCmdID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommunLog_MCSCmdID_KeyPress);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label3.Name = "label3";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.m_StartDTCbx);
            this.panel4.Name = "panel4";
            // 
            // m_StartDTCbx
            // 
            resources.ApplyResources(this.m_StartDTCbx, "m_StartDTCbx");
            this.m_StartDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_StartDTCbx.Name = "m_StartDTCbx";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label7.Name = "label7";
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel13, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel12, 0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel13, "tableLayoutPanel13");
            this.tableLayoutPanel13.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label4.Name = "label4";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.CommunLog_VhID);
            this.panel2.Name = "panel2";
            // 
            // CommunLog_VhID
            // 
            this.CommunLog_VhID.BackColor = System.Drawing.Color.White;
            this.CommunLog_VhID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.CommunLog_VhID, "CommunLog_VhID");
            this.CommunLog_VhID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.CommunLog_VhID.Name = "CommunLog_VhID";
            this.CommunLog_VhID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommunLog_VhID_KeyPress);
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel12, "tableLayoutPanel12");
            this.tableLayoutPanel12.Controls.Add(this.panel8, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            // 
            // panel8
            // 
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.m_EndDTCbx);
            this.panel8.Name = "panel8";
            // 
            // m_EndDTCbx
            // 
            resources.ApplyResources(this.m_EndDTCbx, "m_EndDTCbx");
            this.m_EndDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_EndDTCbx.Name = "m_EndDTCbx";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label6.Name = "label6";
            // 
            // m_AlarmSetTimeCb
            // 
            resources.ApplyResources(this.m_AlarmSetTimeCb, "m_AlarmSetTimeCb");
            this.m_AlarmSetTimeCb.Checked = true;
            this.m_AlarmSetTimeCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_AlarmSetTimeCb.Name = "m_AlarmSetTimeCb";
            this.m_AlarmSetTimeCb.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.elementHost1, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.Last_3days, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.Last_2days, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.Last_24hrs, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.Last_12hrs, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.Last_4hrs, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.Last_1hrs, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.Last_30mins, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.linkLabel2, 7, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // Last_3days
            // 
            this.Last_3days.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_3days, "Last_3days");
            this.Last_3days.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_3days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_3days.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_3days.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_3days.Name = "Last_3days";
            this.Last_3days.TabStop = true;
            this.Last_3days.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_3days.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_3days_LinkClicked);
            // 
            // Last_2days
            // 
            this.Last_2days.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_2days, "Last_2days");
            this.Last_2days.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_2days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_2days.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_2days.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_2days.Name = "Last_2days";
            this.Last_2days.TabStop = true;
            this.Last_2days.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_2days.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_2days_LinkClicked);
            // 
            // Last_24hrs
            // 
            this.Last_24hrs.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_24hrs, "Last_24hrs");
            this.Last_24hrs.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_24hrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_24hrs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_24hrs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_24hrs.Name = "Last_24hrs";
            this.Last_24hrs.TabStop = true;
            this.Last_24hrs.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_24hrs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_24hrs_LinkClicked);
            // 
            // Last_12hrs
            // 
            this.Last_12hrs.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_12hrs, "Last_12hrs");
            this.Last_12hrs.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_12hrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_12hrs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_12hrs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_12hrs.Name = "Last_12hrs";
            this.Last_12hrs.TabStop = true;
            this.Last_12hrs.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_12hrs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_12hrs_LinkClicked);
            // 
            // Last_4hrs
            // 
            this.Last_4hrs.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_4hrs, "Last_4hrs");
            this.Last_4hrs.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_4hrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_4hrs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_4hrs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_4hrs.Name = "Last_4hrs";
            this.Last_4hrs.TabStop = true;
            this.Last_4hrs.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_4hrs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_4hrs_LinkClicked);
            // 
            // Last_1hrs
            // 
            this.Last_1hrs.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_1hrs, "Last_1hrs");
            this.Last_1hrs.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_1hrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_1hrs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_1hrs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_1hrs.Name = "Last_1hrs";
            this.Last_1hrs.TabStop = true;
            this.Last_1hrs.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_1hrs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_1hrs_LinkClicked);
            // 
            // Last_30mins
            // 
            this.Last_30mins.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.Last_30mins, "Last_30mins");
            this.Last_30mins.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_30mins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_30mins.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Last_30mins.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_30mins.Name = "Last_30mins";
            this.Last_30mins.TabStop = true;
            this.Last_30mins.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.Last_30mins.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Last_30mins_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            resources.ApplyResources(this.linkLabel2, "linkLabel2");
            this.linkLabel2.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.linkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.TabStop = true;
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // elementHost1
            // 
            resources.ApplyResources(this.elementHost1, "elementHost1");
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Child = this.uctl_ElasticQuery_sys_process_log;
            // 
            // uc_CommunicationLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "uc_CommunicationLog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox m_AlarmSetTimeCb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Panel panel6;
        private Controller.uc_btn_Custom btn_Search;
        private Controller.uc_btn_Custom btn_Close;
        private LinkLabel Last_30mins;
        private LinkLabel linkLabel2;
        private Panel panel8;
        private DateTimePicker m_EndDTCbx;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel6;
        private LinkLabel Last_1hrs;
        private LinkLabel Last_4hrs;
        private LinkLabel Last_12hrs;
        private LinkLabel Last_24hrs;
        private LinkLabel Last_2days;
        private LinkLabel Last_3days;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel12;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel11;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label7;
        private TableLayoutPanel tableLayoutPanel13;
        private TableLayoutPanel tableLayoutPanel7;
        private Panel panel1;
        private Label label3;
        private RichTextBox CommunLog_MCSCmdID;
        private Panel panel4;
        private DateTimePicker m_StartDTCbx;
        private Label label4;
        private ComboBox cb_HrsInterval;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private ohxc.winform.UI.Components.uctl_ElasticQuery_System_Process uctl_ElasticQuery_sys_process_log;
        private Panel panel2;
        private RichTextBox CommunLog_VhID;

        public PaintEventHandler tableLayoutPanel1_Paint { get; private set; }
    }
}