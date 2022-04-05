using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ids.ohxc.Data.Dao
{
    public class BlockZoneDetailDao
    {
        public Dictionary<string, List<string>> loadBlockZoneByEntrySecDetail(List<ibg3k0.sc.ABLOCKZONEDETAIL>  block_zone_details)
        {
            var query = from block in block_zone_details
                        group block by block.SEC_ID into grp
                        select grp;
            return query.ToDictionary(grp => grp.Key.Trim(), grp => grp.Select(blockDetail => blockDetail.ENTRY_SEC_ID.Trim()).ToList());
        }
    }
}
