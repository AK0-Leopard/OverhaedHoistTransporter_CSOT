namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    partial class uc_LockStatus
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_unlock = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.lbl_lock = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.lbl_title = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_unlock, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_lock, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_title, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 60);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // lbl_unlock
            // 
            this.lbl_unlock.AutoSize = true;
            this.lbl_unlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            this.lbl_unlock.DisplayName = "Unlock";
            this.lbl_unlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_unlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lbl_unlock.ForeColor = System.Drawing.Color.White;
            this.lbl_unlock.Location = new System.Drawing.Point(101, 35);
            this.lbl_unlock.Margin = new System.Windows.Forms.Padding(1, 5, 0, 0);
            this.lbl_unlock.Name = "lbl_unlock";
            this.lbl_unlock.Size = new System.Drawing.Size(99, 25);
            this.lbl_unlock.TabIndex = 6;
            this.lbl_unlock.Text = "Unlock";
            this.lbl_unlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_lock
            // 
            this.lbl_lock.AutoSize = true;
            this.lbl_lock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.lbl_lock.DisplayName = "Lock";
            this.lbl_lock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_lock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.lbl_lock.ForeColor = System.Drawing.Color.White;
            this.lbl_lock.Location = new System.Drawing.Point(0, 35);
            this.lbl_lock.Margin = new System.Windows.Forms.Padding(0, 5, 1, 0);
            this.lbl_lock.Name = "lbl_lock";
            this.lbl_lock.Size = new System.Drawing.Size(99, 25);
            this.lbl_lock.TabIndex = 5;
            this.lbl_lock.Text = "Lock";
            this.lbl_lock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_title, 2);
            this.lbl_title.DisplayName = "Non";
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.Black;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(200, 30);
            this.lbl_title.TabIndex = 7;
            this.lbl_title.Text = "[[Non]]";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uc_LockStatus
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "uc_LockStatus";
            this.Size = new System.Drawing.Size(200, 60);
            this.Load += new System.EventHandler(this.MainSignalBackGround_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_lock;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_unlock;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_title;
    }
}
