using System;
using System.Collections.Generic;

namespace Hs.Domain.Entities.SampleDbEntities
{
    public partial class AuditLog
    {
        public long Id { get; set; }
        public string Event { get; set; }
        public string Source { get; set; }
        public string Category { get; set; }
        public string SubjectIdentifier { get; set; }
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
        public string SubjectAdditionalData { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public DateTime Created { get; set; }
    }
}
