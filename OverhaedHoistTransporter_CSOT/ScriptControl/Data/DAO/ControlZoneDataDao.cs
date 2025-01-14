// ***********************************************************************
// Assembly         : ScriptControl
// Author           : 
// Created          : 03-31-2016
//
// Last Modified By : 
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="AlarmMapDao.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mirle.ibg3k0.bcf.Data;
using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Data.VO;
using NLog;
using com.mirle.ibg3k0.bcf.Common;
using System.Drawing;
using com.mirle.ibg3k0.sc.Common;

namespace com.mirle.ibg3k0.sc.Data.DAO
{
    /// <summary>
    /// Class AlarmMapDao.
    /// </summary>
    /// <seealso cref="com.mirle.ibg3k0.bcf.Data.DaoBase" />
    public class ControlZoneDataDao : DaoBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<ControlZoneData> loadControlZoneDatas(SCApplication scApp)
        {
            try
            {
                DataTable dt = scApp.OHxCConfig.Tables["CONTROLZONEINFO"];
                var query = from c in dt.AsEnumerable()
                            select new ControlZoneData
                            {
                                ID = c.Field<string>("ID"),
                                Points = GetScope(c.Field<string>("AXIS_GROUP")),
                                BlockSectionIDs = GetBlockIDs(c.Field<string>("BLOCKIDS")),
                                VhLimitCount = stringToInt(c.Field<string>("VHLIMITCOUNT"))
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
        List<Point> GetScope(string sAsixGroup)
        {
            List<Point> points = new List<Point>();
            var asix_array = sAsixGroup.Split('#');
            foreach (var asix in asix_array)
            {
                points.Add(StringToPoint(asix));
            }
            return points;
        }
        Point StringToPoint(string s)
        {
            try
            {
                s = s.Replace("(", "").Replace(")", "");
                string[] parts = s.Split(',');
                if (parts.Length != 2)
                {
                    throw new FormatException("Input string is not in the correct format.");
                }

                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                return new Point(x, y);
            }
            catch (Exception ex)
            {
                throw new FormatException("Failed to convert string to Point.", ex);
            }
        }

        int stringToInt(string value)
        {
            int i_value = 0;
            int.TryParse(value, out i_value);
            return i_value;
        }

        private List<string> GetBlockIDs(string blockIDs)
        {
            if (SCUtility.isEmpty(blockIDs))
            {
                throw new Exception($"try parse control zone info, blockIDs is empty.");
            }
            if (!blockIDs.Contains("-"))
            {
                return new List<string>() { blockIDs };
            }
            else
            {
                return blockIDs.Split('-').ToList();
            }
        }
        private List<Point> GetPoints(string x1, string y1,
                                      string x2, string y2,
                                      string x3, string y3,
                                      string x4, string y4)
        {
            if (!int.TryParse(x1, out int x1_int))
            {
                throw new Exception($"try parse control zone info, x1:{x1} can't parse to int.");
            }
            if (!int.TryParse(y1, out int y1_int))
            {
                throw new Exception($"try parse control zone info, y1:{y1} can't parse to int.");
            }
            if (!int.TryParse(x2, out int x2_int))
            {
                throw new Exception($"try parse control zone info, x2:{x2} can't parse to int.");
            }
            if (!int.TryParse(y2, out int y2_int))
            {
                throw new Exception($"try parse control zone info, y2:{y2} can't parse to int.");
            }
            if (!int.TryParse(x3, out int x3_int))
            {
                throw new Exception($"try parse control zone info, x3:{x3} can't parse to int.");
            }
            if (!int.TryParse(y3, out int y3_int))
            {
                throw new Exception($"try parse control zone info, y3:{y3} can't parse to int.");
            }
            if (!int.TryParse(x4, out int x4_int))
            {
                throw new Exception($"try parse control zone info, x4:{x4} can't parse to int.");
            }
            if (!int.TryParse(y4, out int y4_int))
            {
                throw new Exception($"try parse control zone info, y4:{y4} can't parse to int.");
            }

            return new List<Point>()
            {
                new Point(x1_int, y1_int),
                new Point(x2_int, y2_int),
                new Point(x3_int, y3_int),
                new Point(x4_int, y4_int)
            };

        }

    }
}
