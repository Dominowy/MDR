using MDR.Application.Contracts;
using MDR.Application.Devices.Commands;
using MDR.Application.Devices.Services;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetUpdateDeviceFormRequest : IRequest<GetUpdateDeviceFormResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetUpdateDeviceFormHandler(IDeviceService service, DeviceFactory factory) : IRequestHandler<GetUpdateDeviceFormRequest, GetUpdateDeviceFormResponse>
    {
        public async Task<GetUpdateDeviceFormResponse> Handle(GetUpdateDeviceFormRequest request, CancellationToken cancellationToken)
        {
            var form = await service.GetById(request.Id, cancellationToken);

            return new GetUpdateDeviceFormResponse
            {
                Form = new UpdateDeviceRequest
                {
                    Device = form
                }
            };
        }
    }

    public class GetUpdateDeviceFormResponse
    {
        public UpdateDeviceRequest Form { get; set; }
    }
}
