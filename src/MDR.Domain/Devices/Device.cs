namespace MDR.Domain.Devices
{
    public class Device : BaseAggregateNameEnabled
    {
        public DeviceType DeviceType { get; set; }

        public Guid DeviceConfigId { get; set; }
        public DeviceConfig DeviceConfig { get; set; } = null!;
        public ICollection<DeviceData> DeviceDatas { get; set; } = [];
    }
}
