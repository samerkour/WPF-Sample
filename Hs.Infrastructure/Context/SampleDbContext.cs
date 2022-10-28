using System;
using System.Collections.Generic;
using Hs.Domain.Entities.SampleDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hs.Infrastructure.Context
{


    public partial class SampleDbContext : DbContext
    {
        public SampleDbContext()
        {
        }

        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<DateTimePoint> DateTimePoints { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-53A5H27\\LOCALSERVER;Initial Catalog=SampleDb;user id=sa;password=Admin@123");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Model).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DateTimePoint>(entity =>
            {
                entity.ToTable("DateTimePoint");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Timestamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.ClientAgent).HasMaxLength(512);

                entity.Property(e => e.ClientIp).HasMaxLength(128);

                entity.Property(e => e.EnvironmentUserName).HasMaxLength(128);

                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.MachineName).HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
