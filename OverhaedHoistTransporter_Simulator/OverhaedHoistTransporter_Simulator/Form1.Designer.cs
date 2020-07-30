namespace Mirle.Agvc.Simulator
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSend = new System.Windows.Forms.Button();
            this.cbSend = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CmdItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CmdValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAlarmSet = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbAutoApplyReserve = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbLocation = new System.Windows.Forms.GroupBox();
            this.rbtnG = new System.Windows.Forms.RadioButton();
            this.rbtnB = new System.Windows.Forms.RadioButton();
            this.rbtnA = new System.Windows.Forms.RadioButton();
            this.btnNextRandomLoaction = new System.Windows.Forms.Button();
            this.btnSendRandomSingleCmd = new System.Windows.Forms.Button();
            this.btnRandomLoop = new System.Windows.Forms.Button();
            this.timerRandomLoop = new System.Windows.Forms.Timer(this.components);
            this.gbRandomLoadUnload = new System.Windows.Forms.GroupBox();
            this.cbIsInSitu = new System.Windows.Forms.CheckBox();
            this.btnCleanRichTextBox = new System.Windows.Forms.Button();
            this.btnSetReserveReplyOnce = new System.Windows.Forms.Button();
            this.numSeq = new System.Windows.Forms.NumericUpDown();
            this.btnIsNull = new System.Windows.Forms.Button();
            this.txtReserveSectionId = new System.Windows.Forms.TextBox();
            this.txtDebugLogMsg = new System.Windows.Forms.TextBox();
            this.cbReserveSuccess = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Server_Client_Select_RadioBox1 = new System.Windows.Forms.GroupBox();
            this.RadioBtn_Server = new System.Windows.Forms.RadioButton();
            this.RadioBtn_Client = new System.Windows.Forms.RadioButton();
            this.cbRight = new System.Windows.Forms.ComboBox();
            this.cbMiddle = new System.Windows.Forms.ComboBox();
            this.cbLeft = new System.Windows.Forms.ComboBox();
            this.btnXtoY = new System.Windows.Forms.Button();
            this.gbXtoY = new System.Windows.Forms.GroupBox();
            this.txtError = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_InterlockError = new System.Windows.Forms.RadioButton();
            this.radioButton_Abort = new System.Windows.Forms.RadioButton();
            this.radioButton_Cancel = new System.Windows.Forms.RadioButton();
            this.radioButton_Abnormal_DoubleStorage = new System.Windows.Forms.RadioButton();
            this.radioButton_Abnormal_EmptyRetrieval = new System.Windows.Forms.RadioButton();
            this.radioButton_Abnormal_BcrDuplicate = new System.Windows.Forms.RadioButton();
            this.radioButton_Abnormal_BcrMisMatch = new System.Windows.Forms.RadioButton();
            this.radioButton_Abnormal_BcrReadFail = new System.Windows.Forms.RadioButton();
            this.radioButton_Normal = new System.Windows.Forms.RadioButton();
            this.Mismatch_ID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.set_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.gbLocation.SuspendLayout();
            this.gbRandomLoadUnload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeq)).BeginInit();
            this.Server_Client_Select_RadioBox1.SuspendLayout();
            this.gbXtoY.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSend.Location = new System.Drawing.Point(711, 47);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cbSend
            // 
            this.cbSend.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbSend.FormattingEnabled = true;
            this.cbSend.Items.AddRange(new object[] {
            "Cmd31_TransferRequest",
            "Cmd32_TransferCompleteResponse",
            "Cmd33_ControlZoneCancelRequest",
            "Cmd35_CarrierIdRenameRequest",
            "Cmd36_TransferEventResponse",
            "Cmd37_TransferCancelRequest",
            "Cmd39_PauseRequest",
            "Cmd41_ModeChange",
            "Cmd43_StatusRequest",
            "Cmd44_StatusRequest",
            "Cmd45_PowerOnoffRequest",
            "Cmd51_AvoidRequest",
            "Cmd52_AvoidCompleteResponse",
            "Cmd71_RangeTeachRequest",
            "Cmd72_RangeTeachCompleteResponse",
            "Cmd74_AddressTeachResponse",
            "Cmd91_AlarmResetRequest",
            "Cmd94_AlarmResponse",
            "Cmd131_TransferResponse",
            "Cmd132_TransferCompleteReport",
            "Cmd133_ControlZoneCancelResponse",
            "Cmd134_TransferEventReport",
            "Cmd135_CarrierIdRenameResponse",
            "Cmd136_TransferEventReport",
            "Cmd137_TransferCancelResponse",
            "Cmd139_PauseResponse",
            "Cmd141_ModeChangeResponse",
            "Cmd143_StatusResponse",
            "Cmd144_StatusReport",
            "Cmd145_PowerOnoffResponse",
            "Cmd151_AvoidResponse",
            "Cmd152_AvoidCompleteReport",
            "Cmd171_RangeTeachResponse",
            "Cmd172_RangeTeachCompleteReport",
            "Cmd174_AddressTeachReport",
            "Cmd191_AlarmResetResponse",
            "Cmd194_AlarmReport"});
            this.cbSend.Location = new System.Drawing.Point(57, 47);
            this.cbSend.Name = "cbSend";
            this.cbSend.Size = new System.Drawing.Size(649, 24);
            this.cbSend.TabIndex = 1;
            this.cbSend.SelectedValueChanged += new System.EventHandler(this.cbSend_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cmd";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CmdItem,
            this.CmdValue});
            this.dataGridView1.Location = new System.Drawing.Point(12, 77);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(747, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // CmdItem
            // 
            this.CmdItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CmdItem.HeaderText = "Item";
            this.CmdItem.Name = "CmdItem";
            this.CmdItem.Width = 51;
            // 
            // CmdValue
            // 
            this.CmdValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CmdValue.HeaderText = "Value";
            this.CmdValue.Name = "CmdValue";
            // 
            // btnAlarmSet
            // 
            this.btnAlarmSet.Location = new System.Drawing.Point(791, 481);
            this.btnAlarmSet.Name = "btnAlarmSet";
            this.btnAlarmSet.Size = new System.Drawing.Size(156, 23);
            this.btnAlarmSet.TabIndex = 5;
            this.btnAlarmSet.Text = "Alarm Set";
            this.btnAlarmSet.UseVisualStyleBackColor = true;
            this.btnAlarmSet.Click += new System.EventHandler(this.btnAlarmSet_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 800);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(990, 24);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(76, 19);
            this.toolStripStatusLabel1.Text = "Dis-Connect";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(889, 19);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = " ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(10, 19);
            this.toolStripStatusLabel3.Text = " ";
            // 
            // txtIp
            // 
            this.txtIp.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtIp.Location = new System.Drawing.Point(57, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(141, 27);
            this.txtIp.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(30, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(204, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPort.Location = new System.Drawing.Point(243, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(62, 27);
            this.txtPort.TabIndex = 9;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConnect.Location = new System.Drawing.Point(452, 15);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDisConnect.Location = new System.Drawing.Point(533, 15);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(105, 23);
            this.btnDisConnect.TabIndex = 14;
            this.btnDisConnect.Text = "DisConnect";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbAutoApplyReserve
            // 
            this.cbAutoApplyReserve.AutoSize = true;
            this.cbAutoApplyReserve.Checked = true;
            this.cbAutoApplyReserve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoApplyReserve.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbAutoApplyReserve.ForeColor = System.Drawing.Color.ForestGreen;
            this.cbAutoApplyReserve.Location = new System.Drawing.Point(791, 610);
            this.cbAutoApplyReserve.Name = "cbAutoApplyReserve";
            this.cbAutoApplyReserve.Size = new System.Drawing.Size(173, 20);
            this.cbAutoApplyReserve.TabIndex = 16;
            this.cbAutoApplyReserve.Text = "Auto Apply Reserve";
            this.cbAutoApplyReserve.UseVisualStyleBackColor = true;
            this.cbAutoApplyReserve.CheckedChanged += new System.EventHandler(this.cbAutoApplyReserve_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(195, 759);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 19);
            this.label4.TabIndex = 17;
            this.label4.Text = "Loadunload Commands";
            // 
            // gbLocation
            // 
            this.gbLocation.Controls.Add(this.rbtnG);
            this.gbLocation.Controls.Add(this.rbtnB);
            this.gbLocation.Controls.Add(this.rbtnA);
            this.gbLocation.Location = new System.Drawing.Point(383, 765);
            this.gbLocation.Name = "gbLocation";
            this.gbLocation.Size = new System.Drawing.Size(190, 59);
            this.gbLocation.TabIndex = 23;
            this.gbLocation.TabStop = false;
            this.gbLocation.Text = "Location";
            this.gbLocation.Visible = false;
            // 
            // rbtnG
            // 
            this.rbtnG.AutoSize = true;
            this.rbtnG.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbtnG.Location = new System.Drawing.Point(92, 23);
            this.rbtnG.Name = "rbtnG";
            this.rbtnG.Size = new System.Drawing.Size(38, 20);
            this.rbtnG.TabIndex = 2;
            this.rbtnG.Tag = "EnumAddress.G";
            this.rbtnG.Text = "G";
            this.rbtnG.UseVisualStyleBackColor = true;
            this.rbtnG.CheckedChanged += new System.EventHandler(this.gbLoactionRbtn_CheckedChanged);
            // 
            // rbtnB
            // 
            this.rbtnB.AutoSize = true;
            this.rbtnB.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbtnB.Location = new System.Drawing.Point(49, 23);
            this.rbtnB.Name = "rbtnB";
            this.rbtnB.Size = new System.Drawing.Size(37, 20);
            this.rbtnB.TabIndex = 1;
            this.rbtnB.Tag = "EnumAddress.B";
            this.rbtnB.Text = "B";
            this.rbtnB.UseVisualStyleBackColor = true;
            this.rbtnB.CheckedChanged += new System.EventHandler(this.gbLoactionRbtn_CheckedChanged);
            // 
            // rbtnA
            // 
            this.rbtnA.AutoSize = true;
            this.rbtnA.Checked = true;
            this.rbtnA.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbtnA.Location = new System.Drawing.Point(6, 23);
            this.rbtnA.Name = "rbtnA";
            this.rbtnA.Size = new System.Drawing.Size(38, 20);
            this.rbtnA.TabIndex = 0;
            this.rbtnA.TabStop = true;
            this.rbtnA.Tag = "EnumAddress.A";
            this.rbtnA.Text = "A";
            this.rbtnA.UseVisualStyleBackColor = true;
            this.rbtnA.CheckedChanged += new System.EventHandler(this.gbLoactionRbtn_CheckedChanged);
            // 
            // btnNextRandomLoaction
            // 
            this.btnNextRandomLoaction.Location = new System.Drawing.Point(6, 21);
            this.btnNextRandomLoaction.Name = "btnNextRandomLoaction";
            this.btnNextRandomLoaction.Size = new System.Drawing.Size(172, 53);
            this.btnNextRandomLoaction.TabIndex = 24;
            this.btnNextRandomLoaction.Text = "Next Random Location";
            this.btnNextRandomLoaction.UseVisualStyleBackColor = true;
            this.btnNextRandomLoaction.Click += new System.EventHandler(this.btnNextRandomLoaction_Click);
            // 
            // btnSendRandomSingleCmd
            // 
            this.btnSendRandomSingleCmd.Location = new System.Drawing.Point(6, 80);
            this.btnSendRandomSingleCmd.Name = "btnSendRandomSingleCmd";
            this.btnSendRandomSingleCmd.Size = new System.Drawing.Size(172, 53);
            this.btnSendRandomSingleCmd.TabIndex = 25;
            this.btnSendRandomSingleCmd.Text = "Send Next Random Cmd";
            this.btnSendRandomSingleCmd.UseVisualStyleBackColor = true;
            this.btnSendRandomSingleCmd.Click += new System.EventHandler(this.btnSendRandomSingleCmd_Click);
            // 
            // btnRandomLoop
            // 
            this.btnRandomLoop.ForeColor = System.Drawing.Color.Red;
            this.btnRandomLoop.Location = new System.Drawing.Point(6, 139);
            this.btnRandomLoop.Name = "btnRandomLoop";
            this.btnRandomLoop.Size = new System.Drawing.Size(172, 53);
            this.btnRandomLoop.TabIndex = 26;
            this.btnRandomLoop.Text = "Random Loop On/Off";
            this.btnRandomLoop.UseVisualStyleBackColor = true;
            this.btnRandomLoop.Click += new System.EventHandler(this.btnRandomLoop_Click);
            // 
            // timerRandomLoop
            // 
            this.timerRandomLoop.Tick += new System.EventHandler(this.timerRandomLoop_Tick);
            // 
            // gbRandomLoadUnload
            // 
            this.gbRandomLoadUnload.Controls.Add(this.cbIsInSitu);
            this.gbRandomLoadUnload.Controls.Add(this.btnNextRandomLoaction);
            this.gbRandomLoadUnload.Controls.Add(this.btnSendRandomSingleCmd);
            this.gbRandomLoadUnload.Controls.Add(this.btnRandomLoop);
            this.gbRandomLoadUnload.Location = new System.Drawing.Point(581, 759);
            this.gbRandomLoadUnload.Name = "gbRandomLoadUnload";
            this.gbRandomLoadUnload.Size = new System.Drawing.Size(190, 233);
            this.gbRandomLoadUnload.TabIndex = 32;
            this.gbRandomLoadUnload.TabStop = false;
            this.gbRandomLoadUnload.Text = "Random LoadUnload";
            this.gbRandomLoadUnload.Visible = false;
            // 
            // cbIsInSitu
            // 
            this.cbIsInSitu.AutoSize = true;
            this.cbIsInSitu.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbIsInSitu.Location = new System.Drawing.Point(6, 198);
            this.cbIsInSitu.Name = "cbIsInSitu";
            this.cbIsInSitu.Size = new System.Drawing.Size(123, 20);
            this.cbIsInSitu.TabIndex = 27;
            this.cbIsInSitu.Text = "包含原地取放";
            this.cbIsInSitu.UseVisualStyleBackColor = true;
            this.cbIsInSitu.CheckedChanged += new System.EventHandler(this.cbIsInSitu_CheckedChanged);
            // 
            // btnCleanRichTextBox
            // 
            this.btnCleanRichTextBox.Location = new System.Drawing.Point(800, 43);
            this.btnCleanRichTextBox.Name = "btnCleanRichTextBox";
            this.btnCleanRichTextBox.Size = new System.Drawing.Size(190, 27);
            this.btnCleanRichTextBox.TabIndex = 28;
            this.btnCleanRichTextBox.Text = "Clean RichTextBox";
            this.btnCleanRichTextBox.UseVisualStyleBackColor = true;
            this.btnCleanRichTextBox.Click += new System.EventHandler(this.btnCleanRichTextBox_Click);
            // 
            // btnSetReserveReplyOnce
            // 
            this.btnSetReserveReplyOnce.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSetReserveReplyOnce.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnSetReserveReplyOnce.Location = new System.Drawing.Point(791, 697);
            this.btnSetReserveReplyOnce.Name = "btnSetReserveReplyOnce";
            this.btnSetReserveReplyOnce.Size = new System.Drawing.Size(192, 41);
            this.btnSetReserveReplyOnce.TabIndex = 28;
            this.btnSetReserveReplyOnce.Text = "Set Reserve Reply Once";
            this.btnSetReserveReplyOnce.UseVisualStyleBackColor = true;
            this.btnSetReserveReplyOnce.Click += new System.EventHandler(this.btnSetReserveReplyOnce_Click);
            // 
            // numSeq
            // 
            this.numSeq.Location = new System.Drawing.Point(666, 15);
            this.numSeq.Name = "numSeq";
            this.numSeq.Size = new System.Drawing.Size(120, 22);
            this.numSeq.TabIndex = 33;
            // 
            // btnIsNull
            // 
            this.btnIsNull.Location = new System.Drawing.Point(875, 12);
            this.btnIsNull.Name = "btnIsNull";
            this.btnIsNull.Size = new System.Drawing.Size(75, 23);
            this.btnIsNull.TabIndex = 5;
            this.btnIsNull.Text = "IsNull";
            this.btnIsNull.UseVisualStyleBackColor = true;
            this.btnIsNull.Click += new System.EventHandler(this.btnIsNull_Click);
            // 
            // txtReserveSectionId
            // 
            this.txtReserveSectionId.Location = new System.Drawing.Point(791, 647);
            this.txtReserveSectionId.Name = "txtReserveSectionId";
            this.txtReserveSectionId.Size = new System.Drawing.Size(192, 22);
            this.txtReserveSectionId.TabIndex = 34;
            this.txtReserveSectionId.Text = "ReserveSectionId";
            // 
            // txtDebugLogMsg
            // 
            this.txtDebugLogMsg.Location = new System.Drawing.Point(12, 226);
            this.txtDebugLogMsg.Multiline = true;
            this.txtDebugLogMsg.Name = "txtDebugLogMsg";
            this.txtDebugLogMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDebugLogMsg.Size = new System.Drawing.Size(747, 527);
            this.txtDebugLogMsg.TabIndex = 35;
            this.txtDebugLogMsg.Text = "DebugLogMsg";
            // 
            // cbReserveSuccess
            // 
            this.cbReserveSuccess.AutoSize = true;
            this.cbReserveSuccess.Location = new System.Drawing.Point(791, 675);
            this.cbReserveSuccess.Name = "cbReserveSuccess";
            this.cbReserveSuccess.Size = new System.Drawing.Size(59, 16);
            this.cbReserveSuccess.TabIndex = 36;
            this.cbReserveSuccess.Text = "Success";
            this.cbReserveSuccess.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.ForeColor = System.Drawing.Color.ForestGreen;
            this.button1.Location = new System.Drawing.Point(791, 744);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 41);
            this.button1.TabIndex = 37;
            this.button1.Text = "Set Reserve Reply 5";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Server_Client_Select_RadioBox1
            // 
            this.Server_Client_Select_RadioBox1.Controls.Add(this.RadioBtn_Server);
            this.Server_Client_Select_RadioBox1.Controls.Add(this.RadioBtn_Client);
            this.Server_Client_Select_RadioBox1.Location = new System.Drawing.Point(311, 3);
            this.Server_Client_Select_RadioBox1.Name = "Server_Client_Select_RadioBox1";
            this.Server_Client_Select_RadioBox1.Size = new System.Drawing.Size(141, 38);
            this.Server_Client_Select_RadioBox1.TabIndex = 38;
            this.Server_Client_Select_RadioBox1.TabStop = false;
            // 
            // RadioBtn_Server
            // 
            this.RadioBtn_Server.AutoSize = true;
            this.RadioBtn_Server.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RadioBtn_Server.Location = new System.Drawing.Point(72, 12);
            this.RadioBtn_Server.Name = "RadioBtn_Server";
            this.RadioBtn_Server.Size = new System.Drawing.Size(72, 20);
            this.RadioBtn_Server.TabIndex = 1;
            this.RadioBtn_Server.Tag = "EnumAddress.Server";
            this.RadioBtn_Server.Text = "Server";
            this.RadioBtn_Server.UseVisualStyleBackColor = true;
            // 
            // RadioBtn_Client
            // 
            this.RadioBtn_Client.AutoSize = true;
            this.RadioBtn_Client.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RadioBtn_Client.Location = new System.Drawing.Point(5, 12);
            this.RadioBtn_Client.Name = "RadioBtn_Client";
            this.RadioBtn_Client.Size = new System.Drawing.Size(69, 20);
            this.RadioBtn_Client.TabIndex = 0;
            this.RadioBtn_Client.TabStop = true;
            this.RadioBtn_Client.Tag = "EnumAddress.Client";
            this.RadioBtn_Client.Text = "Client";
            this.RadioBtn_Client.UseVisualStyleBackColor = true;
            this.RadioBtn_Client.CheckedChanged += new System.EventHandler(this.RadioBtn_Client_CheckedChanged);
            // 
            // cbRight
            // 
            this.cbRight.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbRight.FormattingEnabled = true;
            this.cbRight.Location = new System.Drawing.Point(98, 30);
            this.cbRight.Name = "cbRight";
            this.cbRight.Size = new System.Drawing.Size(43, 24);
            this.cbRight.TabIndex = 28;
            this.cbRight.Text = "G";
            // 
            // cbMiddle
            // 
            this.cbMiddle.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbMiddle.ForeColor = System.Drawing.Color.Red;
            this.cbMiddle.FormattingEnabled = true;
            this.cbMiddle.Items.AddRange(new object[] {
            ">>",
            "<<"});
            this.cbMiddle.Location = new System.Drawing.Point(49, 30);
            this.cbMiddle.Name = "cbMiddle";
            this.cbMiddle.Size = new System.Drawing.Size(43, 24);
            this.cbMiddle.TabIndex = 29;
            this.cbMiddle.Text = ">>";
            // 
            // cbLeft
            // 
            this.cbLeft.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbLeft.FormattingEnabled = true;
            this.cbLeft.Location = new System.Drawing.Point(0, 30);
            this.cbLeft.Name = "cbLeft";
            this.cbLeft.Size = new System.Drawing.Size(43, 24);
            this.cbLeft.TabIndex = 27;
            this.cbLeft.Text = "A";
            // 
            // btnXtoY
            // 
            this.btnXtoY.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnXtoY.Location = new System.Drawing.Point(147, 31);
            this.btnXtoY.Name = "btnXtoY";
            this.btnXtoY.Size = new System.Drawing.Size(44, 23);
            this.btnXtoY.TabIndex = 30;
            this.btnXtoY.Text = "Go";
            this.btnXtoY.UseVisualStyleBackColor = true;
            this.btnXtoY.Click += new System.EventHandler(this.btnXtoY_Click);
            // 
            // gbXtoY
            // 
            this.gbXtoY.Controls.Add(this.btnXtoY);
            this.gbXtoY.Controls.Add(this.cbLeft);
            this.gbXtoY.Controls.Add(this.cbMiddle);
            this.gbXtoY.Controls.Add(this.cbRight);
            this.gbXtoY.Location = new System.Drawing.Point(185, 780);
            this.gbXtoY.Name = "gbXtoY";
            this.gbXtoY.Size = new System.Drawing.Size(192, 75);
            this.gbXtoY.TabIndex = 31;
            this.gbXtoY.TabStop = false;
            this.gbXtoY.Text = "X >> Y";
            this.gbXtoY.Visible = false;
            // 
            // txtError
            // 
            this.txtError.AutoSize = true;
            this.txtError.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtError.Location = new System.Drawing.Point(832, 77);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(69, 16);
            this.txtError.TabIndex = 41;
            this.txtError.Text = "Opera Set";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_InterlockError);
            this.groupBox2.Controls.Add(this.radioButton_Abort);
            this.groupBox2.Controls.Add(this.radioButton_Cancel);
            this.groupBox2.Controls.Add(this.radioButton_Abnormal_DoubleStorage);
            this.groupBox2.Controls.Add(this.radioButton_Abnormal_EmptyRetrieval);
            this.groupBox2.Controls.Add(this.radioButton_Abnormal_BcrDuplicate);
            this.groupBox2.Controls.Add(this.radioButton_Abnormal_BcrMisMatch);
            this.groupBox2.Controls.Add(this.radioButton_Abnormal_BcrReadFail);
            this.groupBox2.Controls.Add(this.radioButton_Normal);
            this.groupBox2.Location = new System.Drawing.Point(765, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 266);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // radioButton_InterlockError
            // 
            this.radioButton_InterlockError.AutoSize = true;
            this.radioButton_InterlockError.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_InterlockError.Location = new System.Drawing.Point(5, 220);
            this.radioButton_InterlockError.Name = "radioButton_InterlockError";
            this.radioButton_InterlockError.Size = new System.Drawing.Size(128, 20);
            this.radioButton_InterlockError.TabIndex = 49;
            this.radioButton_InterlockError.Tag = "EnumAddress.Client";
            this.radioButton_InterlockError.Text = "InterlockError";
            this.radioButton_InterlockError.UseVisualStyleBackColor = true;
            this.radioButton_InterlockError.CheckedChanged += new System.EventHandler(this.radio_InterlockError_CheckedChanged);
            // 
            // radioButton_Abort
            // 
            this.radioButton_Abort.AutoSize = true;
            this.radioButton_Abort.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Abort.Location = new System.Drawing.Point(5, 194);
            this.radioButton_Abort.Name = "radioButton_Abort";
            this.radioButton_Abort.Size = new System.Drawing.Size(135, 20);
            this.radioButton_Abort.TabIndex = 48;
            this.radioButton_Abort.Tag = "EnumAddress.Server";
            this.radioButton_Abort.Text = "AbortComplete";
            this.radioButton_Abort.UseVisualStyleBackColor = true;
            this.radioButton_Abort.CheckedChanged += new System.EventHandler(this.radioButton_Abort_CheckedChanged);
            // 
            // radioButton_Cancel
            // 
            this.radioButton_Cancel.AutoSize = true;
            this.radioButton_Cancel.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Cancel.Location = new System.Drawing.Point(5, 168);
            this.radioButton_Cancel.Name = "radioButton_Cancel";
            this.radioButton_Cancel.Size = new System.Drawing.Size(143, 20);
            this.radioButton_Cancel.TabIndex = 47;
            this.radioButton_Cancel.Tag = "EnumAddress.Server";
            this.radioButton_Cancel.Text = "CancelComplete";
            this.radioButton_Cancel.UseVisualStyleBackColor = true;
            this.radioButton_Cancel.CheckedChanged += new System.EventHandler(this.radioButton_Cancel_CheckedChanged);
            // 
            // radioButton_Abnormal_DoubleStorage
            // 
            this.radioButton_Abnormal_DoubleStorage.AutoSize = true;
            this.radioButton_Abnormal_DoubleStorage.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Abnormal_DoubleStorage.Location = new System.Drawing.Point(5, 116);
            this.radioButton_Abnormal_DoubleStorage.Name = "radioButton_Abnormal_DoubleStorage";
            this.radioButton_Abnormal_DoubleStorage.Size = new System.Drawing.Size(212, 20);
            this.radioButton_Abnormal_DoubleStorage.TabIndex = 47;
            this.radioButton_Abnormal_DoubleStorage.Tag = "EnumAddress.Server";
            this.radioButton_Abnormal_DoubleStorage.Text = "Abnormal_DoubleStorage";
            this.radioButton_Abnormal_DoubleStorage.UseVisualStyleBackColor = true;
            this.radioButton_Abnormal_DoubleStorage.CheckedChanged += new System.EventHandler(this.radioButton_Abnormal_DoubleStorage_CheckedChanged);
            // 
            // radioButton_Abnormal_EmptyRetrieval
            // 
            this.radioButton_Abnormal_EmptyRetrieval.AutoSize = true;
            this.radioButton_Abnormal_EmptyRetrieval.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Abnormal_EmptyRetrieval.Location = new System.Drawing.Point(5, 142);
            this.radioButton_Abnormal_EmptyRetrieval.Name = "radioButton_Abnormal_EmptyRetrieval";
            this.radioButton_Abnormal_EmptyRetrieval.Size = new System.Drawing.Size(217, 20);
            this.radioButton_Abnormal_EmptyRetrieval.TabIndex = 46;
            this.radioButton_Abnormal_EmptyRetrieval.Tag = "EnumAddress.Server";
            this.radioButton_Abnormal_EmptyRetrieval.Text = "Abnormal_EmptyRetrieval";
            this.radioButton_Abnormal_EmptyRetrieval.UseVisualStyleBackColor = true;
            this.radioButton_Abnormal_EmptyRetrieval.CheckedChanged += new System.EventHandler(this.radioButton_Abnormal_EmptyRetrieval_CheckedChanged);
            // 
            // radioButton_Abnormal_BcrDuplicate
            // 
            this.radioButton_Abnormal_BcrDuplicate.AutoSize = true;
            this.radioButton_Abnormal_BcrDuplicate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Abnormal_BcrDuplicate.Location = new System.Drawing.Point(5, 90);
            this.radioButton_Abnormal_BcrDuplicate.Name = "radioButton_Abnormal_BcrDuplicate";
            this.radioButton_Abnormal_BcrDuplicate.Size = new System.Drawing.Size(200, 20);
            this.radioButton_Abnormal_BcrDuplicate.TabIndex = 45;
            this.radioButton_Abnormal_BcrDuplicate.Tag = "EnumAddress.Server";
            this.radioButton_Abnormal_BcrDuplicate.Text = "Abnormal_BcrDuplicate";
            this.radioButton_Abnormal_BcrDuplicate.UseVisualStyleBackColor = true;
            this.radioButton_Abnormal_BcrDuplicate.CheckedChanged += new System.EventHandler(this.radioButton_Abnormal_BcrDuplicate_CheckedChanged);
            // 
            // radioButton_Abnormal_BcrMisMatch
            // 
            this.radioButton_Abnormal_BcrMisMatch.AutoSize = true;
            this.radioButton_Abnormal_BcrMisMatch.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Abnormal_BcrMisMatch.Location = new System.Drawing.Point(5, 64);
            this.radioButton_Abnormal_BcrMisMatch.Name = "radioButton_Abnormal_BcrMisMatch";
            this.radioButton_Abnormal_BcrMisMatch.Size = new System.Drawing.Size(201, 20);
            this.radioButton_Abnormal_BcrMisMatch.TabIndex = 44;
            this.radioButton_Abnormal_BcrMisMatch.Tag = "EnumAddress.Server";
            this.radioButton_Abnormal_BcrMisMatch.Text = "Abnormal_BcrMisMatch";
            this.radioButton_Abnormal_BcrMisMatch.UseVisualStyleBackColor = true;
            this.radioButton_Abnormal_BcrMisMatch.CheckedChanged += new System.EventHandler(this.radioButton_Abnormal_BcrMisMatch_CheckedChanged);
            // 
            // radioButton_Abnormal_BcrReadFail
            // 
            this.radioButton_Abnormal_BcrReadFail.AutoSize = true;
            this.radioButton_Abnormal_BcrReadFail.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Abnormal_BcrReadFail.Location = new System.Drawing.Point(5, 38);
            this.radioButton_Abnormal_BcrReadFail.Name = "radioButton_Abnormal_BcrReadFail";
            this.radioButton_Abnormal_BcrReadFail.Size = new System.Drawing.Size(194, 20);
            this.radioButton_Abnormal_BcrReadFail.TabIndex = 1;
            this.radioButton_Abnormal_BcrReadFail.Tag = "EnumAddress.Server";
            this.radioButton_Abnormal_BcrReadFail.Text = "Abnormal_BcrReadFail";
            this.radioButton_Abnormal_BcrReadFail.UseVisualStyleBackColor = true;
            this.radioButton_Abnormal_BcrReadFail.CheckedChanged += new System.EventHandler(this.radioButton_Abnormal_BcrReadFail_CheckedChanged);
            // 
            // radioButton_Normal
            // 
            this.radioButton_Normal.AutoSize = true;
            this.radioButton_Normal.Checked = true;
            this.radioButton_Normal.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton_Normal.Location = new System.Drawing.Point(5, 12);
            this.radioButton_Normal.Name = "radioButton_Normal";
            this.radioButton_Normal.Size = new System.Drawing.Size(79, 20);
            this.radioButton_Normal.TabIndex = 0;
            this.radioButton_Normal.TabStop = true;
            this.radioButton_Normal.Tag = "EnumAddress.Client";
            this.radioButton_Normal.Text = "Normal";
            this.radioButton_Normal.UseVisualStyleBackColor = true;
            this.radioButton_Normal.CheckedChanged += new System.EventHandler(this.radioOperaSet_CheckedChanged);
            // 
            // Mismatch_ID
            // 
            this.Mismatch_ID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Mismatch_ID.Location = new System.Drawing.Point(791, 393);
            this.Mismatch_ID.Name = "Mismatch_ID";
            this.Mismatch_ID.Size = new System.Drawing.Size(156, 27);
            this.Mismatch_ID.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(824, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Mismatch ID";
            // 
            // set_button
            // 
            this.set_button.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.set_button.Location = new System.Drawing.Point(830, 435);
            this.set_button.Name = "set_button";
            this.set_button.Size = new System.Drawing.Size(75, 23);
            this.set_button.TabIndex = 46;
            this.set_button.Text = "Set";
            this.set_button.UseVisualStyleBackColor = true;
            this.set_button.Click += new System.EventHandler(this.set_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 824);
            this.Controls.Add(this.set_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Mismatch_ID);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.Server_Client_Select_RadioBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbReserveSuccess);
            this.Controls.Add(this.txtDebugLogMsg);
            this.Controls.Add(this.txtReserveSectionId);
            this.Controls.Add(this.numSeq);
            this.Controls.Add(this.btnSetReserveReplyOnce);
            this.Controls.Add(this.btnCleanRichTextBox);
            this.Controls.Add(this.gbRandomLoadUnload);
            this.Controls.Add(this.gbXtoY);
            this.Controls.Add(this.gbLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbAutoApplyReserve);
            this.Controls.Add(this.btnDisConnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnIsNull);
            this.Controls.Add(this.btnAlarmSet);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSend);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Mirle300Commucator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbLocation.ResumeLayout(false);
            this.gbLocation.PerformLayout();
            this.gbRandomLoadUnload.ResumeLayout(false);
            this.gbRandomLoadUnload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSeq)).EndInit();
            this.Server_Client_Select_RadioBox1.ResumeLayout(false);
            this.Server_Client_Select_RadioBox1.PerformLayout();
            this.gbXtoY.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cbSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CmdItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CmdValue;
        private System.Windows.Forms.Button btnAlarmSet;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbAutoApplyReserve;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbLocation;
        private System.Windows.Forms.RadioButton rbtnG;
        private System.Windows.Forms.RadioButton rbtnB;
        private System.Windows.Forms.RadioButton rbtnA;
        private System.Windows.Forms.Button btnNextRandomLoaction;
        private System.Windows.Forms.Button btnSendRandomSingleCmd;
        private System.Windows.Forms.Button btnRandomLoop;
        private System.Windows.Forms.Timer timerRandomLoop;
        private System.Windows.Forms.GroupBox gbRandomLoadUnload;
        private System.Windows.Forms.CheckBox cbIsInSitu;
        private System.Windows.Forms.Button btnCleanRichTextBox;
        private System.Windows.Forms.Button btnSetReserveReplyOnce;
        private System.Windows.Forms.NumericUpDown numSeq;
        private System.Windows.Forms.Button btnIsNull;
        private System.Windows.Forms.TextBox txtReserveSectionId;
        private System.Windows.Forms.TextBox txtDebugLogMsg;
        private System.Windows.Forms.CheckBox cbReserveSuccess;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox Server_Client_Select_RadioBox1;
        private System.Windows.Forms.RadioButton RadioBtn_Server;
        private System.Windows.Forms.RadioButton RadioBtn_Client;
        private System.Windows.Forms.ComboBox cbRight;
        private System.Windows.Forms.ComboBox cbMiddle;
        private System.Windows.Forms.ComboBox cbLeft;
        private System.Windows.Forms.Button btnXtoY;
        private System.Windows.Forms.GroupBox gbXtoY;
        private System.Windows.Forms.Label txtError;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_Abnormal_DoubleStorage;
        private System.Windows.Forms.RadioButton radioButton_Abnormal_EmptyRetrieval;
        private System.Windows.Forms.RadioButton radioButton_Abnormal_BcrDuplicate;
        private System.Windows.Forms.RadioButton radioButton_Abnormal_BcrMisMatch;
        private System.Windows.Forms.RadioButton radioButton_Abnormal_BcrReadFail;
        private System.Windows.Forms.RadioButton radioButton_Normal;
        private System.Windows.Forms.RadioButton radioButton_Abort;
        private System.Windows.Forms.RadioButton radioButton_Cancel;
        private System.Windows.Forms.RadioButton radioButton_InterlockError;
        private System.Windows.Forms.TextBox Mismatch_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button set_button;
    }
}

