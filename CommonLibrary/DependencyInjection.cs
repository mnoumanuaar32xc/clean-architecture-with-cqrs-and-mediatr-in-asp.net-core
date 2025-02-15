using CommonLibrary.Models;
using CommonLibrary.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommonLibrary
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddCommonLibrary(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, string project = EnumProjects.DemoAPI, string projectType = EnumProjectTypes.API)
        { 
            CommonServiceProvider.Configure(services.BuildServiceProvider());
            return services;
        }


    }
}
