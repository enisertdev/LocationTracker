using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaxiLocation.Persistance.Configuration;
using TaxiLocation.Persistance.Context;

namespace TaxiLocation.Persistance
{
    public class TaxiLocationContextFactory : IDesignTimeDbContextFactory<LocationTrackerDbContext>
    {
        public LocationTrackerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocationTrackerDbContext>();
            optionsBuilder.UseSqlServer(ConnectionConfiguration.ConnectionString);
            return new LocationTrackerDbContext(optionsBuilder.Options);
        }
    }
}
