using FluentValidation;
using MDR.Application.Devices.Services;
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

            services.AddScoped<Mas2Service>();
            services.AddScoped<Mouse2Service>();
            services.AddScoped<Mouse2BService>();
            services.AddScoped<MouseComboService>();

            return services;
        }
    }
}
