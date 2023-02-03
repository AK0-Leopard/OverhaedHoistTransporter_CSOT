using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using Mirle.AK0.Hlt.Utils;
using Mirle.Protos.ReserveModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.Expansion
{
    public static class E_CMD_TYPEExpansion
    {
        public static ActiveType convert2ActiveType(this E_CMD_TYPE hltResult)
        {
            switch (hltResult)
            {
                case E_CMD_TYPE.Load://
                    return ActiveType.Load;
                case E_CMD_TYPE.LoadUnload://
                    return ActiveType.Loadunload;
                case E_CMD_TYPE.Unload://
                    return ActiveType.Unload;
                case E_CMD_TYPE.MoveToMTL://
                    return ActiveType.Movetomtl;
                case E_CMD_TYPE.SystemOut://
                    return ActiveType.Systemout;
                case E_CMD_TYPE.SystemIn://
                    return ActiveType.Systemin;
                case E_CMD_TYPE.MTLHome://
                    return ActiveType.Mtlhome;
                case E_CMD_TYPE.Move://
                case E_CMD_TYPE.Move_Park://
                    return ActiveType.Move;
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
