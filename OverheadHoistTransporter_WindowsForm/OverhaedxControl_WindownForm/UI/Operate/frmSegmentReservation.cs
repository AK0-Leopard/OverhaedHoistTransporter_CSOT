//*********************************************************************************
//      frmSegmentReservation.cs
//*********************************************************************************
// File Name: frmSegmentReservation.cs
// Description: Segment Reservation
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/05/16           Kevin                      N/A                        N/A                         Initial Release
// 2019/11/05           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Operate
{
    public partial class frmSegmentReservation : Form
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static frmSegmentReservation form = null;
        App.WindownApplication app;
        static object lock_obj = new object();
        #endregion 公用參數設定

        public static frmSegmentReservation getInstance(App.WindownApplication _app)
        {
            lock (lock_obj)
            {
                if (form == null || form.IsDisposed)
                {
                    form = new frmSegmentReservation(_app);
                }
                else
                {
                    form.Focus();
                }
            }
            return form;
        }

        private frmSegmentReservation(App.WindownApplication _app)
        {
            try
            {
                InitializeComponent();
                app = _app;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void frmSegmentReservation_Load(object sender, EventArgs e)
        {
            try
            {
                List<sc.ASEGMENT> segment_List = null;
                segment_List = app.ObjCacheManager.GetSegments();
                dgv_segment.AutoGenerateColumns = false;
                dgv_segment.DataSource = segment_List;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

    }
}
