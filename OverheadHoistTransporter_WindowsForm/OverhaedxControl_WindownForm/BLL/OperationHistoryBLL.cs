using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data;
using com.mirle.ibg3k0.sc.Data.DAO;
using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class OperationHistoryBLL
    {
        private static Logger logger = LogManager.GetLogger("OperationLogger");


        public OperationHistoryBLL(WindownApplication _app)
        {
        }

        public void addOperationHis(string user_id, string formName, string action, [CallerMemberName] string Method = "")
        {
            try
            {
                string timeStamp = BCFUtility.formatDateTime(DateTime.Now, SCAppConstants.TimestampFormat_19);
                HOPERATION his = new HOPERATION()
                {
                    T_STAMP = timeStamp,
                    USER_ID = user_id,
                    FORM_NAME = $"{formName}-({Method})",
                    ACTION = action
                };
                PrintOperationLog(his);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Execption:");
            }
        }
        public static void PrintOperationLog(HOPERATION opHis)
        {
            try
            {
                if (opHis == null) { return; }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine(string.Format("{0}Time: {1}", new string(' ', 5), opHis.T_STAMP));
                sb.AppendLine(string.Format("{0}User: {1}", new string(' ', 5), opHis.USER_ID));
                sb.AppendLine(string.Format("{0}UI Name: {1}", new string(' ', 5), opHis.FORM_NAME));
                sb.AppendLine(string.Format("{0}Action: ", new string(' ', 5)));
                sb.AppendLine(string.Format("{0}         {1}", new string(' ', 5), opHis.ACTION));
                logger.Info(sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Execption:");
            }
        }
    }
}
