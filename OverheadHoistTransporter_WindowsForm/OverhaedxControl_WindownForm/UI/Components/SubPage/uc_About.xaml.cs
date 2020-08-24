//*********************************************************************************
//      uc_About.cs
//*********************************************************************************
// File Name: uc_About.cs
// Description: About
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                       Author                    Request No.         Tag                         Description
// -----------------   -----------------   -----------------   -----------------   -----------------------------
// 2019/08/22           Xenia                      N/A                        N/A                         Initial Release
// 2019/11/07           Boan                       N/A                        A0.01                      新增Try Catch。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using NLog;
using STAN.Client;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_About.xaml 的互動邏輯
    /// </summary>
    /// 

    public partial class uc_About : UserControl
    {
        #region 公用參數設定
        public WindownApplication app { get; private set; } = null;
        private string sLineID = string.Empty;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public uc_About()
        {
            try { InitializeComponent(); }
            catch (Exception ex) { logger.Error(ex, "Exception"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Task.Run(() => app = WindownApplication.getInstance());
                //System Version
                lbl_SofwVsion_Val.Text = "Version " + WindownApplication.getMainFormVersion("");

                //System Build Date
                IFormatProvider culture = new CultureInfo("en-US", true);
                string dtBuildDate = WindownApplication.GetBuildDateTime().ToString("yyyy.MM.dd hh:mm tt", culture);
                lbl_SofwBuildDate_Val.Text = " Build " + dtBuildDate;

                //System Line ID
                setLineStatus();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        object lineStatLock = new object();
        private void setLineStatus()
        {
            try
            {
                string subject = BCAppConstants.NATSTopics.NATS_SUBJECT_LINE_INFO;
                //指定要執行的動作
                EventHandler<StanMsgHandlerArgs> msgHandler = (senders, args) =>
                {
                    lock (lineStatLock)
                    {
                        byte[] arrayByte = args.Message.Data;
                        if (arrayByte == null)
                            return;

                        //反序列化  saveLineInfo
                        Google.Protobuf.MessageParser<LINE_INFO> parser = new Google.Protobuf.MessageParser<LINE_INFO>(() => new LINE_INFO());
                        LINE_INFO lineInfo = parser.ParseFrom(arrayByte);

                        if (SCUtility.isEmpty(sLineID))
                        {
                            sLineID = lineInfo.LineID;
                            //更新Line ID
                            Adapter.Invoke(new SendOrPostCallback((o1) =>
                                {
                                    lbl_LineID.Text = lineInfo.LineID;
                                }), null);
                        }
                    }
                };
                //訂閱
                app.GetNatsManager().Subscriber(subject, msgHandler, false, true, 0, null);       //當subject有變化，則進行msgHandler的動作
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
    }
}
