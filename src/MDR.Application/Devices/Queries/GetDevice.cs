using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetDevice : IRequest<GetDeviceResponse>
    {
        public Guid DeviceId { get; set; }
    }

    public class GetDeviceResponse
    {
    }
}
