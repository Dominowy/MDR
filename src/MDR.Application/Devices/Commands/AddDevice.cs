using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class AddDeviceRequest : IRequest<CommandResult>
    {
        public DeviceDto Device { get; set; }
    }

    public class AddDeviceHandler(IDeviceService deviceService) : IRequestHandler<AddDeviceRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(AddDeviceRequest request, CancellationToken cancellationToken)
        {
            var device = await deviceService.Add(request.Device, cancellationToken);

            return new CommandResult
            {
                Id = device
            };
        }
    }
}
