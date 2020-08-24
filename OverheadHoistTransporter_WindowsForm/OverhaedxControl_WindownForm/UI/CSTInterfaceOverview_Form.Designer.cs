namespace com.mirle.ibg3k0.ohxc.winform.UI
{
    partial class CSTInterfaceOverview_Form
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
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cmb_VH = new System.Windows.Forms.ComboBox();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.dgv_cst_transferList = new System.Windows.Forms.DataGridView();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_load_count_name = new System.Windows.Forms.Label();
            this.lbl_unload_count_name = new System.Windows.Forms.Label();
            this.lbl_loadunload_count_name = new System.Windows.Forms.Label();
            this.lbl_load_count_value = new System.Windows.Forms.Label();
            this.lbl_unload_count_value = new System.Windows.Forms.Label();
            this.lbl_loadunload_count_value = new System.Windows.Forms.Label();
            this.lbl_ok_count_name = new System.Windows.Forms.Label();
            this.lbl_ok_count_value = new System.Windows.Forms.Label();
            this.lbl_ng_count_name = new System.Windows.Forms.Label();
            this.lbl_ng_count_value = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cst_transferList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_VH
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmb_VH, 3);
            this.cmb_VH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_VH.FormattingEnabled = true;
            this.cmb_VH.Location = new System.Drawing.Point(133, 46);
            this.cmb_VH.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cmb_VH.Name = "cmb_VH";
            this.cmb_VH.Size = new System.Drawing.Size(357, 30);
            this.cmb_VH.TabIndex = 1;
            // 
            // dtp_start
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtp_start, 3);
            this.dtp_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtp_start.Location = new System.Drawing.Point(131, 3);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(361, 30);
            this.dtp_start.TabIndex = 28;
            // 
            // dgv_cst_transferList
            // 
            this.dgv_cst_transferList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cst_transferList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.time,
            this.port_id,
            this.port_type,
            this.judge_result});
            this.tableLayoutPanel1.SetColumnSpan(this.dgv_cst_transferList, 4);
            this.dgv_cst_transferList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_cst_transferList.Location = new System.Drawing.Point(3, 195);
            this.dgv_cst_transferList.MultiSelect = false;
            this.dgv_cst_transferList.Name = "dgv_cst_transferList";
            this.dgv_cst_transferList.ReadOnly = true;
            this.dgv_cst_transferList.RowTemplate.Height = 24;
            this.dgv_cst_transferList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_cst_transferList.Size = new System.Drawing.Size(489, 649);
            this.dgv_cst_transferList.TabIndex = 30;
            this.dgv_cst_transferList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgv_cst_transferList_RowPrePaint);
            this.dgv_cst_transferList.SelectionChanged += new System.EventHandler(this.dgv_cst_transferList_SelectionChanged);
            // 
            // time
            // 
            this.time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.time.DataPropertyName = "sStart_time";
            this.time.HeaderText = "Time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.Width = 75;
            // 
            // port_id
            // 
            this.port_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.port_id.DataPropertyName = "Port_ID";
            this.port_id.HeaderText = "Port";
            this.port_id.Name = "port_id";
            this.port_id.ReadOnly = true;
            this.port_id.Width = 75;
            // 
            // port_type
            // 
            this.port_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.port_type.DataPropertyName = "sPortType";
            this.port_type.HeaderText = "Type";
            this.port_type.Name = "port_type";
            this.port_type.ReadOnly = true;
            this.port_type.Width = 75;
            // 
            // judge_result
            // 
            this.judge_result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.judge_result.DataPropertyName = "sJudgeResult";
            this.judge_result.HeaderText = "Judge";
            this.judge_result.Name = "judge_result";
            this.judge_result.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_cst_transferList, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dtp_start, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_VH, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Search, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1523, 847);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 22);
            this.label1.TabIndex = 32;
            this.label1.Text = "Data";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 22);
            this.label2.TabIndex = 33;
            this.label2.Text = "Vehicle ID";
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Search.Location = new System.Drawing.Point(405, 83);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(87, 34);
            this.btn_Search.TabIndex = 31;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.chart1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.chart2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(498, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 5);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 841F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1022, 841);
            this.tableLayoutPanel2.TabIndex = 36;
            // 
            // chart1
            // 
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(514, 3);
            this.chart1.Name = "chart1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(505, 835);
            this.chart1.TabIndex = 35;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart2.Location = new System.Drawing.Point(3, 3);
            this.chart2.Name = "chart2";
            series4.Name = "Series1";
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(505, 835);
            this.chart2.TabIndex = 35;
            this.chart2.Text = "chart1";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 4);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_load_count_name, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_unload_count_name, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_loadunload_count_name, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_load_count_value, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_unload_count_value, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_loadunload_count_value, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_ok_count_name, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_ok_count_value, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_ng_count_name, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_ng_count_value, 4, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 123);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(489, 66);
            this.tableLayoutPanel3.TabIndex = 37;
            // 
            // lbl_load_count_name
            // 
            this.lbl_load_count_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_load_count_name.AutoSize = true;
            this.lbl_load_count_name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_load_count_name.Location = new System.Drawing.Point(24, 7);
            this.lbl_load_count_name.Name = "lbl_load_count_name";
            this.lbl_load_count_name.Size = new System.Drawing.Size(54, 19);
            this.lbl_load_count_name.TabIndex = 0;
            this.lbl_load_count_name.Text = "Load:";
            // 
            // lbl_unload_count_name
            // 
            this.lbl_unload_count_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_unload_count_name.AutoSize = true;
            this.lbl_unload_count_name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_unload_count_name.Location = new System.Drawing.Point(168, 7);
            this.lbl_unload_count_name.Name = "lbl_unload_count_name";
            this.lbl_unload_count_name.Size = new System.Drawing.Size(72, 19);
            this.lbl_unload_count_name.TabIndex = 1;
            this.lbl_unload_count_name.Text = "Unload:";
            // 
            // lbl_loadunload_count_name
            // 
            this.lbl_loadunload_count_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_loadunload_count_name.AutoSize = true;
            this.lbl_loadunload_count_name.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_loadunload_count_name.Location = new System.Drawing.Point(339, 7);
            this.lbl_loadunload_count_name.Name = "lbl_loadunload_count_name";
            this.lbl_loadunload_count_name.Size = new System.Drawing.Size(63, 19);
            this.lbl_loadunload_count_name.TabIndex = 2;
            this.lbl_loadunload_count_name.Text = "Total:";
            // 
            // lbl_load_count_value
            // 
            this.lbl_load_count_value.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_load_count_value.AutoSize = true;
            this.lbl_load_count_value.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_load_count_value.Location = new System.Drawing.Point(84, 7);
            this.lbl_load_count_value.Name = "lbl_load_count_value";
            this.lbl_load_count_value.Size = new System.Drawing.Size(18, 19);
            this.lbl_load_count_value.TabIndex = 3;
            this.lbl_load_count_value.Text = "0";
            // 
            // lbl_unload_count_value
            // 
            this.lbl_unload_count_value.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_unload_count_value.AutoSize = true;
            this.lbl_unload_count_value.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_unload_count_value.Location = new System.Drawing.Point(246, 7);
            this.lbl_unload_count_value.Name = "lbl_unload_count_value";
            this.lbl_unload_count_value.Size = new System.Drawing.Size(18, 19);
            this.lbl_unload_count_value.TabIndex = 3;
            this.lbl_unload_count_value.Text = "0";
            // 
            // lbl_loadunload_count_value
            // 
            this.lbl_loadunload_count_value.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_loadunload_count_value.AutoSize = true;
            this.lbl_loadunload_count_value.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_loadunload_count_value.Location = new System.Drawing.Point(408, 7);
            this.lbl_loadunload_count_value.Name = "lbl_loadunload_count_value";
            this.lbl_loadunload_count_value.Size = new System.Drawing.Size(18, 19);
            this.lbl_loadunload_count_value.TabIndex = 3;
            this.lbl_loadunload_count_value.Text = "0";
            // 
            // lbl_ok_count_name
            // 
            this.lbl_ok_count_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ok_count_name.AutoSize = true;
            this.lbl_ok_count_name.ForeColor = System.Drawing.Color.SeaGreen;
            this.lbl_ok_count_name.Location = new System.Drawing.Point(119, 38);
            this.lbl_ok_count_name.Name = "lbl_ok_count_name";
            this.lbl_ok_count_name.Size = new System.Drawing.Size(40, 22);
            this.lbl_ok_count_name.TabIndex = 4;
            this.lbl_ok_count_name.Text = "OK:";
            // 
            // lbl_ok_count_value
            // 
            this.lbl_ok_count_value.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_ok_count_value.AutoSize = true;
            this.lbl_ok_count_value.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ok_count_value.ForeColor = System.Drawing.Color.SeaGreen;
            this.lbl_ok_count_value.Location = new System.Drawing.Point(165, 38);
            this.lbl_ok_count_value.Name = "lbl_ok_count_value";
            this.lbl_ok_count_value.Size = new System.Drawing.Size(20, 22);
            this.lbl_ok_count_value.TabIndex = 3;
            this.lbl_ok_count_value.Text = "0";
            // 
            // lbl_ng_count_name
            // 
            this.lbl_ng_count_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_ng_count_name.AutoSize = true;
            this.lbl_ng_count_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_ng_count_name.Location = new System.Drawing.Point(281, 38);
            this.lbl_ng_count_name.Name = "lbl_ng_count_name";
            this.lbl_ng_count_name.Size = new System.Drawing.Size(40, 22);
            this.lbl_ng_count_name.TabIndex = 4;
            this.lbl_ng_count_name.Text = "NG:";
            // 
            // lbl_ng_count_value
            // 
            this.lbl_ng_count_value.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_ng_count_value.AutoSize = true;
            this.lbl_ng_count_value.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ng_count_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_ng_count_value.Location = new System.Drawing.Point(327, 38);
            this.lbl_ng_count_value.Name = "lbl_ng_count_value";
            this.lbl_ng_count_value.Size = new System.Drawing.Size(20, 22);
            this.lbl_ng_count_value.TabIndex = 3;
            this.lbl_ng_count_value.Text = "0";
            // 
            // CSTInterfaceOverview_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1523, 847);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "CSTInterfaceOverview_Form";
            this.Text = "CST Interface Overview";
            this.Load += new System.EventHandler(this.CSTInterfaceOverview_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cst_transferList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmb_VH;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.DataGridView dgv_cst_transferList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn port_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn port_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge_result;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbl_load_count_name;
        private System.Windows.Forms.Label lbl_unload_count_name;
        private System.Windows.Forms.Label lbl_loadunload_count_name;
        private System.Windows.Forms.Label lbl_load_count_value;
        private System.Windows.Forms.Label lbl_unload_count_value;
        private System.Windows.Forms.Label lbl_loadunload_count_value;
        private System.Windows.Forms.Label lbl_ok_count_name;
        private System.Windows.Forms.Label lbl_ok_count_value;
        private System.Windows.Forms.Label lbl_ng_count_name;
        private System.Windows.Forms.Label lbl_ng_count_value;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}