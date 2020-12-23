using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.bcf.Data.ValueDefMapAction;
using com.mirle.ibg3k0.bcf.Data.VO;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using com.mirle.ibg3k0.sc.Data.SECS;
using com.mirle.ibg3k0.sc.Data.VO;
using com.mirle.ibg3k0.sc.Data.VO.Interface;
using com.mirle.ibg3k0.sc.ObjectRelay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc
{
    public partial class APARKZONEMASTER
    {
        public List<APARKZONEDETAIL> ParkDetails;
        public void setParkDetails(List<APARKZONEDETAIL> allParkZoneDetail)
        {
            ParkDetails = allParkZoneDetail.Where(detail => SCUtility.isMatche(detail.PARK_ZONE_ID, PARK_ZONE_ID)).
                                            ToList();
        }
    }
}
