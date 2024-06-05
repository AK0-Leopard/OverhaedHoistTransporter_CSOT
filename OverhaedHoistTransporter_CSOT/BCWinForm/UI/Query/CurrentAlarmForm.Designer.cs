namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class CurrentAlarmForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgv_Alarm = new System.Windows.Forms.DataGridView();
            this.eqpt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_lvl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.report_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Alarm)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_Alarm, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 477F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1179, 477);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgv_Alarm
            // 
            this.dgv_Alarm.AllowUserToAddRows = false;
            this.dgv_Alarm.AllowUserToDeleteRows = false;
            this.dgv_Alarm.AllowUserToOrderColumns = true;
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
            this.dgv_Alarm.Size = new System.Drawing.Size(1173, 471);
            this.dgv_Alarm.TabIndex = 1;
            // 
            // eqpt_id
            // 
            this.eqpt_id.DataPropertyName = "EQPT_ID";
            this.eqpt_id.HeaderText = "EQPT ID";
            this.eqpt_id.Name = "eqpt_id";
            this.eqpt_id.ReadOnly = true;
            this.eqpt_id.Width = 188;
            // 
            // alarm_code
            // 
            this.alarm_code.DataPropertyName = "ALAM_CODE";
            this.alarm_code.HeaderText = "Code";
            this.alarm_code.Name = "alarm_code";
            this.alarm_code.ReadOnly = true;
            this.alarm_code.Width = 189;
            // 
            // alarm_lvl
            // 
            this.alarm_lvl.DataPropertyName = "ALAM_LVL";
            this.alarm_lvl.HeaderText = "Level";
            this.alarm_lvl.Name = "alarm_lvl";
            this.alarm_lvl.ReadOnly = true;
            this.alarm_lvl.Width = 188;
            // 
            // report_time
            // 
            this.report_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.report_time.DataPropertyName = "RPT_DATE_TIME";
            this.report_time.HeaderText = "Time";
            this.report_time.Name = "report_time";
            this.report_time.ReadOnly = true;
            this.report_time.Width = 70;
            // 
            // alarm_desc
            // 
            this.alarm_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.alarm_desc.DataPropertyName = "ALAM_DESC";
            this.alarm_desc.FillWeight = 200F;
            this.alarm_desc.HeaderText = "Description";
            this.alarm_desc.Name = "alarm_desc";
            this.alarm_desc.ReadOnly = true;
            this.alarm_desc.Width = 133;
            // 
            // CurrentAlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 477);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CurrentAlarmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarm Current Query";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RoadControlForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RoadControlForm_FormClosed);
            this.Load += new System.EventHandler(this.CurrentAlarmForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Alarm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgv_Alarm;
        private System.Windows.Forms.DataGridViewTextBoxColumn eqpt_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_lvl;
        private System.Windows.Forms.DataGridViewTextBoxColumn report_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_desc;
    }
}