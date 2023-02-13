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

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", Corpspace.WebStatus.Program.AppName);
    var host = BuildWebHost(configuration, args);

    LogPackagesVersionInfo();

    Log.Information("Starting web host ({ApplicationContext})...", Corpspace.WebStatus.Program.AppName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Corpspace.WebStatus.Program.AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IWebHost BuildWebHost(IConfiguration config, string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .ConfigureAppConfiguration(x => x.AddConfiguration(config))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .Build();

Serilog.ILogger CreateSerilogLogger(IConfiguration config)
{
    var seqServerUrl = config["Serilog:SeqServerUrl"];
    var logstashUrl = config["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Corpspace.WebStatus.Program.AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl, null)
        .ReadFrom.Configuration(config)
        .CreateLogger();
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

string GetVersion(Assembly assembly)
{
    try
    {
        return $"{assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version} ({assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split()[0]})";
    }
    catch
    {
        return string.Empty;
    }
}

void LogPackagesVersionInfo()
{
    var assemblies = new List<Assembly>();

    foreach (var dependencyName in typeof(Corpspace.WebStatus.Program).Assembly.GetReferencedAssemblies())
    {
        try
        {
            // Try to load the referenced assembly...
            assemblies.Add(Assembly.Load(dependencyName));
        }
        catch
        {
            // Failed to load assembly. Skip it.
        }
    }

    var versionList = assemblies.Select(a => $"-{a.GetName().Name} - {GetVersion(a)}").OrderBy(value => value);

    Log.Logger.ForContext("PackageVersions", string.Join("\n", versionList)).Information("Package versions ({ApplicationContext})", Corpspace.WebStatus.Program.AppName);
}

namespace Corpspace.WebStatus
{
    public partial class Program
    {
        private static readonly string Namespace = typeof(Startup).Namespace;
        public static readonly string AppName = Namespace;
    }
}