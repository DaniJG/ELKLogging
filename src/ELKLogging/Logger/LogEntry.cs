using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELKLogging.Logger
{
    public class LogEntry
    {
        public DateTime DateTime { get; set; }
        public EventId EventId { get; set; }
        [Keyword]
        [JsonConverter(typeof(StringEnumConverter))]
        public Microsoft.Extensions.Logging.LogLevel Level { get; set; }
        [Keyword]
        public string Category { get; set; }
        public string Message { get; set; }

        [Keyword]
        public string TraceIdentifier { get; set; }
        [Keyword]
        public string UserName { get; set; }        
        [Keyword]
        public string ContentType { get; set; }
        [Keyword]
        public string Host { get; set; }         
        [Keyword]
        public string Method { get; set; }        
        [Keyword]
        public string Protocol { get; set; }
        [Keyword]
        public string Scheme { get; set; }
        public string Path { get; set; }
        public string PathBase { get; set; }
        public string QueryString { get; set; }
        public long? ContentLength { get; set; }
        public bool IsHttps { get; set; }
        public IRequestCookieCollection Cookies { get; set; }
        public IHeaderDictionary Headers { get; set; }

        [Keyword]
        public string ExceptionType { get; set; }        
        public string ExceptionMessage { get; set; }
        public string Exception { get; set; }
        public bool HasException { get { return Exception != null; } }
        public string StackTrace { get; set; }
    }
}
