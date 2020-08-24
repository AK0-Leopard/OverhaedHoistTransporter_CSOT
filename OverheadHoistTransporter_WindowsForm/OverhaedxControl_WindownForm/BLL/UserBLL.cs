using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data;
using STAN.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.ibg3k0.ohxc.winform.BLL
{
    public class UserBLL
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        WindownApplication app = null;
        public DB_User OperateDBUser { private set; get; }
        public DB_UserGroup OperateDBUserGroup { private set; get; }
        public DB_UserGroupFunc OperateDBUserGroupFunc { private set; get; }
        public DB_FunctionCode OperateDBFunctionCode { private set; get; }

        public UserBLL(WindownApplication _app)
        {
            app = _app;
            OperateDBUser = new DB_User(app.UserDao, app.UserFuncDao);
            OperateDBUserGroup = new DB_UserGroup(app.UserGroupDao);
            OperateDBUserGroupFunc = new DB_UserGroupFunc(app.UserFuncDao);
            OperateDBFunctionCode = new DB_FunctionCode(app.FunctionCodeDao);
        }



        public class DB_User
        {
            sc.Data.DAO.UserDao userDao = null;
            sc.Data.DAO.UserFuncDao userFuncDao = null;
            public DB_User(sc.Data.DAO.UserDao userDao, sc.Data.DAO.UserFuncDao userFuncDao)
            {
                this.userDao = userDao;
                this.userFuncDao = userFuncDao;
            }
            public List<sc.UASUSR> loadUser()
            {
                List<sc.UASUSR> user = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        user = userDao.loadAllUser(con);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return user;
            }

            public Boolean checkUserPassword(string user_id, string password)
            {
                DBConnection_EF conn = null;
                Boolean result = false;
                try
                {
                    conn = DBConnection_EF.GetContext();
                    conn.BeginTransaction();
                    UASUSR loginUser = userDao.getUser(conn, false, user_id);
                    if (loginUser == null)
                    {
                        result = false;
                    }
                    else if (SCUtility.isMatche(loginUser.PASSWD, password))
                    {
                        result = true;
                    }
                    conn.Commit();
                }
                catch (Exception ex)
                {
                    logger.Warn("Load User Function Failed from UASUFNC [user_id:{0}]",
                        user_id, ex);
                    result = false;
                }
                finally
                {
                    if (conn != null) { try { conn.Close(); } catch { } }
                }
                return result;
            }


            public Boolean checkUserAuthority(string user_id, string function_code)
            {
                DBConnection_EF conn = null;
                Boolean result = true;
                try
                {
                    conn = DBConnection_EF.GetContext();
                    conn.BeginTransaction();
                    UASUSR loginUser = userDao.getUser(conn, false, user_id);
                    if (loginUser == null)
                    {
                        result = false;
                    }
                    else if (loginUser.isPowerUser())
                    {
                        result = true;
                    }
                    else
                    {
                        //A0.01 UserFunc userFunc = userFuncDao.getUserFunc(conn, user_id, function_code);
                        UASUFNC userFunc = userFuncDao.getUserFunc(conn, loginUser.USER_GRP, function_code);
                        if (userFunc == null)
                        {
                            result = false;
                        }
                    }
                    conn.Commit();
                }
                catch (Exception ex)
                {
                    logger.Warn("Load User Function Failed from UASUFNC [user_id:{0}]",
                        user_id, ex);
                    result = false;
                }
                finally
                {
                    if (conn != null)
                    {
                        try
                        {
                            conn.Close();
                        }
                        catch (Exception exception)
                        {
                            logger.Warn("Close Connection Failed.", exception);
                        }
                    }
                }
                return result;
            }
        }



        public class DB_UserGroup
        {
            sc.Data.DAO.UserGroupDao userGroupDao = null;
            public DB_UserGroup(sc.Data.DAO.UserGroupDao userGroupDao)
            {
                this.userGroupDao = userGroupDao;
            }


            public List<sc.UASUSRGRP> loadUserGroup()
            {
                List<sc.UASUSRGRP> user_grp = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        user_grp = userGroupDao.loadAllUserGroup(con);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return user_grp;
            }
        }
        public class DB_UserGroupFunc
        {
            sc.Data.DAO.UserFuncDao userFuncDao = null;
            public DB_UserGroupFunc(sc.Data.DAO.UserFuncDao userFuncDao)
            {
                this.userFuncDao = userFuncDao;
            }


            public List<sc.UASUFNC> loadAllUserGroupFunc()
            {
                List<sc.UASUFNC> rtnList = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        rtnList = userFuncDao.loadAllUserFuncByUserGrp(con);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return rtnList;
            }


            public List<sc.UASUFNC> loadUserGroupFunc(string user_grp)
            {
                List<sc.UASUFNC> rtnList = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        rtnList = userFuncDao.loadUserFuncByUserGrp(con, user_grp);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return rtnList;
            }
        }


        public class DB_FunctionCode
        {
            sc.Data.DAO.FunctionCodeDao functionCodeDao = null;
            public DB_FunctionCode(sc.Data.DAO.FunctionCodeDao functionCodeDao)
            {
                this.functionCodeDao = functionCodeDao;
            }


            public List<sc.UASFNC> loadAllFunctionCode()
            {
                List<sc.UASFNC> rtnList = null;
                try
                {
                    using (DBConnection_EF con = DBConnection_EF.GetUContext())
                    {
                        rtnList = functionCodeDao.loadAllFunctionCode(con);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception");
                }
                return rtnList;
            }
        }

    }
}
