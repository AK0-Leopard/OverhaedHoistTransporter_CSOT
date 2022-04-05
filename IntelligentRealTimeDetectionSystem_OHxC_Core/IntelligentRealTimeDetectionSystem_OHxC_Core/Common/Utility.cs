using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ids.ohxc.Common
{
    public class Utility
    {

        /// <summary>
        /// 比較兩個字串內容是否相同
        /// </summary>
        /// <param name="str1">The STR1.</param>
        /// <param name="str2">The STR2.</param>
        /// <returns>Boolean.</returns>
        public static Boolean isMatche(String str1, String str2)
        {
            try
            {
                if (str1 == str2) return true;
                if (str1 == null || str2 == null) return false;
                return str1.Trim().Equals(str2.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Warn(e, "Exception");
            }
            return false;
        }
    }
}
