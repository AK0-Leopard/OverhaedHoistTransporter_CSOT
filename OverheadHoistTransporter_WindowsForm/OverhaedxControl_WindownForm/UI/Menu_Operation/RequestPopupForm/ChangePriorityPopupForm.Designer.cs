namespace com.mirle.ibg3k0.ohxc.winform.UI.Menu_Operation.RequestPopForm
{
    partial class ChangePriorityPopupForm
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
            this.uc_ChangePriority = new System.Windows.Forms.Integration.ElementHost();
            this.uc_ChangePriority1 = new com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage.uc_ChangePriority();
            this.SuspendLayout();
            // 
            // uc_ChangePriority
            // 
            this.uc_ChangePriority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_ChangePriority.Location = new System.Drawing.Point(0, 0);
            this.uc_ChangePriority.Margin = new System.Windows.Forms.Padding(0);
            this.uc_ChangePriority.Name = "uc_ChangePriority";
            this.uc_ChangePriority.Size = new System.Drawing.Size(689, 507);
            this.uc_ChangePriority.TabIndex = 0;
            this.uc_ChangePriority.Text = "elementHost1";
            this.uc_ChangePriority.Child = this.uc_ChangePriority1;
            // 
            // ChangePriorityPopupForm
            // 
            this.ClientSize = new System.Drawing.Size(689, 507);
            this.Controls.Add(this.uc_ChangePriority);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePriorityPopupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Priority";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChangePriorityPopupForm_FormClosed);
            this.Load += new System.EventHandler(this.ChangePriorityPopupForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost uc_ChangePriority;
        private Components.SubPage.uc_ChangePriority uc_ChangePriority1;
    }
}