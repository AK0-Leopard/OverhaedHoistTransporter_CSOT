using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.Dao.MirleOHS_100
{
    public class SectionControlDao
    {
        public void Update(DBConnection_EF con, ASECTION_CONTROL_100 vh)
        {
            con.SaveChanges();
        }
    }
}
