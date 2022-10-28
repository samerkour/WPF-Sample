using System;
using System.Collections.Generic;

namespace Hs.Domain.Entities.SampleDbEntities
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public short? Year { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
    }
}
