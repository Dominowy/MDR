using MDR.Application.Devices.Dto;
using MDR.Domain.Devices;
using System.Text.Json;

namespace MDR.Application.Devices.Services
{
    public class DeviceFactory
    {
        public object CreateConfig(DeviceType deviceType, CancellationToken token)
        {
            return deviceType switch
            {
                DeviceType.MOUSE2 => new Mouse2ConfigDto(),
                DeviceType.MOUSE2B => new Mouse2BConfigDto(),
                DeviceType.MOUSECOMBO => new MouseComboConfigDto(),
                DeviceType.MAS2 => new Mas2ConfigDto(),
                _ => throw new NotSupportedException($"Unknown device type: {deviceType}")
            };
        }

        public object ConvertConfig(DeviceType deviceType, string config, CancellationToken token)
        {
            return deviceType switch
            {
                DeviceType.MOUSE2 => JsonSerializer.Deserialize<Mouse2ConfigDto>(config)!,
                DeviceType.MOUSE2B => JsonSerializer.Deserialize<Mouse2BConfigDto>(config)!,
                DeviceType.MOUSECOMBO => JsonSerializer.Deserialize<MouseComboConfigDto>(config)!,
                DeviceType.MAS2 => JsonSerializer.Deserialize<Mas2ConfigDto>(config)!,
                _ => throw new NotSupportedException($"Unknown device type: {deviceType}")
            };
        }

        public object ConvertData(DeviceType deviceType, string data, CancellationToken token)
        {
            return deviceType switch
            {
                DeviceType.MOUSE2 => JsonSerializer.Deserialize<Mouse2DataDto>(data)!,
                DeviceType.MOUSE2B => JsonSerializer.Deserialize<Mouse2BDataDto>(data)!,
                DeviceType.MOUSECOMBO => JsonSerializer.Deserialize<MouseComboDataDto>(data)!,
                DeviceType.MAS2 => JsonSerializer.Deserialize<Mas2DataDto>(data)!,
                _ => throw new NotSupportedException($"Unknown device type: {deviceType}")
            };
        }
    }
}
