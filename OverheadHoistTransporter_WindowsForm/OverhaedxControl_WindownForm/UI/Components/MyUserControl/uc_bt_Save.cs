//*********************************************************************************
//      uc_bt_Save.cs
//*********************************************************************************
// File Name: uc_bt_Save.cs
// Description: the button of Save
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                 Author              Request No.    Tag                  Description
// ------------------   ------------------   ------------------   ------------------   ------------------
// 2018/08/13       Boan                N/A                  N/A                  Initialize.
//**********************************************************************************
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
    public partial class uc_bt_Save : UserControl
    {
        public event EventHandler MyClick;
        public uc_bt_Save()
        {
            InitializeComponent();
        }

        private void uc_bt_Save_Click(object sender, EventArgs e)
        {
        }

        private void btn_Button_Click(object sender, EventArgs e)
        {
            MyClick?.Invoke(sender, e);
        }
    }
}
