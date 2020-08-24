//*********************************************************************************
//      TabControlEx.cs
//*********************************************************************************
// File Name: TabControlEx.cs
// Description: TabControl自定義組件
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                 Author             Request No.    Tag                  Description
// ------------------   ------------------   ------------------   ------------------   ------------------
// 2017/12/05     Boan Chen      N/A                   N/A                  Initial Release
// 2018/05/29     Boan Chen      N/A                   A0.01              新增Port狀態中的Transfer Mode顯示與事件監聽。
//**********************************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components
{
    class TabControlEx2 : System.Windows.Forms.TabControl
    {
        public Color ControlBackColor { get; set; }//背景颜色
        public Color SelectedTabColor { get; set; }//选中时选项卡颜色
        public Color UnselectedTabColor { get; set; }//無选中时选项卡颜色
        public Color TabTextColor { get; set; }//文字颜色
        public Color SelectedTabTextColor { get; set; }//选中时文字颜色
        Brush colorBrush;
        Brush selectBrush;
        Brush unselectBrush;
        Rectangle rectText;
        StringFormat sf = new StringFormat();
        Font newFont = new Font("Microsoft Sans Serif", 13);
        Brush textBrush_white = new SolidBrush(Color.White);
        Brush textBrush_black = new SolidBrush(Color.White);

        public TabControlEx2()
        {
            base.SetStyle(ControlStyles.UserPaint |
             ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.ResizeRedraw |
             ControlStyles.SupportsTransparentBackColor,
             true);
            base.UpdateStyles();

            ControlBackColor = Color.FromArgb(0, 51, 102);
            SelectedTabColor = Color.FromArgb(0, 91, 168);
            UnselectedTabColor = Color.FromArgb(0, 51, 102);
            TabTextColor = Color.White;
            SelectedTabTextColor = Color.White;

            colorBrush = new SolidBrush(ControlBackColor);
            selectBrush = new SolidBrush(SelectedTabColor);
            unselectBrush = new SolidBrush(UnselectedTabColor);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            //填充所有背景
            Rectangle boundsControl = this.ClientRectangle;

            e.Graphics.FillRectangle(colorBrush, boundsControl);

            for (int i = 0; i < this.TabCount; i++)
            {
                Rectangle bounds = this.GetTabRect(i);

                //处理选中时的颜色
                if (this.SelectedIndex == i)
                {
                    e.Graphics.FillRectangle(selectBrush, bounds);
                }

                //处理未选中时的颜色
                if (this.SelectedIndex != i)
                {
                    e.Graphics.FillRectangle(unselectBrush, bounds);
                }

                //绘制图标
                int space = bounds.Height * 1 / 6;
                //int imageWidth = 0;
                if (this.ImageList != null && this.ImageList.Images.Count >= i + 1)
                {
                    Image ins = this.ImageList.Images[i];
                }

                SizeF textSize = TextRenderer.MeasureText(this.TabPages[i].Text, this.Font);

                // 注意要加上每个标签的左偏移量X  
                //rectText = new Rectangle(bounds.X + space, bounds.Top, bounds.Width, bounds.Height);
                rectText = new Rectangle(bounds.X, bounds.Top, bounds.Width, bounds.Height);

                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                if (this.SelectedIndex == i)
                {
                    e.Graphics.DrawString(this.TabPages[i].Text, newFont, textBrush_white, rectText, sf);
                }
                else
                {
                    e.Graphics.DrawString(this.TabPages[i].Text, newFont, textBrush_black, rectText, sf);
                }
            }
        }


    }
}
