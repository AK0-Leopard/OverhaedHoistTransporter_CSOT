using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class uc_MTLMTSMaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_MTLMTSMaint));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_AlarmSetTimeCb = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.cb_VhID = new System.Windows.Forms.ComboBox();
            this.panel22 = new System.Windows.Forms.Panel();
            this.btn_AutoL = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.panel23 = new System.Windows.Forms.Panel();
            this.btn_AutoR = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.panel24 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel25 = new System.Windows.Forms.Panel();
            this.cb_MvToStation = new System.Windows.Forms.ComboBox();
            this.panel26 = new System.Windows.Forms.Panel();
            this.btn_Cmd = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_CancelCmd = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.panel27 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Close = new com.mirle.ibg3k0.bc.winform.UI.Controller.uc_btn_Custom();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_MTS2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.uc_MTS2Status = new com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl.uc_MTS2Status();
            this.uc_MTLMTS1Status = new com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl.uc_MTLMTS1Status();
            this.lab_MTLMTS1 = new System.Windows.Forms.Label();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.vehicleObjToShowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vehicleObjToShowBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel26.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleObjToShowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleObjToShowBindingSource1)).BeginInit();
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
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_AlarmSetTimeCb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel27, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_Close, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(34)))));
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Controls.Add(this.label2);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(34)))));
            this.label2.Name = "label2";
            // 
            // m_AlarmSetTimeCb
            // 
            resources.ApplyResources(this.m_AlarmSetTimeCb, "m_AlarmSetTimeCb");
            this.m_AlarmSetTimeCb.Checked = true;
            this.m_AlarmSetTimeCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_AlarmSetTimeCb.Name = "m_AlarmSetTimeCb";
            this.m_AlarmSetTimeCb.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this.label3);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(34)))));
            this.label3.Name = "label3";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.panel20, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel21, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel22, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel23, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel24, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel25, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel26, 4, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.label4);
            resources.ApplyResources(this.panel20, "panel20");
            this.panel20.Name = "panel20";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label4.Name = "label4";
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.cb_VhID);
            resources.ApplyResources(this.panel21, "panel21");
            this.panel21.Name = "panel21";
            // 
            // cb_VhID
            // 
            this.cb_VhID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cb_VhID, "cb_VhID");
            this.cb_VhID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.cb_VhID.FormattingEnabled = true;
            this.cb_VhID.Items.AddRange(new object[] {
            resources.GetString("cb_VhID.Items"),
            resources.GetString("cb_VhID.Items1"),
            resources.GetString("cb_VhID.Items2"),
            resources.GetString("cb_VhID.Items3")});
            this.cb_VhID.Name = "cb_VhID";
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.btn_AutoL);
            resources.ApplyResources(this.panel22, "panel22");
            this.panel22.Name = "panel22";
            // 
            // btn_AutoL
            // 
            this.btn_AutoL.BackColor = System.Drawing.Color.Transparent;
            this.btn_AutoL.ButtonForeColor = System.Drawing.Color.White;
            this.btn_AutoL.ButtonName = "Auto Local";
            resources.ApplyResources(this.btn_AutoL, "btn_AutoL");
            this.btn_AutoL.Name = "btn_AutoL";
            this.btn_AutoL.Button_Click += new System.EventHandler(this.btn_AutoL_Button_Click);
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.btn_AutoR);
            resources.ApplyResources(this.panel23, "panel23");
            this.panel23.Name = "panel23";
            // 
            // btn_AutoR
            // 
            this.btn_AutoR.BackColor = System.Drawing.Color.Transparent;
            this.btn_AutoR.ButtonForeColor = System.Drawing.Color.White;
            this.btn_AutoR.ButtonName = "Auto Remote";
            resources.ApplyResources(this.btn_AutoR, "btn_AutoR");
            this.btn_AutoR.Name = "btn_AutoR";
            this.btn_AutoR.Button_Click += new System.EventHandler(this.btn_AutoR_Button_Click);
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.label5);
            resources.ApplyResources(this.panel24, "panel24");
            this.panel24.Name = "panel24";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.label5.Name = "label5";
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.cb_MvToStation);
            resources.ApplyResources(this.panel25, "panel25");
            this.panel25.Name = "panel25";
            // 
            // cb_MvToStation
            // 
            this.cb_MvToStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cb_MvToStation, "cb_MvToStation");
            this.cb_MvToStation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.cb_MvToStation.FormattingEnabled = true;
            this.cb_MvToStation.Items.AddRange(new object[] {
            resources.GetString("cb_MvToStation.Items"),
            resources.GetString("cb_MvToStation.Items1"),
            resources.GetString("cb_MvToStation.Items2")});
            this.cb_MvToStation.Name = "cb_MvToStation";
            // 
            // panel26
            // 
            this.panel26.Controls.Add(this.btn_Cmd);
            resources.ApplyResources(this.panel26, "panel26");
            this.panel26.Name = "panel26";
            // 
            // btn_Cmd
            // 
            this.btn_Cmd.BackColor = System.Drawing.Color.Transparent;
            this.btn_Cmd.ButtonForeColor = System.Drawing.Color.White;
            this.btn_Cmd.ButtonName = "Command";
            resources.ApplyResources(this.btn_Cmd, "btn_Cmd");
            this.btn_Cmd.Name = "btn_Cmd";
            this.btn_Cmd.Button_Click += new System.EventHandler(this.btn_Cmd_Button_Click);
            this.btn_Cmd.Load += new System.EventHandler(this.btn_Cmd_Load);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btn_CancelCmd, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btn_CancelCmd
            // 
            this.btn_CancelCmd.BackColor = System.Drawing.Color.Transparent;
            this.btn_CancelCmd.ButtonForeColor = System.Drawing.Color.White;
            this.btn_CancelCmd.ButtonName = "Cancel Command";
            resources.ApplyResources(this.btn_CancelCmd, "btn_CancelCmd");
            this.btn_CancelCmd.Name = "btn_CancelCmd";
            this.btn_CancelCmd.Button_Click += new System.EventHandler(this.btn_CancelCmd_Button_Click);
            // 
            // panel27
            // 
            this.panel27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel27.Controls.Add(this.dataGridView2);
            resources.ApplyResources(this.panel27, "panel27");
            this.panel27.Name = "panel27";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridView2, "dataGridView2");
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 14F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(223)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Column8
            // 
            this.Column8.Frozen = true;
            resources.ApplyResources(this.Column8, "Column8");
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.Frozen = true;
            resources.ApplyResources(this.Column9, "Column9");
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.Frozen = true;
            resources.ApplyResources(this.Column10, "Column10");
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.Frozen = true;
            resources.ApplyResources(this.Column11, "Column11");
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.Frozen = true;
            resources.ApplyResources(this.Column12, "Column12");
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.Frozen = true;
            resources.ApplyResources(this.Column13, "Column13");
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.ButtonForeColor = System.Drawing.Color.White;
            this.btn_Close.ButtonName = "Close";
            resources.ApplyResources(this.btn_Close, "btn_Close");
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Button_Click += new System.EventHandler(this.btn_Close_Button_Click);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.lab_MTS2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel8, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lab_MTLMTS1, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel4, 4);
            // 
            // lab_MTS2
            // 
            resources.ApplyResources(this.lab_MTS2, "lab_MTS2");
            this.lab_MTS2.BackColor = System.Drawing.Color.White;
            this.lab_MTS2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(34)))));
            this.lab_MTS2.Name = "lab_MTS2";
            this.lab_MTS2.Click += new System.EventHandler(this.lab_MTS2_Click);
            this.lab_MTS2.MouseEnter += new System.EventHandler(this.lab_MTS2_MouseEnter);
            this.lab_MTS2.MouseLeave += new System.EventHandler(this.lab_MTS2_MouseLeave);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.tableLayoutPanel4.SetColumnSpan(this.panel6, 4);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel4.SetColumnSpan(this.panel8, 5);
            this.panel8.Controls.Add(this.uc_MTS2Status);
            this.panel8.Controls.Add(this.uc_MTLMTS1Status);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // uc_MTS2Status
            // 
            resources.ApplyResources(this.uc_MTS2Status, "uc_MTS2Status");
            this.uc_MTS2Status.Name = "uc_MTS2Status";
            // 
            // uc_MTLMTS1Status
            // 
            resources.ApplyResources(this.uc_MTLMTS1Status, "uc_MTLMTS1Status");
            this.uc_MTLMTS1Status.Name = "uc_MTLMTS1Status";
            // 
            // lab_MTLMTS1
            // 
            resources.ApplyResources(this.lab_MTLMTS1, "lab_MTLMTS1");
            this.lab_MTLMTS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.lab_MTLMTS1.ForeColor = System.Drawing.Color.White;
            this.lab_MTLMTS1.Name = "lab_MTLMTS1";
            this.lab_MTLMTS1.Click += new System.EventHandler(this.lab_MTLMTS1_Click);
            this.lab_MTLMTS1.MouseEnter += new System.EventHandler(this.lab_MTLMTS1_MouseEnter);
            this.lab_MTLMTS1.MouseLeave += new System.EventHandler(this.lab_MTLMTS1_MouseLeave);
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // vehicleObjToShowBindingSource
            // 
            this.vehicleObjToShowBindingSource.DataSource = typeof(com.mirle.ibg3k0.ohxc.winform.ObjectRelay.VehicleObjToShow);
            // 
            // vehicleObjToShowBindingSource1
            // 
            this.vehicleObjToShowBindingSource1.DataSource = typeof(com.mirle.ibg3k0.ohxc.winform.ObjectRelay.VehicleObjToShow);
            // 
            // uc_MTLMTSMaint
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "uc_MTLMTSMaint";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel26.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vehicleObjToShowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleObjToShowBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox m_AlarmSetTimeCb;
        private BindingSource vehicleObjToShowBindingSource;
        private Panel panel2;
        private Label label2;
        private Panel panel3;
        private Label label3;
        private Panel panel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label5;
        private Label label4;
        private ComboBox cb_VhID;
        private Controller.uc_btn_Custom btn_AutoR;
        private Controller.uc_btn_Custom btn_Cmd;
        private Controller.uc_btn_Custom btn_AutoL;
        private ComboBox cb_MvToStation;
        private TableLayoutPanel tableLayoutPanel3;
        private Controller.uc_btn_Custom btn_CancelCmd;
        private Panel panel20;
        private Panel panel21;
        private Panel panel22;
        private Panel panel23;
        private Panel panel24;
        private Panel panel25;
        private Panel panel26;
        private Panel panel27;
        private DataGridView dataGridView2;
        private Controller.uc_btn_Custom btn_Close;
        private TableLayoutPanel tableLayoutPanel4;
        private Panel panel6;
        private Panel panel8;
        private BindingSource vehicleObjToShowBindingSource1;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private ohxc.winform.UI.Components.MyUserControl.uc_MTS2Status uc_MTS2Status;
        private ohxc.winform.UI.Components.MyUserControl.uc_MTLMTS1Status uc_MTLMTS1Status;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private Label lab_MTLMTS1;
        private Label lab_MTS2;

        public PaintEventHandler tableLayoutPanel1_Paint { get; private set; }
    }
}