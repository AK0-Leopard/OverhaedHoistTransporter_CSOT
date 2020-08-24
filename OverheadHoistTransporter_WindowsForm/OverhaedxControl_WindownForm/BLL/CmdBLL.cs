using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data;
using com.mirle.ibg3k0.sc.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mirle.ibg3k0.sc.Data.DAO.EntityFramework;
namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class CmdBLL
    {
        CMD_OHTCDao cmd_ohtcDAO = null;
        CMD_MCSDao cmd_mcsDao = null;
        VCMD_MCSDao vcmd_mcsDao = null;
        WebClientManager webClientManager = null;
        public CmdBLL(WindownApplication _app)
        {
            cmd_ohtcDAO = _app.CMD_OHTCDao;
            cmd_mcsDao = _app.CMD_MCSDao;
            vcmd_mcsDao = _app.VCMD_MCSDao;
            webClientManager = _app.GetWebClientManager();
        }
        public ACMD_OHTC GetCmd_OhtcByID(string cmdID)
        {
            ACMD_OHTC cmd = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd = cmd_ohtcDAO.getByID(con, cmdID);
            }
            return cmd;
        }
        public ACMD_MCS GetCmd_MCSByID(string cmdID)
        {
            ACMD_MCS cmd = null;
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                cmd = cmd_mcsDao.getByID(con, cmdID);
            }
            return cmd;
        }
        public int getCMD_MCSMinProritySum()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSMinPrioritySum(con);
            }
        }
        public int getCMD_MCSMaxProritySum()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSMaxPrioritySum(con);
            }
        }
        public int getCMD_MCSTotalCount()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSTotalCount(con);
            }
        }

        public int getCMD_MCSIsQueueCount()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.getCMD_MCSIsQueueCount(con);
            }
        }

        private List<ACMD_MCS> list()
        {
            List<ACMD_MCS> ACMD_MCSs = null;
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                ACMD_MCSs = cmd_mcsDao.loadACMD_MCSIsQueue(con);
            }
            return ACMD_MCSs;
        }

        public List<ACMD_MCS> loadACMD_MCSIsUnfinished()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return cmd_mcsDao.loadACMD_MCSIsUnfinished(con);
            }
        }

        public List<VACMD_MCS> loadVACMD_MCSIsUnfinished()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return vcmd_mcsDao.loadVACMD_MCSIsUnfinished(con);
            }
        }
        public List<VACMD_MCS> loadVACMD_MCS()
        {
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                return vcmd_mcsDao.loadAllVACMD(con);
            }
        }

        public bool isCMD_OHTCQueueByVh(string vh_id)
        {
            int count = 0;

            //DBConnection_EF con = DBConnection_EF.GetContext();
            //using (DBConnection_EF con = new DBConnection_EF())
            using (DBConnection_EF con = DBConnection_EF.GetUContext())
            {
                count = cmd_ohtcDAO.getVhQueueCMDConut(con, vh_id);
            }
            return count != 0;
        }
        public bool forceUpdataCmdStatus2FnishByVhID(string vh_id)
        {
            string result = string.Empty;
            string[] action_targets = new string[]
            {
                "Engineer",
                "ForceCmdFinish",
            };
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(vh_id)}={vh_id}").Append("&");
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            result = webClientManager.PostInfoToServer(WebClientManager.OHxC_CONTROL_URI, action_targets, WebClientManager.HTTP_METHOD.POST, byteArray);
            return result == "OK";
        }
    }
}
