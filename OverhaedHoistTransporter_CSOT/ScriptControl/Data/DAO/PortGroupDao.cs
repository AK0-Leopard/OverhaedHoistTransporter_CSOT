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

namespace com.mirle.ibg3k0.sc.Data.DAO
{
    /// <summary>
    /// Class AlarmMapDao.
    /// </summary>
    /// <seealso cref="com.mirle.ibg3k0.bcf.Data.DaoBase" />
    public class PortGroupDao : DaoBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<PortGroupMap> loadPortGroupMap(SCApplication scApp)
        {
            try
            {
                DataTable dt = scApp.OHxCConfig.Tables["PORTGROUPMAP"];
                var query = from c in dt.AsEnumerable()
                            select new PortGroupMap
                            {
                                PORT_ID = c.Field<string>("PORT_ID"),
                                ADR_ID = c.Field<string>("ADR_ID"),
                                GROUP_ID = c.Field<string>("GROUP_ID")
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        public List<PortGroupInfo> loadPortGroupInfo(SCApplication scApp)
        {
            try
            {
                DataTable dt = scApp.OHxCConfig.Tables["PORTGROUPINFO"];
                var query = from c in dt.AsEnumerable()
                            select new PortGroupInfo
                            {
                                GROUP_ID = c.Field<string>("GROUP_ID"),
                                MAX_COUNT = int.Parse(c.Field<string>("MAX_COUNT"))
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

    }
}
