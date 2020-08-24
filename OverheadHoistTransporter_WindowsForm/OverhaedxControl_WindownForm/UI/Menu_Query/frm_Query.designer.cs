namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class frm_Query
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Query));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lab_CommunicationLog = new System.Windows.Forms.Label();
            this.lab_McsCommandLog = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_FormTitle = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_Descripstion = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.uc_SP_MCSCommandLog = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_MCSCommandLog1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_MCSCommandLog();
            this.uc_SP_CommunicationLog = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_CommunicationLog1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_CommunicationLog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1448701926_Exit.ico");
            this.imageList1.Images.SetKeyName(1, "export.ico");
            this.imageList1.Images.SetKeyName(2, "export.png");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lab_CommunicationLog, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lab_McsCommandLog, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lab_Descripstion, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lab_CommunicationLog
            // 
            this.lab_CommunicationLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_CommunicationLog.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_CommunicationLog, "lab_CommunicationLog");
            this.lab_CommunicationLog.ForeColor = System.Drawing.Color.White;
            this.lab_CommunicationLog.Name = "lab_CommunicationLog";
            this.lab_CommunicationLog.Click += new System.EventHandler(this.lab_CommunicationLog_Click);
            this.lab_CommunicationLog.MouseEnter += new System.EventHandler(this.lab_CommunicationLog_MouseEnter);
            this.lab_CommunicationLog.MouseLeave += new System.EventHandler(this.lab_CommunicationLog_MouseLeave);
            // 
            // lab_McsCommandLog
            // 
            this.lab_McsCommandLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_McsCommandLog.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_McsCommandLog, "lab_McsCommandLog");
            this.lab_McsCommandLog.ForeColor = System.Drawing.Color.White;
            this.lab_McsCommandLog.Name = "lab_McsCommandLog";
            this.tableLayoutPanel1.SetRowSpan(this.lab_McsCommandLog, 2);
            this.lab_McsCommandLog.Click += new System.EventHandler(this.lab_McsCommandLog_Click);
            this.lab_McsCommandLog.MouseEnter += new System.EventHandler(this.lab_McsCommandLog_MouseEnter);
            this.lab_McsCommandLog.MouseLeave += new System.EventHandler(this.lab_McsCommandLog_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 6);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this.lab_FormTitle);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // lab_FormTitle
            // 
            this.lab_FormTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            resources.ApplyResources(this.lab_FormTitle, "lab_FormTitle");
            this.lab_FormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.lab_FormTitle.Name = "lab_FormTitle";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            this.tableLayoutPanel1.SetRowSpan(this.panel4, 7);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.panel6.Controls.Add(this.label2);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // lab_Descripstion
            // 
            resources.ApplyResources(this.lab_Descripstion, "lab_Descripstion");
            this.lab_Descripstion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(35)))), ((int)(((byte)(56)))));
            this.lab_Descripstion.Name = "lab_Descripstion";
            // 
            // panel7
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel7, 3);
            this.panel7.Controls.Add(this.uc_SP_MCSCommandLog);
            this.panel7.Controls.Add(this.uc_SP_CommunicationLog);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            this.tableLayoutPanel1.SetRowSpan(this.panel7, 3);
            // 
            // uc_SP_MCSCommandLog
            // 
            resources.ApplyResources(this.uc_SP_MCSCommandLog, "uc_SP_MCSCommandLog");
            this.uc_SP_MCSCommandLog.Name = "uc_SP_MCSCommandLog";
            this.uc_SP_MCSCommandLog.Child = this.uc_SP_MCSCommandLog1;
            // 
            // uc_SP_CommunicationLog
            // 
            resources.ApplyResources(this.uc_SP_CommunicationLog, "uc_SP_CommunicationLog");
            this.uc_SP_CommunicationLog.Name = "uc_SP_CommunicationLog";
            this.uc_SP_CommunicationLog.Child = this.uc_SP_CommunicationLog1;
            // 
            // frm_Query
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frm_Query";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Query_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private readonly frm_Query _frm_Query;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lab_CommunicationLog;
        private System.Windows.Forms.Label lab_McsCommandLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lab_FormTitle;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lab_Descripstion;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Integration.ElementHost uc_SP_MCSCommandLog;
        private ohxc.winform.UI.Components.SubPage.uc_SP_MCSCommandLog uc_SP_MCSCommandLog1;
        private System.Windows.Forms.Integration.ElementHost uc_SP_CommunicationLog;
        private ohxc.winform.UI.Components.SubPage.uc_SP_CommunicationLog uc_SP_CommunicationLog1;
    }
}