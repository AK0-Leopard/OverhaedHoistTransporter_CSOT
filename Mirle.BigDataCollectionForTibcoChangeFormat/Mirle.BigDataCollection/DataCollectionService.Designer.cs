namespace Mirle.BigDataCollection
{
    partial class DataCollectionService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataCollectionService));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiShowDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.tspClose = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblAddr = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGetValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.ModBusIP = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModBusIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnC = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGroupAndFFUNo = new System.Windows.Forms.TextBox();
            this.txtModbus = new System.Windows.Forms.TextBox();
            this.rdCSV = new System.Windows.Forms.Button();
            this.tbCSV = new System.Windows.Forms.TextBox();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiShowDebug,
            this.tspClose});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(145, 48);
            // 
            // tmiShowDebug
            // 
            this.tmiShowDebug.Name = "tmiShowDebug";
            this.tmiShowDebug.Size = new System.Drawing.Size(144, 22);
            this.tmiShowDebug.Text = "ShowDebug";
            this.tmiShowDebug.Click += new System.EventHandler(this.tmiShowDebug_Click);
            // 
            // tspClose
            // 
            this.tspClose.Name = "tspClose";
            this.tspClose.Size = new System.Drawing.Size(144, 22);
            this.tspClose.Text = "Close";
            this.tspClose.Click += new System.EventHandler(this.tspClose_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MirleDataCollectionService";
            this.notifyIcon.Visible = true;
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAddr.Location = new System.Drawing.Point(54, 21);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(48, 21);
            this.lblAddr.TabIndex = 1;
            this.lblAddr.Text = "Addr";
            // 
            // txtAddr
            // 
            this.txtAddr.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAddr.Location = new System.Drawing.Point(108, 20);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(100, 22);
            this.txtAddr.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSave.Location = new System.Drawing.Point(132, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 29);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtValue
            // 
            this.txtValue.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtValue.Location = new System.Drawing.Point(108, 62);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 22);
            this.txtValue.TabIndex = 5;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblValue.Location = new System.Drawing.Point(54, 63);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(53, 21);
            this.lblValue.TabIndex = 4;
            this.lblValue.Text = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(54, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Addr";
            // 
            // txtGetValue
            // 
            this.txtGetValue.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtGetValue.Location = new System.Drawing.Point(108, 195);
            this.txtGetValue.Name = "txtGetValue";
            this.txtGetValue.Size = new System.Drawing.Size(100, 22);
            this.txtGetValue.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "Value";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnQuery.Location = new System.Drawing.Point(225, 192);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(76, 29);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ModBusIP
            // 
            this.ModBusIP.AutoSize = true;
            this.ModBusIP.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ModBusIP.Location = new System.Drawing.Point(411, 25);
            this.ModBusIP.Name = "ModBusIP";
            this.ModBusIP.Size = new System.Drawing.Size(76, 15);
            this.ModBusIP.TabIndex = 10;
            this.ModBusIP.Text = "ModBusIP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(411, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Port";
            // 
            // txtModBusIP
            // 
            this.txtModBusIP.Location = new System.Drawing.Point(502, 20);
            this.txtModBusIP.Name = "txtModBusIP";
            this.txtModBusIP.Size = new System.Drawing.Size(100, 22);
            this.txtModBusIP.TabIndex = 12;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(502, 67);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 22);
            this.txtPort.TabIndex = 13;
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(527, 164);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(75, 23);
            this.btnC.TabIndex = 14;
            this.btnC.Text = "連線";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(366, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Group, FFUNo";
            // 
            // txtGroupAndFFUNo
            // 
            this.txtGroupAndFFUNo.Location = new System.Drawing.Point(502, 125);
            this.txtGroupAndFFUNo.Name = "txtGroupAndFFUNo";
            this.txtGroupAndFFUNo.Size = new System.Drawing.Size(100, 22);
            this.txtGroupAndFFUNo.TabIndex = 17;
            // 
            // txtModbus
            // 
            this.txtModbus.Location = new System.Drawing.Point(369, 192);
            this.txtModbus.Multiline = true;
            this.txtModbus.Name = "txtModbus";
            this.txtModbus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtModbus.Size = new System.Drawing.Size(267, 243);
            this.txtModbus.TabIndex = 18;
            // 
            // rdCSV
            // 
            this.rdCSV.Location = new System.Drawing.Point(36, 322);
            this.rdCSV.Name = "rdCSV";
            this.rdCSV.Size = new System.Drawing.Size(75, 23);
            this.rdCSV.TabIndex = 19;
            this.rdCSV.Text = "Read CSV";
            this.rdCSV.UseVisualStyleBackColor = true;
            this.rdCSV.Click += new System.EventHandler(this.rdCSV_Click);
            // 
            // tbCSV
            // 
            this.tbCSV.Location = new System.Drawing.Point(118, 322);
            this.tbCSV.Multiline = true;
            this.tbCSV.Name = "tbCSV";
            this.tbCSV.Size = new System.Drawing.Size(211, 44);
            this.tbCSV.TabIndex = 20;
            // 
            // DataCollectionService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 472);
            this.Controls.Add(this.tbCSV);
            this.Controls.Add(this.rdCSV);
            this.Controls.Add(this.txtModbus);
            this.Controls.Add(this.txtGroupAndFFUNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtModBusIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ModBusIP);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGetValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.lblAddr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataCollectionService";
            this.Text = "MirleDataCollectionService";
            this.Deactivate += new System.EventHandler(this.DataCollectionService_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataCollectionService_FormClosing);
            this.Load += new System.EventHandler(this.DataCollectionService_Load);
            this.Shown += new System.EventHandler(this.DataCollectionService_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataCollectionService_KeyDown);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tspClose;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGetValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label ModBusIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModBusIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGroupAndFFUNo;
        private System.Windows.Forms.TextBox txtModbus;
        private System.Windows.Forms.ToolStripMenuItem tmiShowDebug;
        private System.Windows.Forms.Button rdCSV;
        private System.Windows.Forms.TextBox tbCSV;
    }
}