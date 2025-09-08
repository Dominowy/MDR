using MDR.Application.Common.Exceptions;
using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MDR.Domain.Devices;

namespace MDR.Infrastructure.Gateway
{
    public class DeviceGateway : IDeviceGateway
    {
        public Task<DeviceDataDto> FetchData(DeviceType type, Guid id, CancellationToken cancellationToken)
        {
            object data = type switch
            {
                DeviceType.MOUSE2 => new Mouse2DataDto
                {
                    Voltage = 12.3,
                    Resistance = 45.6
                },
                DeviceType.MOUSE2B => new Mouse2BDataDto
                {
                    Voltage = 11.8,
                    Resistance = 42.1,
                    LeakLocationPercent = 75.0
                },
                DeviceType.MOUSECOMBO => new MouseComboDataDto
                {
                    Voltage = 10.2,
                    Resistance = 38.4,
                    Reflectograms =
                    [
                        new ReflectogramDto { SeriesNumber = 0, Bytes = [1, 2, 3] },
                        new ReflectogramDto { SeriesNumber = 1, Bytes = [4, 5, 6] }
                    ]
                },
                DeviceType.MAS2 => new Mas2DataDto
                {
                    Temperature = 23.4,
                    Humidity = 55.2
                },
                _ => throw new UnsupportedException($"Unknown device type: {type}")
            };

            var dto = new DeviceDataDto
            {
                DeviceId = id,
                Timestamp = DateTime.UtcNow,
                Data = data
            };

            return Task.FromResult(dto);
        }
    }
}
