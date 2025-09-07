using MDR.Domain.Devices;

namespace MDR.Application.Devices.Dto
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Name { get; set; }
        public List<DeviceConfigFieldDto> Fields { get; set; }
    }
}
