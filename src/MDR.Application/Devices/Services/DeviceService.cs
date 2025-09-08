using MDR.Application.Common.Extensions;
using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MDR.Domain.Devices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MDR.Application.Devices.Services
{
    public class DeviceService(IApplicationDbContext dbContext, DeviceFactory factory) : IDeviceService
    {
        public async Task<DeviceDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<Device>()
               .GetExistingAsync(d => d.Id == id, cancellationToken);

            var config = factory.ConvertConfig(device.DeviceType, device.Config, cancellationToken);

            return DeviceDto.Convert(device, config);
        }

        public async Task<List<DeviceDto>> GetAll(CancellationToken cancellationToken)
        {
            var devices = await dbContext.Set<Device>().ToListAsync(cancellationToken);

            var deviceDtos = devices
            .Select(d =>
            {
                var config = factory.ConvertConfig(d.DeviceType, d.Config, cancellationToken);

                return DeviceDto.Convert(d, config);
            })
            .ToList();

            return deviceDtos;
        }

        public async Task<List<DeviceDataDto>> GetAllData(CancellationToken cancellationToken)
        {
            var devices = await dbContext.Set<DeviceData>().ToListAsync(cancellationToken);

            var deviceDataDtos = devices
            .Select(d =>
            {
                var config = factory.ConvertData(d.Device.DeviceType, d.Data, cancellationToken);

                return DeviceDataDto.Convert(d, config);
            })
            .ToList();

            return deviceDataDtos;
        }

        public async Task<Guid> Update(DeviceDto device, CancellationToken cancellationToken)
        {
            var aggregate = await dbContext.Set<Device>()
               .GetExistingAsync(d => d.Id == device.Id, cancellationToken);

            aggregate.SetName(device.Name);
            aggregate.SetConfig(JsonSerializer.Serialize(device.Config));

            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        public async Task<Guid> Add(DeviceDto device, CancellationToken cancellationToken)
        {
            var aggregate = new Device(device.Name);

            aggregate.SetDeviceType(device.Type);
            aggregate.SetConfig(JsonSerializer.Serialize(device.Config));

            dbContext.Set<Device>().Add(aggregate);
            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }


        public async Task Delete(Guid id, CancellationToken ct)
        {
            var device = await dbContext.Set<Device>()
                .GetExistingAsync(d => d.Id == id, ct);

            dbContext.Set<Device>().Remove(device);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}