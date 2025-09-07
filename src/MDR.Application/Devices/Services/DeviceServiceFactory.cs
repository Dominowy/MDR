using MDR.Application.Contracts;
using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using MDR.Domain.Devices.Mases2;
using MDR.Domain.Devices.Mouses2;
using MDR.Domain.Devices.Mouses2B;
using MDR.Domain.Devices.MousesCombo;
using Microsoft.EntityFrameworkCore;

public class DeviceServiceFactory(IApplicationDbContext dbContext)
{
    public IDeviceService Create(DeviceType deviceType)
    {
        return deviceType switch
        {
            DeviceType.MAS2 => new DeviceService<Mas2, Mas2Data>(
                dbContext,
                [nameof(Mas2.TempAlarmThreshold), nameof(Mas2.HumidityAlarmThreshold)],
                [nameof(Mas2Data.Temperature), nameof(Mas2Data.Humidity)]
            ),
            DeviceType.MOUSE2 => new DeviceService<Mouse2, Mouse2Data>(
                dbContext,
                [nameof(Mouse2.AlarmThreshold)],
                [nameof(Mouse2Data.Voltage), nameof(Mouse2Data.Resistance)]
            ),
            DeviceType.MOUSE2B => new DeviceService<Mouse2B, Mouse2BData>(
                dbContext,
                [nameof(Mouse2B.AlarmThreshold), nameof(Mouse2B.CableLength)],
                [nameof(Mouse2BData.Voltage), nameof(Mouse2BData.Resistance), nameof(Mouse2BData.LeakLocationPercent)]
            ),
            DeviceType.MOUSECOMBO => new DeviceService<MouseCombo, MouseComboData>(
                dbContext,
                [nameof(MouseCombo.AlarmThreshold)],
                [nameof(MouseComboData.Reflectograms)]
            ),
            _ => throw new NotSupportedException($"Unknown device type {deviceType}")
        };
    }
}
