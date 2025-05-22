using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationTracker.Application.Interfaces.UserInterface;
using TaxiLocation.Domain.Entities;
using TaxiLocation.Persistance.Context;

namespace TaxiLocation.Persistance.Repositories.UserRepository
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(LocationTrackerDbContext context) : base(context)
        {
        }
    }
}
