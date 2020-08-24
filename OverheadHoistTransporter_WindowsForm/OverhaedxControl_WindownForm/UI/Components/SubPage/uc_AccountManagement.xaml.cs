//*********************************************************************************
//      uc_AccountManagement.xaml.cs
//*********************************************************************************
// File Name: uc_AccountManagement.xaml.cs
// Description: User Account Management Form
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date                      Author                  Request No.        Tag                        Description
// ---------------     ---------------     ---------------     ---------------     ------------------------------
//  2019/07/29        Xenia                     N/A                       N/A                       Initial Release
//  2019/10/29        BoanChen            N/A                       N/A                       Update UI Function。
//**********************************************************************************

using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.VO;
using MirleGO_UIFrameWork.UI.uc_Button;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TreeView;

namespace com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage
{
    /// <summary>
    /// uc_AccountManagement.xaml 的互動邏輯
    /// </summary>
    public partial class uc_AccountManagement : UserControl
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ohxc.winform.App.WindownApplication app = null;
        private List<User> userDataList = new List<User>();
        public event EventHandler<UserEventArgs> userAccountAddRequest;
        public event EventHandler<UserEventArgs> userAccountModifyRequest;
        public event EventHandler<UserEventArgs> userAccountDeleteRequest;
        public event EventHandler<GroupEventArgs> userGroupAddRequest;
        public event EventHandler<GroupEventArgs> userGroupUpdateRequest;
        public event EventHandler<GroupEventArgs> userGroupDeleteRequest;
        public event EventHandler CloseFormEvent;
        //public event EventHandler ClearTxtboxEvent;
        List<UASUSR> users = null;
        #endregion 公用參數設定

