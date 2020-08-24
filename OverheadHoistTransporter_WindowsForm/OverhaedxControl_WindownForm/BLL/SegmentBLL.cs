using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc.Data;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class SegmentBLL
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        WindownApplication app = null;
        public DB OperateDB { private set; get; }
        public WebAPI webAPI { private set; get; }

        public SegmentBLL(WindownApplication _app)
        {
            app = _app;
            OperateDB = new DB(app.SegmentDao);
            webAPI = new WebAPI(app.GetWebClientManager());
        }

        public class DB
        {
            sc.Data.DAO.SegmentDao segmentDao = null;
            public DB(sc.Data.DAO.SegmentDao dao)
            {
                segmentDao = dao;
            }
            public List<sc.ASEGMENT> loadSegments()
            {
                List<sc.ASEGMENT> segments = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        segments = segmentDao.loadAllSegments(con);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return segments;
            }
        }

        public class WebAPI
        {
            WebClientManager webClientManager = null;
            public WebAPI(WebClientManager _webClientManager)
            {
                webClientManager = _webClientManager;
            }

            public (bool isSuccess, string result) SendSegmentStatusUpdate(string seg_id, sc.ASEGMENT.DisableType type, sc.E_SEG_STATUS satus)
            {
                string result = string.Empty;
                string[] action_targets = new string[]
                {
                "Segment",
                "StatusUpdate",
                };
                StringBuilder sb = new StringBuilder();
                sb.Append($"{nameof(seg_id)}={seg_id}").Append("&");
                sb.Append($"{nameof(type)}={type.ToString()}").Append("&");
                sb.Append($"{nameof(satus)}={satus.ToString()}");
                byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                result = webClientManager.PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
                return (result == "", result);
            }

        }


    }
}
