using MDR.Application.Devices.Commands;
using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetDeviceRequest : IRequest<GetDeviceResponse>
    {
        public Guid Id { get; set; }
        public DeviceType DeviceType { get; set; }
    }

    public class GetDeviceHandler(DeviceServiceFactory factory) : IRequestHandler<GetDeviceRequest, GetDeviceResponse>
    {
        public async Task<GetDeviceResponse> Handle(GetDeviceRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);

            var form = await service.GetById(request.Id, cancellationToken);

            return new GetDeviceResponse
            {
                Device = form
            };
        }
    }

    public class GetDeviceResponse
    {
        public DeviceDto Device { get; set; }
    }
}
