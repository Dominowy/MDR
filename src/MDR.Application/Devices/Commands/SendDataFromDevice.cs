using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MediatR;

namespace MDR.Application.Devices.Commands
{
    public class SendDataFromDeviceRequest : IRequest<SendDataFromDeviceResponse>
    {
        public DeviceDataDto Data { get; set; }
    }

    public class SendDataFromDeviceHandler(IDeviceService deviceService) : IRequestHandler<SendDataFromDeviceRequest, SendDataFromDeviceResponse>
    {
        public async Task<SendDataFromDeviceResponse> Handle(SendDataFromDeviceRequest request, CancellationToken cancellationToken)
        {
            await deviceService.AddData(request.Data, cancellationToken);


            return new SendDataFromDeviceResponse();
        }
    }

    public class SendDataFromDeviceResponse
    {
    }
}
