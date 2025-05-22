using TaxiLocation.Domain.Common;

namespace LocationTracker.Application.Interfaces
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(IEnumerable<T> models);
        bool Remove(T model);
        bool RemoveRange(IEnumerable<T> models);
        bool Update(T entity);
        Task<int> SaveChangesAsync();
    }
}
