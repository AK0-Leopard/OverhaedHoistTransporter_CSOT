using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data;
using System;
using System.Data.SqlClient;
using System.Security;
using System.Security.Permissions;

namespace com.mirle.ibg3k0.ohxc.winform.EventAction
{
    public class DBTableWatcher
    {
        private DBConnection_EF conForWatchTable = new DBConnection_EF();
        private ImmediateNotificationRegister<APORTSTATION> PortStationNotification = null;
        private ImmediateNotificationRegister<UASUSR> UserNotification = null;
        private ImmediateNotificationRegister<UASUSRGRP> UserGroupNotification = null;
        private ImmediateNotificationRegister<UASUFNC> UserGroupFuncNotification = null;
        private ImmediateNotificationRegister<ACMD_OHTC> OHTCCommandNotification = null;
        private ImmediateNotificationRegister<ACMD_MCS> MCSCommandNotification = null;
        private ImmediateNotificationRegister<ASEGMENT> SegmentNotification = null;
        //private ImmediateNotificationRegister<AVEHICLE> AVEHICLENotification = null;

        private App.WindownApplication app = null;

        public event EventHandler portStationChange;
        public event EventHandler userChange;
        public event EventHandler userGroupChange;
        public event EventHandler userGroupFuncChange;
        public event EventHandler mcsCMDChange;
        public event EventHandler segmentChange;
        //public event EventHandler vehicleChange;

        public DBTableWatcher(App.WindownApplication _app)
        {
            app = _app;
            ImmediateNotificationRegister<object>.StartMonitor(conForWatchTable);
            CanRequestNotifications();
        }
        private bool CanRequestNotifications()
        {
            // In order to use the callback feature of the
            // SqlDependency, the application must have
            // the SqlClientPermission permission.
            try
            {
                SqlClientPermission perm =
                    new SqlClientPermission(
                    PermissionState.Unrestricted);

                perm.Demand();

                return true;
            }
            catch (SecurityException se)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void initStart()
        {
            PortStationNotification = new ImmediateNotificationRegister<APORTSTATION>(conForWatchTable, app.PortStationDao.getQueryAllSQL(conForWatchTable));
            PortStationNotification.OnChanged += NotificationPositionStationOnChanged;
            UserNotification = new ImmediateNotificationRegister<UASUSR>(conForWatchTable, app.UserDao.getQueryAllSQL(conForWatchTable));
            UserNotification.OnChanged += NotificationUserOnChanged;
            UserGroupNotification = new ImmediateNotificationRegister<UASUSRGRP>(conForWatchTable, app.UserGroupDao.getQueryAllSQL(conForWatchTable));
            UserGroupNotification.OnChanged += NotificationUserGroupOnChanged;
            UserGroupFuncNotification = new ImmediateNotificationRegister<UASUFNC>(conForWatchTable, app.UserFuncDao.getQueryAllSQL(conForWatchTable));
            UserGroupFuncNotification.OnChanged += NotificationUserGroupFuncOnChanged;
            OHTCCommandNotification = new ImmediateNotificationRegister<ACMD_OHTC>(conForWatchTable, app.CMD_OHTCDao.getQueryAllSQL(conForWatchTable));
            OHTCCommandNotification.OnChanged += NotificationVMCSCMDOnChanged;
            MCSCommandNotification = new ImmediateNotificationRegister<ACMD_MCS>(conForWatchTable, app.CMD_MCSDao.getQueryAllSQL(conForWatchTable));
            MCSCommandNotification.OnChanged += NotificationVMCSCMDOnChanged;
            SegmentNotification = new ImmediateNotificationRegister<ASEGMENT>(conForWatchTable, app.SegmentDao.getQueryAllSQL(conForWatchTable));
            SegmentNotification.OnChanged += NotificationSegmentOnChanged;
            //AVEHICLENotification = new ImmediateNotificationRegister<AVEHICLE>(conForWatchTable, app.VehicleDao.getQueryAllSQL(conForWatchTable));
            //AVEHICLENotification.OnChanged += NotificationAVEHICLEOnChanged;
            //UserGroupNotification = new ImmediateNotificationRegister<UASUSRGRP>(conForWatchTable, app.UserGroupDao.getQueryAllSQL(conForWatchTable));
            //UserGroupNotification.OnChanged += NotificationUserGroupOnChanged;
        }



        void NotificationPositionStationOnChanged(object sender, EventArgs e)
        {
            portStationChange?.Invoke(this, e);
        }

        void NotificationUserOnChanged(object sender, EventArgs e)
        {
            userChange?.Invoke(this, e);
        }

        void NotificationUserGroupOnChanged(object sender, EventArgs e)
        {
            userGroupChange?.Invoke(this, e);
        }
        void NotificationUserGroupFuncOnChanged(object sender, EventArgs e)
        {
            userGroupFuncChange?.Invoke(this, e);
        }

        void NotificationVMCSCMDOnChanged(object sender, EventArgs e)
        {
            mcsCMDChange?.Invoke(this, e);
        }
        void NotificationSegmentOnChanged(object sender, EventArgs e)
        {
            segmentChange?.Invoke(this, e);
        }
        //void NotificationAVEHICLEOnChanged(object sender, EventArgs e)
        //{
        //    vehicleChange?.Invoke(this, e);
        //}
    }
}
