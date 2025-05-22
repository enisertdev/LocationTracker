using TaxiLocation.Domain.Entities;

namespace LocationTracker.Application.Interfaces.UserInterface
{
    public interface IUserReadRepository : IReadRepository<User>
    {
        Task<User> GetUserWithLocation(Guid id);
    }
}
