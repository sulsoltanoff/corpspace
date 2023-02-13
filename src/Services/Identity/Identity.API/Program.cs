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

var appName = "Identity.API";
var builder = WebApplication.CreateBuilder();

builder.AddCustomConfiguration();
builder.AddCustomSerilog();
builder.AddCustomMvc();
builder.AddCustomDatabase();
builder.AddCustomIdentity();
builder.AddCustomIdentityServer();
builder.AddCustomAuthentication();
builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var pathBase = builder.Configuration["PATH_BASE"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}
app.UseStaticFiles();

// This cookie policy fixes login issues with Chrome 80+ using HTTP
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseIdentityServer();


app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});
try
{
    app.Logger.LogInformation("Seeding database ({ApplicationName})...", appName);

    // Apply database migration automatically. Note that this approach is not
    // recommended for production scenarios. Consider generating SQL scripts from
    // migrations instead.
    using (var scope = app.Services.CreateScope())
    {
        await SeedData.EnsureSeedData(scope, app.Configuration, app.Logger);
    }

    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();

    return 0;
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);
    return 1;
}
finally
{
    Serilog.Log.CloseAndFlush();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();

    if (!config.GetValue<bool>("UseVault", false)) return builder.Build();
    TokenCredential credential = new ClientSecretCredential(
        config["Vault:TenantId"],
        config["Vault:ClientId"],
        config["Vault:ClientSecret"]);
    builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"), credential);

    return builder.Build();
}