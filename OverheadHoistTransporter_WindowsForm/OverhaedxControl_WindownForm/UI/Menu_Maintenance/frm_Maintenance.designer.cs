using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform;

namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class frm_Maintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Maintenance));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pl_MTLMTS = new System.Windows.Forms.Panel();
            this.pl_Others = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uc_SP_AlarmMaintenance = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_AlarmMaintenance1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_AlarmMaintenance();
            this.uc_SP_VehicleManagement = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_VehicleManagement1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_VehicleManagement();
            this.uc_SP_PortMaint = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_PortMaint1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint();
            this.uc_SP_AdvancedSettings = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_AdvancedSettings1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_AdvancedSettings();
            this.btn_Close = new System.Windows.Forms.Integration.ElementHost();
            this.btn_Close1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.Tool.uc_btn_Close();
            this.uc_SP_MTLMTSMaintenance = new System.Windows.Forms.Integration.ElementHost();
            this.uc_SP_MTLMTSMaintenance1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_SP_MTLMTSMaintenance();
            this.lab_PortMaint = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_FormTitle = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_VhManage = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lab_MTLMTSMaint = new System.Windows.Forms.Label();
            this.lab_AdvSet = new System.Windows.Forms.Label();
            this.lab_AlarmMaint = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pl_MTLMTS.SuspendLayout();
            this.pl_Others.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.pl_MTLMTS, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lab_PortMaint, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lab_VhManage, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lab_MTLMTSMaint, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lab_AdvSet, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lab_AlarmMaint, 1, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // pl_MTLMTS
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pl_MTLMTS, 3);
            this.pl_MTLMTS.Controls.Add(this.pl_Others);
            this.pl_MTLMTS.Controls.Add(this.uc_SP_MTLMTSMaintenance);
            resources.ApplyResources(this.pl_MTLMTS, "pl_MTLMTS");
            this.pl_MTLMTS.Name = "pl_MTLMTS";
            this.tableLayoutPanel1.SetRowSpan(this.pl_MTLMTS, 7);
            // 
            // pl_Others
            // 
            resources.ApplyResources(this.pl_Others, "pl_Others");
            this.pl_Others.Controls.Add(this.panel2, 0, 0);
            this.pl_Others.Controls.Add(this.btn_Close, 2, 0);
            this.pl_Others.Name = "pl_Others";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uc_SP_AlarmMaintenance);
            this.panel2.Controls.Add(this.uc_SP_VehicleManagement);
            this.panel2.Controls.Add(this.uc_SP_PortMaint);
            this.panel2.Controls.Add(this.uc_SP_AdvancedSettings);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // uc_SP_AlarmMaintenance
            // 
            resources.ApplyResources(this.uc_SP_AlarmMaintenance, "uc_SP_AlarmMaintenance");
            this.uc_SP_AlarmMaintenance.Name = "uc_SP_AlarmMaintenance";
            this.uc_SP_AlarmMaintenance.Child = this.uc_SP_AlarmMaintenance1;
            // 
            // uc_SP_VehicleManagement
            // 
            resources.ApplyResources(this.uc_SP_VehicleManagement, "uc_SP_VehicleManagement");
            this.uc_SP_VehicleManagement.Name = "uc_SP_VehicleManagement";
            this.uc_SP_VehicleManagement.Child = this.uc_SP_VehicleManagement1;
            // 
            // uc_SP_PortMaint
            // 
            resources.ApplyResources(this.uc_SP_PortMaint, "uc_SP_PortMaint");
            this.uc_SP_PortMaint.Name = "uc_SP_PortMaint";
            this.uc_SP_PortMaint.Child = this.uc_SP_PortMaint1;
            // 
            // uc_SP_AdvancedSettings
            // 
            resources.ApplyResources(this.uc_SP_AdvancedSettings, "uc_SP_AdvancedSettings");
            this.uc_SP_AdvancedSettings.Name = "uc_SP_AdvancedSettings";
            this.uc_SP_AdvancedSettings.Child = this.uc_SP_AdvancedSettings1;
            // 
            // btn_Close
            // 
            resources.ApplyResources(this.btn_Close, "btn_Close");
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Child = this.btn_Close1;
            // 
            // uc_SP_MTLMTSMaintenance
            // 
            resources.ApplyResources(this.uc_SP_MTLMTSMaintenance, "uc_SP_MTLMTSMaintenance");
            this.uc_SP_MTLMTSMaintenance.Name = "uc_SP_MTLMTSMaintenance";
            this.uc_SP_MTLMTSMaintenance.Child = this.uc_SP_MTLMTSMaintenance1;
            // 
            // lab_PortMaint
            // 
            this.lab_PortMaint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_PortMaint.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_PortMaint, "lab_PortMaint");
            this.lab_PortMaint.ForeColor = System.Drawing.Color.White;
            this.lab_PortMaint.Name = "lab_PortMaint";
            this.lab_PortMaint.Click += new System.EventHandler(this.lab_Click);
            this.lab_PortMaint.MouseEnter += new System.EventHandler(this.lab_PortMaint_MouseEnter);
            this.lab_PortMaint.MouseLeave += new System.EventHandler(this.lab_PortMaint_MouseLeave);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel8, 2);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel7, 5);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
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
            this.tableLayoutPanel1.SetRowSpan(this.panel4, 10);
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
            // lab_VhManage
            // 
            this.lab_VhManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_VhManage.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_VhManage, "lab_VhManage");
            this.lab_VhManage.ForeColor = System.Drawing.Color.White;
            this.lab_VhManage.Name = "lab_VhManage";
            this.tableLayoutPanel1.SetRowSpan(this.lab_VhManage, 2);
            this.lab_VhManage.Click += new System.EventHandler(this.lab_Click);
            this.lab_VhManage.MouseEnter += new System.EventHandler(this.lab_VhManage_MouseEnter);
            this.lab_VhManage.MouseLeave += new System.EventHandler(this.lab_VhManage_MouseLeave);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            // 
            // lab_MTLMTSMaint
            // 
            this.lab_MTLMTSMaint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_MTLMTSMaint.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_MTLMTSMaint, "lab_MTLMTSMaint");
            this.lab_MTLMTSMaint.ForeColor = System.Drawing.Color.White;
            this.lab_MTLMTSMaint.Name = "lab_MTLMTSMaint";
            this.lab_MTLMTSMaint.Click += new System.EventHandler(this.lab_Click);
            this.lab_MTLMTSMaint.MouseEnter += new System.EventHandler(this.lab_MTLMTSMaint_MouseEnter);
            this.lab_MTLMTSMaint.MouseLeave += new System.EventHandler(this.lab_MTLMTSMaint_MouseLeave);
            // 
            // lab_AdvSet
            // 
            this.lab_AdvSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_AdvSet.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_AdvSet, "lab_AdvSet");
            this.lab_AdvSet.ForeColor = System.Drawing.Color.White;
            this.lab_AdvSet.Name = "lab_AdvSet";
            this.lab_AdvSet.Click += new System.EventHandler(this.lab_Click);
            this.lab_AdvSet.MouseEnter += new System.EventHandler(this.lab_AdvSet_MouseEnter);
            this.lab_AdvSet.MouseLeave += new System.EventHandler(this.lab_AdvSet_MouseLeave);
            // 
            // lab_AlarmMaint
            // 
            this.lab_AlarmMaint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lab_AlarmMaint.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lab_AlarmMaint, "lab_AlarmMaint");
            this.lab_AlarmMaint.ForeColor = System.Drawing.Color.White;
            this.lab_AlarmMaint.Name = "lab_AlarmMaint";
            this.lab_AlarmMaint.Click += new System.EventHandler(this.lab_Click);
            this.lab_AlarmMaint.MouseEnter += new System.EventHandler(this.lab_AlarmMaint_MouseEnter);
            this.lab_AlarmMaint.MouseLeave += new System.EventHandler(this.lab_AlarmMaint_MouseLeave);
            // 
            // frm_Maintenance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frm_Maintenance";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_Maintenance_FormClosed);
            this.Load += new System.EventHandler(this.frm_Maintenance_Load);
            this.Shown += new System.EventHandler(this.frm_Maintenance_Shown);
            this.VisibleChanged += new System.EventHandler(this.frm_Maintenance_VisibleChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pl_MTLMTS.ResumeLayout(false);
            this.pl_Others.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private readonly frm_Maintenance _frm_Maintenance;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lab_FormTitle;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_MTS_MTL_MAINTENANCE)]
        private System.Windows.Forms.Label lab_MTLMTSMaint;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS)]
        private System.Windows.Forms.Label lab_AdvSet;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_VEHICLE_MANAGEMENT)]
        private System.Windows.Forms.Label lab_VhManage;
        [AuthorityCheck(FUNCode = BCAppConstants.Maintenance_Function.FUNC_PORT_MAINTENANCE)]
        private System.Windows.Forms.Label lab_PortMaint;
        private ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint uc_SP_PortMaint4;
        private System.Windows.Forms.Panel pl_MTLMTS;
        private System.Windows.Forms.TableLayoutPanel pl_Others;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Integration.ElementHost uc_SP_VehicleManagement;
        private ohxc.winform.UI.Components.SubPage.uc_SP_VehicleManagement uc_SP_VehicleManagement1;
        private System.Windows.Forms.Integration.ElementHost uc_SP_PortMaint;
        private ohxc.winform.UI.Components.SubPage.uc_SP_PortMaint uc_SP_PortMaint1;
        private System.Windows.Forms.Integration.ElementHost uc_SP_AdvancedSettings;
        private ohxc.winform.UI.Components.SubPage.uc_SP_AdvancedSettings uc_SP_AdvancedSettings1;
        private System.Windows.Forms.Integration.ElementHost uc_SP_MTLMTSMaintenance;
        private ohxc.winform.UI.Components.SubPage.uc_SP_MTLMTSMaintenance uc_SP_MTLMTSMaintenance1;
        private System.Windows.Forms.Integration.ElementHost btn_Close;
        private ohxc.winform.UI.Components.Tool.uc_btn_Close btn_Close1;
        private System.Windows.Forms.Label lab_AlarmMaint;
        private System.Windows.Forms.Integration.ElementHost uc_SP_AlarmMaintenance;
        private ohxc.winform.UI.Components.SubPage.uc_SP_AlarmMaintenance uc_SP_AlarmMaintenance1;
        //private ohxc.winform.UI.Components.SubPage.uc_SP_MTLMTSMaintenance uc_SP_MTLMTSMaintenance2;
    }
}