namespace com.mirle.ibg3k0.bc.winform.UI
{
    partial class EngineerForm
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
            this.lbl_fromAdr = new System.Windows.Forms.Label();
            this.cmb_fromAdr = new System.Windows.Forms.ComboBox();
            this.lbl_toAdr = new System.Windows.Forms.Label();
            this.cmb_toAdr = new System.Windows.Forms.ComboBox();
            this.btn_FromSecToAdr = new System.Windows.Forms.Button();
            this.txt_Route = new System.Windows.Forms.TextBox();
            this.btn_FromAdrToAdr = new System.Windows.Forms.Button();
            this.cmb_fromSection = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_fromAdr
            // 
            this.lbl_fromAdr.AutoSize = true;
            this.lbl_fromAdr.Location = new System.Drawing.Point(19, 40);
            this.lbl_fromAdr.Name = "lbl_fromAdr";
            this.lbl_fromAdr.Size = new System.Drawing.Size(54, 12);
            this.lbl_fromAdr.TabIndex = 0;
            this.lbl_fromAdr.Text = "Form Adr:";
            // 
            // cmb_fromAdr
            // 
            this.cmb_fromAdr.FormattingEnabled = true;
            this.cmb_fromAdr.Location = new System.Drawing.Point(16, 55);
            this.cmb_fromAdr.Name = "cmb_fromAdr";
            this.cmb_fromAdr.Size = new System.Drawing.Size(121, 20);
            this.cmb_fromAdr.TabIndex = 1;
            // 
            // lbl_toAdr
            // 
            this.lbl_toAdr.AutoSize = true;
            this.lbl_toAdr.Location = new System.Drawing.Point(179, 22);
            this.lbl_toAdr.Name = "lbl_toAdr";
            this.lbl_toAdr.Size = new System.Drawing.Size(42, 12);
            this.lbl_toAdr.TabIndex = 0;
            this.lbl_toAdr.Text = "To Adr:";
            // 
            // cmb_toAdr
            // 
            this.cmb_toAdr.FormattingEnabled = true;
            this.cmb_toAdr.Location = new System.Drawing.Point(181, 37);
            this.cmb_toAdr.Name = "cmb_toAdr";
            this.cmb_toAdr.Size = new System.Drawing.Size(121, 20);
            this.cmb_toAdr.TabIndex = 1;
            // 
            // btn_FromSecToAdr
            // 
            this.btn_FromSecToAdr.Location = new System.Drawing.Point(16, 190);
            this.btn_FromSecToAdr.Name = "btn_FromSecToAdr";
            this.btn_FromSecToAdr.Size = new System.Drawing.Size(146, 30);
            this.btn_FromSecToAdr.TabIndex = 2;
            this.btn_FromSecToAdr.Text = "Search Section -> Address";
            this.btn_FromSecToAdr.UseVisualStyleBackColor = true;
            this.btn_FromSecToAdr.Click += new System.EventHandler(this.btn_FromSecToAdr_Click);
            // 
            // txt_Route
            // 
            this.txt_Route.Location = new System.Drawing.Point(16, 80);
            this.txt_Route.Multiline = true;
            this.txt_Route.Name = "txt_Route";
            this.txt_Route.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Route.Size = new System.Drawing.Size(624, 104);
            this.txt_Route.TabIndex = 3;
            // 
            // btn_FromAdrToAdr
            // 
            this.btn_FromAdrToAdr.Location = new System.Drawing.Point(17, 226);
            this.btn_FromAdrToAdr.Name = "btn_FromAdrToAdr";
            this.btn_FromAdrToAdr.Size = new System.Drawing.Size(145, 30);
            this.btn_FromAdrToAdr.TabIndex = 2;
            this.btn_FromAdrToAdr.Text = "Search Address ->Address";
            this.btn_FromAdrToAdr.UseVisualStyleBackColor = true;
            this.btn_FromAdrToAdr.Click += new System.EventHandler(this.btn_FromAdrToAdr_Click);
            // 
            // cmb_fromSection
            // 
            this.cmb_fromSection.FormattingEnabled = true;
            this.cmb_fromSection.Location = new System.Drawing.Point(16, 15);
            this.cmb_fromSection.Name = "cmb_fromSection";
            this.cmb_fromSection.Size = new System.Drawing.Size(121, 20);
            this.cmb_fromSection.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "From Section:";
            // 
            // EngineerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 261);
            this.Controls.Add(this.cmb_fromSection);
            this.Controls.Add(this.txt_Route);
            this.Controls.Add(this.btn_FromAdrToAdr);
            this.Controls.Add(this.btn_FromSecToAdr);
            this.Controls.Add(this.cmb_toAdr);
            this.Controls.Add(this.cmb_fromAdr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_toAdr);
            this.Controls.Add(this.lbl_fromAdr);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EngineerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmRouteSearchTool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EngineerForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_fromAdr;
        private System.Windows.Forms.ComboBox cmb_fromAdr;
        private System.Windows.Forms.Label lbl_toAdr;
        private System.Windows.Forms.ComboBox cmb_toAdr;
        private System.Windows.Forms.Button btn_FromSecToAdr;
        private System.Windows.Forms.TextBox txt_Route;
        private System.Windows.Forms.Button btn_FromAdrToAdr;
        private System.Windows.Forms.ComboBox cmb_fromSection;
        private System.Windows.Forms.Label label2;
    }
}