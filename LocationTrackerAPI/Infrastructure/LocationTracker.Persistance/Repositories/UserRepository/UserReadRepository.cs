using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationTracker.Application.Interfaces.UserInterface;
using Microsoft.EntityFrameworkCore;
using TaxiLocation.Domain.Entities;
using TaxiLocation.Persistance.Context;

namespace TaxiLocation.Persistance.Repositories.UserRepository
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        private readonly LocationTrackerDbContext _context;
        public UserReadRepository(LocationTrackerDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserWithLocation(Guid id)
        {
            return await Table.Include(l => l.Location).FirstOrDefaultAsync(data => data.Id == id);
        }
    }
}
