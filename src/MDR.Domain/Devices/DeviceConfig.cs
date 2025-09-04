namespace MDR.Domain.Devices
{
    public class DeviceConfig : BaseEntityEnabled
    {
        public double? AlarmThreshold { get; set; }
        public double? CableLength { get; set; }
        public double? TemperatureAlarmThreshold { get; set; }
        public double? HumidityAlarmThreshold { get; set; }

        public Guid DeviceId { get; set; }
        public Device Device { get; set; } = null!;
    }
}
