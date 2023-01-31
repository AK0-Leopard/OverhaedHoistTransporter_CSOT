using com.mirle.ibg3k0.sc.App;
using com.mirle.ibg3k0.sc.Common;
using System;
using System.Text;
using System.Linq;
using Mirle.AK0.Hlt.Utils;
using Mirle.AK0.Hlt.ReserveSection.Map.ViewModels;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using com.mirle.ibg3k0.sc.Common.Interface;

namespace com.mirle.ibg3k0.sc.BLL
{
    public class ReserveBLL
    {
        public event EventHandler<IReserveModule> ReserveMoudleChange;

        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private MapViewModel mapAPI { get; set; }
        SCApplication scApp { get; set; }
        private IReserveModule localReserveModule { get; set; }
        private IReserveModule remoteReserveModule { get; set; }
        ALINE line;
        private EventHandler reserveStatusChange;
        private object _reserveStatusChangeEventLock = new object();
        public event EventHandler ReserveStatusChange
        {
            add
            {
                lock (_reserveStatusChangeEventLock)
                {
                    reserveStatusChange -= value;
                    reserveStatusChange += value;
                }
            }
            remove
            {
                lock (_reserveStatusChangeEventLock)
                {
                    reserveStatusChange -= value;
                }
            }
        }

        private void onReserveStatusChange()
        {
            reserveStatusChange?.Invoke(this, EventArgs.Empty);
        }

        public ReserveBLL()
        {
        }
        public void start(SCApplication _app)
        {
            scApp = _app;
            mapAPI = _app.getReserveSectionAPI();
            localReserveModule = _app.localReserveModule;
            remoteReserveModule = _app.remoteReserveModule;
        }
        private IReserveModule CurrentUsingReserveModule = null;
        private IReserveModule GetUsingReserveModule()
        {

            if (SystemParameter.IsUsingRemoteReserveModule)
            {
                if (scApp.getEQObjCacheManager().getLine().IsConnectionWithReserveModule)
                {
                    setCurrentUsingReserveModule(remoteReserveModule);
                    return remoteReserveModule;
                }
                else
                {
                    setCurrentUsingReserveModule(localReserveModule);
                    return localReserveModule;
                }
            }
            else
            {
                setCurrentUsingReserveModule(localReserveModule);
                return localReserveModule;
            }
        }
        object lock_obj_set_reserve_module = new object();
        private void setCurrentUsingReserveModule(IReserveModule reserveModule)
        {
            if (CurrentUsingReserveModule != reserveModule)
            {
                lock (lock_obj_set_reserve_module)
                {
                    if (CurrentUsingReserveModule != reserveModule)
                    {
                        CurrentUsingReserveModule = reserveModule;
                        OnReserveMoudleChange(reserveModule);
                    }
                }
            }
        }

        private void OnReserveMoudleChange(IReserveModule reserveModule)
        {
            ReserveMoudleChange?.Invoke(this, reserveModule);
        }


        //public bool DrawAllReserveSectionInfo()
        //{
        //    bool is_success = false;
        //    try
        //    {
        //        mapAPI.RedrawBitmap(false);
        //        mapAPI.DrawOverlapRR();
        //        mapAPI.RefreshBitmap();
        //        mapAPI.ClearOverlapRR();
        //        is_success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Exception");
        //        is_success = false;
        //    }
        //    return is_success;
        //}

        public System.Windows.Media.Imaging.BitmapSource GetCurrentReserveInfoMap()
        {
            //return mapAPI.MapBitmapSource;
            return GetUsingReserveModule().GetCurrentReserveInfoMap();
        }

