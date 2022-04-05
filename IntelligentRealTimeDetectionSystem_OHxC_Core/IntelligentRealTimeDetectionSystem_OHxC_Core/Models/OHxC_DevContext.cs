using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace com.mirle.iibg3k0.ids.ohxc.Models
{
    public partial class OHxC_DevContext : DbContext
    {


        public OHxC_DevContext()
        {
        }

        public OHxC_DevContext(DbContextOptions<OHxC_DevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ABLOCKZONEDETAIL> ABLOCKZONEDETAIL { get; set; }
        public virtual DbSet<ABLOCKZONEMASTER> ABLOCKZONEMASTER { get; set; }
        public virtual DbSet<ASECTION> ASECTION { get; set; }
        public virtual DbSet<ASEGMENT> ASEGMENT { get; set; }
        //public virtual DbSet<AVEHICLE> AVEHICLE { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=db.ohxc.mirle.com.tw\\SQLEXPRESS,1433;Database=OHSC_Dev;User ID=sa;Password=p@ssw0rd;Trusted_Connection=False;");
            }
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

            //    entity.Property(e => e.CST_ID)
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CUR_ADR_ID)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CUR_SEC_ID)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

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
