using System;
using System.Collections.Generic;

namespace Hs.Domain.Entities.SampleDbEntities
{
    public partial class Log
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
        public string Properties { get; set; }
        public string SourceContext { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentUserName { get; set; }
        public string MachineName { get; set; }
        public string ClientIp { get; set; }
        public string ClientAgent { get; set; }
    }
}
