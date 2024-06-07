namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class HistoryTransferForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_TransferCommandList = new System.Windows.Forms.DataGridView();
            this.cMDIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_CARRIER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_STATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_SOURCE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_DESTINATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_INSERT_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMD_START_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMDFINISHTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMDMCSObjToShowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.skinGroupBox4 = new CCWin.SkinControl.SkinGroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.m_EndDTCbx = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.m_StartDTCbx = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.skinGroupBox3 = new CCWin.SkinControl.SkinGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_DestinationIDCbx = new System.Windows.Forms.ComboBox();
            this.m_exportBtn = new CCWin.SkinControl.SkinButton();
            this.btnlSearch = new CCWin.SkinControl.SkinButton();
            this.skinGroupBox1 = new CCWin.SkinControl.SkinGroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.m_AlarmCodeTbl = new System.Windows.Forms.MaskedTextBox();
            this.skinGroupBox2 = new CCWin.SkinControl.SkinGroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.m_SourceIDCbx = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TransferCommandList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cMDMCSObjToShowBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.skinGroupBox4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.skinGroupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.skinGroupBox1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.skinGroupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_TransferCommandList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.80447F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.97765F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1602, 712);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dgv_TransferCommandList
            // 
            this.dgv_TransferCommandList.AllowUserToAddRows = false;
            this.dgv_TransferCommandList.AutoGenerateColumns = false;
            this.dgv_TransferCommandList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_TransferCommandList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_TransferCommandList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_TransferCommandList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TransferCommandList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cMDIDDataGridViewTextBoxColumn,
            this.CMD_CARRIER,
            this.CMD_STATE,
            this.CMD_SOURCE,
            this.CMD_DESTINATION,
            this.CMD_INSERT_TIME,
            this.CMD_START_TIME,
            this.cMDFINISHTIMEDataGridViewTextBoxColumn});
            this.dgv_TransferCommandList.DataSource = this.cMDMCSObjToShowBindingSource;
            this.dgv_TransferCommandList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_TransferCommandList.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_TransferCommandList.Location = new System.Drawing.Point(3, 119);
            this.dgv_TransferCommandList.MultiSelect = false;
            this.dgv_TransferCommandList.Name = "dgv_TransferCommandList";
            this.dgv_TransferCommandList.ReadOnly = true;
            this.dgv_TransferCommandList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_TransferCommandList.RowHeadersWidth = 51;
            this.dgv_TransferCommandList.RowTemplate.Height = 24;
            this.dgv_TransferCommandList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_TransferCommandList.Size = new System.Drawing.Size(1596, 590);
            this.dgv_TransferCommandList.TabIndex = 9;
            // 
            // cMDIDDataGridViewTextBoxColumn
            // 
            this.cMDIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cMDIDDataGridViewTextBoxColumn.DataPropertyName = "CMD_ID";
            this.cMDIDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.cMDIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cMDIDDataGridViewTextBoxColumn.Name = "cMDIDDataGridViewTextBoxColumn";
            this.cMDIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CMD_CARRIER
            // 
            this.CMD_CARRIER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMD_CARRIER.DataPropertyName = "CMD_CARRIER";
            this.CMD_CARRIER.HeaderText = "CARRIER";
            this.CMD_CARRIER.MinimumWidth = 6;
            this.CMD_CARRIER.Name = "CMD_CARRIER";
            this.CMD_CARRIER.ReadOnly = true;
            // 
            // CMD_STATE
            // 
            this.CMD_STATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMD_STATE.DataPropertyName = "CMD_STATE";
            this.CMD_STATE.HeaderText = "STATE";
            this.CMD_STATE.MinimumWidth = 6;
            this.CMD_STATE.Name = "CMD_STATE";
            this.CMD_STATE.ReadOnly = true;
            // 
            // CMD_SOURCE
            // 
            this.CMD_SOURCE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMD_SOURCE.DataPropertyName = "CMD_SOURCE";
            this.CMD_SOURCE.HeaderText = "SOURCE";
            this.CMD_SOURCE.MinimumWidth = 6;
            this.CMD_SOURCE.Name = "CMD_SOURCE";
            this.CMD_SOURCE.ReadOnly = true;
            // 
            // CMD_DESTINATION
            // 
            this.CMD_DESTINATION.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMD_DESTINATION.DataPropertyName = "CMD_DESTINATION";
            this.CMD_DESTINATION.HeaderText = "DESTINATION";
            this.CMD_DESTINATION.MinimumWidth = 6;
            this.CMD_DESTINATION.Name = "CMD_DESTINATION";
            this.CMD_DESTINATION.ReadOnly = true;
            // 
            // CMD_INSERT_TIME
            // 
            this.CMD_INSERT_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMD_INSERT_TIME.DataPropertyName = "CMD_INSERT_TIME";
            this.CMD_INSERT_TIME.HeaderText = "INSERT_TIME";
            this.CMD_INSERT_TIME.MinimumWidth = 6;
            this.CMD_INSERT_TIME.Name = "CMD_INSERT_TIME";
            this.CMD_INSERT_TIME.ReadOnly = true;
            // 
            // CMD_START_TIME
            // 
            this.CMD_START_TIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMD_START_TIME.DataPropertyName = "CMD_START_TIME";
            this.CMD_START_TIME.HeaderText = "START_TIME";
            this.CMD_START_TIME.MinimumWidth = 6;
            this.CMD_START_TIME.Name = "CMD_START_TIME";
            this.CMD_START_TIME.ReadOnly = true;
            // 
            // cMDFINISHTIMEDataGridViewTextBoxColumn
            // 
            this.cMDFINISHTIMEDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cMDFINISHTIMEDataGridViewTextBoxColumn.DataPropertyName = "CMD_FINISH_TIME";
            this.cMDFINISHTIMEDataGridViewTextBoxColumn.HeaderText = "END_TIME";
            this.cMDFINISHTIMEDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cMDFINISHTIMEDataGridViewTextBoxColumn.Name = "cMDFINISHTIMEDataGridViewTextBoxColumn";
            this.cMDFINISHTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cMDMCSObjToShowBindingSource
            // 
            this.cMDMCSObjToShowBindingSource.DataSource = typeof(com.mirle.ibg3k0.sc.ObjectRelay.CMDObjToShow);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1596, 110);
            this.panel1.TabIndex = 11;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 266F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.skinGroupBox4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel2, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.skinGroupBox1, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.skinGroupBox2, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1596, 110);
            this.tableLayoutPanel6.TabIndex = 85;
            // 
            // skinGroupBox4
            // 
            this.skinGroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.skinGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox4.BorderColor = System.Drawing.Color.Black;
            this.skinGroupBox4.Controls.Add(this.tableLayoutPanel3);
            this.skinGroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skinGroupBox4.Font = new System.Drawing.Font("Arial", 14F);
            this.skinGroupBox4.ForeColor = System.Drawing.Color.Black;
            this.skinGroupBox4.Location = new System.Drawing.Point(3, 3);
            this.skinGroupBox4.Name = "skinGroupBox4";
            this.skinGroupBox4.RectBackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox4.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox4.Size = new System.Drawing.Size(393, 104);
            this.skinGroupBox4.TabIndex = 77;
            this.skinGroupBox4.TabStop = false;
            this.skinGroupBox4.Text = "CMD Insert Time ";
            this.skinGroupBox4.TitleBorderColor = System.Drawing.Color.Black;
            this.skinGroupBox4.TitleRectBackColor = System.Drawing.Color.LightSteelBlue;
            this.skinGroupBox4.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.12903F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.87097F));
            this.tableLayoutPanel3.Controls.Add(this.m_EndDTCbx, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.m_StartDTCbx, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(387, 76);
            this.tableLayoutPanel3.TabIndex = 79;
            // 
            // m_EndDTCbx
            // 
            this.m_EndDTCbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_EndDTCbx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.m_EndDTCbx.Font = new System.Drawing.Font("Arial", 14F);
            this.m_EndDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_EndDTCbx.Location = new System.Drawing.Point(142, 42);
            this.m_EndDTCbx.Name = "m_EndDTCbx";
            this.m_EndDTCbx.Size = new System.Drawing.Size(242, 29);
            this.m_EndDTCbx.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 22);
            this.label3.TabIndex = 57;
            this.label3.Text = "From Time";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_StartDTCbx
            // 
            this.m_StartDTCbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_StartDTCbx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.m_StartDTCbx.Font = new System.Drawing.Font("Arial", 14F);
            this.m_StartDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_StartDTCbx.Location = new System.Drawing.Point(142, 4);
            this.m_StartDTCbx.Name = "m_StartDTCbx";
            this.m_StartDTCbx.Size = new System.Drawing.Size(242, 29);
            this.m_StartDTCbx.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 22);
            this.label1.TabIndex = 61;
            this.label1.Text = "End Time ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.skinGroupBox3);
            this.panel2.Controls.Add(this.m_exportBtn);
            this.panel2.Controls.Add(this.btnlSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(912, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(681, 104);
            this.panel2.TabIndex = 78;
            // 
            // skinGroupBox3
            // 
            this.skinGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox3.BorderColor = System.Drawing.Color.Black;
            this.skinGroupBox3.Controls.Add(this.tableLayoutPanel2);
            this.skinGroupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.skinGroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skinGroupBox3.Font = new System.Drawing.Font("Arial", 15F);
            this.skinGroupBox3.ForeColor = System.Drawing.Color.Black;
            this.skinGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.skinGroupBox3.Name = "skinGroupBox3";
            this.skinGroupBox3.RectBackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox3.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox3.Size = new System.Drawing.Size(254, 104);
            this.skinGroupBox3.TabIndex = 86;
            this.skinGroupBox3.TabStop = false;
            this.skinGroupBox3.Text = "DESTINATION";
            this.skinGroupBox3.TitleBorderColor = System.Drawing.Color.Black;
            this.skinGroupBox3.TitleRectBackColor = System.Drawing.Color.LightSteelBlue;
            this.skinGroupBox3.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.m_DestinationIDCbx, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 26);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(248, 75);
            this.tableLayoutPanel2.TabIndex = 84;
            // 
            // m_DestinationIDCbx
            // 
            this.m_DestinationIDCbx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_DestinationIDCbx.Font = new System.Drawing.Font("Arial", 14F);
            this.m_DestinationIDCbx.FormattingEnabled = true;
            this.m_DestinationIDCbx.Location = new System.Drawing.Point(17, 22);
            this.m_DestinationIDCbx.Name = "m_DestinationIDCbx";
            this.m_DestinationIDCbx.Size = new System.Drawing.Size(214, 30);
            this.m_DestinationIDCbx.TabIndex = 53;
            this.m_DestinationIDCbx.SelectedIndexChanged += new System.EventHandler(this.m_DestinationIDCbx_SelectedIndexChanged);
            // 
            // m_exportBtn
            // 
            this.m_exportBtn.BackColor = System.Drawing.Color.Transparent;
            this.m_exportBtn.BaseColor = System.Drawing.Color.LightGray;
            this.m_exportBtn.BorderColor = System.Drawing.Color.Black;
            this.m_exportBtn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.m_exportBtn.DownBack = null;
            this.m_exportBtn.DownBaseColor = System.Drawing.Color.RoyalBlue;
            this.m_exportBtn.Font = new System.Drawing.Font("Arial", 14.25F);
            this.m_exportBtn.Image = global::com.mirle.ibg3k0.bc.winform.Properties.Resources.export;
            this.m_exportBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_exportBtn.ImageSize = new System.Drawing.Size(24, 24);
            this.m_exportBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_exportBtn.Location = new System.Drawing.Point(548, 64);
            this.m_exportBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_exportBtn.MouseBack = null;
            this.m_exportBtn.Name = "m_exportBtn";
            this.m_exportBtn.NormlBack = null;
            this.m_exportBtn.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.m_exportBtn.Size = new System.Drawing.Size(127, 33);
            this.m_exportBtn.TabIndex = 85;
            this.m_exportBtn.Text = "   Export";
            this.m_exportBtn.UseVisualStyleBackColor = false;
            this.m_exportBtn.Visible = false;
            // 
            // btnlSearch
            // 
            this.btnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnlSearch.BaseColor = System.Drawing.Color.LightGray;
            this.btnlSearch.BorderColor = System.Drawing.Color.Black;
            this.btnlSearch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnlSearch.DownBack = null;
            this.btnlSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnlSearch.Font = new System.Drawing.Font("Arial", 14F);
            this.btnlSearch.ForeColor = System.Drawing.Color.Black;
            this.btnlSearch.Image = global::com.mirle.ibg3k0.bc.winform.Properties.Resources.se;
            this.btnlSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnlSearch.ImageSize = new System.Drawing.Size(24, 24);
            this.btnlSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnlSearch.Location = new System.Drawing.Point(548, 23);
            this.btnlSearch.MouseBack = null;
            this.btnlSearch.Name = "btnlSearch";
            this.btnlSearch.NormlBack = null;
            this.btnlSearch.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btnlSearch.Size = new System.Drawing.Size(127, 32);
            this.btnlSearch.TabIndex = 83;
            this.btnlSearch.Text = "Search";
            this.btnlSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlSearch.UseVisualStyleBackColor = false;
            this.btnlSearch.Click += new System.EventHandler(this.btnlSearch_Click_1);
            // 
            // skinGroupBox1
            // 
            this.skinGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox1.BorderColor = System.Drawing.Color.Black;
            this.skinGroupBox1.Controls.Add(this.tableLayoutPanel5);
            this.skinGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.skinGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skinGroupBox1.Font = new System.Drawing.Font("Arial", 14F);
            this.skinGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.skinGroupBox1.Location = new System.Drawing.Point(403, 3);
            this.skinGroupBox1.Name = "skinGroupBox1";
            this.skinGroupBox1.RectBackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox1.Size = new System.Drawing.Size(236, 104);
            this.skinGroupBox1.TabIndex = 76;
            this.skinGroupBox1.TabStop = false;
            this.skinGroupBox1.Text = "CMD ID";
            this.skinGroupBox1.TitleBorderColor = System.Drawing.Color.Black;
            this.skinGroupBox1.TitleRectBackColor = System.Drawing.Color.LightSteelBlue;
            this.skinGroupBox1.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.m_AlarmCodeTbl, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(230, 76);
            this.tableLayoutPanel5.TabIndex = 84;
            // 
            // m_AlarmCodeTbl
            // 
            this.m_AlarmCodeTbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_AlarmCodeTbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_AlarmCodeTbl.Font = new System.Drawing.Font("Arial", 14F);
            this.m_AlarmCodeTbl.Location = new System.Drawing.Point(12, 23);
            this.m_AlarmCodeTbl.Name = "m_AlarmCodeTbl";
            this.m_AlarmCodeTbl.PromptChar = ' ';
            this.m_AlarmCodeTbl.Size = new System.Drawing.Size(206, 29);
            this.m_AlarmCodeTbl.TabIndex = 64;
            // 
            // skinGroupBox2
            // 
            this.skinGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.skinGroupBox2.BorderColor = System.Drawing.Color.Black;
            this.skinGroupBox2.Controls.Add(this.tableLayoutPanel4);
            this.skinGroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.skinGroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.skinGroupBox2.Font = new System.Drawing.Font("Arial", 15F);
            this.skinGroupBox2.ForeColor = System.Drawing.Color.Black;
            this.skinGroupBox2.Location = new System.Drawing.Point(646, 3);
            this.skinGroupBox2.Name = "skinGroupBox2";
            this.skinGroupBox2.RectBackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox2.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox2.Size = new System.Drawing.Size(254, 104);
            this.skinGroupBox2.TabIndex = 75;
            this.skinGroupBox2.TabStop = false;
            this.skinGroupBox2.Text = "SOURCE";
            this.skinGroupBox2.TitleBorderColor = System.Drawing.Color.Black;
            this.skinGroupBox2.TitleRectBackColor = System.Drawing.Color.LightSteelBlue;
            this.skinGroupBox2.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.m_SourceIDCbx, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 26);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(248, 75);
            this.tableLayoutPanel4.TabIndex = 84;
            // 
            // m_SourceIDCbx
            // 
            this.m_SourceIDCbx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_SourceIDCbx.Font = new System.Drawing.Font("Arial", 14F);
            this.m_SourceIDCbx.FormattingEnabled = true;
            this.m_SourceIDCbx.Location = new System.Drawing.Point(17, 22);
            this.m_SourceIDCbx.Name = "m_SourceIDCbx";
            this.m_SourceIDCbx.Size = new System.Drawing.Size(214, 30);
            this.m_SourceIDCbx.TabIndex = 53;
            this.m_SourceIDCbx.SelectedIndexChanged += new System.EventHandler(this.m_SourceIDCbx_SelectedIndexChanged);
            // 
            // HistoryTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1602, 712);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HistoryTransferForm";
            this.Text = "HistoryTransferForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HistoryTransferForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TransferCommandList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cMDMCSObjToShowBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.skinGroupBox4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.skinGroupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.skinGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.skinGroupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_TransferCommandList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox m_SourceIDCbx;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DateTimePicker m_EndDTCbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker m_StartDTCbx;
        private System.Windows.Forms.Label label1;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.MaskedTextBox m_AlarmCodeTbl;
        private System.Windows.Forms.Panel panel2;
        private CCWin.SkinControl.SkinButton m_exportBtn;
        private CCWin.SkinControl.SkinButton btnlSearch;
        private CCWin.SkinControl.SkinGroupBox skinGroupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox m_DestinationIDCbx;
        private System.Windows.Forms.BindingSource cMDMCSObjToShowBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMDIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_CARRIER;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_STATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_SOURCE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_DESTINATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_INSERT_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMD_START_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMDFINISHTIMEDataGridViewTextBoxColumn;
    }
}