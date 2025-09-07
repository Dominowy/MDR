using MDR.Application.Devices.Commands;
using MDR.Application.Devices.Dto;
using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetUpdateDeviceFormRequest : IRequest<GetUpdateDeviceFormResponse>
    {
        public Guid Id { get; set; }
        public DeviceType DeviceType { get; set; }
    }

    public class GetUpdateDeviceFormHandler(DeviceServiceFactory factory) : IRequestHandler<GetUpdateDeviceFormRequest, GetUpdateDeviceFormResponse>
    {
        public async Task<GetUpdateDeviceFormResponse> Handle(GetUpdateDeviceFormRequest request, CancellationToken cancellationToken)
        {
            var service = factory.Create(request.DeviceType);

            var form = await service.GetById(request.Id, cancellationToken);

            return new GetUpdateDeviceFormResponse
            {
                Form = new UpdateDeviceRequest
                {
                    Id = form.Id,
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

    public class GetUpdateDeviceFormResponse
    {
        public UpdateDeviceRequest Form { get; set; }
    }
}
