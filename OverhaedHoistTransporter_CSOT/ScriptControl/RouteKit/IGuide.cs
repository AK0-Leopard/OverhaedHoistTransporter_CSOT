using com.mirle.ibg3k0.sc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteKit
{
    public interface IGuide
    {
        string[] DownstreamSearchSection(string startAdr, string endAdr, int flag, bool isIgnoreStatus = false);
        string[] DownstreamSearchSection_FromSecToSec(string fromSec, string toSec, int flag, bool isIncludeLastSec, bool isIgnoreStatus = false);
        string[] DownstreamSearchSection_FromSecToAdr(string fromSec, string toAdr, int flag, bool isIgnoreStatus = false);
        bool checkRoadIsWalkable(string from_adr, string to_adr);
        bool checkRoadIsWalkable(string from_adr, string to_adr, bool isMaintainDeviceCommand);
        bool checkRoadIsWalkable(string from_adr, string to_adr, out KeyValuePair<string[], double> route_distance);
        bool checkRoadIsWalkable(string from_adr, string to_adr, bool isMaintainDeviceCommand, out KeyValuePair<string[], double> route_distance);
        bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr);
        bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr, bool isMaintainDeviceCommand);
        bool checkRoadIsWalkableForMCSCommand(string from_adr, string to_adr, bool isMaintainDeviceCommand, out KeyValuePair<string[], double> route_distance);

        ASEGMENT CloseSegment(string strSegCode);
        ASEGMENT CloseSegment(string strSegCode, ASEGMENT.DisableType disableType);
        ASEGMENT OpenSegment(string strSegCode);
        ASEGMENT OpenSegment(string strSegCode, ASEGMENT.DisableType disableType);

        int[] getCatchSectionCount();

    }

}
