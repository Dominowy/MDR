namespace MDR.Domain.Devices.Mases2
{
    public class Mas2Data : DeviceData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Mas2 Device { get; set; }
    }
}
