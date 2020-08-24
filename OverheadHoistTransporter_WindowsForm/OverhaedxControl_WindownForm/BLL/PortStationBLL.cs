using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc.Data;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class PortStationBLL
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        WindownApplication app = null;
        public DB OperateDB { private set; get; }

        public PortStationBLL(WindownApplication _app)
        {
            app = _app;
            OperateDB = new DB(app.PortStationDao);
        }



        public class DB
        {
            sc.Data.DAO.PortStationDao portStationDao = null;
            public DB(sc.Data.DAO.PortStationDao dao)
            {
                portStationDao = dao;
            }
            public List<sc.APORTSTATION> loadPortStation()
            {
                List<sc.APORTSTATION> port_station = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        port_station = portStationDao.loadAll(con);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return port_station;
            }
        }

        public bool SendPortPriorityUpdate(string port_id, int priority)
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "PortStation",
                "PriorityUpdate",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(port_id)}={port_id}").Append("&");
            sb.Append($"{nameof(priority)}={priority.ToString()}");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }

        public bool SendPortStatusChange(string port_id, com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage.PortStationServiceStatus status)
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "PortStation",
                "StatusChange",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(port_id)}={port_id}").Append("&");
            sb.Append($"{nameof(status)}={status.ToString()}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = app.GetWebClientManager().PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }
    }
}
