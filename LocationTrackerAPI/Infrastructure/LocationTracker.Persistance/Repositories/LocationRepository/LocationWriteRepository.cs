using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationTracker.Application.Interfaces.LocationInterface;
using TaxiLocation.Domain.Entities;
using TaxiLocation.Persistance.Context;

namespace TaxiLocation.Persistance.Repositories.LocationRepository
{
    public class LocationWriteRepository : WriteRepository<Location>, ILocationWriteRepository
    {
        public LocationWriteRepository(LocationTrackerDbContext context) : base(context)
        {
        }
    }
}
