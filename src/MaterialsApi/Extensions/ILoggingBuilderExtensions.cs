using Serilog;

namespace MaterialsApi.Extensions
{
    public static class ILoggingBuilderExtensions
    {
        public static void AddCustomLogger(this ILoggingBuilder logging, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
               .Enrich.FromLogContext()
               .CreateLogger();
            logging.ClearProviders();
            logging.AddSerilog(logger);
        }
    }
}