        public byte[] GetCurrentReserveInfoMapByte()
        {
            //DrawAllReserveSectionInfo();
            //return BitmapSourceToByte(mapAPI.MapBitmapSource);
            return BitmapSourceToByte(GetUsingReserveModule().GetCurrentReserveInfoMap());
        }
        private byte[] BitmapSourceToByte(System.Windows.Media.Imaging.BitmapSource source)
        {
            try
            {
                var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);
                encoder.Frames.Add(frame);
                var stream = new System.IO.MemoryStream();
                encoder.Save(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }

        public virtual (double x, double y, bool isTR50) GetHltMapAddress(string adrID)
        {
            var adr_obj = mapAPI.GetAddressObjectByID(adrID);
            return (adr_obj.X, adr_obj.Y, adr_obj.IsTR50);
        }
        public virtual HltResult TryAddVehicleOrUpdate(string vhID, string currentSectionID, double vehicleX, double vehicleY, float vehicleAngle, double speedMmPerSecond,
                                                       HltDirection sensorDir, HltDirection forkDir)
        {
            LogHelper.Log(logger: logger, LogLevel: NLog.LogLevel.Debug, Class: nameof(ReserveBLL), Device: "AGV",
               Data: $"add vh in reserve system: vh:{vhID},x:{vehicleX},y:{vehicleY},angle:{vehicleAngle},speedMmPerSecond:{speedMmPerSecond},sensorDir:{sensorDir},forkDir:{forkDir}",
               VehicleID: vhID);
            var hlt_vh = new HltVehicle(vhID, vehicleX, vehicleY, vehicleAngle, speedMmPerSecond, sensorDirection: sensorDir, forkDirection: forkDir, currentSectionID: currentSectionID);
            //HltResult result = mapAPI.TryAddOrUpdateVehicle(hlt_vh, isKeepRestSection: false);
            HltResult result = GetUsingReserveModule().TryAddOrUpdateVehicle(vhID, currentSectionID, vehicleX, vehicleY, vehicleAngle, speedMmPerSecond, sensorDir, forkDir);
            onReserveStatusChange();

            return result;
        }

        public virtual void RemoveManyReservedSectionsByVIDSID(string vhID, string sectionID)
        {
            string sec_id = SCUtility.Trim(sectionID);
            //mapAPI.RemoveManyReservedSectionsByVIDSID(vhID, sec_id);
            GetUsingReserveModule().RemoveManyReservedSectionsByVIDSID(vhID, sec_id);
            onReserveStatusChange();
        }

        public virtual void RemoveVehicle(string vhID)
        {
            //var vh = mapAPI.GetVehicleObjectByID(vhID);
            //if (vh != null)
            //{
            //    mapAPI.RemoveVehicle(vh);
            //}
            GetUsingReserveModule().RemoveVehicle(vhID);
        }

        public virtual string GetCurrentReserveSection()
        {
            StringBuilder sb = new StringBuilder();
            //var current_reserve_sections = mapAPI.HltReservedSections;
            var current_reserve_sections = GetUsingReserveModule().GetCurrentReserveSections("");
            sb.AppendLine("Current reserve section");
            foreach (var reserve_section in current_reserve_sections)
            {
                sb.AppendLine($"section id:{reserve_section.SectionID} vh id:{reserve_section.VehicleID}");
            }
            return sb.ToString();
        }


        public virtual HltResult TryAddReservedSection(string vhID, string sectionID,
            HltDirection sensorDir = HltDirection.ForwardReverse, HltDirection forkDir = HltDirection.None,
            bool isAsk = false)
        {
            string sec_id = SCUtility.Trim(sectionID);
            //HltResult result = mapAPI.TryAddReservedSection(vhID, sec_id, sensorDir, forkDir, vehicle_direction, isAsk);
            HltResult result = GetUsingReserveModule().TryAddReservedSection(vhID, sec_id, sensorDir, forkDir, isAsk);
            LogHelper.Log(logger: logger, LogLevel: NLog.LogLevel.Info, Class: nameof(ReserveBLL), Device: "AGV",
               Data: $"vh:{vhID} Try add reserve section:{sectionID} dir:{sensorDir},result:{result}",
               VehicleID: vhID);
            onReserveStatusChange();

            return result;
        }


        public virtual void RemoveAllReservedSectionsByVehicleID(string vhID)
        {
            //mapAPI.RemoveAllReservedSectionsByVehicleID(vhID);
            GetUsingReserveModule().RemoveAllReservedSectionsByVehicleID(vhID);
            onReserveStatusChange();
        }
        public virtual void RemoveAllReservedSections()
        {
            //mapAPI.RemoveAllReservedSections();
            GetUsingReserveModule().RemoveAllReservedSections();
            onReserveStatusChange();
        }

    }

}