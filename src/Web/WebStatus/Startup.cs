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

namespace Corpspace.WebStatus;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        RegisterAppInsights(services);
        
        services.AddMvc();

        services.AddOptions();
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());

        services
            .AddHealthChecksUI()
            .AddInMemoryStorage();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        //loggerFactory.AddAzureWebAppDiagnostics();
        //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        var pathBase = Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
        }

        app.UseHealthChecksUI(config =>
        {
            config.ResourcesPath = string.IsNullOrEmpty(pathBase) ? "/ui/resources" : $"{pathBase}/ui/resources";
            config.UIPath = "/hc-ui";
        });

        app.UseStaticFiles();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        });
    }

    private void RegisterAppInsights(IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry(Configuration);
        services.AddApplicationInsightsKubernetesEnricher();
    }
}
