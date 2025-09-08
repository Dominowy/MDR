using MDR.Application.Devices.Dto;
using MDR.Domain.Devices;

namespace MDR.Application.Contracts
{
    public interface IDeviceGateway
    {
        public Task<DeviceDataDto> FetchData(DeviceType type, Guid id, CancellationToken cancellationToken);
    }
}
