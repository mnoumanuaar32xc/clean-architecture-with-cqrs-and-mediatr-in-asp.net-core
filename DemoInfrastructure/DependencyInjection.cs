using CommonLibrary.Models;
using DemoApplication.Common.Interfaces.Jwt;
using DemoApplication.Common.Interfaces.UnitOfWorks;
using DemoInfrastructure.Persistence.UnitOfWorks;
using DemoInfrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDemoInfrastructure(this IServiceCollection services,
            IConfiguration configuration, IHostEnvironment environment, string project = EnumProjects.DemoAPI, string projectType = EnumProjectTypes.API)
        {
            services.AddTransient<IMainUnitOfWork, MainUnitOfWork>();
            services.AddTransient<IJwtService, JwtService>();
            return services;
        }
    }
}
