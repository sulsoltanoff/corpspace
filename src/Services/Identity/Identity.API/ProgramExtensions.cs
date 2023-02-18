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

using Corpspace.Services.Identity.API.Configuration;
using Corpspace.Services.Identity.API.Services;
using Serilog;

namespace Corpspace.Services.Identity.API;

public static class ProgramExtensions
{
    private const string AppName = "Identity API";

    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddConfiguration(GetConfiguration()).Build();
        
    }

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];
        var logstashUrl = builder.Configuration["LogstashgUrl"];

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://localhost:8080" : logstashUrl, null)
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void AddCustomMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        builder.Services.AddRazorPages();

    }


    public static void AddCustomDatabase(this WebApplicationBuilder builder) =>
    builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql(builder.Configuration["ConnectionString"]));

    public static void AddCustomIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
    }


    public static void AddCustomIdentityServer(this WebApplicationBuilder builder)
    {
        var identityServerBuilder = builder.Services.AddIdentityServer(options =>
        {
            options.IssuerUri = "null";
            options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        })
                .AddInMemoryIdentityResources(Config.GetResources())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients(builder.Configuration))
                .AddAspNetIdentity<ApplicationUser>();

        // not recommended for production - you need to store your key material somewhere secure
        identityServerBuilder.AddDeveloperSigningCredential();
    }

    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
    }

    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddNpgSql(builder.Configuration["ConnectionString"] ?? throw new InvalidOperationException(),
                    name: "IdentityDB-check",
                    tags: new string[] { "IdentityDB" });
    }

    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IProfileService, ProfileService>();
        builder.Services.AddTransient<ILoginService<ApplicationUser>, EfLoginService>();
        builder.Services.AddTransient<IRedirectService, RedirectService>();
    }

    static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var config = builder.Build();

        if (config.GetValue<bool>("UseVault", false))
        {
            TokenCredential credential = new ClientSecretCredential(
                config["Vault:TenantId"],
                config["Vault:ClientId"],
                config["Vault:ClientSecret"]);
            builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"), credential);
        }

        return builder.Build();
    }
}
