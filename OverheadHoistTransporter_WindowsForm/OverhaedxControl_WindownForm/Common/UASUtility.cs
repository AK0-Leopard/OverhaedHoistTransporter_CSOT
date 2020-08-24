//*********************************************************************************
//      UASUtility.cs
//*********************************************************************************
// File Name: UASUtility.cs
// Description: Type 1 Function
//
//(c) Copyright 2015, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
// 2015/05/29    Kevin Wei      N/A            A0.01   加入"isForceChack"的判斷，用來使用在一次的確認目前的User確實有使用權限。
// 2016/08/11    Steven Hong    N/A         A0.02   Add Login by Badge
// 2016/09/29    Steven Hong    N/A         A0.03   將所有Message Box換為自製元件
// 2016/10/31    Bob Yan        N/A            A0.04   add updateUIDisplayByAuthority(BCApplication bcApp,object targetFormType)
// 2017/01/23    Eric Chiang    N/A           A0.05   修正User被disable時, 即使登入失敗, 還是會改變鎖頭狀態以及顯示User名稱
// 2017/05/09    Boan Chen    N/A           A0.07   實作使用者登出介面
// 2018/04/25    Boan Chen        N/A        A0.08   Tip Message樣式變更。
// 2019/03/07    Mark Chou        N/A        A0.09   uc_btn_Custom加入使用者權限管理，不具權限者將不予顯示。


//**********************************************************************************
using com.mirle.ibg3k0.bc.winform.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.bc.winform.UI.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using com.mirle.ibg3k0.bc.winform.UI;       //A0.07 
using MirleGO_UIFrameWork.UI.uc_Button;
using com.mirle.ibg3k0.ohxc.winform;
using com.mirle.ibg3k0.ohxc.winform.App;
using com.mirle.ibg3k0.ohxc.winform.UI.Menu_System;
using com.mirle.ibg3k0.ohxc.winform.UI.Components.SubPage;
using com.mirle.ibg3k0.bcf.Common;
using System.Threading;
using NLog;

namespace com.mirle.ibg3k0.bc.winform.Common
{
    public class UASUtility
    {
        #region 公用參數設定
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion 公用參數設定

        public static Boolean doLogout(WindownApplication app)
        {
            app.logoff();
            return true;
        }

        //A0.07 Add Start
        public static Boolean doLogout(System.Windows.Forms.IWin32Window window, WindownApplication app)
        {
            string loginUserID = app.LoginUserID;
            Boolean hasLogout = false;
            var confirmResult = TipMessage_Request_Light.Show("Are you sure to log out now?");
            if (confirmResult != DialogResult.Yes)
            {
                hasLogout = false;
                return false;
            }
            else
            {
                app.logoff();
                hasLogout = true;
                TipMessage_Type_Light.Show("", "Logout success!", BCAppConstants.INFO_MSG);
            }
            return hasLogout;
        }
        //A0.07 Add End

        public static Boolean isLogin(WindownApplication app)
        {
            string loginUserID = app.LoginUserID;
            if (SCUtility.isEmpty(loginUserID))
            {
                return false;
            }
            return true;
        }

        public static Boolean doLogin(System.Windows.Forms.IWin32Window window, WindownApplication app, string function_code, bool isForceChack, LoginType loginType)//A0.01
        {
            string loginUserID = app.LoginUserID;
            Boolean hasAuth = false;
            if (!isForceChack && !SCUtility.isEmpty(loginUserID))
            {
                hasAuth = app.UserBLL.OperateDBUser.checkUserAuthority(loginUserID, function_code);
            }
            if (hasAuth)
            {
                return true;
            }
            if (!UASUtility.isLogin(app))
            {
                loginType = LoginType.LogIn;
            }
            else
            {
                loginType = LoginType.ExitCheck;
            }

            Form loginForm = null;
            switch (loginType)
            {
                case LoginType.LogIn:
                    loginForm = new LoginPopupForm(function_code, isForceChack ? false : UASUtility.isLogin(app));
                    break;
                case LoginType.ExitCheck:
                    loginForm = new ExitSystemPopupForm(function_code, isForceChack ? false : UASUtility.isLogin(app));
                    break;
                default:
                    //todo: Log
                    return false;
            }
            System.Windows.Forms.DialogResult result = loginForm.ShowDialog(window);
            loginUserID = (loginForm as ILoginInfo).getLoginUserID();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                loginForm.Dispose();
            }
            else
            {
                loginForm.Dispose();
                return false;
            }
            Boolean loginSuccess = false;
            string loginPassword = (loginForm as ILoginInfo).getLoginPassword();
            if (!SCUtility.isEmpty(loginUserID))
            {
                loginSuccess = app.UserBLL.OperateDBUser.checkUserPassword(loginUserID, loginPassword);
            }
            if (loginSuccess)
            {
                hasAuth = app.UserBLL.OperateDBUser.checkUserAuthority(loginUserID, function_code);
                if (hasAuth)
                {
                    app.login(loginUserID);
                }
            }
            return hasAuth;
        }


        public enum LoginType
        {
            LogIn,
            ExitCheck
        }

        /// <summary>
        /// A0.04
        /// </summary>
        /// <param name="app"></param>
        /// <param name="targetFormType"></param>
        public static void UpdateUIDisplayByAuthority(WindownApplication app, object targetFormType)
        {
            //PropertyDescriptorCollection properties =
            //TypeDescriptor.GetProperties(targetFormType);
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            MemberInfo[] memberInfos = targetFormType.GetType().GetMembers(flag);
            //MemberInfo[] memberInfos = typeof(BCMainForm).GetMembers(flag);

            var typeSwitch = new com.mirle.ibg3k0.ohxc.winform.OHxCMainForm.TypeSwitch()
                .Case((System.Windows.Forms.ToolStripMenuItem tsm, bool tf) => { tsm.Enabled = tf; })
                .Case((System.Windows.Forms.ComboBox cb, bool tf) => { cb.Enabled = tf; })
                //.Case((com.mirle.ibg3k0.bc.winform.UI.Components_New.UserControl_Button bt, bool tf) => { bt.Enabled = tf; })
                .Case((System.Windows.Forms.Label label, bool tf) => { label.Enabled = tf; })
                .Case((System.Windows.Forms.PictureBox px, bool tf) => { px.Enabled = tf; })
                //A0.05 .Case((System.Windows.Forms.Button btn, bool tf) => { btn.Enabled = tf; })
                .Case((CCWin.SkinControl.SkinButton btn, bool tf) => { btn.Enabled = tf; }) //A0.05
                .Case((UI.Controller.uc_btn_Custom btn, bool tf) => { btn.Visible = tf; }); //A0.09

            foreach (MemberInfo memberInfo in memberInfos)
            {
                Attribute AuthorityCheck = memberInfo.GetCustomAttribute(typeof(AuthorityCheck));
                if (AuthorityCheck != null)
                {
                    string attribute_FUNName = ((AuthorityCheck)AuthorityCheck).FUNCode;
                    //ToolStripMenuItem tsl = (ToolStripMenuItem)((FieldInfo)memberInfo).GetValue(this);
                    FieldInfo info = (FieldInfo)memberInfo;
                    if (app.UserBLL.OperateDBUser.checkUserAuthority(app.LoginUserID, attribute_FUNName))
                    {
                        typeSwitch.Switch(info.GetValue(targetFormType), true);
                    }
                    else
                    {
                        typeSwitch.Switch(info.GetValue(targetFormType), false);
                    }
                }
            }

        }

    }

}
