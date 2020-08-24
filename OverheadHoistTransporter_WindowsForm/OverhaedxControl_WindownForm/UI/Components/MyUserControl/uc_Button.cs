using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.bc.winform.UI.Components.MyUserControl
{
    public partial class uc_Button : UserControl
    {
        bool isfocus = false;

        public event EventHandler Button_Click;

        public uc_Button()
        {
            InitializeComponent();


            btn_button.GotFocus += btn_GotFocus;
            btn_button.LostFocus += btn_LostFocus;
        }

        private void btn_LostFocus(object sender, EventArgs e)
        {
            //改變該Button的樣式
            Un_FocusButton();
        }

        private void btn_GotFocus(object sender, EventArgs e)
        {
            //改變該Button的樣式
            FocusButton();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Button_Click?.Invoke(sender, e);
        }

        public void FocusButton()
        {
            isfocus = true;
            SetButtonColorWhenButtonFocus();
        }

        public void Un_FocusButton()
        {
            isfocus = false;
            SetButtonColorWhenButtonUnfocus();
        }

        public void SetButtonText(string text)
        {
            //btn_Close.ButtonText = text;
        }

        private void btn_Close_MouseEnter(object sender, EventArgs e)
        {
            //btn_Close.BaseColor = BCAppConstants.RGBColor.MouseEnterButtonColorChange;
            //btn_Close.BaseColor = BCAppConstants.RGBColor.MouseEnterButtonColorChange;
            ////btn_Close.ButtonColor = BCAppConstants.RGBColor.MouseEnterButtonColorChange;
            //btn_Close.GlowColor = BCAppConstants.RGBColor.MouseEnterButtonColorChange;
            ////btn_Close.HighlightColor = BCAppConstants.RGBColor.MouseEnterButtonColorChange;
            //btn_Close.ForeColor = Color.White;
        }

        private void btn_Close_MouseLeave(object sender, EventArgs e)
        {
            if (isfocus)
            {
                SetButtonColorWhenButtonFocus();
            }
            else
            {
                //btn_Close.BaseColor = BCAppConstants.RGBColor.MouseLeaveButtonColorChange;
                ////btn_Close.ButtonColor = BCAppConstants.RGBColor.MouseLeaveButtonColorChange;
                //btn_Close.GlowColor = BCAppConstants.RGBColor.MouseLeaveButtonColorChange;
                ////btn_Close.HighlightColor = BCAppConstants.RGBColor.MouseLeaveButtonColorChange;
                //btn_Close.ForeColor = Color.White;
            }
        }

        private void SetButtonColorWhenButtonFocus()
        {
            //btn_Close.BaseColor = BCAppConstants.RGBColor.FocusButtonColorChange;
            ////btn_Close.ButtonColor = BCAppConstants.RGBColor.FocusButtonColorChange;
            //btn_Close.GlowColor = BCAppConstants.RGBColor.FocusButtonColorChange;
            ////btn_Close.HighlightColor = BCAppConstants.RGBColor.FocusButtonColorChange;
            //btn_Close.ForeColor = Color.White;
        }

        private void SetButtonColorWhenButtonUnfocus()
        {
            //btn_Close.BaseColor = BCAppConstants.RGBColor.UnfocusButtonColorChange;
            ////btn_Close.ButtonColor = BCAppConstants.RGBColor.UnfocusButtonColorChange;
            //btn_Close.GlowColor = BCAppConstants.RGBColor.UnfocusButtonColorChange;
            ////btn_Close.HighlightColor = BCAppConstants.RGBColor.UnfocusButtonColorChange;
            //btn_Close.ForeColor = Color.White;
        }

        public string pText
        {
            set { btn_button.Text = value; }
            get { return btn_button.Text; }
        }
        public ContentAlignment pTextAlign
        {
            set { btn_button.TextAlign = value; }
            get { return this.btn_button.TextAlign; }
        }
        public Font pFont
        {
            set { btn_button.Font = value; }
            get { return this.btn_button.Font; }
        }
        public Padding pPadding
        {
            set { btn_button.Padding = value; }
            get { return this.btn_button.Padding; }
        }



    }
}
