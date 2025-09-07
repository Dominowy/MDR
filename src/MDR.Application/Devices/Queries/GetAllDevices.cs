using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetAllDevicesRequest : IRequest<GetAllDevicesResponse>
    {
        public DeviceType DeviceType { get; set; }
    }

    public class GetAllDevicesHandler(DeviceServiceFactory factory) : IRequestHandler<GetAllDevicesRequest, GetAllDevicesResponse>
    {
        public async Task<GetAllDevicesResponse> Handle(GetAllDevicesRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);

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
