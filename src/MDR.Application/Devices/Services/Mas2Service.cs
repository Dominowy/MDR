using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MDR.Application.Extensions;
using MDR.Domain.Devices;
using MDR.Domain.Devices.Mases2;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MDR.Application.Devices.Services
{
    public class Mas2Service(IApplicationDbContext dbContext) : IDeviceService
    {
        readonly string[] fieldProperties = { nameof(Mas2.TempAlarmThreshold), nameof(Mas2.HumidityAlarmThreshold) };

        public DeviceConfigDto GetById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public DeviceConfigDto GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public DeviceConfigDto GetAddForm(CancellationToken cancellationToken)
        {
            var deviceType = typeof(Mas2);

            var fields = fieldProperties
            .Select(p =>
            {
                var propInfo = deviceType.GetProperty(p);
                return new DeviceConfigFieldDto
                {
                    Name = p,
                    Value = null,
                    Type = propInfo?.PropertyType.Name ?? "Unknown"
                };
            })
            .ToList();

            return new DeviceConfigDto
            {
                DeviceType = DeviceType.MAS2,
                Fields = fields
            };
        }

        public async Task<DeviceConfigDto> GetEditForm(Guid deviceId, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<Mas2>()
                .GetExistingAsync(d => d.Id == deviceId, cancellationToken);

            var fields = fieldProperties
                .Select(p =>
                {
                    var propInfo = device.GetType().GetProperty(p);
                    return new DeviceConfigFieldDto
                    {
                        Name = p,
                        Value = propInfo?.GetValue(device),
                        Type = propInfo?.PropertyType.Name ?? "Unknown"
                    };
                })
                .ToList();

            return new DeviceConfigDto
            {
                DeviceType = DeviceType.MAS2,
                Fields = fields
            };
        }

        public async Task<Guid> Add(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            var device = new Mas2();

            device.SetName(deviceConfigDto.Name);

            foreach (var field in deviceConfigDto.Fields)
            {
                var prop = typeof(Mas2).GetProperty(field.Name);
                if (prop != null && prop.CanWrite)
                {
                    var convertedValue = field.Value != null
                        ? Convert.ChangeType(field.Value, prop.PropertyType)
                        : default;
                    prop.SetValue(device, convertedValue);
                }
            }

            await dbContext.Set<Mas2>().AddAsync(device, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        public Task<Guid> Update(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid deviceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}