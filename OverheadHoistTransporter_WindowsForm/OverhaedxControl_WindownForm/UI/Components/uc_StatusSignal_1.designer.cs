namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    partial class uc_StatusSignal_1
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
            this.pl_signal_value = new System.Windows.Forms.Panel();
            this.lab_Title_Name = new System.Windows.Forms.Label();
            this.pl_signal_value.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_signal_value
            // 
            this.pl_signal_value.BackColor = System.Drawing.Color.White;
            this.pl_signal_value.Controls.Add(this.lab_Title_Name);
            this.pl_signal_value.Location = new System.Drawing.Point(5, 5);
            this.pl_signal_value.Margin = new System.Windows.Forms.Padding(0);
            this.pl_signal_value.Name = "pl_signal_value";
            this.pl_signal_value.Size = new System.Drawing.Size(240, 40);
            this.pl_signal_value.TabIndex = 13;
            // 
            // lab_Title_Name
            // 
            this.lab_Title_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_Title_Name.BackColor = System.Drawing.Color.Transparent;
            this.lab_Title_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(34)))));
            this.lab_Title_Name.Location = new System.Drawing.Point(0, 0);
            this.lab_Title_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Title_Name.Name = "lab_Title_Name";
            this.lab_Title_Name.Size = new System.Drawing.Size(240, 40);
            this.lab_Title_Name.TabIndex = 12;
            this.lab_Title_Name.Text = "Title Name";
            this.lab_Title_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uc_StatusSignal_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Controls.Add(this.pl_signal_value);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "uc_StatusSignal_1";
            this.Size = new System.Drawing.Size(250, 50);
            this.pl_signal_value.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_signal_value;
        private System.Windows.Forms.Label lab_Title_Name;
    }
}
