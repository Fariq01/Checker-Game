using log4net;
using log4net.Config;

namespace Loggerc
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public static void Configure()
        {
            XmlConfigurator.Configure(new FileInfo("Logger/log4net.config"));
        }

        public static void Debug(string message)
        {
            log.Debug(message);
        }

         public static void Info(string message)
        {
            log.Info(message);
        }

         public static void Error(string message)
        {
            log.Error(message);
        }
    }
}