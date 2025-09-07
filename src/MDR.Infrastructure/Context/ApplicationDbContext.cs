using MDR.Application.Contracts;
using MDR.Domain.Devices.Mases2;
using MDR.Domain.Devices.Mouses2;
using MDR.Domain.Devices.Mouses2B;
using MDR.Domain.Devices.MousesCombo;
using Microsoft.EntityFrameworkCore;

namespace MDR.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Mouse2> Mouse2 { get; set; }
        public DbSet<Mouse2Data> Mouse2Data { get; set; }
        public DbSet<Mouse2B> Mouse2B { get; set; }
        public DbSet<Mouse2BData> Mouse2BData { get; set; }
        public DbSet<MouseCombo> MouseCombo{ get; set; }
        public DbSet<MouseComboData> MouseComboData { get; set; }
        public DbSet<Mas2> Mas2{ get; set; }
        public DbSet<Mas2Data> Mas2Data { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mouse2>()
                .HasMany(m => m.DeviceDatas)
                .WithOne(c => c.Device)
                .HasForeignKey(m => m.DeviceId);

            modelBuilder.Entity<Mouse2B>()
                .HasMany(m => m.DeviceDatas)
                .WithOne(c => c.Device)
                .HasForeignKey(m => m.DeviceId);

            modelBuilder.Entity<MouseCombo>()
                .HasMany(m => m.DeviceDatas)
                .WithOne(c => c.Device)
                .HasForeignKey(m => m.DeviceId);

            modelBuilder.Entity<MouseComboData>()
                .HasMany(m => m.Reflectograms)
                .WithOne(m => m.MouseComboData)
                .HasForeignKey(m => m.MouseComboDataId);

            modelBuilder.Entity<Mas2>()
                .HasMany(m => m.DeviceDatas)
                .WithOne(c => c.Device)
                .HasForeignKey(m => m.DeviceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
