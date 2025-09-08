using MDR.Application.Contracts;
using MDR.Domain.Devices;
using Microsoft.EntityFrameworkCore;

namespace MDR.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceData> DeviceData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasMany(m => m.DeviceDatas)
                .WithOne(c => c.Device)
                .HasForeignKey(m => m.DeviceId);

            modelBuilder.Entity<Device>()
                .HasMany(m => m.DeviceDatas)
                .WithOne(c => c.Device)
                .HasForeignKey(m => m.DeviceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
