using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLocation.Domain.Common;

namespace TaxiLocation.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public Location? Location { get; set; }
        


    }
}
