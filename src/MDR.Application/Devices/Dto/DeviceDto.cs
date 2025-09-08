using MDR.Domain.Devices;

namespace MDR.Application.Devices.Dto
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }

        public object Config { get; set; }

        public List<DeviceDataDto> Datas { get; set; }

        public static DeviceDto Convert(Device device, object config) 
        {
            return new()
            {
                Id = device.Id,
                Name = device.Name,
                Type = device.DeviceType,
                Config = config
            };
        }
    }
}
