namespace MDR.Domain.Devices.Mouses2B
{
    public class Mouse2BData : DeviceData
    {
        public double Voltage { get; set; }
        public double Resistance { get; set; }
        public double LeakLocationPercent { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Mouse2B Device { get; set; }
    }
}
