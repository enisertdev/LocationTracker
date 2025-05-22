using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LocationTracker.Application.Interfaces.LocationInterface;
using Microsoft.EntityFrameworkCore;
using TaxiLocation.Domain.Entities;
using TaxiLocation.Persistance.Context;

namespace TaxiLocation.Persistance.Repositories.LocationRepository
{
    public class LocationReadRepository : ReadRepository<Location>, ILocationReadRepository
    {
        public LocationReadRepository(LocationTrackerDbContext context) : base(context)
        {
        }

        public IEnumerable<Location> GetAllLocationsWithUser()
        {
            return Table.Include(u => u.User).ToList();
        }

        public async Task<Location> GetLocationWithUserById(Guid id)
        {
            return await Table.Include(u => u.User).FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}
