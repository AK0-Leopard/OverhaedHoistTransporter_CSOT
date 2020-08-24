namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    partial class uc_ControlStatusSignal
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
            this.pic_Signal_Value = new System.Windows.Forms.PictureBox();
            this.lab_Title_Name = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Signal_Value)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel1.Controls.Add(this.lab_Title_Name, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pic_Signal_Value, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 20);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pic_Signal_Value
            // 
            this.pic_Signal_Value.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_Signal_Value.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.Control_status_LinkOk;
            this.pic_Signal_Value.Location = new System.Drawing.Point(7, 2);
            this.pic_Signal_Value.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Signal_Value.Name = "pic_Signal_Value";
            this.pic_Signal_Value.Size = new System.Drawing.Size(15, 15);
            this.pic_Signal_Value.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Signal_Value.TabIndex = 0;
            this.pic_Signal_Value.TabStop = false;
            // 
            // lab_Title_Name
            // 
            this.lab_Title_Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_Title_Name.AutoSize = true;
            this.lab_Title_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lab_Title_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.lab_Title_Name.Location = new System.Drawing.Point(30, 1);
            this.lab_Title_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Title_Name.Name = "lab_Title_Name";
            this.lab_Title_Name.Size = new System.Drawing.Size(76, 17);
            this.lab_Title_Name.TabIndex = 2;
            this.lab_Title_Name.Text = "Title Name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 30);
            this.panel1.TabIndex = 1;
            // 
            // uc_ControlStatusSignal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "uc_ControlStatusSignal";
            this.Size = new System.Drawing.Size(250, 30);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Signal_Value)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pic_Signal_Value;
        private System.Windows.Forms.Label lab_Title_Name;
        private System.Windows.Forms.Panel panel1;
    }
}
