using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetAllDevicesRequest : IRequest<GetAllDevicesResponse>
    {
    }

    public class GetAllDevicesHandler(IDeviceService service) : IRequestHandler<GetAllDevicesRequest, GetAllDevicesResponse>
    {
        public async Task<GetAllDevicesResponse> Handle(GetAllDevicesRequest request, CancellationToken cancellationToken)
        {
            var devices = await service.GetAll(cancellationToken);

            return new GetAllDevicesResponse
            {
                Devices = devices
            };
        }
    }

    public class GetAllDevicesResponse
    {
        public List<DeviceDto> Devices { get; set; }
    }
}
