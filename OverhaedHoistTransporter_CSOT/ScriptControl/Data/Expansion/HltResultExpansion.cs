using Mirle.AK0.Hlt.Utils;
using Mirle.Protos.ReserveModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.Expansion
{
    public static class ResultExpansion
    {
        public static HltResult convert2HltResult(this Result hltResult)
        {
            return new HltResult()
            {
                OK = hltResult.Ok,
                VehicleID = string.IsNullOrWhiteSpace(hltResult.VehicleId) ? "" : hltResult.VehicleId,
                SectionID = string.IsNullOrWhiteSpace(hltResult.SectionId) ? "" : hltResult.SectionId,
                Description = string.IsNullOrWhiteSpace(hltResult.Description) ? "" : hltResult.Description
            };

        }
    }
}
