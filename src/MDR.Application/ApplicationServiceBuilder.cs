using FluentValidation;
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

            services.AddScoped<DeviceServiceFactory>();

            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<DeviceServiceFactory>();
                return factory.Create(DeviceType.MAS2);
            });

            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<DeviceServiceFactory>();
                return factory.Create(DeviceType.MOUSE2);
            });

            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<DeviceServiceFactory>();
                return factory.Create(DeviceType.MOUSE2B);
            });

            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<DeviceServiceFactory>();
                return factory.Create(DeviceType.MOUSECOMBO);
            });

            return services;
        }
    }
}
