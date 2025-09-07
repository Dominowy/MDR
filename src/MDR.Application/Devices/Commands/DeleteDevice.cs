using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class DeleteDeviceRequest : IRequest
    {
        public Guid Id { get; set; }
        public DeviceType DeviceType { get; internal set; }
    }

    public class DeleteDeviceHandler(DeviceServiceFactory factory) : IRequestHandler<DeleteDeviceRequest>
    {
        public async Task Handle(DeleteDeviceRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);

            await service.Delete(request.Id, cancellationToken);
        }
    }
}
