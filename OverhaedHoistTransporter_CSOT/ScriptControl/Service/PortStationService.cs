using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.BLL;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace com.mirle.ibg3k0.sc.Service
{
    public class PortStationService
    {

        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private SCApplication scApp = null;
        private ReportBLL reportBLL = null;
        private LineBLL lineBLL = null;
        private ALINE line = null;
        public PortStationService()
        {

        }
        public void start(SCApplication _app)
        {
            scApp = _app;
            reportBLL = _app.ReportBLL;
        }

        public bool doUpdatePortStationPriority(string portID, int priority)
        {
            bool isSuccess = true;
            string result = string.Empty;
            try
            {
                if (isSuccess)
                {
                    using (TransactionScope tx = SCUtility.getTransactionScope())
                    {
                        using (DBConnection_EF con = DBConnection_EF.GetUContext())
                        {
                            isSuccess = scApp.PortStationBLL.OperateDB.updatePriority(portID, priority);
                            if (isSuccess)
                            {
                                tx.Complete();
                                scApp.PortStationBLL.OperateCatch.updatePriority(portID, priority);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.Error(ex, "Execption:");
            }
            return isSuccess;
        }
        public bool doUpdatePortStatus(string portID, E_PORT_STATUS status)
        {
            bool isSuccess = true;
            string result = string.Empty;
            try
            {
                if (isSuccess)
                {
                    using (TransactionScope tx = SCUtility.getTransactionScope())
                    {
                        using (DBConnection_EF con = DBConnection_EF.GetUContext())
                        {
                            isSuccess = scApp.PortStationBLL.OperateDB.updatePortStatus(portID, status);
                            if (isSuccess)
                            {
                                tx.Complete();
                                scApp.PortStationBLL.OperateCatch.updatePortStatus(portID, status);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.Error(ex, "Execption:");
            }
            return isSuccess;
        }

        public bool doUpdatePortStationServiceStatus(string portID, com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage.PortStationServiceStatus status)
        {
            bool isSuccess = true;
            string result = string.Empty;
            try
            {

                var caceh_obj = scApp.PortStationBLL.OperateCatch.getPortStation(portID);
                bool before_is_service = caceh_obj.PORT_STATUS == E_PORT_STATUS.InService &&
                                         caceh_obj.PORT_SERVICE_STATUS == ProtocolFormat.OHTMessage.PortStationServiceStatus.InService;
                using (TransactionScope tx = SCUtility.getTransactionScope())
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        isSuccess = scApp.PortStationBLL.OperateDB.updateServiceStatus(portID, status);
                        if (isSuccess)
                        {
                            tx.Complete();
                            scApp.PortStationBLL.OperateCatch.updateServiceStatus(portID, status);
                        }
                    }
                }
                caceh_obj = scApp.PortStationBLL.OperateCatch.getPortStation(portID);
                bool after_is_service = caceh_obj.PORT_STATUS == E_PORT_STATUS.InService &&
                                        caceh_obj.PORT_SERVICE_STATUS == ProtocolFormat.OHTMessage.PortStationServiceStatus.InService;
                if (before_is_service != after_is_service)
                {
                    if (after_is_service)
                    {
                        scApp.ReportBLL.newReportPortInServeice(portID, null);
                    }
                    else
                    {
                        scApp.ReportBLL.newReportPortOutOfService(portID, null);
                    }
                }

            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.Error(ex, "Execption:");
            }
            return isSuccess;
        }
    }
}
