namespace MDR.Domain.Devices
{
    public class DeviceData : BaseEntity
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public double? Voltage { get; set; }
        public double? Resistance { get; set; }
        public double? LeakLocationPercent { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }

        public Guid DeviceId { get; set; }
        public Device Device { get; set; } = null!;

        public List<DeviceReflectogram> Reflectograms { get; set; } = [];
    }
}
