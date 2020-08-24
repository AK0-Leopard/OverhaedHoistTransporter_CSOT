namespace com.mirle.ibg3k0.ohxc.winform.UI
{
    partial class CSTInterface_Form
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Search = new System.Windows.Forms.Button();
            this.cmb_TransList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_FT = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmb_VH = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(338, 4);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(121, 20);
            this.cmbPort.TabIndex = 28;
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.cmbPort_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "Port List:";
            // 
            // dtp_start
            // 
            this.dtp_start.Location = new System.Drawing.Point(68, 5);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(107, 22);
            this.dtp_start.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(23, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 720);
            this.panel1.TabIndex = 25;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(100, 61);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 24;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // cmb_TransList
            // 
            this.cmb_TransList.FormattingEnabled = true;
            this.cmb_TransList.Location = new System.Drawing.Point(338, 36);
            this.cmb_TransList.Name = "cmb_TransList";
            this.cmb_TransList.Size = new System.Drawing.Size(178, 20);
            this.cmb_TransList.TabIndex = 23;
            this.cmb_TransList.SelectedIndexChanged += new System.EventHandler(this.cmb_TransList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "Transaction List:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "Date:";
            // 
            // cmb_FT
            // 
            this.cmb_FT.FormattingEnabled = true;
            this.cmb_FT.Location = new System.Drawing.Point(535, 2);
            this.cmb_FT.Name = "cmb_FT";
            this.cmb_FT.Size = new System.Drawing.Size(121, 20);
            this.cmb_FT.TabIndex = 20;
            this.cmb_FT.SelectedIndexChanged += new System.EventHandler(this.cmb_FT_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "Flow Type:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(871, 835);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(165, 22);
            this.textBox1.TabIndex = 18;
            // 
            // cmb_VH
            // 
            this.cmb_VH.FormattingEnabled = true;
            this.cmb_VH.Location = new System.Drawing.Point(68, 33);
            this.cmb_VH.Name = "cmb_VH";
            this.cmb_VH.Size = new System.Drawing.Size(107, 20);
            this.cmb_VH.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "Vehicle ID:";
            // 
            // chart1
            // 
            this.chart1.Location = new System.Drawing.Point(963, 122);
            this.chart1.Name = "chart1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(900, 720);
            this.chart1.TabIndex = 29;
            this.chart1.Text = "chart1";
            // 
            // CSTInterface_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1891, 842);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtp_start);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.cmb_TransList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_FT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmb_VH);
            this.Controls.Add(this.label1);
            this.Name = "CSTInterface_Form";
            this.Text = "CST Interface ";
            this.Load += new System.EventHandler(this.CSTInterface_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.ComboBox cmb_TransList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_FT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmb_VH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}