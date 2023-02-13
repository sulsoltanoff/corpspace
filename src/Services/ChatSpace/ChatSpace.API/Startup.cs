#region Corpspace© Apache-2.0
// Copyright © 2023 Sultan Soltanov. All rights reserved.
// Author: Sultan Soltanov
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatSpace.Application.Chat.Impl;
using Corpspace.BuildingBlocks.EventBus;
using Corpspace.BuildingBlocks.EventBus.Abstractions;
using Corpspace.BuildingBlocks.EventBusRabbitMQ;
using Corpspace.BuildingBlocks.IntegrationEventLogEF;
using Corpspace.BuildingBlocks.IntegrationEventLogEF.Services;
using Corpspace.ChatSpace.API.Controllers;
using Corpspace.ChatSpace.API.Infrastructure.AutofacModules;
using Corpspace.ChatSpace.API.Infrastructure.Filters;
using Corpspace.ChatSpace.API.Infrastructure.Services;
using Corpspace.ChatSpace.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace Corpspace.ChatSpace.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }
    
    public virtual IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services
            .AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            })
            .Services
            .AddApplicationInsights(Configuration)
            .AddCustomMvc()
            .AddHealthChecks(Configuration)
            .AddCustomDbContext(Configuration)
            .AddCustomSwagger(Configuration)
            .AddCustomAuthentication(Configuration)
            .AddCustomAuthorization(Configuration)
            .AddCustomIntegrations(Configuration)
            .AddCustomConfiguration(Configuration);
            // .AddEventBus(Configuration);
        //configure autofac

        var container = new ContainerBuilder();
        container.Populate(services);

        container.RegisterModule(new MediatorModule());
        container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]!));

        return new AutofacServiceProvider(container.Build());
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        //loggerFactory.AddAzureWebAppDiagnostics();
        //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

        var pathBase = Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }

        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "ChatSpace.API V1");
                c.OAuthClientId("chatspace-swagger-ui");
                c.OAuthAppName("ChatSpace Swagger UI");
            });

        app.UseRouting();
        app.UseCors("CorsPolicy");
        ConfigureAuth(app);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<ChatAppService>();
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
            endpoints.MapGet("/_proto/", async ctx =>
            {
                ctx.Response.ContentType = "text/plain";
                using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "basket.proto"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    var line = await sr.ReadLineAsync();
                    if (line != "/* >>" || line != "<< */")
                    {
                        await ctx.Response.WriteAsync(line);
                    }
                }
            });
            endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        });

        // ConfigureEventBus(app);
    }


    private void ConfigureEventBus(IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        
        // Add subscribe event
        // eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
    }

    protected virtual void ConfigureAuth(IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}

static class CustomExtensionsMethods
{
    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry(configuration);
        services.AddApplicationInsightsKubernetesEnricher();

        return services;
    }

    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        // Add framework services.
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            // Added for functional tests
            .AddApplicationPart(typeof(ChannelsController).Assembly)
            .AddApplicationPart(typeof(MessagesController).Assembly)
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        hcBuilder
            .AddNpgSql(
                configuration["ConnectionString"]!,
                name: "ChatSpaceDB-check",
                tags: new string[] { "chatspace-service" });
        // hcBuilder
        //     .AddRabbitMQ(
        //         $"amqp://{configuration["EventBusConnection"]}",
        //         name: "chatspace-rabbitmqbus-check",
        //         tags: new string[] { "rabbitmqbus" });
        

        return services;
    }

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChatAppContext>(options =>
                {
                    options.UseNpgsql(configuration["ConnectionString"],
                        npgsqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                        });
                },
                    ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                );

        services.AddDbContext<IntegrationEventLogContext>(options =>
        {
            options.UseNpgsql(configuration["ConnectionString"],
                npgsqlOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                                    });
        });

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Corpspace platform - ChatSpace service HTTP API",
                Version = "v1",
                Description = "The Chat Service HTTP API"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
                        TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
                        Scopes = new Dictionary<string, string>()
                        {
                            { "chatspace", "ChatSpace service API" }
                        }
                    }
                }
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
            sp => (DbConnection c) => new IntegrationEventLogService(c));

        // services.AddTransient<IChatSpaceIntegrationEventService, ChatSpaceIntegrationEventService>();
        
        // services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
        // {
        //     var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();
        //
        //
        //     var factory = new ConnectionFactory()
        //     {
        //         HostName = configuration["EventBusConnection"],
        //         DispatchConsumersAsync = true
        //     };
        //
        //     if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
        //     {
        //         factory.UserName = configuration["EventBusUserName"];
        //     }
        //
        //     if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
        //     {
        //         factory.Password = configuration["EventBusPassword"];
        //     }
        //
        //     var retryCount = 5;
        //     if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
        //     {
        //         retryCount = int.Parse(configuration["EventBusRetryCount"]!);
        //     }
        //
        //     return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
        // });

        return services;
    }

    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json", "application/problem+xml" }
                };
            };
        });

        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];
            var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
            var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            var retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
            {
                retryCount = int.Parse(configuration["EventBusRetryCount"]!);
            }

            return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, iLifetimeScope, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // prevent from mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = "chatspace";
            options.TokenValidationParameters.ValidateAudience = false;
        });

        return services;
    }
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "chatspace");
            });
        });
        return services;
    }
}