using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MDR.Application.Shared;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class UpdateDeviceRequest : DeviceConfigDto, IRequest<CommandResult>
    {
    }

    public class UpdateDeviceHandler(DeviceServiceFactory factory) : IRequestHandler<UpdateDeviceRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(UpdateDeviceRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);

            var id = await service.Update(new DeviceConfigDto
            {
                Id = request.Id,
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
