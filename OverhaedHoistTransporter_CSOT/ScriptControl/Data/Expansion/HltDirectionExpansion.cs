using Mirle.AK0.Hlt.Utils;
using Mirle.Protos.ReserveModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.Expansion
{
    public static class HltDirectionExpansion
    {
        public static Direction convert2Direction(this HltDirection hltDirection)
        {
            switch (hltDirection)
            {
                case HltDirection.None:
                    return Direction.None;
                case HltDirection.Forward:
                    return Direction.Forward;
                case HltDirection.Reverse:
                    return Direction.Reverse;
                case HltDirection.Left:
                    return Direction.Left;
                case HltDirection.Right:
                    return Direction.Right;
                case HltDirection.ForwardReverse:
                    return Direction.ForwardReverse;
                case HltDirection.LeftRight:
                    return Direction.LeftRight;
                case HltDirection.FRLR:
                    return Direction.Frlr;
                case HltDirection.North:
                    return Direction.North;
                case HltDirection.East:
                    return Direction.East;
                case HltDirection.South:
                    return Direction.South;
                case HltDirection.West:
                    return Direction.West;
                case HltDirection.NorthSouth:
                    return Direction.NorthSouth;
                case HltDirection.EastWest:
                    return Direction.EastWest;
                case HltDirection.NESW:
                    return Direction.Nesw;
                default:
                    throw new Exception($"gRPC的HltDirection列舉:{hltDirection}，並無對應到使用的列舉");
            }

        }
    }
}