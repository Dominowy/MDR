namespace MDR.Domain.Devices
{
    public class DeviceData : BaseEntity
    {
        public string Data { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public DateTime Timestamp { get; set; }

        protected DeviceData() : base()
        {
        }

        public DeviceData(string data) : base()
        {
            Data = data;
            Timestamp = DateTime.UtcNow;
        }
    }
}
