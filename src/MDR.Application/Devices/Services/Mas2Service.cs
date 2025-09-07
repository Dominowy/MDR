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

        public async Task<DeviceDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<Mas2>()
                .Include(m => m.DeviceDatas)
                .GetExistingAsync(d => d.Id == id, cancellationToken);

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

            return new DeviceDto
            {
                Id = device.Id,
                DeviceType = DeviceType.MAS2,
                Name = device.Name,
                Fields = fields
            };
        }

        public Task<List<DeviceConfigDto>> GetAll(CancellationToken cancellationToken)
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
                Id = device.Id,
                DeviceType = DeviceType.MAS2,
                Name = device.Name,
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
                    object? value = null;
                    if (field.Value != null)
                    {
                        value = field.Type switch
                        {
                            "Double" => Convert.ToDouble(field.Value),
                            "Int32" => Convert.ToInt32(field.Value),
                            "String" => Convert.ToString(field.Value),
                            _ => field.Value
                        };
                    }

                    prop.SetValue(device, value);
                }
            }

            await dbContext.Set<Mas2>().AddAsync(device, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        public async Task<Guid> Update(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<Mas2>()
                .GetExistingAsync(d => d.Id == deviceConfigDto.Id, cancellationToken);

            foreach (var field in deviceConfigDto.Fields)
            {
                var prop = typeof(Mas2).GetProperty(field.Name, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    object? value = null;

                    if (field.Value != null)
                    {
                        value = field.Type switch
                        {
                            "Double" => Convert.ToDouble(field.Value),
                            "Int32" => Convert.ToInt32(field.Value),
                            "String" => Convert.ToString(field.Value),
                            _ => field.Value
                        };
                    }

                    prop.SetValue(device, value);
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        public async Task Delete(Guid deviceId, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<Mas2>()
                .GetExistingAsync(d => d.Id == deviceId, cancellationToken);

            dbContext.Set<Mas2>().Remove(device);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}