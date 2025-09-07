using MDR.Application.Devices.Dto;

namespace MDR.Application.Devices.Services
{
    public interface IDeviceService
    {
        Task<DeviceDto> GetById(Guid id, CancellationToken cancellationToken);
        Task<List<DeviceDto>> GetAll(CancellationToken cancellationToken);
        DeviceConfigDto GetAddForm(CancellationToken cancellationToken);
        Task<DeviceConfigDto> GetEditForm(Guid deviceId, CancellationToken cancellationToken);
        Task<Guid> Add(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken);
        Task<Guid> Update(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken);
        Task Delete(Guid deviceId, CancellationToken cancellationToken);

        Task AddData(DeviceDto device, CancellationToken cancellationToken);
    }
}
