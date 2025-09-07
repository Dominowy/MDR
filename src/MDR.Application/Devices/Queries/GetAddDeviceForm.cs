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

    public class GetAddDeviceFormHandler(DeviceServiceFactory factory) : IRequestHandler<GetAddDeviceFormRequest, GetAddDeviceFormResponse>
    {
        public async Task<GetAddDeviceFormResponse> Handle(GetAddDeviceFormRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);
            
            var form = service.GetAddForm(cancellationToken);

            return new GetAddDeviceFormResponse
            {
                Form = new AddDeviceRequest
                {
                    Name = form.Name,
                    DeviceType = form.DeviceType,
                    Fields = [.. form.Fields.Select(f => new DeviceConfigFieldDto
                    {
                        Name = f.Name,
                        Value = f.Value,
                        Type = f.Type
                    })]
                }
            };
        }
    }

    public class GetAddDeviceFormResponse
    {
        public AddDeviceRequest Form { get; set;}
    }
}
