namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    partial class uc_SystemSignal
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_Title_Name = new System.Windows.Forms.Label();
            this.pic_Signal_Value = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Signal_Value)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_Title_Name
            // 
            this.lab_Title_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_Title_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_Title_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lab_Title_Name.ForeColor = System.Drawing.Color.White;
            this.lab_Title_Name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lab_Title_Name.Location = new System.Drawing.Point(0, 5);
            this.lab_Title_Name.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lab_Title_Name.Name = "lab_Title_Name";
            this.lab_Title_Name.Size = new System.Drawing.Size(110, 21);
            this.lab_Title_Name.TabIndex = 0;
            this.lab_Title_Name.Text = "Title Name";
            this.lab_Title_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pic_Signal_Value
            // 
            this.pic_Signal_Value.BackColor = System.Drawing.Color.Transparent;
            this.pic_Signal_Value.Image = global::com.mirle.ibg3k0.ohxc.winform.Properties.SystemIcon.icon_Link_ON;
            this.pic_Signal_Value.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pic_Signal_Value.Location = new System.Drawing.Point(30, 34);
            this.pic_Signal_Value.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pic_Signal_Value.Name = "pic_Signal_Value";
            this.pic_Signal_Value.Size = new System.Drawing.Size(50, 50);
            this.pic_Signal_Value.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Signal_Value.TabIndex = 9;
            this.pic_Signal_Value.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.pic_Signal_Value);
            this.panel1.Controls.Add(this.lab_Title_Name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 90);
            this.panel1.TabIndex = 28;
            // 
            // uc_SystemSignal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "uc_SystemSignal";
            this.Size = new System.Drawing.Size(110, 90);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Signal_Value)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lab_Title_Name;
        private System.Windows.Forms.PictureBox pic_Signal_Value;
        private System.Windows.Forms.Panel panel1;
    }
}
