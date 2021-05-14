using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc.Data.ValueDefMapAction;
using com.mirle.ibg3k0.sc.Data.VO.Interface;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.VO
{
    public class BufferDevice : AEQPT
    {
        public List<APORTSTATION> loadPortStations(sc.BLL.PortStationBLL portStationBLL)
        {
            var port_stations = portStationBLL.OperateCatch.getPortStationByEqID(this.EQPT_ID);
            return port_stations;
        }
    }
}
