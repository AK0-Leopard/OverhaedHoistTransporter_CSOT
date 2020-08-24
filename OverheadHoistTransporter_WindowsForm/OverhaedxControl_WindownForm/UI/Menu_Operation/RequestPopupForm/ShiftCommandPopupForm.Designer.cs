using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.ObjectRelay;
using com.mirle.ibg3k0.sc;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_System
{
    partial class ShiftCommandPopupForm
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
            this.uc_TransferCommand = new System.Windows.Forms.Integration.ElementHost();
            this.uc_TransferCommand1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_TransferCommand();
            this.SuspendLayout();
            // 
            // uc_TransferCommand
            // 
            this.uc_TransferCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_TransferCommand.Location = new System.Drawing.Point(0, 0);
            this.uc_TransferCommand.Margin = new System.Windows.Forms.Padding(0);
            this.uc_TransferCommand.Name = "uc_TransferCommand";
            this.uc_TransferCommand.Size = new System.Drawing.Size(404, 361);
            this.uc_TransferCommand.TabIndex = 0;
            this.uc_TransferCommand.Text = "elementHost1";
            this.uc_TransferCommand.Child = this.uc_TransferCommand1;
            // 
            // ShiftCommandPopupForm
            // 
            this.ClientSize = new System.Drawing.Size(404, 361);
            this.Controls.Add(this.uc_TransferCommand);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShiftCommandPopupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shift Command";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShiftCommandPopupForm_FormClosed);
            this.Load += new System.EventHandler(this.ShiftCommandPopupForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost uc_TransferCommand;
        private Components.SubPage.uc_TransferCommand uc_TransferCommand1;
    }
}