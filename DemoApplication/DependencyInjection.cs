using CommonLibrary.Behaviours;
using CommonLibrary.Models;
using DemoApplication.Common.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDemoApplication(this IServiceCollection services,
            IConfiguration configuration, IHostEnvironment environment, string project = EnumProjects.DemoAPI, string projectType = EnumProjectTypes.API)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



            // Get the current configuration.
            var connectionSettingsSection = configuration.GetSection("ConnectionStrings");
            services.Configure<AppConnectionStringSettings>(connectionSettingsSection);


            return services;
        }
    }
}
