using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common.Interface;
using com.mirle.ibg3k0.sc.Data.VO;
using Mirle.AK0.Hlt.ReserveSection.Map.ViewModels;
using Mirle.AK0.Hlt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.sc.Common
{
    public class ReserveModule : IReserveModule
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private MapViewModel mapAPI { get; set; }

        public ReserveModule(SCApplication _scApp)
        {
        }
        #region TryAddReservedSection
        public HltResult TryAddReservedSection(string vhID, string sectionID, HltDirection sensorDir, HltDirection forkDir, bool isAsk = false)
        {
            try
            {
                var result = mapAPI.TryAddReservedSection(vhID, sectionID, sensorDir, forkDir, isAskOnly: isAsk);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return new HltResult() { OK = false };
            }
        }
        #endregion TryAddReservedSection
        #region GetCurrentReserveInfoMap
        public System.Windows.Media.Imaging.BitmapSource GetCurrentReserveInfoMap()
        {
            try
            {
                DrawAllReserveSectionInfo();
                return mapAPI.MapBitmapSource;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return null;
            }
        }
        private bool DrawAllReserveSectionInfo()
        {
            bool is_success = false;
            try
            {
                mapAPI.RedrawBitmap(false);
                mapAPI.DrawOverlapRR();
                mapAPI.RefreshBitmap();
                mapAPI.ClearOverlapRR();
                is_success = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                is_success = false;
            }
            return is_success;
        }
        #endregion GetCurrentReserveInfoMap
        #region TryAddOrUpdateVehicle
        public HltResult TryAddOrUpdateVehicle(string vhID, string currentSectionID, double vehicleX, double vehicleY, float vehicleAngle, double speedMmPerSecond,
                                               HltDirection sensorDir, HltDirection forkDir)
        {
            try
            {
                var hlt_vh = GetHltVehicle(vhID, currentSectionID, vehicleX, vehicleY, vehicleAngle, speedMmPerSecond, sensorDir, forkDir);
                var ask = mapAPI.TryAddOrUpdateVehicle(hlt_vh);
                return ask;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return new HltResult() { OK = false };
            }
        }
        private HltVehicle GetHltVehicle(string vhID, string currentSectionID, double vehicleX, double vehicleY, float vehicleAngle, double speedMmPerSecond,
                                         HltDirection sensorDir, HltDirection forkDir)
        {
            var hlt_vh = new HltVehicle(vhID, vehicleX, vehicleY,
                             angle: vehicleAngle, speedMmPerSecond: speedMmPerSecond,
                             sensorDirection: sensorDir,
                             forkDirection: forkDir);
            return hlt_vh;
        }

        #endregion TryAddOrUpdateVehicle
        #region RemoveManyReservedSectionsByVIDSID
        public void RemoveManyReservedSectionsByVIDSID(string vhID, string sectionID)
        {
            try
            {
                mapAPI.RemoveManyReservedSectionsByVIDSID(vhID, sectionID);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        #endregion RemoveManyReservedSectionsByVIDSID
        #region RemoveVehicle
        public void RemoveVehicle(string vhID)
        {
            try
            {
                var vh = mapAPI.GetVehicleObjectByID(vhID);
                if (vh != null)
                {
                    mapAPI.RemoveVehicle(vh);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        #endregion RemoveVehicle
        #region GetCurrentReserveSection
        public List<ReservedSection> GetCurrentReserveSections(string vhID)
        {
            try
            {
                List<ReservedSection> reserved_sections = new List<ReservedSection>();
                var current_hltreserve_sections = mapAPI.HltReservedSections;
                string vh_id = vhID;
                if (string.IsNullOrWhiteSpace(vh_id))
                {
                    reserved_sections = current_hltreserve_sections.
                                      Select(reserve_info => new ReservedSection(reserve_info.RSVehicleID, reserve_info.RSMapSectionID)).
                                      ToList();
                }
                else
                {
                    reserved_sections = current_hltreserve_sections.
                                        Where(reserve_info => reserve_info.RSVehicleID == vh_id).
                                        Select(reserve_info => new ReservedSection(reserve_info.RSVehicleID, reserve_info.RSMapSectionID)).
                                        ToList();
                }
                return reserved_sections;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                return new List<ReservedSection>();
            }
        }
        #endregion GetCurrentReserveSection
        #region RemoveAllReservedSectionsByVehicleID
        public void RemoveAllReservedSectionsByVehicleID(string vhID)
        {
            try
            {
                mapAPI.RemoveAllReservedSectionsByVehicleID(vhID);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }
        #endregion RemoveAllReservedSectionsByVehicleID
        #region RemoveAllReservedSections
        public void RemoveAllReservedSections()
        {
            try
            {
                mapAPI.RemoveAllReservedSections();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
            }
        }

        public void Start(SCApplication _scApp)
        {
            mapAPI = _scApp.getReserveSectionAPI();

        }
        #endregion RemoveAllReservedSections

    }
}
