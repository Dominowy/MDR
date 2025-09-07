using MDR.Application.Devices.Dto;

namespace MDR.Application.Devices.Services
{
    public class Mouse2BService : IDeviceService
    {
        public Task<Guid> Add(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid deviceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public DeviceConfigDto GetAddForm(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public DeviceConfigDto GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public DeviceConfigDto GetById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceConfigDto> GetEditForm(Guid deviceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Update(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}