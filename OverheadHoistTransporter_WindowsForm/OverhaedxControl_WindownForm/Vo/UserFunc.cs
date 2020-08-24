using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mirle.ibg3k0.bcf.Common;

namespace com.mirle.ibg3k0.sc.Data.VO
{
    public class UserFunc
    {
        //public virtual string User_ID { get; set; }
        //public virtual string Func_Code { get; set; }
        public UserFunc()
        {
            UserFuncPK = new UserFuncPKInfo();
        }
        public virtual UserFuncPKInfo UserFuncPK { get; set; }
    }

    public class UserFuncPKInfo
    {
        public virtual string User_GRP { get; set; }
        public virtual string Func_Code { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is UserFuncPKInfo)
            {
                UserFuncPKInfo pk = obj as UserFuncPKInfo;
                if (BCFUtility.isMatche(this.User_GRP, pk.User_GRP)
                    && BCFUtility.isMatche(this.Func_Code, pk.Func_Code))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
