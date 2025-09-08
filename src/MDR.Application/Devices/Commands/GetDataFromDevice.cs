using MDR.Application.Contracts;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class GetDataFromDeviceRequest : IRequest<GetDataFromDeviceResonse>
    {
        public Guid DeviceId { get; set; }
    }

    public class GetDataFromDeviceHandler(IDeviceService deviceService, IDeviceGateway deviceGateway) : IRequestHandler<GetDataFromDeviceRequest, GetDataFromDeviceResonse>
    {
        public async Task<GetDataFromDeviceResonse> Handle(GetDataFromDeviceRequest request, CancellationToken cancellationToken)
        {
            var device = await deviceService.GetById(request.DeviceId, cancellationToken);

            var data = await deviceGateway.FetchData(device.Type, device.Id, cancellationToken);

            await deviceService.AddData(data, cancellationToken);

            return new GetDataFromDeviceResonse
            {
                Id = device.Id
            };
        }
    }

    public class GetDataFromDeviceResonse
    {
        public Guid Id { get; set; }
    }
}
