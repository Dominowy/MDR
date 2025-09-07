using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MDR.Application.Shared;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class AddDeviceRequest : DeviceConfigDto, IRequest<CommandResult>
    {
    }

    public class AddDeviceHandler(DeviceServiceFactory factory) : IRequestHandler<AddDeviceRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(AddDeviceRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);

            var id = await service.Add(new DeviceConfigDto
            {
                Name = request.Name,
                Fields = request.Fields
            }, cancellationToken);

            return new CommandResult
            {
                Id = id
            };
        }
    }
}
