// ***********************************************************************
// Assembly         : ScriptControl
// Author           : 
// Created          : 03-31-2016
//
// Last Modified By : 
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="AlarmMap.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Data.VO
{
    /// <summary>
    /// Class AlarmMap.
    /// </summary>
    public class PortGroupInfo
    {
        public String GROUP_ID;
        public int MAX_COUNT;
        List<string> PortIDs = new List<string>();

        public void setGroupPortIDs(List<PortGroupMap> groupPortMaps)
        {
            PortIDs = groupPortMaps.
                      Where(map => sc.Common.SCUtility.isMatche(GROUP_ID, map.GROUP_ID)).
                      Select(map => map.PORT_ID).ToList();
        }

        public bool IsInclude(string portID)
        {
            if (sc.Common.SCUtility.isEmpty(portID))
            {
                return false;
            }
            portID = sc.Common.SCUtility.Trim(portID, true);
            return PortIDs.Contains(portID);
        }
    }
}
