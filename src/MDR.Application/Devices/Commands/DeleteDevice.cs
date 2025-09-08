using MDR.Application.Contracts;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class DeleteDeviceRequest : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteDeviceHandler(IDeviceService service) : IRequestHandler<DeleteDeviceRequest>
    {
        public async Task Handle(DeleteDeviceRequest request, CancellationToken cancellationToken)
        {
            await service.Delete(request.Id, cancellationToken);
        }
    }
}
