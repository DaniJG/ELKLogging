using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ELKLogging.ElasticSearch;
using Microsoft.AspNetCore.Http;

namespace ELKLogging.Logger
{
    public class ESLoggerProvider : ILoggerProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ESClientProvider _esClient;
        private readonly string _indexName;
        private readonly FilterLoggerSettings _filter;

        public ESLoggerProvider(IServiceProvider serviceProvider, string indexName = null, FilterLoggerSettings filter = null)
        {
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            _indexName = indexName;

            _esClient = serviceProvider.GetService<ESClientProvider>();
            _esClient.EnsureIndexWithMapping<LogEntry>(indexName);

            _filter = filter ?? new FilterLoggerSettings
            {
                {"*", LogLevel.Warning}
            };
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ESLogger(_esClient, _httpContextAccessor, categoryName, FindLevel(categoryName));
        }

        private LogLevel FindLevel(string categoryName)
        {
            var def = LogLevel.Warning;
            foreach (var s in _filter.Switches)
            {
                if (categoryName.Contains(s.Key)) return s.Value;
                if (s.Key == "*") def = s.Value;
            }

            return def;
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
