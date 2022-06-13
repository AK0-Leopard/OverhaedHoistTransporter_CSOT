﻿using com.mirle.ibg3k0.sc.Data.SECS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace com.mirle.ibg3k0.sc.Data.DAO.EntityFramework
{
    public class CMD_MCSDao
    {
        public const string EXPIRE_TAG_NON_FINISH_MCS_CMD = "EXPIRE_TAG_NON_FINISH_MCS_CMD";
        public void add(DBConnection_EF con, ACMD_MCS rail)
        {
            con.ACMD_MCS.Add(rail);
            con.SaveChanges();
            QueryCacheManager.ExpireTag(EXPIRE_TAG_NON_FINISH_MCS_CMD);
        }
        public void RemoteByBatch(DBConnection_EF con, List<ACMD_MCS> cmd_mcss)
        {
            cmd_mcss.ForEach(entity => con.Entry(entity).State = EntityState.Deleted);
            con.ACMD_MCS.RemoveRange(cmd_mcss);
            con.SaveChanges();
            QueryCacheManager.ExpireTag(EXPIRE_TAG_NON_FINISH_MCS_CMD);
        }


        public void update(DBConnection_EF con, ACMD_MCS cmd)
        {
            con.SaveChanges();
            QueryCacheManager.ExpireTag(EXPIRE_TAG_NON_FINISH_MCS_CMD);
        }

        public ACMD_MCS getByID(DBConnection_EF con, String cmd_id)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.CMD_ID == cmd_id.Trim()
                        select cmd;
            return query.SingleOrDefault();
        }

        public ACMD_MCS getWatingCMDMCSByFrom(DBConnection_EF con, string hostSource)
        {
            var query = from cmd in con.ACMD_MCS
                        where (cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue && 
                               cmd.TRANSFERSTATE < E_TRAN_STATUS.Transferring) &&
                               cmd.HOSTSOURCE.Trim() == hostSource.Trim()
                        select cmd;
            return query.FirstOrDefault();
        }
        public int getWatingCMDMCSByFromOfCount(DBConnection_EF con, string hostSource)
        {
            var query = from cmd in con.ACMD_MCS
                        where (cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue && 
                               cmd.TRANSFERSTATE < E_TRAN_STATUS.Transferring) &&
                               cmd.HOSTSOURCE.Trim() == hostSource.Trim()
                        select cmd;
            return query.Count();
        }


        public List<ACMD_MCS> loadACMD_MCSIsQueue(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS.AsNoTracking()
                        where (cmd.TRANSFERSTATE == E_TRAN_STATUS.Queue || cmd.TRANSFERSTATE == E_TRAN_STATUS.RouteChanging)
                        && cmd.CHECKCODE.Trim() == SECSConst.HCACK_Confirm
                        orderby cmd.PRIORITY_SUM descending, cmd.CMD_INSER_TIME
                        select cmd;

            return query.ToList();
        }

        public List<ACMD_MCS> loadACMD_MCSIsUnfinished(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS.AsNoTracking()
                        where cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                           && cmd.CHECKCODE.Trim() == SECSConst.HCACK_Confirm
                        orderby cmd.PRIORITY_SUM descending, cmd.CMD_INSER_TIME
                        select cmd;

            //return query.ToList();
            return query.FromCache(EXPIRE_TAG_NON_FINISH_MCS_CMD).ToList();
        }

        public List<ACMD_MCS> loadFinishCMD_MCS(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE >= E_TRAN_STATUS.Canceled
                        select cmd;
            return query.ToList();
        }



        public IQueryable getQueryAllSQL(DBConnection_EF con)
        {
            var query = from cmd_mcs in con.ACMD_MCS
                        select cmd_mcs;
            return query;
        }

        public int getCMD_MCSIsQueueCount(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE == E_TRAN_STATUS.Queue
                        select cmd;
            return query.Count();
        }
        public int getCMD_MCSIsExcuteCount(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue
                        && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                        select cmd;
            return query.Count();
        }
        public int getCMD_MCSIsExcuteCount(DBConnection_EF con, DateTime defore_time)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue
                        && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                        && cmd.CMD_INSER_TIME < defore_time
                        select cmd;
            return query.Count();
        }
        public List<string> loadIsExcuteCMD_MCS_ID(DBConnection_EF con, DateTime defore_time)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE > E_TRAN_STATUS.Queue
                        && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                        && cmd.CMD_INSER_TIME < defore_time
                        select cmd.CMD_ID;
            return query.ToList();
        }
        public int getCMD_MCSIsUnfinishedCount(DBConnection_EF con, List<string> port_ids)
        {
            var query = from cmd in con.ACMD_MCS
                        where port_ids.Contains(cmd.HOSTSOURCE.Trim()) &&
                        cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue
                        && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                        select cmd;
            return query.Count();
        }

        public int getCMD_MCSInserCountLastHour(DBConnection_EF con, int hours)
        {
            DateTime nowTime = DateTime.Now;
            DateTime lastTime = nowTime.AddHours(-hours);

            var query = from cmd in con.ACMD_MCS
                        where cmd.CMD_INSER_TIME < nowTime &&
                        cmd.CMD_INSER_TIME > lastTime
                        select cmd;
            return query.Count();
        }
        public int getCMD_MCSFinishCountLastHours(DBConnection_EF con, int hours)
        {
            DateTime nowTime = DateTime.Now;
            DateTime lastTime = nowTime.AddHours(-hours);
            var query = from cmd in con.ACMD_MCS
                        where cmd.CMD_FINISH_TIME < nowTime &&
                        cmd.CMD_FINISH_TIME > lastTime
                        select cmd;
            return query.Count();
        }

        public int getCMD_MCSIsUnfinishedCountByCarrierID(DBConnection_EF con, string carrier_id)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.CARRIER_ID.Trim() == carrier_id.Trim() &&
                        cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue
                        && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                        select cmd;
            return query.Count();
        }


        public int getCMD_MCSIsUnfinishedCountByPortID(DBConnection_EF con, string portID)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.HOSTDESTINATION.Trim() == portID.Trim() &&
                        cmd.TRANSFERSTATE >= E_TRAN_STATUS.Queue
                        && cmd.TRANSFERSTATE < E_TRAN_STATUS.Canceled
                        select cmd;
            return query.Count();
        }



        public int getCMD_MCSMaxPrioritySum(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE == E_TRAN_STATUS.Queue
                        orderby cmd.PRIORITY_SUM descending
                        select cmd.PRIORITY_SUM;
            List<int> prorityList = query.ToList();
            if (prorityList.Count == 0)
            {
                return 0;
            }
            else
            {
                return prorityList[0];
            }
        }
        public int getCMD_MCSMinPrioritySum(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.TRANSFERSTATE == E_TRAN_STATUS.Queue
                        orderby cmd.PRIORITY_SUM ascending
                        select cmd.PRIORITY_SUM;
            List<int> prorityList = query.ToList();
            if (prorityList.Count == 0)
            {
                return 0;
            }
            else
            {
                return prorityList[0];
            }
        }

        public int getCMD_MCSTotalCount(DBConnection_EF con)
        {
            var query = from cmd in con.ACMD_MCS
                        select cmd;
            return query.Count();
        }

        internal string GetCmdPauseFlag(DBConnection_EF con, string cmdMcsID)
        {
            var query = from cmd in con.ACMD_MCS
                        where cmd.CMD_ID == cmdMcsID.Trim()
                        select cmd.PAUSEFLAG;
            return query.FirstOrDefault();
        }
    }

}
