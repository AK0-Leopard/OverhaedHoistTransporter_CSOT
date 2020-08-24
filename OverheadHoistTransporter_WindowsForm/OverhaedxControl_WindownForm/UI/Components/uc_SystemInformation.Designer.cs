namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    partial class uc_SystemInformation
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
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_Title_Value = new System.Windows.Forms.Label();
            this.lab_Title_Name = new System.Windows.Forms.Label();
            this.tableLayoutPanel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel18.Controls.Add(this.lab_Title_Value, 1, 0);
            this.tableLayoutPanel18.Controls.Add(this.lab_Title_Name, 0, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.tableLayoutPanel18.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel18.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(441, 38);
            this.tableLayoutPanel18.TabIndex = 27;
            // 
            // lab_Title_Value
            // 
            this.lab_Title_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_Title_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_Title_Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.lab_Title_Value.ForeColor = System.Drawing.Color.White;
            this.lab_Title_Value.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lab_Title_Value.Location = new System.Drawing.Point(160, 0);
            this.lab_Title_Value.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Title_Value.Name = "lab_Title_Value";
            this.lab_Title_Value.Size = new System.Drawing.Size(281, 38);
            this.lab_Title_Value.TabIndex = 0;
            this.lab_Title_Value.Text = "TitleValue";
            this.lab_Title_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lab_Title_Name
            // 
            this.lab_Title_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_Title_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_Title_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.lab_Title_Name.ForeColor = System.Drawing.Color.White;
            this.lab_Title_Name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lab_Title_Name.Location = new System.Drawing.Point(0, 0);
            this.lab_Title_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Title_Name.Name = "lab_Title_Name";
            this.lab_Title_Name.Size = new System.Drawing.Size(160, 38);
            this.lab_Title_Name.TabIndex = 0;
            this.lab_Title_Name.Text = "TitleName";
            this.lab_Title_Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uc_SystemInformation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel18);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "uc_SystemInformation";
            this.Size = new System.Drawing.Size(441, 38);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.Label lab_Title_Name;
        private System.Windows.Forms.Label lab_Title_Value;

    }
}
