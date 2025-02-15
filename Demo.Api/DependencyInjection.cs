using CommonLibrary.ApplicationSettings;
using CommonLibrary.Models;
using Demo.Api.ApiFramework.Swagger;
using Demo.Api.ApiFramework.Tools;
using Demo.Api.Enums;
using Demo.Api.Filters;
using DemoApplication.Common.Helpers;
using DemoApplication.Common.Interfaces.Jwt;
using DemoApplication.Common.Interfaces.Services.AppUserServices;
using DemoDomain.Enums.DemoApp.Exception;
using DemoDomain.Enums.General.Application;
using DemoDomain.Exceptions.Models;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PolyCache;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Security.Claims;
using System.Text;

namespace Demo.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDemoWebApi(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSettings, IWebHostEnvironment env)
        {
            services.Configure<SiteSettings>(configuration.GetSection(nameof(SiteSettings)));

           // services.AddScoped<ICurrentUserService, CurrentUserService>();

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            
            // Register the AppSettings so it can be resolved in the constructors
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<AppSettings>>().Value);
            
            // register ConnectionStrings
            var connectionSettingsSection = configuration.GetSection("ConnectionStrings");
            services.Configure<AppConnectionStringSettings>(connectionSettingsSection);

            // Register the AppConnectionStringSettings for dependency injection
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<AppConnectionStringSettings>>().Value);

            var AppConnectionString = connectionSettingsSection.Get<AppConnectionStringSettings>();


            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddHttpContextAccessor();
            services. AddJwtAuthentication(siteSettings.JwtSettings);
            services.AddDemoAppControllers(env);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerOptions();
            services.AddAutoMapperConfiguration();
           // services.AddPolyCache(configuration);

            return services;
        }
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
        }
        public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            if (env.IsDevelopment() || env.IsStaging() || env.IsEnvironment(EnumEnvironments.Local))
            {
                app.UseAppSwagger(configuration);
            }
            //The `app.UseStaticFiles()` method is used in an ASP.NET Core application to serve static files such as HTML, CSS, JavaScript, images, and other assets directly to requesting clients
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;

        }
        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();

            //Swagger middleware for generate UI from swagger.json
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demos API v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Demos API v2");
                options.InjectJavascript("/swaggerCustom.js");

                options.DocExpansion(DocExpansion.None);
            });

            //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
            app.UseReDoc(options =>
            {
                options.SpecUrl("/swagger/v1/swagger.json");
                //options.SpecUrl("/swagger/v2/swagger.json");

                #region Customizing
                //By default, the ReDoc UI will be exposed at "/api-docs"
                //options.RoutePrefix = "docs";
                //options.DocumentTitle = "My API Docs";

                options.EnableUntrustedSpec();
                options.ScrollYOffset(10);
                options.HideHostname();
                options.HideDownloadButton();
                options.ExpandResponses("200,201");
                options.RequiredPropsFirst();
                options.NoAutoAuth();
                options.PathInMiddlePanel();
                options.HideLoading();
                options.NativeScrollbars();
                options.DisableSearch();
                options.OnlyRequiredInSamples();
                options.SortPropsAlphabetically();
                #endregion
            });

            return app;
        }
        public static void AddDemoAppControllers(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddControllers(options =>
            {
                // TODO :: fix handling the model state validations.
                //options.Filters.Add(typeof(ValidateModelStateAttribute));

                options.Filters.Add(new ApiExceptionFilter(env));
                 
            });
        }

        #region Swagger
        public static IServiceCollection AddSwaggerOptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                options.OrderActionsBy(api =>
                {
                    if (api.GroupName == null)
                    {
                        return "0"; // Non-grouped routes first
                    }
                    return "1"; // Grouped routes next
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Demo API",
                    Description = "Demo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Demo Test App",
                        Email = "Demo Test App",
                        Url = new Uri("https://DemoTestApp.com/"),
                    },
                });
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Demo API",
                    Description = "Demo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Demo Test App",
                        Email = "Demo Test App",
                        Url = new Uri("https://DemoTestApp.com/"),
                    },
                });

                #region Filters
                //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
                //options.ExampleFilters();

                //options.OperationFilter<CustomControllerNameOperationFilter>();

                options.OperationFilter<ApplySummariesOperationFilter>();

                //Add 401 response and security requirements (Lock icon) to actions that need authorization
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             Array.Empty<string>()
                     }
                 });

                #region Versioning

                options.OperationFilter<RemoveVersionParameters>();
                options.DocumentFilter<SetVersionInPaths>();

                options.DocInclusionPredicate((docName, apiDesc) => true);
                //options.DocInclusionPredicate((docName, apiDesc) =>
                //{
                //    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                //    var versions = methodInfo.DeclaringType
                //        .GetCustomAttributes<ApiVersionAttribute>(true)
                //        .SelectMany(attr => attr.Versions);

                //    return versions.Any(v => $"v{v}" == docName);

                //});
                #endregion

                #endregion
            });
            services.AddSwaggerGenNewtonsoftSupport();
            //If use FluentValidation then must be use this package to show validation in swagger (MicroElements.Swashbuckle.FluentValidation)
            services.AddFluentValidationRulesToSwagger();
            return services;
        }
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtSettings.Issuer,
                    //TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)


                    // need this to avoid the legacy MS claim types. Sigh*
                    RoleClaimType = "role",
                    NameClaimType = "name",
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async context =>
                    {
                        if (context.Exception != null)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                            switch (context.Exception)
                            {
                                case SecurityTokenExpiredException:

                                    context.Response.Headers.Add(EnumResponseHeaders.TokenExpired, "true");

                                    // revoke the expired token from user tokens.
                                    var authorizationHeader = context.Request.Headers["Authorization"].ToString();

                                    string? tokenString = null;

                                    if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                                        tokenString = authorizationHeader["Bearer ".Length..].Trim();

                                    if (tokenString != null)
                                    {
                                        var _jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

                                        var _loginedUserGuid = await _jwtService.ReadGuidFromJwtAccessTokenAsync(tokenString, cancellationToken: context.HttpContext.RequestAborted);

                                        var accessUserType = await _jwtService.GetTypeOfAccessApplicationUserAsync(tokenString, cancellationToken: context.HttpContext.RequestAborted);

                                        if (!string.IsNullOrEmpty(_loginedUserGuid) && accessUserType != EnumAccessApplicationUserTypes.Unknown)
                                        {
                                            var _appUserService = context.HttpContext.RequestServices.GetRequiredService<IAppUserService>();

                                            if (accessUserType == EnumAccessApplicationUserTypes.ClientApplication)
                                            {
                                                await _appUserService.RevokeClientTokenAsync(_loginedUserGuid, tokenString, cancellationToken: context.HttpContext.RequestAborted);
                                            }
                                            else if (accessUserType == EnumAccessApplicationUserTypes.InternalApplicationUser)
                                            {
                                                await _appUserService.RevokeAppUserTokenAsync(_loginedUserGuid, tokenString, cancellationToken: context.HttpContext.RequestAborted);
                                            }
                                        }
                                    }
                                    break;

                                case SecurityTokenException:
                                    context.Response.Headers.Add(EnumResponseHeaders.InvalidToken, "true");
                                    break;
                            }

                            var authenticationFailedExceptionEnum = EnumExceptionErrorMessages.AuthenticationFailedException;

                            var errors = new List<ApiExceptionErrorModel>() { new ApiExceptionErrorModel { AppErrorKey = authenticationFailedExceptionEnum.Value, Messages = authenticationFailedExceptionEnum.Messages } };

                            var result = new ApiResult<int>(-1, StatusCodes.Status401Unauthorized, errors: errors);

                            var responseJson = JsonConvert.SerializeObject(result);

                            context.Response.ContentType = "application/json";

                            context.Response.WriteAsync(responseJson);

                            context.Response.HttpContext.Items["AuthenticationFailedEvent"] = true; // this required to escape same error later in other error handlers. 
                        }

                        await Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        // Check for Claims
                        var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;

                        if (claimsIdentity == null || claimsIdentity.Claims == null || claimsIdentity.Claims?.Any() != true)
                        {
                            context.Fail("This Token has No Claims.");
                            return;
                        }

                        // Get the JWT token string value
                        var tokenString = (context.SecurityToken as JsonWebToken)?.UnsafeToString();

                        if (string.IsNullOrEmpty(tokenString))
                        {
                            context.Fail("This Token Is Invalid Token");
                            return;
                        }

                        // Detect logined user Type {User App / Client App}
                        var _jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

                        var typeOfUser = await _jwtService.GetTypeOfAccessApplicationUserAsync(tokenString, cancellationToken: context.HttpContext.RequestAborted);

                        if (typeOfUser == EnumAccessApplicationUserTypes.Unknown)
                        {
                            context.Fail("Unknown User Access Type.");
                            return;
                        }

                        var _accessUserId = (typeOfUser == EnumAccessApplicationUserTypes.ClientApplication) ? claimsIdentity.Claims.FirstOrDefault(r => r.Type == "ClientId")?.Value : claimsIdentity.Claims.FirstOrDefault(r => r.Type == "AppUserGuid")?.Value;

                        if (string.IsNullOrEmpty(_accessUserId))
                        {
                            context.Fail("This token has no Linked user information.");
                            return;
                        }

                        var _appUserService = context.HttpContext.RequestServices.GetRequiredService<IAppUserService>();

                        var isTokenActive = (typeOfUser == EnumAccessApplicationUserTypes.ClientApplication) ? await _appUserService.IsClientTokenActiveAsync(_accessUserId, tokenString, cancellationToken: context.HttpContext.RequestAborted) : await _appUserService.IsAppUserTokenActiveAsync(_accessUserId, tokenString, cancellationToken: context.HttpContext.RequestAborted);

                        if (!isTokenActive)
                        {
                            context.Fail("Token is expired.");
                            return;
                        }
                    },
                    OnForbidden = context =>
                    {
                        // in case of permission not exist in claims.

                        var forbiddenExceptionExceptionEnum = EnumExceptionErrorMessages.ForbiddenException;

                        var errors = new List<ApiExceptionErrorModel>() { new ApiExceptionErrorModel { AppErrorKey = forbiddenExceptionExceptionEnum.Value, Messages = forbiddenExceptionExceptionEnum.Messages } };

                        var result = JsonConvert.SerializeObject(new ApiResult<int>(-1, StatusCodes.Status401Unauthorized, errors: errors));

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        // in case the error coming from the OnAuthenticationFailed event.

                        if (context.HttpContext.Items.ContainsKey("AuthenticationFailedEvent"))
                            return Task.CompletedTask;

                        var errors = new List<ApiExceptionErrorModel>() { new ApiExceptionErrorModel { AppErrorKey = EnumExceptionErrorMessages.AuthenticationFailedException.Value, Messages = EnumExceptionErrorMessages.AuthenticationFailedException.Messages } };

                        var result = new ApiResult<int>(-1, StatusCodes.Status401Unauthorized, errors: errors);

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        var responseJson = JsonConvert.SerializeObject(result);

                        context.Response.ContentType = "application/json";

                        context.Response.WriteAsync(responseJson);

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                // Policy for internal users
                options.AddPolicy("InternalAppUserPolicy", policy =>
                    policy.RequireClaim("AccessType", "0"));

                // Policy for clients
                options.AddPolicy("ClientPolicy", policy =>
                    policy.RequireClaim("AccessType", "1"));
            });
        }

        #endregion

    }
}
