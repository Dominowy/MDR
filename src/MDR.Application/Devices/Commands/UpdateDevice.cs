using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class UpdateDeviceRequest : IRequest<CommandResult>
    {
        public DeviceDto Device { get; set; }
    }

    public class UpdateDeviceHandler(IDeviceService service) : IRequestHandler<UpdateDeviceRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(UpdateDeviceRequest request, CancellationToken cancellationToken)
        {
            var id = await service.Update(request.Device, cancellationToken);

            return new CommandResult
            {
                Id = id
            };
        }
    }
}
