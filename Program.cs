using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
namespace ct_token_monitor
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = Startup.RegisterServices(args);
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
            ILogger logger = serviceProvider.GetService<ILogger>();




            CommerceTools ct = new CommerceTools(configuration, logger);

            ct.runMonitor();
        }
    }
}