using MDR.Domain.Devices;

namespace MDR.Application.Devices.Dto
{
    public class DeviceDataDto
    {
        public Guid Id { get; set; }

        public Guid DeviceId { get; set; }
        public object Data { get; set; }
        public DateTime Timestamp { get; set; }

        public static DeviceDataDto Convert(DeviceData deviceData, object data)
        {
            return new()
            {
                Id = deviceData.Id,
                DeviceId = deviceData.DeviceId,
                Data = data,
                Timestamp = deviceData.Timestamp
            };
        }
    }
}