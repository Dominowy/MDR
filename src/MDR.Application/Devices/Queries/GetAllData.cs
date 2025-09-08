using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MediatR;

namespace MDR.Application.Devices.Queries
{
    public class GetAllDataRequest : IRequest<GetAllDataResponse>
    {
    }

    public class GetAllDataHandler(IDeviceService service) : IRequestHandler<GetAllDataRequest, GetAllDataResponse>
    {
        public async Task<GetAllDataResponse> Handle(GetAllDataRequest request, CancellationToken cancellationToken)
        {
            var datas = await service.GetAllData(cancellationToken);

            return new GetAllDataResponse
            {
                Datas = datas
            };
        }
    }

    public class GetAllDataResponse
    {
        public List<DeviceDataDto> Datas { get; set; }
    }
}
