using MDR.Application.Devices.Dto;

namespace MDR.Application.Contracts
{
    public interface IDeviceService
    {
        Task<DeviceDto> GetById(Guid id, CancellationToken cancellationToken);
        Task<List<DeviceDto>> GetAll(CancellationToken cancellationToken);

        Task<List<DeviceDataDto>> GetAllData(CancellationToken cancellationToken);
        Task<Guid> Add(DeviceDto device, CancellationToken cancellationToken);
        Task<Guid> Update(DeviceDto device, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);

    }
}
