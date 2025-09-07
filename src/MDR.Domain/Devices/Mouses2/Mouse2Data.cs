namespace MDR.Domain.Devices.Mouses2
{
    public class Mouse2Data : DeviceData
    {
        public double Voltage { get; set; }
        public double Resistance { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Mouse2 Device { get; set; }
    }
}
