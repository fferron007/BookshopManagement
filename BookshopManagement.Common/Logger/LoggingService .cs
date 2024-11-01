using NLog.Extensions.Logging;

namespace BookshopManagement.Common.Logger
{
    public static class LoggingService
    {
        private static Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;
        private static Microsoft.Extensions.Logging.ILogger _logger;

        static LoggingService()
        {
            ConfigureLogger();
        }

        private static void ConfigureLogger()
        {
            // Ensure NLog uses the NLog.config file in the output directory
            NLog.LogManager.LoadConfiguration("NLog.config");

            _loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.AddNLog(); // This uses the configuration from NLog.config
            });

            _logger = _loggerFactory.CreateLogger("ApplicationLogger");
        }

        public static Microsoft.Extensions.Logging.ILogger Logger => _logger;
    }
}
