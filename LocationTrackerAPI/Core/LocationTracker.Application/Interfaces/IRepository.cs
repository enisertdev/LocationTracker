using Microsoft.EntityFrameworkCore;
using TaxiLocation.Domain.Common;

namespace LocationTracker.Application.Interfaces
{
    public interface IRepository<T>  where T : BaseEntity 
    {
        public DbSet<T> Table { get; }
    }
}
