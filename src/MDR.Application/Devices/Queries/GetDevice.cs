using MDR.Application.Contracts;
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
    }

    public class GetDeviceHandler(IDeviceService service) : IRequestHandler<GetDeviceRequest, GetDeviceResponse>
    {
        public async Task<GetDeviceResponse> Handle(GetDeviceRequest request, CancellationToken cancellationToken)
        {
            var device = await service.GetById(request.Id, cancellationToken);

            return new GetDeviceResponse
            {
                Device = device,
            };
        }
    }

    public class GetDeviceResponse
    {
        public DeviceDto Device { get; set; }
    }
}
