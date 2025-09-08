using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class GetDataFromDeviceRequest : IRequest<GetDataFromDeviceResonse>
    {
        public Guid DeviceId { get; set; }
    }

    public class GetDataFromDeviceResonse
    {
    }
}
