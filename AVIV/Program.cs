using Autofac.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace AVIV.API
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = "AVIV.API";
        public static readonly string ProjectName = "AVIV";

        public static IConfiguration _configuration;

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Log.Logger = CreateSerilogLogger(_configuration);
            Log.Information("Configured web host ({ApplicationContext})...", AppName);

            //using (var scope = host.Secteurs.CreateScope())
            //{
            //    var services = scope.SecteurProvider;

            //    try
            //    {
            //        await SeedData.InitializeAsync(services);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred seeding the DB.");
            //    }
            //}

            Log.Information("Starting web host ({ApplicationContext})...", AppName);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builder = config.Build();
                    var keyVaultEndpoint = builder["AzureKeyVaultEndpoint"];
                    if (!string.IsNullOrEmpty(keyVaultEndpoint))
                    {
                        //var azureServiceTokenProvider = new AzureServiceTokenProvider();
                        //var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                        //config.AddAzureKeyVault(keyVaultEndpoint);
                    }
                    else
                    {
                        _configuration = GetConfiguration(context.HostingEnvironment);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.AddConsole();
                        });
                });
        }

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var seqServerKey = configuration["Serilog:SeqServerKey"];
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.WithProperty("ProjectName", ProjectName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(
                    serverUrl: string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl,
                    apiKey: string.IsNullOrWhiteSpace(seqServerKey) ? "" : seqServerKey)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static IConfiguration GetConfiguration(IHostEnvironment env)
        {
            if (env.EnvironmentName == "Production")
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
                return builder.Build();
            }
            else
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
                return builder.Build();
            }
        }
    }
}
