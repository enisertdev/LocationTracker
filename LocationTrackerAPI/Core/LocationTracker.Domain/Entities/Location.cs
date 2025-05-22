using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLocation.Domain.Common;

namespace TaxiLocation.Domain.Entities
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; } = 36.85037306169438;
        public double Longitude { get; set; } = 28.274083486624512;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
