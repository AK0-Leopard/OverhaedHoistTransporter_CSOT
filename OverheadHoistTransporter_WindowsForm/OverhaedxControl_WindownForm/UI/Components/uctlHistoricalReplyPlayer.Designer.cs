namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    partial class uctlHistoricalReplyPlayer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_StartDTCbx = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.m_EndDTCbx = new System.Windows.Forms.DateTimePicker();
            this.btn_playRate_8 = new System.Windows.Forms.Button();
            this.btn_playRate_4 = new System.Windows.Forms.Button();
            this.btn_playRate_2 = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.btn_stop1 = new System.Windows.Forms.Button();
            this.btn_ctu = new System.Windows.Forms.Button();
            this.btn_loaddata = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_playRate_1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_StartSearch = new System.Windows.Forms.Button();
            this.cmb_vh_id = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.30864F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.69136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.m_StartDTCbx, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_EndDTCbx, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_playRate_8, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_playRate_4, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_playRate_2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_pause, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_stop1, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_ctu, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_loaddata, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_playRate_1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_StartSearch, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmb_vh_id, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.192983F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.80701F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(406, 699);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_StartDTCbx
            // 
            this.m_StartDTCbx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.m_StartDTCbx, 2);
            this.m_StartDTCbx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.m_StartDTCbx.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.m_StartDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_StartDTCbx.Location = new System.Drawing.Point(3, 24);
            this.m_StartDTCbx.Name = "m_StartDTCbx";
            this.m_StartDTCbx.Size = new System.Drawing.Size(197, 26);
            this.m_StartDTCbx.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(206, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 69;
            this.label2.Text = "End Time";
            // 
            // m_EndDTCbx
            // 
            this.m_EndDTCbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.m_EndDTCbx, 2);
            this.m_EndDTCbx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.m_EndDTCbx.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.m_EndDTCbx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_EndDTCbx.Location = new System.Drawing.Point(206, 24);
            this.m_EndDTCbx.Name = "m_EndDTCbx";
            this.m_EndDTCbx.Size = new System.Drawing.Size(197, 26);
            this.m_EndDTCbx.TabIndex = 67;
            // 
            // btn_playRate_8
            // 
            this.btn_playRate_8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_playRate_8.Location = new System.Drawing.Point(306, 95);
            this.btn_playRate_8.Name = "btn_playRate_8";
            this.btn_playRate_8.Size = new System.Drawing.Size(97, 31);
            this.btn_playRate_8.TabIndex = 17;
            this.btn_playRate_8.Text = "X8";
            this.btn_playRate_8.UseVisualStyleBackColor = true;
            this.btn_playRate_8.Click += new System.EventHandler(this.btn_playRate_8_Click);
            // 
            // btn_playRate_4
            // 
            this.btn_playRate_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_playRate_4.Location = new System.Drawing.Point(206, 95);
            this.btn_playRate_4.Name = "btn_playRate_4";
            this.btn_playRate_4.Size = new System.Drawing.Size(94, 31);
            this.btn_playRate_4.TabIndex = 16;
            this.btn_playRate_4.Text = "X4";
            this.btn_playRate_4.UseVisualStyleBackColor = true;
            this.btn_playRate_4.Click += new System.EventHandler(this.btn_playRate_4_Click);
            // 
            // btn_playRate_2
            // 
            this.btn_playRate_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_playRate_2.Location = new System.Drawing.Point(104, 95);
            this.btn_playRate_2.Name = "btn_playRate_2";
            this.btn_playRate_2.Size = new System.Drawing.Size(96, 31);
            this.btn_playRate_2.TabIndex = 15;
            this.btn_playRate_2.Text = "X2";
            this.btn_playRate_2.UseVisualStyleBackColor = true;
            this.btn_playRate_2.Click += new System.EventHandler(this.btn_playRate_2_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_pause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_pause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_pause.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pause.ForeColor = System.Drawing.Color.Black;
            this.btn_pause.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_pause.ImageKey = "player_pause.png";
            this.btn_pause.Location = new System.Drawing.Point(104, 132);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(96, 35);
            this.btn_pause.TabIndex = 9;
            this.btn_pause.Text = "Pause";
            this.btn_pause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_pause.UseVisualStyleBackColor = false;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // btn_stop1
            // 
            this.btn_stop1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_stop1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_stop1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_stop1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_stop1.ForeColor = System.Drawing.Color.Black;
            this.btn_stop1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_stop1.ImageKey = "player_stop.png";
            this.btn_stop1.Location = new System.Drawing.Point(306, 132);
            this.btn_stop1.Name = "btn_stop1";
            this.btn_stop1.Size = new System.Drawing.Size(97, 35);
            this.btn_stop1.TabIndex = 13;
            this.btn_stop1.Text = "Stop";
            this.btn_stop1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_stop1.UseVisualStyleBackColor = false;
            this.btn_stop1.Click += new System.EventHandler(this.btn_stop1_Click);
            // 
            // btn_ctu
            // 
            this.btn_ctu.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_ctu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ctu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ctu.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ctu.ForeColor = System.Drawing.Color.Black;
            this.btn_ctu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_ctu.ImageKey = "ctu.png";
            this.btn_ctu.Location = new System.Drawing.Point(206, 132);
            this.btn_ctu.Name = "btn_ctu";
            this.btn_ctu.Size = new System.Drawing.Size(94, 35);
            this.btn_ctu.TabIndex = 12;
            this.btn_ctu.Text = "Continue";
            this.btn_ctu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ctu.UseVisualStyleBackColor = false;
            this.btn_ctu.Click += new System.EventHandler(this.btn_ctu_Click);
            // 
            // btn_loaddata
            // 
            this.btn_loaddata.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_loaddata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_loaddata.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_loaddata.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loaddata.ForeColor = System.Drawing.Color.Black;
            this.btn_loaddata.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_loaddata.ImageKey = "play-circle-128.png";
            this.btn_loaddata.Location = new System.Drawing.Point(3, 132);
            this.btn_loaddata.Name = "btn_loaddata";
            this.btn_loaddata.Size = new System.Drawing.Size(95, 35);
            this.btn_loaddata.TabIndex = 3;
            this.btn_loaddata.Text = "Play";
            this.btn_loaddata.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_loaddata.UseVisualStyleBackColor = false;
            this.btn_loaddata.Click += new System.EventHandler(this.btn_loaddata_Click);
            // 
            // listBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 4);
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(5, 175);
            this.listBox1.Margin = new System.Windows.Forms.Padding(5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(396, 519);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btn_playRate_1
            // 
            this.btn_playRate_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_playRate_1.Location = new System.Drawing.Point(3, 95);
            this.btn_playRate_1.Name = "btn_playRate_1";
            this.btn_playRate_1.Size = new System.Drawing.Size(95, 31);
            this.btn_playRate_1.TabIndex = 14;
            this.btn_playRate_1.Text = "X1";
            this.btn_playRate_1.UseVisualStyleBackColor = true;
            this.btn_playRate_1.Click += new System.EventHandler(this.btn_playRate_1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 68;
            this.label1.Text = "Start Time";
            // 
            // btn_StartSearch
            // 
            this.btn_StartSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_StartSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StartSearch.Location = new System.Drawing.Point(203, 55);
            this.btn_StartSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btn_StartSearch.Name = "btn_StartSearch";
            this.btn_StartSearch.Size = new System.Drawing.Size(100, 37);
            this.btn_StartSearch.TabIndex = 70;
            this.btn_StartSearch.Text = "Search";
            this.btn_StartSearch.UseVisualStyleBackColor = true;
            this.btn_StartSearch.Click += new System.EventHandler(this.btn_StartSearch_Click);
            // 
            // cmb_vh_id
            // 
            this.cmb_vh_id.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_vh_id.FormattingEnabled = true;
            this.cmb_vh_id.Location = new System.Drawing.Point(306, 58);
            this.cmb_vh_id.Name = "cmb_vh_id";
            this.cmb_vh_id.Size = new System.Drawing.Size(97, 32);
            this.cmb_vh_id.TabIndex = 71;
            this.cmb_vh_id.SelectedIndexChanged += new System.EventHandler(this.cmb_vh_id_SelectedIndexChanged);
            // 
            // uctlHistoricalReplyPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "uctlHistoricalReplyPlayer";
            this.Size = new System.Drawing.Size(406, 699);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_stop1;
        private System.Windows.Forms.Button btn_ctu;
        private System.Windows.Forms.Button btn_pause;
        private System.Windows.Forms.Button btn_loaddata;
        private System.Windows.Forms.Button btn_playRate_8;
        private System.Windows.Forms.Button btn_playRate_4;
        private System.Windows.Forms.Button btn_playRate_2;
        private System.Windows.Forms.Button btn_playRate_1;
        private System.Windows.Forms.DateTimePicker m_EndDTCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_StartSearch;
        private System.Windows.Forms.DateTimePicker m_StartDTCbx;
        private System.Windows.Forms.ComboBox cmb_vh_id;
    }
}
