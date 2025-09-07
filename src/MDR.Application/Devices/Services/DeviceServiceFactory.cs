using MDR.Domain.Devices;
using Microsoft.Extensions.DependencyInjection;

namespace MDR.Application.Devices.Services
{
    public class DeviceServiceFactory(IServiceProvider sp)
    {
        public IDeviceService Create(DeviceType deviceType) => deviceType switch
        {
            DeviceType.MOUSE2 => sp.GetRequiredService<Mouse2Service>(),
            DeviceType.MOUSE2B => sp.GetRequiredService<Mouse2BService>(),
            DeviceType.MOUSECOMBO => sp.GetRequiredService<MouseComboService>(),
            DeviceType.MAS2 => sp.GetRequiredService<Mas2Service>(),
            _ => throw new NotSupportedException($"Unknown device type {deviceType}")
        };
    }
}
