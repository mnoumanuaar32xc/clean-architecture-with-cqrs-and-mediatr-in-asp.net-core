using CommonLibrary;
using CommonLibrary.ApplicationSettings;
using CommonLibrary.Models;
using Demo.Api;
using DemoApplication;
using DemoInfrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var configuration = builder.Configuration;
        var environment = builder.Environment;

        var _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();


        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddDemoApplication(configuration, environment, EnumProjects.DemoAPI, EnumProjectTypes.API);
        builder.Services.AddCommonLibrary(configuration, environment,EnumProjects.DemoAPI, EnumProjectTypes.API);
        builder.Services.AddDemoInfrastructure(configuration, environment, EnumProjects.DemoAPI, EnumProjectTypes.API);
        builder.Services.AddDemoInfrastructure(configuration, environment, EnumProjects.DemoAPI, EnumProjectTypes.API);
        builder.Services.AddDemoWebApi(configuration, _siteSettings!, environment);

        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        var app = builder.Build();
       
        // Configure the HTTP request pipeline.
        if (environment.IsDevelopment() || environment.IsStaging() || environment.IsEnvironment("Local"))
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseWebApi(configuration, environment);
        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        //app.MapControllers();

        app.Run();
    }
}