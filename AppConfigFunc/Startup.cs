
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AppConfigFunc.Startup))]

namespace AppConfigFunc
{
    public class Startup : FunctionsStartup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("local.settings.json", true);

            // App Configuration から取り込む
            var builtConfig = config.Build();
            if (builtConfig["AZURE_FUNCTIONS_ENVIRONMENT"] != "Development")
            {
                config.AddAzureAppConfiguration(builtConfig["AppConfigurationConnectionString"]);
            }

            Configuration = config.Build();       
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.Configure<SampleObject>(Configuration.GetSection("Sample"));
        }
    }

    public class SampleObject
    {
        public string Message { get; set; }
    }
}
