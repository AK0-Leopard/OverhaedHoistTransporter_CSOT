namespace com.mirle.ibg3k0.ohxc.winform.UI.Operate
{
    partial class frmSegmentReservation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelExt1 = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelExt2 = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.LabelExt();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.uc_Button3 = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.uc_Button();
            this.uc_Button2 = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.uc_Button();
            this.uc_Button1 = new com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl.uc_Button();
            this.dgv_segment = new System.Windows.Forms.DataGridView();
            this.cLLRptTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLLEqptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLLUnitNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLLAlamCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cllAffectCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_segment)).BeginInit();
            this.SuspendLayout();
            // 
            // labelExt1
            // 
            this.labelExt1.AutoSize = true;
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.tableLayoutPanel1.SetColumnSpan(this.labelExt1, 2);
            this.labelExt1.DisplayName = "Segment_Reservation";
            this.labelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.labelExt1.ForeColor = System.Drawing.Color.White;
            this.labelExt1.Location = new System.Drawing.Point(3, 0);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(1088, 66);
            this.labelExt1.TabIndex = 101;
            this.labelExt1.Text = "[[Segment_Reservation]]";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelExt2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelExt1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgv_segment, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.2F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1094, 724);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelExt2
            // 
            this.labelExt2.AutoSize = true;
            this.labelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.labelExt2.DisplayName = "Sgment_Detail";
            this.labelExt2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExt2.ForeColor = System.Drawing.Color.Black;
            this.labelExt2.Location = new System.Drawing.Point(65, 92);
            this.labelExt2.Margin = new System.Windows.Forms.Padding(65, 26, 3, 0);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Size = new System.Drawing.Size(807, 26);
            this.labelExt2.TabIndex = 102;
            this.labelExt2.Text = "Sgment Detail";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.uc_Button3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.uc_Button2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.uc_Button1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(878, 344);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(175, 153);
            this.tableLayoutPanel2.TabIndex = 103;
            // 
            // uc_Button3
            // 
            this.uc_Button3.BackColor = System.Drawing.Color.Transparent;
            this.uc_Button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_Button3.Location = new System.Drawing.Point(3, 123);
            this.uc_Button3.Name = "uc_Button3";
            this.uc_Button3.pFont = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.uc_Button3.pPadding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uc_Button3.pText = "Cancel";
            this.uc_Button3.pTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uc_Button3.Size = new System.Drawing.Size(169, 29);
            this.uc_Button3.TabIndex = 2;
            // 
            // uc_Button2
            // 
            this.uc_Button2.BackColor = System.Drawing.Color.Transparent;
            this.uc_Button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_Button2.Location = new System.Drawing.Point(3, 63);
            this.uc_Button2.Name = "uc_Button2";
            this.uc_Button2.pFont = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.uc_Button2.pPadding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uc_Button2.pText = "Disable";
            this.uc_Button2.pTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uc_Button2.Size = new System.Drawing.Size(169, 29);
            this.uc_Button2.TabIndex = 1;
            // 
            // uc_Button1
            // 
            this.uc_Button1.BackColor = System.Drawing.Color.Transparent;
            this.uc_Button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_Button1.Location = new System.Drawing.Point(3, 3);
            this.uc_Button1.Name = "uc_Button1";
            this.uc_Button1.pFont = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.uc_Button1.pPadding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.uc_Button1.pText = "Enable";
            this.uc_Button1.pTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uc_Button1.Size = new System.Drawing.Size(169, 29);
            this.uc_Button1.TabIndex = 0;
            // 
            // dgv_segment
            // 
            this.dgv_segment.AllowUserToAddRows = false;
            this.dgv_segment.AllowUserToDeleteRows = false;
            this.dgv_segment.AllowUserToOrderColumns = true;
            this.dgv_segment.AllowUserToResizeColumns = false;
            this.dgv_segment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(228)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgv_segment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_segment.BackgroundColor = System.Drawing.Color.White;
            this.dgv_segment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_segment.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_segment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_segment.ColumnHeadersHeight = 30;
            this.dgv_segment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cLLRptTime,
            this.cLLEqptID,
            this.cLLUnitNum,
            this.cLLAlamCode,
            this.cllAffectCnt});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(168)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_segment.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_segment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_segment.EnableHeadersVisualStyles = false;
            this.dgv_segment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(34)))));
            this.dgv_segment.Location = new System.Drawing.Point(65, 123);
            this.dgv_segment.Margin = new System.Windows.Forms.Padding(65, 5, 5, 33);
            this.dgv_segment.Name = "dgv_segment";
            this.dgv_segment.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_segment.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_segment.RowHeadersWidth = 25;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.dgv_segment.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_segment.RowTemplate.Height = 24;
            this.dgv_segment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_segment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_segment.Size = new System.Drawing.Size(805, 568);
            this.dgv_segment.TabIndex = 104;
            // 
            // cLLRptTime
            // 
            this.cLLRptTime.DataPropertyName = "SEG_NUM";
            this.cLLRptTime.Frozen = true;
            this.cLLRptTime.HeaderText = "Segment No.";
            this.cLLRptTime.Name = "cLLRptTime";
            this.cLLRptTime.ReadOnly = true;
            this.cLLRptTime.Width = 103;
            // 
            // cLLEqptID
            // 
            this.cLLEqptID.DataPropertyName = "STATUS";
            this.cLLEqptID.Frozen = true;
            this.cLLEqptID.HeaderText = "Status";
            this.cLLEqptID.Name = "cLLEqptID";
            this.cLLEqptID.ReadOnly = true;
            this.cLLEqptID.Width = 103;
            // 
            // cLLUnitNum
            // 
            this.cLLUnitNum.DataPropertyName = "PRE_DISABLE_FLAG";
            this.cLLUnitNum.HeaderText = "Preiously Disable Flag";
            this.cLLUnitNum.Name = "cLLUnitNum";
            this.cLLUnitNum.ReadOnly = true;
            this.cLLUnitNum.Width = 185;
            // 
            // cLLAlamCode
            // 
            this.cLLAlamCode.DataPropertyName = "PRE_DISABLE_TIME";
            this.cLLAlamCode.HeaderText = "Previously Disable Time";
            this.cLLAlamCode.Name = "cLLAlamCode";
            this.cLLAlamCode.ReadOnly = true;
            this.cLLAlamCode.Width = 190;
            // 
            // cllAffectCnt
            // 
            this.cllAffectCnt.DataPropertyName = "DISABLE_TIME";
            this.cllAffectCnt.HeaderText = "Disable Time";
            this.cllAffectCnt.Name = "cllAffectCnt";
            this.cllAffectCnt.ReadOnly = true;
            this.cllAffectCnt.Width = 185;
            // 
            // frmSegmentReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 724);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSegmentReservation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Segment Reseration";
            this.Load += new System.EventHandler(this.frmSegmentReservation_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_segment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private bc.winform.UI.Components.MyUserControl.LabelExt labelExt1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private bc.winform.UI.Components.MyUserControl.LabelExt labelExt2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private bc.winform.UI.Components.MyUserControl.uc_Button uc_Button3;
        private bc.winform.UI.Components.MyUserControl.uc_Button uc_Button2;
        private bc.winform.UI.Components.MyUserControl.uc_Button uc_Button1;
        private System.Windows.Forms.DataGridView dgv_segment;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLLRptTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLLEqptID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLLUnitNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLLAlamCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cllAffectCnt;
    }
}