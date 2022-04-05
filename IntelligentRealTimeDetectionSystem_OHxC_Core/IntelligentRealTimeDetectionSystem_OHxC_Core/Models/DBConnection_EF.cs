using com.mirle.iibg3k0.ids.ohxc.Models;
using com.mirle.iibg3k0.ids.ProtocolFormat.OHTMessage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using static com.mirle.iibg3k0.ids.ohxc.App.CSAppConstants;

namespace com.mirle.iibg3k0.ids.ohxc
{
    public class DBConnection_EF : OHxC_DevContext, IDisposable
    {
        private DBConnection_EF(DbContextOptions<OHxC_DevContext> options) : base(options)
        {

        }


        private static string _connectionString;



        public static DBConnection_EF GetUContext()
        {
            return CreateDbContext(null);
        }

        private static DBConnection_EF CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<OHxC_DevContext>();
            builder.UseSqlServer(_connectionString);

            return new DBConnection_EF(builder.Options);
        }

        private static void LoadConnectionString()
        {

            dynamic type = (new IntelligentRealTimeDetectionSystem_OHxC_Core.Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);

            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString(App.CSApplication.OhxC_ID);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ABLOCKZONEDETAIL>(entity =>
            {
                entity.HasKey(e => new { e.SEC_ID, e.ENTRY_SEC_ID });

                entity.Property(e => e.SEC_ID).HasMaxLength(5);

                entity.Property(e => e.ENTRY_SEC_ID).HasMaxLength(5);
            });

            modelBuilder.Entity<ABLOCKZONEMASTER>(entity =>
            {
                entity.HasKey(e => e.ENTRY_SEC_ID);

                entity.Property(e => e.ENTRY_SEC_ID)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LEAVE_ADR_ID_1)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LEAVE_ADR_ID_2)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ASECTION>(entity =>
            {
                entity.HasKey(e => new { e.SEC_ID, e.SUB_VER });

                entity.Property(e => e.SEC_ID)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SUB_VER)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ADD_TIME).HasColumnType("datetime");

                entity.Property(e => e.ADD_USER)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CHG_SEG_NUM_1)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CHG_SEG_NUM_2)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FROM_ADR_ID)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SEG_NUM)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TO_ADR_ID)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UPD_TIME).HasColumnType("datetime");

                entity.Property(e => e.UPD_USER)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ASEGMENT>(entity =>
            {
                entity.HasKey(e => e.SEG_NUM);

                entity.Property(e => e.SEG_NUM)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.STATUS)
                      .HasConversion
                      (v => (int)v,
                       v => (E_SEG_STATUS)v);


                entity.Property(e => e.DISABLE_TIME).HasColumnType("datetime");

                entity.Property(e => e.NOTE).HasMaxLength(40);

                entity.Property(e => e.PRE_DISABLE_TIME).HasColumnType("datetime");

                entity.Property(e => e.RESERVE_FIELD)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            //modelBuilder.Entity<AVEHICLE>(entity =>
            //{
            //    entity.HasKey(e => e.VEHICLE_ID);

            //    entity.Property(e => e.VEHICLE_ID)
            //        .HasMaxLength(5)
            //        .IsUnicode(false)
            //        .ValueGeneratedNever();
            //    entity.Property(e => e.VEHICLE_TYPE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (E_VH_TYPE)v);


            //    entity.Property(e => e.CST_ID)
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CUR_ADR_ID)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CUR_SEC_ID)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

            //    entity.Property(e => e.MODE_STATUS)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VHModeStatus)v);

            //    entity.Property(e => e.ACT_STATUS)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VHActionStatus)v);

            //    entity.Property(e => e.BLOCK_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.CMD_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.OBS_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.HID_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.ERROR)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.EARTHQUAKE_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.SAFETY_DOOR_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.OHXC_OBS_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);
            //    entity.Property(e => e.OHXC_BLOCK_PAUSE)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhStopSingle)v);


            //    entity.Property(e => e.HAS_CST)
            //          .HasConversion
            //          (v => (int)v,
            //           v => (VhLoadCSTStatus)v);


            //    entity.Property(e => e.CYCLERUN_ID)
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CYCLERUN_TIME).HasColumnType("datetime");

            //    entity.Property(e => e.GRIP_MANT_DATE).HasColumnType("datetime");

            //    entity.Property(e => e.MANT_DATE).HasColumnType("datetime");

            //    entity.Property(e => e.MCS_CMD)
            //        .HasMaxLength(64)
            //        .IsUnicode(false);

            //    entity.Property(e => e.NODE_ADR)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

            //    entity.Property(e => e.OHTC_CMD)
            //        .HasMaxLength(64)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PARK_ADR_ID)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PARK_TIME).HasColumnType("datetime");

            //    entity.Property(e => e.SEC_ENTRY_TIME).HasColumnType("datetime");

            //    entity.Property(e => e.UPD_TIME).HasColumnType("datetime");
            //});
        }
    }
}
