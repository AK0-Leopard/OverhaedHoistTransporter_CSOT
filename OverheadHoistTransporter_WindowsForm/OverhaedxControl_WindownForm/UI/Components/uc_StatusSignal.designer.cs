namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.MyUserControl
{
    partial class uc_StatusSignal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_StatusSignal));
            this.pic_StatusSignal_Value = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_StatusSignal_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_StatusSignal_Value
            // 
            this.pic_StatusSignal_Value.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pic_StatusSignal_Value.ErrorImage")));
            this.pic_StatusSignal_Value.Image = ((System.Drawing.Image)(resources.GetObject("pic_StatusSignal_Value.Image")));
            this.pic_StatusSignal_Value.InitialImage = ((System.Drawing.Image)(resources.GetObject("pic_StatusSignal_Value.InitialImage")));
            this.pic_StatusSignal_Value.Location = new System.Drawing.Point(0, 0);
            this.pic_StatusSignal_Value.Margin = new System.Windows.Forms.Padding(0);
            this.pic_StatusSignal_Value.Name = "pic_StatusSignal_Value";
            this.pic_StatusSignal_Value.Size = new System.Drawing.Size(15, 15);
            this.pic_StatusSignal_Value.TabIndex = 0;
            this.pic_StatusSignal_Value.TabStop = false;
            // 
            // uc_StatusSignal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pic_StatusSignal_Value);
            this.Name = "uc_StatusSignal";
            this.Size = new System.Drawing.Size(15, 15);
            ((System.ComponentModel.ISupportInitialize)(this.pic_StatusSignal_Value)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_StatusSignal_Value;
    }
}
