//*********************************************************************************
//      User.cs
//*********************************************************************************
// File Name: User.cs
// Description: User類別
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
// 2016/08/11    Steven Hong    N/A            A0.01   Add Badge Number
// 2016/12/06    Eric Chiang     N/A            A0.02   Add User Group
// 2017/07/04    Harris Kuo       N/A            A0.03   Add Department 
//**********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc.App;

namespace com.mirle.ibg3k0.sc.Data.VO
{
    public class User
    {
        public virtual string User_ID { get; set; }
        public string sUser_ID
        {
            get { return User_ID.Trim(); }
        }

        public virtual string Passwd { get; set; }

        public virtual string Badge_Number { get; set; }  //A0.01

        public virtual string User_Name { get; set; }
        public string sUser_Name
        {
            get { return User_Name.Trim(); }
        }

        public virtual string Disable_Flg { get; set; }

        public virtual string Power_User_Flg { get; set; }

        public virtual string Admin_Flg { get; set; }

        public virtual string User_Grp { get; set; }  //A0.02
        public string sUser_Grp
        {
            get { return User_Grp.Trim(); }
        }

        public virtual string Department { get; set; }  //A0.03

        public virtual Boolean isDisable()
        {
            if (BCFUtility.isMatche(Disable_Flg, SCAppConstants.YES_FLAG))
            {
                return true;
            }
            return false;
        }

        public virtual Boolean isAdmin()
        {
            if (BCFUtility.isMatche(Admin_Flg, SCAppConstants.YES_FLAG))
            {
                return true;
            }
            return false;
        }

        public virtual Boolean isPowerUser()
        {
            if (BCFUtility.isMatche(Power_User_Flg, SCAppConstants.YES_FLAG))
            {
                return true;
            }
            return isAdmin();
        }
    }
}
