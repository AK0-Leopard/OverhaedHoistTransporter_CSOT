namespace com.mirle.ibg3k0.ohxc.winform.UI.Operate
{
    partial class frmMTL_MTSOperation
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.uc_LockStatus1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uc_LockStatus();
            this.uc_LockStatus2 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uc_LockStatus();
            this.uc_MTLMTSInfo1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.uc_MTLMTSInfo();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mtl_mode_name = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.lbl_mts_mode_name = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.lbl_mtl_command_name = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.lbl_mtl_mode_value = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.lbl_mts_mode_value = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.875F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.uc_MTLMTSInfo1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1094, 724);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.45064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.45064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.09872F));
            this.tableLayoutPanel2.Controls.Add(this.uc_LockStatus1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.uc_LockStatus2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 86);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1094, 101);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // uc_LockStatus1
            // 
            this.uc_LockStatus1.BackColor = System.Drawing.Color.Transparent;
            this.uc_LockStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_LockStatus1.Location = new System.Drawing.Point(379, 25);
            this.uc_LockStatus1.Margin = new System.Windows.Forms.Padding(90, 25, 0, 15);
            this.uc_LockStatus1.Name = "uc_LockStatus1";
            this.uc_LockStatus1.Size = new System.Drawing.Size(199, 61);
            this.uc_LockStatus1.TabIndex = 0;
            this.uc_LockStatus1.Title = "MTS_Interlock";
            // 
            // uc_LockStatus2
            // 
            this.uc_LockStatus2.BackColor = System.Drawing.Color.Transparent;
            this.uc_LockStatus2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_LockStatus2.Location = new System.Drawing.Point(90, 25);
            this.uc_LockStatus2.Margin = new System.Windows.Forms.Padding(90, 25, 0, 15);
            this.uc_LockStatus2.Name = "uc_LockStatus2";
            this.uc_LockStatus2.Size = new System.Drawing.Size(199, 61);
            this.uc_LockStatus2.TabIndex = 1;
            this.uc_LockStatus2.Title = "Lifter_Interlock";
            // 
            // uc_MTLMTSInfo1
            // 
            this.uc_MTLMTSInfo1.BackColor = System.Drawing.Color.Transparent;
            this.uc_MTLMTSInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_MTLMTSInfo1.Location = new System.Drawing.Point(0, 187);
            this.uc_MTLMTSInfo1.Margin = new System.Windows.Forms.Padding(0);
            this.uc_MTLMTSInfo1.Name = "uc_MTLMTSInfo1";
            this.uc_MTLMTSInfo1.Padding = new System.Windows.Forms.Padding(65, 0, 61, 46);
            this.uc_MTLMTSInfo1.Size = new System.Drawing.Size(1094, 537);
            this.uc_MTLMTSInfo1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_mtl_mode_name, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_mts_mode_name, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_mtl_command_name, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_mtl_mode_value, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_mts_mode_value, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1094, 86);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // lbl_mtl_mode_name
            // 
            this.lbl_mtl_mode_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_mtl_mode_name.AutoSize = true;
            this.lbl_mtl_mode_name.DisplayName = "MTL_Mode";
            this.lbl_mtl_mode_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_mtl_mode_name.Location = new System.Drawing.Point(51, 4);
            this.lbl_mtl_mode_name.Name = "lbl_mtl_mode_name";
            this.lbl_mtl_mode_name.Size = new System.Drawing.Size(88, 20);
            this.lbl_mtl_mode_name.TabIndex = 0;
            this.lbl_mtl_mode_name.Text = "MTL Mode:";
            // 
            // lbl_mts_mode_name
            // 
            this.lbl_mts_mode_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_mts_mode_name.AutoSize = true;
            this.lbl_mts_mode_name.DisplayName = "MTS_Mode";
            this.lbl_mts_mode_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_mts_mode_name.Location = new System.Drawing.Point(49, 32);
            this.lbl_mts_mode_name.Name = "lbl_mts_mode_name";
            this.lbl_mts_mode_name.Size = new System.Drawing.Size(90, 20);
            this.lbl_mts_mode_name.TabIndex = 0;
            this.lbl_mts_mode_name.Text = "MTS Mode:";
            // 
            // lbl_mtl_command_name
            // 
            this.lbl_mtl_command_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_mtl_command_name.AutoSize = true;
            this.lbl_mtl_command_name.DisplayName = "Command";
            this.lbl_mtl_command_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_mtl_command_name.Location = new System.Drawing.Point(53, 61);
            this.lbl_mtl_command_name.Name = "lbl_mtl_command_name";
            this.lbl_mtl_command_name.Size = new System.Drawing.Size(86, 20);
            this.lbl_mtl_command_name.TabIndex = 0;
            this.lbl_mtl_command_name.Text = "Command:";
            // 
            // lbl_mtl_mode_value
            // 
            this.lbl_mtl_mode_value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_mtl_mode_value.AutoSize = true;
            this.lbl_mtl_mode_value.DisplayName = "Auto";
            this.lbl_mtl_mode_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_mtl_mode_value.Location = new System.Drawing.Point(145, 4);
            this.lbl_mtl_mode_value.Name = "lbl_mtl_mode_value";
            this.lbl_mtl_mode_value.Size = new System.Drawing.Size(946, 20);
            this.lbl_mtl_mode_value.TabIndex = 0;
            this.lbl_mtl_mode_value.Text = "Auto";
            // 
            // lbl_mts_mode_value
            // 
            this.lbl_mts_mode_value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_mts_mode_value.AutoSize = true;
            this.lbl_mts_mode_value.DisplayName = "Auto";
            this.lbl_mts_mode_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_mts_mode_value.Location = new System.Drawing.Point(145, 32);
            this.lbl_mts_mode_value.Name = "lbl_mts_mode_value";
            this.lbl_mts_mode_value.Size = new System.Drawing.Size(946, 20);
            this.lbl_mts_mode_value.TabIndex = 0;
            this.lbl_mts_mode_value.Text = "Auto";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(145, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "[ OHT01 ] System -> MTS";
            // 
            // frmMTL_MTSOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 724);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmMTL_MTSOperation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTL/MTS Operation";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Components.uc_LockStatus uc_LockStatus1;
        private Components.uc_LockStatus uc_LockStatus2;
        private Components.uc_MTLMTSInfo uc_MTLMTSInfo1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_mtl_mode_name;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_mts_mode_name;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_mtl_command_name;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_mtl_mode_value;
        private bc.winform.UI.Components.MyUserControl.LabelExt lbl_mts_mode_value;
        private System.Windows.Forms.Label label1;
    }
}