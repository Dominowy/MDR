using FluentValidation;
using MDR.Application.Contracts;
using MDR.Application.Devices.Services;
using MDR.Domain.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDR.Application
{
    public static class ApplicationServiceBuilder
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<DeviceFactory>();
            services.AddScoped<IDeviceService, DeviceService>();

            return services;
        }
    }
}
