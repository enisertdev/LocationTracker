using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaxiLocation.Domain.Entities;
using TaxiLocation.Domain.Entities.Identity;

namespace TaxiLocation.Persistance.Context
{
    public class LocationTrackerDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public LocationTrackerDbContext(DbContextOptions<LocationTrackerDbContext> options) : base(options)
        {
            
        }
    }
}
