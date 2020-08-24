namespace com.mirle.ibg3k0.bc.winform.UI.Components
{
    partial class uctl_Map
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uctl_Map));
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_Map = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_DynamicCmd = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.trackBar_scale = new System.Windows.Forms.TrackBar();
            this.lbl_maxScale = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.pic_fuu_status = new System.Windows.Forms.PictureBox();
            this.dgv_transCMD = new System.Windows.Forms.DataGridView();
            this.CMD_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OHTC_CMD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VH_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARRIER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HOSTSOURCE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HOSTDESTINATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRIORITY_SUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_START_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pic_safty_door_status = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_MonitorRoadContorlStaus = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_scale)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_fuu_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_transCMD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_safty_door_status)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 300;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1872, 862);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pnl_Map);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1866, 701);
            this.panel1.TabIndex = 2;
            // 
            // pnl_Map
            // 
            this.pnl_Map.Location = new System.Drawing.Point(1, 0);
            this.pnl_Map.Name = "pnl_Map";
            this.pnl_Map.Size = new System.Drawing.Size(1208, 631);
            this.pnl_Map.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 787);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1872, 75);
            this.tableLayoutPanel2.TabIndex = 96;
            // 
            // chk_DynamicCmd
            // 
            this.chk_DynamicCmd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_DynamicCmd.AutoSize = true;
            this.chk_DynamicCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chk_DynamicCmd.ForeColor = System.Drawing.Color.Transparent;
            this.chk_DynamicCmd.Location = new System.Drawing.Point(14, 5);
            this.chk_DynamicCmd.Name = "chk_DynamicCmd";
            this.chk_DynamicCmd.Size = new System.Drawing.Size(166, 24);
            this.chk_DynamicCmd.TabIndex = 94;
            this.chk_DynamicCmd.Text = "Dynamic Command";
            this.chk_DynamicCmd.UseVisualStyleBackColor = true;
            this.chk_DynamicCmd.CheckedChanged += new System.EventHandler(this.chk_DynamicCmd_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.trackBar_scale);
            this.panel2.Controls.Add(this.lbl_maxScale);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1602, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 75);
            this.panel2.TabIndex = 2;
            // 
            // trackBar_scale
            // 
            this.trackBar_scale.LargeChange = 1;
            this.trackBar_scale.Location = new System.Drawing.Point(3, 27);
            this.trackBar_scale.Maximum = 5;
            this.trackBar_scale.Minimum = 1;
            this.trackBar_scale.Name = "trackBar_scale";
            this.trackBar_scale.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBar_scale.Size = new System.Drawing.Size(260, 45);
            this.trackBar_scale.TabIndex = 88;
            this.trackBar_scale.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_scale.Value = 5;
            this.trackBar_scale.Scroll += new System.EventHandler(this.trackBar_scale_Scroll);
            // 
            // lbl_maxScale
            // 
            this.lbl_maxScale.AutoSize = true;
            this.lbl_maxScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_maxScale.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_maxScale.Location = new System.Drawing.Point(232, 11);
            this.lbl_maxScale.Name = "lbl_maxScale";
            this.lbl_maxScale.Size = new System.Drawing.Size(30, 17);
            this.lbl_maxScale.TabIndex = 90;
            this.lbl_maxScale.Text = "10x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 17);
            this.label1.TabIndex = 89;
            this.label1.Text = "1x";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel4.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.pic_fuu_status, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.dgv_transCMD, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pic_safty_door_status, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1872, 80);
            this.tableLayoutPanel4.TabIndex = 97;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Font = new System.Drawing.Font("Consolas", 10.5F);
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(1695, 30);
            this.button1.Margin = new System.Windows.Forms.Padding(0, 19, 0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 38);
            this.button1.TabIndex = 97;
            this.button1.Text = "Emergency Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // pic_fuu_status
            // 
            this.pic_fuu_status.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_fuu_status.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.FUU;
            this.pic_fuu_status.Location = new System.Drawing.Point(1758, 16);
            this.pic_fuu_status.Margin = new System.Windows.Forms.Padding(0);
            this.pic_fuu_status.Name = "pic_fuu_status";
            this.pic_fuu_status.Size = new System.Drawing.Size(47, 47);
            this.pic_fuu_status.TabIndex = 4;
            this.pic_fuu_status.TabStop = false;
            this.pic_fuu_status.Visible = false;
            // 
            // dgv_transCMD
            // 
            this.dgv_transCMD.AllowUserToAddRows = false;
            this.dgv_transCMD.AllowUserToDeleteRows = false;
            this.dgv_transCMD.AllowUserToResizeColumns = false;
            this.dgv_transCMD.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgv_transCMD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_transCMD.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.dgv_transCMD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_transCMD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_transCMD.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_transCMD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_transCMD.ColumnHeadersHeight = 30;
            this.dgv_transCMD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CMD_ID,
            this.OHTC_CMD,
            this.VH_ID,
            this.CARRIER_ID,
            this.HOSTSOURCE,
            this.HOSTDESTINATION,
            this.PRIORITY_SUM,
            this.CMD_START_TIME});
            this.dgv_transCMD.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_transCMD.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_transCMD.EnableHeadersVisualStyles = false;
            this.dgv_transCMD.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.dgv_transCMD.Location = new System.Drawing.Point(20, 10);
            this.dgv_transCMD.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.dgv_transCMD.Name = "dgv_transCMD";
            this.dgv_transCMD.ReadOnly = true;
            this.dgv_transCMD.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_transCMD.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_transCMD.RowHeadersVisible = false;
            this.dgv_transCMD.RowHeadersWidth = 30;
            this.dgv_transCMD.RowTemplate.Height = 30;
            this.dgv_transCMD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_transCMD.Size = new System.Drawing.Size(1200, 60);
            this.dgv_transCMD.TabIndex = 96;
            this.dgv_transCMD.Visible = false;
            // 
            // CMD_ID
            // 
            this.CMD_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CMD_ID.DataPropertyName = "CMD_ID";
            this.CMD_ID.HeaderText = "MCS Command ID";
            this.CMD_ID.Name = "CMD_ID";
            this.CMD_ID.ReadOnly = true;
            this.CMD_ID.Width = 190;
            // 
            // OHTC_CMD
            // 
            this.OHTC_CMD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OHTC_CMD.DataPropertyName = "OHTC_CMD";
            this.OHTC_CMD.HeaderText = "OHTC Command ID";
            this.OHTC_CMD.Name = "OHTC_CMD";
            this.OHTC_CMD.ReadOnly = true;
            this.OHTC_CMD.Width = 150;
            // 
            // VH_ID
            // 
            this.VH_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VH_ID.DataPropertyName = "VH_ID";
            this.VH_ID.HeaderText = "Vehicle ID";
            this.VH_ID.Name = "VH_ID";
            this.VH_ID.ReadOnly = true;
            this.VH_ID.Width = 80;
            // 
            // CARRIER_ID
            // 
            this.CARRIER_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CARRIER_ID.DataPropertyName = "CARRIER_ID";
            this.CARRIER_ID.HeaderText = "Carrier ID";
            this.CARRIER_ID.Name = "CARRIER_ID";
            this.CARRIER_ID.ReadOnly = true;
            this.CARRIER_ID.Width = 80;
            // 
            // HOSTSOURCE
            // 
            this.HOSTSOURCE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.HOSTSOURCE.DataPropertyName = "HOSTSOURCE";
            this.HOSTSOURCE.HeaderText = "Source";
            this.HOSTSOURCE.Name = "HOSTSOURCE";
            this.HOSTSOURCE.ReadOnly = true;
            this.HOSTSOURCE.Width = 110;
            // 
            // HOSTDESTINATION
            // 
            this.HOSTDESTINATION.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.HOSTDESTINATION.DataPropertyName = "HOSTDESTINATION";
            this.HOSTDESTINATION.HeaderText = "Desrination";
            this.HOSTDESTINATION.Name = "HOSTDESTINATION";
            this.HOSTDESTINATION.ReadOnly = true;
            this.HOSTDESTINATION.Width = 110;
            // 
            // PRIORITY_SUM
            // 
            this.PRIORITY_SUM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PRIORITY_SUM.DataPropertyName = "PRIORITY_SUM";
            this.PRIORITY_SUM.HeaderText = "Priority";
            this.PRIORITY_SUM.Name = "PRIORITY_SUM";
            this.PRIORITY_SUM.ReadOnly = true;
            this.PRIORITY_SUM.Width = 60;
            // 
            // CMD_START_TIME
            // 
            this.CMD_START_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CMD_START_TIME.DataPropertyName = "CMD_START_TIME";
            this.CMD_START_TIME.HeaderText = "Start Time";
            this.CMD_START_TIME.Name = "CMD_START_TIME";
            this.CMD_START_TIME.ReadOnly = true;
            this.CMD_START_TIME.Width = 170;
            // 
            // pic_safty_door_status
            // 
            this.pic_safty_door_status.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_safty_door_status.Image = ((System.Drawing.Image)(resources.GetObject("pic_safty_door_status.Image")));
            this.pic_safty_door_status.Location = new System.Drawing.Point(1812, 10);
            this.pic_safty_door_status.Margin = new System.Windows.Forms.Padding(0);
            this.pic_safty_door_status.Name = "pic_safty_door_status";
            this.pic_safty_door_status.Size = new System.Drawing.Size(60, 60);
            this.pic_safty_door_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_safty_door_status.TabIndex = 7;
            this.pic_safty_door_status.TabStop = false;
            this.pic_safty_door_status.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.chk_MonitorRoadContorlStaus, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.chk_DynamicCmd, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1405, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(194, 69);
            this.tableLayoutPanel3.TabIndex = 95;
            // 
            // chk_MonitorRoadContorlStaus
            // 
            this.chk_MonitorRoadContorlStaus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_MonitorRoadContorlStaus.AutoSize = true;
            this.chk_MonitorRoadContorlStaus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chk_MonitorRoadContorlStaus.ForeColor = System.Drawing.Color.Transparent;
            this.chk_MonitorRoadContorlStaus.Location = new System.Drawing.Point(38, 39);
            this.chk_MonitorRoadContorlStaus.Name = "chk_MonitorRoadContorlStaus";
            this.chk_MonitorRoadContorlStaus.Size = new System.Drawing.Size(118, 24);
            this.chk_MonitorRoadContorlStaus.TabIndex = 95;
            this.chk_MonitorRoadContorlStaus.Text = "Road Status";
            this.chk_MonitorRoadContorlStaus.UseVisualStyleBackColor = true;
            this.chk_MonitorRoadContorlStaus.CheckedChanged += new System.EventHandler(this.chk_MonitorRoadContorlStaus_CheckedChanged);
            // 
            // uctl_Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "uctl_Map";
            this.Size = new System.Drawing.Size(1872, 862);
            this.Load += new System.EventHandler(this.uctl_Map_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_scale)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_fuu_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_transCMD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_safty_door_status)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chk_DynamicCmd;
        private System.Windows.Forms.Label lbl_maxScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_scale;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Map;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.PictureBox pic_safty_door_status;
        private System.Windows.Forms.PictureBox pic_fuu_status;
        private System.Windows.Forms.DataGridView dgv_transCMD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OHTC_CMD;
        private System.Windows.Forms.DataGridViewTextBoxColumn VH_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARRIER_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn HOSTSOURCE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HOSTDESTINATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRIORITY_SUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_START_TIME;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox chk_MonitorRoadContorlStaus;
    }
}
