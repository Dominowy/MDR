using MDR.Application.Devices.Commands;
using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetAddDeviceFormRequest : IRequest<GetAddDeviceFormResponse>
    {
        public DeviceType DeviceType { get; set; }
    }

    public class GetAddDeviceFormHandler(DeviceFactory factory) : IRequestHandler<GetAddDeviceFormRequest, GetAddDeviceFormResponse>
    {
        public async Task<GetAddDeviceFormResponse> Handle(GetAddDeviceFormRequest request, CancellationToken cancellationToken)
        {
            var form = factory.CreateConfig(request.DeviceType, cancellationToken);

            return new GetAddDeviceFormResponse
            {
                Form = new AddDeviceRequest
                {
                    Device = new DeviceDto
                    {
                        Type = request.DeviceType,
                        Config = form
                    }
                }
            };
        }
    }

    public class GetAddDeviceFormResponse
    {
        public AddDeviceRequest Form { get; set;}
    }
}
