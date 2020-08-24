namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    partial class uc_DeviceStatusSignal
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
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Signal_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tableLayoutPanel1.Controls.Add(this.pic_Signal_Value, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lab_Title_Name, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(197, 30);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pic_Signal_Value
            // 
            this.pic_Signal_Value.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_Signal_Value.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.Resources.Connection_Control_Off;
            this.pic_Signal_Value.Location = new System.Drawing.Point(15, 5);
            this.pic_Signal_Value.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Signal_Value.Name = "pic_Signal_Value";
            this.pic_Signal_Value.Size = new System.Drawing.Size(20, 20);
            this.pic_Signal_Value.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Signal_Value.TabIndex = 0;
            this.pic_Signal_Value.TabStop = false;
            // 
            // lab_Title_Name
            // 
            this.lab_Title_Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_Title_Name.AutoSize = true;
            this.lab_Title_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.lab_Title_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.lab_Title_Name.Location = new System.Drawing.Point(50, 4);
            this.lab_Title_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Title_Name.Name = "lab_Title_Name";
            this.lab_Title_Name.Size = new System.Drawing.Size(97, 22);
            this.lab_Title_Name.TabIndex = 1;
            this.lab_Title_Name.Text = "Title Name";
            // 
            // uc_DeviceStatusSignal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "uc_DeviceStatusSignal";
            this.Size = new System.Drawing.Size(197, 30);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Signal_Value)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pic_Signal_Value;
        private System.Windows.Forms.Label lab_Title_Name;
    }
}
