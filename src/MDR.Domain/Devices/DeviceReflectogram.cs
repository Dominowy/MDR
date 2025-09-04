namespace MDR.Domain.Devices
{
    public class DeviceReflectogram : BaseEntity
    {
        public int SeriesNumber { get; set; }
        public byte[] Data { get; set; }

        public Guid DeviceDataId { get; set; }
        public DeviceData DeviceData { get; set; } = null!;
    }
}
