namespace com.mirle.ibg3k0.ohxc.winform.UI
{
    partial class LogQueryForm
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
            this.tab_main = new System.Windows.Forms.TabControl();
            this.tab_mcs_cmd = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_MCS_Cmd_Query_StartDTCbx = new System.Windows.Forms.DateTimePicker();
            this.m_MCS_Cmd_Query_EndDTCbx = new System.Windows.Forms.DateTimePicker();
            this.btn_search_mcs_cmd_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.uctl_ElasticQuery_CMDExcute_1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uctl_ElasticQuery();
            this.tab_mcs_cmd_detail = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.uctl_ElasticQuery_sys_process_log = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uctl_ElasticQuery_System_Process();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dt_sys_process_log_start_time = new System.Windows.Forms.DateTimePicker();
            this.dt_sys_process_log_end_time = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_sys_process_log_cmd_id = new System.Windows.Forms.TextBox();
            this.btn_sys_process_log_search = new System.Windows.Forms.Button();
            this.tab_vh_act_log = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.elementHost3 = new System.Windows.Forms.Integration.ElementHost();
            this.uctl_ElasticQuery_System_Process1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uctl_ElasticQuery_System_Process();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_sys_process_log_vh_search = new System.Windows.Forms.Button();
            this.cb_vh_id = new System.Windows.Forms.ComboBox();
            this.tab_main.SuspendLayout();
            this.tab_mcs_cmd.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tab_mcs_cmd_detail.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tab_vh_act_log.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_main
            // 
            this.tab_main.Controls.Add(this.tab_mcs_cmd);
            this.tab_main.Controls.Add(this.tab_mcs_cmd_detail);
            this.tab_main.Controls.Add(this.tab_vh_act_log);
            this.tab_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_main.Location = new System.Drawing.Point(0, 0);
            this.tab_main.Name = "tab_main";
            this.tab_main.SelectedIndex = 0;
            this.tab_main.Size = new System.Drawing.Size(1214, 747);
            this.tab_main.TabIndex = 0;
            // 
            // tab_mcs_cmd
            // 
            this.tab_mcs_cmd.Controls.Add(this.tableLayoutPanel1);
            this.tab_mcs_cmd.Location = new System.Drawing.Point(4, 31);
            this.tab_mcs_cmd.Name = "tab_mcs_cmd";
            this.tab_mcs_cmd.Padding = new System.Windows.Forms.Padding(3);
            this.tab_mcs_cmd.Size = new System.Drawing.Size(1206, 712);
            this.tab_mcs_cmd.TabIndex = 0;
            this.tab_mcs_cmd.Text = "MCS Command";
            this.tab_mcs_cmd.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.elementHost1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.49133F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.50867F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1200, 706);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.m_MCS_Cmd_Query_StartDTCbx, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_MCS_Cmd_Query_EndDTCbx, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_search_mcs_cmd_search, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1194, 32);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // m_MCS_Cmd_Query_StartDTCbx
            // 
            this.m_MCS_Cmd_Query_StartDTCbx.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_MCS_Cmd_Query_StartDTCbx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.m_MCS_Cmd_Query_StartDTCbx.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.m_MCS_Cmd_Query_StartDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_MCS_Cmd_Query_StartDTCbx.Location = new System.Drawing.Point(103, 3);
            this.m_MCS_Cmd_Query_StartDTCbx.Name = "m_MCS_Cmd_Query_StartDTCbx";
            this.m_MCS_Cmd_Query_StartDTCbx.Size = new System.Drawing.Size(214, 26);
            this.m_MCS_Cmd_Query_StartDTCbx.TabIndex = 69;
            // 
            // m_MCS_Cmd_Query_EndDTCbx
            // 
            this.m_MCS_Cmd_Query_EndDTCbx.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_MCS_Cmd_Query_EndDTCbx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.m_MCS_Cmd_Query_EndDTCbx.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.m_MCS_Cmd_Query_EndDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_MCS_Cmd_Query_EndDTCbx.Location = new System.Drawing.Point(373, 3);
            this.m_MCS_Cmd_Query_EndDTCbx.Name = "m_MCS_Cmd_Query_EndDTCbx";
            this.m_MCS_Cmd_Query_EndDTCbx.Size = new System.Drawing.Size(214, 26);
            this.m_MCS_Cmd_Query_EndDTCbx.TabIndex = 70;
            // 
            // btn_search_mcs_cmd_search
            // 
            this.btn_search_mcs_cmd_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_search_mcs_cmd_search.Location = new System.Drawing.Point(1088, 3);
            this.btn_search_mcs_cmd_search.Name = "btn_search_mcs_cmd_search";
            this.btn_search_mcs_cmd_search.Size = new System.Drawing.Size(103, 26);
            this.btn_search_mcs_cmd_search.TabIndex = 71;
            this.btn_search_mcs_cmd_search.Text = "Search";
            this.btn_search_mcs_cmd_search.UseVisualStyleBackColor = true;
            this.btn_search_mcs_cmd_search.Click += new System.EventHandler(this.btn_search_mcs_cmd_query_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 72;
            this.label1.Text = "Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(323, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 32);
            this.label2.TabIndex = 73;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 41);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1194, 662);
            this.elementHost1.TabIndex = 2;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.uctl_ElasticQuery_CMDExcute_1;
            // 
            // tab_mcs_cmd_detail
            // 
            this.tab_mcs_cmd_detail.Controls.Add(this.tableLayoutPanel3);
            this.tab_mcs_cmd_detail.Location = new System.Drawing.Point(4, 31);
            this.tab_mcs_cmd_detail.Name = "tab_mcs_cmd_detail";
            this.tab_mcs_cmd_detail.Padding = new System.Windows.Forms.Padding(3);
            this.tab_mcs_cmd_detail.Size = new System.Drawing.Size(1206, 712);
            this.tab_mcs_cmd_detail.TabIndex = 1;
            this.tab_mcs_cmd_detail.Text = "System Process";
            this.tab_mcs_cmd_detail.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.elementHost2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.27168F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.72832F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1200, 706);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // elementHost2
            // 
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(3, 82);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(1194, 621);
            this.elementHost2.TabIndex = 0;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.uctl_ElasticQuery_sys_process_log;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.dt_sys_process_log_start_time, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.dt_sys_process_log_end_time, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.txt_sys_process_log_cmd_id, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.btn_sys_process_log_search, 5, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1194, 73);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // dt_sys_process_log_start_time
            // 
            this.dt_sys_process_log_start_time.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dt_sys_process_log_start_time.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dt_sys_process_log_start_time.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.dt_sys_process_log_start_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_sys_process_log_start_time.Location = new System.Drawing.Point(123, 5);
            this.dt_sys_process_log_start_time.Name = "dt_sys_process_log_start_time";
            this.dt_sys_process_log_start_time.Size = new System.Drawing.Size(214, 26);
            this.dt_sys_process_log_start_time.TabIndex = 70;
            // 
            // dt_sys_process_log_end_time
            // 
            this.dt_sys_process_log_end_time.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dt_sys_process_log_end_time.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dt_sys_process_log_end_time.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.dt_sys_process_log_end_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_sys_process_log_end_time.Location = new System.Drawing.Point(363, 5);
            this.dt_sys_process_log_end_time.Name = "dt_sys_process_log_end_time";
            this.dt_sys_process_log_end_time.Size = new System.Drawing.Size(214, 26);
            this.dt_sys_process_log_end_time.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 22);
            this.label3.TabIndex = 75;
            this.label3.Text = "~";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 22);
            this.label4.TabIndex = 76;
            this.label4.Text = "Time";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 22);
            this.label6.TabIndex = 76;
            this.label6.Text = "MCS CMD ID";
            // 
            // txt_sys_process_log_cmd_id
            // 
            this.txt_sys_process_log_cmd_id.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_sys_process_log_cmd_id.Location = new System.Drawing.Point(123, 39);
            this.txt_sys_process_log_cmd_id.Name = "txt_sys_process_log_cmd_id";
            this.txt_sys_process_log_cmd_id.Size = new System.Drawing.Size(214, 30);
            this.txt_sys_process_log_cmd_id.TabIndex = 73;
            // 
            // btn_sys_process_log_search
            // 
            this.btn_sys_process_log_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_sys_process_log_search.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sys_process_log_search.Location = new System.Drawing.Point(1116, 39);
            this.btn_sys_process_log_search.Name = "btn_sys_process_log_search";
            this.btn_sys_process_log_search.Size = new System.Drawing.Size(75, 31);
            this.btn_sys_process_log_search.TabIndex = 74;
            this.btn_sys_process_log_search.Text = "Search";
            this.btn_sys_process_log_search.UseVisualStyleBackColor = true;
            this.btn_sys_process_log_search.Click += new System.EventHandler(this.btn_sys_process_log_search_Click);
            // 
            // tab_vh_act_log
            // 
            this.tab_vh_act_log.Controls.Add(this.tableLayoutPanel5);
            this.tab_vh_act_log.Location = new System.Drawing.Point(4, 31);
            this.tab_vh_act_log.Name = "tab_vh_act_log";
            this.tab_vh_act_log.Padding = new System.Windows.Forms.Padding(3);
            this.tab_vh_act_log.Size = new System.Drawing.Size(1206, 712);
            this.tab_vh_act_log.TabIndex = 2;
            this.tab_vh_act_log.Text = "Vehicle Action";
            this.tab_vh_act_log.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.elementHost3, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.99422F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.00578F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1200, 706);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // elementHost3
            // 
            this.elementHost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost3.Location = new System.Drawing.Point(3, 87);
            this.elementHost3.Name = "elementHost3";
            this.elementHost3.Size = new System.Drawing.Size(1194, 616);
            this.elementHost3.TabIndex = 0;
            this.elementHost3.Text = "elementHost3";
            this.elementHost3.Child = this.uctl_ElasticQuery_System_Process1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 6;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.dateTimePicker1, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.dateTimePicker2, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.btn_sys_process_log_vh_search, 5, 1);
            this.tableLayoutPanel6.Controls.Add(this.cb_vh_id, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1194, 78);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(123, 6);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker1.TabIndex = 70;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePicker2.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(363, 6);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker2.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 22);
            this.label5.TabIndex = 75;
            this.label5.Text = "~";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 22);
            this.label7.TabIndex = 76;
            this.label7.Text = "Time";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 22);
            this.label8.TabIndex = 76;
            this.label8.Text = "VH ID";
            // 
            // btn_sys_process_log_vh_search
            // 
            this.btn_sys_process_log_vh_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_sys_process_log_vh_search.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sys_process_log_vh_search.Location = new System.Drawing.Point(1116, 42);
            this.btn_sys_process_log_vh_search.Name = "btn_sys_process_log_vh_search";
            this.btn_sys_process_log_vh_search.Size = new System.Drawing.Size(75, 33);
            this.btn_sys_process_log_vh_search.TabIndex = 74;
            this.btn_sys_process_log_vh_search.Text = "Search";
            this.btn_sys_process_log_vh_search.UseVisualStyleBackColor = true;
            this.btn_sys_process_log_vh_search.Click += new System.EventHandler(this.btn_sys_process_log_vh_search_Click);
            // 
            // cb_vh_id
            // 
            this.cb_vh_id.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_vh_id.FormattingEnabled = true;
            this.cb_vh_id.Location = new System.Drawing.Point(123, 48);
            this.cb_vh_id.Name = "cb_vh_id";
            this.cb_vh_id.Size = new System.Drawing.Size(214, 30);
            this.cb_vh_id.TabIndex = 77;
            // 
            // LogQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 747);
            this.Controls.Add(this.tab_main);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "LogQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogQueryForm";
            this.tab_main.ResumeLayout(false);
            this.tab_mcs_cmd.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tab_mcs_cmd_detail.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tab_vh_act_log.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_main;
        private System.Windows.Forms.TabPage tab_mcs_cmd;
        private System.Windows.Forms.TabPage tab_mcs_cmd_detail;
        private System.Windows.Forms.TabPage tab_vh_act_log;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DateTimePicker m_MCS_Cmd_Query_StartDTCbx;
        private System.Windows.Forms.DateTimePicker m_MCS_Cmd_Query_EndDTCbx;
        private System.Windows.Forms.Button btn_search_mcs_cmd_search;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private Components.uctl_ElasticQuery uctl_ElasticQuery_CMDExcute_1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private Components.uctl_ElasticQuery_System_Process uctl_ElasticQuery_sys_process_log;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DateTimePicker dt_sys_process_log_end_time;
        private System.Windows.Forms.DateTimePicker dt_sys_process_log_start_time;
        private System.Windows.Forms.TextBox txt_sys_process_log_cmd_id;
        private System.Windows.Forms.Button btn_sys_process_log_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Integration.ElementHost elementHost3;
        private Components.uctl_ElasticQuery_System_Process uctl_ElasticQuery_System_Process1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_sys_process_log_vh_search;
        private System.Windows.Forms.ComboBox cb_vh_id;
    }
}