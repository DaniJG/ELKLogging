using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELKLogging.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddESLogger(this ILoggerFactory factory, IServiceProvider serviceProvider, string indexName = null, FilterLoggerSettings filter = null)
        {
            factory.AddProvider(new ESLoggerProvider(serviceProvider, indexName, filter));
            return factory;
        }
    }
}
