using System.Linq.Expressions;
using TaxiLocation.Domain.Common;

namespace LocationTracker.Application.Interfaces
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetOneByIdAsync(Guid id, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    }
}
