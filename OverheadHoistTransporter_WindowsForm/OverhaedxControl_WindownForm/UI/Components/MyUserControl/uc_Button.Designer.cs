namespace com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl
{
    partial class uc_Button
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
            this.components = new System.ComponentModel.Container();
            this.btn_button = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // btn_button
            // 
            this.btn_button.BackColor = System.Drawing.Color.Transparent;
            this.btn_button.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.btn_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.btn_button.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_button.DownBack = null;
            this.btn_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.btn_button.ForeColor = System.Drawing.Color.White;
            this.btn_button.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.btn_button.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.btn_button.IsDrawGlass = false;
            this.btn_button.Location = new System.Drawing.Point(0, 0);
            this.btn_button.Margin = new System.Windows.Forms.Padding(0);
            this.btn_button.MouseBack = null;
            this.btn_button.MouseBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.btn_button.Name = "btn_button";
            this.btn_button.NormlBack = null;
            this.btn_button.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btn_button.Radius = 10;
            this.btn_button.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.btn_button.Size = new System.Drawing.Size(150, 35);
            this.btn_button.TabIndex = 1;
            this.btn_button.Text = "Button";
            this.btn_button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_button.UseVisualStyleBackColor = false;
            this.btn_button.Click += new System.EventHandler(this.btn_Close_Click);
            this.btn_button.MouseEnter += new System.EventHandler(this.btn_Close_MouseEnter);
            this.btn_button.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
            // 
            // uc_Button
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btn_button);
            this.Name = "uc_Button";
            this.Size = new System.Drawing.Size(150, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinButton btn_button;
    }
}
