namespace MDR.Domain.Devices
{
    public class Device : BaseAggregateNameEnabled
    {
        public DeviceType DeviceType { get; set; }
        public string Config { get; set; } = string.Empty;

        public List<DeviceData> DeviceDatas { get; set; } = [];

        protected Device() : base()
        {
        }

        public Device(Guid id) : base(id)
        {
            SetEnabled(true);
        }

        public Device(string name) : this(Guid.NewGuid())
        {
            SetName(name);
            SetEnabled(true);
        }

        public void SetDeviceType(DeviceType deviceType)
        {
            var anyChange = DeviceType != deviceType;
            if (!anyChange) return;
            DeviceType = deviceType;
        }

        public void SetConfig(string config)
        {
            var anyChange = Config != config;
            if (!anyChange) return;
            Config = config;
        }

        public void SetDeviceData(DeviceData data)
        {
            DeviceDatas.Add(data);
        }
    }
}
