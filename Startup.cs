using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;
using ILogger = Serilog.ILogger;

namespace ct_token_monitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
        }

        private static IConfiguration SetupConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }
        private static ILogger SetupLogger()
        {
            return Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Directory.GetCurrentDirectory() + "/Log.txt")
            .CreateLogger();
        }
        internal static ServiceProvider RegisterServices(string[] args)
        {
            IConfiguration configuration = SetupConfiguration(args);
            ILogger logger = SetupLogger();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddSingleton(logger);


            return serviceCollection.BuildServiceProvider();
        }
    }
}
