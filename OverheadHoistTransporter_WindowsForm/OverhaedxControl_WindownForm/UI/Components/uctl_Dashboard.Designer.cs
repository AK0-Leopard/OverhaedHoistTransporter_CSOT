namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    partial class uctl_Dashboard
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.progress_bar_vehicle_status = new CCWin.SkinControl.SkinProgressBar();
            this.progress_bar_ohxc_effectiveness = new CCWin.SkinControl.SkinProgressBar();
            this.progress_bar_mcs_cmd_queue_status = new CCWin.SkinControl.SkinProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pic_cmdQueueTime = new System.Windows.Forms.PictureBox();
            this.pic_cmdCount = new System.Windows.Forms.PictureBox();
            this.lbl_Idle_count = new System.Windows.Forms.Label();
            this.lbl_system_effectiveness = new System.Windows.Forms.Label();
            this.lbl_mcs_cmd_queue_count = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cmdQueueTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cmdCount)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // progress_bar_vehicle_status
            // 
            this.progress_bar_vehicle_status.Back = null;
            this.progress_bar_vehicle_status.BackColor = System.Drawing.Color.Transparent;
            this.progress_bar_vehicle_status.BarBack = null;
            this.progress_bar_vehicle_status.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progress_bar_vehicle_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progress_bar_vehicle_status.ForeColor = System.Drawing.Color.Red;
            this.progress_bar_vehicle_status.Location = new System.Drawing.Point(4, 25);
            this.progress_bar_vehicle_status.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progress_bar_vehicle_status.Name = "progress_bar_vehicle_status";
            this.progress_bar_vehicle_status.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progress_bar_vehicle_status.Size = new System.Drawing.Size(319, 20);
            this.progress_bar_vehicle_status.TabIndex = 9;
            this.progress_bar_vehicle_status.TextFormat = CCWin.SkinControl.SkinProgressBar.TxtFormat.None;
            // 
            // progress_bar_ohxc_effectiveness
            // 
            this.progress_bar_ohxc_effectiveness.Back = null;
            this.progress_bar_ohxc_effectiveness.BackColor = System.Drawing.Color.Transparent;
            this.progress_bar_ohxc_effectiveness.BarBack = null;
            this.progress_bar_ohxc_effectiveness.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progress_bar_ohxc_effectiveness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progress_bar_ohxc_effectiveness.ForeColor = System.Drawing.Color.Red;
            this.progress_bar_ohxc_effectiveness.Location = new System.Drawing.Point(4, 165);
            this.progress_bar_ohxc_effectiveness.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progress_bar_ohxc_effectiveness.Name = "progress_bar_ohxc_effectiveness";
            this.progress_bar_ohxc_effectiveness.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progress_bar_ohxc_effectiveness.Size = new System.Drawing.Size(319, 20);
            this.progress_bar_ohxc_effectiveness.TabIndex = 11;
            this.progress_bar_ohxc_effectiveness.TextFormat = CCWin.SkinControl.SkinProgressBar.TxtFormat.None;
            // 
            // progress_bar_mcs_cmd_queue_status
            // 
            this.progress_bar_mcs_cmd_queue_status.Back = null;
            this.progress_bar_mcs_cmd_queue_status.BackColor = System.Drawing.Color.Transparent;
            this.progress_bar_mcs_cmd_queue_status.BarBack = null;
            this.progress_bar_mcs_cmd_queue_status.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progress_bar_mcs_cmd_queue_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progress_bar_mcs_cmd_queue_status.ForeColor = System.Drawing.Color.Red;
            this.progress_bar_mcs_cmd_queue_status.FormatString = "{0:0}";
            this.progress_bar_mcs_cmd_queue_status.Location = new System.Drawing.Point(4, 95);
            this.progress_bar_mcs_cmd_queue_status.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progress_bar_mcs_cmd_queue_status.Maximum = 23;
            this.progress_bar_mcs_cmd_queue_status.Name = "progress_bar_mcs_cmd_queue_status";
            this.progress_bar_mcs_cmd_queue_status.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.progress_bar_mcs_cmd_queue_status.Size = new System.Drawing.Size(319, 20);
            this.progress_bar_mcs_cmd_queue_status.TabIndex = 13;
            this.progress_bar_mcs_cmd_queue_status.TextFormat = CCWin.SkinControl.SkinProgressBar.TxtFormat.None;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 333F));
            this.tableLayoutPanel1.Controls.Add(this.pic_cmdQueueTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pic_cmdCount, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1556, 870);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // pic_cmdQueueTime
            // 
            this.pic_cmdQueueTime.BackColor = System.Drawing.Color.Black;
            this.pic_cmdQueueTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_cmdQueueTime.Location = new System.Drawing.Point(4, 5);
            this.pic_cmdQueueTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pic_cmdQueueTime.Name = "pic_cmdQueueTime";
            this.tableLayoutPanel1.SetRowSpan(this.pic_cmdQueueTime, 2);
            this.pic_cmdQueueTime.Size = new System.Drawing.Size(1215, 424);
            this.pic_cmdQueueTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_cmdQueueTime.TabIndex = 7;
            this.pic_cmdQueueTime.TabStop = false;
            // 
            // pic_cmdCount
            // 
            this.pic_cmdCount.BackColor = System.Drawing.Color.Black;
            this.pic_cmdCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_cmdCount.Location = new System.Drawing.Point(4, 439);
            this.pic_cmdCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pic_cmdCount.Name = "pic_cmdCount";
            this.tableLayoutPanel1.SetRowSpan(this.pic_cmdCount, 2);
            this.pic_cmdCount.Size = new System.Drawing.Size(1215, 426);
            this.pic_cmdCount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_cmdCount.TabIndex = 8;
            this.pic_cmdCount.TabStop = false;
            // 
            // lbl_Idle_count
            // 
            this.lbl_Idle_count.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_Idle_count.AutoSize = true;
            this.lbl_Idle_count.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Idle_count.Location = new System.Drawing.Point(152, 50);
            this.lbl_Idle_count.Name = "lbl_Idle_count";
            this.lbl_Idle_count.Size = new System.Drawing.Size(22, 20);
            this.lbl_Idle_count.TabIndex = 11;
            this.lbl_Idle_count.Text = "0";
            // 
            // lbl_system_effectiveness
            // 
            this.lbl_system_effectiveness.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_system_effectiveness.AutoSize = true;
            this.lbl_system_effectiveness.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_system_effectiveness.Location = new System.Drawing.Point(152, 190);
            this.lbl_system_effectiveness.Name = "lbl_system_effectiveness";
            this.lbl_system_effectiveness.Size = new System.Drawing.Size(22, 24);
            this.lbl_system_effectiveness.TabIndex = 11;
            this.lbl_system_effectiveness.Text = "0";
            // 
            // lbl_mcs_cmd_queue_count
            // 
            this.lbl_mcs_cmd_queue_count.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_mcs_cmd_queue_count.AutoSize = true;
            this.lbl_mcs_cmd_queue_count.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mcs_cmd_queue_count.Location = new System.Drawing.Point(152, 120);
            this.lbl_mcs_cmd_queue_count.Name = "lbl_mcs_cmd_queue_count";
            this.lbl_mcs_cmd_queue_count.Size = new System.Drawing.Size(22, 20);
            this.lbl_mcs_cmd_queue_count.TabIndex = 11;
            this.lbl_mcs_cmd_queue_count.Text = "0";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.lbl_system_effectiveness, 0, 8);
            this.tableLayoutPanel5.Controls.Add(this.lbl_mcs_cmd_queue_count, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.progress_bar_ohxc_effectiveness, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.lbl_Idle_count, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.progress_bar_mcs_cmd_queue_status, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.progress_bar_vehicle_status, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1226, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 9;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(327, 428);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "Idle Vehicle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "MCS Command Queue Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "OHxC Effectiveness";
            // 
            // uctl_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "uctl_Dashboard";
            this.Size = new System.Drawing.Size(1556, 870);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_cmdQueueTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cmdCount)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CCWin.SkinControl.SkinProgressBar progress_bar_vehicle_status;
        private CCWin.SkinControl.SkinProgressBar progress_bar_ohxc_effectiveness;
        private CCWin.SkinControl.SkinProgressBar progress_bar_mcs_cmd_queue_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Idle_count;
        private System.Windows.Forms.Label lbl_system_effectiveness;
        private System.Windows.Forms.Label lbl_mcs_cmd_queue_count;
        private System.Windows.Forms.PictureBox pic_cmdQueueTime;
        private System.Windows.Forms.PictureBox pic_cmdCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