        public uc_AccountManagement()
        {
            try
            {
                InitializeComponent();
                initTitle();
                start();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void initTitle()
        {
            try
            {
                UA_UserID.SetTitleName("User ID");
                UA_Password.SetTitleName("Password");
                UA_ConfrimPassword.SetTitleName("Confrim Password");
                UA_Group.SetTitleName("Group");
                UA_AccountActivation.SetTitleName("Account Activation");
                UA_UserName.SetTitleName("User Name");
                UA_Department.SetTitleName("Department");
                UA_BadgeNumber.SetTitleName("Badge Number");
                GA_Group.SetTitleName("Group");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ObjCacheManager_UserUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                refresh_grid_UserAcc();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        private void ObjCacheManager_UserGroupUpdateComplete(object sender, EventArgs e)
        {
            try
            {
                refresh_UserGrp();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void start()
        {
            try
            {
                app = ohxc.winform.App.WindownApplication.getInstance();
                registerEvent();
                this.users = users;
                grid_UserAcc.ItemsSource = app.ObjCacheManager.GetUsers();
                List<UASUSRGRP> user_grps = app.ObjCacheManager.GetUserGroups();
                grid_UserGroup.ItemsSource = user_grps;
                //UA_Group.combo_Content.ItemsSource = app.ObjCacheManager.GetUserGroups();
                UA_Group.combo_Content.Items.Clear();
                //GA_Group.combo_Content.Items.Clear();

                foreach (UASUSRGRP ugrp in user_grps)
                {
                    UA_Group.combo_Content.Items.Add(ugrp.USER_GRP);
                    //GA_Group.combo_Content.Items.Add(ugrp.USER_GRP);
                }

                refresh_grid_UserAcc();

                refresh_UserGrp();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void registerEvent()
        {
            try
            {
                app.ObjCacheManager.UserUpdateComplete += ObjCacheManager_UserUpdateComplete;
                app.ObjCacheManager.UserGroupUpdateComplete += ObjCacheManager_UserGroupUpdateComplete;
                userAccountAddRequest += uc_AddUserAccountRequest;
                userAccountModifyRequest += uc_UpdateUserAccountRequest;
                userAccountDeleteRequest += uc_DeleteUserAccountRequest;
                userGroupAddRequest += uc_AddUserGroupRequest;
                userGroupUpdateRequest += uc_UpdateUserGroupRequest;
                userGroupDeleteRequest += uc_DeleteUserGroupRequest;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void unRegisterEvent()
        {
            try
            {
                app.ObjCacheManager.UserUpdateComplete -= ObjCacheManager_UserUpdateComplete;
                app.ObjCacheManager.UserGroupUpdateComplete -= ObjCacheManager_UserGroupUpdateComplete;
                userAccountAddRequest -= uc_AddUserAccountRequest;
                userAccountModifyRequest -= uc_UpdateUserAccountRequest;
                userAccountDeleteRequest -= uc_DeleteUserAccountRequest;
                userGroupAddRequest -= uc_AddUserGroupRequest;
                userGroupUpdateRequest -= uc_UpdateUserGroupRequest;
                userGroupDeleteRequest -= uc_DeleteUserGroupRequest;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void refresh_grid_UserAcc()
        {
            try
            {
                bcf.Common.Adapter.Invoke((obj) =>
                {
                    grid_UserAcc.ItemsSource = app.ObjCacheManager.GetUsers();
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void refresh_UserGrp()
        {
            try
            {
                bcf.Common.Adapter.Invoke((obj) =>
                {
                    grid_UserGroup.ItemsSource = app.ObjCacheManager.GetUserGroups();
                    UA_Group.combo_Content.SelectedIndex = -1;
                    UA_Group.combo_Content.Items.Clear();
                    foreach (UASUSRGRP ugrp in app.ObjCacheManager.GetUserGroups())
                    {
                        UA_Group.combo_Content.Items.Add(ugrp.USER_GRP);
                        //GA_Group.combo_Content.Items.Add(ugrp.USER_GRP);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UA_UserID.txt_Content.Focus(); //將游標指定在userID位置

                List<UASFNC> FunctionCodeList = app.ObjCacheManager.GetFunctionCodes();

                //updateUserGroupTreeView(userFuncList);

                tV_Permission.ItemsSource = TreeViewModel.SetTree("Select All", FunctionCodeList);

                tV_Permission.Height = 445;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void TabItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TabItemPreviewMouseLeftButtonUp(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void TabItemPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender.Equals(TabItem_UA))
                {
                    UserAccountInfo.Visibility = Visibility;
                    GroupAccountInfo.Visibility = Visibility.Collapsed;
                }
                else if (sender.Equals(TabItem_GA))
                {
                    UserAccountInfo.Visibility = Visibility.Collapsed;
                    GroupAccountInfo.Visibility = Visibility;
                    //registerFunction();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        public void Refresh(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender.Equals(TabItem_UA))
                {
                    clearTextBox();
                    refresh_grid_UserAcc();
                }
                else if (sender.Equals(TabItem_GA))
                {
                    clearTextBox();
                    refresh_UserGrp();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void clearTextBox()
        {
            try
            {
                UA_UserID.txt_Content.Clear();
                UA_Password.pwd_Password.Clear();
                UA_ConfrimPassword.pwd_Password.Clear();
                UA_UserName.txt_Content.Clear();
                UA_Department.txt_Content.Clear();
                UA_BadgeNumber.txt_Content.Clear();
                UA_Group.combo_Content.SelectedIndex = -1;
                UA_AccountActivation.radbtn_Yes.IsChecked = true;
                UA_AccountActivation.radbtn_No.IsChecked = false;
                GA_Group.txt_Content.Clear();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private UserGroup getSelectedRowToTextBox_UserGroup()
        {
            //A0.01 Start
            UserGroup selectUserGroup = new UserGroup();

            try
            {
                if (grid_UserGroup.SelectedItems == null)
                    return null;
                if (grid_UserGroup.SelectedItems.Count < 1)
                    return null;

                selectUserGroup.User_Grp = grid_UserGroup.SelectedItems[0].ToString();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return selectUserGroup;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonClick(sender, e);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender.Equals(btn_Clear))
                {
                    clearTextBox();

                    clearUserGroupTreeview();
                }

                else if (sender.Equals(btn_Add))
                {
                    if (TabItem_UA.IsSelected)
                    {
                        string userID = UA_UserID.txt_Content.Text;
                        string password = UA_Password.pwd_Password.Password;
                        string password_v = UA_ConfrimPassword.pwd_Password.Password;
                        //string group_id =UA_Group
                        string group_id = UA_Group.combo_Content.SelectedValue.ToString();
                        string disable_flag = UA_AccountActivation.radbtn_No.IsChecked.ToString();

                        string user_name = UA_UserName.txt_Content.Text;
                        string department = UA_Department.txt_Content.Text;
                        string badge_num = UA_BadgeNumber.txt_Content.Text;
                        //string group_id  =.Text;

                        if (password != password_v)
                        {
                            TipMessage_Type_Light.Show("Failure", "Please confirm the password.", BCAppConstants.INFO_MSG);
                            return;
                        }
                        else if (SCUtility.isEmpty(password) || SCUtility.isEmpty(password_v))
                        {
                            TipMessage_Type_Light.Show("Failure", "Please fill in the password.", BCAppConstants.INFO_MSG);
                            return;
                        }
                        else
                        {
                            userAccountAddRequest?.Invoke(this, new UserEventArgs(userID, password, group_id, disable_flag, user_name, department, badge_num));
                        }
                    }

                    else
                    {
                        string group_id = GA_Group.txt_Content.Text == null ? string.Empty : GA_Group.txt_Content.Text;

                        if (string.IsNullOrWhiteSpace(group_id))
                        {
                            TipMessage_Type_Light.Show("Failure", "Group could not be empty.", BCAppConstants.INFO_MSG);
                            return;
                        }

                        TreeViewModel root = (TreeViewModel)tV_Permission.Items[0];

                        string funcAccountManagement = string.Empty;
                        string funcLogin = string.Empty;
                        string funcCloseSystem = string.Empty;
                        string funcSystemControlMode = string.Empty;
                        string funcVehicleManagement = string.Empty;
                        string funcTransferManagement = string.Empty;
                        string funcMTLMTSMaintenance = string.Empty;
                        string funcPortMaintenance = string.Empty;
                        string funcDebug = string.Empty;
                        string funcAdcancedSettings = string.Empty;

                        funcAccountManagement = getTreeviewItemsSetting(root, 0, 0);
                        funcCloseSystem = getTreeviewItemsSetting(root, 0, 1);
                        funcLogin = getTreeviewItemsSetting(root, 0, 2);
                        funcSystemControlMode = getTreeviewItemsSetting(root, 1, 0);
                        funcTransferManagement = getTreeviewItemsSetting(root, 1, 1);
                        funcAdcancedSettings = getTreeviewItemsSetting(root, 2, 0);
                        funcMTLMTSMaintenance = getTreeviewItemsSetting(root, 2, 1);
                        funcPortMaintenance = getTreeviewItemsSetting(root, 2, 2);
                        funcVehicleManagement = getTreeviewItemsSetting(root, 2, 3);
                        funcDebug = getTreeviewItemsSetting(root, 3, 0);

                        userGroupAddRequest?.Invoke(this,
                            new GroupEventArgs(
                                group_id, funcCloseSystem,
                                funcSystemControlMode,
                                funcLogin,
                                funcAccountManagement,
                                funcVehicleManagement,
                                funcTransferManagement,
                                funcMTLMTSMaintenance,
                                funcPortMaintenance,
                                funcDebug,
                                funcAdcancedSettings)
                                );
                    }
                }

                else if (sender.Equals(btn_Modify))
                {
                    if (TabItem_UA.IsSelected)
                    {
                        string userID = UA_UserID.txt_Content.Text;
                        string password = UA_Password.pwd_Password.Password;
                        string password_v = UA_ConfrimPassword.pwd_Password.Password;
                        //string group_id = UA_Group.txt_Content.Text;
                        string group_id = UA_Group.combo_Content.SelectedValue.ToString();
                        //string disable_flag = UA_AccountActivation.txt_Content.Text;
                        string disable_flag = UA_AccountActivation.radbtn_No.IsChecked.ToString();
                        string user_name = UA_UserName.txt_Content.Text;
                        string department = UA_Department.txt_Content.Text;
                        string badge_num = UA_BadgeNumber.txt_Content.Text;
                        //string group_id  =.Text;

                        if (password != password_v)
                        {
                            TipMessage_Type_Light.Show("Failure", "Please confirm the password.", BCAppConstants.INFO_MSG);
                            return;
                        }
                        else if (SCUtility.isEmpty(password) || SCUtility.isEmpty(password_v))
                        {
                            TipMessage_Type_Light.Show("Failure", "Please fill in the password.", BCAppConstants.INFO_MSG);
                            return;
                        }
                        else
                        {
                            userAccountModifyRequest?.Invoke(this, new UserEventArgs(userID, password, group_id, disable_flag, user_name, department, badge_num));
                        }
                    }
                    else
                    {
                        string group_id = GA_Group.txt_Content.Text == null ? string.Empty : GA_Group.txt_Content.Text;

                        if (string.IsNullOrWhiteSpace(group_id))
                        {
                            TipMessage_Type_Light.Show("Failure", "Group could not be empty.", BCAppConstants.INFO_MSG);
                            return;
                        }

                        TreeViewModel root = (TreeViewModel)tV_Permission.Items[0];

                        string funcAccountManagement = string.Empty;
                        string funcLogin = string.Empty;
                        string funcCloseSystem = string.Empty;
                        string funcSystemControlMode = string.Empty;
                        string funcVehicleManagement = string.Empty;
                        string funcTransferManagement = string.Empty;
                        string funcMTLMTSMaintenance = string.Empty;
                        string funcPortMaintenance = string.Empty;
                        string funcDebug = string.Empty;
                        string funcAdcancedSettings = string.Empty;

                        funcAccountManagement = getTreeviewItemsSetting(root, 0, 0);
                        funcCloseSystem = getTreeviewItemsSetting(root, 0, 1);
                        funcLogin = getTreeviewItemsSetting(root, 0, 2);
                        funcSystemControlMode = getTreeviewItemsSetting(root, 1, 0);
                        funcTransferManagement = getTreeviewItemsSetting(root, 1, 1);
                        funcAdcancedSettings = getTreeviewItemsSetting(root, 2, 0);
                        funcMTLMTSMaintenance = getTreeviewItemsSetting(root, 2, 1);
                        funcPortMaintenance = getTreeviewItemsSetting(root, 2, 2);
                        funcVehicleManagement = getTreeviewItemsSetting(root, 2, 3);
                        funcDebug = getTreeviewItemsSetting(root, 3, 0);

                        userGroupUpdateRequest?.Invoke(this,
                            new GroupEventArgs(
                            group_id,
                            funcCloseSystem,
                            funcSystemControlMode,
                            funcLogin,
                            funcAccountManagement,
                            funcVehicleManagement,
                            funcTransferManagement,
                            funcMTLMTSMaintenance,
                            funcPortMaintenance,
                            funcDebug,
                            funcAdcancedSettings)
                            );
                    }
                }

                else if (sender.Equals(btn_Delete))
                {
                    if (TabItem_UA.IsSelected)
                    {
                        string userID = UA_UserID.txt_Content.Text;
                        userAccountDeleteRequest?.Invoke(this, new UserEventArgs(userID, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
                    }
                    else
                    {
                        string group_id = GA_Group.txt_Content.Text == null ? string.Empty : GA_Group.txt_Content.Text;

                        userGroupDeleteRequest?.Invoke(this,
                            new GroupEventArgs(
                                group_id,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null)
                                );

                        clearUserGroupTreeview();
                    }
                }

                else if (sender.Equals(btn_Close))
                {
                    CloseFormEvent?.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            finally
            {
            }
        }

        private string getTreeviewItemsSetting(TreeViewModel root, int index1, int index2)
        {
            string result = string.Empty;
            try
            {
                if ((bool)root.Children[index1].Children[index2].IsChecked)
                {
                    result = true.ToString();
                    return result;
                }
                else
                {
                    result = false.ToString();
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return result;
        }

        private void uc_AddUserAccountRequest(object sender, uc_AccountManagement.UserEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean createSuccess =
                    app.LineBLL.SendUserAccountAddRequest(
                        e.userID,
                        e.password,
                        e.user_name,
                        e.disable_flag,
                        e.grp_id,
                        e.badge_num,
                        e.department,
                        out result);

                if (createSuccess)
                {
                    TipMessage_Type_Light.Show("Succeed", "Create Success.", BCAppConstants.INFO_MSG);
                    Refresh(TabItem_UA, null);
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Create Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_UpdateUserAccountRequest(object sender, uc_AccountManagement.UserEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean updateSuccess =
                    app.LineBLL.SendUserAccountUpdateRequest(
                        e.userID,
                        e.password,
                        e.user_name,
                        e.disable_flag,
                        e.grp_id,
                        e.badge_num,
                        e.department,
                        out result);

                if (updateSuccess)
                {
                    TipMessage_Type_Light.Show("Succeed", "Update Success.", BCAppConstants.INFO_MSG);
                    Refresh(TabItem_UA, null);
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Update Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_DeleteUserAccountRequest(object sender, UserEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean deleteSuccess =
                    app.LineBLL.SendUserAccountDeleteRequest(e.userID, out result);

                if (deleteSuccess)
                {
                    TipMessage_Type_Light.Show("Succeed", "Delete Success.", BCAppConstants.INFO_MSG);
                    Refresh(TabItem_UA, null);
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Delete Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_AddUserGroupRequest(object sender, GroupEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean createSuccess =
                    app.LineBLL.SendUserGroupAddRequest(
                        e.grp_id,
                        e.funcCloseSystem,
                        e.funcSystemControlMode,
                        e.funcLogin,
                        e.funcAccountManagement,
                        e.funcVehicleManagement,
                        e.funcTransferManagement,
                        e.funcMTLMTSMaintenance,
                        e.funcPortMaintenance,
                        e.funcDebug,
                        e.funcAdvancedSetting,
                        out result);

                if (createSuccess)
                {
                    TipMessage_Type_Light.Show("Succeed", "Create Success.", BCAppConstants.INFO_MSG);
                    Refresh(TabItem_GA, null);
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Create Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_UpdateUserGroupRequest(object sender, GroupEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean updateSuccess =
                    app.LineBLL.SendUserGroupUpdateRequest(
                        e.grp_id,
                        e.funcCloseSystem,
                        e.funcSystemControlMode,
                        e.funcLogin,
                        e.funcAccountManagement,
                        e.funcVehicleManagement,
                        e.funcTransferManagement,
                        e.funcMTLMTSMaintenance,
                        e.funcPortMaintenance,
                        e.funcDebug,
                        e.funcAdvancedSetting,
                        out result);

                if (updateSuccess)
                {
                    TipMessage_Type_Light.Show("Succeed", "Update Success.", BCAppConstants.INFO_MSG);
                    Refresh(TabItem_GA, null);
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Update Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void uc_DeleteUserGroupRequest(object sender, uc_AccountManagement.GroupEventArgs e)
        {
            try
            {
                string result = string.Empty;
                Boolean deleteSuccess =
                    app.LineBLL.SendUserGroupDeleteRequest(e.grp_id, out result);

                if (deleteSuccess)
                {
                    TipMessage_Type_Light.Show("Succeed", "Update Success.", BCAppConstants.INFO_MSG);
                    Refresh(TabItem_GA, null);
                }
                else
                {
                    TipMessage_Type_Light.Show("Failure", "Update Failed.", BCAppConstants.INFO_MSG);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void grid_UserAcc_cell_click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UASUSR userAccData = (UASUSR)grid_UserAcc.SelectedItem;
                if (userAccData == null) return;

                UA_UserID.txt_Content.Text = userAccData.USER_ID == null ? string.Empty : userAccData.USER_ID;
                UA_Password.pwd_Password.Password = userAccData.PASSWD == null ? string.Empty : userAccData.PASSWD;
                UA_ConfrimPassword.pwd_Password.Password = userAccData.PASSWD == null ? string.Empty : userAccData.PASSWD;
                UA_UserName.txt_Content.Text = userAccData.USER_NAME == null ? string.Empty : userAccData.USER_NAME;
                UA_Group.combo_Content.Text = userAccData.USER_GRP == null ? string.Empty : userAccData.USER_GRP;
                UA_BadgeNumber.txt_Content.Text = userAccData.BADGE_NUMBER == null ? string.Empty : userAccData.BADGE_NUMBER;

                if (userAccData.isDisable())
                {
                    UA_AccountActivation.radbtn_Yes.IsChecked = false;
                    UA_AccountActivation.radbtn_No.IsChecked = true;
                }
                else
                {
                    UA_AccountActivation.radbtn_Yes.IsChecked = true;
                    UA_AccountActivation.radbtn_No.IsChecked = false;
                }

                UA_Department.txt_Content.Text = userAccData.DEPARTMENT == null ? string.Empty : userAccData.DEPARTMENT;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void grid_UserGroup_cell_click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UASUSRGRP usergroupData = (UASUSRGRP)grid_UserGroup.SelectedItem;
                if (usergroupData == null) return;

                GA_Group.txt_Content.Text = usergroupData.USER_GRP == null ? string.Empty : usergroupData.USER_GRP;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void UserGroup_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                UserGroup selectUserGroup = getGroupIDFromGroupList();

                if (selectUserGroup == null) return;

                List<UASUFNC> userFuncList = app.ObjCacheManager.GetUserFuncs(selectUserGroup.User_Grp);

                clearUserGroupTreeview();

                foreach (UASUFNC uasufnc in userFuncList)
                {
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.System_Function.FUNC_ACCOUNT_MANAGEMENT))
                    {
                        getEachTreeViewItemCheckboxSetting(0, 0);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.System_Function.FUNC_CLOSE_SYSTEM))
                    {
                        getEachTreeViewItemCheckboxSetting(0, 1);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.System_Function.FUNC_LOGIN))
                    {
                        getEachTreeViewItemCheckboxSetting(0, 2);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Operation_Function.FUNC_SYSTEM_CONCROL_MODE))
                    {
                        getEachTreeViewItemCheckboxSetting(1, 0);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Operation_Function.FUNC_TRANSFER_MANAGEMENT))
                    {
                        getEachTreeViewItemCheckboxSetting(1, 1);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Maintenance_Function.FUNC_ADVANCED_SETTINGS))
                    {
                        getEachTreeViewItemCheckboxSetting(2, 0);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Maintenance_Function.FUNC_MTS_MTL_MAINTENANCE))
                    {
                        getEachTreeViewItemCheckboxSetting(2, 1);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Maintenance_Function.FUNC_PORT_MAINTENANCE))
                    {
                        getEachTreeViewItemCheckboxSetting(2, 2);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Maintenance_Function.FUNC_VEHICLE_MANAGEMENT))
                    {
                        getEachTreeViewItemCheckboxSetting(2, 3);
                    }
                    if (SCUtility.isMatche(uasufnc.FUNC_CODE.Trim(), BCAppConstants.Debug_Function.FUNC_DEBUG))
                    {
                        getEachTreeViewItemCheckboxSetting(3, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private void getEachTreeViewItemCheckboxSetting(int index1, int index2)
        {
            try
            {
                TreeViewModel root = (TreeViewModel)tV_Permission.Items[0];
                root = (TreeViewModel)tV_Permission.Items[0];
                root.Children[index1].Children[index2].IsChecked = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        private UserGroup getGroupIDFromGroupList()
        {
            //A0.01 Start
            UserGroup selectUserGroup = new UserGroup();

            try
            {
                if (grid_UserGroup.SelectedItem == null)
                    return null;
                if (grid_UserGroup.Items.Count < 1)
                    return null;

                selectUserGroup.User_Grp = (grid_UserGroup.Columns[0].GetCellContent(grid_UserGroup.Items[grid_UserGroup.SelectedIndex]) as TextBlock).Text.ToString();

                //int selectedRowCnt = UserGroupGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
                //if (selectedRowCnt <= 0) 
                //{
                //    return null;
                //}
                //int selectedIndex = UserGroupGridView.SelectedRows[0].Index;
                //if (userGroupList.Count <= selectedIndex) 
                //{
                //    return null;
                //}
                //UserGroup selectUserGroup = userGroupList[selectedIndex];
                //A0.01 End
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
            return selectUserGroup;
        }

        private void clearUserGroupTreeview()
        {
            try
            {
                TreeViewModel root1 = (TreeViewModel)tV_Permission.Items[0];

                for (int i = 0; i < root1.Children.Count; i++)
                {
                    root1.Children[i].IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }


        //public class UserModifyEventArgs : EventArgs
        //{
        //    public UserModifyEventArgs(string userID, string password, string grp_id, string disable_flag, string user_name, string department, string badge_num)
        //    {
        //        this.userID = userID;
        //        this.password = password;
        //        this.grp_id = grp_id;
        //        this.disable_flag = disable_flag;
        //        this.user_name = user_name;
        //        this.department = department;
        //        this.badge_num = badge_num;
        //    }

        //    public string userID { get; private set; }
        //    public string password { get; private set; }
        //    public string grp_id { get; private set; }
        //    public string disable_flag { get; private set; }
        //    public string user_name { get; private set; }
        //    public string department { get; private set; }
        //    public string badge_num { get; private set; }
        //}

        public class UserEventArgs : EventArgs
        {
            public UserEventArgs(
                string userID,
                string password,
                string grp_id,
                string disable_flag,
                string user_name,
                string department,
                string badge_num)
            {
                this.userID = userID;
                this.password = password;
                this.grp_id = grp_id;
                this.disable_flag = disable_flag;
                this.user_name = user_name;
                this.department = department;
                this.badge_num = badge_num;
            }

            public string userID { get; private set; }
            public string password { get; private set; }
            public string grp_id { get; private set; }
            public string disable_flag { get; private set; }
            public string user_name { get; private set; }
            public string department { get; private set; }
            public string badge_num { get; private set; }
        }

        public class GroupEventArgs : EventArgs
        {
            public GroupEventArgs(
                string grp_id,
                string funcCloseSystem,
                string funcSystemControlMode,
                string funcLogin,
                string funcAccountManagement,
                string funcVehicleManagement,
                string funcTransferManagement,
                string funcMTLMTSMaintenance,
                string funcPortMaintenance,
                string funcDebug,
                string funcAdvancedSetting)
            {
                this.grp_id = grp_id;
                this.funcCloseSystem = funcCloseSystem;
                this.funcSystemControlMode = funcSystemControlMode;
                this.funcLogin = funcLogin;
                this.funcAccountManagement = funcAccountManagement;
                this.funcVehicleManagement = funcVehicleManagement;
                this.funcTransferManagement = funcTransferManagement;
                this.funcMTLMTSMaintenance = funcMTLMTSMaintenance;
                this.funcPortMaintenance = funcPortMaintenance;
                this.funcDebug = funcDebug;
                this.funcAdvancedSetting = funcAdvancedSetting;
            }
            public string grp_id { get; private set; }
            public string funcCloseSystem { get; private set; }
            public string funcSystemControlMode { get; private set; }
            public string funcLogin { get; private set; }
            public string funcAccountManagement { get; private set; }
            public string funcVehicleManagement { get; private set; }
            public string funcTransferManagement { get; private set; }
            public string funcMTLMTSMaintenance { get; private set; }
            public string funcPortMaintenance { get; private set; }
            public string funcDebug { get; private set; }
            public string funcAdvancedSetting { get; private set; }

        }

    }
}
