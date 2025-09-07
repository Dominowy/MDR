using MDR.Domain.Devices;

namespace MDR.Application.Devices.Dto
{
    public class DeviceConfigDto
    {
        public DeviceType DeviceType { get; set; }
        public string Name { get; set; }
        public List<DeviceConfigFieldDto> Fields { get; set; }
    }
}
