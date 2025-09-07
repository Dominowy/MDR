using MDR.Application.Contracts;
using MDR.Application.Devices.Dto;
using MDR.Application.Extensions;
using MDR.Domain.Devices;
using MDR.Domain.Devices.Mases2;
using MDR.Domain.Devices.Mouses2;
using MDR.Domain.Devices.Mouses2B;
using MDR.Domain.Devices.MousesCombo;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace MDR.Application.Devices.Services
{
    public class DeviceService<TDevice, TDeviceData>(IApplicationDbContext dbContext, string[] fieldProperties, string[] fieldDataProperties) : IDeviceService
    where TDevice : Device, new()
    where TDeviceData : class
    {
        public async Task<DeviceDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<TDevice>()
                .Include("DeviceDatas")
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

            var datafields = fieldDataProperties
                .Select(p =>
                {
                    var propInfo = device.GetType().GetProperty(p);
                    return new DeviceDataFieldDto
                    {
                        Name = p,
                        Value = propInfo?.GetValue(device),
                    };
                })
                .ToList();

            return new DeviceDto
            {
                Id = device.Id,
                DeviceType = GetDeviceType<TDevice>(),
                Name = device.Name,
                Fields = fields
            };
        }

        public async Task<List<DeviceDto>> GetAll(CancellationToken cancellationToken)
        {
            var devices = await dbContext.Set<TDevice>()
                .ToListAsync(cancellationToken);

            var deviceDtos = devices.Select(device =>
            {
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
                    DeviceType = GetDeviceType<TDevice>(),
                    Name = device.Name,
                    Fields = fields
                };
            }).ToList();

            return deviceDtos;
        }


        public DeviceConfigDto GetAddForm(CancellationToken cancellationToken)
        {
            var deviceType = typeof(TDevice);

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
                DeviceType = GetDeviceType<TDevice>(),
                Fields = fields
            };
        }

        public async Task<DeviceConfigDto> GetEditForm(Guid deviceId, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<TDevice>()
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
                DeviceType = GetDeviceType<TDevice>(),
                Name = device.Name,
                Fields = fields
            };
        }

        public async Task<Guid> Add(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            var device = new TDevice();

            device.SetName(deviceConfigDto.Name);

            foreach (var field in deviceConfigDto.Fields)
            {
                var prop = typeof(TDevice).GetProperty(field.Name, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    object? value = null;

                    if (field.Value != null)
                    {
                        if (field.Value is JsonElement jsonElement)
                        {
                            value = GetValueFromJsonElement(jsonElement, field.Type);
                        }
                        else
                        {
                            value = field.Type switch
                            {
                                "Double" => Convert.ToDouble(field.Value),
                                "Int32" => Convert.ToInt32(field.Value),
                                "String" => Convert.ToString(field.Value),
                                _ => field.Value
                            };
                        }
                    }

                    prop.SetValue(device, value);
                }
            }

            await dbContext.Set<TDevice>().AddAsync(device, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        public async Task<Guid> Update(DeviceConfigDto deviceConfigDto, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<TDevice>()
                .GetExistingAsync(d => d.Id == deviceConfigDto.Id, cancellationToken);

            foreach (var field in deviceConfigDto.Fields)
            {
                var prop = typeof(TDevice).GetProperty(field.Name, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    object? value = null;

                    if (field.Value != null)
                    {
                        if (field.Value is JsonElement jsonElement)
                        {
                            value = GetValueFromJsonElement(jsonElement, field.Type);
                        }
                        else
                        {
                            value = field.Type switch
                            {
                                "Double" => Convert.ToDouble(field.Value),
                                "Int32" => Convert.ToInt32(field.Value),
                                "String" => Convert.ToString(field.Value),
                                _ => field.Value
                            };
                        }
                    }

                    prop.SetValue(device, value);
                }
            }


            await dbContext.SaveChangesAsync(cancellationToken);

            return device.Id;
        }

        public async Task Delete(Guid deviceId, CancellationToken cancellationToken)
        {
            var device = await dbContext.Set<TDevice>()
                .GetExistingAsync(d => d.Id == deviceId, cancellationToken);

            dbContext.Set<TDevice>().Remove(device);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private object? GetValueFromJsonElement(JsonElement json, string type)
        {
            try
            {
                return type switch
                {
                    "Double" => json.ValueKind switch
                    {
                        JsonValueKind.Number => json.GetDouble(),
                        JsonValueKind.String => double.Parse(json.GetString()!),
                        _ => null
                    },
                    "Int32" => json.ValueKind switch
                    {
                        JsonValueKind.Number => json.GetInt32(),
                        JsonValueKind.String => int.Parse(json.GetString()!),
                        _ => null
                    },
                    "String" => json.GetString(),
                    _ => null
                };
            }
            catch
            {
                return null;
            }
        }

        private DeviceType GetDeviceType<T>() => typeof(T) switch
        {
            Type t when t == typeof(Mas2) => DeviceType.MAS2,
            Type t when t == typeof(Mouse2) => DeviceType.MOUSE2,
            Type t when t == typeof(Mouse2B) => DeviceType.MOUSE2B,
            Type t when t == typeof(MouseCombo) => DeviceType.MOUSECOMBO,
            _ => throw new NotSupportedException()
        };
    }
}