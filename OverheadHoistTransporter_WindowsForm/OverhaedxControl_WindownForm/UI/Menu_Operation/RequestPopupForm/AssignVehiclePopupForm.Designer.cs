using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_System
{
    partial class AssignVehiclePopupForm
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.uc_TransferCommand1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_TransferCommand();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Margin = new System.Windows.Forms.Padding(0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(404, 361);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.uc_TransferCommand1;
            // 
            // AssignVehiclePopupForm
            // 
            this.ClientSize = new System.Drawing.Size(404, 361);
            this.Controls.Add(this.elementHost1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssignVehiclePopupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assign Vehicle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AssignVehiclePopupForm_FormClosed);
            this.Load += new System.EventHandler(this.AssignVehiclePopupForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private Components.SubPage.uc_TransferCommand uc_TransferCommand1;
    }
}