using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetUpdateDeviceForm : IRequest<GetUpdateDeviceFormResponse>
    {
        public Guid DeviceId { get; set; }
    }

    public class GetUpdateDeviceFormResponse
    {
    }
}
