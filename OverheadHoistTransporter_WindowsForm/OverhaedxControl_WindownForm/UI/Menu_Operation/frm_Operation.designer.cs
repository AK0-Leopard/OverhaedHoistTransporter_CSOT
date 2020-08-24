using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class frm_Operation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Operation));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_FormTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab_SystemModeControl = new System.Windows.Forms.Label();
            this.lab_TransferManagement = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uc_SP_PathControlList = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_PathControlList1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_PathControlList();
            this.uc_SP_TransferManagement = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_TransferManagement1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_TransferManagement();
            this.uc_SP_SystemModeControl = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_SystemModeControl1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_SystemModeControl();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lab_RoadControl = new System.Windows.Forms.Label();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            this.tableLayoutPanel1.SetRowSpan(this.panel4, 8);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 5);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lab_SystemModeControl
            // 
            this.lab_SystemModeControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_SystemModeControl.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_SystemModeControl, "lab_SystemModeControl");
            this.lab_SystemModeControl.ForeColor = System.Drawing.Color.White;
            this.lab_SystemModeControl.Name = "lab_SystemModeControl";
            this.tableLayoutPanel1.SetRowSpan(this.lab_SystemModeControl, 2);
            this.lab_SystemModeControl.Click += new System.EventHandler(this.lab_SystemModeControl_Click);
            this.lab_SystemModeControl.MouseEnter += new System.EventHandler(this.lab_SystemModeControl_MouseEnter);
            this.lab_SystemModeControl.MouseLeave += new System.EventHandler(this.lab_SystemModeControl_MouseLeave);
            // 
            // lab_TransferManagement
            // 
            this.lab_TransferManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_TransferManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_TransferManagement, "lab_TransferManagement");
            this.lab_TransferManagement.ForeColor = System.Drawing.Color.White;
            this.lab_TransferManagement.Name = "lab_TransferManagement";
            this.lab_TransferManagement.Click += new System.EventHandler(this.lab_TransferManagement_Click);
            this.lab_TransferManagement.MouseEnter += new System.EventHandler(this.lab_TransferManagement_MouseEnter);
            this.lab_TransferManagement.MouseLeave += new System.EventHandler(this.lab_TransferManagement_MouseLeave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lab_TransferManagement, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lab_SystemModeControl, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lab_RoadControl, 1, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 5);
            this.panel2.Controls.Add(this.uc_SP_PathControlList);
            this.panel2.Controls.Add(this.uc_SP_TransferManagement);
            this.panel2.Controls.Add(this.uc_SP_SystemModeControl);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 5);
            // 
            // uc_SP_PathControlList
            // 
            resources.ApplyResources(this.uc_SP_PathControlList, "uc_SP_PathControlList");
            this.uc_SP_PathControlList.Name = "uc_SP_PathControlList";
            this.uc_SP_PathControlList.Child = this.uc_SP_PathControlList1;
            // 
            // uc_SP_TransferManagement
            // 
            resources.ApplyResources(this.uc_SP_TransferManagement, "uc_SP_TransferManagement");
            this.uc_SP_TransferManagement.Name = "uc_SP_TransferManagement";
            this.uc_SP_TransferManagement.Child = this.uc_SP_TransferManagement1;
            // 
            // uc_SP_SystemModeControl
            // 
            resources.ApplyResources(this.uc_SP_SystemModeControl, "uc_SP_SystemModeControl");
            this.uc_SP_SystemModeControl.Name = "uc_SP_SystemModeControl";
            this.uc_SP_SystemModeControl.Child = this.uc_SP_SystemModeControl1;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel7, 2);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // lab_RoadControl
            // 
            this.lab_RoadControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_RoadControl.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_RoadControl, "lab_RoadControl");
            this.lab_RoadControl.ForeColor = System.Drawing.Color.White;
            this.lab_RoadControl.Name = "lab_RoadControl";
            this.lab_RoadControl.Click += new System.EventHandler(this.lab_RoadControl_Click);
            this.lab_RoadControl.MouseEnter += new System.EventHandler(this.lab_RoadControl_MouseEnter);
            this.lab_RoadControl.MouseLeave += new System.EventHandler(this.lab_RoadControl_MouseLeave);
            // 
            // frm_Operation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frm_Operation";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Operation_Load);
            this.VisibleChanged += new System.EventHandler(this.frm_Operation_VisibleChanged);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private readonly frm_Operation _frm_Operation;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        [AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_TRANSFER_MANAGEMENT)]
        private System.Windows.Forms.Label lab_TransferManagement;
        [AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_SYSTEM_CONCROL_MODE)]
        private System.Windows.Forms.Label lab_SystemModeControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lab_FormTitle;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel2;
        private ohxc.winform.UI.Components.SubPage.uc_SP_TransferManagement uc_SP_TransferManagement1;
        private ohxc.winform.UI.Components.SubPage.uc_SP_SystemModeControl uc_SP_SystemModeControl1;
        private System.Windows.Forms.Integration.ElementHost uc_SP_SystemModeControl;
        private System.Windows.Forms.Integration.ElementHost uc_SP_TransferManagement;
        //[AuthorityCheck(FUNCode = BCAppConstants.Operation_Function.FUNC_ROAD_CONTROL)]
        private System.Windows.Forms.Label lab_RoadControl;
        private System.Windows.Forms.Integration.ElementHost uc_SP_PathControlList;
        private ohxc.winform.UI.Components.SubPage.uc_SP_PathControlList uc_SP_PathControlList1;
    }
}